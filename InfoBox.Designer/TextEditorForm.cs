// <copyright file="TextEditorForm.cs" company="Johann Blais">
// Copyright (c) 2008 All Right Reserved
// </copyright>
// <author>Johann Blais</author>
// <summary>Text editor form for InformationBox content</summary>

namespace InfoBox.Designer
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// Text editor form for editing InformationBox content.
    /// </summary>
    public partial class TextEditorForm : Form
    {
        private readonly InformationBoxDesigner parentDesigner;
        private bool isUpdating = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextEditorForm"/> class.
        /// </summary>
        private TextEditorForm()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextEditorForm"/> class.
        /// </summary>
        /// <param name="parentDesigner">The parent designer form.</param>
        public TextEditorForm(InformationBoxDesigner parentDesigner): this()
        {
            this.parentDesigner = parentDesigner ?? throw new ArgumentNullException(nameof(parentDesigner));
            this.InitializeComponent();
            this.SetupDataBinding();
        }

        /// <summary>
        /// Sets up data binding between the text editor and parent designer.
        /// </summary>
        private void SetupDataBinding()
        {
            // Set up two-way synchronization
            this.txtContent.Text = this.parentDesigner.TextContent.Text;
            this.txtContent.TextChanged += TxtContent_TextChanged;
            this.parentDesigner.TextContent.TextChanged += ParentText_TextChanged;
        }

        /// <summary>
        /// Handles text changes in the editor.
        /// </summary>
        private void TxtContent_TextChanged(object sender, EventArgs e)
        {
            if (!isUpdating)
            {
                isUpdating = true;
                this.parentDesigner.TextContent.Text = this.txtContent.Text;
                isUpdating = false;
            }
        }

        /// <summary>
        /// Handles text changes in the parent designer.
        /// </summary>
        private void ParentText_TextChanged(object sender, EventArgs e)
        {
            if (!isUpdating)
            {
                isUpdating = true;
                this.txtContent.Text = this.parentDesigner.TextContent.Text;
                isUpdating = false;
            }
        }

        /// <summary>
        /// Cleans up event handlers when the form is closed.
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Unsubscribe from events
            this.txtContent.TextChanged -= TxtContent_TextChanged;
            this.parentDesigner.TextContent.TextChanged -= ParentText_TextChanged;
            base.OnFormClosing(e);
        }

        /// <summary>
        /// Updates the font and color from the parent designer.
        /// </summary>
        public void UpdateFontAndColor(Font font, Color color)
        {
            if (font != null)
            {
                this.txtContent.Font = font;
            }

            if (color != Color.Empty)
            {
                this.txtContent.ForeColor = color;
            }
        }
    }
}
