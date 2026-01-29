namespace InfoBoxCore.ManualTests
{
    partial class ManualTestsForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnTestFixedWidthEightPoints = new Button();
            btnTestFixedWidthFourPoints = new Button();
            btnTestFixedWidthTwelvePoints = new Button();
            btnTestLongLinesFixedFont = new Button();
            SuspendLayout();
            // 
            // btnTestFixedWidthEightPoints
            // 
            btnTestFixedWidthEightPoints.Location = new Point(12, 12);
            btnTestFixedWidthEightPoints.Name = "btnTestFixedWidthEightPoints";
            btnTestFixedWidthEightPoints.Size = new Size(182, 23);
            btnTestFixedWidthEightPoints.TabIndex = 0;
            btnTestFixedWidthEightPoints.Text = "Fixed width font 8.25F";
            btnTestFixedWidthEightPoints.UseVisualStyleBackColor = true;
            btnTestFixedWidthEightPoints.Click += btnTestFixedWidthEightPoints_Click;
            // 
            // btnTestFixedWidthFourPoints
            // 
            btnTestFixedWidthFourPoints.Location = new Point(12, 41);
            btnTestFixedWidthFourPoints.Name = "btnTestFixedWidthFourPoints";
            btnTestFixedWidthFourPoints.Size = new Size(182, 23);
            btnTestFixedWidthFourPoints.TabIndex = 1;
            btnTestFixedWidthFourPoints.Text = "Fixed width font 4.25F";
            btnTestFixedWidthFourPoints.UseVisualStyleBackColor = true;
            btnTestFixedWidthFourPoints.Click += btnTestFixedWidthFourPoints_Click;
            // 
            // btnTestFixedWidthTwelvePoints
            // 
            btnTestFixedWidthTwelvePoints.Location = new Point(12, 70);
            btnTestFixedWidthTwelvePoints.Name = "btnTestFixedWidthTwelvePoints";
            btnTestFixedWidthTwelvePoints.Size = new Size(182, 23);
            btnTestFixedWidthTwelvePoints.TabIndex = 2;
            btnTestFixedWidthTwelvePoints.Text = "Fixed width font 12.25F";
            btnTestFixedWidthTwelvePoints.UseVisualStyleBackColor = true;
            btnTestFixedWidthTwelvePoints.Click += btnTestFixedWidthTwelvePoints_Click;
            // 
            // btnTestLongLinesFixedFont
            // 
            btnTestLongLinesFixedFont.Location = new Point(12, 99);
            btnTestLongLinesFixedFont.Name = "btnTestLongLinesFixedFont";
            btnTestLongLinesFixedFont.Size = new Size(182, 23);
            btnTestLongLinesFixedFont.TabIndex = 3;
            btnTestLongLinesFixedFont.Text = "Fixed width font long lines";
            btnTestLongLinesFixedFont.UseVisualStyleBackColor = true;
            btnTestLongLinesFixedFont.Click += btnTestLongLinesFixedFont_Click;
            // 
            // ManualTestsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnTestLongLinesFixedFont);
            Controls.Add(btnTestFixedWidthTwelvePoints);
            Controls.Add(btnTestFixedWidthFourPoints);
            Controls.Add(btnTestFixedWidthEightPoints);
            Name = "ManualTestsForm";
            Text = "Manual tests";
            ResumeLayout(false);
        }

        #endregion

        private Button btnTestFixedWidthEightPoints;
        private Button btnTestFixedWidthFourPoints;
        private Button btnTestFixedWidthTwelvePoints;
        private Button btnTestLongLinesFixedFont;
    }
}
