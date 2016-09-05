using System;
using System.Linq;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.Data;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.UI;
using Telerik.WinForms.Documents.Model;
using Telerik.WinControls.UI.Docking;
using Telerik.WinControls.UI.RichTextEditorRibbonUI;
using Telerik.WinControls.RichTextEditor.UI;
using Email;

namespace Email
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}