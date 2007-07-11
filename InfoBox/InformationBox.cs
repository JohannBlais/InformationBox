namespace InfoBox
{
    /// <summary>
    /// Displays a message box that can contain text, buttons, and symbols that inform and instruct the user.
    /// </summary>
    public class InformationBox
    {
        #region Show

        /// <summary>
        /// Displays a message box with the specified text and parameters.
        /// <list type="table">
        /// <listheader><term>If the type of the parameter is</term>
        ///             <description>The value will be used as</description>
        /// </listheader>
        /// <item>
        ///     <term><see cref="System.String"/></term>
        ///     <description>the title of the <see cref="InformationBox"/>.</description>
        /// </item>
        /// <item>
        ///     <term><see cref="InformationBoxButtons"/></term>
        ///     <description>which buttons to display on the <see cref="InformationBox"/>.</description>
        /// </item>
        /// <item>
        ///     <term><see cref="InformationBoxIcon"/></term>
        ///     <description>which predefined icon to use for the <see cref="InformationBoxIcon"/>.</description>
        /// </item>
        /// <item>
        ///     <term><see cref="System.Drawing.Icon"/></term>
        ///     <description>the icon to use for the <see cref="InformationBox"/>.</description>
        /// </item>
        /// <item>
        ///     <term><see cref="InformationBoxDefaultButton"/></term>
        ///     <description>which button to set as default for the <see cref="InformationBox"/>.</description>
        /// </item>
        /// <item>
        ///     <term><see cref="System.String"/>[]</term>
        ///     <description>the text for the custom buttons of the <see cref="InformationBox"/>.</description>
        /// </item>
        /// <item>
        ///     <term><see cref="InformationBoxButtonsLayout"/></term>
        ///     <description>the layout for the buttons of the <see cref="InformationBox"/>.</description>
        /// </item>
        /// <item>
        ///     <term><see cref="InformationBoxAutoSizeMode"/></term>
        ///     <description>how the <see cref="InformationBox"/> will resize itself according to the text.</description>
        /// </item>
        /// <item>
        ///     <term><see cref="InformationBoxPosition"/></term>
        ///     <description>where the <see cref="InformationBox"/> will appear on the screen.</description>
        /// </item>
		/// <item>
		///     <term><see cref="Boolean"/></term>
		///     <description>whether the help button is displayed or not.</description>
		/// </item>
		/// </list>
        /// </summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="parameters">The parameters of the message box.</param>
        /// <returns>One of the <see cref="InformationBoxResult"/> values.</returns>
        public static InformationBoxResult Show(string text, params object[] parameters)
        {
            return new InformationBoxForm(text, parameters).Show();
        }

        #endregion Show
    }
}
