using BIS.Business;
using BIS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls;

namespace GUI
{
    public partial class frmTranslation : Form
    {
        public string filename  = "";
        private TranslationModel translation;

        public frmTranslation(IModel model)
        {
            InitializeComponent();
            translation = (TranslationModel) model;
            txtTranslation.Text = translation.stringValue;
            txtSentence.Text = translation.stringKey;
            txtSentence.ReadOnly = true;
        }
        public frmTranslation()
        {
            InitializeComponent();
        }

        private void radButtonSave_Click(object sender, EventArgs e)
        {
            if(translation!=null)
            {
                if (updateTranslate() == true)
                {
                    this.DialogResult = DialogResult.Yes;
                    this.Close();
                }
                else
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString("You have not succesfully updated translation for") != null)
                            RadMessageBox.Show( resxSet.GetString("You have not succesfully updated translation for") + " " + translation.stringKey + " !");
                        else
                            RadMessageBox.Show("You have not succesfully updated translation for " + translation.stringKey + " !");
                    }
                }
            }
            else 
            { 
                TranslationBUS tb = new TranslationBUS();
                List<TranslationModel> tm =  new List<TranslationModel>();
                tm = (List<TranslationModel>) tb.CheckIfTranslationExists(Login._user.lngUser,txtSentence.Text);
                if(tm!=null)
                {
                    if (tm.Count > 0)
                    {
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (resxSet.GetString("That sentance already exist in translation!") != null)
                                RadMessageBox.Show(resxSet.GetString("That sentance already exist in translation!"));
                            else
                                RadMessageBox.Show("That sentance already exist in translation!");
                        }
                    }
                    else
                    {
                        if (insertTranslate() == true)
                        {
                            this.DialogResult = DialogResult.Yes;
                            this.Close();
                        }
                        else
                        {
                            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                            {
                                if (resxSet.GetString("You have not succesfully inserted translation!") != null)
                                    RadMessageBox.Show(resxSet.GetString("You have not succesfully inserted translation!"));
                                else
                                    RadMessageBox.Show("You have not succesfully inserted translation!");
                            }
                        }
                    }
                }
                else
                {
                    if (insertTranslate() == true)
                    {
                        this.DialogResult = DialogResult.Yes;
                        this.Close();
                    }
                    else
                    {
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (resxSet.GetString("You have not succesfully inserted translation!") != null)
                                RadMessageBox.Show(resxSet.GetString("You have not succesfully inserted translation!"));
                            else
                                RadMessageBox.Show("You have not succesfully inserted translation!");
                        }
                    }
                }
            }
                     
        }

        private void radButtonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void frmTranslation_Load(object sender, EventArgs e)
        {
            translate();

        }

        private Boolean insertTranslate()
        {
            TranslationBUS tb = new TranslationBUS();
            translation = new TranslationModel();
            translation.stringKey = txtSentence.Text;
            translation.stringValue = txtTranslation.Text;
            translation.dtString = DateTime.Now;
            if (tb.Save(translation, this.Name, Login._user.idUser) == true)
                return true;
            else
                return false;
        }

        private Boolean updateTranslate()
        {
            TranslationBUS tb = new TranslationBUS();
            translation.stringValue = txtTranslation.Text;
            translation.dtString = DateTime.Now;
            if (tb.Update(translation, Login._user.lngUser, this.Name, Login._user.idUser) == true)
                return true;
            else
                return false;
        }

        private void translate()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(radLabelTranlation.Text) != null)
                    radLabelTranlation.Text = resxSet.GetString(radLabelTranlation.Text);
                if (resxSet.GetString(radLabelSentance.Text) != null)
                    radLabelSentance.Text = resxSet.GetString(radLabelSentance.Text);
                if (resxSet.GetString(radButtonSave.Text) != null)
                    radButtonSave.Text = resxSet.GetString(radButtonSave.Text);
                if (resxSet.GetString(radButtonCancel.Text) != null)
                    radButtonCancel.Text = resxSet.GetString(radButtonCancel.Text);
            }
        }
    }
}
