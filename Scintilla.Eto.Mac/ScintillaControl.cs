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

        private int fontsize = 12;

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

            nativecontrol = new ScintillaView();

            SetStyle();

            var infobar = new InfoBar();
            infobar.Bounds = new CoreGraphics.CGRect(0, 0, 400, 0);
            infobar.SetDisplay(IBDisplay.All);

            nativecontrol.SetInfoBar(infobar, false);

            this.Control = nativecontrol;

        }

        public void SetStyle()
        {

            var forecolor = SystemColors.ControlText.ToNSUI();
            var backcolor = SystemColors.ControlBackground.ToNSUI();

            SetParameter(Constants.SCI_STYLERESETDEFAULT, 0, 0);

            SetParameter(Constants.SCI_STYLESETFONT, Constants.STYLE_DEFAULT, "Menlo");
            SetParameter(Constants.SCI_STYLESETSIZE, Constants.STYLE_DEFAULT, fontsize);

            SetParameter(Constants.SCI_STYLESETBACK, Constants.STYLE_DEFAULT, backcolor);
            SetParameter(Constants.SCI_STYLESETFORE, Constants.STYLE_DEFAULT, forecolor);

            SetParameter(Constants.SCI_STYLECLEARALL, 0, 0);

            SetParameter(Constants.SCI_SETLEXER, Constants.SCLEX_PYTHON, 0);

            SetParameter(Constants.SCI_SETAUTOMATICFOLD, Constants.SC_AUTOMATICFOLD_CLICK, 0);

            SetParameter(Constants.SCI_SETMARGINWIDTHN, 0, 35);

            SetParameter(Constants.SCI_SETMARGINWIDTHN, 1, 0);

            SetParameter(Constants.SCI_SETMARGINWIDTHN, 2, 20);
            SetParameter(Constants.SCI_SETMARGINMASKN, 2, Constants.SC_MASK_FOLDERS);
            SetParameter(Constants.SCI_SETMARGINSENSITIVEN, 2, 1);

            SetParameter(Constants.SCI_SETPROPERTY, "tab.timmy.whinge.level", "1");
            SetParameter(Constants.SCI_SETPROPERTY, "fold", "1");

            SetParameter(Constants.SCI_MARKERDEFINE, Constants.SC_MARKNUM_FOLDER, Constants.SC_MARK_BOXPLUS);
            SetParameter(Constants.SCI_MARKERDEFINE, Constants.SC_MARKNUM_FOLDEROPEN, Constants.SC_MARK_BOXMINUS);
            SetParameter(Constants.SCI_MARKERDEFINE, Constants.SC_MARKNUM_FOLDEROPENMID, Constants.SC_MARK_BOXMINUSCONNECTED);
            SetParameter(Constants.SCI_MARKERDEFINE, Constants.SC_MARKNUM_FOLDEREND, Constants.SC_MARK_BOXPLUSCONNECTED);
            SetParameter(Constants.SCI_MARKERDEFINE, Constants.SC_MARKNUM_FOLDERSUB, Constants.SC_MARK_VLINE);
            SetParameter(Constants.SCI_MARKERDEFINE, Constants.SC_MARKNUM_FOLDERTAIL, Constants.SC_MARK_LCORNERCURVE);
            SetParameter(Constants.SCI_MARKERDEFINE, Constants.SC_MARKNUM_FOLDERMIDTAIL, Constants.SC_MARK_TCORNER);

            SetParameter(Constants.SCI_MARKERSETFORE, Constants.SC_MARKNUM_FOLDER, backcolor);
            SetParameter(Constants.SCI_MARKERSETFORE, Constants.SC_MARKNUM_FOLDEROPEN, backcolor);
            SetParameter(Constants.SCI_MARKERSETFORE, Constants.SC_MARKNUM_FOLDEROPENMID, backcolor);
            SetParameter(Constants.SCI_MARKERSETFORE, Constants.SC_MARKNUM_FOLDEREND, backcolor);
            SetParameter(Constants.SCI_MARKERSETFORE, Constants.SC_MARKNUM_FOLDERSUB, backcolor);
            SetParameter(Constants.SCI_MARKERSETFORE, Constants.SC_MARKNUM_FOLDERTAIL, backcolor);

            SetParameter(Constants.SCI_MARKERSETBACK, Constants.SC_MARKNUM_FOLDER, forecolor);
            SetParameter(Constants.SCI_MARKERSETBACK, Constants.SC_MARKNUM_FOLDEROPEN, forecolor);
            SetParameter(Constants.SCI_MARKERSETBACK, Constants.SC_MARKNUM_FOLDEROPENMID, forecolor);
            SetParameter(Constants.SCI_MARKERSETBACK, Constants.SC_MARKNUM_FOLDEREND, forecolor);
            SetParameter(Constants.SCI_MARKERSETBACK, Constants.SC_MARKNUM_FOLDERSUB, forecolor);
            SetParameter(Constants.SCI_MARKERSETBACK, Constants.SC_MARKNUM_FOLDERTAIL, forecolor);

            SetParameter(Constants.SCI_STYLESETFORE, Constants.SCE_P_COMMENTBLOCK, Colors.DarkGreen.ToNSUI());
            SetParameter(Constants.SCI_STYLESETFORE, Constants.SCE_P_COMMENTLINE, Colors.DarkGreen.ToNSUI());

            SetParameter(Constants.SCI_STYLESETFORE, Constants.SCE_P_STRINGEOL, Color.FromArgb(0, 0, 0).ToNSUI());
            SetParameter(Constants.SCI_STYLESETBACK, Constants.SCE_P_STRINGEOL, Color.FromArgb(224, 192, 224).ToNSUI());

            SetParameter(Constants.SCI_STYLESETEOLFILLED, Constants.SCE_P_STRINGEOL, true);

            SetParameter(Constants.SCI_STYLESETFORE, Constants.SCE_P_TRIPLE, Colors.DarkGreen.ToNSUI());
            SetParameter(Constants.SCI_STYLESETFORE, Constants.SCE_P_TRIPLEDOUBLE, Colors.DarkGreen.ToNSUI());

            SetParameter(Constants.SCI_STYLESETFORE, Constants.SCE_P_NUMBER, Colors.DarkOrange.ToNSUI());
            SetParameter(Constants.SCI_STYLESETITALIC, Constants.SCE_P_COMMENTLINE, true);

            SetParameter(Constants.SCI_STYLESETFORE, Constants.SCE_P_STRING, Colors.DeepPink.ToNSUI());
            SetParameter(Constants.SCI_STYLESETFORE, Constants.SCE_P_CHARACTER, Colors.DeepPink.ToNSUI());

            SetParameter(Constants.SCI_STYLESETFORE, Constants.SCE_P_WORD, Colors.SteelBlue.ToNSUI());
            SetParameter(Constants.SCI_STYLESETFORE, Constants.SCE_P_WORD2, Colors.Salmon.ToNSUI());

            SetParameter(Constants.SCI_STYLESETFORE, Constants.SCE_P_CLASSNAME, NSColor.Brown);
            SetParameter(Constants.SCI_STYLESETFORE, Constants.SCE_P_DEFNAME, Colors.Chocolate.ToNSUI());

            SetParameter(Constants.SCI_STYLESETITALIC, Constants.SCE_P_COMMENTBLOCK, true);
            SetParameter(Constants.SCI_STYLESETITALIC, Constants.SCE_P_COMMENTLINE, true);

            SetParameter(Constants.SCI_STYLESETBOLD, Constants.SCE_P_CLASSNAME, true);
            SetParameter(Constants.SCI_STYLESETBOLD, Constants.SCE_P_DEFNAME, true);
            SetParameter(Constants.SCI_STYLESETBOLD, Constants.SCE_P_OPERATOR, true);
            SetParameter(Constants.SCI_STYLESETBOLD, Constants.SCE_P_WORD2, true);

            SetParameter(Constants.SCI_STYLESETBACK, Constants.STYLE_LINENUMBER, backcolor);
            SetParameter(Constants.SCI_STYLESETFORE, Constants.STYLE_LINENUMBER, forecolor);

            SetParameter(Constants.SCI_SETFOLDMARGINCOLOUR, "1", backcolor);
            SetParameter(Constants.SCI_SETFOLDMARGINHICOLOUR, "1", backcolor);

            string python2 = "and as assert break class continue def del elif else except exec finally for from global if import in" +
            " is lambda not or pass print raise return try while with yield";
            string python3 = "False None True and as assert break class continue def del elif else except finally for from global i" +
            "f import in is lambda nonlocal not or pass raise return try while with yield";

            SetParameter(Constants.SCI_SETKEYWORDS, 0, (python2 + " " + python3));

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
                else if (param2 is bool)
                {
                    Console.WriteLine("ScintillaView set bool param");
                    nativecontrol.SetGeneralProperty(message, (int)param1, (bool)param2 == true ? 1 : 0);
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
                if (param2 is string)
                {
                    Console.WriteLine("ScintillaView set lexer string param");
                    nativecontrol.SetLexerProperty((string)param1, (string)param2);
                }
                else if (param2 is NSColor)
                {
                    Console.WriteLine("ScintillaView set NSColor param");
                    nativecontrol.SetColorProperty(message, int.Parse((string)param1), (NSColor)param2);
                }
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

        public override bool Enabled { get => nativecontrol.IsEditable(); set => nativecontrol.SetEditable(value); }


        public void Cut()
        {
            SetParameter(Constants.SCI_CUT, 0, 0);
        }

        public void Copy()
        {
            SetParameter(Constants.SCI_COPY, 0, 0);
        }

        public void Paste()
        {
            SetParameter(Constants.SCI_PASTE, 0, 0);
        }

        public void Undo()
        {
            SetParameter(Constants.SCI_UNDO, 0, 0);
        }

        public void Redo()
        {
            SetParameter(Constants.SCI_REDO, 0, 0);
        }

        public void ToggleCommenting()
        {
            var lines = nativecontrol.SelectedString().Split(System.Environment.NewLine.ToCharArray());
            string newlines = "";
            foreach (string l in lines)
            {
                if (l != "")
                {
                    if (l.StartsWith("#")) { newlines += l.TrimStart('#') + System.Environment.NewLine; }
                    else { newlines += l.Insert(0, "#") + System.Environment.NewLine; }
                }
            }
            SetParameter(Constants.SCI_REPLACESEL, 0, newlines.TrimEnd(System.Environment.NewLine.ToCharArray()));
        }

        public void Indent()
        {
            var lines = nativecontrol.SelectedString().Split(System.Environment.NewLine.ToCharArray());
            string newlines = "";
            foreach (string l in lines)
            {
                if (l != "") newlines += l.Insert(0, '\t'.ToString()) + System.Environment.NewLine;
            }
            SetParameter(Constants.SCI_REPLACESEL, 0, newlines.TrimEnd(System.Environment.NewLine.ToCharArray()));
        }

        public void Unindent()
        {
            var lines = nativecontrol.SelectedString().Split(System.Environment.NewLine.ToCharArray());
            string newlines = "";
            foreach (string l in lines)
            {
                if (l != "") newlines += l.TrimStart('\t') + System.Environment.NewLine;
            }
            SetParameter(Constants.SCI_REPLACESEL, 0, newlines.TrimEnd(System.Environment.NewLine.ToCharArray()));
        }

        public void InsertSnippet(string snippet)
        {

            nativecontrol.InsertText(Foundation.NSObject.FromObject(snippet));

        }

        public void Print()
        {
            nativecontrol.Print(nativecontrol);
        }

        public void IncreaseFontSize()
        {
            fontsize += 1;
            SetStyle();
        }

        public void DecreaseFontSize()
        {
            fontsize -= 1;
            SetStyle();
        }

    }

}
