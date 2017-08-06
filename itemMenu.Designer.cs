namespace SagraElCoda
{
    partial class itemMenu
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
            this.lblDescrizione = new System.Windows.Forms.Label();
            this.numericDecision = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericDecision)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDescrizione
            // 
            this.lblDescrizione.AutoSize = true;
            this.lblDescrizione.Location = new System.Drawing.Point(4, 5);
            this.lblDescrizione.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDescrizione.Name = "lblDescrizione";
            this.lblDescrizione.Size = new System.Drawing.Size(55, 14);
            this.lblDescrizione.TabIndex = 0;
            this.lblDescrizione.Text = "label1";
            // 
            // numericDecision
            // 
            this.numericDecision.Location = new System.Drawing.Point(202, 3);
            this.numericDecision.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.numericDecision.Name = "numericDecision";
            this.numericDecision.Size = new System.Drawing.Size(93, 22);
            this.numericDecision.TabIndex = 1;
            this.numericDecision.ValueChanged += new System.EventHandler(this.numericDecision_ValueChanged);
            // 
            // itemMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.numericDecision);
            this.Controls.Add(this.lblDescrizione);
            this.Font = new System.Drawing.Font("Monaco", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "itemMenu";
            this.Size = new System.Drawing.Size(299, 28);
            ((System.ComponentModel.ISupportInitialize)(this.numericDecision)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDescrizione;
        private System.Windows.Forms.NumericUpDown numericDecision;
    }
}
