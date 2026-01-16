namespace formApp
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gb1 = new System.Windows.Forms.GroupBox();
            this.lbSpicesStored = new System.Windows.Forms.ListBox();
            this.gb2 = new System.Windows.Forms.GroupBox();
            this.lbSpicesLent = new System.Windows.Forms.ListBox();
            this.btnReq = new System.Windows.Forms.Button();
            this.btnRet = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.lbSpicesRequesting = new System.Windows.Forms.ListBox();
            this.lbSpicesReturning = new System.Windows.Forms.ListBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btnConn = new System.Windows.Forms.Button();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.gb1.SuspendLayout();
            this.gb2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb1
            // 
            this.gb1.Controls.Add(this.lbSpicesStored);
            this.gb1.Controls.Add(this.btnReq);
            this.gb1.Location = new System.Drawing.Point(6, 6);
            this.gb1.Name = "gb1";
            this.gb1.Size = new System.Drawing.Size(250, 358);
            this.gb1.TabIndex = 0;
            this.gb1.TabStop = false;
            this.gb1.Text = "Spices Stored";
            // 
            // lbSpicesStored
            // 
            this.lbSpicesStored.FormattingEnabled = true;
            this.lbSpicesStored.ItemHeight = 25;
            this.lbSpicesStored.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
            this.lbSpicesStored.Location = new System.Drawing.Point(10, 30);
            this.lbSpicesStored.Name = "lbSpicesStored";
            this.lbSpicesStored.Size = new System.Drawing.Size(226, 254);
            this.lbSpicesStored.TabIndex = 1;
            // 
            // gb2
            // 
            this.gb2.Controls.Add(this.lbSpicesLent);
            this.gb2.Controls.Add(this.btnRet);
            this.gb2.Location = new System.Drawing.Point(542, 6);
            this.gb2.Name = "gb2";
            this.gb2.Size = new System.Drawing.Size(250, 358);
            this.gb2.TabIndex = 2;
            this.gb2.TabStop = false;
            this.gb2.Text = "Spices Lent";
            // 
            // lbSpicesLent
            // 
            this.lbSpicesLent.FormattingEnabled = true;
            this.lbSpicesLent.ItemHeight = 25;
            this.lbSpicesLent.Items.AddRange(new object[] {
            ""});
            this.lbSpicesLent.Location = new System.Drawing.Point(12, 30);
            this.lbSpicesLent.Name = "lbSpicesLent";
            this.lbSpicesLent.Size = new System.Drawing.Size(226, 254);
            this.lbSpicesLent.TabIndex = 2;
            // 
            // btnReq
            // 
            this.btnReq.Location = new System.Drawing.Point(10, 300);
            this.btnReq.Name = "btnReq";
            this.btnReq.Size = new System.Drawing.Size(226, 42);
            this.btnReq.TabIndex = 3;
            this.btnReq.Text = "Request";
            this.btnReq.UseVisualStyleBackColor = true;
            this.btnReq.Click += new System.EventHandler(this.btnReq_Click);
            // 
            // btnRet
            // 
            this.btnRet.Location = new System.Drawing.Point(12, 300);
            this.btnRet.Name = "btnRet";
            this.btnRet.Size = new System.Drawing.Size(230, 42);
            this.btnRet.TabIndex = 5;
            this.btnRet.Text = "Return";
            this.btnRet.UseVisualStyleBackColor = true;
            this.btnRet.Click += new System.EventHandler(this.btnRet_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(814, 418);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lbSpicesReturning);
            this.tabPage1.Controls.Add(this.lbSpicesRequesting);
            this.tabPage1.Controls.Add(this.gb1);
            this.tabPage1.Controls.Add(this.gb2);
            this.tabPage1.Location = new System.Drawing.Point(8, 39);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(798, 371);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Request";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(8, 39);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(798, 371);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Restock";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnConn);
            this.tabPage3.Controls.Add(this.comboBox1);
            this.tabPage3.Location = new System.Drawing.Point(8, 39);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(798, 371);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "COM";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // lbSpicesRequesting
            // 
            this.lbSpicesRequesting.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lbSpicesRequesting.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lbSpicesRequesting.FormattingEnabled = true;
            this.lbSpicesRequesting.ItemHeight = 25;
            this.lbSpicesRequesting.Location = new System.Drawing.Point(271, 24);
            this.lbSpicesRequesting.Name = "lbSpicesRequesting";
            this.lbSpicesRequesting.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lbSpicesRequesting.Size = new System.Drawing.Size(256, 154);
            this.lbSpicesRequesting.TabIndex = 3;
            // 
            // lbSpicesReturning
            // 
            this.lbSpicesReturning.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lbSpicesReturning.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lbSpicesReturning.FormattingEnabled = true;
            this.lbSpicesReturning.ItemHeight = 25;
            this.lbSpicesReturning.Location = new System.Drawing.Point(271, 196);
            this.lbSpicesReturning.Name = "lbSpicesReturning";
            this.lbSpicesReturning.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lbSpicesReturning.Size = new System.Drawing.Size(256, 154);
            this.lbSpicesReturning.TabIndex = 4;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(25, 24);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(549, 33);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.DropDown += new System.EventHandler(this.comboBox1_DropDown);
            // 
            // btnConn
            // 
            this.btnConn.Location = new System.Drawing.Point(590, 24);
            this.btnConn.Name = "btnConn";
            this.btnConn.Size = new System.Drawing.Size(183, 45);
            this.btnConn.TabIndex = 1;
            this.btnConn.Text = "Connect";
            this.btnConn.UseVisualStyleBackColor = true;
            this.btnConn.Click += new System.EventHandler(this.btnConn_Click);
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(835, 444);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.Text = "Spice Storage App";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.gb1.ResumeLayout(false);
            this.gb2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gb1;
        private System.Windows.Forms.ListBox lbSpicesStored;
        private System.Windows.Forms.GroupBox gb2;
        private System.Windows.Forms.ListBox lbSpicesLent;
        private System.Windows.Forms.Button btnReq;
        private System.Windows.Forms.Button btnRet;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ListBox lbSpicesReturning;
        private System.Windows.Forms.ListBox lbSpicesRequesting;
        private System.Windows.Forms.Button btnConn;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Timer timer1;
    }
}

