using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class SaveDialogLayouts : Form
    {
        public string filename  = "";
        public SaveDialogLayouts()
        {
            InitializeComponent();
        }

        private void SaveDialogLayouts_Load(object sender, EventArgs e)
        {

        }
        private static string GetValidFileName(string fileName) 
        {
            // remove any invalid character from the filename.
            String ret = Regex.Replace(fileName.Trim(), "[^A-Za-z0-9_. ]+", "");
            return ret.Replace(" ", String.Empty);
        }
        private void radButtonSave_Click(object sender, EventArgs e)
        {

            filename = txtFilename.Text;
            bool dot = txtFilename.Text.Contains('.');                        
            if (dot == true) filename = txtFilename.Text.Replace(".", "");
            filename = GetValidFileName(filename);

            if (filename.Trim() == "")
            {
                radLabelFilename.ForeColor = Color.DarkRed;
                return;
            }

            filename += ".xml";


            if (File.Exists(MainForm.gridCustomFilter + "\\" + filename) == true)
            {
                translateRadMessageBox m = new translateRadMessageBox();
                m.translateAllMessageBox("That custom filter already exists!");
                //radLabelFilename.Text = "Enter filename: (File with name: " + filename + " already exist)";
                //radLabelFilename.ForeColor = Color.DarkRed;
                return;
            }

            this.DialogResult = DialogResult.Yes;

            
            this.Close();
                                    
        }

        private void radButtonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }
    }
}
