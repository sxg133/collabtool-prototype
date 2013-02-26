namespace CollabClient
{
    partial class UsernameDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.tName = new System.Windows.Forms.TextBox();
            this.bOK = new System.Windows.Forms.Button();
            this.lError = new System.Windows.Forms.Label();
            this.bCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(27, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "What\'s your name?";
            // 
            // tName
            // 
            this.tName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tName.Location = new System.Drawing.Point(31, 70);
            this.tName.Name = "tName";
            this.tName.Size = new System.Drawing.Size(242, 26);
            this.tName.TabIndex = 1;
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(164, 124);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(63, 23);
            this.bOK.TabIndex = 2;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // lError
            // 
            this.lError.AutoSize = true;
            this.lError.ForeColor = System.Drawing.Color.Red;
            this.lError.Location = new System.Drawing.Point(28, 99);
            this.lError.Name = "lError";
            this.lError.Size = new System.Drawing.Size(29, 13);
            this.lError.TabIndex = 3;
            this.lError.Text = "Error";
            // 
            // bCancel
            // 
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.Location = new System.Drawing.Point(233, 124);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(63, 23);
            this.bCancel.TabIndex = 4;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // UsernameDialog
            // 
            this.AcceptButton = this.bOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bCancel;
            this.ClientSize = new System.Drawing.Size(309, 159);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.lError);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.tName);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UsernameDialog";
            this.Text = "Who are you?";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tName;
        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.Label lError;
        private System.Windows.Forms.Button bCancel;
    }
}