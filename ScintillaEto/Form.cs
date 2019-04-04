using Eto.Forms.Controls.Scintilla.Shared;

namespace Eto.Forms.Controls.Scintilla.Tests
{
    class TestForm: Form
    {

        public TestForm():base()
        {
            Init();            
        }

        void Init()
        {
            
            var skcontrol = new ScintillaControl();

            Title = "Scintilla Demo";

            ClientSize = new Drawing.Size(500, 500);
            
            Content = skcontrol;
        
        }

    }
}
