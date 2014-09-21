namespace WindowsFormsApplication1
{
    partial class Add_Command
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
            this.cboCommand = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDivId = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbView = new System.Windows.Forms.RadioButton();
            this.rbJson = new System.Windows.Forms.RadioButton();
            this.txtViewname = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtActionKey = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboCommand
            // 
            this.cboCommand.FormattingEnabled = true;
            this.cboCommand.Location = new System.Drawing.Point(70, 59);
            this.cboCommand.Name = "cboCommand";
            this.cboCommand.Size = new System.Drawing.Size(408, 21);
            this.cboCommand.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Command";
            // 
            // txtDivId
            // 
            this.txtDivId.Location = new System.Drawing.Point(105, 35);
            this.txtDivId.Name = "txtDivId";
            this.txtDivId.Size = new System.Drawing.Size(248, 20);
            this.txtDivId.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Div ID";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbView);
            this.groupBox2.Controls.Add(this.rbJson);
            this.groupBox2.Location = new System.Drawing.Point(6, 97);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(472, 41);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Result Type";
            // 
            // rbView
            // 
            this.rbView.AutoSize = true;
            this.rbView.Location = new System.Drawing.Point(78, 18);
            this.rbView.Name = "rbView";
            this.rbView.Size = new System.Drawing.Size(80, 17);
            this.rbView.TabIndex = 0;
            this.rbView.Text = "Partial View";
            this.rbView.UseVisualStyleBackColor = true;
            // 
            // rbJson
            // 
            this.rbJson.AutoSize = true;
            this.rbJson.Checked = true;
            this.rbJson.Location = new System.Drawing.Point(16, 18);
            this.rbJson.Name = "rbJson";
            this.rbJson.Size = new System.Drawing.Size(47, 17);
            this.rbJson.TabIndex = 1;
            this.rbJson.TabStop = true;
            this.rbJson.Text = "Json";
            this.rbJson.UseVisualStyleBackColor = true;
            this.rbJson.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // txtViewname
            // 
            this.txtViewname.Location = new System.Drawing.Point(106, 9);
            this.txtViewname.Name = "txtViewname";
            this.txtViewname.Size = new System.Drawing.Size(247, 20);
            this.txtViewname.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Result View Name";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(372, 144);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(106, 69);
            this.btnAdd.TabIndex = 15;
            this.btnAdd.Text = "ADD";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtDivId);
            this.panel1.Controls.Add(this.txtViewname);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(6, 144);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(360, 69);
            this.panel1.TabIndex = 16;
            this.panel1.TabStop = true;
            this.panel1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Action Key";
            // 
            // txtActionKey
            // 
            this.txtActionKey.Location = new System.Drawing.Point(70, 21);
            this.txtActionKey.Name = "txtActionKey";
            this.txtActionKey.Size = new System.Drawing.Size(408, 20);
            this.txtActionKey.TabIndex = 18;
            // 
            // Add_Command
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 225);
            this.Controls.Add(this.txtActionKey);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboCommand);
            this.Controls.Add(this.panel1);
            this.Name = "Add_Command";
            this.Text = "Add New Command";
            this.Load += new System.EventHandler(this.Add_Command_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboCommand;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDivId;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbView;
        private System.Windows.Forms.RadioButton rbJson;
        private System.Windows.Forms.TextBox txtViewname;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtActionKey;

    }
}