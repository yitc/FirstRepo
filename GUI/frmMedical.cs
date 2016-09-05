using BIS.Business;
using BIS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace GUI
{
    public partial class frmMedical : frmTemplate
    {

        List<QuestGroupModel> medqgm;
        List<QuestModel> medqm;
        List<AnswerModel> medam;
        List<AnswerTypeModel> medatm;
        int idGroup = -1;
        bool isOneAns = false;
        bool isOneQuest = false;
        bool isOneInGroup = false;
        bool isLoad = true;

        private string layoutMedicalAnswer = MainForm.gridFiltersFolder + "\\layoutMedicalAnswer.xml";

          
        int idCompany;

        public frmMedical()
        {
            InitializeComponent();
        }

        public frmMedical(IModel model)
        {
            InitializeComponent();

        }

        private void frmMedical_Load(object sender, EventArgs e)
        {
           
            radRibbonMemo.Visibility = ElementVisibility.Collapsed;
            radRibbonDocuments.Visibility = ElementVisibility.Collapsed;
            
            btnWord.Visibility = ElementVisibility.Collapsed;
            btnEmail.Visibility = ElementVisibility.Collapsed;
            btnReport.Visibility = ElementVisibility.Collapsed;
            radRibbonContact.Visibility = ElementVisibility.Collapsed;
            radRibbonTask.Visibility = ElementVisibility.Collapsed;
            radRibbonMeeting.Visibility = ElementVisibility.Collapsed;

            this.radGridAnswer.CellEditorInitialized += radGridAnswer_CellEditorInitialized;
            this.radGridQuestion.CellEditorInitialized += radGridQuestion_CellEditorInitialized;


            idCompany = Login._companyModelList[0].idCompany;
            MedicalVoluntaryBUS mvb = new MedicalVoluntaryBUS();

            setQuestionGroup();
            setQuestion();
            setAnswer();
            translation();
         
            GridViewComboBoxColumn ddl = new GridViewComboBoxColumn();
            ddl.DataSource = mvb.GetAnswerType();
            ddl.DisplayMember = "nameAnsType";
            ddl.ValueMember = "idAnsType";
            ddl.FieldName = "idAnsType";
            ddl.Name = "Type";
            ddl.HeaderText = "Type";
            radGridAnswer.Columns.Add(ddl);

            GridViewComboBoxColumn ddl1 = new GridViewComboBoxColumn();
            ddl1.DataSource = mvb.GetSkills(MainForm.idLabelList);
            ddl1.DisplayMember = "txtQuest";
            ddl1.ValueMember = "idQuestSkills";
            ddl1.FieldName = "idQuestSkills";
            ddl1.Name = "Skills" ;
            ddl1.HeaderText = "Skills";
            radGridAnswer.Columns.Add(ddl1);
          
            //radGridAnswer.Columns["Skills"].HeaderText = (radGridAnswer.Columns["Skills"].HeaderText);


            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(radGridAnswer.Columns["Type"].HeaderText) != null)
                    radGridAnswer.Columns["Type"].HeaderText = resxSet.GetString(radGridAnswer.Columns["Type"].HeaderText);

                if (resxSet.GetString(radGridAnswer.Columns["Skills"].HeaderText) != null)
                    radGridAnswer.Columns["Skills"].HeaderText = resxSet.GetString(radGridAnswer.Columns["Skills"].HeaderText);
            }

            radGridAnswer.Columns["Type"].Width = (int)(this.CreateGraphics().MeasureString(radGridAnswer.Columns["Type"].HeaderText, this.Font).Width + 38);
            radGridAnswer.Columns["Skills"].Width = (int)(this.CreateGraphics().MeasureString(radGridAnswer.Columns["Skills"].HeaderText, this.Font).Width + 60);
           
            if (File.Exists(layoutMedicalAnswer))
            {
                radGridAnswer.LoadLayout(layoutMedicalAnswer);
            }             

            isLoad = false;

        }

        private void translation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(QuestionGroup.Text) != null)
                    QuestionGroup.Text = resxSet.GetString(QuestionGroup.Text);
                if (resxSet.GetString(Question.Text) != null)
                    Question.Text = resxSet.GetString(Question.Text);
                if (resxSet.GetString(this.Text) != null)
                    this.Text = resxSet.GetString(this.Text);
                if (resxSet.GetString(radMenuButtonSaveLayout.Text) != null)
                    radMenuButtonSaveLayout.Text = resxSet.GetString(radMenuButtonSaveLayout.Text);

            }
        }

        private void setQuestionGroup()
        {
            MedicalVoluntaryBUS mvb = new MedicalVoluntaryBUS();
            medqgm = mvb.GetQuestionGroupMedical(idCompany);
            radGridQuestionGroup.DataSource = medqgm;
            radGridQuestionGroup.Show();

            
        }

        private void setQuestion()
        {
            MedicalVoluntaryBUS mvb = new MedicalVoluntaryBUS();
            medqm = new List<QuestModel>();
            if (radGridQuestionGroup.SelectedRows.Count > 0)
            {
                medqm = mvb.GetQuestionMedical(idCompany, Convert.ToInt32(radGridQuestionGroup.SelectedRows[0].Cells["idQuestGroup"].Value.ToString()));
            }
            radGridQuestion.DataSource = medqm;
            radGridQuestion.Show();
        }

        private void radGridQuestionGroup_SelectionChanged(object sender, EventArgs e)
        {

            if (radGridQuestionGroup.SelectedRows.Count > 0)
             {
                 GridViewDataRowInfo row = radGridQuestionGroup.SelectedRows[0] as GridViewDataRowInfo;
                 if (row.Cells["idQuestGroup"] != null)
                 {
                     idGroup = Convert.ToInt32(row.Cells["idQuestGroup"].Value.ToString());
                     setQuestion();
                     setAnswer();
                 }
             }
        }


        private void radGridQuestion_SelectionChanged(object sender, EventArgs e)
        {
            if (radGridQuestion.SelectedRows.Count > 0)
            {
                GridViewDataRowInfo row = radGridQuestion.SelectedRows[0] as GridViewDataRowInfo;
                if (row.Cells["idQuest"] != null)
                {
                        setAnswer();
                    
                }
            }
        }

        private void setAnswer()
        {

            MedicalVoluntaryBUS mvb = new MedicalVoluntaryBUS();
            medam = new List<AnswerModel>();
            if (radGridQuestion.SelectedRows.Count > 0)
            {
                medam = mvb.GetAnswerMedical(Convert.ToInt32(radGridQuestion.SelectedRows[0].Cells["idQuest"].Value.ToString()), MainForm.idLabelList);
               
            }
            radGridAnswer.DataSource = medam;
            radGridAnswer.Show();
        }

        private void radGridQuestionGroup_RowsChanged(object sender, GridViewCollectionChangedEventArgs e)
        {
            MedicalVoluntaryBUS mvb = new MedicalVoluntaryBUS();
            if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.ItemChanged)
            {
                GridViewDataRowInfo newRow = e.NewItems[0] as GridViewDataRowInfo;
                QuestGroupModel qg = new QuestGroupModel();
                qg.nameQuestGroup = newRow.Cells["nameQuestGroup"].Value.ToString();
                qg.idCompany = Login._companyModelList[0].idCompany;
                qg.idQuestGroup = Convert.ToInt32(newRow.Cells["idQuestGroup"].Value.ToString());
                if (mvb.UpdateMedicalQuestGroup(qg, this.Name, Login._user.idUser) != true)
                {
                    this.radGridQuestionGroup.EndEdit();
                    this.radGridQuestionGroup.CancelEdit();
                }
                else
                {
                    setQuestion();
                    setAnswer();
                }
            }
            if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Add)
            {
                setQuestionGroup();
                setQuestion();
                setAnswer();
            }
            if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Remove)
            {
                setQuestionGroup();
            }
        }
        private void radGridQuestionGroup_RowsChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (sender.GetType() == typeof(MasterGridViewTemplate))
            {
                MedicalVoluntaryBUS mvb = new MedicalVoluntaryBUS();
                MasterGridViewTemplate mgvt = (MasterGridViewTemplate)sender;
                QuestGroupModel qg = new QuestGroupModel();
                qg.nameQuestGroup = mgvt.CurrentRow.Cells["nameQuestGroup"].Value.ToString();
                //     idGroup = Convert.ToInt32(mgvt.CurrentRow.Cells["idQuestGroup"].Value.ToString());
                qg.idCompany = Login._companyModelList[0].idCompany;
                if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Add)
                {

                    if (mvb.SaveMedicalQuestGroup(qg, this.Name, Login._user.idUser) != true)
                    {
                        e.Cancel = true;
                        translateRadMessageBox trs = new translateRadMessageBox();
                        trs.translateAllMessageBox("You haven't successfully add question group");
                    }
                }
                else if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Remove)
                {
                    string messageGroup = "";
                    if (mgvt.CurrentRow.Cells["idQuestGroup"].Value != null)
                    {
                        messageGroup = mgvt.CurrentRow.Cells["nameQuestGroup"].Value.ToString();
                        qg.idQuestGroup = Convert.ToInt32(mgvt.CurrentRow.Cells["idQuestGroup"].Value.ToString());
                        if (mvb.GetQuestionMedical(qg.idCompany, qg.idQuestGroup).Count == 0)
                        {
                            if (mvb.DeleteMedicalQuestGroup(qg, this.Name, Login._user.idUser) != true)
                            {
                                e.Cancel = true;
                                translateRadMessageBox trs = new translateRadMessageBox();
                                trs.translateAllMessageBox("You haven't successfully delete question group");
                            }
                            else
                            {
                                translateRadMessageBox trs = new translateRadMessageBox();
                                trs.translateAllMessageBox("You have delete successfully: " + messageGroup + " group");

                            }
                            setQuestion();
                            setAnswer();
                        }
                        else
                        {
                            e.Cancel = true;
                            translateRadMessageBox trs = new translateRadMessageBox();
                            trs.translateAllMessageBox("First you have to delete questions");
                        }
                    }
                    else
                    {
                        translateRadMessageBox trs = new translateRadMessageBox();
                        trs.translateAllMessageBox("You haven't successfully delete question group because there is no id");
                    }
                }
                else
                {
                    e.Cancel = true;
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("You haven't successfully change question group");
                }
            }
        }

        private void radGridQuestion_RowsChanged(object sender, GridViewCollectionChangedEventArgs e)
        {
            
                MedicalVoluntaryBUS mvb = new MedicalVoluntaryBUS();
                if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.ItemChanged)
                {
                    GridViewDataRowInfo newRow = e.NewItems[0] as GridViewDataRowInfo;
                    QuestModel qg = new QuestModel();
                    qg.txtQuest = newRow.Cells["txtQuest"].Value.ToString();
                   //original  qg.questSort = Convert.ToInt32(newRow.Cells["questSort"].Value.ToString());
                    if (newRow.Cells["questSort"].Value != null)
                        qg.questSort = Convert.ToDecimal(newRow.Cells["questSort"].Value.ToString());
                    else
                        qg.questSort = 0;
                    qg.idQuest = Convert.ToInt32(newRow.Cells["idQuest"].Value.ToString());
                    if (mvb.UpdateMedicalQuest(qg, this.Name, Login._user.idUser) != true)
                    {
                        this.radGridQuestion.EndEdit();
                        this.radGridQuestion.CancelEdit();
                    }
                    else
                    {
                        setAnswer();
                    }
                }
                if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Add)
                {
                    setQuestion();
                    setAnswer();
                }
                if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Remove)
                {
                    if (isOneInGroup == true)
                    {
                        setQuestionGroup();
                        radPageQuestion.SelectedPage = QuestionGroup;
                      
                    }
                    if (isOneInGroup == false)

                        setQuestion();
                }
           

        }
        private void radGridQuestion_RowsChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (sender.GetType() == typeof(MasterGridViewTemplate))
            {
                MedicalVoluntaryBUS mvb = new MedicalVoluntaryBUS();
                MasterGridViewTemplate mgvt = (MasterGridViewTemplate)sender;
                QuestModel qg = new QuestModel();
                if (mgvt.CurrentRow.Cells["txtQuest"].Value != null)
                    qg.txtQuest = mgvt.CurrentRow.Cells["txtQuest"].Value.ToString();
                if (mgvt.CurrentRow.Cells["questSort"].Value != null)
                    //qg.questSort = Convert.ToInt32(mgvt.CurrentRow.Cells["questSort"].Value.ToString());
                    qg.questSort = Convert.ToDecimal(mgvt.CurrentRow.Cells["questSort"].Value.ToString());
                else
                    qg.questSort = 0;
                if (radGridQuestionGroup.SelectedRows.Count > 0)
                    qg.idQuestGroup = Convert.ToInt32(radGridQuestionGroup.SelectedRows[0].Cells["idQuestGroup"].Value.ToString());
                if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Add)
                {

                    if (mvb.SaveMedicalQuest(qg, this.Name, Login._user.idUser) != true)
                    {
                        e.Cancel = true;
                        translateRadMessageBox trs = new translateRadMessageBox();
                        trs.translateAllMessageBox("You haven't successfully add question");
                    }
                }
                else if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Remove)
                {
                    string messageQuest = "";
                    string messageGroup = "";

                    QuestGroupModel qgroup = new QuestGroupModel();
                    if (mgvt.CurrentRow.Cells["idQuest"].Value != null)
                    {

                        qg.idQuest = Convert.ToInt32(mgvt.CurrentRow.Cells["idQuest"].Value.ToString());
                        qg.txtQuest = mgvt.CurrentRow.Cells["txtQuest"].Value.ToString();
                        qg.idQuestGroup = Convert.ToInt32(mgvt.CurrentRow.Cells["idQuestGroup"].Value.ToString());
                        if (radGridQuestionGroup.SelectedRows.Count > 0)
                            qgroup.nameQuestGroup = (radGridQuestionGroup.SelectedRows[0].Cells["nameQuestGroup"].Value.ToString());

                        messageQuest += qg.txtQuest;

                        if (mvb.checkIsOneQuestInGroup(qg) <= 1)
                        {
                            isOneInGroup = true;
                            messageGroup += " and group " + qgroup.nameQuestGroup;


                        }
                        else
                        {
                            isOneInGroup = false;
                        }

                        if (mvb.GetAnswerMedical(qg.idQuest, MainForm.idLabelList).Count == 0)
                        {
                            if (qg.idQuest != 169)
                            {
                                if (qg.idQuest != 219)
                                {
                                    if (qg.idQuest != 177)
                                    {
                                        if (qg.idQuest != 168)
                                        {
                                            if (qg.idQuest != 178)
                                            {
                                                if (qg.idQuest != 179)
                                                {
                                                    if (mvb.DeleteQuestSript(qg, isOneInGroup, this.Name, Login._user.idUser) != true)
                                                    {
                                                        e.Cancel = true;
                                                        translateRadMessageBox trs = new translateRadMessageBox();
                                                        trs.translateAllMessageBox("You haven't successfully delete question");
                                                    }
                                                    else
                                                    {
                                                        translateRadMessageBox trs = new translateRadMessageBox();
                                                        trs.translateAllMessageBox("You have delete successfully: " + messageQuest + messageGroup);
                                                    }
                                                }
                                                else
                                                {
                                                    translateRadMessageBox trs = new translateRadMessageBox();
                                                    trs.translateAllMessageBox("You can't delete this question, because this is quest for medication.");
                                                }
                                            }
                                            else
                                            {
                                                translateRadMessageBox trs = new translateRadMessageBox();
                                                trs.translateAllMessageBox("You can't delete this question, because this is quest for epilepsie.");
                                            }
                                        }
                                        else
                                        {
                                            translateRadMessageBox trs = new translateRadMessageBox();
                                            trs.translateAllMessageBox("You can't delete this question, because this is quest for devices.");
                                        }
                                    }
                                    else
                                    {
                                        translateRadMessageBox trs = new translateRadMessageBox();
                                        trs.translateAllMessageBox("You can't delete this question, because this is quest for diet.");
                                    }
                                }
                                else
                                {
                                    translateRadMessageBox trs = new translateRadMessageBox();
                                    trs.translateAllMessageBox("You can't delete this question, because this is quest for rented device.");
                                }
                            }
                            else
                            {
                                translateRadMessageBox trs = new translateRadMessageBox();
                                trs.translateAllMessageBox("You can't delete this question, because this is quest for airport code.");
                            }
                        }
                        else
                        {
                            e.Cancel = true;
                            translateRadMessageBox trs = new translateRadMessageBox();
                            trs.translateAllMessageBox("First you have to delete answers");
                        }
                    }
                    else
                    {
                        translateRadMessageBox trs = new translateRadMessageBox();
                        trs.translateAllMessageBox("You haven't successfully delete question because there is no id");
                    }
                }
                else
                {
                    e.Cancel = true;
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("You haven't successfully change question");
                }
            }
        }

        
        private void radGridAnswer_RowsChanged(object sender, GridViewCollectionChangedEventArgs e)
        {
            MedicalVoluntaryBUS mvb = new MedicalVoluntaryBUS();
            if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.ItemChanged)
            {
                GridViewDataRowInfo newRow = e.NewItems[0] as GridViewDataRowInfo;
                AnswerModel am = new AnswerModel();
                if (newRow.Cells["txtAns"].Value != null)
                    am.txtAns = newRow.Cells["txtAns"].Value.ToString();
                if (newRow.Cells["ansSort"].Value != null)
                    am.ansSort = Convert.ToInt32(newRow.Cells["ansSort"].Value.ToString());
                else
                    am.ansSort = 0;
                am.idAns = Convert.ToInt32(newRow.Cells["idAns"].Value.ToString());
                if (newRow.Cells["idQuestSkills"].Value != null)
                    am.idQuestSkills = Convert.ToInt32(newRow.Cells["idQuestSkills"].Value.ToString());
              

                if (newRow.Cells["idAnsType"].Value != null)
                {
                    am.idAnsType = Convert.ToInt32(newRow.Cells["idAnsType"].Value.ToString());
                    if (mvb.UpdateMedicalAnswer(am, this.Name, Login._user.idUser) != true)
                    {
                        this.radGridAnswer.EndEdit();
                        this.radGridAnswer.CancelEdit();
                    }
                    else
                    {
                        setAnswer();
                    }
                }
                else
                {
                    RadMessageBox.Show("You haven to feel type");
                }
            }
            if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Add)
            {
                  setAnswer();
            }
            if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Remove)
            {
                if (isOneAns == true && isOneQuest == false)
                {

                    setQuestion();
                }
                if (isOneAns == true && isOneQuest == true)
                {
                    setQuestionGroup();
                    radPageQuestion.SelectedPage = QuestionGroup;
                }

            }
        }

        private void radGridAnswer_RowsChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (sender.GetType() == typeof(MasterGridViewTemplate))
            if (radGridQuestion.SelectedRows.Count > 0)
            {
                MedicalVoluntaryBUS mvb = new MedicalVoluntaryBUS();
                MasterGridViewTemplate mgvt = (MasterGridViewTemplate) radGridAnswer.MasterTemplate;
                AnswerModel am = new AnswerModel();
                if (mgvt.CurrentRow.Cells["txtAns"].Value != null)
                    am.txtAns = mgvt.CurrentRow.Cells["txtAns"].Value.ToString();

                if (mgvt.CurrentRow.Cells["idQuestSkills"].Value != null)                 
                    am.idQuestSkills = Convert.ToInt32(mgvt.CurrentRow.Cells["idQuestSkills"].Value.ToString());
              
                //string name=mgvt.CurrentRow.Cells["Skills"].Value.ToString();
                
                
                if (mgvt.CurrentRow.Cells["ansSort"].Value != null)
                    am.ansSort = Convert.ToInt32(mgvt.CurrentRow.Cells["ansSort"].Value.ToString());
                else
                    am.ansSort = 0;
                if (mgvt.CurrentRow.Cells["idAnsType"].Value != null)
                {
                    am.idAnsType = Convert.ToInt32(mgvt.CurrentRow.Cells["idAnsType"].Value.ToString());
                    if (radGridQuestion.SelectedRows.Count > 0)
                        am.idQuest = Convert.ToInt32(radGridQuestion.SelectedRows[0].Cells["idQuest"].Value.ToString());
                    //if(am.)
                    
                    if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Add)
                    {
                        
                        if (MainForm.idLabelList.Count > 0)
                        {
                            for (int i = 0; i < MainForm.idLabelList.Count; i++)
                            {
                                am.idQueryType = MainForm.idLabelList[i];
                                am.idAns = mvb.GetLastAnswerId();
                                if (mvb.SaveMedicalAnswer(am, this.Name, Login._user.idUser) != true)
                                {
                                    e.Cancel = true;
                                    translateRadMessageBox trs = new translateRadMessageBox();
                                    trs.translateAllMessageBox("You haven't successfully add answer for " + Login._medicalLabels.Find(item => item.idLabel == MainForm.idLabelList[i]).nameLabel);
                                }
                                else
                                    mgvt.CurrentRow.Cells["nameLabel"].Value = Login._medicalLabels.Find(item => item.idLabel == MainForm.idLabelList[i]).nameLabel;
                            }
                        }
                        else
                        {
                            for (int i = 0; i < Login._medicalLabels.Count; i++)
                            {
                                am.idQueryType = Login._medicalLabels[i].idLabel;
                                am.idAns = mvb.GetLastAnswerId();
                                if (mvb.SaveMedicalAnswer(am, this.Name, Login._user.idUser) != true)
                                {
                                    translateRadMessageBox trs = new translateRadMessageBox();
                                    trs.translateAllMessageBox("You haven't successfully add answer for " + Login._medicalLabels[i].nameLabel);
                                    e.Cancel = true;
                                }
                                else
                                    mgvt.CurrentRow.Cells["nameLabel"].Value = Login._medicalLabels[i].nameLabel;
                            }
                        }
                    }
                    else if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Remove)
                    {
                        string messageAnswer = "";
                        string messageQuest = "";
                        string messageGroup = "";
                        //bool isOneAns = false;
                        //bool isOneQuest = false;
                        QuestModel qg = new QuestModel();
                        QuestGroupModel qgroup = new QuestGroupModel();
                       
                        if (mgvt.CurrentRow.Cells["idAns"].Value != null)
                        {
                            if (mgvt.CurrentRow.Cells["txtAns"].Value != null)
                                am.txtAns = mgvt.CurrentRow.Cells["txtAns"].Value.ToString();
                            am.idAns = Convert.ToInt32(mgvt.CurrentRow.Cells["idAns"].Value.ToString());
                            am.idQuest = Convert.ToInt32(mgvt.CurrentRow.Cells["idQuest"].Value.ToString());
                           // am.nameQuest = Convert.ToInt32(mgvt.CurrentRow.Cells["idQuest"].Value.ToString());
                            am.idQueryType = Convert.ToInt32(mgvt.CurrentRow.Cells["idQueryType"].Value.ToString());
                            //    int index = Convert.ToInt32(radGridQuestion.CurrentRow.Cells["idQueryType"].Value.ToString());
                            //am.idQ= Convert.ToInt32(mgvt.CurrentRow.Cells["idQueryType"].Value.ToString());
                            if (radGridQuestion.SelectedRows.Count > 0)
                                qg.txtQuest = (radGridQuestion.SelectedRows[0].Cells["txtQuest"].Value.ToString());

                            if (radGridQuestionGroup.SelectedRows.Count > 0)
                                qgroup.nameQuestGroup = (radGridQuestionGroup.SelectedRows[0].Cells["nameQuestGroup"].Value.ToString());


                            messageAnswer += am.txtAns;

                            if (mvb.checkIsOneAns(am) <= 1)
                            {
                                isOneAns = true;
                                messageQuest += "with question " + qg.txtQuest;

                            }
                            else
                            {
                                isOneAns = false;
                            }

                            if (mvb.checkIsOneQuest(idGroup) <= 1)
                            {
                                isOneQuest = true;
                              
                            }
                            else
                            {
                                isOneQuest = false;
                            }

                            if (isOneAns == true && isOneQuest== true)
                            {
                              messageGroup += "and " + qg.txtQuest + " group";
                            }

                            if (am.idAns != 151)
                            {
                                if (am.idAns != 152 || am.idAns != 323)
                                {
                                    if (am.idAns != 153)
                                    {
                                        if (am.idAns != 324)
                                        {
                                            if (am.idAns != 514 || am.idAns != 515 || am.idAns != 516)
                                            {
                                                if (isInListIdQuest(am.idAns, 169)==false)
                                                {
                                                     if (isInListIdQuest(am.idAns, 219)==false)                                            
                                                     {
                                                         if (isInListIdQuest(am.idAns, 168)==false)                                                                                                
                                                         {
                                                              if (isInListIdQuest(am.idAns, 177)==false)                                                                                                                                                        
                                                              {
                                                                  if (isInListIdQuest(am.idAns, 178)==false)                                                                                                                                                                                                                     
                                                                  {
                                                                      if (isInListIdQuest(am.idAns, 179) == false)
                                                                      {                                                
                                                                          if (mvb.checkAnsIsInMedCpr(am.idAns, Convert.ToInt32(mgvt.CurrentRow.Cells["idQueryType"].Value.ToString())) == 0)
                                                                          {
                                                                              if (mvb.DeleteAnsSript(am, isOneAns, isOneQuest, idGroup, this.Name, Login._user.idUser) != true)                                                     
                                                                              {
                                                                                  e.Cancel = true;
                                                                                  translateRadMessageBox trs = new translateRadMessageBox();
                                                                                  trs.translateAllMessageBox("You haven't successfully delete answer");
                                                                              }
                                                                              else
                                                                              {
                                                                                  translateRadMessageBox trs = new translateRadMessageBox();
                                                                                  trs.translateAllMessageBox("You have delete successfully: " + messageAnswer + " " + messageQuest + " " + messageGroup);
                                                                              }                                                    
                                                                          }                                                  
                                                                          else                                                  
                                                                          {                                                      
                                                                              e.Cancel = true;
                                                                              translateRadMessageBox trs = new translateRadMessageBox();
                                                                              trs.translateAllMessageBox("You can't delete answer, because some person already filled this answer");                                                    
                                                                          }
                                                                      }                                                                      
                                                                      else                                                                 
                                                                      {
                                                                          e.Cancel = true;
                                                                          translateRadMessageBox trs = new translateRadMessageBox();
                                                                          trs.translateAllMessageBox("You can't delete answer, because it is answer for medication question");
                                                                      }                                                             
                                                                  }
                                                                  else 
                                                                  {
                                                                      e.Cancel = true;
                                                                      translateRadMessageBox trs = new translateRadMessageBox();
                                                                      trs.translateAllMessageBox("You can't delete answer, because it is answer for epilepsie question");
                                                                  }
                                                              }
                                                              else
                                                              {
                                                                  e.Cancel = true;
                                                                  translateRadMessageBox trs = new translateRadMessageBox();
                                                                  trs.translateAllMessageBox("You can't delete answer, because it is answer for diets question");
                                                              }
                                                         }
                                                         else
                                                         {
                                                             e.Cancel = true;
                                                             translateRadMessageBox trs = new translateRadMessageBox();
                                                             trs.translateAllMessageBox("You can't delete answer, because it is answer for devices question");
                                                         }
                                                     }
                                                     else
                                                     {
                                                         e.Cancel = true;
                                                         translateRadMessageBox trs = new translateRadMessageBox();
                                                         trs.translateAllMessageBox("You can't delete answer, because it is answer for rented device question");
                                                     }
                                                }

                                                else
                                                {
                                                    e.Cancel = true;
                                                    translateRadMessageBox trs = new translateRadMessageBox();
                                                    trs.translateAllMessageBox("You can't delete answer, because it is answer for airport codes question");
                                                }
                                            }
                                            else
                                            {
                                                e.Cancel = true;
                                                translateRadMessageBox trs = new translateRadMessageBox();
                                                trs.translateAllMessageBox("You can't delete this answer, because it is answer for anchorage");
                                            }
                                        }
                                        else
                                        {
                                            e.Cancel = true;
                                            translateRadMessageBox trs = new translateRadMessageBox();
                                            trs.translateAllMessageBox("You can't delete this answer, because it is answer for arm using sometimes");
                                        }
                                    }
                                    else
                                    {
                                        e.Cancel = true;
                                        translateRadMessageBox trs = new translateRadMessageBox();
                                        trs.translateAllMessageBox("You can't delete this answer, because it is answer for arm using always");
                                    }
                                }
                                else
                                {
                                    e.Cancel = true;
                                    translateRadMessageBox trs = new translateRadMessageBox();
                                    trs.translateAllMessageBox("You can't delete this answer, because it is answer for wheelchair");
                                }
                            }
                            else
                            {
                                e.Cancel = true;
                                translateRadMessageBox trs = new translateRadMessageBox();
                                trs.translateAllMessageBox("You can't delete this answer, because it is answer for rollator");
                            }
                        }
                        else
                        {
                            translateRadMessageBox trs = new translateRadMessageBox();
                            trs.translateAllMessageBox("You haven't successfully delete answer because there is no id");
                        }
                    }
                    else
                    {
                        e.Cancel = true;
                        translateRadMessageBox trs = new translateRadMessageBox();
                        trs.translateAllMessageBox("You haven't successfully change answer");
                    }
                }
                else
                {
                    e.Cancel = true;
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("You have to feel type");
                }
            }
            else
            {
                e.Cancel = true;
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("You have to select or add question that you want to fill answers.");
            }
        }

        private void radGridAnswer_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            int i = 0;
           
            foreach (var column in radGridAnswer.Columns)
            {
                column.Width = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 38);
                
                column.MinWidth = column.Width;
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString(radGridAnswer.Columns[i].HeaderText) != null)
                        radGridAnswer.Columns[i].HeaderText = resxSet.GetString(radGridAnswer.Columns[i].HeaderText);
                }

                i++;
            }
          
               
           
                if (radGridAnswer.Columns.Count > 0)
            {
                radGridAnswer.Columns["idQueryType"].IsVisible = false;
                radGridAnswer.Columns["idQuest"].IsVisible = false;
                radGridAnswer.Columns["idAnsType"].IsVisible = false;
                radGridAnswer.Columns["idAns"].IsVisible = false;
                radGridAnswer.Columns["nameLabel"].ReadOnly = true;
                radGridAnswer.Columns["idQuestSkills"].IsVisible = false;

                if (isLoad == false)
                {
                    radGridAnswer.Columns["Skills"].Width = (int)(this.CreateGraphics().MeasureString(radGridAnswer.Columns["Skills"].HeaderText, this.Font).Width + 60);
                    radGridAnswer.Columns["Type"].Width = (int)(this.CreateGraphics().MeasureString(radGridAnswer.Columns["Type"].HeaderText, this.Font).Width + 38);

                    if (File.Exists(layoutMedicalAnswer))
                    {
                        radGridAnswer.LoadLayout(layoutMedicalAnswer);
                    }
                }
            }


        }

        private void radGridQuestion_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            int i = 0;
            foreach (var column in radGridQuestion.Columns)
            {
                column.Width = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 38);
                column.MinWidth = column.Width;

                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString(radGridQuestion.Columns[i].HeaderText) != null)
                        radGridQuestion.Columns[i].HeaderText = resxSet.GetString(radGridQuestion.Columns[i].HeaderText);
                }

                i++;
            }
            if (radGridQuestion.Columns.Count > 0)
            {
                radGridQuestion.Columns["idQuestGroup"].IsVisible = false;
                radGridQuestion.Columns["idQuest"].IsVisible = false;
            }
        }

        private void radGridQuestionGroup_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            int i = 0;
            foreach (var column in radGridQuestionGroup.Columns)
            {
                column.Width = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 38);
                column.MinWidth = column.Width;

                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString(radGridQuestionGroup.Columns[i].HeaderText) != null)
                        radGridQuestionGroup.Columns[i].HeaderText = resxSet.GetString(radGridQuestionGroup.Columns[i].HeaderText);
                }

                i++;
            }
            if (radGridQuestionGroup.Columns.Count > 0)
            {
                radGridQuestionGroup.Columns["idQuestGroup"].IsVisible = false;
                radGridQuestionGroup.Columns["idCompany"].IsVisible = false;
            }
        }
        private bool isInListIdQuest(int idAns, int idQuest)
        {
            bool rez=false;
            MedicalVoluntaryBUS mvBUS =new MedicalVoluntaryBUS();
            List<int> listAns=new List<int>();
            listAns = mvBUS.checkAnsForQuestDevice(idQuest);
            if (listAns != null)
            {
                if (listAns.Count > 0)
                {
                    for (int i = 0; i < listAns.Count; i++)
                    {
                        if (idAns == listAns[i])
                        {
                            rez = true;
                            break;
                        }
                    }
                }
                else
                {
                    rez = false;
                }
            }
            else
            {
                rez = false;
            }
          return  rez;
        }

        private void radMenuButtonSaveLayout_Click(object sender, EventArgs e)
        {
            if (File.Exists(layoutMedicalAnswer))
            {
                File.Delete(layoutMedicalAnswer);
            }
            radGridAnswer.SaveLayout(layoutMedicalAnswer);

            //if (radGridAnswer.Columns["select"] != null)
            //    radGridAnswer.Columns["select"].IsVisible = true;

            translateRadMessageBox tr = new translateRadMessageBox();
            tr.translateAllMessageBox("Layout saved");
        }

        private void radGridAnswer_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
            //Restriction  mouse wheel and KayUp, KeyDown for grid when is in Edit mode Gorance 29 08
            var editor = e.ActiveEditor as GridSpinEditor;
            if (editor != null)
            {
                var element = editor.EditorElement as GridSpinEditorElement;
                element.InterceptArrowKeys = false;
                element.EnableMouseWheel = false;
            }
        }

        private void radGridQuestion_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
            //Restriction  mouse wheel and KayUp, KeyDown for grid when is in Edit mode Gorance 29 08
            var editor = e.ActiveEditor as GridSpinEditor;
            if (editor != null)
            {
                var element = editor.EditorElement as GridSpinEditorElement;
                element.InterceptArrowKeys = false;
                element.EnableMouseWheel = false;
            }
        }

    }
}
