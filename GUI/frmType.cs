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
using BIS.Business;
using BIS.Model;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using System.Resources;
using BIS;
using BIS.DAO;

namespace GUI
{
    public partial class frmType : Form
    {
        private TypeModel type;
        private List<TypeModel> fmodel;
        List<IModel> binding;
        public bool isChanged;
        List<LastIdModel> idList;
       
        // dodato zbog prevoda
        UsersBUS ubus = new UsersBUS();
        List<TranslateUstrModel> translate;
        string prevodioc = "";



        private TypeDAO _typeDAO;
        private TypeBUS _typeBUS;
        Dictionary<string, string> dictionary = new Dictionary<string, string>();

        public frmType()
        {
            InitializeComponent();
        }
        public frmType(IModel model)
        {
            type = (TypeModel)model;

            InitializeComponent();
        }
        private void radButtonSave_Click(object sender, EventArgs e)
        {
            if (type != null)

                Update();

            if (type == null)

                Insert();

        }

        #region Insert

        private void InsertAccDailyType()
        {
            _typeDAO = new TypeDAO();
            DataTable dataTable = _typeDAO.idAccDailyType();
            if (dataTable.Rows.Count > 0)
            {
                int Lastid = Convert.ToInt32(dataTable.Rows[0][0]);
                if (_typeBUS.InsertAccDailyType(Lastid + 1, txtNewName.Text.ToString(), this.Name, Login._user.idUser) == false)
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("Something went wrong with inserting!");
                }
                else
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("You inserted acc daily type successfully!");
                    this.DialogResult = DialogResult.Yes;
                    this.Close();

                }
            }
        }

        private void InsertAddress()
        {
            _typeDAO = new TypeDAO();
            DataTable dataTable = _typeDAO.idAddressType();
            if (dataTable.Rows.Count > 0)
            {
                int Lastid = Convert.ToInt32(dataTable.Rows[0][0]);
                if (_typeBUS.InsertTypesAddress(Lastid + 1, txtNewName.Text.ToString(), this.Name, Login._user.idUser) == false)
                {
                    translateRadMessageBox trs = new translateRadMessageBox(); 
                    trs.translateAllMessageBox("Something went wrong with inserting!");
                }
                else
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("You inserted address type successfully!");
                    this.DialogResult = DialogResult.Yes;
                    this.Close();

                }
            }
        }
        private void InsertClient()
        {
            _typeDAO = new TypeDAO();
            DataTable dataTable = _typeDAO.idClientType();
            if (dataTable.Rows.Count > 0)
            {
                int Lastid = Convert.ToInt32(dataTable.Rows[0][0]);
                if (_typeBUS.InsertClientTypes(Lastid + 1, txtNewName.Text.ToString(), this.Name, Login._user.idUser) == false)
                {
                    translateRadMessageBox trs = new translateRadMessageBox(); 
                    trs.translateAllMessageBox("Something went wrong with inserting!");
                }
                else
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("You inserted client type successfully!");
                    this.DialogResult = DialogResult.Yes;
                    this.Close();

                }
            }
        }
        private void InsertContact()
        {
            _typeDAO = new TypeDAO();
            DataTable dataTable = _typeDAO.idContactType();
            if (dataTable.Rows.Count > 0)
            {
                int Lastid = Convert.ToInt32(dataTable.Rows[0][0]);
                if (_typeBUS.InsertContactType(Lastid + 1, txtNewName.Text.ToString(), this.Name, Login._user.idUser) == false)
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("Something went wrong with inserting!");
            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("You inserted contact type successfully!");
                this.DialogResult = DialogResult.Yes;
                this.Close();

            }
        }
        }
        private void InsertEmail()
        {
            _typeDAO = new TypeDAO();
            DataTable dataTable = _typeDAO.idEmailType();
            if (dataTable.Rows.Count > 0)
            {
                int Lastid = Convert.ToInt32(dataTable.Rows[0][0]);
                if (_typeBUS.InsertTypesEmail(Lastid + 1, txtNewName.Text.ToString(), this.Name, Login._user.idUser) == false)
                {
                    translateRadMessageBox trs = new translateRadMessageBox(); 
                    trs.translateAllMessageBox("Something went wrong with inserting!");
                }
                else
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("You inserted email type successfully!");
                    this.DialogResult = DialogResult.Yes;
                    this.Close();

                }
            }
        }
        private void InsertMedAnsType()
        {
            _typeDAO = new TypeDAO();
            DataTable dataTable = _typeDAO.idMedAnsType();
            if (dataTable.Rows.Count > 0)
            {
                int Lastid = Convert.ToInt32(dataTable.Rows[0][0]);
                if (_typeBUS.InsertMedAnsType(Lastid + 1, txtNewName.Text.ToString(), this.Name, Login._user.idUser) == false)
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("Something went wrong with inserting!");
                }
                else
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("You inserted medical answer type successfully!");
                    this.DialogResult = DialogResult.Yes;
                    this.Close();

                }
            }
        }

        private void InsertNote()
        {
            _typeDAO = new TypeDAO();
            DataTable dataTable = _typeDAO.idNoteType();
            if (dataTable.Rows.Count > 0)
            {
                int Lastid = Convert.ToInt32(dataTable.Rows[0][0]);
                if (_typeBUS.InsertTypesNote(Lastid + 1, txtNewName.Text.ToString(), this.Name, Login._user.idUser) == false)
                {
                    translateRadMessageBox trs = new translateRadMessageBox(); 
                    trs.translateAllMessageBox("Something went wrong with inserting!");
                }
                else
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("You inserted note type successfully!");
                    this.DialogResult = DialogResult.Yes;
                    this.Close();

                }
            }
        }
        private void InsertTelephone()
        {
            _typeDAO = new TypeDAO();
            DataTable dataTable = _typeDAO.idTelephoneType();
            if (dataTable.Rows.Count > 0)
            {
                int Lastid = Convert.ToInt32(dataTable.Rows[0][0]);
                if (_typeBUS.InsertTypesTel(Lastid + 1, txtNewName.Text.ToString(), this.Name, Login._user.idUser) == false)
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("Something went wrong with inserting!");
                }
                else
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("You inserted telephone type successfully!");
                    this.DialogResult = DialogResult.Yes;
                    this.Close();

                }
            }
        }
        private void InsertToDo()
        {
            _typeDAO = new TypeDAO();
            DataTable dataTable = _typeDAO.idToDoType();
            if (dataTable.Rows.Count > 0)
            {
                int Lastid = Convert.ToInt32(dataTable.Rows[0][0]);
                if (_typeBUS.InsertToDoType(Lastid + 1, txtNewName.Text.ToString(), this.Name, Login._user.idUser) == false)
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("Something went wrong with inserting!");
                }
                else
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("You inserted to do type successfully!");
                    this.DialogResult = DialogResult.Yes;
                    this.Close();

                }
            }
        }
        private void InsertVolAnswer()
        {
            _typeDAO = new TypeDAO();
            DataTable dataTable = _typeDAO.idVolAnsType();
            if (dataTable.Rows.Count > 0)
            {
                int Lastid = Convert.ToInt32(dataTable.Rows[0][0]);
                if (_typeBUS.InsertVolAnsType(Lastid + 1, txtNewName.Text.ToString(), this.Name, Login._user.idUser) == false)
                {
                    translateRadMessageBox trs = new translateRadMessageBox(); 
                    trs.translateAllMessageBox("Something went wrong with inserting!");
                }
                else
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("You inserted volontary answer type!");
                    this.DialogResult = DialogResult.Yes;
                    this.Close();

                }
            }
        }
        private void InsertMeetingCategory()
        {
            _typeDAO = new TypeDAO();
            DataTable dataTable = _typeDAO.idMeetingCategory();
            if (dataTable.Rows.Count > 0)
            {
                int Lastid = Convert.ToInt32(dataTable.Rows[0][0]);
                if (_typeBUS.InsertMeetingCategory(Lastid + 1, txtNewName.Text.ToString(), this.Name, Login._user.idUser) == false)
                {
                    translateRadMessageBox trs = new translateRadMessageBox(); 
                    trs.translateAllMessageBox("Something went wrong with inserting!");
                }
                else
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("You inserted meeting category successfully!");
                    this.DialogResult = DialogResult.Yes;
                    this.Close();

                }
            }
        }
          private void InsertMeetingsPriority()
          {
              _typeDAO = new TypeDAO();
              DataTable dataTable = _typeDAO.idMeetingPriority();
              if (dataTable.Rows.Count > 0)
              {
                  int Lastid = Convert.ToInt32(dataTable.Rows[0][0]);
                  if (_typeBUS.InsertMeetingsPriority(Lastid + 1, txtNewName.Text.ToString(), this.Name, Login._user.idUser) == false)
                  {
                      translateRadMessageBox trs = new translateRadMessageBox(); 
                      trs.translateAllMessageBox("Something went wrong with inserting!");
                  }
                  else
                  {
                      translateRadMessageBox trs = new translateRadMessageBox();
                      trs.translateAllMessageBox("You inserted meeting priority successfully!");
                      this.DialogResult = DialogResult.Yes;
                      this.Close();
                  }
              }
          }
          private void InsertToDoPriority()
          {
              _typeDAO = new TypeDAO();
              DataTable dataTable = _typeDAO.idToDoPriority();

              if (dataTable.Rows.Count > 0)
              {
                  int Lastid = Convert.ToInt32(dataTable.Rows[0][0]);
                  if (_typeBUS.InsertToDoPriority(Lastid + 1, txtNewName.Text.ToString(), this.Name, Login._user.idUser) == false)
                  {
                      translateRadMessageBox trs = new translateRadMessageBox(); 
                      trs.translateAllMessageBox("Something went wrong with inserting!");
                  }
                  else
                  {
                      translateRadMessageBox trs = new translateRadMessageBox();
                      trs.translateAllMessageBox("You inserted to do priority successfully!");
                      this.DialogResult = DialogResult.Yes;
                      this.Close();
                  }
              }
          }
          private void InsertTitle()
          {
              _typeDAO = new TypeDAO();
              DataTable dataTable = _typeDAO.idTitle();
              if (dataTable.Rows.Count > 0)
              {
                  int Lastid = Convert.ToInt32(dataTable.Rows[0][0]);
                  if (_typeBUS.InsertTitle(Lastid + 1, txtNewName.Text.ToString(), this.Name, Login._user.idUser) == false)
                  {
                      translateRadMessageBox trs = new translateRadMessageBox();
                      trs.translateAllMessageBox("Something went wrong with inserting!");
                  }
                  else
                  {
                      translateRadMessageBox trs = new translateRadMessageBox();
                      trs.translateAllMessageBox("You inserted title successfully!");
                      this.DialogResult = DialogResult.Yes;
                      this.Close();
                  }
              }
          }
          private void InsertEmployeeFunction()
          {
                _typeDAO = new TypeDAO();
              DataTable dataTable = _typeDAO.idEmployeeFunction();
              if (dataTable.Rows.Count > 0)
              {
                  int Lastid = Convert.ToInt32(dataTable.Rows[0][0]);
                  if (_typeBUS.InsertEmployeeFunction(Lastid + 1, txtNewName.Text.ToString(), this.Name, Login._user.idUser) == false)
                  {
                      translateRadMessageBox trs = new translateRadMessageBox(); 
                      trs.translateAllMessageBox("Something went wrong with inserting!");
                  }
                  else
                  {
                      translateRadMessageBox trs = new translateRadMessageBox();
                      trs.translateAllMessageBox("You inserted employee function successfully!");
                      this.DialogResult = DialogResult.Yes;
                      this.Close();
                  }
              }
          }
          private void InsertEmployeeStatus()
          {
              _typeDAO = new TypeDAO();
              DataTable dataTable = _typeDAO.idEmployeeStatus(); if (dataTable.Rows.Count > 0)
              {
                  int Lastid = Convert.ToInt32(dataTable.Rows[0][0]);
                  if (_typeBUS.InsertEmployeeStatus(Lastid + 1, txtNewName.Text.ToString(), this.Name, Login._user.idUser) == false)
                  {
                      translateRadMessageBox trs = new translateRadMessageBox(); 
                      trs.translateAllMessageBox("Something went wrong with inserting!");
                  }
                  else
                  {
                      translateRadMessageBox trs = new translateRadMessageBox();
                      trs.translateAllMessageBox("You insered employee status successfully!");
                      this.DialogResult = DialogResult.Yes;
                      this.Close();
                  }
              }
          }

          private void InsertMeetingsStatus()
          {
              _typeDAO = new TypeDAO();
              DataTable dataTable = _typeDAO.idMeetingsStatus();
              if (dataTable.Rows.Count > 0)
              {
                  int Lastid = Convert.ToInt32(dataTable.Rows[0][0]);
                  if (_typeBUS.InsertMeetingsStatus(Lastid + 1, txtNewName.Text.ToString(), this.Name, Login._user.idUser) == false)
                  {
                      translateRadMessageBox trs = new translateRadMessageBox(); 
                      trs.translateAllMessageBox("Something went wrong with inserting!");
                  }
                  else
                  {
                      translateRadMessageBox trs = new translateRadMessageBox();
                      trs.translateAllMessageBox("You inserted meetings status successfully!");
                      this.DialogResult = DialogResult.Yes;
                      this.Close();
                  }
              }
          }
          private void InsertToDoStatus()
          {
              _typeDAO = new TypeDAO();
              DataTable dataTable = _typeDAO.idToDoStatus();
              if (dataTable.Rows.Count > 0)
              {
                  int Lastid = Convert.ToInt32(dataTable.Rows[0][0]);
                  if (_typeBUS.InsertToDoStatus(Lastid + 1, txtNewName.Text.ToString(), this.Name, Login._user.idUser) == false)
                  {
                      translateRadMessageBox trs = new translateRadMessageBox(); 
                      trs.translateAllMessageBox("Something went wrong with inserting!");
                  }
                  else
                  {
                      translateRadMessageBox trs = new translateRadMessageBox();
                      trs.translateAllMessageBox("You inserted to do status successfully!");
                      this.DialogResult = DialogResult.Yes;
                      this.Close();
                  }
              }
          }
          private void InsertContactReason()
          {
              _typeDAO = new TypeDAO();
              DataTable datatable = _typeDAO.idContactReason();
              if (datatable.Rows.Count > 0)
              {
                  int Lastid = Convert.ToInt32(datatable.Rows[0][0]);
                  if (_typeBUS.InsertContactReason(Lastid + 1, txtNewName.Text.ToString(), this.Name, Login._user.idUser) == false)
                  {
                      translateRadMessageBox trs = new translateRadMessageBox();
                      trs.translateAllMessageBox("Something went wrongwith inserting!");
                  }
                  else
                  {
                      translateRadMessageBox trs = new translateRadMessageBox();
                      trs.translateAllMessageBox("You inserted contact reason successfully!");
                      this.DialogResult = DialogResult.Yes;
                      this.Close();
                  }
              }
          }
        #endregion

        #region Update
          private void UpdateAccDailyType()
          {
              if (_typeBUS.UpdateAccDailyType(Convert.ToInt32(type.ID), txtNewName.Text.ToString(), this.Name, Login._user.idUser) == false)
              {
                  translateRadMessageBox trs = new translateRadMessageBox(); 
                  trs.translateAllMessageBox("Something went wrong with updating!");
              }
              else
              {
                  translateRadMessageBox trs = new translateRadMessageBox();
                  trs.translateAllMessageBox("You updated acc daily type successfully!");
                  this.DialogResult = DialogResult.Yes;
                  this.Close();

              }
          }

        private void UpdateAddress()
        {
            if (_typeBUS.UpdateTypesAddress(Convert.ToInt32(type.ID), txtNewName.Text.ToString(), this.Name, Login._user.idUser) == false)
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("Something went wrong with updating!");
            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("You updated address type successfully!");
                this.DialogResult = DialogResult.Yes;
                this.Close();

            }
        }
        private void UpdateClient()
        {
            if (_typeBUS.UpdateClientType(Convert.ToInt32(type.ID), txtNewName.Text.ToString(), this.Name, Login._user.idUser) == false)
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("Something went wrong with updating!");
            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("You updated client type successfully!");
                this.DialogResult = DialogResult.Yes;
                this.Close();

            }
        }

        private void UpdateContact()
        {
            if (_typeBUS.UpdateContactType(Convert.ToInt32(type.ID), txtNewName.Text.ToString(), this.Name, Login._user.idUser) == false)
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("Something went wrongwith updating!");
            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("You updated contact type successfully");
                this.DialogResult = DialogResult.Yes;
                this.Close();

            }
        }

        private void UpdateEmail()
        {
            if (_typeBUS.UpdateTypesEmail(Convert.ToInt32(type.ID), txtNewName.Text.ToString(), this.Name, Login._user.idUser) == false)
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("Something went wrongwith updating!");
            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("You updated email type successfully!");
                this.DialogResult = DialogResult.Yes;
                this.Close();

            }
        }
        private void UpdateMedicalAnswer()
        {
            if (_typeBUS.UpdateMedAnsType(Convert.ToInt32(type.ID), txtNewName.Text.ToString(), this.Name, Login._user.idUser) == false)
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("Something went wrong with updating!");
            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("You updated medical answer type successfully!");
                this.DialogResult = DialogResult.Yes;
                this.Close();

            }
        }
        private void UpdateNote()
        {
            if (_typeBUS.UpdateTypesNote(Convert.ToInt32(type.ID), txtNewName.Text.ToString(), this.Name, Login._user.idUser) == false)
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("Something went wrong with updating!");
            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("You updated note type successfully!");
                this.DialogResult = DialogResult.Yes;
                this.Close();

            }

        }
        private void UpdateTelephone()
        {
            if (_typeBUS.UpdateTypesTel(Convert.ToInt32(type.ID), txtNewName.Text.ToString(), this.Name, Login._user.idUser) == false)
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("Something went wrongwith updating!");
            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("You updated telephone type successfully!");
                this.DialogResult = DialogResult.Yes;
                this.Close();

            }
        }
        private void UpdateToDo()
        {
            if (_typeBUS.UpdateToDoType(Convert.ToInt32(type.ID), txtNewName.Text.ToString(), this.Name, Login._user.idUser) == false)
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("Something went wrong with updating!");
            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("You updated to do type successfully!");
                this.DialogResult = DialogResult.Yes;
                this.Close();

            }
        }
        private void UpdateVolAnswer()
        {
            if (_typeBUS.UpdateVolAnsType(Convert.ToInt32(type.ID), txtNewName.Text.ToString(), this.Name, Login._user.idUser) == false)
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("Something went wrong with updating!");
            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("You updated volontary answer type!");
                this.DialogResult = DialogResult.Yes;
                this.Close();

            }
        }

        private void UpdateMeetingCategory()
        {
            if (_typeBUS.UpdateMeetingCategory(Convert.ToInt32(type.ID), txtNewName.Text.ToString(), this.Name, Login._user.idUser) == false)
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("Something went wrong with updating!");
            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("You updated meeting category successfully!");
                this.DialogResult = DialogResult.Yes;
                this.Close();

            }
        }
        private void UpdateMeetingPriority()
        {
            if (_typeBUS.UpdateMeetingPriority(Convert.ToInt32(type.ID), txtNewName.Text.ToString(), this.Name, Login._user.idUser) == false)
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("Something went wrong with updating!");
            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("You updated meeting priority successfully!");
                this.DialogResult = DialogResult.Yes;
                this.Close();

            }
        }

        private void UpdateToDoPriority()
        {
            if (_typeBUS.UpdateToDoPriority(Convert.ToInt32(type.ID), txtNewName.Text.ToString(), this.Name, Login._user.idUser) == false)
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("Something went wrong with updating!");
            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("You updated to do priority successfully!");
                this.DialogResult = DialogResult.Yes;
                this.Close();

            }
        }

        private void UpdateTitle()
        {
            if (_typeBUS.UpdateTitle(Convert.ToInt32(type.ID), txtNewName.Text.ToString(), this.Name, Login._user.idUser) == false)
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("Something went wrongwith updating!");
            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("You updated title successfully!");
                this.DialogResult = DialogResult.Yes;
                this.Close();

            }
        }

        private void UpdateEmployeeFunction()
        {
            if (_typeBUS.UpdateEmployeeFunction(Convert.ToInt32(type.ID), txtNewName.Text.ToString(), this.Name, Login._user.idUser) == false)
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("Something went wrong with updating!");
            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("You updated employee function successfully!");
                this.DialogResult = DialogResult.Yes;
                this.Close();

            }
        }

        private void UpdateEmployeeStatus()
        {
            if (_typeBUS.UpdateEmployeeStatus(Convert.ToInt32(type.ID), txtNewName.Text.ToString(), this.Name, Login._user.idUser) == false)
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("Something went wrong with updating!");
            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("You updated employee status successfully!");
                this.DialogResult = DialogResult.Yes;
                this.Close();

            }
        }

        private void UpdateMeetingStatus()
        {
            if (_typeBUS.UpdateMeetingStatus(Convert.ToInt32(type.ID), txtNewName.Text.ToString(), this.Name, Login._user.idUser) == false)
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("Something went wrong with updating!");
            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("You updated meeting status successfully!");
                this.DialogResult = DialogResult.Yes;
                this.Close();

            }
        }

        private void UpdateToDoStatus()
        {
            if (_typeBUS.UpdateToDoStatus(Convert.ToInt32(type.ID), txtNewName.Text.ToString(), this.Name, Login._user.idUser) == false)
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("Something went wrong with updating!");
            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("You updated to do status successfully!");
                this.DialogResult = DialogResult.Yes;
                this.Close();

            }
        }

        private void UpdateContactReason()
        {
            if (_typeBUS.UpdateContactReason(Convert.ToInt32(type.ID), txtNewName.Text.ToString(), this.Name, Login._user.idUser) == false)
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("Something went wrong with updating!");
            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("You updated contact reason successfully!");
                this.DialogResult = DialogResult.Yes;
                this.Close();

            }
        }
        #endregion

        public void Update()
        {

            _typeBUS = new TypeBUS();
            type.name = txtNewName.Text;

            //if (type.type == "Acc daily type")
            //{
            //    UpdateAccDailyType();
            //}
            _typeBUS = new TypeBUS();
            type.name = txtNewName.Text;

            translate = ubus.Translate("Acc daily type", Login._user.lngUser);
            if (translate != null)
            {
                if (translate.Count > 0)
                {
                    prevodioc = translate[0].stringValue;
                    if (type.type == prevodioc)
                    {

                        UpdateAccDailyType();
                    }
                }
            }
            //if (type.type == "Address type")
            //{
            //    UpdateAddress();
            //}
            _typeBUS = new TypeBUS();
            type.name = txtNewName.Text;

            translate = ubus.Translate("Address type", Login._user.lngUser);
            if (translate != null)
            {
                if (translate.Count > 0)
                {
                    prevodioc = translate[0].stringValue;
                    if (type.type == prevodioc)
                    {

                        UpdateAddress();
                    }
                }
            }

            //if (type.type == "Client types")
            //{
            //    UpdateClient();
            //}
            _typeBUS = new TypeBUS();
            type.name = txtNewName.Text;

            translate = ubus.Translate("Client type", Login._user.lngUser);
            if (translate != null)
            {
                if (translate.Count > 0)
                {
                    prevodioc = translate[0].stringValue;
                    if (type.type == prevodioc)
                    {

                        UpdateClient();
                    }
                }
            }


            //if (type.type == "Contact type")
            //{
            //    UpdateContact();
            //}
            _typeBUS = new TypeBUS();
            type.name = txtNewName.Text;

            translate = ubus.Translate("Contact type", Login._user.lngUser);
            if (translate != null)
            {
                if (translate.Count > 0)
                {
                    prevodioc = translate[0].stringValue;
                    if (type.type == prevodioc)
                    {

                        UpdateContact();
                    }
                }
            }

            //if (type.type == "Email type")
            //{
            //    UpdateEmail();
            //}
            _typeBUS = new TypeBUS();
            type.name = txtNewName.Text;

            translate = ubus.Translate("Email type", Login._user.lngUser);
            if (translate != null)
            {
                if (translate.Count > 0)
                {
                    prevodioc = translate[0].stringValue;
                    if (type.type == prevodioc)
                    {

                        UpdateEmail();
                    }
                }
            }

            //if (type.type == "Medical answer type")
            //{
            //    UpdateMedicalAnswer();
            //}
            _typeBUS = new TypeBUS();
            type.name = txtNewName.Text;

            translate = ubus.Translate("Medical answer type", Login._user.lngUser);
            if (translate != null)
            {
                if (translate.Count > 0)
                {
                    prevodioc = translate[0].stringValue;
                    if (type.type == prevodioc)
                    {

                        UpdateMedicalAnswer();
                    }
                }
            }
            //if (type.type == "Note type")
            //{
            //    UpdateNote();
            //}
            _typeBUS = new TypeBUS();
            type.name = txtNewName.Text;

            translate = ubus.Translate("Note type", Login._user.lngUser);
            if (translate != null)
            {
                if (translate.Count > 0)
                {
                    prevodioc = translate[0].stringValue;
                    if (type.type == prevodioc)
                    {

                        UpdateNote();
                    }
                }
            }

            //if (type.type == "Telephone type")
            //{
            //    UpdateTelephone();
            //}
            _typeBUS = new TypeBUS();
            type.name = txtNewName.Text;

            translate = ubus.Translate("Telephone type", Login._user.lngUser);
            if (translate != null)
            {
                if (translate.Count > 0)
                {
                    prevodioc = translate[0].stringValue;
                    if (type.type == prevodioc)
                    {

                        UpdateTelephone();
                    }
                }
            }

            //if (type.type == "To do type")
            //{
            //    UpdateToDo();
            //}
            _typeBUS = new TypeBUS();
            type.name = txtNewName.Text;

            translate = ubus.Translate("To do type", Login._user.lngUser);
            if (translate != null)
            {
                if (translate.Count > 0)
                {
                    prevodioc = translate[0].stringValue;
                    if (type.type == prevodioc)
                    {

                        UpdateToDo();
                    }
                }
            }


            //if (type.type == "Volontary answer type")
            //{
            //    UpdateVolAnswer();
            //}
            _typeBUS = new TypeBUS();
            type.name = txtNewName.Text;

            translate = ubus.Translate("Volontary answer type", Login._user.lngUser);
            if (translate != null)
            {
                if (translate.Count > 0)
                {
                    prevodioc = translate[0].stringValue;
                    if (type.type == prevodioc)
                    {

                        UpdateVolAnswer();
                    }
                }
            }
            //if (type.type == "Meeting category")
            //{
            //    UpdateMeetingCategory();
            //}
            _typeBUS = new TypeBUS();
            type.name = txtNewName.Text;

            translate = ubus.Translate("Meeting category", Login._user.lngUser);
            if (translate != null)
            {
                if (translate.Count > 0)
                {
                    prevodioc = translate[0].stringValue;
                    if (type.type == prevodioc)
                    {

                        UpdateMeetingCategory();
                    }
                }
            }
            //if (type.type == "Meeting priority")
            //{
            //    UpdateMeetingPriority();
            //}
            _typeBUS = new TypeBUS();
            type.name = txtNewName.Text;

            translate = ubus.Translate("Meeting priority", Login._user.lngUser);
            if (translate != null)
            {
                if (translate.Count > 0)
                {
                    prevodioc = translate[0].stringValue;
                    if (type.type == prevodioc)
                    {

                        UpdateMeetingPriority();
                    }
                }
            }

            //if (type.type == "To do priority")
            //{
            //    UpdateToDoPriority();
            //}
            _typeBUS = new TypeBUS();
            type.name = txtNewName.Text;

            translate = ubus.Translate("To do priority", Login._user.lngUser);
            if (translate != null)
            {
                if (translate.Count > 0)
                {
                    prevodioc = translate[0].stringValue;
                    if (type.type == prevodioc)
                    {

                        UpdateToDoPriority();
                    }
                }
            }

            //if (type.type == "Title")
            //{
            //    UpdateTitle();
            //}

            _typeBUS = new TypeBUS();
            type.name = txtNewName.Text;

            translate = ubus.Translate("Title", Login._user.lngUser);
            if (translate != null)
            {
                if (translate.Count > 0)
                {
                    prevodioc = translate[0].stringValue;
                    if (type.type == prevodioc)
                    {

                        UpdateTitle();
                    }
                }
            }

            //if (type.type == "Employee function")
            //{
            //    UpdateEmployeeFunction();
            //}


            _typeBUS = new TypeBUS();
            type.name = txtNewName.Text;

            translate = ubus.Translate("Employee function", Login._user.lngUser);
            if (translate != null)
            {
                if (translate.Count > 0)
                {
                    prevodioc = translate[0].stringValue;
                    if (type.type == prevodioc)
                    {

                        UpdateEmployeeFunction();
                    }
                }
            }

            //if (type.type == "Employee status")
            //{
            //    UpdateEmployeeStatus();
            //}

            _typeBUS = new TypeBUS();
            type.name = txtNewName.Text;

            translate = ubus.Translate("Employee status", Login._user.lngUser);
            if (translate != null)
            {
                if (translate.Count > 0)
                {
                    prevodioc = translate[0].stringValue;
                    if (type.type == prevodioc)
                    {

                        UpdateEmployeeStatus();
                    }
                }
            }
            //if (type.type == "Meeting status")
            //{
            //    UpdateMeetingStatus();
            //}

            _typeBUS = new TypeBUS();
            type.name = txtNewName.Text;

            translate = ubus.Translate("Meeting status", Login._user.lngUser);
            if (translate != null)
            {
                if (translate.Count > 0)
                {
                    prevodioc = translate[0].stringValue;
                    if (type.type == prevodioc)
                    {

                        UpdateMeetingStatus();
                    }
                }
            }

            //if (type.type == "To do status")
            //{
            //    UpdateToDoStatus();
            //}

            _typeBUS = new TypeBUS();
            type.name = txtNewName.Text;

            translate = ubus.Translate("To do status", Login._user.lngUser);
            if (translate != null)
            {
                if (translate.Count > 0)
                {
                    prevodioc = translate[0].stringValue;
                    if (type.type == prevodioc)
                    {

                        UpdateToDoStatus();
                    }
                }
            }

            //if (type.type == "Contact reason")
            //{
            //    UpdateContactReason();
            //}

            _typeBUS = new TypeBUS();
            type.name = txtNewName.Text;

            translate = ubus.Translate("Contact reason", Login._user.lngUser);
            if (translate != null)
            {
                if (translate.Count > 0)
                {
                    prevodioc = translate[0].stringValue;
                    if (type.type == prevodioc)
                    {

                        UpdateContactReason();
                    }
                }
            }
         
        }

        public void Insert()
        {
            _typeBUS = new TypeBUS();
            //  type.name = txtNewName.Text;
            if (ddlType.SelectedIndex != 0 && txtNewName.Text != "")
            {

                if (ddlType.SelectedIndex == 1)// indeks 1 u drop down listi je TypesAddress
                {
                    InsertAccDailyType();
                }

                if (ddlType.SelectedIndex == 2)// indeks 2 u drop down listi je TypesAddress
                {
                    InsertAddress();
                }
               

                if (ddlType.SelectedIndex == 3)// indeks 3 u drop down listi je ClientTypes
                {
                    InsertClient();
                }
                
                if (ddlType.SelectedIndex == 4)// // indeks 4 u drop down listi je ContactType
                {
                    InsertContact();
                }
                if (ddlType.SelectedIndex == 5)// indeks 5 u drop down listi je ContactReason
                {
                    InsertContactReason();
                }
                if (ddlType.SelectedIndex == 6)  // indeks 6 u drop down listi je TypesEmail
                {
                    InsertEmail();
                }

                if (ddlType.SelectedIndex == 7)  // indeks 7 u drop down listi je EmployeeFunction
                {
                    InsertEmployeeFunction();
                }

                if (ddlType.SelectedIndex == 8)  // indeks 8 u drop down listi je EmployeeStatus
                {
                    InsertEmployeeStatus();
                }

                if (ddlType.SelectedIndex == 9)   // indeks 9 u drop down listi je MedAnsType
                {
                    InsertMedAnsType();
                }

                if (ddlType.SelectedIndex == 10)   // indeks 10 u drop down listi je MeetingCategory
                {
                    InsertMeetingCategory();
                }

                if (ddlType.SelectedIndex == 11)   // indeks 11 u drop down listi je MeetingsPriority
                {
                    InsertMeetingsPriority();
                }

                if (ddlType.SelectedIndex == 12)   // indeks 12 u drop down listi je MeetingsStatus
                {
                    InsertMeetingsStatus();
                }

                if (ddlType.SelectedIndex == 13)   // indeks 13 u drop down listi je TypesNote
                {
                    InsertNote();
                }

                if (ddlType.SelectedIndex == 14)   // indeks 14 u drop down listi je TypesTel
                {
                    InsertTelephone();
                }

                if (ddlType.SelectedIndex == 15)   // indeks 15 u drop down listi je Title
                {
                    InsertTitle();
                }

                if (ddlType.SelectedIndex == 16)   // indeks 16 u drop down listi je ToDoPriority
                {
                    InsertToDoPriority();
                }

                if (ddlType.SelectedIndex == 17)   // indeks 17 u drop down listi je ToDoType
                {
                    InsertToDo();
                }

                if (ddlType.SelectedIndex == 18)   // indeks 18 u drop down listi je ToDoStatus
                {
                    InsertToDoStatus();
                }

                if (ddlType.SelectedIndex == 19)   // indeks 19 u drop down listi je VolAnsType
                {
                    InsertVolAnswer();
                }
             
            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox(); 
                trs.translateAllMessageBox("Something went wrong. Checked data!");
            }


        }

        private void radButtonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void frmType_Load(object sender, EventArgs e)
        {
            setTranslation();
            if (type != null)
            {
                ddlType.Visible = false;
                txtNewName.Text = type.name;

            }

        }

        private void setTranslation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (btnSave.Text!=null)
                    btnSave.Text = resxSet.GetString(btnSave.Text);
                if (btnCancel.Text!=null)
                    btnCancel.Text = resxSet.GetString(btnCancel.Text);
                if (lblNew.Text!=null)
                    lblNew.Text = resxSet.GetString(lblNew.Text);

            }


        }
    }
}
