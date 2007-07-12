namespace InfoBox
{
    using System.Drawing;
    
    internal class IconHelper
    {
		/// <summary>
		/// Return the <see cref="System.Drawing.Icon"/> associated with the specified <see cref="InformationBoxIcon"/>.
		/// </summary>
		/// <param name="iconType">Type of the icon.</param>
		/// <returns></returns>
		public static Icon FromEnum(InformationBoxIcon iconType)
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
    }
}
