using System;
using Eto.Forms;
using Eto;
using System.IO;
using Eto.Forms.Controls.Scintilla.Shared;

namespace Eto.Forms.Controls.Scintilla.Tests
{
    class Tests
    {

        [STAThread]
        static void Main()
        {

            // set renderers

            string WinR, LinuxR, MacR;

            WinR = "WinForms"; // WPF, WinForms, GTK
            LinuxR = "GTK"; // GTK, WinForms
            MacR = "XammMac2"; // XamMac, WinForms, GTK

            Eto.Platform platform = null;

            if (RunningPlatform() == Platform.Windows)
            {
                switch (WinR)
                {
                    case "WinForms":
                        platform = new Eto.WinForms.Platform();
                        platform.Add<ScintillaControl.IScintillaControl>(() => new WinForms.ScintillaControlHandler());
                        break;
                }
            }
            else if (RunningPlatform() == Platform.Linux)
            {
                switch (LinuxR)
                {
                    case "WinForms":
                        platform = new Eto.WinForms.Platform();
                        platform.Add<ScintillaControl.IScintillaControl>(() => new WinForms.ScintillaControlHandler());
                        break;
                    case "GTK":
                        platform = new Eto.GtkSharp.Platform();
                        platform.Add<ScintillaControl.IScintillaControl>(() => new GTK.ScintillaControlHandler());
                        break;
                }
            }
            else if (RunningPlatform() == Platform.Mac)
            {
                switch (MacR)
                {
                    case "XamMac2":
                        platform = new Eto.Mac.Platform();
                        //platform.Add<ScintillaControl.IScintillaControl>(() => new Mac.ScintillaControlHandler());
                        break;
                }
            }

            // test control

            new Application(platform).Run(new TestForm());

        }

        public enum Platform
        {
            Windows,
            Linux,
            Mac
        }

        public static Platform RunningPlatform()
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Unix:
                    if (Directory.Exists("/Applications") & Directory.Exists("/System") & Directory.Exists("/Users") & Directory.Exists("/Volumes"))
                    {
                        return Platform.Mac;
                    }
                    else
                    {
                        return Platform.Linux;
                    }
                case PlatformID.MacOSX:
                    return Platform.Mac;
                default:
                    return Platform.Windows;
            }
        }

    }
}
