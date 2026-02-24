using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Speech.Recognition;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace formApp
{
    public partial class Form1 : Form
    {
        SpiceManager spiceManager;
        private ConcurrentQueue<Int32> dataQueue;

        private SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine();
        private CancellationTokenSource voiceTimeout;

        public Form1()
        {
            spiceManager = SpiceManager.Instance;
            dataQueue = new ConcurrentQueue<Int32>();

            InitializeComponent();
            UpdateListBoxes();
            
            spiceManager.UpdateListBox(SpiceManager.SpiceState.Stored,   lbSpicesStored);
            spiceManager.UpdateListBox(SpiceManager.SpiceState.Lending,  lbSpicesLending);
            spiceManager.UpdateListBox(SpiceManager.SpiceState.Lent,     lbSpicesLent);
            spiceManager.UpdateListBox(SpiceManager.SpiceState.Storing,  lbSpicesStoring);
            spiceManager.UpdateListBox(SpiceManager.SpiceState.Removing, lbSpicesLending);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            SetupGrammer();
        }

        private void SetupGrammer()
        {
            recognizer.SetInputToDefaultAudioDevice();
            recognizer.UnloadAllGrammars();
            recognizer.LoadGrammar(spiceManager.BuildGrammer());
            recognizer.SpeechRecognized += HandleSpeechRecognized;
        }

        private void HandleSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (lbSpicesStored.FindStringExact(e.Result.Text) != ListBox.NoMatches)
            {
                ResetTimeout();
                object spice = lbSpicesStored.Items[lbSpicesStored.FindStringExact(e.Result.Text)];
                RequestSpice(spice);
            }
        }

        private void btnVoiceReq_Click(object sender, EventArgs e)
        {
            Console.WriteLine("STARTING");
            recognizer.RecognizeAsync(RecognizeMode.Multiple);
            ResetTimeout();
        }

        private async void ResetTimeout()
        {
            voiceTimeout?.Cancel();
            voiceTimeout = new CancellationTokenSource();
            var token = voiceTimeout.Token;

            try
            {
                await Task.Delay(4000, token); // 4 seconds from LAST speech
                voiceTimeout?.Cancel();
                recognizer.RecognizeAsyncStop();
            }
            catch (TaskCanceledException) { }
            // expected when speech happens again
        }

        private void RequestSpice(object spice)
        {
            int index = spiceManager.UpdateState(spice.ToString(), SpiceManager.SpiceState.Lending);
            SendPacket(SpiceManager.Commands.Request, index);
        }

        private void ReturnSpice(object spice)
        {
            int index = spiceManager.UpdateState(spice.ToString(), SpiceManager.SpiceState.Storing);
            SendPacket(SpiceManager.Commands.Return, index);
        }

        private void btnReq_Click(object sender, EventArgs e)
        {
            if (lbSpicesStored.SelectedItem == null)
            { MessageBox.Show("Must first select a spice to request!","Error!"); return; }

            RequestSpice(lbSpicesStored.SelectedItem);
        }

        private void btnRet_Click(object sender, EventArgs e)
        {
            if (lbSpicesLent.SelectedItem == null)
            { MessageBox.Show("Must first select a spice to return!","Error!"); return; }

            ReturnSpice(lbSpicesLent.SelectedItem);
        }

        private void UpdateListBoxes()
        {
            lbSpicesStored.Items.Clear();
            lbSpicesLending.Items.Clear();
            lbSpicesLent.Items.Clear();
            lbSpicesStoring.Items.Clear();
            
            lbAdd.Items.Clear();
            lbRemove.Items.Clear();

            foreach (KeyValuePair<string,(string,int,SpiceManager.SpiceState)> entry in spiceManager.State)
            {
                (string name, _, SpiceManager.SpiceState state) = entry.Value;

                switch (state)
                {
                    case SpiceManager.SpiceState.Stored:
                        lbSpicesStored.Items.Add(name);
                        break;
                    case SpiceManager.SpiceState.Lending:
                        lbSpicesLending.Items.Add(name);
                        break;
                    case SpiceManager.SpiceState.Lent:
                        lbSpicesLent.Items.Add(name);
                        break;
                    case SpiceManager.SpiceState.Storing:
                        lbSpicesStoring.Items.Add(name);
                        break;
                    case SpiceManager.SpiceState.Removing:
                        lbSpicesLending.Items.Add(name);
                        break;
                }

                lbRemove.Items.Add(name);
            }

            foreach(string spice in spiceManager.Options)
            {
                lbAdd.Items.Add(spice);
            }
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());
            if (comboBox1.Items.Count == 0)
                comboBox1.Text = "No COM ports!";
            else
                comboBox1.SelectedIndex = 0;
        }

        private void btnConn_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            { // then close port
                serialPort1.Close();
                btnConn.Text = "Connect";
            }
            else if (!serialPort1.IsOpen)
            { // then open port
                if (comboBox1.Text == "No COM ports!" || comboBox1.Text == "")
                {
                    MessageBox.Show("Must select a valid port!");
                }
                else
                {
                    serialPort1.PortName = comboBox1.Text;
                    serialPort1.Open();
                    btnConn.Text = "Stop";
                }
            }
        }

        private void SendPacket(SpiceManager.Commands command, int index)
        {
            byte[] bytes = new byte[3];
            bytes[0] = 255;
            bytes[1] = (byte) command;
            bytes[2] = (byte) index;

            try
            { serialPort1.Write(bytes, 0, 3); }
            catch
            { Console.WriteLine("Error! Serial port could not write"); }
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            int newByte = 0;
            int bytesToRead = 1;

            while (bytesToRead != 0)
            {
                try
                {
                    newByte = serialPort1.ReadByte();
                    dataQueue.Enqueue(newByte);
                    bytesToRead = serialPort1.BytesToRead; 
                }
                catch { bytesToRead = 0; }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int startCount = dataQueue.Count;
            while(startCount > 0)
            {
                if (dataQueue.TryDequeue(out int item))
                {
                    startCount--;
                    if (item == 1)
                    {
                        (string spice, int index, SpiceManager.SpiceState state) = spiceManager.State[lbSpicesLending.Items[0].ToString()];
                        if (state != SpiceManager.SpiceState.Removing)
                        {
                            spiceManager.UpdateState(
                                spice,
                                SpiceManager.SpiceState.Lent
                            );
                        }
                        else
                        {
                            spiceManager.RemoveSpice(spice);
                        }
                    }
                    else if (item == 2)
                    {
                        spiceManager.UpdateState(
                            lbSpicesStoring.Items[0].ToString(),
                            SpiceManager.SpiceState.Stored
                        );
                    }
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateListBoxes();
        }

        private void btnAddSpice_Click(object sender, EventArgs e)
        {
            if (lbAdd.SelectedItem == null)
            { MessageBox.Show("Must first select a spice to add!","Error!"); return; }

            int index = spiceManager.AddSpice(lbAdd.SelectedItem.ToString());
            if (index == -1)
            { MessageBox.Show("Spice already added or out of space, duplicates are not allowed!","Error!"); return; }
            SendPacket(SpiceManager.Commands.Return, index);

            UpdateListBoxes();
        }

        private void btnNewSpice_Click(object sender, EventArgs e)
        {
            string newSpice = NewSpicePrompt.ShowDialog("Enter New Spice Option:","Add New");
            
            if (newSpice == "")
            { return; }
            if (!spiceManager.AddOption(newSpice))
            { MessageBox.Show($"{newSpice} already exists as an option!","Error!"); return; }
            SetupGrammer();

            int index = spiceManager.AddSpice(newSpice);
            if (index == -1)
            { MessageBox.Show("Spice already added or out of space, duplicates are not allowed!","Error!"); return; }
            SendPacket(SpiceManager.Commands.Return, index);
            
            UpdateListBoxes();
        }

        private void btnRemoveSpice_Click(object sender, EventArgs e)
        {
            (_, _, SpiceManager.SpiceState state) = spiceManager.State[lbRemove.SelectedItem.ToString()];
            if (state == SpiceManager.SpiceState.Removing)
            { MessageBox.Show("Spice already queued for removal!","Error!"); return; }
            else if (state == SpiceManager.SpiceState.Stored) 
            {
                int index = spiceManager.UpdateState(
                    lbRemove.SelectedItem.ToString(),
                    SpiceManager.SpiceState.Removing
                );
                SendPacket(SpiceManager.Commands.Request, index);
            }
            else if (state == SpiceManager.SpiceState.Lent)
            { spiceManager.RemoveSpice(lbRemove.SelectedItem.ToString()); }
            else
            { MessageBox.Show("Spice is not in a removable state! (Stored or Lent) Please finish previous action.","Error!"); return; }

            MessageBox.Show("Spice removal has been queued!","Success!");
            UpdateListBoxes();
        }

        private void btnZero_Click(object sender, EventArgs e)
        {
            SendPacket(SpiceManager.Commands.Request,9);
        }
    }
}
