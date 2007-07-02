using System.Drawing;
namespace InfoBox
{
    /// <summary>
    /// Displays a message box that can contain text, buttons, and symbols that inform and instruct the user.
    /// </summary>
    public class InformationBox
    {
        #region Show

        /// <summary>
        /// Displays a message box with specified text.
        /// </summary>
        /// <param name="text">The text to display in the message box. </param>
        /// <returns>One of the <see cref="InformationBoxResult"/> values.</returns>
        public static InformationBoxResult Show(string text)
        {
            InformationBoxForm boxForm = new InformationBoxForm(text);
            return boxForm.Show();
        }

        /// <summary>
        /// Displays a message box with specified text and caption.
        /// </summary>
        /// <param name="text">The text to display in the message box. </param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <returns>One of the <see cref="InformationBoxResult"/> values.</returns>
        public static InformationBoxResult Show(string text, string caption)
        {
            InformationBoxForm boxForm = new InformationBoxForm(text, caption);
            return boxForm.Show();
        }

        /// <summary>
        /// Displays a message box with specified text, caption, and buttons.
        /// </summary>
        /// <param name="text">The text to display in the message box. </param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the <see cref="InformationBoxButtons" /> values that specifies which buttons to display in the message box.</param>
        /// <returns>One of the <see cref="InformationBoxResult"/> values.</returns>
        public static InformationBoxResult Show(string text, string caption, InformationBoxButtons buttons)
        {
            InformationBoxForm boxForm = new InformationBoxForm(text, caption, buttons);
            return boxForm.Show();
        }

        /// <summary>
        /// Displays a message box with specified text, caption, buttons, and text for the first button.
        /// </summary>
        /// <param name="text">The text to display in the message box. </param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the <see cref="InformationBoxButtons" /> values that specifies which buttons to display in the message box.</param>
        /// <param name="button1Text">The text to display in the first button.</param>
        /// <returns>One of the <see cref="InformationBoxResult"/> values.</returns>
        public static InformationBoxResult Show(string text, string caption, InformationBoxButtons buttons, string button1Text)
        {
            InformationBoxForm boxForm = new InformationBoxForm(text, caption, buttons, button1Text);
            return boxForm.Show();
        }

        /// <summary>
        /// Displays a message box with specified text, caption, buttons, and text for the first and second button.
        /// </summary>
        /// <param name="text">The text to display in the message box. </param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the <see cref="InformationBoxButtons" /> values that specifies which buttons to display in the message box.</param>
        /// <param name="button1Text">The text to display in the first button.</param>
        /// <param name="button2Text">The text to display in the second button.</param>
        /// <returns>One of the <see cref="InformationBoxResult"/> values.</returns>
        public static InformationBoxResult Show(string text, string caption, InformationBoxButtons buttons, string button1Text, string button2Text)
        {
            InformationBoxForm boxForm = new InformationBoxForm(text, caption, buttons, button1Text, button2Text);
            return boxForm.Show();
        }

        /// <summary>
        /// Displays a message box with specified text, caption, buttons, and icon.
        /// </summary>
        /// <param name="text">The text to display in the message box. </param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the <see cref="InformationBoxButtons" /> values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the <see cref="InformationBoxIcon" /> values that specifies which icon to display in the message box.</param>
        /// <returns>One of the <see cref="InformationBoxResult"/> values.</returns>
        public static InformationBoxResult Show(string text, string caption, InformationBoxButtons buttons, InformationBoxIcon icon)
        {
            InformationBoxForm boxForm = new InformationBoxForm(text, caption, buttons, icon);
            return boxForm.Show();
        }

        /// <summary>
        /// Displays a message box with specified text, caption, buttons, text for the first button, and icon.
        /// </summary>
        /// <param name="text">The text to display in the message box. </param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the <see cref="InformationBoxButtons" /> values that specifies which buttons to display in the message box.</param>
        /// <param name="button1Text">The text to display in the first button.</param>
        /// <param name="icon">One of the <see cref="InformationBoxIcon" /> values that specifies which icon to display in the message box.</param>
        /// <returns>One of the <see cref="InformationBoxResult"/> values.</returns>
        public static InformationBoxResult Show(string text, string caption, InformationBoxButtons buttons, string button1Text, InformationBoxIcon icon)
        {
            InformationBoxForm boxForm = new InformationBoxForm(text, caption, buttons, button1Text, icon);
            return boxForm.Show();
        }

        /// <summary>
        /// Displays a message box with specified text, caption, buttons, text for the first and second button, and icon.
        /// </summary>
        /// <param name="text">The text to display in the message box. </param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the <see cref="InformationBoxButtons" /> values that specifies which buttons to display in the message box.</param>
        /// <param name="button1Text">The text to display in the first button.</param>
        /// <param name="button2Text">The text to display in the second button.</param>
        /// <param name="icon">One of the <see cref="InformationBoxIcon" /> values that specifies which icon to display in the message box.</param>
        /// <returns>One of the <see cref="InformationBoxResult"/> values.</returns>
        public static InformationBoxResult Show(string text, string caption, InformationBoxButtons buttons, string button1Text, string button2Text, InformationBoxIcon icon)
        {
            InformationBoxForm boxForm = new InformationBoxForm(text, caption, buttons, button1Text, button2Text, icon);
            return boxForm.Show();
        }

        /// <summary>
        /// Displays a message box with the specified text, caption, buttons, icon, and default button.
        /// </summary>
        /// <param name="text">The text to display in the message box. </param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the <see cref="InformationBoxButtons" /> values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">One of the <see cref="InformationBoxIcon" /> values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton">One of the <see cref="InformationBoxDefaultButton" /> values that specifies the default button for the message box.</param>
        /// <returns>One of the <see cref="InformationBoxResult"/> values.</returns>
        public static InformationBoxResult Show(string text, string caption, InformationBoxButtons buttons, InformationBoxIcon icon, InformationBoxDefaultButton defaultButton)
        {
            InformationBoxForm boxForm = new InformationBoxForm(text, caption, buttons, icon, defaultButton);
            return boxForm.Show();
        }

        /// <summary>
        /// Displays a message box with the specified text, caption, buttons, icon, text for the first button, and default button.
        /// </summary>
        /// <param name="text">The text to display in the message box. </param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the <see cref="InformationBoxButtons" /> values that specifies which buttons to display in the message box.</param>
        /// <param name="button1Text">The text to display in the first button.</param>
        /// <param name="icon">One of the <see cref="InformationBoxIcon" /> values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton">One of the <see cref="InformationBoxDefaultButton" /> values that specifies the default button for the message box.</param>
        /// <returns>One of the <see cref="InformationBoxResult"/> values.</returns>
        public static InformationBoxResult Show(string text, string caption, InformationBoxButtons buttons, string button1Text, InformationBoxIcon icon, InformationBoxDefaultButton defaultButton)
        {
            InformationBoxForm boxForm = new InformationBoxForm(text, caption, buttons, button1Text, icon, defaultButton);
            return boxForm.Show();
        }

        /// <summary>
        /// Displays a message box with the specified text, caption, buttons, icon, text for the first and second button, and default button.
        /// </summary>
        /// <param name="text">The text to display in the message box. </param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the <see cref="InformationBoxButtons" /> values that specifies which buttons to display in the message box.</param>
        /// <param name="button1Text">The text to display in the first button.</param>
        /// <param name="button2Text">The text to display in the second button.</param>
        /// <param name="icon">One of the <see cref="InformationBoxIcon" /> values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton">One of the <see cref="InformationBoxDefaultButton" /> values that specifies the default button for the message box.</param>
        /// <returns>One of the <see cref="InformationBoxResult"/> values.</returns>
        public static InformationBoxResult Show(string text, string caption, InformationBoxButtons buttons, string button1Text, string button2Text, InformationBoxIcon icon, InformationBoxDefaultButton defaultButton)
        {
            InformationBoxForm boxForm = new InformationBoxForm(text, caption, buttons, button1Text, button2Text, icon, defaultButton);
            return boxForm.Show();
        }

        /// <summary>
        /// Displays a message box with the specified text, caption, buttons, icon, text for the first and second button, default button, and buttons layout.
        /// </summary>
        /// <param name="text">The text to display in the message box. </param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the <see cref="InformationBoxButtons" /> values that specifies which buttons to display in the message box.</param>
        /// <param name="button1Text">The text to display in the first button.</param>
        /// <param name="button2Text">The text to display in the second button.</param>
        /// <param name="icon">One of the <see cref="InformationBoxIcon" /> values that specifies which icon to display in the message box.</param>
        /// <param name="defaultButton">One of the <see cref="InformationBoxDefaultButton" /> values that specifies the default button for the message box.</param>
        /// <returns>One of the <see cref="InformationBoxResult"/> values.</returns>
        public static InformationBoxResult Show(string text, string caption, InformationBoxButtons buttons, string button1Text, string button2Text, InformationBoxIcon icon, InformationBoxDefaultButton defaultButton, InformationBoxButtonsLayout buttonsLayout)
        {
            InformationBoxForm boxForm = new InformationBoxForm(text, caption, buttons, button1Text, button2Text, icon, defaultButton, buttonsLayout);
            return boxForm.Show();
        }

        /// <summary>
        /// Displays a message box with specified text, caption, buttons, and icon.
        /// </summary>
        /// <param name="text">The text to display in the message box. </param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the <see cref="InformationBoxButtons" /> values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">Icon to display in the message box.</param>
        /// <returns>One of the <see cref="InformationBoxResult"/> values.</returns>
        public static InformationBoxResult Show(string text, string caption, InformationBoxButtons buttons, Icon icon)
        {
            InformationBoxForm boxForm = new InformationBoxForm(text, caption, buttons, icon);
            return boxForm.Show();
        }

        /// <summary>
        /// Displays a message box with specified text, caption, buttons, text for the first button, and icon.
        /// </summary>
        /// <param name="text">The text to display in the message box. </param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the <see cref="InformationBoxButtons" /> values that specifies which buttons to display in the message box.</param>
        /// <param name="button1Text">The text to display in the first button.</param>
        /// <param name="icon">Icon to display in the message box.</param>
        /// <returns>One of the <see cref="InformationBoxResult"/> values.</returns>
        public static InformationBoxResult Show(string text, string caption, InformationBoxButtons buttons, string button1Text, Icon icon)
        {
            InformationBoxForm boxForm = new InformationBoxForm(text, caption, buttons, button1Text, icon);
            return boxForm.Show();
        }

        /// <summary>
        /// Displays a message box with specified text, caption, buttons, text for the first and second button, and icon.
        /// </summary>
        /// <param name="text">The text to display in the message box. </param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the <see cref="InformationBoxButtons" /> values that specifies which buttons to display in the message box.</param>
        /// <param name="button1Text">The text to display in the first button.</param>
        /// <param name="button2Text">The text to display in the second button.</param>
        /// <param name="icon">Icon to display in the message box.</param>
        /// <returns>One of the <see cref="InformationBoxResult"/> values.</returns>
        public static InformationBoxResult Show(string text, string caption, InformationBoxButtons buttons, string button1Text, string button2Text, Icon icon)
        {
            InformationBoxForm boxForm = new InformationBoxForm(text, caption, buttons, button1Text, button2Text, icon);
            return boxForm.Show();
        }

        /// <summary>
        /// Displays a message box with the specified text, caption, buttons, icon, and default button.
        /// </summary>
        /// <param name="text">The text to display in the message box. </param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the <see cref="InformationBoxButtons" /> values that specifies which buttons to display in the message box.</param>
        /// <param name="icon">Icon to display in the message box.</param>
        /// <param name="defaultButton">One of the <see cref="InformationBoxDefaultButton" /> values that specifies the default button for the message box.</param>
        /// <returns>One of the <see cref="InformationBoxResult"/> values.</returns>
        public static InformationBoxResult Show(string text, string caption, InformationBoxButtons buttons, Icon icon, InformationBoxDefaultButton defaultButton)
        {
            InformationBoxForm boxForm = new InformationBoxForm(text, caption, buttons, icon, defaultButton);
            return boxForm.Show();
        }

        /// <summary>
        /// Displays a message box with the specified text, caption, buttons, icon, text for the first button, and default button.
        /// </summary>
        /// <param name="text">The text to display in the message box. </param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the <see cref="InformationBoxButtons" /> values that specifies which buttons to display in the message box.</param>
        /// <param name="button1Text">The text to display in the first button.</param>
        /// <param name="icon">Icon to display in the message box.</param>
        /// <param name="defaultButton">One of the <see cref="InformationBoxDefaultButton" /> values that specifies the default button for the message box.</param>
        /// <returns>One of the <see cref="InformationBoxResult"/> values.</returns>
        public static InformationBoxResult Show(string text, string caption, InformationBoxButtons buttons, string button1Text, Icon icon, InformationBoxDefaultButton defaultButton)
        {
            InformationBoxForm boxForm = new InformationBoxForm(text, caption, buttons, button1Text, icon, defaultButton);
            return boxForm.Show();
        }

        /// <summary>
        /// Displays a message box with the specified text, caption, buttons, icon, text for the first and second button, and default button.
        /// </summary>
        /// <param name="text">The text to display in the message box. </param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the <see cref="InformationBoxButtons" /> values that specifies which buttons to display in the message box.</param>
        /// <param name="button1Text">The text to display in the first button.</param>
        /// <param name="button2Text">The text to display in the second button.</param>
        /// <param name="icon">Icon to display in the message box.</param>
        /// <param name="defaultButton">One of the <see cref="InformationBoxDefaultButton" /> values that specifies the default button for the message box.</param>
        /// <returns>One of the <see cref="InformationBoxResult"/> values.</returns>
        public static InformationBoxResult Show(string text, string caption, InformationBoxButtons buttons, string button1Text, string button2Text, Icon icon, InformationBoxDefaultButton defaultButton)
        {
            InformationBoxForm boxForm = new InformationBoxForm(text, caption, buttons, button1Text, button2Text, icon, defaultButton);
            return boxForm.Show();
        }

        /// <summary>
        /// Displays a message box with the specified text, caption, buttons, icon, text for the first and second button, default button, and buttons layout.
        /// </summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="caption">The text to display in the title bar of the message box.</param>
        /// <param name="buttons">One of the <see cref="InformationBoxButtons"/> values that specifies which buttons to display in the message box.</param>
        /// <param name="button1Text">The text to display in the first button.</param>
        /// <param name="button2Text">The text to display in the second button.</param>
        /// <param name="icon">Icon to display in the message box.</param>
        /// <param name="defaultButton">One of the <see cref="InformationBoxDefaultButton"/> values that specifies the default button for the message box.</param>
        /// <param name="buttonsLayout">The buttons layout.</param>
        /// <returns>
        /// One of the <see cref="InformationBoxResult"/> values.
        /// </returns>
        public static InformationBoxResult Show(string text, string caption, InformationBoxButtons buttons, string button1Text, string button2Text, Icon icon, InformationBoxDefaultButton defaultButton, InformationBoxButtonsLayout buttonsLayout)
        {
            InformationBoxForm boxForm = new InformationBoxForm(text, caption, buttons, button1Text, button2Text, icon, defaultButton, buttonsLayout);
            return boxForm.Show();
        }

        #endregion Show
    }
}
