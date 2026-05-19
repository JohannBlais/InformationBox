// <copyright file="InformationBoxTitleIcon.cs" company="Johann Blais">
// Copyright (c) 2008 All Right Reserved
// </copyright>
// <author>Johann Blais</author>
// <summary>Represents the icon for the title bar</summary>

namespace InfoBox
{
    using System;
    using System.Drawing;

    /// <summary>
    /// Represents the icon for the title bar.
    /// </summary>
    /// <remarks>
    /// When constructed with a file name, this instance owns the loaded <see cref="Icon"/>
    /// and disposes it through <see cref="Dispose()"/>. When constructed with a caller-supplied
    /// <see cref="Icon"/>, the caller retains ownership; disposing this wrapper will not
    /// dispose the underlying icon.
    /// </remarks>
    public class InformationBoxTitleIcon : IDisposable
    {
        #region Attributes

        /// <summary>
        /// The title icon file
        /// </summary>
        private readonly Icon icon;

        /// <summary>
        /// True when this instance loaded the icon and should dispose it; false when
        /// the caller supplied an existing <see cref="Icon"/>.
        /// </summary>
        private readonly bool ownsIcon;

        /// <summary>
        /// Tracks whether the instance has already been disposed.
        /// </summary>
        private bool disposed;

        #endregion Attributes

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="InformationBoxTitleIcon"/> class
        /// by loading an icon from the specified file. The loaded icon is owned by this
        /// instance and will be disposed when <see cref="Dispose()"/> is called.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public InformationBoxTitleIcon(string fileName)
        {
            this.icon = new Icon(fileName);
            this.ownsIcon = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InformationBoxTitleIcon"/> class
        /// wrapping a caller-supplied icon. The caller retains ownership and is responsible
        /// for disposing <paramref name="icon"/>.
        /// </summary>
        /// <param name="icon">The title icon.</param>
        public InformationBoxTitleIcon(Icon icon)
        {
            this.icon = icon;
            this.ownsIcon = false;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the icon.
        /// </summary>
        /// <value>The title icon.</value>
        internal Icon Icon
        {
            get { return this.icon; }
        }

        #endregion Properties

        #region IDisposable

        /// <summary>
        /// Releases the loaded icon if this instance owns it. Has no effect on a
        /// caller-supplied icon.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases the loaded icon if this instance owns it.
        /// </summary>
        /// <param name="disposing">True when called from <see cref="Dispose()"/>; false when called from a finalizer.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing && this.ownsIcon)
            {
                this.icon?.Dispose();
            }

            this.disposed = true;
        }

        #endregion IDisposable
    }
}
