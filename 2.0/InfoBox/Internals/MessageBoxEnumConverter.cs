using System;
using System.Windows.Forms;

namespace InfoBox.Internals
{
    internal static class MessageBoxEnumConverter
    {
        /// <summary>
        /// Parses the specified MessageBoxButtons value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        internal static InformationBoxButtons Parse(MessageBoxButtons value)
        {
            return (InformationBoxButtons) Enum.Parse(typeof(InformationBoxButtons), value.ToString());
        }

        /// <summary>
        /// Parses the specified MessageBoxIcon value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        internal static InformationBoxIcon Parse(MessageBoxIcon value)
        {
            return (InformationBoxIcon) Enum.Parse(typeof(InformationBoxIcon), value.ToString());
        }

        /// <summary>
        /// Parses the specified MessageBoxDefaultButton value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        internal static InformationBoxDefaultButton Parse(MessageBoxDefaultButton value)
        {
            return (InformationBoxDefaultButton) Enum.Parse(typeof(InformationBoxDefaultButton), value.ToString());
        }
    }
}