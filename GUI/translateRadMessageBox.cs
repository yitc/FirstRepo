using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls;

namespace GUI
{
    public class translateRadMessageBox
    {

        public  translateRadMessageBox ()
        {
        }

        public void translateAllMessageBox(string text)
        {
            RadMessageBox.SetThemeName("Windows8");
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(text) != null)
                    RadMessageBox.Show(resxSet.GetString(text));
                else
                    RadMessageBox.Show(text);
            }
        }

        public void translatePartAndNonTranslatedPart(string text, string nontranslate)
        {
            RadMessageBox.SetThemeName("Windows8");
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(text) != null)
                    RadMessageBox.Show(resxSet.GetString(text) + " " +nontranslate + ".");
                else
                    RadMessageBox.Show(text + " " + nontranslate + ".");
            }
        }

        public void translatePartAndNonTranslatedPartInMiddleText(string textBefore, string nontranslate, string textAfter)
        {
            RadMessageBox.SetThemeName("Windows8");
            string txtBefore = textBefore;
            string txtAfter = textAfter;

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(txtBefore) != null)
                    txtBefore = resxSet.GetString(txtBefore);

                if (resxSet.GetString(textAfter) != null)
                    textAfter = resxSet.GetString(textAfter);
            }

            RadMessageBox.Show(txtBefore + " " + nontranslate + " " + textAfter);
        }


        public DialogResult translateAllMessageBoxDialog(string text, string title)
        {
            RadMessageBox.SetThemeName("Windows8");
            DialogResult dialogStandardFilter = DialogResult.Cancel;
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(text) != null)
                {

                    if (resxSet.GetString(title) != null)
                        dialogStandardFilter = RadMessageBox.Show(resxSet.GetString(text), resxSet.GetString(title), MessageBoxButtons.YesNoCancel);
                        
                    else
                        dialogStandardFilter = RadMessageBox.Show(resxSet.GetString(text), title, MessageBoxButtons.YesNoCancel);
                }
                else
                {
                    if (resxSet.GetString(title) != null)
                        dialogStandardFilter = RadMessageBox.Show(text, resxSet.GetString(title), MessageBoxButtons.YesNoCancel);
                    else
                        dialogStandardFilter = RadMessageBox.Show(text, title, MessageBoxButtons.YesNoCancel);
                }
            }

            return dialogStandardFilter;
        }

        public DialogResult translateAllMessageBoxDialogYesNo(string text, string title)
        {
            RadMessageBox.SetThemeName("Windows8");
            DialogResult dialogStandardFilter = DialogResult.No;
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(text) != null)
                {

                    if (resxSet.GetString(title) != null)
                        dialogStandardFilter = RadMessageBox.Show(resxSet.GetString(text), resxSet.GetString(title), MessageBoxButtons.YesNo);

                    else
                        dialogStandardFilter = RadMessageBox.Show(resxSet.GetString(text), title, MessageBoxButtons.YesNo);
                }
                else
                {
                    if (resxSet.GetString(title) != null)
                        dialogStandardFilter = RadMessageBox.Show(text, resxSet.GetString(title), MessageBoxButtons.YesNo);
                    else
                        dialogStandardFilter = RadMessageBox.Show(text, title, MessageBoxButtons.YesNo);
                }
            }

            return dialogStandardFilter;
        }

    }
}
