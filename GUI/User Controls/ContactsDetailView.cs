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


namespace GUI
{
    public partial class ContactsDetailView : UserControl
    {
        private int selectedReasonID;
        private int selectedTypeID;
        private bool pageLoaded = false;

        public ContactsDetailView()
        {
            InitializeComponent();
        }

        private void ContactsDetailView_Load(object sender, EventArgs e)
        {
            // FIll dropdown reason

            ContactReasonBUS tsbus = new ContactReasonBUS();
            List<ContactReasonModel> reasonList = tsbus.GetAllContactReason(Login._user.lngUser);
            ContactReasonModel sm = new ContactReasonModel();
            sm.idContactReason = 0;
            sm.descContactReason = "All";
            reasonList.Add(sm);

            radDropDownListReason.DataSource = reasonList;
            radDropDownListReason.ValueMember = "idContactReason";
            radDropDownListReason.DisplayMember = "descContactReason";

            // Fill dropdown types

            ContactTypeBUS tybus = new ContactTypeBUS();
            List<ContactTypeModel> typesList = tybus.GetAllContactType(Login._user.lngUser);
            ContactTypeModel ts = new ContactTypeModel();
            ts.idContactType = 0;
            ts.descContactType = "All";
            typesList.Add(ts);

            radDropDownListType.DataSource = typesList;
            radDropDownListType.ValueMember = "idContactType";
            radDropDownListType.DisplayMember = "descContactType";

            radDropDownListReason.SelectedValue = 0;
            radDropDownListType.SelectedValue = 0;

            selectedReasonID = 0;           
            selectedTypeID = 0;




            listContacts.Columns.Add("ID");
            listContacts.Columns.Add("Date Contact");
            listContacts.Columns.Add("Reason");
            listContacts.Columns.Add("idContPerson");
            listContacts.Columns.Add("idClient");

            listContacts.Columns["ID"].Visible = false;
            listContacts.Columns["idContPerson"].Visible = false;
            listContacts.Columns["idClient"].Visible = false;
            listContacts.Columns["Date Contact"].Width = 70;

            listContacts.EnableSorting = true;
            listContacts.EnableColumnSort = true;

            PopulateDataTypes(Login._user.idEmployee, selectedReasonID, selectedTypeID);

            pageLoaded = true;
        }

        public void PopulateDataTypes(int param, int reason, int type)
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                // -1 ako se ucitavaju svi takovi
                // > 0 po employee ID-u
                List<ContactsModel> lista = null;

                if (param != -1)
                {
                    ContactsBUS conBUS = new ContactsBUS();
                    lista = conBUS.GetContactsByCreatorTypes(param, selectedReasonID, selectedTypeID);
                }
                else
                {
                    // ako je selektovano all apppointments
                    ContactsBUS conBUS = new ContactsBUS();
                    lista = conBUS.GetContactsALL();
                }

                listContacts.Items.Clear();
                if (lista != null)
                {
                    foreach (ContactsModel td in lista)
                    {
                        listContacts.Items.Add(td.idContact, td.dateContact.ToShortDateString(), td.reasonContact, td.idContPers, td.idClient);
                    }
                }
            }catch(Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }

            Cursor.Current = Cursors.Default;
        }

        private void listContacts_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void radDropDownListReason_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (pageLoaded == true)
            {
                selectedReasonID = (int)radDropDownListReason.SelectedValue;
                PopulateDataTypes(Login._user.idEmployee, selectedReasonID, selectedTypeID);
            }
        }

        private void radDropDownListType_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (pageLoaded == true)
            {
                selectedTypeID = (int)radDropDownListType.SelectedValue;
                PopulateDataTypes(Login._user.idEmployee, selectedReasonID, selectedTypeID);
            }
        }

        private void listContacts_ItemMouseDoubleClick(object sender, Telerik.WinControls.UI.ListViewItemEventArgs e)
        {
            try
            {
                //MessageBox.Show(e.Item["ID"].ToString());
                string what = "open";
                string stype = "List Contacts";
                int id = (int)e.Item["ID"];
                int personID = (int)e.Item["idContPerson"];
                int clientID = (int)e.Item["idClient"];

                frmContacts frmContact = new frmContacts(id, what, stype, clientID, personID);
                frmContact.ShowDialog();

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
