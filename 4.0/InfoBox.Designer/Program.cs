// <copyright file="Program.cs" company="Johann Blais">
// Copyright (c) 2008 All Right Reserved
// </copyright>
// <author>Johann Blais</author>
// <summary>Contains the entry point</summary>

namespace InfoBox.Test
{
    using System;
    using System.Windows.Forms;
    using InfoBox.Designer;

    /// <summary>
    /// Entry point for the designer
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new InformationBoxDesigner());
        }
    }
}