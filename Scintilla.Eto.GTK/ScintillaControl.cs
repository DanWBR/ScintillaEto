using Eto.Forms.Controls.Scintilla.Shared;
using Eto.Drawing;
using Gdk;
using System;
using System.Runtime.InteropServices;

namespace Eto.Forms.Controls.Scintilla.GTK
{

    public class ScintillaControlHandler : Eto.GtkSharp.Forms.GtkControl<Gtk.Widget, ScintillaControl, ScintillaControl.ICallback>, ScintillaControl.IScintillaControl
    {

        [DllImport("scintilla", CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr scintilla_new();

        [DllImport("scintilla", CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr scintilla_send_message(IntPtr ptr, int iMessage, IntPtr wParam, IntPtr lParam);

        IntPtr editor;
        Gtk.Widget nativecontrol;

        public string ScriptText
        {
            get
            {
                var sp = SetParameter(Constants.SCI_GETTEXT, 0.ToIntPtr(), 0.ToIntPtr());
                return sp.ToString2();
            }
            set
            {
                SetParameter(Constants.SCI_SETTEXT, 0.ToIntPtr(), value.ToIntPtr());
            }
        }

        public ScintillaControlHandler()
        {

            editor = scintilla_new();

            Console.WriteLine("Editor Created " + editor.ToString());

            nativecontrol = new Gtk.Widget(editor);

            Console.WriteLine("Managed Editor Created " + nativecontrol.Name);

            SetParameter(Constants.SCI_STYLERESETDEFAULT, new IntPtr(0), new IntPtr(0));

            SetParameter(Constants.SCI_STYLESETFONT, Constants.STYLE_DEFAULT.ToIntPtr(), "DejaVu Sans Mono".ToIntPtr());
            SetParameter(Constants.SCI_STYLESETSIZE, Constants.STYLE_DEFAULT.ToIntPtr(), 10.ToIntPtr());

            SetParameter(Constants.SCI_STYLECLEARALL, new IntPtr(0), new IntPtr(0));

            SetParameter(Constants.SCI_SETLEXER, Constants.SCLEX_PYTHON.ToIntPtr(), new IntPtr(0));

            SetParameter(Constants.SCI_SETPROPERTY, "tab.timmy.whinge.level".ToIntPtr(), "1".ToIntPtr());
            SetParameter(Constants.SCI_SETPROPERTY, "fold".ToIntPtr(), "1".ToIntPtr());

            SetParameter(Constants.SCI_SETAUTOMATICFOLD, Constants.SC_AUTOMATICFOLD_CLICK.ToIntPtr(), IntPtr.Zero);

            SetParameter(Constants.SCI_SETMARGINWIDTHN, 0.ToIntPtr(), 35.ToIntPtr());

            SetParameter(Constants.SCI_SETMARGINWIDTHN, 1.ToIntPtr(), 20.ToIntPtr());
            SetParameter(Constants.SCI_SETMARGINTYPEN, 1.ToIntPtr(), Constants.SC_MARGIN_SYMBOL.ToIntPtr());
            SetParameter(Constants.SCI_SETMARGINMASKN, 1.ToIntPtr(), Constants.SC_MASK_FOLDERS.ToIntPtr());
            SetParameter(Constants.SCI_SETMARGINSENSITIVEN, 1.ToIntPtr(), 1.ToIntPtr());

            SetParameter(Constants.SCI_MARKERDEFINE, Constants.SC_MARKNUM_FOLDER.ToIntPtr(), Constants.SC_MARK_BOXPLUS.ToIntPtr());
            SetParameter(Constants.SCI_MARKERDEFINE, Constants.SC_MARKNUM_FOLDEROPEN.ToIntPtr(), Constants.SC_MARK_BOXMINUS.ToIntPtr());
            SetParameter(Constants.SCI_MARKERDEFINE, Constants.SC_MARKNUM_FOLDEROPENMID.ToIntPtr(), Constants.SC_MARK_BOXMINUSCONNECTED.ToIntPtr());
            SetParameter(Constants.SCI_MARKERDEFINE, Constants.SC_MARKNUM_FOLDEREND.ToIntPtr(), Constants.SC_MARK_BOXPLUSCONNECTED.ToIntPtr());
            SetParameter(Constants.SCI_MARKERDEFINE, Constants.SC_MARKNUM_FOLDERSUB.ToIntPtr(), Constants.SC_MARK_VLINE.ToIntPtr());
            SetParameter(Constants.SCI_MARKERDEFINE, Constants.SC_MARKNUM_FOLDERTAIL.ToIntPtr(), Constants.SC_MARK_LCORNERCURVE.ToIntPtr());
            SetParameter(Constants.SCI_MARKERDEFINE, Constants.SC_MARKNUM_FOLDERMIDTAIL.ToIntPtr(), Constants.SC_MARK_TCORNER.ToIntPtr());

            var forecolor = Int32.Parse(SystemColors.ControlText.ToHex(false).TrimStart('#'), System.Globalization.NumberStyles.HexNumber);
            var backcolor = Int32.Parse(SystemColors.ControlBackground.ToHex(false).TrimStart('#'), System.Globalization.NumberStyles.HexNumber);

            SetParameter(Constants.SCI_MARKERSETFORE, Constants.SC_MARKNUM_FOLDER.ToIntPtr(), backcolor.ToIntPtr());
            SetParameter(Constants.SCI_MARKERSETFORE, Constants.SC_MARKNUM_FOLDEROPEN.ToIntPtr(), backcolor.ToIntPtr());
            SetParameter(Constants.SCI_MARKERSETFORE, Constants.SC_MARKNUM_FOLDEROPENMID.ToIntPtr(), backcolor.ToIntPtr());
            SetParameter(Constants.SCI_MARKERSETFORE, Constants.SC_MARKNUM_FOLDEREND.ToIntPtr(), backcolor.ToIntPtr());
            SetParameter(Constants.SCI_MARKERSETFORE, Constants.SC_MARKNUM_FOLDERSUB.ToIntPtr(), backcolor.ToIntPtr());
            SetParameter(Constants.SCI_MARKERSETFORE, Constants.SC_MARKNUM_FOLDERTAIL.ToIntPtr(), backcolor.ToIntPtr());

            SetParameter(Constants.SCI_MARKERSETBACK, Constants.SC_MARKNUM_FOLDER.ToIntPtr(), forecolor.ToIntPtr());
            SetParameter(Constants.SCI_MARKERSETBACK, Constants.SC_MARKNUM_FOLDEROPEN.ToIntPtr(), forecolor.ToIntPtr());
            SetParameter(Constants.SCI_MARKERSETBACK, Constants.SC_MARKNUM_FOLDEROPENMID.ToIntPtr(), forecolor.ToIntPtr());
            SetParameter(Constants.SCI_MARKERSETBACK, Constants.SC_MARKNUM_FOLDEREND.ToIntPtr(), forecolor.ToIntPtr());
            SetParameter(Constants.SCI_MARKERSETBACK, Constants.SC_MARKNUM_FOLDERSUB.ToIntPtr(), forecolor.ToIntPtr());
            SetParameter(Constants.SCI_MARKERSETBACK, Constants.SC_MARKNUM_FOLDERTAIL.ToIntPtr(), forecolor.ToIntPtr());

            SetParameter(Constants.SCI_STYLESETFORE, Constants.SCE_P_COMMENTBLOCK.ToIntPtr(), 0x008000.ToIntPtr());
            SetParameter(Constants.SCI_STYLESETFORE, Constants.SCE_P_COMMENTLINE.ToIntPtr(), 0x008000.ToIntPtr());
            SetParameter(Constants.SCI_STYLESETFORE, Constants.SCE_P_NUMBER.ToIntPtr(), 0x808000.ToIntPtr());
            SetParameter(Constants.SCI_STYLESETFORE, Constants.SCE_P_WORD.ToIntPtr(), 0x800000.ToIntPtr());
            SetParameter(Constants.SCI_STYLESETFORE, Constants.SCE_P_STRING.ToIntPtr(), 0x800080.ToIntPtr());
            SetParameter(Constants.SCI_STYLESETBOLD, Constants.SCE_P_OPERATOR.ToIntPtr(), 1.ToIntPtr());

            string python2 = "and as assert break class continue def del elif else except exec finally for from global if import in" +
            " is lambda not or pass print raise return try while with yield";
            string python3 = "False None True and as assert break class continue def del elif else except finally for from global i" +
            "f import in is lambda nonlocal not or pass raise return try while with yield";

            SetParameter(Constants.SCI_SETKEYWORDS, 0.ToIntPtr(), (python2 + (" " + python3)).ToIntPtr());
            
            Console.WriteLine("Managed Editor Added");

            this.Control = nativecontrol;
        }

        public IntPtr SetParameter(int message, IntPtr param1, IntPtr param2)
        {

            return scintilla_send_message(editor, message, param1, param2);

        }

        public void SetKeywords(int level, string keywords)
        {
            SetParameter(Constants.SCI_SETKEYWORDS, level.ToIntPtr(), keywords.ToIntPtr());
        }

        public void SetStyle(int styleID, int item, object value)
        {
            if (value is Eto.Drawing.Color)
            {
                var color = Int32.Parse(((Eto.Drawing.Color)value).ToHex(false).TrimStart('#'), System.Globalization.NumberStyles.HexNumber);
                SetParameter(styleID, item.ToIntPtr(), color.ToIntPtr());
            }
            else
            {
                SetParameter(styleID, item.ToIntPtr(), ((int)value).ToIntPtr());
            }
        }

        public void SetFont(string fontname)
        {
            SetParameter(Constants.SCI_STYLESETFONT, Constants.STYLE_DEFAULT.ToIntPtr(), fontname.ToIntPtr());
        }

        public void SetFontSize(int fontsize)
        {
            SetParameter(Constants.SCI_STYLESETSIZE, Constants.STYLE_DEFAULT.ToIntPtr(), fontsize.ToIntPtr());
        }

        public void ResetDefaultStyle()
        {
            SetParameter(Constants.SCI_STYLERESETDEFAULT, new IntPtr(0), new IntPtr(0));
        }

        public void ClearAllStyles()
        {
            SetParameter(Constants.SCI_STYLECLEARALL, new IntPtr(0), new IntPtr(0));
        }
    }

}
