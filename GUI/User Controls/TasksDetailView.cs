using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BIS.Business;
using BIS.Model;
using Telerik.WinControls.Data;

namespace GUI
{
    public partial class TasksDetailView : UserControl
    {
        private int selectedPriorityID;
        private int selectedStatusID;
        private int selectedTypeID;
        private bool pageLoaded = false;

        public TasksDetailView()
        {
            InitializeComponent();
        }

        private void TasksDetailView_Load(object sender, EventArgs e)
        {
            // Fill Priority dropdown
            ToDoPriorityBUS tpbus = new ToDoPriorityBUS();
            List<ToDoPriorityModel> priorList = tpbus.GetAllToDoPriority(Login._user.lngUser);
            ToDoPriorityModel pm = new ToDoPriorityModel();
            pm.idPriorityToDo = 0;
            pm.descriptionPriority = "All";
            priorList.Add(pm);

            radDropDownPriority.DataSource = priorList;
            radDropDownPriority.ValueMember = "idPriorityToDo";
            radDropDownPriority.DisplayMember = "descriptionPriority";

            // FIll dropdown status

            ToDoStatusBUS tsbus = new ToDoStatusBUS();
            List<ToDoStatusModel> statusList = tsbus.GetAllToDoStatus(Login._user.lngUser);
            ToDoStatusModel sm =  new ToDoStatusModel();
            sm.idStatusToDo = 0;
            sm.descriptionStatus = "All";
            statusList.Add(sm);

            radDropDownListStatus.DataSource = statusList;
            radDropDownListStatus.ValueMember = "idStatusToDo";
            radDropDownListStatus.DisplayMember = "descriptionStatus";

            // Fill dropdown types

            ToDoTypeBUS tybus = new ToDoTypeBUS();
            List<ToDoTypeModel> typesList = tybus.GetAllToDoTypes(Login._user.lngUser);
            ToDoTypeModel ts = new ToDoTypeModel();
            ts.idToDoType = 0;
            ts.descriptionToDoType = "All";
            typesList.Add(ts);

            radDropDownType.DataSource = typesList;
            radDropDownType.ValueMember = "idToDoType";
            radDropDownType.DisplayMember = "descriptionToDoType";


            radDropDownPriority.SelectedValue = 0;
            radDropDownListStatus.SelectedValue = 0;
            radDropDownType.SelectedValue = 0;
            selectedPriorityID = 0;
            selectedStatusID = 0;
            selectedTypeID = 0;

            // Fill list
            listTasks.Columns.Add("ID");
            listTasks.Columns.Add("Date End");
            listTasks.Columns.Add("Description");
            listTasks.Columns.Add("personid");
            listTasks.Columns.Add("idClient");
            listTasks.Columns.Add("idContact");
            listTasks.Columns.Add("idProject");
            

            listTasks.Columns["ID"].Visible = false;          
            listTasks.Columns["Date End"].Width = 70;
            listTasks.Columns["personid"].Visible = false;
            listTasks.Columns["idClient"].Visible = false;
            listTasks.Columns["idContact"].Visible = false;
            listTasks.Columns["idProject"].Visible = false;

            SortDescriptor descriptor = new SortDescriptor();
            descriptor.PropertyName = "Description";
            descriptor.Direction = ListSortDirection.Descending;
            listTasks.SortDescriptors.Add(descriptor);

            listTasks.EnableSorting = true;
            listTasks.EnableColumnSort = true;

            //radSplitContainerCombos.Height = radDropDownType.Height;

            //PopulateData(Login._user.idEmployee);
            PopulateDataTypes(Login._user.idEmployee, selectedPriorityID, selectedStatusID, selectedTypeID);

            pageLoaded = true;
        }

        public void PopulateDataTypes(int param, int priority, int status, int type)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                // -1 ako se ucitavaju svi takovi
                // > 0 po employee ID-u
                List<ToDoModel> lista = null;

                if (param != -1)
                {
                    ToDoBUS todoBUS = new ToDoBUS();
                    // lista = todoBUS.GetToDoEmployee(param);
                    lista = todoBUS.GetToDoEmployeeByType(param, priority, status, type);
                }
                else
                {
                    // ako je selektovano all apppointments
                    ToDoBUS todoBUS = new ToDoBUS();
                    lista = todoBUS.GetToDoALL();
                }

                listTasks.Items.Clear();
                if (lista != null)
                {
                    foreach (ToDoModel td in lista)
                    {
                        listTasks.Items.Add(td.idToDo, td.dtEndDate.ToShortDateString(), td.descriptionToDo, td.idContPers, td.idClient, td.idContact, td.idProject);
                    }
                }
            }catch(Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
                
            }

            Cursor.Current = Cursors.Default;
        }

        private void radDropDownPriority_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if(pageLoaded == true)
            {
                selectedPriorityID = (int) radDropDownPriority.SelectedValue;
                PopulateDataTypes(Login._user.idEmployee, selectedPriorityID, selectedStatusID, selectedTypeID);
            }
        }

        private void radDropDownListStatus_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (pageLoaded == true)
            {
                selectedStatusID = (int)radDropDownListStatus.SelectedValue;
                PopulateDataTypes(Login._user.idEmployee, selectedPriorityID, selectedStatusID, selectedTypeID);
            }
        }

        private void radDropDownType_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (pageLoaded == true)
            {
                selectedTypeID = (int)radDropDownType.SelectedValue;
                PopulateDataTypes(Login._user.idEmployee, selectedPriorityID, selectedStatusID, selectedTypeID);
            }
        }

        private void listTasks_ItemMouseDoubleClick(object sender, Telerik.WinControls.UI.ListViewItemEventArgs e)
        {
            try
            {
                string what = "open";
                string stype = "List ToDo";
                int id = (int)e.Item["ID"];
                int personID = (int)e.Item["personid"];
                int clientID = (int)e.Item["idClient"];
                int contactID = (int)e.Item["idContact"];
                int projectID = (int)e.Item["idProject"];

                frmTasks frmTask = new frmTasks(id, personID, what, stype, clientID, contactID);
                frmTask.ShowDialog();

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
