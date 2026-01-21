// <copyright file="ButtonDefinition.cs" company="Johann Blais">
// Copyright (c) 2008 All Right Reserved
// </copyright>
// <author>Johann Blais</author>
// <summary>Definition of a button for InformationBox</summary>

namespace InfoBox.Presentation
{
    /// <summary>
    /// Defines a button to be displayed in an InformationBox.
    /// </summary>
    /// <remarks>
    /// This class represents button configuration without WinForms dependencies,
    /// making button generation logic fully testable.
    /// See TESTABILITY_ROADMAP.md - P0.1
    /// </remarks>
    public class ButtonDefinition
    {
        /// <summary>
        /// Gets or sets the button name (used for identification).
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the button text (displayed to user).
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the result to return when this button is clicked.
        /// </summary>
        public InformationBoxResult Result { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this button is the default button.
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ButtonDefinition"/> class.
        /// </summary>
        public ButtonDefinition()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ButtonDefinition"/> class.
        /// </summary>
        /// <param name="name">Button name</param>
        /// <param name="text">Button text</param>
        /// <param name="result">Result to return</param>
        /// <param name="isDefault">Whether this is the default button</param>
        public ButtonDefinition(string name, string text, InformationBoxResult result, bool isDefault = false)
        {
            this.Name = name;
            this.Text = text;
            this.Result = result;
            this.IsDefault = isDefault;
        }
    }
}
