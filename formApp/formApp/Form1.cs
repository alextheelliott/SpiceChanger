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
            updateListBoxes();
            
            spiceManager.UpdateListBox(SpiceManager.SpiceState.Stored,  lbSpicesStored);
            spiceManager.UpdateListBox(SpiceManager.SpiceState.Lending, lbSpicesLending);
            spiceManager.UpdateListBox(SpiceManager.SpiceState.Lent,    lbSpicesLent);
            spiceManager.UpdateListBox(SpiceManager.SpiceState.Storing, lbSpicesStoring);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();

            recognizer.SetInputToDefaultAudioDevice();
            recognizer.UnloadAllGrammars();
            recognizer.LoadGrammar(spiceManager.BuildGrammer());
            recognizer.SpeechRecognized += handleSpeechRecognized;
        }

        private void handleSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (lbSpicesStored.FindStringExact(e.Result.Text) != ListBox.NoMatches)
            {
                resetTimeout();
                object spice = lbSpicesStored.Items[lbSpicesStored.FindStringExact(e.Result.Text)];
                requestSpice(spice);
            }
        }

        private void btnVoiceReq_Click(object sender, EventArgs e)
        {
            Console.WriteLine("STARTING");
            recognizer.RecognizeAsync(RecognizeMode.Multiple);
            resetTimeout();
        }

        private async void resetTimeout()
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

        private void requestSpice(object spice)
        {
            int index = spiceManager.UpdateState(spice.ToString(), SpiceManager.SpiceState.Lending);
            sendPacket(SpiceManager.Commands.Request, index);
        }

        private void returnSpice(object spice)
        {
            int index = spiceManager.UpdateState(spice.ToString(), SpiceManager.SpiceState.Storing);
            sendPacket(SpiceManager.Commands.Return, index);
        }

        private void btnReq_Click(object sender, EventArgs e)
        {
            if (lbSpicesStored.SelectedItem == null)
            { MessageBox.Show("Must first select a spice to request!","Error!"); return; }

            requestSpice(lbSpicesStored.SelectedItem);
        }

        private void btnRet_Click(object sender, EventArgs e)
        {
            if (lbSpicesLent.SelectedItem == null)
            { MessageBox.Show("Must first select a spice to return!","Error!"); return; }

            returnSpice(lbSpicesLent.SelectedItem);
        }

        private void updateListBoxes()
        {
            lbSpicesStored.Items.Clear();
            lbSpicesLending.Items.Clear();
            lbSpicesLent.Items.Clear();
            lbSpicesStoring.Items.Clear();

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
                }
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

        private void sendPacket(SpiceManager.Commands command, int index)
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
                    if (item == 0)
                    {
                        spiceManager.UpdateState(
                            lbSpicesLending.Items[0].ToString(),
                            SpiceManager.SpiceState.Lent
                        );
                    }
                    else if (item == 1)
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
            updateListBoxes();
        }

    }
}
