using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace formApp
{
    public partial class Form1 : Form
    {
        SpiceManager spiceManager;
        private ConcurrentQueue<Int32> dataQueue;

        public Form1()
        {
            spiceManager = SpiceManager.Instance;
            dataQueue = new ConcurrentQueue<Int32>();

            InitializeComponent();
            updateListBoxes();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void btnReq_Click(object sender, EventArgs e)
        {
            if (lbSpicesStored.SelectedItem == null)
            { MessageBox.Show("Must first select a spice to request!","Error!"); return; }

            int index = spiceManager.RequestSpice(lbSpicesStored.SelectedItem.ToString());
            Console.WriteLine(index.ToString());
            sendPacket(index);

            lbSpicesRequesting.Items.Add(lbSpicesStored.SelectedItem);
            lbSpicesStored.Items.Remove(lbSpicesStored.SelectedItem);
        }

        private void btnRet_Click(object sender, EventArgs e)
        {
            if (lbSpicesLent.SelectedItem == null)
            { MessageBox.Show("Must first select a spice to return!","Error!"); return; }

            int index = spiceManager.ReturnSpice(lbSpicesLent.SelectedItem.ToString());
            Console.WriteLine(index.ToString());
            sendPacket(index);
            
            lbSpicesReturning.Items.Add(lbSpicesLent.SelectedItem);
            lbSpicesLent.Items.Remove(lbSpicesLent.SelectedItem);
        }

        private void updateListBoxes()
        {
            lbSpicesStored.Items.Clear();
            foreach (KeyValuePair<string, int> entry in spiceManager.SpicesStored)
            { lbSpicesStored.Items.Add($"{entry.Key}"); }

            lbSpicesLent.Items.Clear();
            foreach (KeyValuePair<string, int> entry in spiceManager.SpicesLent)
            { lbSpicesLent.Items.Add($"{entry.Key}"); }
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

        private void sendPacket(int index)
        {
            byte[] bytes = new byte[2];
            bytes[0] = 255;
            bytes[1] = (byte) index;
            serialPort1.Write(bytes, 0, 2);
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
                        spiceManager.ConfirmReturnSpice(lbSpicesRequesting.Items[0].ToString());
                        
                        lbSpicesLent.Items.Add(lbSpicesRequesting.Items[0]);
                        lbSpicesRequesting.Items.Remove(lbSpicesRequesting.Items[0]);
                    }
                    else if (item == 1)
                    {
                        spiceManager.ConfirmReturnSpice(lbSpicesReturning.Items[0].ToString());
                        
                        lbSpicesStored.Items.Add(lbSpicesReturning.Items[0]);
                        lbSpicesReturning.Items.Remove(lbSpicesReturning.Items[0]);
                    }
                }
            }
        }
        
    }
}
