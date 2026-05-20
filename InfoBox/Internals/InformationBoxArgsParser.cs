// <copyright file="InformationBoxArgsParser.cs" company="Johann Blais">
// Copyright (c) 2008 All Right Reserved
// </copyright>
// <author>Johann Blais</author>
// <summary>Parses the loosely-typed params object[] of InformationBoxForm</summary>

namespace InfoBox.Internals
{
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// Parses the loosely-typed <c>params object[]</c> handed to
    /// <see cref="InformationBoxForm"/>'s flexible constructor into a strongly-typed
    /// <see cref="InformationBoxArgs"/>.
    /// </summary>
    /// <remarks>
    /// This is a pure function with no WinForms dependencies on the host control,
    /// so it can be exercised directly from unit tests without instantiating the form.
    /// First-match-wins ordering matches the historical if/else chain it replaces.
    /// Last-value-wins for repeated kinds (the original assigned through every loop
    /// iteration, leaving the final occurrence in place).
    /// </remarks>
    internal static class InformationBoxArgsParser
    {
        /// <summary>
        /// Parses <paramref name="parameters"/> into an <see cref="InformationBoxArgs"/>.
        /// <see langword="null"/> entries are skipped. Unrecognized entry kinds are
        /// silently ignored (same behavior as the original dispatcher).
        /// </summary>
        /// <param name="parameters">
        /// The raw parameter array. May itself be <see langword="null"/>, in which case
        /// a default <see cref="InformationBoxArgs"/> is returned.
        /// </param>
        /// <returns>The aggregated dispatch result.</returns>
        public static InformationBoxArgs Parse(object[] parameters)
        {
            var args = new InformationBoxArgs();

            if (parameters is null)
            {
                return args;
            }

            // First pass: detect InformationBoxInitialization.FromParametersOnly,
            // which suppresses scope loading. Any other value (or absence) leaves
            // LoadScope at its default true.
            foreach (object parameter in parameters)
            {
                if (parameter is InformationBoxInitialization init &&
                    init == InformationBoxInitialization.FromParametersOnly)
                {
                    args.LoadScope = false;
                }
            }

            // Second pass: type-dispatch each non-null entry to its destination on args.
            int stringCount = 0;

            foreach (object parameter in parameters)
            {
                if (parameter is null)
                {
                    continue;
                }

                switch (parameter)
                {
                    // Strings are assigned positionally:
                    //   0 -> caption, 1 -> help file, 2 -> help topic, 3 -> "do not show again" text.
                    // 5th+ string keeps incrementing stringCount but is otherwise ignored.
                    case string s:
                        switch (stringCount)
                        {
                            case 0: args.Title = s; break;
                            case 1: args.HelpFile = s; break;
                            case 2: args.HelpTopic = s; break;
                            case 3: args.DoNotShowAgainText = s; break;
                        }

                        stringCount++;
                        break;

                    case InformationBoxButtons b:
                        args.Buttons = b;
                        break;

                    case InformationBoxIcon i:
                        args.Icon = i;
                        break;

                    case Icon ico:
                        // Form will set iconType to UserDefined when CustomIcon is non-null.
                        args.CustomIcon = ico;
                        break;

                    case InformationBoxDefaultButton db:
                        args.DefaultButton = db;
                        break;

                    case string[] labels:
                        if (labels.Length > 0)
                        {
                            args.ButtonUser1Text = labels[0];
                        }

                        if (labels.Length > 1)
                        {
                            args.ButtonUser2Text = labels[1];
                        }

                        if (labels.Length > 2)
                        {
                            args.ButtonUser3Text = labels[2];
                        }

                        break;

                    case InformationBoxButtonsLayout bl:
                        args.ButtonsLayout = bl;
                        break;

                    case InformationBoxAutoSizeMode asm:
                        args.AutoSizeMode = asm;
                        break;

                    case InformationBoxPosition pos:
                        args.Position = pos;
                        break;

                    case bool showHelp:
                        args.ShowHelpButton = showHelp;
                        break;

                    case HelpNavigator hn:
                        args.HelpNavigator = hn;
                        break;

                    case InformationBoxCheckBox cb:
                        args.CheckBox = cb;
                        break;

                    case InformationBoxStyle st:
                        args.Style = st;
                        break;

                    case AutoCloseParameters ac:
                        args.AutoClose = ac;
                        break;

                    case DesignParameters dp:
                        args.Design = dp;
                        break;

                    case FontParameters fp:
                        args.FontParameters = fp;
                        break;

                    case Font f:
                        // Direct font parameter - use for both message and title via FontParameters.
                        args.FontParameters = new FontParameters(f);
                        break;

                    case InformationBoxTitleIconStyle tis:
                        args.TitleStyle = tis;
                        break;

                    case InformationBoxTitleIcon ti:
                        args.TitleIcon = ti.Icon;
                        break;

                    case MessageBoxButtons mb:
                        args.Buttons = MessageBoxEnumConverter.Parse(mb);
                        break;

                    case MessageBoxIcon mi:
                        args.Icon = MessageBoxEnumConverter.Parse(mi);
                        break;

                    case MessageBoxDefaultButton mdb:
                        args.DefaultButton = MessageBoxEnumConverter.Parse(mdb);
                        break;

                    case InformationBoxBehavior beh:
                        args.Behavior = beh;
                        break;

                    case AsyncResultCallback arc:
                        args.Callback = arc;
                        break;

                    case InformationBoxOpacity op:
                        args.Opacity = op;
                        break;

                    case Form parentForm:
                        args.Parent = parentForm;
                        break;

                    case InformationBoxOrder ord:
                        args.Order = ord;
                        break;

                    case InformationBoxSound sd:
                        args.Sound = sd;
                        break;
                }
            }

            return args;
        }
    }
}
