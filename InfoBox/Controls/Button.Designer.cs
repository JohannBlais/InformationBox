namespace InfoBox.Controls
{
    /// <summary>
    /// A button with a glass look and feel
    /// </summary>
    public partial class Button
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
                this.components.Dispose();
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
            this.buttonText = new System.Windows.Forms.Label();
            this.timerFade = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // buttonText
            // 
            this.buttonText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonText.BackColor = System.Drawing.Color.Transparent;
            this.buttonText.ForeColor = System.Drawing.Color.White;
            this.buttonText.Location = new System.Drawing.Point(5, 6);
            this.buttonText.Margin = new System.Windows.Forms.Padding(0);
            this.buttonText.Name = "buttonText";
            this.buttonText.Size = new System.Drawing.Size(199, 26);
            this.buttonText.TabIndex = 0;
            this.buttonText.Text = "Label de test";
            this.buttonText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.buttonText.MouseLeave += new System.EventHandler(this.OnTextLeave);
            this.buttonText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnTextDown);
            this.buttonText.MouseEnter += new System.EventHandler(this.OnTextEnter);
            this.buttonText.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnTextUp);
            // 
            // timerFade
            // 
            this.timerFade.Interval = 20;
            this.timerFade.Tick += new System.EventHandler(this.OnFadeTick);
            // 
            // Button
            // 
            this.Controls.Add(this.buttonText);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MinimumSize = new System.Drawing.Size(16, 16);
            this.Size = new System.Drawing.Size(209, 41);
            this.ForeColorChanged += new System.EventHandler(this.OnNewForeColor);
            this.EnabledChanged += new System.EventHandler(this.OnEnabledChanged);
            this.ResumeLayout(false);

        }

        #endregion

        /// <summary>
        /// Inner label for the button
        /// </summary>
        private System.Windows.Forms.Label buttonText;

        /// <summary>
        /// Inner timer for effects
        /// </summary>
        private System.Windows.Forms.Timer timerFade;
    }
}
