using System;
using Eto.Forms;

namespace Eto.Forms.Controls.Scintilla.Shared
{

    [Eto.Handler(typeof(IScintillaControl))]
    public class ScintillaControl : Eto.Forms.Control
    {
        new IScintillaControl Handler { get { return (IScintillaControl)base.Handler; } }

        public interface IScintillaControl : Eto.Forms.Control.IHandler
        {
            string ScriptText { get; set; }

            void SetKeywords(int level, string keywords);

            void SetStyle(int styleID, int item, object value);

            void SetFont(string fontname);

            void SetFontSize(int fontsize);

            void ResetDefaultStyle();

            void ClearAllStyles();

        }

        public string ScriptText
        {
            get
            {
                return Handler.ScriptText;
            }
            set
            {
                Handler.ScriptText = value;
            }
        }

        public void SetKeywords(int level, string keywords)
        {
            Handler.SetKeywords(level, keywords);
        }

        public void SetStyle(int styleID, int item, object value)
        {
            Handler.SetStyle(styleID, item, value);
        }

        public void SetFont(string fontname)
        {
            Handler.SetFont(fontname);
        }

        public void SetFontSize(int fontsize)
        {
            Handler.SetFontSize(fontsize);
        }

        public void ResetDefaultStyle()
        {
            Handler.ResetDefaultStyle();
        }

        public void ClearAllStyles()
        {
            Handler.ClearAllStyles();
        }

    }
}


