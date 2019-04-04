using Eto.Forms.Controls.Scintilla.Shared;
using Gdk;
using System;
using System.Runtime.InteropServices;
using Eto.Drawing;

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
                var sp = SetParameter(NativeMethods.SCI_GETTEXT, 0.ToIntPtr(), 0.ToIntPtr());
                return sp.ToString2();
            }
            set
            {
                SetParameter(NativeMethods.SCI_SETTEXT, 0.ToIntPtr(), value.ToIntPtr());
            }
        }

        public ScintillaControlHandler()
        {

            editor = scintilla_new();

            Console.WriteLine("Editor Created " + editor.ToString());

            nativecontrol = new Gtk.Widget(editor);

            Console.WriteLine("Managed Editor Created " + nativecontrol.Name);

            SetParameter(NativeMethods.SCI_STYLERESETDEFAULT, new IntPtr(0), new IntPtr(0));

            SetParameter(NativeMethods.SCI_STYLESETFONT, NativeMethods.STYLE_DEFAULT.ToIntPtr(), "DejaVu Sans Mono".ToIntPtr());
            SetParameter(NativeMethods.SCI_STYLESETSIZE, NativeMethods.STYLE_DEFAULT.ToIntPtr(), 10.ToIntPtr());
            
            SetParameter(NativeMethods.SCI_STYLECLEARALL, new IntPtr(0), new IntPtr(0));

            SetParameter(NativeMethods.SCI_SETLEXER, NativeMethods.SCLEX_PYTHON.ToIntPtr(), new IntPtr(0));

            SetParameter(NativeMethods.SCI_SETPROPERTY, "tab.timmy.whinge.level".ToIntPtr(), "1".ToIntPtr());
            SetParameter(NativeMethods.SCI_SETPROPERTY, "fold".ToIntPtr(), "1".ToIntPtr());

            SetParameter(NativeMethods.SCI_SETAUTOMATICFOLD, NativeMethods.SC_AUTOMATICFOLD_CLICK.ToIntPtr() , IntPtr.Zero);

            SetParameter(NativeMethods.SCI_SETMARGINWIDTHN, 0.ToIntPtr(), 20.ToIntPtr());

            SetParameter(NativeMethods.SCI_SETMARGINWIDTHN, 1.ToIntPtr(), 20.ToIntPtr());
            SetParameter(NativeMethods.SCI_SETMARGINTYPEN, 1.ToIntPtr(), NativeMethods.SC_MARGIN_SYMBOL.ToIntPtr());
            SetParameter(NativeMethods.SCI_SETMARGINMASKN, 1.ToIntPtr(), NativeMethods.SC_MASK_FOLDERS.ToIntPtr());
            SetParameter(NativeMethods.SCI_SETMARGINSENSITIVEN, 1.ToIntPtr(), 1.ToIntPtr());

            SetParameter(NativeMethods.SCI_MARKERDEFINE, NativeMethods.SC_MARKNUM_FOLDER.ToIntPtr(), NativeMethods.SC_MARK_BOXPLUS.ToIntPtr());
            SetParameter(NativeMethods.SCI_MARKERDEFINE, NativeMethods.SC_MARKNUM_FOLDEROPEN.ToIntPtr(), NativeMethods.SC_MARK_BOXMINUS.ToIntPtr());
            SetParameter(NativeMethods.SCI_MARKERDEFINE, NativeMethods.SC_MARKNUM_FOLDEROPENMID.ToIntPtr(), NativeMethods.SC_MARK_BOXMINUSCONNECTED.ToIntPtr());
            SetParameter(NativeMethods.SCI_MARKERDEFINE, NativeMethods.SC_MARKNUM_FOLDEREND.ToIntPtr(), NativeMethods.SC_MARK_BOXPLUSCONNECTED.ToIntPtr());
            SetParameter(NativeMethods.SCI_MARKERDEFINE, NativeMethods.SC_MARKNUM_FOLDERSUB.ToIntPtr(), NativeMethods.SC_MARK_VLINE.ToIntPtr());
            SetParameter(NativeMethods.SCI_MARKERDEFINE, NativeMethods.SC_MARKNUM_FOLDERTAIL.ToIntPtr(), NativeMethods.SC_MARK_LCORNERCURVE.ToIntPtr());
            
            var forecolor = Int32.Parse(SystemColors.ControlText.ToHex(false).TrimStart('#'), System.Globalization.NumberStyles.HexNumber);
            var backcolor = Int32.Parse(SystemColors.ControlBackground.ToHex(false).TrimStart('#'), System.Globalization.NumberStyles.HexNumber);

            SetParameter(NativeMethods.SCI_MARKERSETFORE, NativeMethods.SC_MARKNUM_FOLDER.ToIntPtr(), backcolor.ToIntPtr());
            SetParameter(NativeMethods.SCI_MARKERSETFORE, NativeMethods.SC_MARKNUM_FOLDEROPEN.ToIntPtr(), backcolor.ToIntPtr());
            SetParameter(NativeMethods.SCI_MARKERSETFORE, NativeMethods.SC_MARKNUM_FOLDEROPENMID.ToIntPtr(), backcolor.ToIntPtr());
            SetParameter(NativeMethods.SCI_MARKERSETFORE, NativeMethods.SC_MARKNUM_FOLDEREND.ToIntPtr(), backcolor.ToIntPtr());
            SetParameter(NativeMethods.SCI_MARKERSETFORE, NativeMethods.SC_MARKNUM_FOLDERSUB.ToIntPtr(), backcolor.ToIntPtr());
            SetParameter(NativeMethods.SCI_MARKERSETFORE, NativeMethods.SC_MARKNUM_FOLDERTAIL.ToIntPtr(), backcolor.ToIntPtr());

            SetParameter(NativeMethods.SCI_MARKERSETBACK, NativeMethods.SC_MARKNUM_FOLDER.ToIntPtr(), forecolor.ToIntPtr());
            SetParameter(NativeMethods.SCI_MARKERSETBACK, NativeMethods.SC_MARKNUM_FOLDEROPEN.ToIntPtr(), forecolor.ToIntPtr());
            SetParameter(NativeMethods.SCI_MARKERSETBACK, NativeMethods.SC_MARKNUM_FOLDEROPENMID.ToIntPtr(), forecolor.ToIntPtr());
            SetParameter(NativeMethods.SCI_MARKERSETBACK, NativeMethods.SC_MARKNUM_FOLDEREND.ToIntPtr(), forecolor.ToIntPtr());
            SetParameter(NativeMethods.SCI_MARKERSETBACK, NativeMethods.SC_MARKNUM_FOLDERSUB.ToIntPtr(), forecolor.ToIntPtr());
            SetParameter(NativeMethods.SCI_MARKERSETBACK, NativeMethods.SC_MARKNUM_FOLDERTAIL.ToIntPtr(), forecolor.ToIntPtr());

            SetParameter(NativeMethods.SCI_STYLESETFORE, NativeMethods.SCE_C_COMMENT.ToIntPtr(), 0x008000.ToIntPtr());
            SetParameter(NativeMethods.SCI_STYLESETFORE, NativeMethods.SCE_C_COMMENTLINE.ToIntPtr(), 0x008000.ToIntPtr());
            SetParameter(NativeMethods.SCI_STYLESETFORE, NativeMethods.SCE_C_NUMBER.ToIntPtr(), 0x808000.ToIntPtr());
            SetParameter(NativeMethods.SCI_STYLESETFORE, NativeMethods.SCE_C_WORD.ToIntPtr(), 0x800000.ToIntPtr());
            SetParameter(NativeMethods.SCI_STYLESETFORE, NativeMethods.SCE_C_STRING.ToIntPtr(), 0x800080.ToIntPtr());
            SetParameter(NativeMethods.SCI_STYLESETBOLD, NativeMethods.SCE_C_OPERATOR.ToIntPtr(), 1.ToIntPtr());

            string python2 = "and as assert break class continue def del elif else except exec finally for from global if import in" +
            " is lambda not or pass print raise return try while with yield";
            string python3 = "False None True and as assert break class continue def del elif else except finally for from global i" +
            "f import in is lambda nonlocal not or pass raise return try while with yield";

            SetParameter(NativeMethods.SCI_SETKEYWORDS, 0.ToIntPtr(), (python2 + (" " + python3)).ToIntPtr());

            var txt = "from datetime import datetime\nnow = datetime.now()\nmm = str(now.month)\ndd = str(now.day)\nyyyy = str(now.year)\nhour = str(now.hour)\nmi = str(now.minute)\nss = str(now.second)\nprint mm +'/' + dd + '/' + yyyy ' ' + hour + ':' + mi + ':' + ss";

            SetParameter(NativeMethods.SCI_SETTEXT, new IntPtr(0), txt.ToIntPtr());

            Console.WriteLine("Managed Editor Added");
            
            this.Control = nativecontrol;
        }

        public IntPtr SetParameter(int message, IntPtr param1, IntPtr param2)
        {

            return scintilla_send_message(editor, message, param1, param2);

        }

    }

}
