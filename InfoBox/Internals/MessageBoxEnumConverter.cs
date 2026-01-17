// <copyright file="MessageBoxEnumConverter.cs" company="Johann Blais">
// Copyright (c) 2008 All Right Reserved
// </copyright>
// <author>Johann Blais</author>
// <summary>Contains methods to convert from MessageBox enums to InformationBox enums</summary>

namespace InfoBox.Internals
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// Contains methods to convert from MessageBox enums to InformationBox enums.
    /// </summary>
    internal static class MessageBoxEnumConverter
    {
        /// <summary>
        /// Parses the specified MessageBoxButtons value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The converted value</returns>
        /// <exception cref="ArgumentException">Thrown when conversion fails.</exception>
        internal static InformationBoxButtons Parse(MessageBoxButtons value)
        {
            if (Enum.TryParse<InformationBoxButtons>(value.ToString(), out var result))
            {
                return result;
            }

            throw new ArgumentException($"Cannot convert '{value}' to InformationBoxButtons", nameof(value));
        }

        /// <summary>
        /// Parses the specified MessageBoxIcon value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The converted value</returns>
        /// <exception cref="ArgumentException">Thrown when conversion fails.</exception>
        internal static InformationBoxIcon Parse(MessageBoxIcon value)
        {
            if (Enum.TryParse<InformationBoxIcon>(value.ToString(), out var result))
            {
                return result;
            }

            throw new ArgumentException($"Cannot convert '{value}' to InformationBoxIcon", nameof(value));
        }

        /// <summary>
        /// Parses the specified MessageBoxDefaultButton value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The converted value</returns>
        /// <exception cref="ArgumentException">Thrown when conversion fails.</exception>
        internal static InformationBoxDefaultButton Parse(MessageBoxDefaultButton value)
        {
            if (Enum.TryParse<InformationBoxDefaultButton>(value.ToString(), out var result))
            {
                return result;
            }

            throw new ArgumentException($"Cannot convert '{value}' to InformationBoxDefaultButton", nameof(value));
        }
    }
}