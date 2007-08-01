namespace InfoBox
{
    internal partial class InformationBoxForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InformationBoxForm));
            this.lblText = new System.Windows.Forms.Label();
            this.pcbIcon = new System.Windows.Forms.PictureBox();
            this.pnlBas = new System.Windows.Forms.Panel();
            this.chbDoNotShow = new System.Windows.Forms.CheckBox();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlIcon = new System.Windows.Forms.Panel();
            this.pnlText = new System.Windows.Forms.Panel();
            this.pnlButtons = new GlassComponents.Controls.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pcbIcon)).BeginInit();
            this.pnlBas.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlIcon.SuspendLayout();
            this.pnlText.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblText
            // 
            this.lblText.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblText.Location = new System.Drawing.Point(81, 28);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(28, 13);
            this.lblText.TabIndex = 0;
            this.lblText.Text = "Text";
            // 
            // pcbIcon
            // 
            this.pcbIcon.Location = new System.Drawing.Point(10, 10);
            this.pcbIcon.Name = "pcbIcon";
            this.pcbIcon.Size = new System.Drawing.Size(48, 48);
            this.pcbIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pcbIcon.TabIndex = 1;
            this.pcbIcon.TabStop = false;
            // 
            // pnlBas
            // 
            this.pnlBas.BackColor = System.Drawing.Color.Transparent;
            this.pnlBas.Controls.Add(this.pnlButtons);
            this.pnlBas.Controls.Add(this.chbDoNotShow);
            this.pnlBas.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBas.Location = new System.Drawing.Point(0, 68);
            this.pnlBas.Name = "pnlBas";
            this.pnlBas.Size = new System.Drawing.Size(278, 53);
            this.pnlBas.TabIndex = 2;
            // 
            // chbDoNotShow
            // 
            this.chbDoNotShow.BackColor = System.Drawing.Color.Transparent;
            this.chbDoNotShow.Dock = System.Windows.Forms.DockStyle.Top;
            this.chbDoNotShow.Location = new System.Drawing.Point(0, 0);
            this.chbDoNotShow.Name = "chbDoNotShow";
            this.chbDoNotShow.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.chbDoNotShow.Size = new System.Drawing.Size(278, 18);
            this.chbDoNotShow.TabIndex = 4;
            this.chbDoNotShow.Text = "Do not show...";
            this.chbDoNotShow.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.chbDoNotShow.UseVisualStyleBackColor = false;
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.Transparent;
            this.pnlMain.Controls.Add(this.pnlIcon);
            this.pnlMain.Controls.Add(this.pnlText);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(278, 68);
            this.pnlMain.TabIndex = 3;
            // 
            // pnlIcon
            // 
            this.pnlIcon.Controls.Add(this.pcbIcon);
            this.pnlIcon.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlIcon.Location = new System.Drawing.Point(0, 0);
            this.pnlIcon.MaximumSize = new System.Drawing.Size(68, 0);
            this.pnlIcon.MinimumSize = new System.Drawing.Size(68, 0);
            this.pnlIcon.Name = "pnlIcon";
            this.pnlIcon.Size = new System.Drawing.Size(68, 68);
            this.pnlIcon.TabIndex = 3;
            // 
            // pnlText
            // 
            this.pnlText.Controls.Add(this.lblText);
            this.pnlText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlText.Location = new System.Drawing.Point(0, 0);
            this.pnlText.Name = "pnlText";
            this.pnlText.Size = new System.Drawing.Size(278, 68);
            this.pnlText.TabIndex = 2;
            // 
            // pnlButtons
            // 
            this.pnlButtons.BackColor = System.Drawing.Color.Black;
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.ForeColor = System.Drawing.Color.White;
            this.pnlButtons.Location = new System.Drawing.Point(0, 18);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.SideBorder = GlassComponents.Controls.SideBorder.None;
            this.pnlButtons.SideBorderBottomColor = System.Drawing.Color.Transparent;
            this.pnlButtons.SideBorderTopColor = System.Drawing.Color.Transparent;
            this.pnlButtons.SideBorderWidth = 1;
            this.pnlButtons.Size = new System.Drawing.Size(278, 35);
            this.pnlButtons.TabIndex = 5;
            // 
            // InformationBoxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(278, 121);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlBas);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InformationBoxForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.InformationBox_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pcbIcon)).EndInit();
            this.pnlBas.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.pnlIcon.ResumeLayout(false);
            this.pnlText.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblText;
        private System.Windows.Forms.PictureBox pcbIcon;
        private System.Windows.Forms.Panel pnlBas;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlIcon;
        private System.Windows.Forms.Panel pnlText;
        private System.Windows.Forms.CheckBox chbDoNotShow;
        private GlassComponents.Controls.Panel pnlButtons;
    }
}