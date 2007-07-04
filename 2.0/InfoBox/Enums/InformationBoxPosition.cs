using System;
using System.Collections.Generic;
using System.Text;

namespace InfoBox
{
    /// <summary>
    /// Specifies constants defining the position of the <see cref="InformationBox"/>.
    /// </summary>
    public enum InformationBoxPosition
    {
        /// <summary>
        /// the <see cref="InformationBox"/> will be centered on the parent window. This is the default value.
        /// </summary>
        CenterOnParent,
        /// <summary>
        /// the <see cref="InformationBox"/> will be centered on the screen.
        /// </summary>
        CenterOnScreen,
    }
}
