// <copyright file="InformationBox.cs" company="Johann Blais">
// Copyright (c) 2008 All Right Reserved
// </copyright>
// <author>Johann Blais</author>
// <summary>Displays a message box that can contain text, buttons, and symbols that inform and instruct the user</summary>

namespace InfoBox
{
    using System.Security.Permissions;
    using System.Windows.Forms;

    /// <summary>
    /// Displays a message box that can contain text, buttons, and symbols that inform and instruct the user.
    /// </summary>
    [UIPermission(SecurityAction.Demand)]
    public static class InformationBox
    {
        #region Show

        /// <summary>
        /// Displays a message box with the specified text and parameters.
        /// <list type="table">
        ///     <listheader><term>If the type of the parameter is</term>
        ///                 <description>The value will be used as</description>
        ///     </listheader>
        ///     <item>
        ///         <term><see cref="System.String"/></term>
        ///         <description>the title of the <see cref="InformationBox"/> for the first string, the second string for the help file name, and the third one for the help topic id</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="InformationBoxButtons"/></term>
        ///         <description>which buttons to display on the <see cref="InformationBox"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="InformationBoxIcon"/></term>
        ///         <description>which predefined icon to use for the <see cref="InformationBoxIcon"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="System.Drawing.Icon"/></term>
        ///         <description>the icon to use for the <see cref="InformationBox"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="InformationBoxDefaultButton"/></term>
        ///         <description>which button to set as default for the <see cref="InformationBox"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="System.String"/>[]</term>
        ///         <description>the text for the custom buttons of the <see cref="InformationBox"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="InformationBoxBehavior"/></term>
        ///         <description>the modal/modeless state of the <see cref="InformationBox"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="InformationBoxButtonsLayout"/></term>
        ///         <description>the layout for the buttons of the <see cref="InformationBox"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="InformationBoxAutoSizeMode"/></term>
        ///         <description>how the <see cref="InformationBox"/> will resize itself according to the text.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="InformationBoxPosition"/></term>
        ///         <description>where the <see cref="InformationBox"/> will appear on the screen.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="System.Boolean"/></term>
        ///         <description>whether the help button is displayed or not.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="System.Windows.Forms.HelpNavigator"/></term>
        ///         <description>the Help content. You can use the following values for navigator: TableOfContents, Find, Index, or Topic.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="InformationBoxCheckBox"/></term>
        ///         <description>whether the "Do not show this dialog again" checkbox is displayed or not.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="AutoCloseParameters"/></term>
        ///         <description>The parameters for the auto-close feature.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="DesignParameters"/></term>
        ///         <description>the parameters for the design (colors).</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="InformationBoxTitleIconStyle"/></term>
        ///         <description>which icon will be displayed in the title bar.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="InformationBoxTitleIcon"/></term>
        ///         <description>the custom icon that will be displayed in the title bar.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="InformationBoxOpacity"/></term>
        ///         <description>the opacity of the <see cref="InformationBox"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="AsyncResultCallback"/></term>
        ///         <description>a method that will be called when a modeless dialog is closed.</description>
        ///     </item>
        ///     <item>
        ///         <term>A MessageBox enum value</term>
        ///         <description>the value for the corresponding <see cref="InformationBox"/> enum value.</description>
        ///     </item>
        /// </list>
        /// </summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="parameters">The parameters of the message box.</param>
        /// <returns>One of the <see cref="InformationBoxResult"/> values.</returns>
        public static InformationBoxResult Show(string text, params object[] parameters)
        {
            return new InformationBoxForm(text, parameters).Show();
        }

        /// <summary>
        /// Displays a message box with the specified text and parameters.
        /// <list type="table">
        ///     <listheader><term>If the type of the parameter is</term>
        ///                 <description>The value will be used as</description>
        ///     </listheader>
        ///     <item>
        ///         <term><see cref="System.String"/></term>
        ///         <description>the title of the <see cref="InformationBox"/> for the first string, the second string for the help file name, and the third one for the help topic id</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="InformationBoxButtons"/></term>
        ///         <description>which buttons to display on the <see cref="InformationBox"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="InformationBoxIcon"/></term>
        ///         <description>which predefined icon to use for the <see cref="InformationBoxIcon"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="System.Drawing.Icon"/></term>
        ///         <description>the icon to use for the <see cref="InformationBox"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="InformationBoxDefaultButton"/></term>
        ///         <description>which button to set as default for the <see cref="InformationBox"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="System.String"/>[]</term>
        ///         <description>the text for the custom buttons of the <see cref="InformationBox"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="InformationBoxBehavior"/></term>
        ///         <description>the modal/modeless state of the <see cref="InformationBox"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="InformationBoxButtonsLayout"/></term>
        ///         <description>the layout for the buttons of the <see cref="InformationBox"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="InformationBoxAutoSizeMode"/></term>
        ///         <description>how the <see cref="InformationBox"/> will resize itself according to the text.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="InformationBoxPosition"/></term>
        ///         <description>where the <see cref="InformationBox"/> will appear on the screen.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="System.Boolean"/></term>
        ///         <description>whether the help button is displayed or not.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="System.Windows.Forms.HelpNavigator"/></term>
        ///         <description>the Help content. You can use the following values for navigator: TableOfContents, Find, Index, or Topic.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="InformationBoxCheckBox"/></term>
        ///         <description>whether the "Do not show this dialog again" checkbox is displayed or not.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="AutoCloseParameters"/></term>
        ///         <description>the parameters for the auto-close feature.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="DesignParameters"/></term>
        ///         <description>the parameters for the design (colors).</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="InformationBoxTitleIconStyle"/></term>
        ///         <description>which icon will be displayed in the title bar.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="InformationBoxTitleIcon"/></term>
        ///         <description>the custom icon that will be displayed in the title bar.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="InformationBoxOpacity"/></term>
        ///         <description>the opacity of the <see cref="InformationBox"/>.</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="AsyncResultCallback"/></term>
        ///         <description>a method that will be called when a modeless dialog is closed.</description>
        ///     </item>
        ///     <item>
        ///         <term>A MessageBox enum value</term>
        ///         <description>the value for the correspondin <see cref="InformationBox"/> enum value.</description>
        ///     </item>
        /// </list>
        /// </summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="checkBoxState">The final value of the "Do not show this dialog again" checkbox.</param>
        /// <param name="parameters">The parameters of the message box.</param>
        /// <returns>
        /// One of the <see cref="InformationBoxResult"/> values.
        /// </returns>
        public static InformationBoxResult Show(string text, out CheckState checkBoxState, params object[] parameters)
        {
            return new InformationBoxForm(text, parameters).Show(out checkBoxState);
        }

        #endregion Show
    }
}
