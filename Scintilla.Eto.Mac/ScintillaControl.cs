using AppKit;
using Eto.Drawing;
using Eto.Mac;
using Eto.Forms.Controls.Scintilla.Shared;
using ScintillaBinder;
using System;

namespace Eto.Forms.Controls.Scintilla.Mac
{
    public class ScintillaControlHandler : Eto.Mac.Forms.MacView<NSView, ScintillaControl, ScintillaControl.ICallback>, ScintillaControl.IScintillaControl
    {

        ScintillaBinder.ScintillaView nativecontrol;

        public string ScriptText
        {
            get
            {
                return nativecontrol.String;
            }
            set
            {
                nativecontrol.String = value;
            }
        }

        public ScintillaControlHandler()
        {

            Console.WriteLine("Creating ScintillaView...");

            nativecontrol = new ScintillaView();
            
            Console.WriteLine("ScintillaView Created!");

            SetParameter(Constants.SCI_STYLERESETDEFAULT, 0, 0);

            SetParameter(Constants.SCI_STYLESETFONT, Constants.STYLE_DEFAULT, "Menlo");
            SetParameter(Constants.SCI_STYLESETSIZE, Constants.STYLE_DEFAULT, 12);

            SetParameter(Constants.SCI_STYLECLEARALL, 0, 0);

            SetParameter(Constants.SCI_SETLEXER, Constants.SCLEX_PYTHON, 0);
            
            SetParameter(Constants.SCI_SETPROPERTY, "fold", "1");
            SetParameter(Constants.SCI_SETPROPERTY, "fold.compact", "1");
            SetParameter(Constants.SCI_SETPROPERTY, "fold.comment", "1");
            SetParameter(Constants.SCI_SETPROPERTY, "fold.preprocessor", "1");

            SetParameter(Constants.SCI_SETAUTOMATICFOLD, Constants.SC_AUTOMATICFOLD_CLICK, 0);

            SetParameter(Constants.SCI_SETMARGINWIDTHN, 0, 30);

            SetParameter(Constants.SCI_SETMARGINWIDTHN, 1, 16);

            SetParameter(Constants.SCI_SETMARGINWIDTHN, 2, 16);
            SetParameter(Constants.SCI_SETMARGINTYPEN, 2, Constants.SC_MARGIN_SYMBOL);
            SetParameter(Constants.SCI_SETMARGINMASKN, 2, Constants.SC_MASK_FOLDERS);
            SetParameter(Constants.SCI_SETMARGINSENSITIVEN, 2, 1);

            SetParameter(Constants.SCI_MARKERDEFINE, Constants.SC_MARKNUM_FOLDER, Constants.SC_MARK_BOXPLUS);
            SetParameter(Constants.SCI_MARKERDEFINE, Constants.SC_MARKNUM_FOLDEROPEN, Constants.SC_MARK_BOXMINUS);
            SetParameter(Constants.SCI_MARKERDEFINE, Constants.SC_MARKNUM_FOLDEROPENMID, Constants.SC_MARK_BOXMINUSCONNECTED);
            SetParameter(Constants.SCI_MARKERDEFINE, Constants.SC_MARKNUM_FOLDEREND, Constants.SC_MARK_BOXPLUSCONNECTED);
            SetParameter(Constants.SCI_MARKERDEFINE, Constants.SC_MARKNUM_FOLDERSUB, Constants.SC_MARK_VLINE);
            SetParameter(Constants.SCI_MARKERDEFINE, Constants.SC_MARKNUM_FOLDERTAIL, Constants.SC_MARK_LCORNER);
            SetParameter(Constants.SCI_MARKERDEFINE, Constants.SC_MARKNUM_FOLDERMIDTAIL, Constants.SC_MARK_TCORNER);

            var forecolor = SystemColors.ControlText.ToNSUI();
            var backcolor = SystemColors.ControlBackground.ToNSUI();

            for (int n = 25; n < 32; ++n) // Markers 25..31 are reserved for folding.
            {
                SetParameter(Constants.SCI_MARKERSETFORE, n, backcolor);
                SetParameter(Constants.SCI_MARKERSETBACK, n, forecolor);
            }
            
            SetParameter(Constants.SCI_STYLESETFORE, Constants.SCE_P_COMMENTLINE, Color.FromArgb(0x00, 0x7F, 0x00).ToNSUI());
            SetParameter(Constants.SCI_STYLESETITALIC, Constants.SCE_P_COMMENTLINE, 1);
            SetParameter(Constants.SCI_STYLESETFORE, Constants.SCE_P_NUMBER, Color.FromArgb(0x00, 0x7F, 0x7F).ToNSUI());
            SetParameter(Constants.SCI_STYLESETFORE, Constants.SCE_P_STRING, Color.FromArgb(0x7F, 0x00, 0x7F).ToNSUI());
            SetParameter(Constants.SCI_STYLESETFORE, Constants.SCE_P_CHARACTER, Color.FromArgb(0x7F, 0x00, 0x7F).ToNSUI());
            SetParameter(Constants.SCI_STYLESETFORE, Constants.SCE_P_WORD, Color.FromArgb(0x00, 0x00, 0x7F).ToNSUI());
            SetParameter(Constants.SCI_STYLESETBOLD, Constants.SCE_P_WORD, 1);
            SetParameter(Constants.SCI_STYLESETFORE, Constants.SCE_P_TRIPLEDOUBLE, Color.FromArgb(0x7F, 0x00, 0x00).ToNSUI());
            SetParameter(Constants.SCI_STYLESETFORE, Constants.SCE_P_TRIPLEDOUBLE, Color.FromArgb(0x7F, 0x00, 0x00).ToNSUI());
            SetParameter(Constants.SCI_STYLESETFORE, Constants.SCE_P_CLASSNAME,  Color.FromArgb(0x00, 0x00, 0xFF).ToNSUI());
            SetParameter(Constants.SCI_STYLESETBOLD, Constants.SCE_P_CLASSNAME, 1);
            SetParameter(Constants.SCI_STYLESETFORE, Constants.SCE_P_DEFNAME,  Color.FromArgb(0x00, 0x7F, 0x7F).ToNSUI());
            SetParameter(Constants.SCI_STYLESETBOLD, Constants.SCE_P_DEFNAME,  1);
            SetParameter(Constants.SCI_STYLESETBOLD, Constants.SCE_P_OPERATOR,  1);
            SetParameter(Constants.SCI_STYLESETFORE, Constants.SCE_P_COMMENTBLOCK,  Color.FromArgb(0x7F, 0x7F, 0x7F).ToNSUI());
            SetParameter(Constants.SCI_STYLESETITALIC, Constants.SCE_P_COMMENTBLOCK, 1);
            SetParameter(Constants.SCI_STYLESETFORE, Constants.SCE_P_STRINGEOL,  Color.FromArgb(0x00, 0x00, 0x00).ToNSUI());
            SetParameter(Constants.SCI_STYLESETBACK, Constants.SCE_P_STRINGEOL,  Color.FromArgb(0xE0, 0xC0, 0xE0).ToNSUI());
            SetParameter(Constants.SCI_STYLESETEOLFILLED, Constants.SCE_P_STRINGEOL,  1);
            SetParameter(Constants.SCI_STYLESETFORE, Constants.SCE_P_WORD2,  Color.FromArgb(0x40, 0x70, 0x90).ToNSUI());
            SetParameter(Constants.SCI_STYLESETFORE, Constants.SCE_P_DECORATOR, Color.FromArgb(0x80, 0x50, 0x00).ToNSUI());

            string python2 = "and as assert break class continue def del elif else except exec finally for from global if import in" +
            " is lambda not or pass print raise return try while with yield";
            string python3 = "False None True and as assert break class continue def del elif else except finally for from global i" +
            "f import in is lambda nonlocal not or pass raise return try while with yield";

            SetParameter(Constants.SCI_SETKEYWORDS, 0, (python2 + " " + python3));
            
            var infobar = new InfoBar();
            infobar.Bounds = new CoreGraphics.CGRect(0, 0, 400, 0);
            infobar.SetDisplay(IBDisplay.All);

            nativecontrol.SetInfoBar(infobar, false);

            this.Control = nativecontrol;

        }

        public void SetParameter(int message, object param1, object param2)
        {

            if (param1 is int)
            {
                if (param2 is int)
                {
                    Console.WriteLine("ScintillaView set int param");
                    nativecontrol.SetGeneralProperty(message, (int)param1, (int)param2);
                }
                else if (param2 is uint)
                {
                    Console.WriteLine("ScintillaView set uint param");
                    nativecontrol.SetReferenceProperty(message, (int)param1, ((uint)param2).ToIntPtr());
                }
                else if (param2 is string)
                {
                    Console.WriteLine("ScintillaView set string param");
                    nativecontrol.SetStringProperty(message, (int)param1, (string)param2);
                }
                else if (param2 is NSColor)
                {
                    Console.WriteLine("ScintillaView set NSColor param");
                    nativecontrol.SetColorProperty(message, (int)param1, (NSColor)param2);
                }
            }
            else if (param1 is string)
            {
                Console.WriteLine("ScintillaView set lexer string param");
                nativecontrol.SetLexerProperty((string)param1, (string)param2);
            }
        }

        public void SetKeywords(int level, string keywords)
        {
            SetParameter(Constants.SCI_SETKEYWORDS, level, keywords);
        }

        public void SetStyle(int styleID, int item, object value)
        {
            if (value is Eto.Drawing.Color)
            {
                var color = ((Eto.Drawing.Color)value).ToNSUI();
                SetParameter(styleID, item.ToIntPtr(), color);
            }
            else
            {
                SetParameter(styleID, item, (int)value);
            }
        }

        public void SetFont(string fontname)
        {
            SetParameter(Constants.SCI_STYLESETFONT, Constants.STYLE_DEFAULT, fontname);
        }

        public void SetFontSize(int fontsize)
        {
            SetParameter(Constants.SCI_STYLESETSIZE, Constants.STYLE_DEFAULT, fontsize);
        }

        public void ResetDefaultStyle()
        {
            SetParameter(Constants.SCI_STYLERESETDEFAULT, 0, 0);
        }

        public void ClearAllStyles()
        {
            SetParameter(Constants.SCI_STYLECLEARALL, 0, 0);
        }

        public override NSView ContainerControl
        {
            get
            {
                return Control;
            }
        }

    }

}
