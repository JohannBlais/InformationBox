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
					return Resources.IconInfo;
                case InformationBoxIcon.Information:
                    return Resources.IconInfo;
                
                case InformationBoxIcon.Error:
					return Resources.IconError;
				case InformationBoxIcon.Hand:
					return Resources.IconError;
				case InformationBoxIcon.Stop:
                    return Resources.IconError;
                
                case InformationBoxIcon.Exclamation:
					return Resources.IconWarning;
				case InformationBoxIcon.Warning:
                    return Resources.IconWarning;
                
                case InformationBoxIcon.Question:
                    return Resources.IconQuestion;
                
                case InformationBoxIcon.Success:
                    return Resources.IconGood;
                
                case InformationBoxIcon.None:
                default:
                    return null;
            }
        }
    }
}
