using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Scheduler.Dialogs;
using BIS.Business;
using BIS.Model;
using System.Resources;

namespace GUI
{
    public partial class MeetingEditAppointment : EditAppointmentDialog
    {
        int perosnID = -1;
        string personName = "";
        public int idArr = -1;
        private string nameArr = "";

        Guid GuidCheck = Guid.Empty;
        public MeetingEditAppointment()
        {
            InitializeComponent();
        }
        public MeetingEditAppointment(int personid, string personame)
        {
            InitializeComponent();
            perosnID = personid;
            personName = personame;
        }

        private void MeetingEditAppointment_Load(object sender, EventArgs e)
        {
            setTranslation();

            if (Login._user.isUserManager == false)
            {
                btnOwner.Enabled = false;
                btnResponsible.Enabled = false;
            }
            
        }
        protected override void LoadSettingsFromEvent(IEvent ev)
        {
            base.LoadSettingsFromEvent(ev);

            AppointmentWithEmail appointmentWithEmail = ev as AppointmentWithEmail;
            if (appointmentWithEmail != null)
            {                              
                this.GuidCheck = Guid.Parse(appointmentWithEmail.UniqueId.ToString());
                this.txtCategory.Text = appointmentWithEmail.Category.ToString();
                this.txtClient.Text = appointmentWithEmail.Client.ToString();
                this.txtContact.Text = appointmentWithEmail.Contact.ToString();

                
                this.txtOwner.Text = appointmentWithEmail.Owner.ToString();

                this.txtPriority.Text = appointmentWithEmail.Priority.ToString();
                this.txtProject.Text = appointmentWithEmail.Project.ToString();
                this.txtResponsible.Text = appointmentWithEmail.Responsible.ToString();
                this.txtStatus.Text = appointmentWithEmail.Status.ToString();
                chkAllDay.Checked = appointmentWithEmail.AllDay;

                txtCategoryName.Text = appointmentWithEmail.CategoryName;
                txtClientName.Text = appointmentWithEmail.ClientName;
                txtContactName.Text = appointmentWithEmail.ContactName;

              
                txtOwnerName.Text = appointmentWithEmail.OwnerName;

                txtPriorityName.Text = appointmentWithEmail.PriorityName;
                txtProjectName.Text = appointmentWithEmail.ProjectName;
                txtResponsibleName.Text = appointmentWithEmail.ResponsibleName;
                txtStatusName.Text = appointmentWithEmail.StatusName;

                txtNameArrangement.Text = appointmentWithEmail.Arrangement.ToString();
                txtArrangement.Text = appointmentWithEmail.IdArrangement.ToString();
                              
                if (txtArrangement.Text.Trim() == "0")
                {
                    if (idArr > 0)
                    {
                        ArrangementBUS abus = new ArrangementBUS();
                        ArrangementModel amod = new ArrangementModel();

                        amod = abus.GetArrangementById(idArr);
                        if (amod != null)
                        {
                            txtArrangement.Text = amod.idArrangement.ToString();
                            txtNameArrangement.Text = amod.nameArrangement;
                        }
                    }
                }

               if(Login._user.isUserManager == false)
               {
                   EmployeeBUS empbus = new EmployeeBUS();
                   EmployeeModel emmodel = new EmployeeModel();
                   emmodel = empbus.GetEmployee(Login._user.idEmployee,Login._user.lngUser);

                   txtOwner.Text = emmodel.idEmployee.ToString();
                   txtOwnerName.Text = emmodel.firstNameEmployee + " " + emmodel.lastNameEmployee;

                   txtResponsible.Text = emmodel.idEmployee.ToString();
                   txtResponsibleName.Text = emmodel.firstNameEmployee + " " + emmodel.lastNameEmployee;
               }
                AppointmentsBUS app = new AppointmentsBUS();

            bool b = app.ChechIfAppointmentExist(GuidCheck);
            if (b == true)
            {
                List<BISAppointment> bmodel = new List<BISAppointment>(); 
                 AppointmentsBUS bus = new AppointmentsBUS();
                 string idguid = GuidCheck.ToString();
                 bmodel = bus.GetAppointmentsById(idguid);
                 if (bmodel != null)
                 {
                     if (bmodel.Count > 0)
                     {
                         txtNameArrangement.Text = bmodel[0].NameArrangement;
                         txtArrangement.Text = bmodel[0].IdArrangement.ToString();
                     }
                 }
            }

               if (perosnID != -1)
               {
                   txtContact.Text = perosnID.ToString();
                   txtContactName.Text = personName;
               }
            }            
        }

        protected override void ApplySettingsToEvent(IEvent ev)
        {
            AppointmentWithEmail appointmentWithEmail = ev as AppointmentWithEmail;
            if (appointmentWithEmail != null)
            {                
                appointmentWithEmail.Category = Int32.Parse(this.txtCategory.Text);
                appointmentWithEmail.Client =  Int32.Parse(this.txtClient.Text);
                appointmentWithEmail.Contact = Int32.Parse(this.txtContact.Text);
                appointmentWithEmail.Owner = Int32.Parse(this.txtOwner.Text);
                appointmentWithEmail.Priority = Int32.Parse(this.txtPriority.Text);
                appointmentWithEmail.Project = Int32.Parse(this.txtProject.Text);
                appointmentWithEmail.Responsible = Int32.Parse(this.txtResponsible.Text);
                appointmentWithEmail.Status  = Int32.Parse(txtStatus.Text);
                appointmentWithEmail.AllDay = chkAllDay.Checked;

                appointmentWithEmail.CategoryName = txtCategoryName.Text;
                appointmentWithEmail.ClientName = txtClientName.Text;
                appointmentWithEmail.ContactName = txtContactName.Text;
                appointmentWithEmail.OwnerName = txtOwnerName.Text;
                appointmentWithEmail.PriorityName = txtPriorityName.Text;
                appointmentWithEmail.ProjectName = txtProjectName.Text;
                appointmentWithEmail.ResponsibleName = txtResponsibleName.Text;
                appointmentWithEmail.StatusName = txtStatusName.Text;
                appointmentWithEmail.IdArrangement = Int32.Parse(txtArrangement.Text);
                appointmentWithEmail.Arrangement = txtNameArrangement.Text;

                
                
                
                
            }
            base.ApplySettingsToEvent(ev);
        }

        protected override IEvent CreateNewEvent()
        {
            return new AppointmentWithEmail();
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            MeetingsBUS bus = new MeetingsBUS();
            List<IModel> gm = new List<IModel>();
            gm = bus.GetMeetingCategories(Login._user.lngUser);

            var dlgSave = new GridLookupForm(gm, "Categories");

            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {                
                MeetingsCategoryModel m = new MeetingsCategoryModel();
                m = (MeetingsCategoryModel)dlgSave.selectedRow;
                //set textbox
                txtCategory.Text = m.idMeetingCategory.ToString();
                txtCategoryName.Text = m.categoryDescription;
                //update model
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnPriority_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            MeetingsBUS bus = new MeetingsBUS();
            List<IModel> gm = new List<IModel>();
            gm = bus.GetMeetingPriorities(Login._user.lngUser);

            var dlgSave = new GridLookupForm(gm, "Priority");

            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {
                MeetingsPriorityModel m = new MeetingsPriorityModel();
                m = (MeetingsPriorityModel)dlgSave.selectedRow;
                //set textbox
                txtPriority.Text = m.idPriority.ToString();
                txtPriorityName.Text = m.descriptionPriority;
                //update model
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnStatus_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            MeetingsBUS bus = new MeetingsBUS();
            List<IModel> gm = new List<IModel>();
            gm = bus.GetMeetingStatuses();

            var dlgSave = new GridLookupForm(gm, "Status");

            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {
                MeetingsStatus m = new MeetingsStatus();
                m = (MeetingsStatus)dlgSave.selectedRow;
                //set textbox
                txtStatus.Text = m.idMeetingStatus.ToString();
                txtStatusName.Text = m.desriptionMeetingStatus;
                //update model
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnClient_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            ClientBUS bus = new ClientBUS();
            List<IModel> gm = new List<IModel>();
            gm = bus.GetAllClients(Login._user.lngUser);

            var dlgSave = new GridLookupForm(gm, "Client");

            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {
                ClientModel m = new ClientModel();
                m = (ClientModel)dlgSave.selectedRow;
                //set textbox
                txtClient.Text = m.idClient.ToString();
                txtClientName.Text = m.nameClient;
                //update model
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnContact_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            PersonBUS bus = new PersonBUS();
            List<IModel> gm = new List<IModel>();
            gm = bus.GetPersonsNoFilter();

            var dlgSave = new GridLookupForm(gm, "Person");

            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {
                PersonModel m = new PersonModel();
                m = (PersonModel)dlgSave.selectedRow;
                //set textbox
                txtContact.Text = m.idContPers.ToString() ;
                txtContactName.Text = m.fullname;
                //update model
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnOwner_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            EmployeeBUS bus = new EmployeeBUS();
            List<IModel> gm = new List<IModel>();
            gm = bus.GetAllEmployees(0,null,Login._user.lngUser);

            var dlgSave = new GridLookupForm(gm, "Employee");

            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {
                EmployeeModel m = new EmployeeModel();
                m = (EmployeeModel)dlgSave.selectedRow;
                //set textbox
                txtOwner.Text = m.idEmployee.ToString();
                txtOwnerName.Text = m.firstNameEmployee + " " + m.lastNameEmployee;
                //update model
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnResponsible_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            EmployeeBUS bus = new EmployeeBUS();
            List<IModel> gm = new List<IModel>();
            gm = bus.GetAllEmployees(0, null, Login._user.lngUser);

            var dlgSave = new GridLookupForm(gm, "Employee");

            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {
                EmployeeModel m = new EmployeeModel();
                m = (EmployeeModel)dlgSave.selectedRow;
                //set textbox
                txtResponsible.Text = m.idEmployee.ToString();
                txtResponsibleName.Text = m.firstNameEmployee + " " + m.lastNameEmployee;
                //update model
            }
            Cursor.Current = Cursors.Default;
        }

        private void cmbBackground_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {           
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            AppointmentsBUS appBus = new AppointmentsBUS();

            bool b = appBus.ChechIfAppointmentExist(GuidCheck);
            if(b == false)
            {
                //insert new
                try
                {                    
                    BISAppointment appModel = new BISAppointment();
                    appModel.Id = GuidCheck;
                    appModel.Subject = txtSubject.Text;
                    appModel.Location = txtLocation.Text;
                    appModel.Background = cmbBackground.SelectedItem.Text.Trim();
                    appModel.ShowTime = cmbShowTimeAs.SelectedItem.Text.ToString();
                    string dts = dateStart.Value.ToString();
                    appModel.Start = DateTime.Parse(dateStart.Value.ToString());
                    appModel.End = DateTime.Parse(dateEnd.Value.ToString());
                    appModel.Category = Int32.Parse(txtCategory.Text);
                    appModel.Priority = Int32.Parse(txtPriority.Text);
                    appModel.Status = Int32.Parse(txtStatus.Text);
                    appModel.Owner = Int32.Parse(txtOwner.Text);
                    appModel.Client = Int32.Parse(txtClient.Text);
                    appModel.Contact = Int32.Parse(txtContact.Text);
                    appModel.Project = Int32.Parse(txtProject.Text);
                    appModel.Responsible = Int32.Parse(txtResponsible.Text);
                    appModel.Description = textBoxDescription.Text;
                    appModel.IsAllDay = chkAllDay.Checked;

                    appModel.CategoryName = txtCategoryName.Text;
                    appModel.PriorityName = txtPriorityName.Text;
                    appModel.StatusName = txtStatusName.Text;
                    appModel.OwnerName = txtOwnerName.Text;
                    appModel.ClientName = txtClientName.Text;
                    appModel.ContactName = txtContactName.Text;
                    appModel.ProjectName = txtProjectName.Text;
                    appModel.ResponsibleName = txtResponsibleName.Text;
                    appModel.IdArrangement = Int32.Parse(txtArrangement.Text);
                    appModel.NameArrangement = txtNameArrangement.Text; 

                    if (radDropDownListReminder.SelectedValue != null)
                    {
                        Type t = radDropDownListReminder.SelectedValue.GetType();
                        if (t == typeof(TimeSpan))
                        {
                            string s = radDropDownListReminder.SelectedValue.ToString();
                            TimeSpan sp = TimeSpan.Parse(s);
                            appModel.Reminder = new TimeSpan(sp.Hours, sp.Minutes, sp.Seconds);
                        }
                    }
                    else
                    {
                        appModel.Reminder = null;
                        
                    }                    
                    appBus.Save(appModel, this.Name, Login._user.idUser);
                
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }

            }
            else
            {
                //update existing
                try
                {
                    BISAppointment appModel = new BISAppointment();
                    appModel.Id = GuidCheck;
                    appModel.Subject = txtSubject.Text;
                    appModel.Location = txtLocation.Text;
                    appModel.Background = cmbBackground.SelectedItem.Text.Trim();
                    appModel.ShowTime = cmbShowTimeAs.SelectedItem.Text.ToString();
                    string dts = dateStart.Value.ToString();
                    //appModel.Start = DateTime.Parse(dateStart.Value.TOS.ToString());
                    //appModel.End = DateTime.Parse(dateEnd.Value.ToString());
                    appModel.Start = DateTime.Parse(dateStart.Value.ToShortDateString() + " " + timeStart.Value.ToShortTimeString());
                    appModel.End = DateTime.Parse(dateEnd.Value.ToShortDateString() + " " + timeEnd.Value.ToShortTimeString());

                    appModel.Category = Int32.Parse(txtCategory.Text);
                    appModel.Priority = Int32.Parse(txtPriority.Text);
                    appModel.Status = Int32.Parse(txtStatus.Text);
                    appModel.Owner = Int32.Parse(txtOwner.Text);
                    appModel.Client = Int32.Parse(txtClient.Text);
                    appModel.Contact = Int32.Parse(txtContact.Text);
                    appModel.Project = Int32.Parse(txtProject.Text);
                    appModel.Responsible = Int32.Parse(txtResponsible.Text);
                    appModel.Description = textBoxDescription.Text;
                    appModel.IsAllDay = chkAllDay.Checked;

                    appModel.CategoryName = txtCategoryName.Text;
                    appModel.PriorityName = txtPriorityName.Text;
                    appModel.StatusName = txtStatusName.Text;
                    appModel.OwnerName = txtOwnerName.Text;
                    appModel.ClientName = txtClientName.Text;
                    appModel.ContactName = txtContactName.Text;
                    appModel.ProjectName = txtProjectName.Text;
                    appModel.ResponsibleName = txtResponsibleName.Text;
                    appModel.IdArrangement = Int32.Parse(txtArrangement.Text);
                    appModel.NameArrangement = txtNameArrangement.Text;

                    if (radDropDownListReminder.SelectedValue != null)
                    {
                        Type t = radDropDownListReminder.SelectedValue.GetType();
                        if(t == typeof(TimeSpan))                        
                            appModel.Reminder = TimeSpan.Parse(radDropDownListReminder.SelectedValue.ToString());
                    }
                    else
                    {
                        appModel.Reminder = null;
                        
                    }

                    appBus.Update(appModel, this.Name, Login._user.idUser);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }            
        }

        private  void buttonDelete_Click(object sender, EventArgs e)
        {
            
            //DialogResult dr = MessageBox.Show("Delete appointment: " + txtSubject.Text + " ?","",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            //if(dr == DialogResult.Yes)
            //{
                 AppointmentsBUS appBus = new AppointmentsBUS();
                 appBus.Delete(GuidCheck, this.Name, Login._user.idUser);
                
            //}            
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            //if (radDropDownListReminder.SelectedValue != null)
              //  MessageBox.Show(radDropDownListReminder.SelectedValue.ToString());
        }

        
        private void setTranslation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(labelCategory.Text) != null)
                labelCategory.Text = resxSet.GetString(labelCategory.Text);

                if (resxSet.GetString(labelClient.Text) != null)
                labelClient.Text = resxSet.GetString(labelClient.Text);

                if (resxSet.GetString(labelContact.Text) != null) 
                    labelContact.Text = resxSet.GetString(labelContact.Text);

                if (resxSet.GetString(labelProject.Text) != null)
                labelProject.Text = resxSet.GetString(labelProject.Text);

                if (resxSet.GetString(labelResponsible.Text) != null)
                labelResponsible.Text = resxSet.GetString(labelResponsible.Text);

                if (resxSet.GetString(labelPriority.Text) != null)
                labelPriority.Text = resxSet.GetString(labelPriority.Text);

                if (resxSet.GetString(labelStatusC.Text) != null)
                labelStatusC.Text = resxSet.GetString(labelStatusC.Text);

                if (resxSet.GetString(labelOwner.Text) != null)
                labelOwner.Text = resxSet.GetString(labelOwner.Text);

                if (resxSet.GetString(lblArrangement.Text)!=null)
                lblArrangement.Text = resxSet.GetString(lblArrangement.Text);
            }
        }

        private void btnProject_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Cursor.Current = Cursors.Default;
        }

        private void btnArrangement_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            ArrangementBUS bus = new ArrangementBUS();
            List<IModel> gm = new List<IModel>();
            gm = bus.GetAllArrangements();

            var dlgSave = new GridLookupForm(gm, "Arrangement");

            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {
                ArrangementModel m = new ArrangementModel();
                m = (ArrangementModel)dlgSave.selectedRow;
                //set textbox
                txtArrangement.Text = m.idArrangement.ToString();
                txtNameArrangement.Text = m.nameArrangement;
                idArr = m.idArrangement;
                nameArr = m.nameArrangement;
                //update model
            }
            Cursor.Current = Cursors.Default;
        }
    }

    public class AppointmentWithEmail : Appointment
    {
        public AppointmentWithEmail()
            : base()
        {
        }
        private string email = string.Empty;
        private int category = 0;
        private string categoryName = string.Empty;
        private int priority = 0;
        private string priorityName = string.Empty;
        private int status = 0;
        private string statusName = string.Empty;
        private int client = 0;
        private string clientName = string.Empty;
        private int contact = 0;
        private string contactName = string.Empty;
        private int project = 0;
        private string projectName = string.Empty;
        private int owner = 0;
        private string ownerName = string.Empty;
        private int responsible = 0;
        private string responsibleName = string.Empty;
        private int idArrangement = 0;
        private string nameArrangement = string.Empty;

        public int Category
        {
            get
            {
                return this.category;                
            }
            set
            {
                if (this.category != value)
                {
                    this.category = value;
                    this.OnPropertyChanged("Category");
                }
            }
        }
        public string CategoryName
        {
            get
            {
                return this.categoryName;
            }
            set
            {
                if (this.categoryName != value)
                {
                    this.categoryName = value;
                    this.OnPropertyChanged("CategoryName");
                }
            }
        }

        public int Priority
        {
            get
            {
                return this.priority;
            }
            set
            {
                if (this.priority != value)
                {
                    this.priority = value;
                    this.OnPropertyChanged("Priority");
                }
            }
        }

        public string PriorityName
        {
            get
            {
                return this.priorityName;
            }
            set
            {
                if (this.priorityName != value)
                {
                    this.priorityName = value;
                    this.OnPropertyChanged("PriorityName");
                }
            }
        }

        public int Status
        {
            get
            {
                return this.status;
            }
            set
            {
                if (this.status != value)
                {
                    this.status = value;
                    this.OnPropertyChanged("Status");
                }
            }
        }

        public string StatusName
        {
            get
            {
                return this.statusName;
            }
            set
            {
                if (this.statusName != value)
                {
                    this.statusName = value;
                    this.OnPropertyChanged("StatusName");
                }
            }
        }

        public int Client
        {
            get
            {
                return this.client;
            }
            set
            {
                if (this.client != value)
                {
                    this.client = value;
                    this.OnPropertyChanged("Client");
                }
            }
        }

        public string ClientName
        {
            get
            {
                return this.clientName;
            }
            set
            {
                if (this.clientName != value)
                {
                    this.clientName = value;
                    this.OnPropertyChanged("ClientName");
                }
            }
        }

        public int Contact
        {
            get
            {
                return this.contact;
            }
            set
            {
                if (this.contact != value)
                {
                    this.contact = value;
                    this.OnPropertyChanged("Contact");
                }
            }
        }

        public string ContactName
        {
            get
            {
                return this.contactName;
            }
            set
            {
                if (this.contactName != value)
                {
                    this.contactName = value;
                    this.OnPropertyChanged("ContactName");
                }
            }
        }

        public int Project
        {
            get
            {
                return this.project;
            }
            set
            {
                if (this.project != value)
                {
                    this.project = value;
                    this.OnPropertyChanged("Project");
                }
            }
        }

        public string ProjectName
        {
            get
            {
                return this.projectName;
            }
            set
            {
                if (this.projectName != value)
                {
                    this.projectName = value;
                    this.OnPropertyChanged("ProjectName");
                }
            }
        }

        public int Owner
        {
            get
            {
                return this.owner;
            }
            set
            {
                if (this.owner != value)
                {
                    this.owner = value;
                    this.OnPropertyChanged("Owner");
                }
            }
        }

        public string OwnerName
        {
            get
            {
                return this.ownerName;
            }
            set
            {
                if (this.ownerName != value)
                {
                    this.ownerName = value;
                    this.OnPropertyChanged("OwnerName");
                }
            }
        }

        public int Responsible
        {
            get
            {
                return this.responsible;
            }
            set
            {
                if (this.responsible != value)
                {
                    this.responsible = value;
                    this.OnPropertyChanged("Responsible");
                }
            }
        }

        public string ResponsibleName
        {
            get
            {
                return this.responsibleName;
            }
            set
            {
                if (this.responsibleName != value)
                {
                    this.responsibleName = value;
                    this.OnPropertyChanged("ResponsibleName");
                }
            }
        }
        public int IdArrangement
        {
            get
            {
                return this.idArrangement;
            }
            set
            {
                if (this.idArrangement != value)
                {
                    this.idArrangement = value;
                    this.OnPropertyChanged("IdArrangement");
                }
            }
        }
        public string Arrangement
        {
            get
            {
                return this.nameArrangement;
            }
            set
            {
                if (this.nameArrangement != value)
                {
                    this.nameArrangement = value;
                    this.OnPropertyChanged("NameArrangement");
                }
            }
        }

    }

    public class CustomAppointmentFactory : IAppointmentFactory
    {
        #region IAppointmentFactory Members
        public IEvent CreateNewAppointment()
        {
            return new AppointmentWithEmail();
        }
        #endregion
    }
   
}
