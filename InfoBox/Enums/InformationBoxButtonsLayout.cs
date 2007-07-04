using System;
using System.Collections.Generic;
using System.Text;

namespace InfoBox
{
    /// <summary>
    /// Specifies constants defining how to place buttons on the <see cref="InformationBox"/>.
    /// </summary>
    public enum InformationBoxButtonsLayout
    {
        /// <summary>
        /// Buttons are grouped on the left side.
        /// </summary>
        GroupLeft,
        /// <summary>
        /// Buttons are grouped in the middle.
        /// </summary>
        GroupMiddle,
        /// <summary>
        /// Buttons are grouped on the right side.
        /// </summary>
        GroupRight,
        /// <summary>
        /// Spacing is constant between the buttons and the edges of the <see cref="InformationBox"/>.
        /// </summary>
        Separate,
    }
}
