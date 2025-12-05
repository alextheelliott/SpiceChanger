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
            this.gb1 = new System.Windows.Forms.GroupBox();
            this.lbSpicesStored = new System.Windows.Forms.ListBox();
            this.gb2 = new System.Windows.Forms.GroupBox();
            this.lbSpicesLent = new System.Windows.Forms.ListBox();
            this.btnReq = new System.Windows.Forms.Button();
            this.btnStartStopRet = new System.Windows.Forms.Button();
            this.btnRet = new System.Windows.Forms.Button();
            this.btnStartStopRes = new System.Windows.Forms.Button();
            this.tbSpiceToRestock = new System.Windows.Forms.TextBox();
            this.btnRes = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnResRem = new System.Windows.Forms.Button();
            this.gb1.SuspendLayout();
            this.gb2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb1
            // 
            this.gb1.Controls.Add(this.lbSpicesStored);
            this.gb1.Location = new System.Drawing.Point(12, 12);
            this.gb1.Name = "gb1";
            this.gb1.Size = new System.Drawing.Size(250, 200);
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
            this.lbSpicesStored.Size = new System.Drawing.Size(226, 154);
            this.lbSpicesStored.TabIndex = 1;
            this.lbSpicesStored.SelectedValueChanged += new System.EventHandler(this.lbSpicesStored_SelectedValueChanged);
            // 
            // gb2
            // 
            this.gb2.Controls.Add(this.lbSpicesLent);
            this.gb2.Location = new System.Drawing.Point(273, 12);
            this.gb2.Name = "gb2";
            this.gb2.Size = new System.Drawing.Size(250, 200);
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
            this.lbSpicesLent.Size = new System.Drawing.Size(226, 154);
            this.lbSpicesLent.TabIndex = 2;
            this.lbSpicesLent.SelectedValueChanged += new System.EventHandler(this.lbSpicesLent_SelectedValueChanged);
            // 
            // btnReq
            // 
            this.btnReq.Location = new System.Drawing.Point(12, 218);
            this.btnReq.Name = "btnReq";
            this.btnReq.Size = new System.Drawing.Size(250, 153);
            this.btnReq.TabIndex = 3;
            this.btnReq.Text = "Request:";
            this.btnReq.UseVisualStyleBackColor = true;
            this.btnReq.Click += new System.EventHandler(this.btnReq_Click);
            // 
            // btnStartStopRet
            // 
            this.btnStartStopRet.Location = new System.Drawing.Point(273, 218);
            this.btnStartStopRet.Name = "btnStartStopRet";
            this.btnStartStopRet.Size = new System.Drawing.Size(250, 47);
            this.btnStartStopRet.TabIndex = 4;
            this.btnStartStopRet.Text = "Start Returning";
            this.btnStartStopRet.UseVisualStyleBackColor = true;
            this.btnStartStopRet.Click += new System.EventHandler(this.btnStartStopRet_Click);
            // 
            // btnRet
            // 
            this.btnRet.Location = new System.Drawing.Point(273, 271);
            this.btnRet.Name = "btnRet";
            this.btnRet.Size = new System.Drawing.Size(250, 100);
            this.btnRet.TabIndex = 5;
            this.btnRet.Text = "Return:";
            this.btnRet.UseVisualStyleBackColor = true;
            this.btnRet.Click += new System.EventHandler(this.btnRet_Click);
            // 
            // btnStartStopRes
            // 
            this.btnStartStopRes.Location = new System.Drawing.Point(6, 30);
            this.btnStartStopRes.Name = "btnStartStopRes";
            this.btnStartStopRes.Size = new System.Drawing.Size(242, 91);
            this.btnStartStopRes.TabIndex = 6;
            this.btnStartStopRes.Text = "Start Restocking";
            this.btnStartStopRes.UseVisualStyleBackColor = true;
            this.btnStartStopRes.Click += new System.EventHandler(this.btnStartStopRes_Click);
            // 
            // tbSpiceToRestock
            // 
            this.tbSpiceToRestock.Location = new System.Drawing.Point(20, 132);
            this.tbSpiceToRestock.Name = "tbSpiceToRestock";
            this.tbSpiceToRestock.Size = new System.Drawing.Size(215, 31);
            this.tbSpiceToRestock.TabIndex = 7;
            // 
            // btnRes
            // 
            this.btnRes.Location = new System.Drawing.Point(6, 174);
            this.btnRes.Name = "btnRes";
            this.btnRes.Size = new System.Drawing.Size(242, 86);
            this.btnRes.TabIndex = 8;
            this.btnRes.Text = "Add to Stock";
            this.btnRes.UseVisualStyleBackColor = true;
            this.btnRes.Click += new System.EventHandler(this.btnRes_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnResRem);
            this.groupBox1.Controls.Add(this.btnStartStopRes);
            this.groupBox1.Controls.Add(this.btnRes);
            this.groupBox1.Controls.Add(this.tbSpiceToRestock);
            this.groupBox1.Location = new System.Drawing.Point(534, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(254, 359);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Restock Spices";
            // 
            // btnResRem
            // 
            this.btnResRem.Location = new System.Drawing.Point(6, 266);
            this.btnResRem.Name = "btnResRem";
            this.btnResRem.Size = new System.Drawing.Size(242, 86);
            this.btnResRem.TabIndex = 9;
            this.btnResRem.Text = "Remove from Stock";
            this.btnResRem.UseVisualStyleBackColor = true;
            this.btnResRem.Click += new System.EventHandler(this.btnResRem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 387);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnRet);
            this.Controls.Add(this.btnStartStopRet);
            this.Controls.Add(this.btnReq);
            this.Controls.Add(this.gb2);
            this.Controls.Add(this.gb1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.Text = "Spice Storage App";
            this.gb1.ResumeLayout(false);
            this.gb2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gb1;
        private System.Windows.Forms.ListBox lbSpicesStored;
        private System.Windows.Forms.GroupBox gb2;
        private System.Windows.Forms.ListBox lbSpicesLent;
        private System.Windows.Forms.Button btnReq;
        private System.Windows.Forms.Button btnStartStopRet;
        private System.Windows.Forms.Button btnRet;
        private System.Windows.Forms.Button btnStartStopRes;
        private System.Windows.Forms.TextBox tbSpiceToRestock;
        private System.Windows.Forms.Button btnRes;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnResRem;
    }
}

