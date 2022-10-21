// <copyright file="IconHelper.cs" company="Johann Blais">
// Copyright (c) 2008 All Right Reserved
// </copyright>
// <author>Johann Blais</author>
// <summary>Contains utility methods related to icons</summary>

namespace InfoBox.Internals
{
    using InfoBox.Properties;

    using System.Drawing;

    /// <summary>
    /// Contains utility methods related to icons
    /// </summary>
    internal static class IconHelper
    {
        /// <summary>
        /// Return the <see cref="System.Drawing.Icon"/> associated with the specified <see cref="InformationBoxIcon"/>.
        /// </summary>
        /// <param name="iconType">Type of the icon.</param>
        /// <returns>An icon corresponding to the iconType</returns>
        internal static Icon FromEnum(InformationBoxIcon iconType)
        {
            switch (iconType)
            {
                case InformationBoxIcon.Asterisk:
                case InformationBoxIcon.Information:
                    return Resources.IconInfo;

                case InformationBoxIcon.Error:
                case InformationBoxIcon.Hand:
                case InformationBoxIcon.Stop:
                    return Resources.IconError;

                case InformationBoxIcon.Exclamation:
                case InformationBoxIcon.Warning:
                    return Resources.IconWarning;

                case InformationBoxIcon.Question:
                    return Resources.IconQuestion;

                case InformationBoxIcon.Success:
                    return Resources.IconGood;

                case InformationBoxIcon.Fax:
                    return Resources.IconFax;

                case InformationBoxIcon.Gamepad:
                    return Resources.IconGamepad;

                case InformationBoxIcon.Joystick:
                    return Resources.IconJoystick;

                case InformationBoxIcon.Keys:
                    return Resources.IconKeys;

                case InformationBoxIcon.Locker:
                    return Resources.IconLocker;

                case InformationBoxIcon.Phone:
                    return Resources.IconPhone;

                case InformationBoxIcon.Printer:
                    return Resources.IconPrinter;

                case InformationBoxIcon.Scanner:
                    return Resources.IconScanner;

                case InformationBoxIcon.Speakers:
                    return Resources.IconSpeakers;

                case InformationBoxIcon.None:
                default:
                    return null;
            }
        }

        /// <summary>
        /// Gets the category of the icon.
        /// </summary>
        /// <param name="iconType">Type of the icon.</param>
        /// <returns>A message category corresponding to the iconType</returns>
        internal static InformationBoxMessageCategory GetCategory(InformationBoxIcon iconType)
        {
            switch (iconType)
            {
                case InformationBoxIcon.Asterisk:
                case InformationBoxIcon.Information:
                    return InformationBoxMessageCategory.Asterisk;

                case InformationBoxIcon.Error:
                case InformationBoxIcon.Hand:
                case InformationBoxIcon.Stop:
                    return InformationBoxMessageCategory.Hand;

                case InformationBoxIcon.Exclamation:
                case InformationBoxIcon.Warning:
                    return InformationBoxMessageCategory.Exclamation;

                case InformationBoxIcon.Question:
                    return InformationBoxMessageCategory.Question;

                default:
                    return InformationBoxMessageCategory.Other;
            }
        }
    }
}