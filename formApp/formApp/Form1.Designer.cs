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
            this.btnReq = new System.Windows.Forms.Button();
            this.gb2 = new System.Windows.Forms.GroupBox();
            this.lbSpicesLent = new System.Windows.Forms.ListBox();
            this.btnRet = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lbSpicesStoring = new System.Windows.Forms.ListBox();
            this.lbSpicesLending = new System.Windows.Forms.ListBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnNewSpice = new System.Windows.Forms.Button();
            this.btnAddSpice = new System.Windows.Forms.Button();
            this.lbAdd = new System.Windows.Forms.ListBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnConn = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnVoiceReq = new System.Windows.Forms.Button();
            this.lbRemove = new System.Windows.Forms.ListBox();
            this.btnRemoveSpice = new System.Windows.Forms.Button();
            this.gb1.SuspendLayout();
            this.gb2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            // btnReq
            // 
            this.btnReq.Location = new System.Drawing.Point(11, 300);
            this.btnReq.Name = "btnReq";
            this.btnReq.Size = new System.Drawing.Size(226, 42);
            this.btnReq.TabIndex = 3;
            this.btnReq.Text = "Request";
            this.btnReq.UseVisualStyleBackColor = true;
            this.btnReq.Click += new System.EventHandler(this.btnReq_Click);
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
            this.lbSpicesLent.Location = new System.Drawing.Point(10, 30);
            this.lbSpicesLent.Name = "lbSpicesLent";
            this.lbSpicesLent.Size = new System.Drawing.Size(226, 254);
            this.lbSpicesLent.TabIndex = 2;
            // 
            // btnRet
            // 
            this.btnRet.Location = new System.Drawing.Point(10, 300);
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
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lbSpicesStoring);
            this.tabPage1.Controls.Add(this.lbSpicesLending);
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
            // lbSpicesStoring
            // 
            this.lbSpicesStoring.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lbSpicesStoring.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lbSpicesStoring.FormattingEnabled = true;
            this.lbSpicesStoring.ItemHeight = 25;
            this.lbSpicesStoring.Location = new System.Drawing.Point(270, 196);
            this.lbSpicesStoring.Name = "lbSpicesStoring";
            this.lbSpicesStoring.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lbSpicesStoring.Size = new System.Drawing.Size(256, 154);
            this.lbSpicesStoring.TabIndex = 4;
            // 
            // lbSpicesLending
            // 
            this.lbSpicesLending.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lbSpicesLending.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lbSpicesLending.FormattingEnabled = true;
            this.lbSpicesLending.ItemHeight = 25;
            this.lbSpicesLending.Location = new System.Drawing.Point(270, 24);
            this.lbSpicesLending.Name = "lbSpicesLending";
            this.lbSpicesLending.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lbSpicesLending.Size = new System.Drawing.Size(256, 154);
            this.lbSpicesLending.TabIndex = 3;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(8, 39);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(798, 371);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Restock";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnRemoveSpice);
            this.groupBox2.Controls.Add(this.lbRemove);
            this.groupBox2.Location = new System.Drawing.Point(404, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(388, 358);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Remove Spice";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnNewSpice);
            this.groupBox1.Controls.Add(this.btnAddSpice);
            this.groupBox1.Controls.Add(this.lbAdd);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(388, 358);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Add New Spice";
            // 
            // btnNewSpice
            // 
            this.btnNewSpice.Location = new System.Drawing.Point(197, 300);
            this.btnNewSpice.Name = "btnNewSpice";
            this.btnNewSpice.Size = new System.Drawing.Size(180, 42);
            this.btnNewSpice.TabIndex = 2;
            this.btnNewSpice.Text = "New";
            this.btnNewSpice.UseVisualStyleBackColor = true;
            this.btnNewSpice.Click += new System.EventHandler(this.btnNewSpice_Click);
            // 
            // btnAddSpice
            // 
            this.btnAddSpice.Location = new System.Drawing.Point(11, 300);
            this.btnAddSpice.Name = "btnAddSpice";
            this.btnAddSpice.Size = new System.Drawing.Size(180, 42);
            this.btnAddSpice.TabIndex = 1;
            this.btnAddSpice.Text = "Add";
            this.btnAddSpice.UseVisualStyleBackColor = true;
            this.btnAddSpice.Click += new System.EventHandler(this.btnAddSpice_Click);
            // 
            // lbAdd
            // 
            this.lbAdd.FormattingEnabled = true;
            this.lbAdd.ItemHeight = 25;
            this.lbAdd.Location = new System.Drawing.Point(10, 30);
            this.lbAdd.Name = "lbAdd";
            this.lbAdd.Size = new System.Drawing.Size(364, 254);
            this.lbAdd.TabIndex = 0;
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
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(25, 24);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(549, 33);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.DropDown += new System.EventHandler(this.comboBox1_DropDown);
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnVoiceReq
            // 
            this.btnVoiceReq.Location = new System.Drawing.Point(574, 4);
            this.btnVoiceReq.Name = "btnVoiceReq";
            this.btnVoiceReq.Size = new System.Drawing.Size(230, 42);
            this.btnVoiceReq.TabIndex = 5;
            this.btnVoiceReq.Text = "Voice Request";
            this.btnVoiceReq.UseVisualStyleBackColor = true;
            this.btnVoiceReq.Click += new System.EventHandler(this.btnVoiceReq_Click);
            // 
            // lbRemove
            // 
            this.lbRemove.FormattingEnabled = true;
            this.lbRemove.ItemHeight = 25;
            this.lbRemove.Location = new System.Drawing.Point(10, 30);
            this.lbRemove.Name = "lbRemove";
            this.lbRemove.Size = new System.Drawing.Size(364, 254);
            this.lbRemove.TabIndex = 3;
            // 
            // btnRemoveSpice
            // 
            this.btnRemoveSpice.Location = new System.Drawing.Point(10, 300);
            this.btnRemoveSpice.Name = "btnRemoveSpice";
            this.btnRemoveSpice.Size = new System.Drawing.Size(364, 42);
            this.btnRemoveSpice.TabIndex = 4;
            this.btnRemoveSpice.Text = "Remove";
            this.btnRemoveSpice.UseVisualStyleBackColor = true;
            this.btnRemoveSpice.Click += new System.EventHandler(this.btnRemoveSpice_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(835, 444);
            this.Controls.Add(this.btnVoiceReq);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.Text = "Spice Storage App";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.gb1.ResumeLayout(false);
            this.gb2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
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
        private System.Windows.Forms.ListBox lbSpicesStoring;
        private System.Windows.Forms.ListBox lbSpicesLending;
        private System.Windows.Forms.Button btnConn;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnVoiceReq;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lbAdd;
        private System.Windows.Forms.Button btnNewSpice;
        private System.Windows.Forms.Button btnAddSpice;
        private System.Windows.Forms.Button btnRemoveSpice;
        private System.Windows.Forms.ListBox lbRemove;
    }
}

