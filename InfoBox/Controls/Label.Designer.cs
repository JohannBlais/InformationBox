namespace InfoBox.Controls
{
    /// <summary>
    /// A label with a glass look and feel
    /// </summary>
    partial class Label
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
            this.labelText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelText
            // 
            this.labelText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelText.BackColor = System.Drawing.Color.Transparent;
            this.labelText.ForeColor = System.Drawing.Color.White;
            this.labelText.Location = new System.Drawing.Point(3, 6);
            this.labelText.Margin = new System.Windows.Forms.Padding(0);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(205, 26);
            this.labelText.TabIndex = 0;
            this.labelText.Text = "Label de test";
            this.labelText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelText_MouseDown);
            this.labelText.MouseMove += new System.Windows.Forms.MouseEventHandler(this.labelText_MouseMove);
            this.labelText.MouseUp += new System.Windows.Forms.MouseEventHandler(this.labelText_MouseUp);
            // 
            // Label
            // 
            this.Controls.Add(this.labelText);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MinimumSize = new System.Drawing.Size(16, 16);
            this.Size = new System.Drawing.Size(209, 41);
            this.ForeColorChanged += new System.EventHandler(this.OnNewForeColor);
            this.EnabledChanged += new System.EventHandler(this.OnEnabledChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelText;
    }
}
