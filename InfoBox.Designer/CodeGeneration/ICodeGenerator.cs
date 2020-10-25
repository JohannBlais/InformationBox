using System;
using System.Windows.Forms;

namespace InfoBox.Designer.CodeGeneration
{
    internal interface ICodeGenerator
    {
        /// <summary>
        /// Generates a single method call to display an InfoBox.
        /// </summary>
        /// <param name="behavior">The behavior.</param>
        /// <param name="text">The text.</param>
        /// <param name="title">The title.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="button1Text">The button1 text.</param>
        /// <param name="button2Text">The button2 text.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="iconFileName">Name of the icon file.</param>
        /// <param name="defaultButton">The default button.</param>
        /// <param name="buttonsLayout">The buttons layout.</param>
        /// <param name="autoSize">The autosize parameters</param>
        /// <param name="position">The position.</param>
        /// <param name="showHelp">if set to <c>true</c> [show help].</param>
        /// <param name="helpFile">The help file.</param>
        /// <param name="helpTopic">The help topic.</param>
        /// <param name="navigator">The navigator.</param>
        /// <param name="checkState">The state of the checkbox.</param>
        /// <param name="style">The style.</param>
        /// <param name="useAutoClose">if set to <c>true</c> [use auto close].</param>
        /// <param name="autoClose">The auto-close parameters.</param>
        /// <param name="design">The design.</param>
        /// <param name="titleStyle">The title style.</param>
        /// <param name="titleIconFileName">Filename of the title icon .</param>
        /// <param name="opacity">The opacity.</param>
        /// <param name="order">The order.</param>
        /// <param name="sound">The sound.</param>
        /// <returns></returns>
        string GenerateSingleCall(InformationBoxBehavior behavior,
                                  String text,
                                  String title,
                                  InformationBoxButtons buttons,
                                  String button1Text,
                                  String button2Text,
                                  InformationBoxIcon icon,
                                  String iconFileName,
                                  InformationBoxDefaultButton defaultButton,
                                  InformationBoxButtonsLayout buttonsLayout,
                                  InformationBoxAutoSizeMode autoSize,
                                  InformationBoxPosition position,
                                  Boolean showHelp,
                                  String helpFile,
                                  String helpTopic,
                                  HelpNavigator navigator,
                                  InformationBoxCheckBox checkState,
                                  InformationBoxStyle style,
                                  Boolean useAutoClose,
                                  AutoCloseParameters autoClose,
                                  DesignParameters design,
                                  InformationBoxTitleIconStyle titleStyle,
                                  String titleIconFileName,
                                  InformationBoxOpacity opacity,
                                  InformationBoxOrder order,
                                  InformationBoxSound sound);
    }
}
