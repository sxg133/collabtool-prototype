namespace UserPanel
{
    partial class UserPane
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lUsername = new System.Windows.Forms.Label();
            this.pStatus = new System.Windows.Forms.Panel();
            this.ttMain = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // lUsername
            // 
            this.lUsername.AutoSize = true;
            this.lUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lUsername.Location = new System.Drawing.Point(42, 11);
            this.lUsername.Name = "lUsername";
            this.lUsername.Size = new System.Drawing.Size(46, 17);
            this.lUsername.TabIndex = 0;
            this.lUsername.Text = "label1";
            // 
            // pStatus
            // 
            this.pStatus.Dock = System.Windows.Forms.DockStyle.Left;
            this.pStatus.Location = new System.Drawing.Point(0, 0);
            this.pStatus.Name = "pStatus";
            this.pStatus.Size = new System.Drawing.Size(36, 36);
            this.pStatus.TabIndex = 1;
            // 
            // UserPane
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pStatus);
            this.Controls.Add(this.lUsername);
            this.Name = "UserPane";
            this.Size = new System.Drawing.Size(178, 36);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lUsername;
        private System.Windows.Forms.Panel pStatus;
        private System.Windows.Forms.ToolTip ttMain;
    }
}
