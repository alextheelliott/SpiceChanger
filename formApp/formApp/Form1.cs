using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace formApp
{
    public partial class Form1 : Form
    {
        SpiceManager spiceManager;

        public Form1()
        {
            spiceManager = SpiceManager.Instance;
            InitializeComponent();
            updateButtons();
            updateListBoxes();
        }

        private void btnReq_Click(object sender, EventArgs e)
        {
            if (lbSpicesStored.SelectedItem == null)
            { MessageBox.Show("Must first select a spice to request!","Error!"); return; }

            spiceManager.RequestSpice(lbSpicesStored.SelectedItem.ToString().Split(':')[0]);

            lbSpicesLent.Items.Add(lbSpicesStored.SelectedItem);
            lbSpicesStored.Items.Remove(lbSpicesStored.SelectedItem);
        }

        private void lbSpicesStored_SelectedValueChanged(object sender, EventArgs e)
        {
            if (lbSpicesStored.SelectedItem == null)
            { btnReq.Text = "Request:"; return; }

            btnReq.Text = $"Request: \n{lbSpicesStored.SelectedItem.ToString().Split(':')[0]}";
        }

        private void btnStartStopRet_Click(object sender, EventArgs e)
        {
            if (spiceManager.State == SpiceManagerState.Requesting)
            { // Start returning
                spiceManager.StartReturning();
            }
            else if (spiceManager.State == SpiceManagerState.Returning)
            { // Stop returning
                spiceManager.StopReturning();
            }
            updateButtons();
        }

        private void btnRet_Click(object sender, EventArgs e)
        {
            if (lbSpicesLent.SelectedItem == null)
            { MessageBox.Show("Must first select a spice to return!","Error!"); return; }

            spiceManager.ReturnSpice(lbSpicesLent.SelectedItem.ToString().Split(':')[0]);
            
            lbSpicesStored.Items.Add(lbSpicesLent.SelectedItem);
            lbSpicesLent.Items.Remove(lbSpicesLent.SelectedItem);
        }

        private void lbSpicesLent_SelectedValueChanged(object sender, EventArgs e)
        {
            if (lbSpicesLent.SelectedItem == null)
            { btnRet.Text = "Return:"; return; }

            btnRet.Text = $"Return: \n{lbSpicesLent.SelectedItem.ToString().Split(':')[0]}";
        }

        private void btnStartStopRes_Click(object sender, EventArgs e)
        {
            if (spiceManager.State == SpiceManagerState.Requesting)
            { // Start restocking
                spiceManager.StartRestocking();
            }
            else if (spiceManager.State == SpiceManagerState.Restocking)
            { // Stop restocking
                spiceManager.StopRestocking();
            }
            updateButtons();
        }

        private void btnRes_Click(object sender, EventArgs e)
        {

        }

        private void btnResRem_Click(object sender, EventArgs e)
        {

        }

        private void updateButtons()
        {
            switch (spiceManager.State)
            {
                case SpiceManagerState.Requesting:
                    btnReq.Enabled = true;
                    btnStartStopRet.Enabled = true;
                    btnStartStopRet.Text = "Start Returning";
                    btnRet.Enabled = false;
                    btnStartStopRes.Enabled = true;
                    btnStartStopRes.Text = "Start Restocking";
                    btnRes.Enabled = false;
                    break;
                case SpiceManagerState.Returning:
                    btnReq.Enabled = false;
                    btnStartStopRet.Enabled = true;
                    btnStartStopRet.Text = "Stop Returning";
                    btnRet.Enabled = true;
                    btnStartStopRes.Enabled = false;
                    btnRes.Enabled = false;
                    break;
                case SpiceManagerState.Restocking:
                    btnReq.Enabled = false;
                    btnStartStopRet.Enabled = false;
                    btnRet.Enabled = false;
                    btnStartStopRes.Enabled = true;
                    btnStartStopRes.Text = "Stop Restocking";
                    btnRes.Enabled = true;
                    break;
            }
        }

        private void updateListBoxes()
        {
            lbSpicesStored.Items.Clear();
            foreach (KeyValuePair<string, int> entry in spiceManager.SpicesStored)
            { lbSpicesStored.Items.Add($"{entry.Key}: {entry.Value}"); }

            lbSpicesLent.Items.Clear();
            foreach (KeyValuePair<string, int> entry in spiceManager.SpicesLent)
            { lbSpicesLent.Items.Add($"{entry.Key}: {entry.Value}"); }
        }

    }
}
