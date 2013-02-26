namespace CollabClient
{
    partial class Main
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
            this.pUsers = new System.Windows.Forms.Panel();
            this.cbStatus = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lUserName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tUserMessage = new System.Windows.Forms.TextBox();
            this.bUpdateUserMessage = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pUsers
            // 
            this.pUsers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pUsers.AutoScroll = true;
            this.pUsers.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pUsers.Location = new System.Drawing.Point(12, 193);
            this.pUsers.Name = "pUsers";
            this.pUsers.Size = new System.Drawing.Size(253, 369);
            this.pUsers.TabIndex = 0;
            // 
            // cbStatus
            // 
            this.cbStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbStatus.FormattingEnabled = true;
            this.cbStatus.Items.AddRange(new object[] {
            "Busy",
            "Available",
            "I Need Help"});
            this.cbStatus.Location = new System.Drawing.Point(12, 56);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.Size = new System.Drawing.Size(253, 26);
            this.cbStatus.TabIndex = 1;
            this.cbStatus.SelectedIndexChanged += new System.EventHandler(this.cbStatus_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "My Status";
            // 
            // lUserName
            // 
            this.lUserName.AutoSize = true;
            this.lUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lUserName.Location = new System.Drawing.Point(12, 11);
            this.lUserName.Name = "lUserName";
            this.lUserName.Size = new System.Drawing.Size(163, 24);
            this.lUserName.TabIndex = 3;
            this.lUserName.Text = "[Your Name Here]";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 18);
            this.label2.TabIndex = 4;
            this.label2.Text = "Message";
            // 
            // tUserMessage
            // 
            this.tUserMessage.Location = new System.Drawing.Point(12, 116);
            this.tUserMessage.Name = "tUserMessage";
            this.tUserMessage.Size = new System.Drawing.Size(253, 20);
            this.tUserMessage.TabIndex = 5;
            // 
            // bUpdateUserMessage
            // 
            this.bUpdateUserMessage.Location = new System.Drawing.Point(167, 142);
            this.bUpdateUserMessage.Name = "bUpdateUserMessage";
            this.bUpdateUserMessage.Size = new System.Drawing.Size(98, 26);
            this.bUpdateUserMessage.TabIndex = 6;
            this.bUpdateUserMessage.Text = "Save Message";
            this.bUpdateUserMessage.UseVisualStyleBackColor = true;
            this.bUpdateUserMessage.Click += new System.EventHandler(this.bUpdateUserMessage_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(277, 574);
            this.Controls.Add(this.bUpdateUserMessage);
            this.Controls.Add(this.tUserMessage);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lUserName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbStatus);
            this.Controls.Add(this.pUsers);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.Text = "Collaboration Tool Demo";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pUsers;
        private System.Windows.Forms.ComboBox cbStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lUserName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tUserMessage;
        private System.Windows.Forms.Button bUpdateUserMessage;
    }
}

