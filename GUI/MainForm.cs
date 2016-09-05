using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Text;
using Telerik.WinControls.UI;
using Telerik.WinForms;
using Telerik.Windows;
using Telerik.Collections;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using BIS.Business;
using BIS.Model;
using System.Configuration;
using Telerik.WinControls;
using BIS.Core;
using System.Reflection;
using System.Resources;
using Telerik.WinControls.UI.Localization;
using GUI;
using GUI.User_Controls;
using Telerik.WinControls.UI.Docking;

namespace GUI
{     
    public partial class MainForm : Telerik.WinControls.UI.RadForm
    {
        // Sadrze putanje do folder-a za filtere na svim gridovima na stranicama - Shimmy
        public static string gridFiltersFolder;
        //putanja do odredjenih filtera za proveru da li vec postoji na savedialoglayout
        public static string gridCustomFilter;
        public static string myEmailFolder;
        public static string DocumentsFolder;
        public static string TemplatesFolder;

        RadTreeView rl ;  // general tree menu 
        private RadTreeView portalTreeMenu = new RadTreeView(); // tree menu za portal
        private RadTreeView accountingTreeMenu = new RadTreeView(); // tree menu za accounting


        //customLayoutFilterDirectory
        List<string> folders;
        
        // Data source za grid.
        public List<IModel> modelData;

        // activeGrid referencira aktivan grid
        public IBISGrid activeGrid;

        //meeting scheduler
        public static MeetingSchedluer_uc meetingScheduler;
        TasksDetailView taskDetailView;
        ContactsDetailView contactsDetailView;
        
        

        // Lista filtera
        public List<FilterModel> filters;

        //Lista labela (ID)
        public static List<int> idLabelList;

        //id selektovanog filtera
        private string selectedFilter = "0";
        
        //Prvi node u tree view (kod cuvanja filtera) se ucitavo duplo. Otklonjeno ovom varijablom.
        private Boolean isSaveLayoutDialogClicked = false;

        // book Year
        BookYear  bookyear;
        // Grid persons sa pratecim kontrolama
        GridViewPersons gridViewPerson;
        PersonDetailView personDetailView;
        ClientDetailView clientDetailView;

        // Reports Management Control
        ReportsManagmentPanel reportsManagementPanel;

        // Grid clients
        GridViewClients gridViewClients;

        // Grid users
        GridViewUsers gridViewUsers;

        // Grid medical
        GridViewMedical gridViewMedical;

        // Grid voluntary
        GridViewVoluntary gridViewVoluntary;

        // Grid voluntaryFunction
        GridViewVoluntaryFunc gridViewVoluntaryFunc;

        // Grid voluntaryTrip
        GridViewVoluntaryTrip gridViewVoluntaryTrip;

        // Grid Employee
        GridViewEmployee gridViewEmployee;

        // Grif Filter Label
        GridViewFilterLabel gridViewFilterLabel;

        // Grid Ledger Account
        GridViewLedger gridViewLedger;
        //Grid Type
        GridViewType gridViewType;
        // Grid Ledger Class
        GridViewAccountClass gridClass;
        //Grid Cost
        GridViewCost gridViewCost;
        // Grid Translation
        GridViewTranslation gridViewTranslation;
        //Grid Tax
        GridViewTax gridViewTax;
        //Grid Daily
        GridViewDaily gridViewDaily;
        //Grid Daily Entry
        GridViewDailyEntry gridViewDailyEntry;
        //Grid Layouts
        GridViewLayouts gridViewLayouts;
        //Grid Roles
        GridViewRoles gridViewRoles;
        //Grid Menus
        GridViewMenus gridViewMenus;
        //Grid Country
        GridViewCountry gridViewCountry;
        //Grid Account settings
        GridViewAccSettings gridViewAccSettings;
        
        //Grid Account payments
        GridViewPayments gridViewPayments;

        GridViewInvoice gridViewInvoice;

        //Grid Travel data
        GridTravelData gridTravelData;
        //Grid Reason
        GridReason gridViewReason;

        //===================================== Arrangement section =========
        //Grid Arrangement
        GridViewArrangement gridViewArrangement;
        ArrangementBookDetailView arrdetailview;
        //Grid Artikal groups
        GridViewArticalGroups gridViewArticalGroups;
        //Grid Artical
        GridViewArtical gridViewArtical;
        //Grid Multimedia
        GridViewMultimedia gridViewMultimedia;
        //Grid MultimediaServer
        GridViewMultimediaServer gridViewMultimediaServer;
        //Grid Booking
        //GridViewBooking gridViewBooking;

        ////Grid voluntary booking
        //GridViewBookingVoluntary gridViewBookingVoluntary;

        //Grid Arrangement Insurance
        GridViewArrangementInsurance gridviewArrangementInsurance;
        GridViewArrangementInsurancePremie gridviewArrangementInsurancePremie;

        //Grid Hotel services
        GridHotelServices gridViewHotelservices;
        //Grid Type
        GridAgeCategory gridViewAgeCategory;
        //Grid Period
        GridViewPeriod gridViewPeriod;


        private int selectedMenuItem = 0;
        Color selectedMenuItemColor = Color.LightGray;
        Color backgroundMenuItemColor = Color.Transparent;

        // langugaeVariables
        public string lngFavorites;
        public string lngFavoritesAll;
        public string lngCustomFilters;
        private List<FilterModel> fModel;
        private List<LabelModel> lmodel;


        public void initTripCalculation()
        {
        }

        public void initArrangementInsurance()
        { 
               
            gridviewArrangementInsurance = new GridViewArrangementInsurance();      
            //event za grid ako treba dodati ovde
            gridviewArrangementInsurance.Dock = System.Windows.Forms.DockStyle.Fill;

            activeGrid = gridviewArrangementInsurance;
            setStandardFilterIfExist();
            modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
            activeGrid.SetDataPersonBinding(modelData);
        }
        public void initPeriod()
        {
            //Inizijalizacija GRID Period
            gridViewPeriod = new GridViewPeriod();
            //  gridTravelData.TravelDataStatusSelectedRowchanged += TravelData_HandleRowSelectChanged;
            gridViewPeriod.Dock = System.Windows.Forms.DockStyle.Fill;

            activeGrid = gridViewPeriod;
            setStandardFilterIfExist();
            modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
            activeGrid.SetDataPersonBinding(modelData);

        }
        public void initReason()
        {
            //Inizijalizacija GRID Reason
            gridViewReason = new GridReason();
            gridViewReason.ReasonStatusSelectedRowchanged += Reason_HandleRowSelectChanged;
            gridViewReason.Dock = System.Windows.Forms.DockStyle.Fill;

            activeGrid = gridViewReason;
            setStandardFilterIfExist();
            modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
            activeGrid.SetDataPersonBinding(modelData);

        }
        public void initinitArrangementInsurancePremie()
        {
            gridviewArrangementInsurancePremie = new GridViewArrangementInsurancePremie();
            //event za grid ako treba dodati ovde
            gridviewArrangementInsurancePremie.Dock = System.Windows.Forms.DockStyle.Fill;

            activeGrid = gridviewArrangementInsurancePremie;
            setStandardFilterIfExist();
            modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
            activeGrid.SetDataPersonBinding(modelData);
        }
    

        public void initInvoice()
        {
            if (gridViewInvoice == null)
            {
                gridViewInvoice = new GridViewInvoice();
                //  gridViewArrangement.ArrangementStatusSelectedRowchanged += Arrangement_HandleRowSelectChanged;
                gridViewInvoice.Dock = System.Windows.Forms.DockStyle.Fill;

                activeGrid = gridViewInvoice;
                setStandardFilterIfExist();
                modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
                activeGrid.SetDataPersonBinding(modelData);
            }
        }
        public void initAgeCategory()
        {
            //Inizijalizacija GRID AgeCategory
            gridViewAgeCategory = new GridAgeCategory();
            //  gridTravelData.TravelDataStatusSelectedRowchanged += TravelData_HandleRowSelectChanged;
            gridViewAgeCategory.Dock = System.Windows.Forms.DockStyle.Fill;

            activeGrid = gridViewAgeCategory;
            setStandardFilterIfExist();
            modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
            activeGrid.SetDataPersonBinding(modelData);

        }

        public void initHotelServices()
        {
            //Inizijalizacija GRID Type
            gridViewHotelservices = new GridHotelServices();
            //  gridTravelData.TravelDataStatusSelectedRowchanged += TravelData_HandleRowSelectChanged;
            gridViewHotelservices.Dock = System.Windows.Forms.DockStyle.Fill;

            activeGrid = gridViewHotelservices;
            setStandardFilterIfExist();
            modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
            activeGrid.SetDataPersonBinding(modelData);

        }

        public void initLayouts()
        {
            radRibbonBarGroupBoomarks.Visibility = ElementVisibility.Visible;
            radButtonCreateBookmark.Visibility = ElementVisibility.Visible;
            radButtonDeleteBookmark.Visibility = ElementVisibility.Visible;

            gridViewLayouts = new GridViewLayouts();
            gridViewLayouts.layoutsRowSelectChanged += Layouts_HandleRowSelectChanged;
            gridViewLayouts.Dock = System.Windows.Forms.DockStyle.Fill;

            activeGrid = gridViewLayouts;
            setStandardFilterIfExist();
            modelData = activeGrid.GetData(0, null, "");
            activeGrid.SetDataPersonBinding(modelData);

        }
        public void initPayments()
        {
            if (gridViewPayments == null)
            {
                gridViewPayments = new GridViewPayments();
                //  gridViewArrangement.ArrangementStatusSelectedRowchanged += Arrangement_HandleRowSelectChanged;
                gridViewPayments.Dock = System.Windows.Forms.DockStyle.Fill;

                activeGrid = gridViewPayments;
                setStandardFilterIfExist();
                modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
                activeGrid.SetDataPersonBinding(modelData);
            }

        }

        public void initSaleBooking()
        {
            if (gridViewArrangement == null)
            {
                gridViewArrangement = new GridViewArrangement();
                //  gridViewArrangement.ArrangementStatusSelectedRowchanged += Arrangement_HandleRowSelectChanged;
                gridViewArrangement.Dock = System.Windows.Forms.DockStyle.Fill;

                activeGrid = gridViewArrangement;
                setStandardFilterIfExist();
                modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
                activeGrid.SetDataPersonBinding(modelData);
                
            }
            arrdetailview = new ArrangementBookDetailView();
            gridViewArrangement.ArrangementSelectedRowchanged += ArrangementBook_HandleRowSelectChanged;
            
        }
        public void initVoluntaryBooking()
        {
            if (gridViewArrangement == null)
            {
                gridViewArrangement = new GridViewArrangement();
                //  gridViewArrangement.ArrangementStatusSelectedRowchanged += Arrangement_HandleRowSelectChanged;
                gridViewArrangement.Dock = System.Windows.Forms.DockStyle.Fill;

                activeGrid = gridViewArrangement;
                setStandardFilterIfExist();
                modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
                activeGrid.SetDataPersonBinding(modelData);
                //gridViewArrangement.ArrangementSelectedRowchanged += ArrangementBook_HandleRowSelectChanged;
            }
        }

        public void initTravelData()
        {
            //Inizijalizacija GRID Type
            gridTravelData = new GridTravelData();
          //  gridTravelData.TravelDataStatusSelectedRowchanged += TravelData_HandleRowSelectChanged;
            gridTravelData.Dock = System.Windows.Forms.DockStyle.Fill;

            activeGrid = gridTravelData;
            setStandardFilterIfExist();
            modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
            activeGrid.SetDataPersonBinding(modelData);

        }

        public void initArr()
        {
            //Inizijalizacija GRID USERS
            gridViewArrangement = new GridViewArrangement();
            //  gridViewArrangement.ArrangementStatusSelectedRowchanged += Arrangement_HandleRowSelectChanged;
            gridViewArrangement.Dock = System.Windows.Forms.DockStyle.Fill;

            activeGrid = gridViewArrangement;
            setStandardFilterIfExist();
            modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
            activeGrid.SetDataPersonBinding(modelData);
            //gridViewArrangement.ArrangementSelectedRowchanged += ArrangementBook_HandleRowSelectChanged;
        }

        public void initTourleading()
        {
            if (gridViewArrangement == null)
            {
                gridViewArrangement = new GridViewArrangement();
                //  gridViewArrangement.ArrangementStatusSelectedRowchanged += Arrangement_HandleRowSelectChanged;
                gridViewArrangement.Dock = System.Windows.Forms.DockStyle.Fill;

                activeGrid = gridViewArrangement;
                setStandardFilterIfExist();
                modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
                activeGrid.SetDataPersonBinding(modelData);
            }
        }

        public void initCountry()
        {
        
            gridViewCountry = new GridViewCountry();
            //  gridViewArrangement.ArrangementStatusSelectedRowchanged += Arrangement_HandleRowSelectChanged;
            gridViewCountry.Dock = System.Windows.Forms.DockStyle.Fill;

            activeGrid = gridViewCountry;
            setStandardFilterIfExist();
            modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
            activeGrid.SetDataPersonBinding(modelData);
        }

        public void initAccSettings()
        {

            gridViewAccSettings = new GridViewAccSettings();
            //  gridViewArrangement.ArrangementStatusSelectedRowchanged += Arrangement_HandleRowSelectChanged;
            gridViewAccSettings.Dock = System.Windows.Forms.DockStyle.Fill;

            activeGrid = gridViewAccSettings;
            setStandardFilterIfExist();
            modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
            activeGrid.SetDataPersonBinding(modelData);
        }

        public void initArticals()
        {
            gridViewArtical = new GridViewArtical();
            gridViewArtical.Dock = System.Windows.Forms.DockStyle.Fill;

            activeGrid = gridViewArtical;
            setStandardFilterIfExist();
            modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
            activeGrid.SetDataPersonBinding(modelData);
        }
        public void initMultimediaServer()
        {
            gridViewMultimediaServer = new GridViewMultimediaServer();
            gridViewMultimediaServer.Dock = System.Windows.Forms.DockStyle.Fill;

            activeGrid = gridViewMultimediaServer;
            setStandardFilterIfExist();
            modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
            activeGrid.SetDataPersonBinding(modelData);
        }

        public void initPerson()
        {
            //Inizijalizacija GRID PERSONS
            gridViewPerson = new GridViewPersons();
            gridViewPerson.StatusRowSelectChanged += Person_HandleRowSelectChanged;
            gridViewPerson.Dock = System.Windows.Forms.DockStyle.Fill;
           
            personDetailView = new PersonDetailView();           
                        
            //if (activeGrid == null)
            //{
                activeGrid = gridViewPerson;
                setStandardFilterIfExist();
                modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList,Login._user.lngUser);
                activeGrid.SetDataPersonBinding(modelData);
            //}                       
            
        }
        public void initClient()
        {
            //Inizijalizacija GRID CLIENTS
            gridViewClients = new GridViewClients();
            gridViewClients.ClientStatusSelectedRowchanged += Client_HandleRowSelectChanged;
            gridViewClients.Dock = System.Windows.Forms.DockStyle.Fill;

            clientDetailView = new ClientDetailView();

            activeGrid = gridViewClients;
            setStandardFilterIfExist();
            modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
            activeGrid.SetDataPersonBinding(modelData);

        }
        public void initRoles()
        {
            //Inizijalizacija GRID Roles
            gridViewRoles = new GridViewRoles();
            gridViewRoles.RolesStatusSelectedRowchanged += Roles_HandleRowSelectChanged;
            gridViewRoles.Dock = System.Windows.Forms.DockStyle.Fill;

            activeGrid = gridViewRoles;
            setStandardFilterIfExist();
            modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
            activeGrid.SetDataPersonBinding(modelData);

        }
        public void initMenus()
        {
            //Inizijalizacija GRID Roles
            gridViewMenus = new GridViewMenus();
            gridViewMenus.MenusStatusSelectedRowchanged += Menus_HandleRowSelectChanged;
            gridViewMenus.Dock = System.Windows.Forms.DockStyle.Fill;

            activeGrid = gridViewMenus;
            setStandardFilterIfExist();
            modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
            activeGrid.SetDataPersonBinding(modelData);

        }
         public void initUsers()
        {
            //Inizijalizacija GRID USERS
            gridViewUsers = new GridViewUsers();
            gridViewUsers.UserStatusSelectedRowchanged += Users_HandleRowSelectChanged;
            gridViewUsers.Dock = System.Windows.Forms.DockStyle.Fill;

            activeGrid = gridViewUsers;
            setStandardFilterIfExist();
            modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList,Login._user.lngUser);
            activeGrid.SetDataPersonBinding(modelData);                   
            
        }

         public void initEmployee()
         {             
             gridViewEmployee = new GridViewEmployee();
             //gridViewEmployee.ClientStatusSelectedRowchanged += Client_HandleRowSelectChanged;

             gridViewEmployee.Dock = System.Windows.Forms.DockStyle.Fill;

             activeGrid = gridViewEmployee;
             setStandardFilterIfExist();
             modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
             activeGrid.SetDataPersonBinding(modelData);

         }

         public void initMedical()
         {             
             gridViewMedical = new GridViewMedical();
             //gridViewEmployee.ClientStatusSelectedRowchanged += Client_HandleRowSelectChanged;

             gridViewMedical.Dock = System.Windows.Forms.DockStyle.Fill;

             activeGrid = gridViewMedical;
             setStandardFilterIfExist();
             modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
             activeGrid.SetDataPersonBinding(modelData);

         }

         public void initVoluntary()
         {             
             gridViewVoluntary = new GridViewVoluntary();
             //gridViewEmployee.ClientStatusSelectedRowchanged += Client_HandleRowSelectChanged;

             gridViewVoluntary.Dock = System.Windows.Forms.DockStyle.Fill;

             activeGrid = gridViewVoluntary;
             setStandardFilterIfExist();
             modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
             activeGrid.SetDataPersonBinding(modelData);

         }

         public void initVoluntaryFunc()
         {             
             gridViewVoluntaryFunc = new GridViewVoluntaryFunc();
             //gridViewEmployee.ClientStatusSelectedRowchanged += Client_HandleRowSelectChanged;

             gridViewVoluntaryFunc.Dock = System.Windows.Forms.DockStyle.Fill;

             activeGrid = gridViewVoluntaryFunc;
             setStandardFilterIfExist();
             modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
             activeGrid.SetDataPersonBinding(modelData);

         }
         public void initVoluntaryTrip()
         {             
             gridViewVoluntaryTrip = new GridViewVoluntaryTrip();
             //gridViewEmployee.ClientStatusSelectedRowchanged += Client_HandleRowSelectChanged;

             gridViewVoluntaryTrip.Dock = System.Windows.Forms.DockStyle.Fill;

             activeGrid = gridViewVoluntaryTrip;
             setStandardFilterIfExist();
             modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
             activeGrid.SetDataPersonBinding(modelData);

         }


         public void initLedger()
         {
             //Inizijalizacija GRID USERS
             gridViewLedger = new GridViewLedger();
             //   gridViewLedger.LedgerStatusSelectedRowchanged += Ledger_HandleRowSelectChanged;
             gridViewLedger.Dock = System.Windows.Forms.DockStyle.Fill;

             activeGrid = gridViewLedger;
             setStandardFilterIfExist();
             modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
            // activeGrid.SetDataPersonBinding(modelData);
         }

         public void initAccClass()
         {
             //Inizijalizacija GRID USERS
           //  gridViewAccountClass = new GridViewAccountClass();
             gridClass = new GridViewAccountClass();
             //   gridViewLedger.LedgerStatusSelectedRowchanged += Ledger_HandleRowSelectChanged;
             gridClass.Dock = System.Windows.Forms.DockStyle.Fill;

             activeGrid = gridClass;
             setStandardFilterIfExist();
             modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
             activeGrid.SetDataPersonBinding(modelData);
         }

         public void initCost()
         {
             //Inizijalizacija GRID USERS
             gridViewCost = new GridViewCost();
             //   gridViewLedger.LedgerStatusSelectedRowchanged += Ledger_HandleRowSelectChanged;
             gridViewCost.Dock = System.Windows.Forms.DockStyle.Fill;

             activeGrid = gridViewCost;
             setStandardFilterIfExist();
             modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
             activeGrid.SetDataPersonBinding(modelData);
         }
         public void initTax()
         {
             //Inizijalizacija GRID USERS
             gridViewTax = new GridViewTax();
             //   gridViewLedger.LedgerStatusSelectedRowchanged += Ledger_HandleRowSelectChanged;
             gridViewTax.Dock = System.Windows.Forms.DockStyle.Fill;

             activeGrid = gridViewTax;
             setStandardFilterIfExist();
             modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
             activeGrid.SetDataPersonBinding(modelData);
         }
         public void initDaily()
         {
             //Inizijalizacija GRID USERS
             gridViewDaily = new GridViewDaily();
             //   gridViewLedger.LedgerStatusSelectedRowchanged += Ledger_HandleRowSelectChanged;
             gridViewDaily.Dock = System.Windows.Forms.DockStyle.Fill;

             activeGrid = gridViewDaily;
             setStandardFilterIfExist();
             modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
             activeGrid.SetDataPersonBinding(modelData);
         }
         public void initDailyEntry()
         {
             //Inizijalizacija GRID USERS
             gridViewDailyEntry = new GridViewDailyEntry();
             //   gridViewLedger.LedgerStatusSelectedRowchanged += Ledger_HandleRowSelectChanged;
             gridViewDailyEntry.Dock = System.Windows.Forms.DockStyle.Fill;

             activeGrid = gridViewDailyEntry;
             setStandardFilterIfExist();
             modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
             activeGrid.SetDataPersonBinding(modelData);
         }

         public void initArticalGroups()
         {
             gridViewArticalGroups = new GridViewArticalGroups();
             gridViewArticalGroups.Dock = System.Windows.Forms.DockStyle.Fill;

             activeGrid = gridViewArticalGroups;
             setStandardFilterIfExist();
             modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
             activeGrid.SetDataPersonBinding(modelData);
         }

         public void initMultimedia()
         {
             gridViewMultimedia = new GridViewMultimedia();
             gridViewMultimedia.MultimediaSelectedRowchanged += Multimedia_HandleRowSelectChanged;
             gridViewMultimedia.Dock = System.Windows.Forms.DockStyle.Fill;

             activeGrid = gridViewMultimedia;
             setStandardFilterIfExist();
             modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
             activeGrid.SetDataPersonBinding(modelData);
         }

         
         public void initPortal()
         {
             meetingScheduler = new MeetingSchedluer_uc();
             meetingScheduler.StatusRadioAppointmentSelectChanged += Tasks_HandleSchedulerOptionSelectChanged;
             splitPanel5.Controls.Clear();
             meetingScheduler.Dock = DockStyle.Fill;
             splitPanel5.Controls.Add(meetingScheduler);


             taskDetailView = new TasksDetailView();
             taskDetailView.Dock = DockStyle.Fill;
             splitPanel7.Controls.Add(taskDetailView);

             contactsDetailView = new ContactsDetailView();
             contactsDetailView.Dock = DockStyle.Fill;
             splitPanel8.Controls.Add(contactsDetailView);

             MenuBUS mtBus = new MenuBUS();
             List<MenuModel> menusBUS = mtBus.GetMenus(30, Login._user.idUser, Login._user.lngUser);

             portalTreeMenu = new RadTreeView();
             portalTreeMenu.DataSource = menusBUS;
             portalTreeMenu.DisplayMember = "nameMenu\\nameMenu";
             portalTreeMenu.ChildMember = "Categories\\subMenus";

             portalTreeMenu.SelectedNodeChanged -= portal_SelectedNodeChanged;
             portalTreeMenu.SelectedNodeChanged += portal_SelectedNodeChanged;

             portalTreeMenu.NodeMouseClick -= portal_SelectedNodeClick;
             portalTreeMenu.NodeMouseClick += portal_SelectedNodeClick;

             portalTreeMenu.NodeDataBound -= rl_NodeDataBound;
             portalTreeMenu.NodeDataBound += rl_NodeDataBound;

             portalTreeMenu.CreateNodeElement -= rl_CreateNodeElement;
             portalTreeMenu.CreateNodeElement += rl_CreateNodeElement;

             portalTreeMenu.NodeFormatting -= rl_NodeFormatting;
             portalTreeMenu.NodeFormatting += rl_NodeFormatting;

             portalTreeMenu.Dock = System.Windows.Forms.DockStyle.Fill;
           //  portalTreeMenu.ThemeName = "Windows8";
             portalTreeMenu.ImageList = imageMenuTree;

             splitPanelAccounting.Controls.Clear();
             splitPanelAccounting.Controls.Add(portalTreeMenu);

             // rl.ExpandAll();
             portalTreeMenu.CollapseAll();
             if (portalTreeMenu.Nodes.Count > 2)
                 portalTreeMenu.Nodes[2].Expand();

             if (portalTreeMenu != null)
                 portalTreeMenu.SelectedNode = null;

             radRibbonBarGroup1.Visibility = ElementVisibility.Collapsed;

         }

         public void initType()
         {
             //Inizijalizacija GRID Type
             gridViewType = new GridViewType();
             gridViewType.TypeStatusSelectedRowchanged += Type_HandleRowSelectChanged;
             gridViewType.Dock = System.Windows.Forms.DockStyle.Fill;

             activeGrid = gridViewType;
             setStandardFilterIfExist();
             modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
             activeGrid.SetDataPersonBinding(modelData);

         }             
         public void initFilterLabel()
         {
             //Inizijalizacija GRID FilterLabel
             gridViewFilterLabel = new GridViewFilterLabel();
             gridViewFilterLabel.FilterLabelStatusSelectedRowchanged += FilterLabel_HandleRowSelectChanged;
             gridViewFilterLabel.Dock = System.Windows.Forms.DockStyle.Fill;

             activeGrid = gridViewFilterLabel;
             setStandardFilterIfExist();
             modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
             activeGrid.SetDataPersonBinding(modelData);

         

         }

         public void initTranslation()
         {
             //Inizijalizacija GRID Translation
             gridViewTranslation = new GridViewTranslation();
             //gridViewTranslation.TranslationStatusSelectedRowchanged += Translation_HandleRowSelectChanged;
             gridViewTranslation.Dock = System.Windows.Forms.DockStyle.Fill;

             activeGrid = gridViewTranslation;
             setStandardFilterIfExist();
             modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
             activeGrid.SetDataPersonBinding(modelData);

         }

         
         public void initProject()
         {
          
             gridViewEmployee = new GridViewEmployee();
             //gridViewEmployee.ClientStatusSelectedRowchanged += Client_HandleRowSelectChanged;

             gridViewEmployee.Dock = System.Windows.Forms.DockStyle.Fill;

             activeGrid = gridViewEmployee;
             setStandardFilterIfExist();
             modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
             activeGrid.SetDataPersonBinding(modelData);

         }

         public void initAccount()
         {
          
             gridViewLedger = new GridViewLedger();
             //gridViewEmployee.ClientStatusSelectedRowchanged += Client_HandleRowSelectChanged;

             gridViewLedger.Dock = System.Windows.Forms.DockStyle.Fill;

             activeGrid = gridViewLedger;
             setStandardFilterIfExist();
             modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
             activeGrid.SetDataPersonBinding(modelData);

             RadTreeNode node = treeViewFilters.GetNodeByName(lngFavorites);
             if (node != null) node.Remove();
             node = treeViewFilters.GetNodeByName(lngCustomFilters);
             if (node != null) node.Remove();
             MenuBUS mtBus = new MenuBUS();
             List<MenuModel> menusBUS = mtBus.GetMenus(29,Login._user.idUser,Login._user.lngUser);


             accountingTreeMenu = new RadTreeView();
             accountingTreeMenu.DataSource = menusBUS;
             accountingTreeMenu.DisplayMember = "nameMenu\\nameMenu";
             accountingTreeMenu.ChildMember = "Categories\\subMenus";

             accountingTreeMenu.SelectedNodeChanged -= accountingTreeMenu_SelectedNodeChanged;
             accountingTreeMenu.SelectedNodeChanged += accountingTreeMenu_SelectedNodeChanged;

             accountingTreeMenu.NodeMouseClick -= accountingTreeMenu_SelectedNodeClick;
             accountingTreeMenu.NodeMouseClick += accountingTreeMenu_SelectedNodeClick;

             accountingTreeMenu.NodeDataBound -= rl_NodeDataBound;
             accountingTreeMenu.NodeDataBound += rl_NodeDataBound;

             accountingTreeMenu.NodeFormatting -= rl_NodeFormatting;
             accountingTreeMenu.NodeFormatting += rl_NodeFormatting;

             accountingTreeMenu.Dock = System.Windows.Forms.DockStyle.Fill;
          //   accountingTreeMenu.ThemeName = "Windows8";
             accountingTreeMenu.ImageList = imageMenuTree;

             //splitPanelAccounting.Controls.Clear();
             //splitPanelAccounting.Controls.Add(accountingTreeMenu);

             // rl.ExpandAll();
             //accountingTreeMenu.CollapseAll();
             //if (accountingTreeMenu.Nodes.Count > 2)
             //    accountingTreeMenu.Nodes[2].Expand();

             if (accountingTreeMenu != null)
                 accountingTreeMenu.SelectedNode = null;

             //rl = new RadTreeView();
             //rl.DataSource = menusBUS;
             //rl.DisplayMember = "nameMenu\\nameMenu";
             //rl.ChildMember = "Categories\\subMenus";

             //rl.SelectedNodeChanged += rl_SelectedNodeChanged;
             //rl.NodeDataBound += rl_NodeDataBound;
             //rl.CreateNodeElement += rl_CreateNodeElement;
             //rl.NodeFormatting += rl_NodeFormatting;

             //rl.Dock = System.Windows.Forms.DockStyle.Fill;
             //rl.ThemeName = "Windows8";
             //rl.ImageList = imageMenuTree;


                          
         }

         public void initArrangement()
         {
             RadTreeNode node = treeViewFilters.GetNodeByName(lngFavorites);
             if (node != null) node.Remove();
             node = treeViewFilters.GetNodeByName(lngCustomFilters);
             if (node != null) node.Remove();
             MenuBUS mtBus = new MenuBUS();
             List<MenuModel> menusBUS = mtBus.GetMenus(28, Login._user.idUser, Login._user.lngUser);

             rl = new RadTreeView();
             rl.DataSource = menusBUS;
             rl.DisplayMember = "nameMenu\\nameMenu";
             rl.ChildMember = "Categories\\subMenus";

             rl.SelectedNodeChanged += rl_SelectedNodeChanged;
             rl.NodeDataBound += rl_NodeDataBound;
             rl.CreateNodeElement += rl_CreateNodeElement;
             rl.NodeFormatting += rl_NodeFormatting;

             rl.Dock = System.Windows.Forms.DockStyle.Fill;
            // rl.ThemeName = "Windows8";
             rl.ImageList = imageMenuTree;


         }

         public void initManagmentReports()
         {
             reportsManagementPanel = new ReportsManagmentPanel();
             reportsManagementPanel.Dock = System.Windows.Forms.DockStyle.Fill;                          
         }

         
         //     public void initAdmin()
         //{

         //    treeViewLabels.Nodes.Clear();
         //    treeViewLabels.Controls.Clear();
         //    treeViewLabels.Refresh();
         //    treeViewFilters.Nodes.Clear();
         //    treeViewFilters.Controls.Clear();
         //    treeViewFilters.Refresh();

         //    noGridSituation();

         //    OnAdminClick();


         //}

         public void initAdmin()
         {

             MenuBUS mtBus = new MenuBUS();
             List<MenuModel> menusBUS = mtBus.GetMenus(33, Login._user.idUser, Login._user.lngUser);

             rl = new RadTreeView();
             rl.DataSource = menusBUS;
             rl.DisplayMember = "nameMenu\\nameMenu";
             rl.ChildMember = "Categories\\subMenus";

             rl.SelectedNodeChanged += rl_SelectedNodeChanged;
             rl.NodeDataBound += rl_NodeDataBound;
             rl.CreateNodeElement += rl_CreateNodeElement;
             rl.NodeFormatting += rl_NodeFormatting;

             rl.Dock = System.Windows.Forms.DockStyle.Fill;
         //    rl.ThemeName = "Windows8";
             rl.ImageList = imageMenuTree;

             //splitPanelAccounting.Controls.Clear();
             //splitPanelAccounting.Controls.Add(rl);

             rl.ExpandAll();

             if (rl != null)
                 rl.SelectedNode = null;


         }


        
        public MainForm()
        {
            folders = new List<string>();
            idLabelList = new List<int>();
            //creates all folder for user and filters
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username)))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username));
                 Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters"));
                 Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters"));
            }
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters"));
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters"));
            }
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters"));
            }

            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\grid_filters")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\grid_filters"));
            }

            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels"));
            }
            //for AllowDrop grids that have SaveDialogLayouts button - Shimmy
            gridFiltersFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\grid_filters");


            PathsBUS pbus = new PathsBUS();
            PathsModel pmodel = new PathsModel();
            pmodel = pbus.GetPathsByType("EML");
            
            //email documenti
            if (pmodel != null && Directory.Exists(pmodel.path) == true)
            {
                myEmailFolder = pmodel.path; 
            }
            else
            {
                if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\MySentEmails")))
                {
                    Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\MySentEmails"));
                }
                myEmailFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\MySentEmails");
            }

            pmodel = pbus.GetPathsByType("WCPLET");
            //template documenti
            if (pmodel != null && Directory.Exists(pmodel.path) == true)
            {
                TemplatesFolder = pmodel.path;
            }
            else
            {
                if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates")))
                {
                    Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates"));
                }
                TemplatesFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates");
            }


            pmodel = pbus.GetPathsByType("ALL");

            // svi ostali documenti
            if (pmodel != null && Directory.Exists(pmodel.path) == true)
            {
                DocumentsFolder = pmodel.path;
            }
            else
            {
                if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Documents")))
                {
                    Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Documents"));
                }

                DocumentsFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Documents");
            }
                        
            filters = new List<FilterModel>();

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                // ovo mora pre inicijalizacije jer treeView se inicijalizuje i ako ove varijable nisu popunjene puca code
                lngFavorites = resxSet.GetString("Favorites");
                lngFavoritesAll = resxSet.GetString("All");
                lngCustomFilters = resxSet.GetString("Custom filters");
            }

            InitializeComponent();

            this.Icon = Login.iconForm;
            radRibbonCustomFilters.Visibility = ElementVisibility.Collapsed;

            //bookyear = new BookYear();
            //bookyear.BookingYearComboChanged += BookYear_HandleBookYearChanged;
            //ddbeYear.Click += BookYear_HandleBookYearChanged;

            Application.ApplicationExit += new EventHandler(this.logout);

            //ako ne postoji kreiraj folder za grid filtere
            if (!Directory.Exists(gridFiltersFolder))
                Directory.CreateDirectory(gridFiltersFolder);

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                // ovo mora posle inicijalizacije posto buttoni jos nisu kreirani
                btnNew.Text = resxSet.GetString("New");
                btnDelete.Text = resxSet.GetString("Delete");
                btnSaveFilter.Text = resxSet.GetString("Save");
                btnDeleteFilter.Text = resxSet.GetString("Delete");
                btnClearFilters.Text = resxSet.GetString("Clear");
                radButtonVolReports.Text = resxSet.GetString("Voluntary");
                radRibbonBarGroup1.Text = resxSet.GetString("New");
                radRibbonCustomFilters.Text = resxSet.GetString("Custom filters");
                
                
                if (resxSet.GetString(radButtonElementPurchaseReport.Text) != null)
                    radButtonElementPurchaseReport.Text = resxSet.GetString(radButtonElementPurchaseReport.Text);

                if (resxSet.GetString(radButtonElementSellReport.Text) != null)
                    radButtonElementSellReport.Text = resxSet.GetString(radButtonElementSellReport.Text);

                if (resxSet.GetString(radRibbonBarGroupReports.Text) != null)
                    radRibbonBarGroupReports.Text = resxSet.GetString(radRibbonBarGroupReports.Text);

                if (resxSet.GetString(radButtonElementAccountReports.Text) != null)
                    radButtonElementAccountReports.Text = resxSet.GetString(radButtonElementAccountReports.Text);

                if (resxSet.GetString(ddbeYear.ActionButton.Text) != null)
                    ddbeYear.ActionButton.Text = resxSet.GetString(ddbeYear.ActionButton.Text);
                
            }

            
            
            // Ucitava User Menu i kreira evente za svaki button click
            for (int i = 0; i < Login._menuModelList.Count; i++) 
            {
                RadMenuButtonItem rbn = new RadMenuButtonItem();
                rbn.Name = Login._menuModelList[i].idMenu.ToString();
                rbn.ToolTipText = Login._menuModelList[i].nameMenu;
                //=== za prikazivanje godine knjizenja u accountingu
                //if (Login._menuModelList[i].nameMenu == "Accounting" || Login._menuModelList[i].nameMenu == "Boekhouding")
                //    radRibbonBarYear.Visibility = ElementVisibility.Visible;
                //=====
                rbn.AutoSize = false;
                rbn.Size = new System.Drawing.Size(36, 36);
                rbn.ButtonElement.ShowBorder = false;
                if (i == 0)
                    rbn.ButtonElement.ButtonFillElement.BackColor = selectedMenuItemColor;
                else
                    rbn.ButtonElement.ButtonFillElement.BackColor = backgroundMenuItemColor;
                BIS.Core.ImageDB image = new BIS.Core.ImageDB();
                rbn.Image = image.setImage(Login._menuModelList[i].imageMenu);
                rbn.Click += new EventHandler(RadMenuButtonClick);

                if(Login._menuModelList[i].initMethod != "")
                {
                    Type type = typeof(MainForm);
                    MethodInfo method = type.GetMethod(Login._menuModelList[i].initMethod);                    
                    method.Invoke(this, null);
                }
                initSubmenus(new MenuBUS().GetMenus(Login._menuModelList[i].idMenu, Login._user.idUser, Login._user.lngUser));
                this.radMenu1.Items.Add(rbn);
            }
             
        }

        private void initSubmenus(List<MenuModel> menusBUS)
        {
            foreach (MenuModel r in menusBUS)
            {
                List<MenuModel> mSub = new MenuBUS().GetMenus(r.idMenu, Login._user.idUser, Login._user.lngUser);
                foreach (MenuModel m in mSub)
                {
                    string initMethodName = m.initMethod;
                    if (initMethodName != null && initMethodName != "")
                    {
                        Type type = typeof(MainForm);
                        MethodInfo method = type.GetMethod(initMethodName);
                        method.Invoke(this, null);
                    }
                }
            }
        }

        public void Person_HandleRowSelectChanged(object sender, StatusSelectedRowchanged args)
        {
            // Ukolio se promeni selekcija na Person Gridu updejtuj PersonDetailView sa novim informacijama.
            if (args != null)
            {
                personDetailView.SetPersonDetails(args.person);
            }

        }
        public void Multimedia_HandleRowSelectChanged(object sender, MultimediaSelectedRowchanged args)
        {
            // Ukolio se promeni selekcija na Menus Gridu 
            if (args != null)
            {
                MultimediaModel r = args.a;
            }
        }
        public void Client_HandleRowSelectChanged(object sender, ClientStatusSelectedRowchanged args)
        {
            // Ukolio se promeni selekcija na Client Gridu 
            if (args != null)
            {
                clientDetailView.SetClientDetails(args.client);
            }
        }
        public void Reason_HandleRowSelectChanged(object sender, ReasonStatusSelectedRowchanged args)
        {
            // Ukolio se promeni selekcija na  Reason Gridu 
            if (args != null)
            {
                ReasonModel p = args.type;
            }
        }

        public void ArrangementBook_HandleRowSelectChanged(object sender, ArrangementSelectedRowchanged args)
        {
            if (args.vod != null)
            {
                arrdetailview.setArrangementBookDetails(args.vod);
            }
        }

        public void Users_HandleRowSelectChanged(object sender, UserStatusSelectedRowchanged args)
        {
            // Ukolio se promeni selekcija na Client Gridu 
            if (args != null)
            {
                UsersAllModel p = args.user;
            }
        }

        public void FilterLabel_HandleRowSelectChanged(object sender, FilterLabelStatusSelectedRowchanged args)
        {
            // Ukolio se promeni selekcija na  FilterLabel Gridu 
            if (args != null)
            {
                FiltersLabelsModel p = args.filterLabel;
            }
        }

        public void Medical_HandleRowSelectChanged(object sender, MedicalStatusSelectedRowchanged args)
        {
            // Ukolio se promeni selekcija na Medical Gridu 
            if (args != null)
            {
                MedicalVoluntaryQuestModel p = args.med;
            }
        }

        public void Voluntary_HandleRowSelectChanged(object sender, VoluntaryStatusSelectedRowchanged args)
        {
            // Ukolio se promeni selekcija na Voluntary Gridu 
            if (args != null)
            {
                MedicalVoluntaryQuestModel p = args.vod;
            }
        }

        public void Tasks_HandleSchedulerOptionSelectChanged(object sender, RadioAppointmentSelectChanged args)
        {
            // Ukolio se promeni selekcija na Person Gridu updejtuj PersonDetailView sa novim informacijama.
            if (args != null)
            {
                taskDetailView.PopulateDataTypes(args.id,0,0,0);
                contactsDetailView.PopulateDataTypes(args.id,0,0);
            }

        }
        public void Type_HandleRowSelectChanged(object sender, TypeStatusSelectedRowchanged args)
        {
            // Ukolio se promeni selekcija na  Type Gridu 
            if (args != null)
            {
                TypeModel p = args.type;
            }
        }

        public void Layouts_HandleRowSelectChanged(object sender, LayoutsSelectedRowchanged args)
        {
            // Ukolio se promeni selekcija na  Layout Gridu 
            if (args != null)
            {
                LayoutsModel p = args.layouts;


            }
        }
        public void Roles_HandleRowSelectChanged(object sender, RolesStatusSelectedRowchanged args)
        {
            // Ukolio se promeni selekcija na Role Gridu 
            if (args != null)
            {
                RoleModel r = args.role;
            }
        }
        public void Menus_HandleRowSelectChanged(object sender, MenusStatusSelectedRowchanged args)
        {
            // Ukolio se promeni selekcija na Menus Gridu 
            if (args != null)
            {
                MenuRoleModel r = args.Menu;
            }
        }
        public void HotelServices_HandleRowSelectChanged(object sender, HotelservicesStatusSelectedRowchanged args)
        {
            // Ukolio se promeni selekcija na  Type Gridu 
            if (args != null)
            {
                HotelServicesModel p = args.hotel;
            }
        }
        public void AgeCategory_HandleRowSelectChanged(object sender, AgeCategoryStatusSelectedRowchanged args)
        {
            // Ukolio se promeni selekcija na  AgeCategory Gridu 
            if (args != null)
            {
                AgeCategoryModel p = args.age;
            }
        }
        public void Period_HandleRowSelectChanged(object sender, PeriodStatusSelectedRowchanged args)
        {
            // Ukolio se promeni selekcija na  Period Gridu 
            if (args != null)
            {
                PeriodModel p = args.period;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

            radRibbonBarGroupBoomarks.Visibility = ElementVisibility.Collapsed;
            radButtonCreateBookmark.Visibility = ElementVisibility.Collapsed;
            radButtonDeleteBookmark.Visibility = ElementVisibility.Collapsed;
            radRibbonBarGroupAccounting.Visibility = ElementVisibility.Collapsed;

            //this.splitPanel1.SizeInfo.SizeMode = Telerik.WinControls.UI.Docking.SplitPanelSizeMode.Absolute;
            //this.splitPanel2.SizeInfo.SizeMode = Telerik.WinControls.UI.Docking.SplitPanelSizeMode.Absolute;
           // this.splitPanel5.SizeInfo.SizeMode = Telerik.WinControls.UI.Docking.SplitPanelSizeMode.Absolute;
           // this.splitPanel6.SizeInfo.SizeMode = Telerik.WinControls.UI.Docking.SplitPanelSizeMode.Absolute;
            //this.splitPanel7.SizeInfo.SizeMode = Telerik.WinControls.UI.Docking.SplitPanelSizeMode.Absolute;
            //this.splitPanel8.SizeInfo.SizeMode = Telerik.WinControls.UI.Docking.SplitPanelSizeMode.Absolute;

           // this.split_cont_left.SizeInfo.SizeMode = Telerik.WinControls.UI.Docking.SplitPanelSizeMode.Absolute;
           // this.split_cont_right.SizeInfo.SizeMode = Telerik.WinControls.UI.Docking.SplitPanelSizeMode.Absolute;
            //this.split_cont_Main.SizeInfo.SizeMode = Telerik.WinControls.UI.Docking.SplitPanelSizeMode.Absolute;
           // this.split_cont_middle.SizeInfo.SizeMode = Telerik.WinControls.UI.Docking.SplitPanelSizeMode.Absolute;

            this.splitPanel1.SizeInfo.MinimumSize = new Size(10, 0);
            this.splitPanel1.SizeInfo.MaximumSize = new Size(220, 0);

            this.splitPanel6.SizeInfo.MinimumSize = new Size(10, 0);
            this.splitPanel6.SizeInfo.MaximumSize = new Size(250, 0);

            this.splitPanelMenu.SizeInfo.MinimumSize = new Size(10, 120);
            this.splitPanelMenu.SizeInfo.MaximumSize = new Size(220, 120);

            accountButton();  // dugme za finansijsku godinu
            
            RadGridLocalizationProvider.CurrentProvider = new DutchGridLocalizationProvider();
            RadSchedulerLocalizationProvider.CurrentProvider = new CustomSchedulerLocalizationProvider();
            radLabelElement1.Text = Login._user.nameUser;
            treeViewFilters.LazyMode = false;

            RadTreeNode node = treeViewFilters.GetNodeByName(lngFavorites);
            node.Expand();

            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            node.Expand();

            if (rl != null)
                rl.ExpandAll();

        }

        private void LoadTreeViewRootFilters(IList<RadTreeNode> nodes)
        {
            RadTreeNode node = new RadTreeNode(lngFavorites);
            node.ImageKey = lngFavorites.ToLower();
            nodes.Add(node);

            node = new RadTreeNode(lngCustomFilters);
            node.ImageKey = lngCustomFilters.ToLower();
            nodes.Add(node);

        }
        private void treeViewFilters_NodesNeeded(object sender, NodesNeededEventArgs e)
        {
            if (e.Parent == null)
            {
                LoadTreeViewRootFilters(e.Nodes);
                return;
            }

            if (e.Parent.Text == lngFavorites)
            {
                if (activeGrid != null)
                    activeGrid.LoadTreeFavorites(e.Nodes, this.activeGrid.ReturnFilters());
            }

            if (e.Parent.Text == lngCustomFilters)
            {
                if (activeGrid != null)
                    this.activeGrid.LoadCustomFilters(e.Nodes, isSaveLayoutDialogClicked);

            }
        }

        private void treeViewLabels_NodesNeeded(object sender, NodesNeededEventArgs e)
        {
            if (e.Parent == null)
            {
               if (this.activeGrid != null)        
                   activeGrid.LoadTreeViewRootLabels(e.Nodes, this.activeGrid.ReturnLabels());             
                return;
            }
        }
        private void treeViewLabels_NodeFormatting(object sender, TreeNodeFormattingEventArgs e)
        {
            if (e.Node.Nodes.Count > 0)
            {
                e.NodeElement.ExpanderElement.Visibility = ElementVisibility.Visible;
            }

        }
       

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            List<Form> lista = new List<Form>();
            foreach (Form f in Application.OpenForms)
            {
                if (f.Name != "MainForm" && f.Name != "Login")
                {
                    lista.Add(f);
                }
            }

            foreach (var f1 in lista)
            {
                f1.Dispose();
            }
        }

      
        private void btnSaveFilter_Click(object sender, EventArgs e)
        {

            translateRadMessageBox tr = new translateRadMessageBox();
            DialogResult dlgStandard = tr.translateAllMessageBoxDialog("Are you want to add standard filter", "Confirmation");
            if (dlgStandard != DialogResult.Cancel)
            {
                string filename = null;
                if (dlgStandard == DialogResult.Yes)
                {
                    filename = "Standard.xml";
                    saveFilters(filename);
                }
                else if (dlgStandard == DialogResult.No)
                {
                    var dlgSave = new SaveDialogLayouts();

                    if (dlgSave.ShowDialog(this) == DialogResult.Yes)
                    {
                        isSaveLayoutDialogClicked = true;
                        filename = dlgSave.filename;

                        saveFilters(filename);

                        isSaveLayoutDialogClicked = false;
                    }
                    dlgSave.Dispose();
                }

            }

        }
        private void saveFilters(string filename)
        {
            if (filename != "")
            {

                if (!File.Exists(activeGrid.FilterFolder + "\\" + filename))
                {
                    treeViewFilters.BeginEdit();
                    treeViewFilters.GetNodeByName(lngCustomFilters).ImageKey = "customnodes";
                    RadTreeNode rt = new RadTreeNode(filename.Replace(".xml", ""));

                    treeViewFilters.GetNodeByName(lngCustomFilters).Nodes.Add(rt);


                    treeViewFilters.GetNodeByName(lngCustomFilters).Nodes[filename.Replace(".xml", "")].Selected = true;
                    treeViewFilters.GetNodeByName(lngCustomFilters).Nodes[filename.Replace(".xml", "")].Current = true;
                    treeViewFilters.EndEdit();
                }
                treeViewFilters.ExpandAll();
                treeViewFilters.Invalidate();

                this.activeGrid.SaveLayout(filename);


                //treeViewFilters.GetNodeByName(lngCustomFilters).Expand();


                if (File.Exists(Path.Combine(activeGrid.LabelFolder, filename)))
                {
                    File.Delete(Path.Combine(activeGrid.LabelFolder, filename));
                }

                XmlDocument xml = new XmlDocument();
                XmlElement root = xml.CreateElement("Root");
                xml.AppendChild(root);

                XmlElement Filter = xml.CreateElement("Filter");
                Filter.InnerText = selectedFilter;
                root.AppendChild(Filter);

                string labelList = "";

                idLabelList.Clear();
                foreach (var n in treeViewLabels.Nodes)
                {
                    if (n.Checked == true)
                        idLabelList.Add(Int32.Parse(n.Name));
                }
                foreach (int labels in idLabelList)
                {
                    labelList = labelList + labels.ToString() + ",";
                }

                if (labelList != "")
                {
                    XmlElement Labels = xml.CreateElement("Labels");
                    Labels.InnerText = labelList.Substring(0, labelList.Length - 1);
                    root.AppendChild(Labels);
                }

                xml.Save(Path.Combine(activeGrid.LabelFolder, filename));

            }
        }
        private void treeViewFilters_NodeMouseClick(object sender, RadTreeViewEventArgs e)
        {
            if (e.Node.RootNode.Text == lngFavorites && e.Node.Text != lngFavorites)
            {
                if (e.Node.Text == lngFavoritesAll)
                {

                }
                idLabelList.Clear();
                foreach (var node in treeViewLabels.Nodes)
                {
                    if (node.Checked == true)
                    {
                        idLabelList.Add(Int32.Parse(node.Name));
                    }
                }
                selectedFilter = e.Node.Name;

                Cursor.Current = Cursors.WaitCursor;
                modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);

                this.activeGrid.SetDataPersonBinding(null);
                this.activeGrid.SetDataPersonBinding(modelData);
                this.activeGrid.ClearDescriptors();

                Cursor.Current = Cursors.Default;
            }

            if (e.Node.RootNode.Text == lngCustomFilters && e.Node.Text != lngCustomFilters)
            {
                Cursor.Current = Cursors.WaitCursor;

                //resetuje labele
                foreach (var node in treeViewLabels.Nodes)
                {
                    node.Checked = false;
                }

                string file = e.Node.Text;
                if (file.Contains(".xml") == false)
                    file = file + ".xml";


                this.activeGrid.ClearDescriptors();

                string filterFor = null;
                if (File.Exists(Path.Combine(activeGrid.LabelFolder, e.Node.Name + ".xml")))
                {
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.Load(Path.Combine(activeGrid.LabelFolder, e.Node.Name + ".xml"));
                    XmlNodeList xList = xmldoc.SelectNodes("/Root");
                    foreach (XmlNode xn in xList)
                    {
                        if (xn.SelectSingleNode("Labels") != null)
                        {
                            String[] nn = xn.SelectSingleNode("Labels").InnerText.Split(',');
                            for (int i = 0; i < nn.Length; i++)
                            {
                                foreach (var node in treeViewLabels.Nodes)
                                {
                                    if (node.Name == nn[i]) node.Checked = true;
                                }
                            }
                        }
                        if (xn.SelectSingleNode("Filter") != null)
                        {
                            filterFor = xn.SelectSingleNode("Filter").InnerText;
                        }
                    }

                    if (filterFor != null)
                    {

                        modelData = this.activeGrid.GetData(Convert.ToInt32(filterFor), idLabelList, Login._user.lngUser);
                        this.activeGrid.SetDataPersonBinding(null);
                        this.activeGrid.SetDataPersonBinding(modelData);

                    }
                }

                this.activeGrid.LoadLayout(file);

                Cursor.Current = Cursors.Default;
            }
        }

       
        
        private void btnClearFilters_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Clear Layout to Default ?", "Layout", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                this.activeGrid.ClearDescriptors();
                foreach (GridViewColumn col in this.activeGrid.Columns)
                {
                    col.IsVisible = true;
                }
            }
        }

        private void btnDeleteFilter_Click(object sender, EventArgs e)
        {
            if (treeViewFilters.SelectedNode != null)
            {
                if (treeViewFilters.SelectedNode.Parent != null)
                {
                    if (treeViewFilters.SelectedNode.Parent.Text == lngCustomFilters)
                    {
                        DialogResult dr = MessageBox.Show("Delete Filter " + treeViewFilters.SelectedNode.Text + "?", "Delete Filter", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == DialogResult.Yes)
                        {
                            if (File.Exists(this.activeGrid.LabelFolder + "\\" + treeViewFilters.SelectedNode.Text + ".xml"))
                            {
                                File.Delete(this.activeGrid.LabelFolder + "\\" + treeViewFilters.SelectedNode.Text + ".xml");
                            }
                            File.Delete(this.activeGrid.FilterFolder + "\\" + treeViewFilters.SelectedNode.Text + ".xml");
                            treeViewFilters.SelectedNode.Remove();
                            this.activeGrid.ClearDescriptors();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid operation");
                    }
                }

            }
            else
                MessageBox.Show("Filter is not selected!");

        }
     

        private void treeViewLabels_NodeCheckedChanged(object sender, TreeNodeCheckedEventArgs e)
        {                       
            idLabelList.Clear();
            foreach (var node in treeViewLabels.Nodes)
            {
                if (node.Checked == true)
                {
                    idLabelList.Add(Int32.Parse(node.Name));

                }
            }

            Cursor.Current = Cursors.WaitCursor;
            modelData = this.activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
            this.activeGrid.SetDataPersonBinding(null);
            this.activeGrid.SetDataPersonBinding(modelData);


            treeViewLabels.Enabled = true;
            Cursor.Current = Cursors.Default;
            
        }
        public void OnArrangementInsuranceClick()
        {

            btnNew.Visibility = ElementVisibility.Visible;
            btnDelete.Visibility = ElementVisibility.Visible;
            radRibbonBarGroup1.Visibility = ElementVisibility.Visible;
            radRibbonBarYear.Visibility = ElementVisibility.Collapsed;

            splitPanel7.Controls.Clear();
            splitPanel5.Controls.Clear();

            activeGrid = gridviewArrangementInsurance;
            activeGrid.bLoadTreeMenu = true;
            splitPanel5.Controls.Add(gridviewArrangementInsurance);

            RadTreeNode node = treeViewFilters.GetNodeByName(lngFavorites);
            if (node != null) node.Remove();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            if (node != null) node.Remove();

            treeViewLabels.Nodes.Clear();
            treeViewLabels.Controls.Clear();
            treeViewLabels.Refresh();

            //mora ponovo da bi se otvorili
            node = treeViewFilters.GetNodeByName(lngFavorites);
            node.Expand();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            node.Expand();

            splitPanel8.Controls.Clear();

            splitPanelFavorites.Controls.Clear();
            splitPanelFavorites.Controls.Add(treeViewFilters);

            //if (rl != null)
            //rl.SelectedNode = null;
            selectStandardFilterIfExist();
            gridCustomFilter = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\ArrangementInsurance");
        }
        public void OnReasonClick()
        {

            btnNew.Visibility = ElementVisibility.Visible;
            btnDelete.Visibility = ElementVisibility.Visible;
            radRibbonBarGroup1.Visibility = ElementVisibility.Visible;
            radRibbonBarYear.Visibility = ElementVisibility.Collapsed;
            // splitPanelAccounting.Controls.Clear();

            splitPanel7.Controls.Clear();
            splitPanel5.Controls.Clear();

            activeGrid = gridViewReason;
            activeGrid.bLoadTreeMenu = true;
            splitPanel5.Controls.Add(gridViewReason);

            RadTreeNode node = treeViewFilters.GetNodeByName(lngFavorites);
            if (node != null) node.Remove();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            if (node != null) node.Remove();

            treeViewLabels.Nodes.Clear();
            treeViewLabels.Controls.Clear();
            treeViewLabels.Refresh();

            //mora ponovo da bi se otvorili
            node = treeViewFilters.GetNodeByName(lngFavorites);
            node.Expand();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            node.Expand();

            splitPanel8.Controls.Clear();

            splitPanelFavorites.Controls.Clear();
            splitPanelFavorites.Controls.Add(treeViewFilters);

            //if (rl != null)
            //rl.SelectedNode = null;
            selectStandardFilterIfExist();
            gridCustomFilter = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\type");
        }

        public void OnPeriodClick()
        {

            btnNew.Visibility = ElementVisibility.Visible;
            btnDelete.Visibility = ElementVisibility.Visible;
            radRibbonBarGroup1.Visibility = ElementVisibility.Visible;
            radRibbonBarYear.Visibility = ElementVisibility.Collapsed;
            
            // splitPanelAccounting.Controls.Clear();

            splitPanel7.Controls.Clear();
            splitPanel5.Controls.Clear();

            activeGrid = gridViewPeriod;
            activeGrid.bLoadTreeMenu = true;
            splitPanel5.Controls.Add(gridViewPeriod);

            RadTreeNode node = treeViewFilters.GetNodeByName(lngFavorites);
            if (node != null) node.Remove();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            if (node != null) node.Remove();

            treeViewLabels.Nodes.Clear();
            treeViewLabels.Controls.Clear();
            treeViewLabels.Refresh();

            //mora ponovo da bi se otvorili
            node = treeViewFilters.GetNodeByName(lngFavorites);
            node.Expand();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            node.Expand();

            splitPanel8.Controls.Clear();

            splitPanelFavorites.Controls.Clear();
            splitPanelFavorites.Controls.Add(treeViewFilters);

            //if (rl != null)
            //rl.SelectedNode = null;
            selectStandardFilterIfExist();
            gridCustomFilter = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\period");
        }
        public void OnAgeCategoryClick()
        {
            // splitPanelAccounting.Controls.Clear();
            btnNew.Visibility = ElementVisibility.Visible;
            btnDelete.Visibility = ElementVisibility.Visible;
            radRibbonBarGroup1.Visibility = ElementVisibility.Visible;
            radRibbonBarYear.Visibility = ElementVisibility.Collapsed;

            splitPanel7.Controls.Clear();
            splitPanel5.Controls.Clear();

            activeGrid = gridViewAgeCategory;
            activeGrid.bLoadTreeMenu = true;
            splitPanel5.Controls.Add(gridViewAgeCategory);

            RadTreeNode node = treeViewFilters.GetNodeByName(lngFavorites);
            if (node != null) node.Remove();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            if (node != null) node.Remove();

            treeViewLabels.Nodes.Clear();
            treeViewLabels.Controls.Clear();
            treeViewLabels.Refresh();

            //mora ponovo da bi se otvorili
            node = treeViewFilters.GetNodeByName(lngFavorites);
            node.Expand();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            node.Expand();

            splitPanel8.Controls.Clear();

            splitPanelFavorites.Controls.Clear();
            splitPanelFavorites.Controls.Add(treeViewFilters);

            //if (rl != null)
            //rl.SelectedNode = null;
            selectStandardFilterIfExist();
            gridCustomFilter = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\ageCategory");
        }

        public void OnHotelServicesClick()
        {
            // splitPanelAccounting.Controls.Clear();
            btnNew.Visibility = ElementVisibility.Visible;
            btnDelete.Visibility = ElementVisibility.Visible;
            radRibbonBarGroup1.Visibility = ElementVisibility.Visible;
            radRibbonBarYear.Visibility = ElementVisibility.Collapsed;


            splitPanel7.Controls.Clear();
            splitPanel5.Controls.Clear();

            activeGrid = gridViewHotelservices;
            activeGrid.bLoadTreeMenu = true;
            splitPanel5.Controls.Add(gridViewHotelservices);

            RadTreeNode node = treeViewFilters.GetNodeByName(lngFavorites);
            if (node != null) node.Remove();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            if (node != null) node.Remove();

            treeViewLabels.Nodes.Clear();
            treeViewLabels.Controls.Clear();
            treeViewLabels.Refresh();

            //mora ponovo da bi se otvorili
            node = treeViewFilters.GetNodeByName(lngFavorites);
            node.Expand();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            node.Expand();

            splitPanel8.Controls.Clear();

            splitPanelFavorites.Controls.Clear();
            splitPanelFavorites.Controls.Add(treeViewFilters);

            //if (rl != null)
            //rl.SelectedNode = null;
            selectStandardFilterIfExist();
            gridCustomFilter = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\type");
        }

        public void OnArrangementInsurancePremieClick()
        {
            btnNew.Visibility = ElementVisibility.Visible;
            btnDelete.Visibility = ElementVisibility.Visible;
            radRibbonBarGroup1.Visibility = ElementVisibility.Visible;
            radRibbonBarYear.Visibility = ElementVisibility.Collapsed;
            

            splitPanel7.Controls.Clear();
            splitPanel5.Controls.Clear();

            activeGrid = gridviewArrangementInsurancePremie;
            activeGrid.bLoadTreeMenu = true;
            splitPanel5.Controls.Add(gridviewArrangementInsurancePremie);

            RadTreeNode node = treeViewFilters.GetNodeByName(lngFavorites);
            if (node != null) node.Remove();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            if (node != null) node.Remove();

            treeViewLabels.Nodes.Clear();
            treeViewLabels.Controls.Clear();
            treeViewLabels.Refresh();

            //mora ponovo da bi se otvorili
            node = treeViewFilters.GetNodeByName(lngFavorites);
            node.Expand();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            node.Expand();

            splitPanel8.Controls.Clear();

            splitPanelFavorites.Controls.Clear();
            splitPanelFavorites.Controls.Add(treeViewFilters);

            //if (rl != null)
            //rl.SelectedNode = null;
            selectStandardFilterIfExist();
            gridCustomFilter = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\ArrangementInsurancePremie");
        }
        public void OnRolesClick()
        {
            btnNew.Visibility = ElementVisibility.Visible;
            btnDelete.Visibility = ElementVisibility.Visible;
            radRibbonBarGroup1.Visibility = ElementVisibility.Visible;
            radRibbonBarYear.Visibility = ElementVisibility.Collapsed;
            //    splitPanelAccounting.Controls.Clear();
            splitPanel7.Controls.Clear();
            splitPanel5.Controls.Clear();

            activeGrid = gridViewRoles;
            activeGrid.bLoadTreeMenu = true;
            splitPanel5.Controls.Add(gridViewRoles);

            RadTreeNode node = treeViewFilters.GetNodeByName(lngFavorites);
            if (node != null) node.Remove();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            if (node != null) node.Remove();

            treeViewLabels.Nodes.Clear();
            treeViewLabels.Controls.Clear();
            treeViewLabels.Refresh();

            //mora ponovo da bi se otvorili
            node = treeViewFilters.GetNodeByName(lngFavorites);
            node.Expand();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            node.Expand();

            splitPanel8.Controls.Clear();

            splitPanelFavorites.Controls.Clear();
            splitPanelFavorites.Controls.Add(treeViewFilters);

            //if(rl!=null)
            //rl.SelectedNode = null;
            selectStandardFilterIfExist();
            gridCustomFilter = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\roles");

        }
        public void OnPersonClick()
        {
            this.splitPanel1.SizeInfo.MinimumSize = new Size(10, 0);
            this.splitPanel1.SizeInfo.MaximumSize = new Size(220, 0);

            this.splitPanel6.SizeInfo.MinimumSize = new Size(10, 0);
            this.splitPanel6.SizeInfo.MaximumSize = new Size(250, 0);

           // this.splitPanel7.SizeInfo.MinimumSize = new Size(10, 0);

            this.splitPanelMenu.SizeInfo.MinimumSize = new Size(10, 120);
            this.splitPanelMenu.SizeInfo.MaximumSize = new Size(220, 120);


            btnNew.Visibility = ElementVisibility.Visible;
            btnDelete.Visibility = ElementVisibility.Visible;
            radRibbonBarGroup1.Visibility = ElementVisibility.Visible;

            splitPanel6.Collapsed = false;
            splitPanel8.Collapsed = true;
            splitPanelAccounting.Collapsed = true;
            splitPanel7.Refresh();


            radRibbonBarGroupReports.Visibility = ElementVisibility.Visible;
            radButtonElementPurchaseReport.Visibility = ElementVisibility.Collapsed;
            radButtonElementSellReport.Visibility = ElementVisibility.Collapsed;
            radButtonInvoiceSelection.Visibility = ElementVisibility.Collapsed;
            radButtonVolReports.Visibility = ElementVisibility.Visible;
            radButtonSearchAndBooking.Visibility = ElementVisibility.Collapsed;
            radRibbonBarGroupAccounting.Visibility = ElementVisibility.Collapsed;
            radRibbonBarYear.Visibility = ElementVisibility.Collapsed;

            splitPanelAccounting.Controls.Clear();
            splitPanel5.Controls.Clear();
            splitPanelFavorites.Controls.Clear();
            splitPanelFavorites.Controls.Add(treeViewFilters);

            activeGrid = gridViewPerson;
            activeGrid.bLoadTreeMenu = true;

            splitPanel5.Controls.Add(gridViewPerson);
            splitPanel7.Controls.Clear();
            personDetailView.Dock = DockStyle.Fill;
            splitPanel7.Controls.Add(personDetailView);

            RadTreeNode node = treeViewFilters.GetNodeByName(lngFavorites);
            if (node != null)
            {
                node.Remove();
            }
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            if (node != null)
            {
                node.Remove();
            }

            treeViewLabels.Nodes.Clear();

            //mora ponovo da bi se otvorili
            node = treeViewFilters.GetNodeByName(lngFavorites);
            node.Expand();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            node.Expand();

            splitPanel8.Controls.Clear();

            if (rl != null)
            rl.SelectedNode = null;
            selectStandardFilterIfExist();
            gridCustomFilter = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\person");

        }
        public void OnMultimediaClick()
        {
            btnNew.Visibility = ElementVisibility.Visible;
            btnDelete.Visibility = ElementVisibility.Visible;
            radRibbonBarGroup1.Visibility = ElementVisibility.Visible;

            radRibbonBarGroupReports.Visibility = ElementVisibility.Collapsed;
            radRibbonBarYear.Visibility = ElementVisibility.Collapsed;

            splitPanel5.Controls.Clear();
            splitPanel8.Controls.Clear();
            splitPanelFavorites.Controls.Clear();
            splitPanelFavorites.Controls.Add(treeViewFilters);


            activeGrid = gridViewMultimedia;
            activeGrid.bLoadTreeMenu = true;
            splitPanel5.Controls.Add(gridViewMultimedia);
            splitPanel7.Controls.Clear();

            RadTreeNode node = treeViewFilters.GetNodeByName(lngFavorites);
            if (node != null) node.Remove();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            if (node != null) node.Remove();

            treeViewLabels.Nodes.Clear();
            treeViewLabels.Controls.Clear();
            treeViewLabels.Refresh();

            //mora ponovo da bi se otvorili
            node = treeViewFilters.GetNodeByName(lngFavorites);
            node.Expand();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            node.Expand();
            selectStandardFilterIfExist();
            gridCustomFilter = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\multimedia");

        }
        public void OnTravelDataClick()
        {
            // splitPanelAccounting.Controls.Clear();
            btnNew.Visibility = ElementVisibility.Visible;
            btnDelete.Visibility = ElementVisibility.Visible;
            radRibbonBarGroup1.Visibility = ElementVisibility.Visible;
            radRibbonBarYear.Visibility = ElementVisibility.Collapsed;

            splitPanel7.Controls.Clear();
            splitPanel5.Controls.Clear();

            activeGrid = gridTravelData;
            activeGrid.bLoadTreeMenu = true;
            splitPanel5.Controls.Add(gridTravelData);

            RadTreeNode node = treeViewFilters.GetNodeByName(lngFavorites);
            if (node != null) node.Remove();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            if (node != null) node.Remove();

            treeViewLabels.Nodes.Clear();
            treeViewLabels.Controls.Clear();
            treeViewLabels.Refresh();

            //mora ponovo da bi se otvorili
            node = treeViewFilters.GetNodeByName(lngFavorites);
            node.Expand();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            node.Expand();

            splitPanel8.Controls.Clear();

            splitPanelFavorites.Controls.Clear();
            splitPanelFavorites.Controls.Add(treeViewFilters);

            //if (rl != null)
            //rl.SelectedNode = null;
            selectStandardFilterIfExist();
            gridCustomFilter = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\type");
        }

        public void OnPaymentsClick()
        {
            btnNew.Visibility = ElementVisibility.Visible;
            btnDelete.Visibility = ElementVisibility.Visible;
            radRibbonBarGroup1.Visibility = ElementVisibility.Visible;
            radRibbonBarYear.Visibility = ElementVisibility.Visible;

            splitPanel5.Controls.Clear();
            //splitPanel8.Controls.Clear();
            splitPanelFavorites.Controls.Clear();
            splitPanelFavorites.Controls.Add(treeViewFilters);


            activeGrid = gridViewPayments;
            activeGrid.bLoadTreeMenu = true;
            splitPanel5.Controls.Add(gridViewPayments);
            splitPanel7.Controls.Clear();

            RadTreeNode node = treeViewFilters.GetNodeByName(lngFavorites);
            if (node != null) node.Remove();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            if (node != null) node.Remove();

            treeViewLabels.Nodes.Clear();
            treeViewLabels.Controls.Clear();
            treeViewLabels.Refresh();

            //mora ponovo da bi se otvorili
            node = treeViewFilters.GetNodeByName(lngFavorites);
            node.Expand();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            node.Expand();
            selectStandardFilterIfExist();
          
            gridCustomFilter = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\payment");

        }

        public void OnSaleBookingClick()
        {
            //btnNew.Visibility = ElementVisibility.Visible;
            //btnDelete.Visibility = ElementVisibility.Visible;
            radRibbonBarGroup1.Visibility = ElementVisibility.Collapsed;

            radRibbonBarGroupReports.Visibility = ElementVisibility.Visible;
            radButtonElementPurchaseReport.Visibility = ElementVisibility.Collapsed;
            radButtonElementSellReport.Visibility = ElementVisibility.Visible;
            radButtonInvoiceSelection.Visibility = ElementVisibility.Visible;
            radButtonVolReports.Visibility = ElementVisibility.Collapsed;
            radButtonSearchAndBooking.Visibility = ElementVisibility.Visible;
            radRibbonBarGroupAccounting.Visibility = ElementVisibility.Collapsed;
            radRibbonBarYear.Visibility = ElementVisibility.Collapsed;

            splitPanel5.Controls.Clear();
            splitPanel8.Controls.Clear();
            splitPanelFavorites.Controls.Clear();
            this.activeGrid.ClearDescriptors();
            foreach (GridViewColumn col in this.activeGrid.Columns)
            {
               
                col.IsVisible = true;
            }        
            

            splitPanelFavorites.Controls.Add(treeViewFilters);

            gridViewArrangement.onClickReference = 2;
            activeGrid = gridViewArrangement;
            gridViewArrangement.setFiltersAndLabels();
            activeGrid.bLoadTreeMenu = true;
            splitPanel5.Controls.Add(gridViewArrangement);
            splitPanel7.Controls.Clear();
            splitPanel7.Controls.Add(arrdetailview);



            RadTreeNode node = treeViewFilters.GetNodeByName(lngFavorites);
            if (node != null) node.Remove();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            if (node != null) node.Remove();

            treeViewLabels.Nodes.Clear();
            treeViewLabels.Controls.Clear();
            treeViewLabels.Refresh();


            //mora ponovo da bi se otvorili
            node = treeViewFilters.GetNodeByName(lngFavorites);
            node.Expand();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            node.Expand();
            selectStandardFilterIfExist();
            gridCustomFilter = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\arrangementTraveler");

        }

        public void OnVoluntaryHelperBookingClick()
        {
            btnNew.Visibility = ElementVisibility.Visible;
            btnDelete.Visibility = ElementVisibility.Visible;
            radRibbonBarGroup1.Visibility = ElementVisibility.Visible;
            radRibbonBarYear.Visibility = ElementVisibility.Collapsed;

            // gridCustomFilter = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\arrangementVH");
            splitPanel5.Controls.Clear();
            splitPanel8.Controls.Clear();
            splitPanelFavorites.Controls.Clear();
            this.activeGrid.ClearDescriptors();
            foreach (GridViewColumn col in this.activeGrid.Columns)
            {
                col.IsVisible = true;
            }
            splitPanelFavorites.Controls.Add(treeViewFilters);

            gridViewArrangement.onClickReference = 3;
            activeGrid = gridViewArrangement;
            gridViewArrangement.setFiltersAndLabels();
            activeGrid.bLoadTreeMenu = true;
            splitPanel5.Controls.Add(gridViewArrangement);
            splitPanel7.Controls.Clear();

            RadTreeNode node = treeViewFilters.GetNodeByName(lngFavorites);
            if (node != null) node.Remove();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            if (node != null) node.Remove();

            treeViewLabels.Nodes.Clear();
            treeViewLabels.Controls.Clear();
            treeViewLabels.Refresh();

            //mora ponovo da bi se otvorili
            node = treeViewFilters.GetNodeByName(lngFavorites);
            node.Expand();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            node.Expand();
            selectStandardFilterIfExist();
            gridCustomFilter = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\arrangementVH");

        }

        public void OnMenusClick()
        {
            btnNew.Visibility = ElementVisibility.Visible;
            btnDelete.Visibility = ElementVisibility.Visible;
            radRibbonBarGroup1.Visibility = ElementVisibility.Visible;
            radRibbonBarYear.Visibility = ElementVisibility.Collapsed;
          
            //    splitPanelAccounting.Controls.Clear();
            splitPanel7.Controls.Clear();
            splitPanel5.Controls.Clear();

            activeGrid = gridViewMenus;
            activeGrid.bLoadTreeMenu = true;
            splitPanel5.Controls.Add(gridViewMenus);

            RadTreeNode node = treeViewFilters.GetNodeByName(lngFavorites);
            if (node != null) node.Remove();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            if (node != null) node.Remove();

            treeViewLabels.Nodes.Clear();
            treeViewLabels.Controls.Clear();
            treeViewLabels.Refresh();

            //mora ponovo da bi se otvorili
            node = treeViewFilters.GetNodeByName(lngFavorites);
            node.Expand();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            node.Expand();

            splitPanel8.Controls.Clear();

            splitPanelFavorites.Controls.Clear();
            splitPanelFavorites.Controls.Add(treeViewFilters);

            //if(rl!=null)
            //rl.SelectedNode = null;
            selectStandardFilterIfExist();
            gridCustomFilter = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\menus");

        }
        public void OnManagmentReportsClick()
        {
            radRibbonBarGroup1.Visibility = ElementVisibility.Collapsed;
            radRibbonBarGroupReports.Visibility = ElementVisibility.Collapsed;
            radRibbonBarGroupAccounting.Visibility = ElementVisibility.Collapsed;
            radRibbonBarYear.Visibility = ElementVisibility.Collapsed;

            splitPanel6.Collapsed = true;
            splitPanel8.Collapsed = true;


            splitPanelAccounting.Controls.Clear();
            splitPanel5.Controls.Clear();
            splitPanelFavorites.Controls.Clear();                        
            splitPanel5.Controls.Add(reportsManagementPanel);
            splitPanel7.Controls.Clear();
            splitPanel8.Controls.Clear();
            
        }
        public void OnClientsClick()
        {
            btnNew.Visibility = ElementVisibility.Visible;
            btnDelete.Visibility = ElementVisibility.Visible;
            radRibbonBarGroup1.Visibility = ElementVisibility.Visible;
            radRibbonBarGroupReports.Visibility = ElementVisibility.Collapsed;
            radRibbonBarGroupAccounting.Visibility = ElementVisibility.Collapsed;
            radRibbonBarYear.Visibility = ElementVisibility.Collapsed;

            splitPanelAccounting.Controls.Clear();
            splitPanel5.Controls.Clear();
            splitPanelFavorites.Controls.Clear();
            splitPanelFavorites.Controls.Add(treeViewFilters);

            splitPanel8.Collapsed = true;

            activeGrid = gridViewClients;
            activeGrid.bLoadTreeMenu = true;
            splitPanel5.Controls.Add(gridViewClients);
            splitPanel7.Controls.Clear();
            splitPanel7.Controls.Add(clientDetailView);

            RadTreeNode node = treeViewFilters.GetNodeByName(lngFavorites);
            if (node != null) node.Remove();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            if (node != null) node.Remove();

            treeViewLabels.Nodes.Clear();
            treeViewLabels.Controls.Clear();
            treeViewLabels.Refresh();

            //mora ponovo da bi se otvorili
            node = treeViewFilters.GetNodeByName(lngFavorites);
            node.Expand();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            node.Expand();

            splitPanel8.Controls.Clear();

            if (rl != null)
            rl.SelectedNode = null;
            selectStandardFilterIfExist();
            gridCustomFilter = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\client");

        }
        public void OnMultimediaServerClick()
        {
            btnNew.Visibility = ElementVisibility.Visible;
            btnDelete.Visibility = ElementVisibility.Visible;
            radRibbonBarGroup1.Visibility = ElementVisibility.Visible;
            radRibbonBarYear.Visibility = ElementVisibility.Collapsed;

            splitPanel5.Controls.Clear();
            splitPanel8.Controls.Clear();
            splitPanelFavorites.Controls.Clear();
            splitPanelFavorites.Controls.Add(treeViewFilters);


            activeGrid = gridViewMultimediaServer;
            activeGrid.bLoadTreeMenu = true;
            splitPanel5.Controls.Add(gridViewMultimediaServer);
            splitPanel7.Controls.Clear();

            RadTreeNode node = treeViewFilters.GetNodeByName(lngFavorites);
            if (node != null) node.Remove();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            if (node != null) node.Remove();

            treeViewLabels.Nodes.Clear();
            treeViewLabels.Controls.Clear();
            treeViewLabels.Refresh();

            //mora ponovo da bi se otvorili
            node = treeViewFilters.GetNodeByName(lngFavorites);
            node.Expand();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            node.Expand();
            selectStandardFilterIfExist();
            gridCustomFilter = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\multimediaserver");

        }

        public void OnCountryClick()
        {
            btnNew.Visibility = ElementVisibility.Visible;
            btnDelete.Visibility = ElementVisibility.Visible;
            radRibbonBarGroup1.Visibility = ElementVisibility.Visible;
            radRibbonBarYear.Visibility = ElementVisibility.Collapsed;

            splitPanel5.Controls.Clear();
            splitPanel8.Controls.Clear();
            splitPanelFavorites.Controls.Clear();
            splitPanelFavorites.Controls.Add(treeViewFilters);


            activeGrid = gridViewCountry;
            activeGrid.bLoadTreeMenu = true;
            splitPanel5.Controls.Add(gridViewCountry);
            splitPanel7.Controls.Clear();

            RadTreeNode node = treeViewFilters.GetNodeByName(lngFavorites);
            if (node != null) node.Remove();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            if (node != null) node.Remove();

            treeViewLabels.Nodes.Clear();
            treeViewLabels.Controls.Clear();
            treeViewLabels.Refresh();

            //mora ponovo da bi se otvorili
            node = treeViewFilters.GetNodeByName(lngFavorites);
            node.Expand();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            node.Expand();
            selectStandardFilterIfExist();
            gridCustomFilter = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\country");

        }
        public void OnAccSettingsClick()
        {
            btnNew.Visibility = ElementVisibility.Visible;
            btnDelete.Visibility = ElementVisibility.Visible;
            radRibbonBarGroup1.Visibility = ElementVisibility.Visible;
            radRibbonBarYear.Visibility = ElementVisibility.Visible;

            splitPanel5.Controls.Clear();
          //  splitPanel8.Controls.Clear();
            splitPanelFavorites.Controls.Clear();
            splitPanelFavorites.Controls.Add(treeViewFilters);


            activeGrid = gridViewAccSettings;
            activeGrid.bLoadTreeMenu = true;
            AccSettingsBUS acb = new AccSettingsBUS();
            List<AccSettingsModel> acm = new List<AccSettingsModel>();
           
            acm = acb.GetAllAccSettings(Login._bookyear);
            gridViewAccSettings.SetDataPersonBinding1(acm); //modelData
           // splitPanel5.Controls.Remove(gridViewAccSettings);
            splitPanel5.Controls.Add(null);
            splitPanel5.Controls.Add(gridViewAccSettings);
         
            splitPanel7.Controls.Clear();
            
            //radRibbonBarGroupAccounting.Visibility = ElementVisibility.Visible;

            RadTreeNode node = treeViewFilters.GetNodeByName(lngFavorites);
            if (node != null) node.Remove();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            if (node != null) node.Remove();

            treeViewLabels.Nodes.Clear();
            treeViewLabels.Controls.Clear();
            treeViewLabels.Refresh();

            //mora ponovo da bi se otvorili
            node = treeViewFilters.GetNodeByName(lngFavorites);
            node.Expand();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            node.Expand();
            //selectStandardFilterIfExist();
            gridCustomFilter = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\accsettings");

        }


        public void OnInvoiceClick()
        {
            splitPanel5.Controls.Clear();
          //  splitPanel8.Controls.Clear();
            splitPanelFavorites.Controls.Clear();
            splitPanelFavorites.Controls.Add(treeViewFilters);

            activeGrid = gridViewInvoice;
            modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
            activeGrid.SetDataPersonBinding(modelData);

            activeGrid.bLoadTreeMenu = true;
            splitPanel5.Controls.Add(gridViewInvoice);
            splitPanel7.Controls.Clear();

            RadTreeNode node = treeViewFilters.GetNodeByName(lngFavorites);
            if (node != null) node.Remove();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            if (node != null) node.Remove();

            treeViewLabels.Nodes.Clear();
            //treeViewLabels.Controls.Clear();
            //treeViewLabels.Refresh();

            //mora ponovo da bi se otvorili
            node = treeViewFilters.GetNodeByName(lngFavorites);
            node.Expand();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            node.Expand();
            selectStandardFilterIfExist();
            gridCustomFilter = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\arrangement");
        }

        public void OnLedgerClassClick()
        {
            btnNew.Visibility = ElementVisibility.Visible;
            btnDelete.Visibility = ElementVisibility.Visible;
            radRibbonBarGroup1.Visibility = ElementVisibility.Visible;
            radRibbonBarYear.Visibility = ElementVisibility.Visible;
            splitPanel5.Controls.Clear();
        //    splitPanel8.Controls.Clear();
            splitPanelFavorites.Controls.Clear();
            splitPanelFavorites.Controls.Add(treeViewFilters);

            splitPanel6.Collapsed = true;
            splitPanelLabels.Collapsed = true;
          //  split_cont_right.Collapsed = true;
          //  splitPanel7.Collapsed = true;


            activeGrid = gridClass;
            activeGrid.bLoadTreeMenu = true;
            splitPanel5.Controls.Add(gridClass);
            splitPanel7.Controls.Clear();

            RadTreeNode node = treeViewFilters.GetNodeByName(lngFavorites);
            if (node != null) node.Remove();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            if (node != null) node.Remove();

            treeViewLabels.Nodes.Clear();
            treeViewLabels.Controls.Clear();
            treeViewLabels.Refresh();

            //mora ponovo da bi se otvorili
            node = treeViewFilters.GetNodeByName(lngFavorites);
            node.Expand();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            node.Expand();
            selectStandardFilterIfExist();

            gridCustomFilter = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\accountClass");

        }


        public void OnCostCentarClick()
        {
            btnNew.Visibility = ElementVisibility.Visible;
            btnDelete.Visibility = ElementVisibility.Visible;
            radRibbonBarGroup1.Visibility = ElementVisibility.Visible;
            radRibbonBarYear.Visibility = ElementVisibility.Visible;

            splitPanel5.Controls.Clear();
          //  splitPanel8.Controls.Clear();
            splitPanelFavorites.Controls.Clear();
            splitPanelFavorites.Controls.Add(treeViewFilters);


            activeGrid = gridViewCost;
            activeGrid.bLoadTreeMenu = true;
            splitPanel5.Controls.Add(gridViewCost);
            splitPanel7.Controls.Clear();

            RadTreeNode node = treeViewFilters.GetNodeByName(lngFavorites);
            if (node != null) node.Remove();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            if (node != null) node.Remove();

            treeViewLabels.Nodes.Clear();
            treeViewLabels.Controls.Clear();
            treeViewLabels.Refresh();

            //mora ponovo da bi se otvorili
            node = treeViewFilters.GetNodeByName(lngFavorites);
            node.Expand();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            node.Expand();
            selectStandardFilterIfExist();
            gridCustomFilter = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\costs");

        }

        public void OnDailyClick()
        {
            btnNew.Visibility = ElementVisibility.Visible;
            btnDelete.Visibility = ElementVisibility.Visible;
            radRibbonBarGroup1.Visibility = ElementVisibility.Visible;
            radRibbonBarYear.Visibility = ElementVisibility.Visible;

            splitPanel5.Controls.Clear();
          //  splitPanel8.Controls.Clear();
            splitPanelFavorites.Controls.Clear();
            splitPanelFavorites.Controls.Add(treeViewFilters);


            activeGrid = gridViewDaily;
            activeGrid.bLoadTreeMenu = true;

            AccDailyBUS acb = new AccDailyBUS(Login._bookyear);
            List<IModel> acm = new List<IModel>();

            acm = acb.GetAllDailys();
            gridViewDaily.SetDataPersonBinding1(acm);

            splitPanel5.Controls.Add(gridViewDaily);
            splitPanel7.Controls.Clear();
            
            //radRibbonBarGroupAccounting.Visibility = ElementVisibility.Visible;

            RadTreeNode node = treeViewFilters.GetNodeByName(lngFavorites);
            if (node != null) node.Remove();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            if (node != null) node.Remove();

            treeViewLabels.Nodes.Clear();
            treeViewLabels.Controls.Clear();
            treeViewLabels.Refresh();

            //mora ponovo da bi se otvorili
            node = treeViewFilters.GetNodeByName(lngFavorites);
            node.Expand();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            node.Expand();
            selectStandardFilterIfExist();
            gridCustomFilter = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\daily");

        }
        public void OnDailyEntryClick()
        {
        
            btnNew.Visibility = ElementVisibility.Collapsed;
            btnDelete.Visibility = ElementVisibility.Collapsed;
            radRibbonBarGroup1.Visibility = ElementVisibility.Hidden;
            radRibbonBarYear.Visibility = ElementVisibility.Visible;
            radRibbonBarGroup1.Text = "";

            splitPanel5.Controls.Clear();
        //    splitPanel8.Controls.Clear();
            splitPanelFavorites.Controls.Clear();
            splitPanelFavorites.Controls.Add(treeViewFilters);


            activeGrid = gridViewDailyEntry;
            activeGrid.bLoadTreeMenu = true;

            AccDailyBUS acb = new AccDailyBUS(Login._bookyear);
            List<IModel> acm = new List<IModel>();

            acm = acb.GetAllDailys();
            gridViewDailyEntry.SetDataPersonBinding(acm);

            splitPanel5.Controls.Add(gridViewDailyEntry);
            splitPanel7.Controls.Clear();
            
            //radRibbonBarGroupAccounting.Visibility = ElementVisibility.Visible;

            RadTreeNode node = treeViewFilters.GetNodeByName(lngFavorites);
            if (node != null) node.Remove();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            if (node != null) node.Remove();

            treeViewLabels.Nodes.Clear();
            treeViewLabels.Controls.Clear();
            treeViewLabels.Refresh();

            //mora ponovo da bi se otvorili
            node = treeViewFilters.GetNodeByName(lngFavorites);
            node.Expand();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            node.Expand();
            selectStandardFilterIfExist();
            gridCustomFilter = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\dailyentry");
           

        }
       

        public void OnApproveClick()
        {
            btnNew.Visibility = ElementVisibility.Collapsed;
            btnDelete.Visibility = ElementVisibility.Collapsed;
            radRibbonBarGroup1.Visibility = ElementVisibility.Collapsed;
            radRibbonBarYear.Visibility = ElementVisibility.Collapsed;

            splitPanel5.Controls.Clear();
            //    splitPanel8.Controls.Clear();
            splitPanelFavorites.Controls.Clear();
            splitPanelFavorites.Controls.Add(treeViewFilters);


            activeGrid = gridViewDailyEntry;
            activeGrid.bLoadTreeMenu = true;
            //  splitPanel5.Controls.Add(gridViewDailyEntry);
            splitPanel7.Controls.Clear();

            frmAccCreditorApprove fcpa = new frmAccCreditorApprove();
            fcpa.ShowDialog();

            //RadTreeNode node = treeViewFilters.GetNodeByName(lngFavorites);
            //if (node != null) node.Remove();
            //node = treeViewFilters.GetNodeByName(lngCustomFilters);
            //if (node != null) node.Remove();

            //treeViewLabels.Nodes.Clear();
            //treeViewLabels.Controls.Clear();
            //treeViewLabels.Refresh();

            ////mora ponovo da bi se otvorili
            //node = treeViewFilters.GetNodeByName(lngFavorites);
            //node.Expand();
            //node = treeViewFilters.GetNodeByName(lngCustomFilters);
            //node.Expand();
            //selectStandardFilterIfExist();
            //gridCustomFilter = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\dailyentry");

        }

        public void OnTaxClick()
        {
            btnNew.Visibility = ElementVisibility.Visible;
            btnDelete.Visibility = ElementVisibility.Visible;
            radRibbonBarGroup1.Visibility = ElementVisibility.Visible;
            radRibbonBarYear.Visibility = ElementVisibility.Visible;

            splitPanel5.Controls.Clear();
        //    splitPanel8.Controls.Clear();
            splitPanelFavorites.Controls.Clear();
            splitPanelFavorites.Controls.Add(treeViewFilters);


            activeGrid = gridViewTax;
            activeGrid.bLoadTreeMenu = true;
            splitPanel5.Controls.Add(gridViewTax);
            splitPanel7.Controls.Clear();
            
            //radRibbonBarGroupAccounting.Visibility = ElementVisibility.Visible;

            RadTreeNode node = treeViewFilters.GetNodeByName(lngFavorites);
            if (node != null) node.Remove();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            if (node != null) node.Remove();

            treeViewLabels.Nodes.Clear();
            treeViewLabels.Controls.Clear();
            treeViewLabels.Refresh();

            //mora ponovo da bi se otvorili
            node = treeViewFilters.GetNodeByName(lngFavorites);
            node.Expand();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            node.Expand();
            selectStandardFilterIfExist();
            gridCustomFilter = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\tax");

        }

        public void OnLedgerClick()
        {
            
           // splitPanel5.Controls.Clear();
           //// splitPanel8.Controls.Clear();
           // splitPanelFavorites.Controls.Clear();
           //splitPanelFavorites.Controls.Add(treeViewFilters);


           //activeGrid = gridViewLedger;
           ////==
           //modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
           //activeGrid.SetDataPersonBinding(modelData);
           ////==
           //activeGrid.bLoadTreeMenu = true;
           //splitPanel5.Controls.Add(gridViewLedger);
           //splitPanel7.Controls.Clear();

           // RadTreeNode node = treeViewFilters.GetNodeByName(lngFavorites);
           // if (node != null) node.Remove();
           // node = treeViewFilters.GetNodeByName(lngCustomFilters);
           // if (node != null) node.Remove();

           // treeViewLabels.Nodes.Clear();
           // treeViewLabels.Controls.Clear();
           // treeViewLabels.Refresh();

           // //mora ponovo da bi se otvorili
           // node = treeViewFilters.GetNodeByName(lngFavorites);
           // node.Expand();
           // node = treeViewFilters.GetNodeByName(lngCustomFilters);
           // node.Expand();
           // selectStandardFilterIfExist();
           // gridCustomFilter = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\ledger");
            btnNew.Visibility = ElementVisibility.Visible;
            btnDelete.Visibility = ElementVisibility.Visible;
            radRibbonBarGroup1.Visibility = ElementVisibility.Visible;
            radRibbonBarYear.Visibility = ElementVisibility.Visible;

            splitPanel5.Controls.Clear();
            //    splitPanel8.Controls.Clear();
            splitPanelFavorites.Controls.Clear();

            List<IModel> binding = new List<IModel>();  //=== ubaceno da kod svakog klika refresuje zbog godine
            binding = new LedgerAccountBUS(Login._bookyear).GetAllAccounts();

            gridViewLedger.SetDataPersonBinding(binding);

            activeGrid = gridViewLedger;
            activeGrid.bLoadTreeMenu = true;
            splitPanel5.Controls.Add(gridViewLedger);
            splitPanel7.Controls.Clear();
                      
            
            //radRibbonBarGroupAccounting.Visibility = ElementVisibility.Visible;

            RadTreeNode node = treeViewFilters.GetNodeByName(lngFavorites);
            if (node != null) node.Remove();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            if (node != null) node.Remove();

            treeViewLabels.Nodes.Clear();
            treeViewLabels.Controls.Clear();
            treeViewLabels.Refresh();

            //mora ponovo da bi se otvorili
            node = treeViewFilters.GetNodeByName(lngFavorites);
            node.Expand();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            node.Expand();
            selectStandardFilterIfExist();
            gridCustomFilter = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\ledger");

            splitPanelFavorites.Controls.Add(treeViewFilters);

        }

        public void OnArrClick()
        {
            btnNew.Visibility = ElementVisibility.Visible;
            btnDelete.Visibility = ElementVisibility.Visible;
            radRibbonBarGroup1.Visibility = ElementVisibility.Visible;

            radRibbonBarGroupReports.Visibility = ElementVisibility.Visible;
            radButtonElementPurchaseReport.Visibility = ElementVisibility.Visible;
            radButtonElementSellReport.Visibility = ElementVisibility.Collapsed;
            radButtonInvoiceSelection.Visibility = ElementVisibility.Collapsed;
            radButtonVolReports.Visibility = ElementVisibility.Collapsed;
            radRibbonBarGroupAccounting.Visibility = ElementVisibility.Collapsed;
            radRibbonBarYear.Visibility = ElementVisibility.Collapsed;

            splitPanel6.Collapsed = false;
            splitPanel8.Collapsed = true;

            splitPanel5.Controls.Clear();
            splitPanel8.Controls.Clear();
            splitPanelFavorites.Controls.Clear();
            this.activeGrid.ClearDescriptors();
            foreach (GridViewColumn col in this.activeGrid.Columns)
            {
                col.IsVisible = true;
            }
            splitPanelFavorites.Controls.Add(treeViewFilters);

            gridViewArrangement.onClickReference = 1;


            activeGrid = gridViewArrangement;
            gridViewArrangement.setFiltersAndLabels();
            activeGrid.bLoadTreeMenu = true;
            splitPanel5.Controls.Add(gridViewArrangement);
            splitPanel7.Controls.Clear();
            splitPanel7.Controls.Add(arrdetailview);

            RadTreeNode node = treeViewFilters.GetNodeByName(lngFavorites);
            if (node != null) node.Remove();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            if (node != null) node.Remove();

            treeViewLabels.Nodes.Clear();
            treeViewLabels.Controls.Clear();
            treeViewLabels.Refresh();

            //mora ponovo da bi se otvorili
            node = treeViewFilters.GetNodeByName(lngFavorites);
            node.Expand();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            node.Expand();
            selectStandardFilterIfExist();
            gridCustomFilter = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\arrangement");

        }

        public void OnTourleadingClick()
        {
            //btnNew.Visibility = ElementVisibility.Visible;
            //btnDelete.Visibility = ElementVisibility.Visible;
            radRibbonBarGroup1.Visibility = ElementVisibility.Collapsed;

            radRibbonBarGroupReports.Visibility = ElementVisibility.Visible;
            radButtonElementPurchaseReport.Visibility = ElementVisibility.Collapsed;
            radButtonElementSellReport.Visibility = ElementVisibility.Visible;
            radButtonInvoiceSelection.Visibility = ElementVisibility.Visible;
            radButtonVolReports.Visibility = ElementVisibility.Collapsed;
            radButtonSearchAndBooking.Visibility = ElementVisibility.Collapsed;
            radRibbonBarGroupAccounting.Visibility = ElementVisibility.Collapsed;
            radRibbonBarYear.Visibility = ElementVisibility.Collapsed;

            splitPanel5.Controls.Clear();
            splitPanel8.Controls.Clear();
            splitPanelFavorites.Controls.Clear();
            this.activeGrid.ClearDescriptors();
            foreach (GridViewColumn col in this.activeGrid.Columns)
            {
                col.IsVisible = true;
            }
            splitPanelFavorites.Controls.Add(treeViewFilters);

            gridViewArrangement.onClickReference = 3;
            activeGrid = gridViewArrangement;
            gridViewArrangement.setFiltersAndLabels();
            activeGrid.bLoadTreeMenu = true;
            splitPanel5.Controls.Add(gridViewArrangement);
            splitPanel7.Controls.Clear();



            RadTreeNode node = treeViewFilters.GetNodeByName(lngFavorites);
            if (node != null) node.Remove();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            if (node != null) node.Remove();

            treeViewLabels.Nodes.Clear();
            treeViewLabels.Controls.Clear();
            treeViewLabels.Refresh();

            //mora ponovo da bi se otvorili
            node = treeViewFilters.GetNodeByName(lngFavorites);
            node.Expand();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            node.Expand();
            selectStandardFilterIfExist();
            gridCustomFilter = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\arrangementTraveler");

        }

        public void OnArticalsClick()
        {
            btnNew.Visibility = ElementVisibility.Visible;
            btnDelete.Visibility = ElementVisibility.Visible;
            radRibbonBarGroup1.Visibility = ElementVisibility.Visible;

            radRibbonBarGroupReports.Visibility = ElementVisibility.Collapsed;
            radRibbonBarYear.Visibility = ElementVisibility.Collapsed;
            splitPanel5.Controls.Clear();
            splitPanel8.Controls.Clear();
            splitPanelFavorites.Controls.Clear();
            splitPanelFavorites.Controls.Add(treeViewFilters);


            activeGrid = gridViewArtical;
            activeGrid.bLoadTreeMenu = true;
            splitPanel5.Controls.Add(gridViewArtical);
            splitPanel7.Controls.Clear();

            RadTreeNode node = treeViewFilters.GetNodeByName(lngFavorites);
            if (node != null) node.Remove();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            if (node != null) node.Remove();

            treeViewLabels.Nodes.Clear();
            treeViewLabels.Controls.Clear();
            treeViewLabels.Refresh();

            //mora ponovo da bi se otvorili
            node = treeViewFilters.GetNodeByName(lngFavorites);
            node.Expand();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            node.Expand();
            selectStandardFilterIfExist();
            gridCustomFilter = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\artical");
        }


        public void OnUsersClick()
        {
            btnNew.Visibility = ElementVisibility.Visible;
            btnDelete.Visibility = ElementVisibility.Visible;
            radRibbonBarGroup1.Visibility = ElementVisibility.Visible;

            radRibbonBarGroupReports.Visibility = ElementVisibility.Collapsed;
            radRibbonBarYear.Visibility = ElementVisibility.Collapsed;
        //    splitPanelAccounting.Controls.Clear();
            splitPanel7.Controls.Clear();
            splitPanel5.Controls.Clear();

            activeGrid = gridViewUsers;
            activeGrid.bLoadTreeMenu = true;
            splitPanel5.Controls.Add(gridViewUsers);

            RadTreeNode node = treeViewFilters.GetNodeByName(lngFavorites);
            if (node != null) node.Remove();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            if (node != null) node.Remove();

            treeViewLabels.Nodes.Clear();
            treeViewLabels.Controls.Clear();
            treeViewLabels.Refresh();

            //mora ponovo da bi se otvorili
            node = treeViewFilters.GetNodeByName(lngFavorites);
            node.Expand();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            node.Expand();

            splitPanel8.Controls.Clear();

            splitPanelFavorites.Controls.Clear();
            splitPanelFavorites.Controls.Add(treeViewFilters);

            selectStandardFilterIfExist();
            gridCustomFilter = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\users");

        }

        public void OnMedicalClick()
        {
            btnNew.Visibility = ElementVisibility.Visible;
            btnDelete.Visibility = ElementVisibility.Visible;
            radRibbonBarGroup1.Visibility = ElementVisibility.Visible;
            radRibbonBarYear.Visibility = ElementVisibility.Collapsed;

            // splitPanelAccounting.Controls.Clear();
            splitPanel7.Controls.Clear();
            splitPanel5.Controls.Clear();

            activeGrid = gridViewMedical;
            activeGrid.bLoadTreeMenu = true;
            splitPanel5.Controls.Add(gridViewMedical);

            splitPanelFavorites.Controls.Clear();
            splitPanelFavorites.Controls.Add(treeViewFilters);

            RadTreeNode node = treeViewFilters.GetNodeByName(lngFavorites);
            if (node != null) node.Remove();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            if (node != null) node.Remove();

            treeViewLabels.Nodes.Clear();
            // treeViewLabels.Controls.Clear();
            treeViewLabels.Refresh();

            if (treeViewLabels.TreeViewElement.GetNodeCount(false) == 0)
            {
                splitPanelLabels.Collapsed = true;
            }
            else
            {
                splitPanelLabels.Collapsed = false;
            }

            //mora ponovo da bi se otvorili
            node = treeViewFilters.GetNodeByName(lngFavorites);
            if (node != null) node.Remove();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            if (node != null) node.Remove();

            splitPanel8.Controls.Clear();



            //if (rl != null)
            //rl.SelectedNode = null;
            selectStandardFilterIfExist();
            gridCustomFilter = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\medical");

        }

        public void OnVoluntaryClick()
        {
            btnNew.Visibility = ElementVisibility.Visible;
            btnDelete.Visibility = ElementVisibility.Visible;
            radRibbonBarGroup1.Visibility = ElementVisibility.Visible;
            radRibbonBarYear.Visibility = ElementVisibility.Collapsed;

            // splitPanelAccounting.Controls.Clear();
            splitPanel7.Controls.Clear();
            splitPanel5.Controls.Clear();

            activeGrid = gridViewVoluntary;
            activeGrid.bLoadTreeMenu = true;
            splitPanel5.Controls.Add(gridViewVoluntary);

            splitPanelFavorites.Controls.Clear();
            splitPanelFavorites.Controls.Add(treeViewFilters);

            RadTreeNode node = treeViewFilters.GetNodeByName(lngFavorites);
            if (node != null) node.Remove();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            if (node != null) node.Remove();

            treeViewLabels.Nodes.Clear();
            // treeViewLabels.Controls.Clear();
            treeViewLabels.Refresh();

            if (treeViewLabels.TreeViewElement.GetNodeCount(false) == 0)
            {
                splitPanelLabels.Collapsed = true;
            }
            else
            {
                splitPanelLabels.Collapsed = false;
            }

            //mora ponovo da bi se otvorili
            node = treeViewFilters.GetNodeByName(lngFavorites);
            node.Expand();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            node.Expand();

            splitPanel8.Controls.Clear();

            selectStandardFilterIfExist();
            gridCustomFilter = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\voluntary");

        }

        public void OnVoluntaryFuncClick()
        {
            btnNew.Visibility = ElementVisibility.Visible;
            btnDelete.Visibility = ElementVisibility.Visible;
            radRibbonBarGroup1.Visibility = ElementVisibility.Visible;
            radRibbonBarYear.Visibility = ElementVisibility.Collapsed;

            // splitPanelAccounting.Controls.Clear();
            splitPanel7.Controls.Clear();
            splitPanel5.Controls.Clear();

            activeGrid = gridViewVoluntaryFunc;
            activeGrid.bLoadTreeMenu = true;
            splitPanel5.Controls.Add(gridViewVoluntaryFunc);

            splitPanelFavorites.Controls.Clear();
            splitPanelFavorites.Controls.Add(treeViewFilters);

            RadTreeNode node = treeViewFilters.GetNodeByName(lngFavorites);
            if (node != null) node.Remove();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            if (node != null) node.Remove();

            treeViewLabels.Nodes.Clear();
            // treeViewLabels.Controls.Clear();
            treeViewLabels.Refresh();

            if (treeViewLabels.TreeViewElement.GetNodeCount(false) == 0)
            {
                splitPanelLabels.Collapsed = true;
            }
            else
            {
                splitPanelLabels.Collapsed = false;
            }

            //mora ponovo da bi se otvorili
            node = treeViewFilters.GetNodeByName(lngFavorites);
            node.Expand();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            node.Expand();

            splitPanel8.Controls.Clear();

            selectStandardFilterIfExist();
            gridCustomFilter = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\voluntaryFunc");

        }
        public void OnVoluntaryTripClick()
        {
            btnNew.Visibility = ElementVisibility.Visible;
            btnDelete.Visibility = ElementVisibility.Visible;
            radRibbonBarGroup1.Visibility = ElementVisibility.Visible;
            radRibbonBarYear.Visibility = ElementVisibility.Collapsed;

            // splitPanelAccounting.Controls.Clear();
            splitPanel7.Controls.Clear();
            splitPanel5.Controls.Clear();

            activeGrid = gridViewVoluntaryTrip;
            activeGrid.bLoadTreeMenu = true;
            splitPanel5.Controls.Add(gridViewVoluntaryTrip);

            splitPanelFavorites.Controls.Clear();
            splitPanelFavorites.Controls.Add(treeViewFilters);

            RadTreeNode node = treeViewFilters.GetNodeByName(lngFavorites);
            if (node != null) node.Remove();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            if (node != null) node.Remove();

            treeViewLabels.Nodes.Clear();
            // treeViewLabels.Controls.Clear();
            treeViewLabels.Refresh();

            if (treeViewLabels.TreeViewElement.GetNodeCount(false) == 0)
            {
                splitPanelLabels.Collapsed = true;
            }
            else
            {
                splitPanelLabels.Collapsed = false;
            }

            //mora ponovo da bi se otvorili
            node = treeViewFilters.GetNodeByName(lngFavorites);
            node.Expand();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            node.Expand();

            splitPanel8.Controls.Clear();


            selectStandardFilterIfExist();
            gridCustomFilter = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\voluntaryTrip");

        }

        public void OnEmployeeClick()

        {
            splitPanelAccounting.Collapsed = true;
            this.splitPanel1.SizeInfo.MinimumSize = new Size(10, 0);
            this.splitPanel1.SizeInfo.MaximumSize = new Size(220, 0);

            this.splitPanel6.SizeInfo.MinimumSize = new Size(10, 0);
         //   this.splitPanel6.SizeInfo.MaximumSize = new Size(250, 0);

            this.splitPanelMenu.SizeInfo.MinimumSize = new Size(10, 120);
            this.splitPanelMenu.SizeInfo.MaximumSize = new Size(220, 120);
            splitPanelLabels.Collapsed = false;
            splitPanel6.Collapsed = true;



            btnNew.Visibility = ElementVisibility.Visible;
            btnDelete.Visibility = ElementVisibility.Visible;
            radRibbonBarGroup1.Visibility = ElementVisibility.Visible;

            radRibbonBarGroupReports.Visibility = ElementVisibility.Collapsed;
            radRibbonBarGroupAccounting.Visibility = ElementVisibility.Collapsed;
            radRibbonBarYear.Visibility = ElementVisibility.Collapsed;
//
            //splitPanel6.Collapsed = false;

            splitPanelAccounting.Controls.Clear();
            splitPanelFavorites.Controls.Clear();
            splitPanelFavorites.Controls.Add(treeViewFilters);

            splitPanel7.Controls.Clear();
            splitPanel5.Controls.Clear();
            activeGrid = gridViewEmployee;
            activeGrid.bLoadTreeMenu = true;
            splitPanel5.Controls.Add(gridViewEmployee);

            RadTreeNode node = treeViewFilters.GetNodeByName(lngFavorites);
            if (node != null) node.Remove();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            if (node != null) node.Remove();

            treeViewLabels.Nodes.Clear();
            treeViewLabels.Controls.Clear();
            treeViewLabels.Refresh();

            splitPanel8.Controls.Clear();

            splitPanelFavorites.Controls.Clear();
            splitPanelFavorites.Controls.Add(treeViewFilters);

            //if (rl != null)
            //rl.SelectedNode = null;
            selectStandardFilterIfExist();
            gridCustomFilter = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\employees");

        }
        public void OnFilterLabelClick()
        {
            btnNew.Visibility = ElementVisibility.Visible;
            btnDelete.Visibility = ElementVisibility.Visible;
            radRibbonBarGroup1.Visibility = ElementVisibility.Visible;
            radRibbonBarYear.Visibility = ElementVisibility.Collapsed; 

          //  splitPanelAccounting.Controls.Clear();
            splitPanel7.Controls.Clear();
            splitPanel5.Controls.Clear();

            activeGrid = gridViewFilterLabel;
            activeGrid.bLoadTreeMenu = true;
            splitPanel5.Controls.Add(gridViewFilterLabel);

            RadTreeNode node = treeViewFilters.GetNodeByName(lngFavorites);
            if (node != null) node.Remove();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            if (node != null) node.Remove();

            treeViewLabels.Nodes.Clear();
            treeViewLabels.Controls.Clear();
            treeViewLabels.Refresh();

            //mora ponovo da bi se otvorili
            node = treeViewFilters.GetNodeByName(lngFavorites);
            node.Expand();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            node.Expand();

            splitPanel8.Controls.Clear();

            splitPanelFavorites.Controls.Clear();
            splitPanelFavorites.Controls.Add(treeViewFilters);

            selectStandardFilterIfExist();
            gridCustomFilter = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\filtersLabels");
        }

        public void OnProjectClick()
        {

            splitPanel6.Collapsed = true;
            //btnNew.Visibility = ElementVisibility.Visible;
            //btnDelete.Visibility = ElementVisibility.Visible;
            radRibbonBarGroup1.Visibility = ElementVisibility.Collapsed;
            radRibbonBarGroupReports.Visibility = ElementVisibility.Collapsed;
            radRibbonBarGroupAccounting.Visibility = ElementVisibility.Collapsed;
            radRibbonBarYear.Visibility = ElementVisibility.Collapsed;

            splitPanelAccounting.Controls.Clear();
            splitPanelFavorites.Controls.Clear();
            splitPanelFavorites.Controls.Add(treeViewFilters);

            splitPanel7.Controls.Clear();
            splitPanel5.Controls.Clear();
            //activeGrid = gridViewEmployee;
            activeGrid.bLoadTreeMenu = true;
            //splitPanel5.Controls.Add(gridViewEmployee);

            RadTreeNode node = treeViewFilters.GetNodeByName(lngFavorites);
            if (node != null) node.Remove();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            if (node != null) node.Remove();

            treeViewLabels.Nodes.Clear();
            treeViewLabels.Controls.Clear();
            treeViewLabels.Refresh();

            //mora ponovo da bi se otvorili
            node = treeViewFilters.GetNodeByName(lngFavorites);
            node.Expand();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            node.Expand();

            splitPanel8.Controls.Clear();

        }

       

            public void OnPortalClick()
        {

            splitPanelAccounting.Collapsed = false;
            this.splitPanel1.SizeInfo.MinimumSize = new Size(10, 0);
            this.splitPanel1.SizeInfo.MaximumSize = new Size(220, 0);

            this.splitPanel6.SizeInfo.MinimumSize = new Size(10, 0);
            this.splitPanel6.SizeInfo.MaximumSize = new Size(250, 0);

            this.splitPanel7.SizeInfo.MinimumSize = new Size(10, 493);
          //  this.splitPanel7.SizeInfo.MaximumSize = new Size(250, 493);

            this.splitPanel8.SizeInfo.MinimumSize = new Size(10, 284);
           // this.splitPanel8.SizeInfo.MaximumSize = new Size(250, 284);

            this.splitPanelMenu.SizeInfo.MinimumSize = new Size(10, 120);
            this.splitPanelMenu.SizeInfo.MaximumSize = new Size(220, 120);
            splitPanelLabels.Collapsed = false;
            splitPanel6.Collapsed = false;

            splitPanel8.Collapsed = false;

            btnNew.Visibility = ElementVisibility.Visible;
            btnDelete.Visibility = ElementVisibility.Visible;
            radRibbonBarGroup1.Visibility = ElementVisibility.Collapsed;

            radRibbonBarGroupReports.Visibility = ElementVisibility.Collapsed;
            radRibbonBarGroupAccounting.Visibility = ElementVisibility.Collapsed;
            radRibbonBarYear.Visibility = ElementVisibility.Collapsed;

            splitPanelAccounting.Controls.Clear();
//
            splitPanel6.Collapsed = false;

            splitPanel5.Controls.Clear();
            splitPanel5.Controls.Add(meetingScheduler);

            splitPanel7.Controls.Clear();
            taskDetailView = new TasksDetailView();
            taskDetailView.Dock = DockStyle.Fill;
            splitPanel7.Controls.Add(taskDetailView);

            splitPanel8.Controls.Clear();
            contactsDetailView = new ContactsDetailView();
            contactsDetailView.Dock = DockStyle.Fill;
            splitPanel8.Controls.Add(contactsDetailView);
            splitPanel8.Refresh();

            treeViewLabels.Nodes.Clear();
            treeViewLabels.Controls.Clear();
            treeViewLabels.Refresh();
            treeViewFilters.Nodes.Clear();
            treeViewFilters.Controls.Clear();
            treeViewFilters.Refresh();

            //MenuBUS mtBus = new MenuBUS();
            //List<MenuModel> menusBUS = mtBus.GetMenus(30, Login._user.idUser, Login._user.lngUser);

            //rl = new RadTreeView();
            //rl.DataSource = menusBUS;
            //rl.DisplayMember = "nameMenu\\nameMenu";
            //rl.ChildMember = "Categories\\subMenus";

            //rl.SelectedNodeChanged += rl_SelectedNodeChanged;
            //rl.NodeDataBound += rl_NodeDataBound;
            //rl.CreateNodeElement += rl_CreateNodeElement;
            //rl.NodeFormatting += rl_NodeFormatting;

            //rl.Dock = System.Windows.Forms.DockStyle.Fill;
            //rl.ThemeName = "Windows8";
            //rl.ImageList = imageMenuTree;

            splitPanelAccounting.Controls.Clear();
            splitPanelAccounting.Controls.Add(portalTreeMenu);

            // rl.ExpandAll();
            //rl.CollapseAll();
            //if (rl.Nodes.Count > 2)
            //    rl.Nodes[2].Expand();

            //if (rl != null)
            //    rl.SelectedNode = null;

        }

        public void OnEmptyAccountClick()
        {
            treeViewLabels.Nodes.Clear();
            treeViewLabels.Controls.Clear();
            treeViewLabels.Refresh();

            splitPanel5.Controls.Clear();
            splitPanel5.Controls.Add(null);

            splitPanelLabels.Collapsed = true;
            splitPanel7.Controls.Clear();

            splitPanel6.Collapsed = true;
            splitPanel8.Collapsed = true;
            splitPanelLabels.Collapsed = true;
            splitPanel7.Refresh();
            
           // radRibbonBarGroupAccounting.Visibility = ElementVisibility.Visible;
           // splitPanel8.Controls.Clear();

            treeViewFilters.Nodes.Clear();
            treeViewFilters.Controls.Clear();
            treeViewFilters.Refresh();
            
        }


        public void OnEmptySaleClick()
        {
            radRibbonBarGroupReports.Visibility = ElementVisibility.Collapsed;
            splitPanel5.Controls.Clear();
            splitPanel5.Controls.Add(null);
          
            splitPanel7.Controls.Clear();
           
          
            
            // splitPanel8.Controls.Clear();
            splitPanel6.Collapsed = false;
            splitPanel8.Collapsed = true;
            splitPanelLabels.Collapsed = true;
            splitPanel7.Refresh();

            treeViewLabels.Nodes.Clear();
            treeViewLabels.Controls.Clear();
            treeViewLabels.Refresh();
            treeViewFilters.Nodes.Clear();
            treeViewFilters.Controls.Clear();
            treeViewFilters.Refresh();
        }

        public void OnAccountClick()
        {
            splitPanelAccounting.Collapsed = false;
            this.splitPanel1.SizeInfo.MinimumSize = new Size(10, 0);
            this.splitPanel1.SizeInfo.MaximumSize = new Size(220, 0);

            this.splitPanel6.SizeInfo.MinimumSize = new Size(10, 0);
            this.splitPanel6.SizeInfo.MaximumSize = new Size(250, 0);

            this.splitPanelMenu.SizeInfo.MinimumSize = new Size(10, 120);
            this.splitPanelMenu.SizeInfo.MaximumSize = new Size(220, 120);


            treeViewLabels.Nodes.Clear();
            treeViewLabels.Controls.Clear();
            splitPanelLabels.Collapsed = true;
          //  treeViewLabels.Refresh();

            OnEmptyAccountClick();
            splitPanelLabels.Collapsed = true;
            splitPanelFavorites.Dock = DockStyle.Fill;

            btnNew.Visibility = ElementVisibility.Visible;
            btnDelete.Visibility = ElementVisibility.Visible;
            radRibbonBarGroup1.Visibility = ElementVisibility.Visible;
            radRibbonBarYear.Visibility = ElementVisibility.Visible;

            radRibbonBarGroupReports.Visibility = ElementVisibility.Collapsed;
            radRibbonBarGroup1.Visibility = ElementVisibility.Collapsed;
            //=====
           // BookYear bky = new BookYear();  //this
            splitPanel6.Collapsed = true;
            splitPanel8.Collapsed = true;
          
           
            treeViewFilters.Nodes.Clear();
            treeViewFilters.Controls.Clear();
            treeViewFilters.Refresh();

            splitPanelFavorites.Controls.Add(treeViewFilters);

           
           // bky.ddlBook.SelectedIndexChanged += bky.ddlBook_SelectedIndexChanged;
            splitPanelFavorites.Controls.Clear();
            splitPanel8.Controls.Clear();
            //== ubacuje u panel 8 combo
          //  splitPanel8.Controls.Add(bookyear);
            //==
            splitPanel5.Controls.Clear();
            splitPanel5.Controls.Add(null);

            splitPanel7.Controls.Clear();
            
            radRibbonBarGroupAccounting.Visibility = ElementVisibility.Visible;

            ////treeViewLabels.Nodes.Clear();
            ////treeViewLabels.Controls.Clear();
            ////treeViewLabels.Refresh();
           

           // MenuBUS mtBus = new MenuBUS();
            //List<MenuModel> menusBUS = mtBus.GetMenus(29, Login._user.idUser, Login._user.lngUser);

            //rl = new RadTreeView();
            //rl.DataSource = menusBUS;
            //rl.DisplayMember = "nameMenu\\nameMenu";
            //rl.ChildMember = "Categories\\subMenus";

            //rl.SelectedNodeChanged += rl_SelectedNodeChanged;
            //rl.NodeDataBound += rl_NodeDataBound;
            //rl.CreateNodeElement += rl_CreateNodeElement;
            //rl.NodeFormatting += rl_NodeFormatting;

            //rl.Dock = System.Windows.Forms.DockStyle.Fill;
            //rl.ThemeName = "Windows8";
            //rl.ImageList = imageMenuTree;


            splitPanelAccounting.Controls.Clear();
            splitPanelAccounting.Controls.Add(accountingTreeMenu);

            accountingTreeMenu.CollapseAll();
            if (accountingTreeMenu.Nodes.Count > 2)
                accountingTreeMenu.Nodes[2].Expand();

            if (accountingTreeMenu != null)
                accountingTreeMenu.SelectedNode = null;


           
            //if (rl != null)
                //rl.SelectedNode = null;
        }
        public void OnArrangementClick()

        {
            splitPanelAccounting.Collapsed = false;
            this.splitPanel1.SizeInfo.MinimumSize = new Size(10, 0);
            this.splitPanel1.SizeInfo.MaximumSize = new Size(220, 0);

            this.splitPanel6.SizeInfo.MinimumSize = new Size(10, 0);
            this.splitPanel6.SizeInfo.MaximumSize = new Size(250, 0);

            this.splitPanelMenu.SizeInfo.MinimumSize = new Size(10, 120);
            this.splitPanelMenu.SizeInfo.MaximumSize = new Size(220, 120);
            splitPanelLabels.Collapsed = false;

            splitPanel6.Collapsed = false;
            splitPanel8.Collapsed = true;

            btnNew.Visibility = ElementVisibility.Visible;
            btnDelete.Visibility = ElementVisibility.Visible;
            radRibbonBarGroup1.Visibility = ElementVisibility.Collapsed;
            radRibbonBarGroupReports.Visibility = ElementVisibility.Collapsed;
            radRibbonBarGroupAccounting.Visibility = ElementVisibility.Collapsed;
            radRibbonBarYear.Visibility = ElementVisibility.Collapsed;
            
            splitPanelFavorites.Controls.Clear();

            splitPanel8.Controls.Clear();
            splitPanel5.Controls.Clear();
            splitPanel5.Controls.Add(null);

            splitPanel7.Controls.Clear();

            treeViewLabels.Nodes.Clear();
            treeViewLabels.Controls.Clear();
            treeViewLabels.Refresh();
            treeViewFilters.Nodes.Clear();
            treeViewFilters.Controls.Clear();
            treeViewFilters.Refresh();

            MenuBUS mtBus = new MenuBUS();
            List<MenuModel> menusBUS = mtBus.GetMenus(28, Login._user.idUser, Login._user.lngUser);

            rl = new RadTreeView();
            rl.DataSource = menusBUS;
            rl.DisplayMember = "nameMenu\\nameMenu";
            rl.ChildMember = "Categories\\subMenus";

            

            rl.SelectedNodeChanged += rl_SelectedNodeChanged;
            rl.NodeDataBound += rl_NodeDataBound;
            rl.CreateNodeElement += rl_CreateNodeElement;
            rl.NodeFormatting += rl_NodeFormatting;

            rl.Dock = System.Windows.Forms.DockStyle.Fill;
           // rl.ThemeName = "Windows8";
            rl.ImageList = imageMenuTree;

            splitPanelAccounting.Controls.Clear();
            splitPanelAccounting.Controls.Add(rl);

            if (rl != null)
                rl.SelectedNode = null;
            selectStandardFilterIfExist();
            gridCustomFilter = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\arrange");
        }

        //public void OnAdminClick()
        //{
        //    btnNew.Visibility = ElementVisibility.Visible;
        //    btnDelete.Visibility = ElementVisibility.Visible;
        //    radRibbonBarGroup1.Visibility = ElementVisibility.Visible;


        //    splitPanelFavorites.Controls.Clear();
        //    splitPanel8.Controls.Clear();
        //    splitPanel5.Controls.Clear();
        //    splitPanel5.Controls.Add(null);

        //    splitPanel7.Controls.Clear();

        //    treeViewLabels.Nodes.Clear();
        //    treeViewLabels.Controls.Clear();
        //    treeViewLabels.Refresh();
        //    treeViewFilters.Nodes.Clear();
        //    treeViewFilters.Controls.Clear();
        //    treeViewFilters.Refresh();


        //    MenuBUS mtBus = new MenuBUS();
        //    List<MenuModel> menusBUS = mtBus.GetMenus(33, Login._user.idUser, Login._user.lngUser);

        //    rl = new RadTreeView();
        //    rl.DataSource = menusBUS;
        //    rl.DisplayMember = "nameMenu\\nameMenu";
        //    rl.ChildMember = "Categories\\subMenus";

        //    rl.SelectedNodeChanged += rl_SelectedNodeChanged;
        //    rl.NodeDataBound += rl_NodeDataBound;
        //    rl.CreateNodeElement += rl_CreateNodeElement;
        //    rl.NodeFormatting += rl_NodeFormatting;

        //    rl.Dock = System.Windows.Forms.DockStyle.Fill;
        //    rl.ThemeName = "Windows8";
        //    rl.ImageList = imageMenuTree;

        //    splitPanelAccounting.Controls.Clear();
        //    splitPanelAccounting.Controls.Add(rl);

        //    rl.ExpandAll();

        //    if (rl != null)
        //        rl.SelectedNode = null;


        //}

        public void OnAdminClick()
        {
            splitPanelAccounting.Collapsed = false;
            this.splitPanel1.SizeInfo.MinimumSize = new Size(10, 0);
            this.splitPanel1.SizeInfo.MaximumSize = new Size(220, 0);

            this.splitPanel6.SizeInfo.MinimumSize = new Size(10, 0);
            this.splitPanel6.SizeInfo.MaximumSize = new Size(250, 0);

            this.splitPanelMenu.SizeInfo.MinimumSize = new Size(10, 120);
            this.splitPanelMenu.SizeInfo.MaximumSize = new Size(220, 120);
            splitPanelLabels.Collapsed = false;
            splitPanel6.Collapsed = false;


            btnNew.Visibility = ElementVisibility.Visible;
            btnDelete.Visibility = ElementVisibility.Visible;
            radRibbonBarGroup1.Visibility = ElementVisibility.Collapsed;
            radRibbonBarGroupReports.Visibility = ElementVisibility.Collapsed;
            radRibbonBarGroupAccounting.Visibility = ElementVisibility.Collapsed;
            radRibbonBarYear.Visibility = ElementVisibility.Collapsed;

            splitPanelFavorites.Controls.Clear();
            splitPanel8.Controls.Clear();
            splitPanel5.Controls.Clear();
            splitPanel5.Controls.Add(null);

            splitPanel7.Controls.Clear();

            treeViewLabels.Nodes.Clear();
            treeViewLabels.Controls.Clear();
            treeViewLabels.Refresh();
            treeViewFilters.Nodes.Clear();
            treeViewFilters.Controls.Clear();
            treeViewFilters.Refresh();

            noGridSituation();


            MenuBUS mtBus = new MenuBUS();
            List<MenuModel> menusBUS = mtBus.GetMenus(33, Login._user.idUser, Login._user.lngUser);

            rl = new RadTreeView();
            rl.DataSource = menusBUS;
            rl.DisplayMember = "nameMenu\\nameMenu";
            rl.ChildMember = "Categories\\subMenus";

            rl.SelectedNodeChanged += rl_SelectedNodeChanged;
            rl.NodeDataBound += rl_NodeDataBound;
            rl.CreateNodeElement += rl_CreateNodeElement;
            rl.NodeFormatting += rl_NodeFormatting;

            rl.Dock = System.Windows.Forms.DockStyle.Fill;
         //   rl.ThemeName = "Windows8";
            rl.ImageList = imageMenuTree;

            splitPanelAccounting.Controls.Clear();
            splitPanelAccounting.Controls.Add(rl);

            rl.ExpandAll();

            if (rl != null)
                rl.SelectedNode = null;


        }

        public void OnArticalGroupsClick()
        {
            btnNew.Visibility = ElementVisibility.Visible;
            btnDelete.Visibility = ElementVisibility.Visible;
            radRibbonBarGroup1.Visibility = ElementVisibility.Visible;

            radRibbonBarGroupReports.Visibility = ElementVisibility.Collapsed;
            radRibbonBarGroupAccounting.Visibility = ElementVisibility.Collapsed;
            radRibbonBarYear.Visibility = ElementVisibility.Collapsed;

            splitPanel5.Controls.Clear();
            splitPanel8.Controls.Clear();
            splitPanelFavorites.Controls.Clear();
            splitPanelFavorites.Controls.Add(treeViewFilters);


            activeGrid = gridViewArticalGroups;
            activeGrid.bLoadTreeMenu = true;
            splitPanel5.Controls.Add(gridViewArticalGroups);
            splitPanel7.Controls.Clear();

            RadTreeNode node = treeViewFilters.GetNodeByName(lngFavorites);
            if (node != null) node.Remove();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            if (node != null) node.Remove();

            treeViewLabels.Nodes.Clear();
            treeViewLabels.Controls.Clear();
            treeViewLabels.Refresh();

            //mora ponovo da bi se otvorili
            node = treeViewFilters.GetNodeByName(lngFavorites);
            node.Expand();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            node.Expand();
            selectStandardFilterIfExist();
            gridCustomFilter = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\articalgroups");

        }

        public void OnTypeClick()
        {
           // splitPanelAccounting.Controls.Clear();
            btnNew.Visibility = ElementVisibility.Visible;
            btnDelete.Visibility = ElementVisibility.Visible;
            radRibbonBarGroup1.Visibility = ElementVisibility.Visible;
            radRibbonBarYear.Visibility = ElementVisibility.Collapsed;

            splitPanel7.Controls.Clear();
            splitPanel5.Controls.Clear();

            activeGrid = gridViewType;
            activeGrid.bLoadTreeMenu = true;
            splitPanel5.Controls.Add(gridViewType);

            RadTreeNode node = treeViewFilters.GetNodeByName(lngFavorites);
            if (node != null) node.Remove();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            if (node != null) node.Remove();

            treeViewLabels.Nodes.Clear();
            treeViewLabels.Controls.Clear();
            treeViewLabels.Refresh();

            //mora ponovo da bi se otvorili
            node = treeViewFilters.GetNodeByName(lngFavorites);
            node.Expand();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            node.Expand();

            splitPanel8.Controls.Clear();

            splitPanelFavorites.Controls.Clear();
            splitPanelFavorites.Controls.Add(treeViewFilters);

            //if (rl != null)
            //rl.SelectedNode = null;
            selectStandardFilterIfExist();
            gridCustomFilter = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\type");
        }

        public void OnTranslationClick()
        {
          //  splitPanelAccounting.Controls.Clear();
            btnNew.Visibility = ElementVisibility.Visible;
            btnDelete.Visibility = ElementVisibility.Visible;
            radRibbonBarGroup1.Visibility = ElementVisibility.Visible;
            radRibbonBarYear.Visibility = ElementVisibility.Collapsed;

            splitPanel7.Controls.Clear();
            splitPanel5.Controls.Clear();

            activeGrid = gridViewTranslation;
            activeGrid.bLoadTreeMenu = true;
            splitPanel5.Controls.Add(gridViewTranslation);

            RadTreeNode node = treeViewFilters.GetNodeByName(lngFavorites);
            if (node != null) node.Remove();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            if (node != null) node.Remove();

            treeViewLabels.Nodes.Clear();
            treeViewLabels.Controls.Clear();
            treeViewLabels.Refresh();

            //mora ponovo da bi se otvorili
            node = treeViewFilters.GetNodeByName(lngFavorites);
            node.Expand();
            node = treeViewFilters.GetNodeByName(lngCustomFilters);
            node.Expand();

            splitPanel8.Controls.Clear();

            splitPanelFavorites.Controls.Clear();
            splitPanelFavorites.Controls.Add(treeViewFilters);

            //if (rl != null)
            //rl.SelectedNode = null;
            selectStandardFilterIfExist();
            gridCustomFilter = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\translation");
        }

        public void OnEmptyBookmarkClick()
        {
            btnNew.Visibility = ElementVisibility.Visible;
            btnDelete.Visibility = ElementVisibility.Visible;
            radRibbonBarGroup1.Visibility = ElementVisibility.Visible;
            radRibbonBarYear.Visibility = ElementVisibility.Collapsed;

            splitPanel5.Controls.Clear();
            //splitPanel5.Controls.Add(null);

            activeGrid = gridViewLayouts;
            activeGrid.bLoadTreeMenu = false;
            splitPanel5.Controls.Add(gridViewLayouts);

            splitPanel7.Controls.Clear();

            splitPanel8.Controls.Clear();

            treeViewLabels.Nodes.Clear();
            treeViewLabels.Controls.Clear();
            treeViewLabels.Refresh();
            treeViewFilters.Nodes.Clear();
            treeViewFilters.Controls.Clear();
            treeViewFilters.Refresh();

            //frmBookmarks frmBookmarks = new frmBookmarks();
            //frmBookmarks.ShowDialog();
            selectStandardFilterIfExist();
            gridCustomFilter = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\layouts");
        }
        public void OnOpenNewYearClick()
        {
            if (Login._user.isAccountManager == true)
            {
                using (frmBeginYear fby = new frmBeginYear())
                {
                    fby.ShowDialog();

                }
            }
            else
            {
                translateRadMessageBox msg1 = new translateRadMessageBox();
                msg1.translateAllMessageBox("Sorry, You don't have permision for this operation!");
            }
        }
        public void OnCreditpayClick()
        {
            using (frmCreditPay fcp = new frmCreditPay())
            {
                fcp.ShowDialog();

            }
                     
        }
        public void OnFolderSettingsClick()
        {
            using (frmPaths paths = new frmPaths())
            {
                paths.ShowDialog();
            }
        }
        public void OnOpenLinesClick()
        {
            
            using (frmOpenLines openLlines = new frmOpenLines())
            {
                openLlines.ShowDialog();                                
            }
        }
        public void OnImportME940Click()
        {
            using (ImportGmu gmu = new ImportGmu())
            {
                gmu.ShowDialog();                
            }
        }
        public void OnInvoiceSelectionClick()
        {
            using (frmInvoiceSelection fis = new frmInvoiceSelection())
            {
                fis.ShowDialog();                
            }
        }
        public void OnBankCreditPayClick()
        {
            using (frmBankCreditPay fbp = new frmBankCreditPay())
            {
                fbp.ShowDialog();                
            }
        }
        public void OnClosingLinesClick()
        {
            using (frmLineClose frmC = new frmLineClose())
            {
                frmC.ShowDialog();                
            }
        }

        void rl_NodeDataBound(object sender, Telerik.WinControls.UI.RadTreeViewEventArgs e)
        {
            if (e.Node != null)
            {
                e.Node.Expanded = true;
            }
        }

        void rl_CreateNodeElement(object sender, Telerik.WinControls.UI.CreateTreeNodeElementEventArgs e)
        {
            //if (e.Node != null)
            //{
            //    e.Node.Expanded = true;
            //}
        }
        private void rl_NodeFormatting(object sender, TreeNodeFormattingEventArgs e)
        {

            if (e.Node.Level == 0)
            {
                Font f = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
                e.Node.ImageIndex = 0;
                e.NodeElement.ContentElement.Font = f;
            }

            if (e.Node.Level == 1)
            {
                e.Node.ImageIndex = 1;
            }
        }

        void rl_SelectedNodeChanged(object sender, Telerik.WinControls.UI.RadTreeViewEventArgs e)
        {
            if (rl.SelectedNode != null)
            {
                splitPanelFavorites.Controls.Clear();
                MenuModel m = e.Node.DataBoundItem as MenuModel;
                if (m != null)
                {
                    ImageDB im = new ImageDB();
                    if (m.imageNew != null)
                        btnNew.Image = im.setImage(m.imageNew);
                    if (m.imageDelete != null)
                        btnDelete.Image = im.setImage(m.imageDelete);

                    string methodName = m.onClickMenu;
                    if (methodName != null && methodName != "")
                    {
                        if(methodName == "EmptyAccount")
                        {
                            radRibbonBarGroupReports.Visibility = ElementVisibility.Collapsed;
                            splitPanelLabels.Collapsed = true;
                        }

                        Type type = typeof(MainForm);
                        MethodInfo method = type.GetMethod("On" + methodName + "Click");
                        if (method != null)
                           method.Invoke(this, null);
                    }                    
                }
            }
            //posle svakog clicka ispitaj da li u treeViewLabels ima nodova. Ako nema sakrij panel
            if (treeViewLabels.TreeViewElement.GetNodeCount(false) == 0)
            {
                splitPanelLabels.Collapsed = true;
            }
            else
            {
                splitPanelLabels.Collapsed = false;
            }
        }

        // TREE MENU FOR PORTAL - EVENTS
        void portal_SelectedNodeChanged(object sender, Telerik.WinControls.UI.RadTreeViewEventArgs e)
        {
            if (portalTreeMenu.SelectedNode != null)
            {
                splitPanelFavorites.Controls.Clear();
                MenuModel m = e.Node.DataBoundItem as MenuModel;
                
                if (m != null && m.isGrid == true)
                {
                    ImageDB im = new ImageDB();
                    if (m.imageNew != null)
                        btnNew.Image = im.setImage(m.imageNew);
                    if (m.imageDelete != null)
                        btnDelete.Image = im.setImage(m.imageDelete);

                    string methodName = m.onClickMenu;
                    if (methodName != null && methodName != "")
                    {
                        if (methodName == "EmptyAccount")
                        {
                            radRibbonBarGroupReports.Visibility = ElementVisibility.Collapsed;
                        }

                        Type type = typeof(MainForm);
                        MethodInfo method = type.GetMethod("On" + methodName + "Click");
                        method.Invoke(this, null);
                    }
                }
            }


            //posle svakog clicka ispitaj da li u treeViewLabels ima nodova. Ako nema sakrij panel
            if (treeViewLabels.TreeViewElement.GetNodeCount(false) == 0)
            {
                splitPanelLabels.Collapsed = true;
            }
            else
            {
                splitPanelLabels.Collapsed = false;
            }
        }

        void portal_SelectedNodeClick(object sender, Telerik.WinControls.UI.RadTreeViewEventArgs e)
        {
            if (portalTreeMenu.SelectedNode != null)
            {
                splitPanelFavorites.Controls.Clear();
                MenuModel m = e.Node.DataBoundItem as MenuModel;

                if (m != null && m.isGrid == false)
                {
                    ImageDB im = new ImageDB();
                    if (m.imageNew != null)
                        btnNew.Image = im.setImage(m.imageNew);
                    if (m.imageDelete != null)
                        btnDelete.Image = im.setImage(m.imageDelete);

                    string methodName = m.onClickMenu;
                    if (methodName != null && methodName != "")
                    {
                        if (methodName == "EmptyAccount")
                        {
                            radRibbonBarGroupReports.Visibility = ElementVisibility.Collapsed;
                            splitPanelLabels.Collapsed = true;
                        }

                        Type type = typeof(MainForm);
                        MethodInfo method = type.GetMethod("On" + methodName + "Click");
                        method.Invoke(this, null);
                    }
                }
            }
        }

        // TREE MENU FOR ACCOUNTING - EVENTS

        void accountingTreeMenu_SelectedNodeChanged(object sender, Telerik.WinControls.UI.RadTreeViewEventArgs e)
        {
            if (accountingTreeMenu.SelectedNode != null)
            {
                //splitPanelFavorites.Controls.Clear();
                MenuModel m = e.Node.DataBoundItem as MenuModel;
                                
                if (m != null && m.isGrid == true)               
                {
                    ImageDB im = new ImageDB();
                    if (m.imageNew != null)
                        btnNew.Image = im.setImage(m.imageNew);
                    if (m.imageDelete != null)
                        btnDelete.Image = im.setImage(m.imageDelete);

                    string methodName = m.onClickMenu;
                    if (methodName != null && methodName != "")
                    {
                        if (methodName == "EmptyAccount")
                        {
                            radRibbonBarGroupReports.Visibility = ElementVisibility.Collapsed;
                            splitPanelLabels.Collapsed = true;
                        }

                        Type type = typeof(MainForm);
                        MethodInfo method = type.GetMethod("On" + methodName + "Click");
                        method.Invoke(this, null);
                    }
                }
            }


            //posle svakog clicka ispitaj da li u treeViewLabels ima nodova. Ako nema sakrij panel
         
            if (treeViewLabels.TreeViewElement.GetNodeCount(false) == 0)
            {
                splitPanelLabels.Collapsed = true;
            }
            else
            {
            //     splitPanelLabels.Collapsed = false;
            }
        }

        void accountingTreeMenu_SelectedNodeClick(object sender, Telerik.WinControls.UI.RadTreeViewEventArgs e)
        {
            if (accountingTreeMenu.SelectedNode != null)
            {
                //splitPanelFavorites.Controls.Clear();
                MenuModel m = e.Node.DataBoundItem as MenuModel;

                if (m != null && m.isGrid == false)
                {
                    ImageDB im = new ImageDB();
                    if (m.imageNew != null)
                        btnNew.Image = im.setImage(m.imageNew);
                    if (m.imageDelete != null)
                        btnDelete.Image = im.setImage(m.imageDelete);

                    string methodName = m.onClickMenu;
                    if (methodName != null && methodName != "")
                    {
                        if (methodName == "EmptyAccount")
                        {
                            radRibbonBarGroupReports.Visibility = ElementVisibility.Collapsed;
                            splitPanelLabels.Collapsed = true;
                        }

                        Type type = typeof(MainForm);
                        MethodInfo method = type.GetMethod("On" + methodName + "Click");
                        method.Invoke(this, null);
                    }
                }
            }
        }

        protected void RadMenuButtonClick(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            //deselect last menu item selected
            RadMenuButtonItem buttonSelected = radMenu1.Items[selectedMenuItem] as RadMenuButtonItem;
            buttonSelected.ButtonElement.ButtonFillElement.BackColor = backgroundMenuItemColor;

            RadMenuButtonItem button = sender as RadMenuButtonItem;
            //select clicked menu item
            button.ButtonElement.ButtonFillElement.BackColor = selectedMenuItemColor;
            selectedMenuItem = Login._menuModelList.FindIndex(item => item.idMenu.ToString() == button.Name);

            string methodName = Login._menuModelList.Find(item => item.idMenu.ToString() == button.Name).onClickMenu;
            if (methodName != null)
            {
                Type type = typeof(MainForm);
                MethodInfo method = type.GetMethod("On" + methodName + "Click");                
                method.Invoke(this, null);
            }

            //posle svakog clicka ispitaj da li u treeViewLabels ima nodova. Ako nema sakrij panel
            if (treeViewLabels.TreeViewElement.GetNodeCount(false) == 0)
            {
              
                   splitPanelLabels.Collapsed = true;
            }
            else
            {
                if (methodName != "Account")  //ubaceno 21.8 saki
                splitPanelLabels.Collapsed = false;
            }

            ImageDB im = new ImageDB();
            btnNew.Image = im.setImage(Login._menuModelList.Find(item => item.idMenu.ToString() == button.Name).imageNew);
            btnDelete.Image = im.setImage(Login._menuModelList.Find(item => item.idMenu.ToString() == button.Name).imageDelete);

            // ako je onclick polje prazno izbrisi sve iz panela
            string strmethod = (Login._menuModelList.Find(item => item.idMenu.ToString() == button.Name).onClickMenu);
            if (strmethod == null)
            {
                splitPanel5.Controls.Clear();
                splitPanel7.Controls.Clear();
                treeViewFilters.Nodes.Clear();
                treeViewLabels.Nodes.Clear();

            }
            Cursor.Current = Cursors.Default;
        }

        private void splitPanel5_ControlAdded(object sender, System.Windows.Forms.ControlEventArgs e)
        {            
            bool t = typeof(IBISGrid).IsAssignableFrom(e.Control.GetType());            
            
            if(t == true)
            {          

                radRibbonCustomFilters.Visibility = ElementVisibility.Visible;
               // radRibbonBarGroup1.Visibility = ElementVisibility.Visible;
                treeViewFilters.Visible = true;
                treeViewLabels.Visible = true;
            }
            else
            {                
                radRibbonCustomFilters.Visibility = ElementVisibility.Collapsed;
               // radRibbonBarGroup1.Visibility = ElementVisibility.Collapsed;
                treeViewFilters.Nodes.Clear();
                treeViewLabels.Nodes.Clear();
                treeViewFilters.Visible = false;
                treeViewLabels.Visible = false;
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            string formName = Login._menuModelList[selectedMenuItem].nameForm;

            if (rl != null && rl.SelectedNode != null &&
                Login._menuModelList[selectedMenuItem].onClickMenu != "Portal" && Login._menuModelList[selectedMenuItem].onClickMenu != "Account")
            {                
                    formName = rl.SelectedNode.Name;
                    TranslationBUS tb = new TranslationBUS();
                    List<TranslationModel> tm = new List<TranslationModel>();
                    tm = tb.CheckIfTranslationValueExists(Login._user.lngUser, rl.SelectedNode.Name);
                    if (tm != null)
                    {
                        if (tm.Count > 0)
                        {
                            formName = tm[0].stringKey;
                        }
                    }
                    MenuModel mm = new MenuModel();
                    MenuBUS mb = new MenuBUS();
                    mm = mb.GetMenuByName(formName, Login._user.idUser, Login._user.lngUser);
                    formName = mm.nameForm;                
            }
            else if (portalTreeMenu != null && portalTreeMenu.SelectedNode != null && Login._menuModelList[selectedMenuItem].onClickMenu == "Portal")
            {
                formName = portalTreeMenu.SelectedNode.Name;
                TranslationBUS tb = new TranslationBUS();
                List<TranslationModel> tm = new List<TranslationModel>();
                tm = tb.CheckIfTranslationValueExists(Login._user.lngUser, portalTreeMenu.SelectedNode.Name);
                if (tm != null)
                {
                    if (tm.Count > 0)
                    {
                        formName = tm[0].stringKey;
                    }
                }
                MenuModel mm = new MenuModel();
                MenuBUS mb = new MenuBUS();
                mm = mb.GetMenuByName(formName, Login._user.idUser, Login._user.lngUser);
                formName = mm.nameForm; 
            }
            else if (accountingTreeMenu != null && accountingTreeMenu.SelectedNode != null && Login._menuModelList[selectedMenuItem].onClickMenu == "Account")
            {
                formName = accountingTreeMenu.SelectedNode.Name;
                TranslationBUS tb = new TranslationBUS();
                List<TranslationModel> tm = new List<TranslationModel>();
                tm = tb.CheckIfTranslationValueExists(Login._user.lngUser, accountingTreeMenu.SelectedNode.Name);
                if (tm != null)
                {
                    if (tm.Count > 0)
                    {
                        formName = tm[0].stringKey;
                    }
                }
                MenuModel mm = new MenuModel();
                MenuBUS mb = new MenuBUS();
                mm = mb.GetMenuByName(formName, Login._user.idUser, Login._user.lngUser);
                formName = mm.nameForm; 
            }

            if (formName != null && formName != "")
            {
                if (formName == "frmAccSettings")  //dozvoljen samo jedan slog u tabeli
                {
                    AccSettingsBUS asb = new AccSettingsBUS();
                    List<AccSettingsModel> asm = new List<AccSettingsModel>();
                    asm = asb.GetAllAccSettings(Login._bookyear);
                    if (asm != null)
                    {
                        if (asm.Count >= 1)
                        {
                            translateRadMessageBox trs = new translateRadMessageBox();
                            trs.translateAllMessageBox("Only Edit is allowed!");
                            return;
                        }
                    }
                    else
                    {
                        var form = (Form)Activator.CreateInstance(Type.GetType("GUI." + formName));
                        form.ShowDialog();
                        if (activeGrid != null)
                        {
                            modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
                            activeGrid.SetDataPersonBinding(modelData);
                        }
                    }
                }
                else
                {
                    var form = (Form)Activator.CreateInstance(Type.GetType("GUI." + formName));
                    form.ShowDialog();
                    if (activeGrid != null)
                    {
                            modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
                            activeGrid.SetDataPersonBinding(modelData);
          
                    }
                }
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string formName = Login._menuModelList[selectedMenuItem].nameForm;

            if (rl != null && rl.SelectedNode != null &&
               Login._menuModelList[selectedMenuItem].onClickMenu != "Portal" && Login._menuModelList[selectedMenuItem].onClickMenu != "Account")
            {
                formName = rl.SelectedNode.Name;
                TranslationBUS tb = new TranslationBUS();
                List<TranslationModel> tm = new List<TranslationModel>();
                tm = tb.CheckIfTranslationValueExists(Login._user.lngUser, rl.SelectedNode.Name);
                if (tm != null)
                {
                    if (tm.Count > 0)
                    {
                        formName = tm[0].stringKey;
                    }
                }
                MenuModel mm = new MenuModel();
                MenuBUS mb = new MenuBUS();
                mm = mb.GetMenuByName(formName, Login._user.idUser, Login._user.lngUser);
                formName = mm.nameForm;
            }
            else if (portalTreeMenu != null && portalTreeMenu.SelectedNode != null && Login._menuModelList[selectedMenuItem].onClickMenu == "Portal")
            {
                formName = portalTreeMenu.SelectedNode.Name;
                TranslationBUS tb = new TranslationBUS();
                List<TranslationModel> tm = new List<TranslationModel>();
                tm = tb.CheckIfTranslationValueExists(Login._user.lngUser, portalTreeMenu.SelectedNode.Name);
                if (tm != null)
                {
                    if (tm.Count > 0)
                    {
                        formName = tm[0].stringKey;
                    }
                }
                MenuModel mm = new MenuModel();
                MenuBUS mb = new MenuBUS();
                mm = mb.GetMenuByName(formName, Login._user.idUser, Login._user.lngUser);
                formName = mm.nameForm;
            }
            else if (accountingTreeMenu != null && accountingTreeMenu.SelectedNode != null && Login._menuModelList[selectedMenuItem].onClickMenu == "Account")
            {
                formName = accountingTreeMenu.SelectedNode.Name;
                TranslationBUS tb = new TranslationBUS();
                List<TranslationModel> tm = new List<TranslationModel>();
                tm = tb.CheckIfTranslationValueExists(Login._user.lngUser, accountingTreeMenu.SelectedNode.Name);
                if (tm != null)
                {
                    if (tm.Count > 0)
                    {
                        formName = tm[0].stringKey;
                    }
                }
                MenuModel mm = new MenuModel();
                MenuBUS mb = new MenuBUS();
                mm = mb.GetMenuByName(formName, Login._user.idUser, Login._user.lngUser);
                formName = mm.nameForm;
            }

            if (formName != null && formName != "")
            {
                if (formName == "frmBTW")
                {
                    deleteNodeBTW();

                }
                if (formName == "Kostenplaats" || formName == "frmCost")
                {
                    deleteNodeCost();
                }
                if (formName == "frmAccPayment")
                {
                    deletePayment();
                }
                if (formName == "Daily" || formName == "frmDaily")
                {
                    deleteNodeDaily();
                }
                if (formName == "Reis" || formName == "frmArrangement")
                {
                    deleteArrangement();
                }
                
                if (formName == "frmCountry")
                {
                    deleteCountry();

                }
                if (formName == "frmPeriod")
                {
                    deletePeriod();      
                }
                if (formName == "frmArticleGroups")
                {
                    deleteArticleGroup();
                }
                   
                if (formName == "frmAgeCategory")
                {
                    deleteAgeCategory();
                }

                if (formName == "frmEmployee")
                {
                    deleteEmployee();
                }                   
                if (formName == "frmFiltersLabels")
                {
                    deleteFilterLabel();                      
                }
                if (formName == "frmHotelServices")
                {
                    deleteHotelservice();
                }
                if (formName == "frmType")
                {
                    deleteType();
                }                   
                if (formName == "frmArticle")
                {
                    deleteArticle();                    
                }                
                if (formName == "frmMultimediaServer")
                {
                    deleteMultimediaServer();
                }
                if (formName == "frmUsers")
                {
                    deleteUser();
                }
                if (formName == "frmMedical")
                {
                    deleteMedical();
                } 

                if (formName == "frmTravelData")
                {
                    deleteTravelData();
                }         
                if (formName == "frmArrangementInsurance")
                {
                    deletefrmArrangementInsurance();
                }

                if (formName == "frmArrangementInsurancePremie")
                {
                    deletefrmArrangementInsurancePremie();  
                }

                if (formName == "frmClient")
                {
                    deleteClient();
                }
                if (formName == "frmReason")
                {
                    deletefrmReason();
                }
                if (formName == "frmRoles")
                {
                    deleteRole();
                }
                if (formName == "frmVoluntary")
                {
                    deleteVoluntary();
                }
                if (formName == "frmVoluntaryFunc")
                {
                    deleteVoluntaryFunc();
                }
                if (formName == "frmVoluntaryTrip")
                {
                    deleteVoluntaryTrip();
                }
                if (formName == "frmBookmark2")
                {
                    deleteBookmark();
                } 
                
                if (formName == "frmPerson")
                {
                    deletePerson();
                }
                
            }
        }

        public static string GetTemporaryFolder()
        {
            var tempDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempDirectory);
            return tempDirectory;
        }

        # region dlelete


        private void deleteRole()
        {
            RoleBUS pb = new RoleBUS();
            if (gridViewRoles != null)
            {
                if (gridViewRoles.SelectedRowRoles != null)
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    if (tr.translateAllMessageBoxDialog("Are you sure that you want to delete this role?", "Delete") == System.Windows.Forms.DialogResult.Yes)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        if (pb.checkIsInUsers(gridViewRoles.SelectedRowRoles.idRole) <= 0)
                        {
                            if (pb.checkIsInMenuRoleSecurity(gridViewRoles.SelectedRowRoles.idRole) <= 0)
                            {
                                if (pb.DeleteRoleSript(gridViewRoles.SelectedRowRoles.idRole, this.Name, Login._user.idUser) == true)
                                {
                                    gridViewRoles.removeRow(gridViewRoles.SelectedRowRoles);

                                    translateRadMessageBox trs = new translateRadMessageBox();
                                    trs.translateAllMessageBox("You have delete successfully!");
                                }

                                else
                                {
                                    translateRadMessageBox trs = new translateRadMessageBox();
                                    trs.translateAllMessageBox("Something went wrong. Please contact your administrator!");
                                }
                            }
                            else
                            {
                                translateRadMessageBox trs = new translateRadMessageBox();
                                trs.translateAllMessageBox("You can't delete this role, because some of menu have this role!");
                            }
                        }
                        else
                        {
                            translateRadMessageBox trs = new translateRadMessageBox();
                            trs.translateAllMessageBox("You can't delete this role, because some of user have this role!");
                        }
                    }

                }
                else
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("You have to have selected at least one role!");
                }
            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("You have to have at least one role!");
            }
            Cursor.Current = Cursors.Default;
        }

        private void deleteNodeBTW()
        {
            int idKey = Convert.ToInt32(gridViewTax.SelectedRowTax.idTax);
            string code = gridViewTax.SelectedRowTax.codeTax;
            AccLineBUS aclb = new AccLineBUS(Login._bookyear);
            List<AccLineModel> aclm = new List<AccLineModel>();
            aclm = aclb.GetLinesByBTW(idKey);
            if (aclm != null && aclm.Count > 0)
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                //RadMessageBox.Show("This BTW cannot be deleted!");
                trs.translateAllMessageBox("This BTW has booking lines!");
                return;
            }
            else
            {
                AccTaxBUS nbus1 = new AccTaxBUS();
                translateRadMessageBox tr = new translateRadMessageBox();
                string frmName = "frmBTW";
                if (tr.translateAllMessageBoxDialog("Do you want to delete this BTW?", "BTW") == System.Windows.Forms.DialogResult.Yes)
                {
                    if (nbus1.Delete(idKey, frmName, Login._user.idUser) == true)  //this.Name
                    {
                        translateRadMessageBox trs = new translateRadMessageBox();
                        trs.translateAllMessageBox("You have delete successfully!");
                    }
                }

                else
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("Something went wrong!Please try again or contact your administrator!");
                    return;
                }
            }

            //AccTaxValidityBUS nbus = new AccTaxValidityBUS();
            //List<AccTaxValidityModel> nmod = new List<AccTaxValidityModel>();
            //nmod = nbus.GetTaxValidity(code);
            //if (nmod.Count > 0)
            //{
            //    translateRadMessageBox trs = new translateRadMessageBox();
            //    //RadMessageBox.Show("This BTW cannot be deleted!");
            //    trs.translateAllMessageBox("This BTW cannot be deleted!");

            //}
            //else
            //{
            //    AccTaxBUS nbus1 = new AccTaxBUS();

            //    translateRadMessageBox tr = new translateRadMessageBox();

            //    if (tr.translateAllMessageBoxDialog("Do you want to delete this BTW?", "BTW") == System.Windows.Forms.DialogResult.Yes)
            //    {
            //        if (nbus1.Delete(idKey) == true)
            //        {
            //            translateRadMessageBox trs = new translateRadMessageBox();
            //            trs.translateAllMessageBox("You have delete successfully!");
            //        }
            //    }

            //    else
            //    {
            //        translateRadMessageBox trs = new translateRadMessageBox();
            //        trs.translateAllMessageBox("Something went wrong!Please try again or contact your administrator!");
            //        return;
            //    }
                modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
                activeGrid.SetDataPersonBinding(modelData);

           // }
        }
        private void deletefrmReason()
        {


            string tip = gridViewReason.SelectedRowReason.type;
            List<TranslateUstrModel> translate;
            UsersBUS ub = new UsersBUS();
            ReasonBUS rb = new ReasonBUS();
            string prevod = "";

            translate = ub.Translate("Contact person reason out", Login._user.lngUser);
            if (translate != null)
            {
                if (translate.Count > 0)
                {
                    prevod = translate[0].stringValue;
                    if (tip == prevod)
                    {
                        DeleteCPReasonOut();
                    }
                }
            }

            translate = ub.Translate("Contact person reason in", Login._user.lngUser);
            if (translate != null)
            {
                if (translate.Count > 0)
                {
                    prevod = translate[0].stringValue;
                    if (tip == prevod)
                    {
                        DeleteCPReasonIn();
                    }
                }
            }
            translate = ub.Translate("Voluntary reason out", Login._user.lngUser);
            if (translate != null)
            {
                if (translate.Count > 0)
                {
                    prevod = translate[0].stringValue;
                    if (tip == prevod)
                    {
                        DeleteVHReasonOut();
                    }
                }
            }
            translate = ub.Translate("Voluntary reason in", Login._user.lngUser);
            if (translate != null)
            {
                if (translate.Count > 0)
                {
                    prevod = translate[0].stringValue;
                    if (tip == prevod)
                    {
                        DeleteVHReasonIn();
                    }
                }
            }

        }

        private void DeleteCPReasonOut()
        {
            int id = Convert.ToInt32(gridViewReason.SelectedRowReason.ID);
            ReasonBUS vBUS = new ReasonBUS();
            List<ReasonModel> list = new List<ReasonModel>();
            list = vBUS.AllCPReasonOut();
            bool isContains = false;


            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].ID.ToString() == id.ToString())
                {
                    isContains = true;
                    break;
                }

            }
            if (isContains == false)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                if (tr.translateAllMessageBoxDialog("Are you sure that you want to delete this Reason?", "Rason") == System.Windows.Forms.DialogResult.Yes)
                {

                    if (vBUS.DeleteContactPersonReasonOut(id, this.Name, Login._user.idUser) == true)
                    {
                        translateRadMessageBox trs = new translateRadMessageBox();
                        trs.translateAllMessageBox("You have delete successfully!");
                    }
                    else
                    {
                        translateRadMessageBox trs = new translateRadMessageBox();
                        trs.translateAllMessageBox("Something went wrong!Please try again or contact your administrator!");
                    }

                    modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
                    activeGrid.SetDataPersonBinding(modelData);
                }
            }

            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("This record cannot be deleted!");

            }

        }

        private void DeleteCPReasonIn()
        {
            int id = Convert.ToInt32(gridViewReason.SelectedRowReason.ID);
            ReasonBUS vBUS = new ReasonBUS();
            List<ReasonModel> list = new List<ReasonModel>();
            list = vBUS.AllCPReasonIn();
            bool isContains = false;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].ID.ToString() == id.ToString())
                {
                    isContains = true;
                    break;
                }
            }
            if (isContains == false)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                if (tr.translateAllMessageBoxDialog("Are you sure that you want to delete this Reason?", "Rason") == System.Windows.Forms.DialogResult.Yes)
                {
                    if (vBUS.DeleteContactPersonReasonIn(id, this.Name, Login._user.idUser) == true)
                    {
                        translateRadMessageBox trs = new translateRadMessageBox();
                        trs.translateAllMessageBox("You have delete successfully!");
                    }
                    else
                    {
                        translateRadMessageBox trs = new translateRadMessageBox();
                        trs.translateAllMessageBox("Something went wrong!Please try again or contact your administrator!");
                    }
                    modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
                    activeGrid.SetDataPersonBinding(modelData);
                }
            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("This record cannot be deleted!");

            }

        }

        private void DeleteVHReasonOut()
        {
            int id = Convert.ToInt32(gridViewReason.SelectedRowReason.ID);
            ReasonBUS vBUS = new ReasonBUS();
            List<ReasonModel> list = new List<ReasonModel>();
            list = vBUS.AllVHReasonOut();
            bool isContains = false;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].ID.ToString() == id.ToString())
                {
                    isContains = true;
                    break;
                }
            }
            if (isContains == false)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                if (tr.translateAllMessageBoxDialog("Are you sure that you want to delete this Reason?", "Rason") == System.Windows.Forms.DialogResult.Yes)
                {
                    if (vBUS.DeleteVoluntaryReasonOut(id, this.Name, Login._user.idUser) == true)
                    {
                        translateRadMessageBox trs = new translateRadMessageBox();
                        trs.translateAllMessageBox("You have delete successfully!");
                    }
                    else
                    {
                        translateRadMessageBox trs = new translateRadMessageBox();
                        trs.translateAllMessageBox("Something went wrong!Please try again or contact your administrator!");
                    }
                    modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
                    activeGrid.SetDataPersonBinding(modelData);
                }
            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("This record cannot be deleted!");

            }

        }

        private void DeleteVHReasonIn()
        {
            int id = Convert.ToInt32(gridViewReason.SelectedRowReason.ID);
            ReasonBUS vBUS = new ReasonBUS();
            List<ReasonModel> list = new List<ReasonModel>();
            list = vBUS.AllVHReasonIn();
            bool isContains = false;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].ID.ToString() == id.ToString())
                {
                    isContains = true;
                    break;
                }
            }
            if (isContains == false)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                if (tr.translateAllMessageBoxDialog("Are you sure that you want to delete this Reason?", "Rason") == System.Windows.Forms.DialogResult.Yes)
                {
                    if (vBUS.DeleteVoluntaryReasonIn(id, this.Name, Login._user.idUser) == true)
                    {
                        translateRadMessageBox trs = new translateRadMessageBox();
                        trs.translateAllMessageBox("You have delete successfully!");
                    }
                    else
                    {
                        translateRadMessageBox trs = new translateRadMessageBox();
                        trs.translateAllMessageBox("Something went wrong!Please try again or contact your administrator!");
                    }
                    modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
                    activeGrid.SetDataPersonBinding(modelData);
                }
            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("This record cannot be deleted!");

            }

        }

        private void deleteNodeCost()
        {
            bool isContains = false;
            bool isContains1 = false;

            string id = gridViewCost.SelectedRowCost.codeCost;
            AccCostBUS acBUS = new AccCostBUS();

            LedgerAccountBUS laBUS = new LedgerAccountBUS(Login._bookyear);
            AccLineBUS alBUS = new AccLineBUS(Login._bookyear);

            List<LedgerAccountModel> lmList = new List<LedgerAccountModel>();
            List<AccLineModel> amList = new List<AccLineModel>();

            lmList = laBUS.GetCostCenterFromAccLedgerAccount(id);

            if (lmList != null)
            {
                if (lmList.Count > 0)
                {
                    isContains = true;
                }
                else
                {
                    isContains = false;
                }
            }

            amList = alBUS.GetCostLineFromAccLine(id);
            if (amList != null)
            {
                if (amList.Count > 0)
                {
                    isContains1 = true;
                }
            }
            else
            {
                isContains1 = false;
            }

            if (isContains == false && isContains1 == false)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                if (tr.translateAllMessageBoxDialog("Do you want to delete this Acc Cost?", "Cost") == System.Windows.Forms.DialogResult.Yes)
                {

                    if (acBUS.Delete(id, this.Name, Login._user.idUser) == true)
                    {
                        translateRadMessageBox trs = new translateRadMessageBox();
                        trs.translateAllMessageBox("You have delete successfully!");
                    }
                }
                else
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("Something went wrong!Please try again or contact your administrator!");
                    return;
                }
                modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
                activeGrid.SetDataPersonBinding(modelData);
            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("This record cannot be deleted!");

            }
        }

        private void deleteNodeDaily()
        {
            if (DialogResult.Yes == RadMessageBox.Show("Do you want to delete this Daily?", "Delete", MessageBoxButtons.YesNo, RadMessageIcon.Question))
            {
                try
                {
                    int idKey = Convert.ToInt32(gridViewDaily.SelectedRowDaily.idDaily);
                    if (gridViewDaily.SelectedRowDaily.unBooked != 0)
                    {
                        RadMessageBox.Show("Can't delete, there is child record");
                        return;
                    }
                    //}
                    //else
                    //{
                    AccDailyBUS nbus6 = new AccDailyBUS(Login._bookyear);
                    string frmName = "frmDaily";
                    if (nbus6.Delete(idKey, frmName, Login._user.idUser) == false)  //this.Name
                    {
                        RadMessageBox.Show("Error deleting ");
                    }
                    else
                    {
                        modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
                        activeGrid.SetDataPersonBinding(modelData);
                    }
                    // }
                }
                catch (Exception ex)
                {
                    RadMessageBox.Show("Error deleting Daily. \nMessage: " + ex.Message, "Error delete", MessageBoxButtons.OK, RadMessageIcon.Error);
                }
            }
        }
        private void deletePayment()
        {
            if (DialogResult.Yes == RadMessageBox.Show("Do you want to delete this Payment?", "Delete", MessageBoxButtons.YesNo, RadMessageIcon.Question))
            {
                try
                {
                    int idKey = Convert.ToInt32(gridViewPayments.SelectedRowPayment.idPayment);
                    List<AccDebCreModel> apl = new List<AccDebCreModel>();
                    AccPaymentBUS apbbb = new AccPaymentBUS();
                    apl = apbbb.GetAccPaymentForDelete(idKey);
                    if (apl != null && apl.Count > 0)
                    {
                        RadMessageBox.Show("Can't delete, there is child record");
                        return;
                    }
                    //}
                    //else
                    //{
                   // AccPaymentBUS nbus6 = new AccPaymentBUS();
                    if (apbbb.Delete(idKey,this.Name, Login._user.idUser) == false)
                    {
                        RadMessageBox.Show("Error deleting ");
                    }
                    else
                    {
                        modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
                        activeGrid.SetDataPersonBinding(modelData);
                    }
                    // }
                }
                catch (Exception ex)
                {
                    RadMessageBox.Show("Error deleting Payment. \nMessage: " + ex.Message, "Error delete", MessageBoxButtons.OK, RadMessageIcon.Error);
                }
            }
        }

        private void deleteNodeArrangement()
        {
            bool isContains = false;


            int idArr = gridViewArrangement.SelectedRowArrangement.idArrangement;
            ArrangementBUS arr = new ArrangementBUS();

            List<ArrangementModel> list = new List<ArrangementModel>();
            list = arr.IsIn(idArr);
            if (list != null)
            {
                if (list.Count > 0)
                    isContains = true;
            }
            else
            {
                isContains = false;
            }


            if (isContains == false)
            {

                translateRadMessageBox tr = new translateRadMessageBox();
                if (tr.translateAllMessageBoxDialog("Are you sure that you want to delete Arrangement?", "Arrangement") == System.Windows.Forms.DialogResult.Yes)
                {
                    if (arr.Delete(idArr, this.Name, Login._user.idUser) == true)
                    {
                        translateRadMessageBox trs = new translateRadMessageBox();
                        trs.translateAllMessageBox("You have delete successfully!");
                        modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
                        activeGrid.SetDataPersonBinding(modelData);
                    }
                }

                else
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("Something went wrong with deleting!");
                    return;
                }
                modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
                activeGrid.SetDataPersonBinding(modelData);
            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("This arrangement cannot be deleted!");
            }
        }

        private void deleteArrangement()
        {
            CanDeleteBUS cd = new CanDeleteBUS();
            if (gridViewArrangement != null)
            {
                if (gridViewArrangement.SelectedRowArrangement != null)
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    if (tr.translateAllMessageBoxDialog("Are you sure that you want to delete this arrangement?", "Delete") == System.Windows.Forms.DialogResult.Yes)
                    {
                         Cursor.Current = Cursors.WaitCursor;

                         if (cd.canDelete("ArrangementInvoicePrice","idArrangement",gridViewArrangement.SelectedRowArrangement.idArrangement.ToString()) == false)
                         {
                             if (cd.canDelete("ArrangementBook", "idArrangement", gridViewArrangement.SelectedRowArrangement.idArrangement.ToString()) == false)
                             {
                                 if (cd.canDelete("PriceList", "idArrangement", gridViewArrangement.SelectedRowArrangement.idArrangement.ToString()) == false)
                                 {
                                     if (cd.canDelete("ArrangementPrice", "idArrangement", gridViewArrangement.SelectedRowArrangement.idArrangement.ToString()) == false)
                                     {
                                         if (cd.canDelete("ArrangementTraveler", "idArrangement", gridViewArrangement.SelectedRowArrangement.idArrangement.ToString()) == false)
                                         {
                                             if (cd.canDelete("ArrangementRooms", "idArrangement", gridViewArrangement.SelectedRowArrangement.idArrangement.ToString()) == false)
                                             {
                                                 if (cd.canDelete("VolLookup", "idArrangement", gridViewArrangement.SelectedRowArrangement.idArrangement.ToString()) == false)
                                                 {
                                                     if (cd.canDelete("BISAppointment", "idArrangement", gridViewArrangement.SelectedRowArrangement.idArrangement.ToString()) == false)
                                                     {
                                                         ArrangementBUS ab = new ArrangementBUS();
                                                         if (ab.Delete(gridViewArrangement.SelectedRowArrangement.idArrangement, this.Name, Login._user.idUser) == true)
                                                         {
                                                             modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
                                                             activeGrid.SetDataPersonBinding(modelData);
                                                             translateRadMessageBox trs = new translateRadMessageBox();
                                                             trs.translateAllMessageBox("You have delete successfully!");
                                                         }
                                                         else
                                                         {
                                                             translateRadMessageBox trs = new translateRadMessageBox();
                                                             trs.translateAllMessageBox("Something went wrong with deleting please contact your administrator!");
                                                         }
                                                     }
                                                     else
                                                     {
                                                         translateRadMessageBox trs = new translateRadMessageBox();
                                                         trs.translateAllMessageBox("You can't delete arrangement because you have/had appointment for this arrangement!");
                                                     }
                                                 }
                                                 else
                                                 {
                                                     translateRadMessageBox trs = new translateRadMessageBox();
                                                     trs.translateAllMessageBox("You can't delete arrangement because you have some voluntary function or skills booked on this arrangement!");
                                                 }
                                             }
                                             else
                                             {
                                                 translateRadMessageBox trs = new translateRadMessageBox();
                                                 trs.translateAllMessageBox("You can't delete arrangement because you have rooms on this arrangement!");
                                             }
                                         }
                                         else
                                         {
                                             translateRadMessageBox trs = new translateRadMessageBox();
                                             trs.translateAllMessageBox("You can't delete arrangement because you have some travelers on this arrangement!");
                                         }
                                     }
                                     else
                                     {
                                         translateRadMessageBox trs = new translateRadMessageBox();
                                         trs.translateAllMessageBox("You can't delete arrangement because you have some articles without contracts on this arrangement!");
                                     }
                                 }
                                 else
                                 {
                                     translateRadMessageBox trs = new translateRadMessageBox();
                                     trs.translateAllMessageBox("You can't delete arrangement because you have some contracts on this arrangement!");
                                 }
                             }
                             else
                             {
                                 translateRadMessageBox trs = new translateRadMessageBox();
                                 trs.translateAllMessageBox("You can't delete arrangement because you have booking on it!");
                             }
                         }
                         else
                         {
                             translateRadMessageBox trs = new translateRadMessageBox();
                             trs.translateAllMessageBox("You can't delete arrangement because you have invoice created on it!");
                         }
                    }
                }
            }
        }

        private void deletePerson()
        {
            PersonBUS pb = new PersonBUS();
            CanDeleteBUS cd = new CanDeleteBUS();

            if (gridViewPerson != null)
            {
                if (gridViewPerson.SelectedRowPerson != null)
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    if (tr.translateAllMessageBoxDialog("Are you sure that you want to delete this person?", "Delete") == System.Windows.Forms.DialogResult.Yes)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        if (pb.checkIsInDebitor(gridViewPerson.SelectedRowPerson.idContPers) <= 0)
                        {
                            if (pb.checkIsInArrangementBook(gridViewPerson.SelectedRowPerson.idContPers) <= 0)
                            {
                                if (cd.canDelete("ArrangementBookPersons", "idContPers", gridViewPerson.SelectedRowPerson.idContPers.ToString()) == false)
                                {
                                    if (cd.canDelete("ArrangementTravelers", "idContPers", gridViewPerson.SelectedRowPerson.idContPers.ToString()) == false)
                                    {
                                        if (cd.canDelete("ArrangementTravelers", "idTravelWithPerson", gridViewPerson.SelectedRowPerson.idContPers.ToString()) == false)
                                        {
                                            if (pb.checkIsInBISAppointment(gridViewPerson.SelectedRowPerson.idContPers) <= 0)
                                            {
                                                if (pb.checkIsInClientPerson(gridViewPerson.SelectedRowPerson.idContPers) <= 0)
                                                {
                                                    if (pb.checkIsInDocuments(gridViewPerson.SelectedRowPerson.idContPers) <= 0)
                                                    {
                                                        if (pb.checkIsInInvoice(gridViewPerson.SelectedRowPerson.idContPers) <= 0)
                                                        {
                                                            if (cd.canDelete("Contacts", "idContPers", gridViewPerson.SelectedRowPerson.idContPers.ToString()) == false)
                                                            {
                                                                if (cd.canDelete("Meetings", "contactPersonId", gridViewPerson.SelectedRowPerson.idContPers.ToString()) == false)
                                                                {
                                                                    if (cd.canDelete("ToDo", "idContPers", gridViewPerson.SelectedRowPerson.idContPers.ToString()) == false)
                                                                    {
                                                                        if (pb.DeletePerson(gridViewPerson.SelectedRowPerson.idContPers, this.Name, Login._user.idUser) == true)
                                                                        {
                                                                            gridViewPerson.removeRow(gridViewPerson.SelectedRowPerson);
                                                                            //if (activeGrid != null)
                                                                            //{
                                                                            //    modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
                                                                            //    activeGrid.SetDataPersonBinding(modelData);
                                                                            //} 
                                                                            translateRadMessageBox trs = new translateRadMessageBox();
                                                                            trs.translateAllMessageBox("You have delete successfully!");
                                                                        }
                                                                        else
                                                                        {
                                                                            translateRadMessageBox trs = new translateRadMessageBox();
                                                                            trs.translateAllMessageBox("Something went wrong. Please contact your administrator!");
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        translateRadMessageBox trs = new translateRadMessageBox();
                                                                        trs.translateAllMessageBox("First you have to delete that person tasks!");
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    translateRadMessageBox trs = new translateRadMessageBox();
                                                                    trs.translateAllMessageBox("First you have to delete that person meetings!");
                                                                }
                                                            }
                                                            else
                                                            {
                                                                translateRadMessageBox trs = new translateRadMessageBox();
                                                                trs.translateAllMessageBox("First you have to delete that person's contacts!");
                                                            }
                                                        }
                                                        else
                                                        {
                                                            translateRadMessageBox trs = new translateRadMessageBox();
                                                            trs.translateAllMessageBox("First you have to delete that person's invoice if this is possible!");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        translateRadMessageBox trs = new translateRadMessageBox();
                                                        trs.translateAllMessageBox("First you have to delete that person's document!");
                                                    }
                                                }
                                                else
                                                {
                                                    translateRadMessageBox trs = new translateRadMessageBox();
                                                    trs.translateAllMessageBox("First you have to delete that person at client!");
                                                }
                                            }
                                            else
                                            {
                                                translateRadMessageBox trs = new translateRadMessageBox();
                                                trs.translateAllMessageBox("First you have to delete appointment for that person!");
                                            }
                                        }
                                        else
                                        {
                                            translateRadMessageBox trs = new translateRadMessageBox();
                                            trs.translateAllMessageBox("Delete failed. Person is travler with on arrangement.");
                                        }
                                    }
                                    else
                                    {
                                        translateRadMessageBox trs = new translateRadMessageBox();
                                        trs.translateAllMessageBox("Delete failed. Person has travelers on booking.");
                                    }
                                }
                                else
                                {
                                    translateRadMessageBox trs = new translateRadMessageBox();
                                    trs.translateAllMessageBox("First you have to delete booking for that person!");
                                }
                            }
                            else
                            {
                                translateRadMessageBox trs = new translateRadMessageBox();
                                trs.translateAllMessageBox("First you have to delete booking for that person!");
                            }

                        }
                        else
                        {
                            translateRadMessageBox trs = new translateRadMessageBox();
                            trs.translateAllMessageBox("First you have to delete that person as debitor!");
                        }
                    }
                }
                else
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("You have to have selected at least one person!");
                }
            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("You have to have at least one person!");
            }
            Cursor.Current = Cursors.Default;
          }

        private void deleteCountry()
          {
              CountryBUS pb = new CountryBUS();
              if (gridViewCountry != null)
              {
                  if (gridViewCountry.SelectedRowCountry != null)
                  {
                      translateRadMessageBox tr = new translateRadMessageBox();
                      if (tr.translateAllMessageBoxDialog("Are you sure that you want to delete this country?", "Delete") == System.Windows.Forms.DialogResult.Yes)
                      {
                          Cursor.Current = Cursors.WaitCursor;
                          if (pb.checkIsInArrrangement(gridViewCountry.SelectedRowCountry.idCountry) <= 0)
                          {
                              if (pb.checkIsInEmployees(gridViewCountry.SelectedRowCountry.idCountry) <= 0)
                              {
                                  if (pb.checkIsInCompany(gridViewCountry.SelectedRowCountry.idCountry) <= 0)
                                  {
                                      if (pb.checkIsInContactPersonPassport(gridViewCountry.SelectedRowCountry.idCountry) <= 0)
                                      {
                                          if (pb.checkIsInProvinces(gridViewCountry.SelectedRowCountry.idCountry) <= 0)
                                          {
                                              if (pb.checkIsInArrangementCalculationFirstNotArticles(gridViewCountry.SelectedRowCountry.idCountry) <= 0)
                                              {
                                                  if (pb.DeleteCountrySript(gridViewCountry.SelectedRowCountry.idCountry) == true)
                                                  {
                                                      gridViewCountry.removeRow(gridViewCountry.SelectedRowCountry);
                                                      //if (activeGrid != null)
                                                      //{
                                                      //    modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
                                                      //    activeGrid.SetDataPersonBinding(modelData);
                                                      //} 
                                                      translateRadMessageBox trs = new translateRadMessageBox();
                                                      trs.translateAllMessageBox("You have delete successfully!");
                                                  }
                                                  else
                                                  {
                                                      translateRadMessageBox trs = new translateRadMessageBox();
                                                      trs.translateAllMessageBox("Something went wrong. Please contact your administrator!");
                                                  }

                                              }
                                              else
                                              {
                                                  translateRadMessageBox trs = new translateRadMessageBox();
                                                  trs.translateAllMessageBox("You can't delete, because  this country is in arrangement calculation!");
                                              }

                                          }

                                          else
                                          {
                                              translateRadMessageBox trs = new translateRadMessageBox();
                                              trs.translateAllMessageBox("You can't delete, because some of province have this country!");
                                          }

                                      }
                                      else
                                      {
                                          translateRadMessageBox trs = new translateRadMessageBox();
                                          trs.translateAllMessageBox("First you have to delete person passport with this country!");
                                      }
                                  }
                                  else
                                  {
                                      translateRadMessageBox trs = new translateRadMessageBox();
                                      trs.translateAllMessageBox("First you have delete company with this country!");
                                  }
                              }
                              else
                              {
                                  translateRadMessageBox trs = new translateRadMessageBox();
                                  trs.translateAllMessageBox("You have to delete employee with this country!");
                              }
                          }
                          else
                          {
                              translateRadMessageBox trs = new translateRadMessageBox();
                              trs.translateAllMessageBox("First you have to delete arrangement with this country!");
                          }
                      }
                  }
                  else
                  {
                      translateRadMessageBox trs = new translateRadMessageBox();
                      trs.translateAllMessageBox("You have to have selected at least one person!");
                  }
              }
              else
              {
                  translateRadMessageBox trs = new translateRadMessageBox();
                  trs.translateAllMessageBox("You have to have at least one person!");
              }
              Cursor.Current = Cursors.Default;
          }

        private void deletePeriod()
          {
              CanDeleteBUS cd = new CanDeleteBUS();
              PeriodBUS pb = new PeriodBUS();
              if (gridViewPeriod != null)
              {
                  if (gridViewPeriod.SelectedRowPeriod != null)
                  {
                      translateRadMessageBox tr = new translateRadMessageBox();
                      if (tr.translateAllMessageBoxDialog("Are you sure that you want to delete this period?", "Delete") == System.Windows.Forms.DialogResult.Yes)
                      {
                          Cursor.Current = Cursors.WaitCursor;
                          if (cd.canDelete("Multimedia", "idPeriod", gridViewPeriod.SelectedRowPeriod.idPeriod.ToString()) == false)
                          {


                              if (pb.DeletePeriodSript(gridViewPeriod.SelectedRowPeriod.idPeriod, this.Name, Login._user.idUser) == true)
                              {
                                  gridViewPeriod.removeRow(gridViewPeriod.SelectedRowPeriod);
                                  //if (activeGrid != null)
                                  //{
                                  //    modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
                                  //    activeGrid.SetDataPersonBinding(modelData);
                                  //} 
                                  translateRadMessageBox trs = new translateRadMessageBox();
                                  trs.translateAllMessageBox("You have delete successfully!");
                              }
                              else
                              {
                                  translateRadMessageBox trs = new translateRadMessageBox();
                                  trs.translateAllMessageBox("Something went wrong. Please contact your administrator!");
                              }

                          }

                          else
                          {
                              translateRadMessageBox trs = new translateRadMessageBox();
                              trs.translateAllMessageBox("You haven't deleted because some of multimedia using this period!");
                          }
                      }

                  }
                  else
                  {
                      translateRadMessageBox trs = new translateRadMessageBox();
                      trs.translateAllMessageBox("You have to have selected at least one period!");
                  }
              }
              else
              {
                  translateRadMessageBox trs = new translateRadMessageBox();
                  trs.translateAllMessageBox("You have to have at least one period!");
              }
              Cursor.Current = Cursors.Default;
          }

        private void deleteClient()
        {
            ClientBUS pb = new ClientBUS();
            CanDeleteBUS cd = new CanDeleteBUS();

            if (gridViewClients != null)
            {
                if (gridViewClients.SelectedRowClient != null)
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    if (tr.translateAllMessageBoxDialog("Are you sure that you want to delete this client?", "Delete") == System.Windows.Forms.DialogResult.Yes)
                    {
                        Cursor.Current = Cursors.WaitCursor;

                       
                            if (pb.checkIsInInvoice(gridViewClients.SelectedRowClient.idClient) <= 0)
                            {
                                if (pb.checkIsInMeetings(gridViewClients.SelectedRowClient.idClient) <= 0)
                                {
                                    if (pb.checkIsInMultimedia(gridViewClients.SelectedRowClient.idClient) <= 0)
                                    {
                                        if (pb.checkIsInArrangementPrice(gridViewClients.SelectedRowClient.idClient) <= 0)
                                        {
                                            if (pb.checkIsInArrangementBookArticles(gridViewClients.SelectedRowClient.idClient) <= 0)
                                            {
                                                if (cd.canDelete("BISAppointment", "client", gridViewClients.SelectedRowClient.idClient.ToString()) == false)
                                                {
                                                    if (cd.canDelete("ArrangementCalculationFirstArticles", "idClient", gridViewClients.SelectedRowClient.idClient.ToString()) == false)
                                                    {
                                                        if (cd.canDelete("Documents", "idClient", gridViewClients.SelectedRowClient.idClient.ToString()) == false)
                                                        {
                                                            if (cd.canDelete("ToDo", "idClient", gridViewClients.SelectedRowClient.idClient.ToString()) == false)
                                                            {
                                                                if (cd.canDelete("Contacts", "idClient", gridViewClients.SelectedRowClient.idClient.ToString()) == false)
                                                                {
                                                                    if (pb.DeleteClient(gridViewClients.SelectedRowClient.idClient, this.Name, Login._user.idUser) == true)
                                                                    {

                                                                        gridViewClients.removeRow(gridViewClients.SelectedRowClient);
                                                                        //if (activeGrid != null)
                                                                        //{
                                                                        //    modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
                                                                        //    activeGrid.SetDataPersonBinding(modelData);
                                                                        //} 
                                                                        translateRadMessageBox trs = new translateRadMessageBox();
                                                                        trs.translateAllMessageBox("You have delete successfully!");
                                                                    }
                                                                    else
                                                                    {
                                                                        translateRadMessageBox trs = new translateRadMessageBox();
                                                                        trs.translateAllMessageBox("Something went wrong. Please contact your administrator!");
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    translateRadMessageBox trs = new translateRadMessageBox();
                                                                    trs.translateAllMessageBox("First you have to delete contacts for that client!");
                                                                }
                                                            }
                                                            else
                                                            {
                                                                translateRadMessageBox trs = new translateRadMessageBox();
                                                                trs.translateAllMessageBox("First you have to delete task for that client!");
                                                            }
                                                        }
                                                        else
                                                        {
                                                            translateRadMessageBox trs = new translateRadMessageBox();
                                                            trs.translateAllMessageBox("First you have to delete documents for that client!");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        translateRadMessageBox trs = new translateRadMessageBox();
                                                        trs.translateAllMessageBox("First you have to delete arrangement first calculation articles for that client!");
                                                    }
                                                }
                                                else
                                                {
                                                    translateRadMessageBox trs = new translateRadMessageBox();
                                                    trs.translateAllMessageBox("First you have to appointments for that client!");
                                                }
                                            }
                                            else
                                            {
                                                translateRadMessageBox trs = new translateRadMessageBox();
                                                trs.translateAllMessageBox("Client have not be deleted because it was booked some of the article contract!");
                                            }
                                        }
                                        else
                                        {
                                            translateRadMessageBox trs = new translateRadMessageBox();
                                            trs.translateAllMessageBox("First you have to delete arrangement price for that client!");
                                        }
                                    }

                                    else
                                    {
                                        translateRadMessageBox trs = new translateRadMessageBox();
                                        trs.translateAllMessageBox("First you have to delete multimedia for that client!");
                                    }
                                }
                                else
                                {
                                    translateRadMessageBox trs = new translateRadMessageBox();
                                    trs.translateAllMessageBox("First you have to delete meetings for that client!");
                                }
                            }

                            else
                            {
                                translateRadMessageBox trs = new translateRadMessageBox();
                                trs.translateAllMessageBox("First you have to delete that client's invoice if this is possible!");
                            }
                      
                    }
                }
                else
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("You have to have selected at least one client!");
                }
            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("You have to have at least one client!");
            }
            Cursor.Current = Cursors.Default;
        }
       
        private void deleteAgeCategory()
        {
            AgeCategoryBUS pb = new AgeCategoryBUS();
            if (gridViewAgeCategory != null)
            {
                if (gridViewAgeCategory.SelectedRowAgeCategory != null)
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    if (tr.translateAllMessageBoxDialog("Are you sure that you want to delete this age category?", "Delete") == System.Windows.Forms.DialogResult.Yes)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        if (pb.checkIsInArrangemnet(gridViewAgeCategory.SelectedRowAgeCategory.idAgeCategory) <= 0)
                        {


                            if (pb.DeleteAgeCategorySript(gridViewAgeCategory.SelectedRowAgeCategory.idAgeCategory, this.Name, Login._user.idUser) == true)
                            {
                                gridViewAgeCategory.removeRow(gridViewAgeCategory.SelectedRowAgeCategory);
                                //if (activeGrid != null)
                                //{
                                //    modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
                                //    activeGrid.SetDataPersonBinding(modelData);
                                //} 
                                translateRadMessageBox trs = new translateRadMessageBox();
                                trs.translateAllMessageBox("You have delete successfully!");
                            }
                            else
                            {
                                translateRadMessageBox trs = new translateRadMessageBox();
                                trs.translateAllMessageBox("Something went wrong. Please contact your administrator!");
                            }

                        }

                        else
                        {
                            translateRadMessageBox trs = new translateRadMessageBox();
                            trs.translateAllMessageBox("You haven't delete because this some of Arrangement use this age category!");
                        }
                    }

                }
                else
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("You have to have selected at least one age category!");
                }
            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("You have to have at least one age category!");
            }
            Cursor.Current = Cursors.Default;
        }

        private void deleteEmployee()
        {
            EmployeeBUS pb = new EmployeeBUS();
            CanDeleteBUS cd = new CanDeleteBUS();
            if (gridViewEmployee != null)
            {
                if (gridViewEmployee.SelectedRowPerson != null)
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    if (tr.translateAllMessageBoxDialog("Are you sure that you want to delete this Employee?", "Delete") == System.Windows.Forms.DialogResult.Yes)
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        if (cd.canDelete("Documents", "idEmployee", gridViewEmployee.SelectedRowPerson.idEmployee.ToString()) == false)
                        {
                            if (cd.canDelete("ToDo", "idEmployee", gridViewEmployee.SelectedRowPerson.idEmployee.ToString()) == false)
                            {
                                if (cd.canDelete("Meetings", "emploeeOwner", gridViewEmployee.SelectedRowPerson.idEmployee.ToString()) == false)
                                {
                                    if (cd.canDelete("Users", "idEmployee", gridViewEmployee.SelectedRowPerson.idEmployee.ToString()) == false)
                                    {
                                        if (cd.canDelete("ContactPersonNotes", "idEmployee", gridViewEmployee.SelectedRowPerson.idEmployee.ToString()) == false)
                                        {
                                            if (cd.canDelete("ClientNotes", "idEmployee", gridViewEmployee.SelectedRowPerson.idEmployee.ToString()) == false)
                                            {
                                                if (pb.DeleteEmployee(gridViewEmployee.SelectedRowPerson.idEmployee, this.Name, Login._user.idUser) == true)
                                                {
                                                    gridViewEmployee.removeRow(gridViewEmployee.SelectedRowPerson);

                                                    translateRadMessageBox trs = new translateRadMessageBox();
                                                    trs.translateAllMessageBox("You have delete successfully!");
                                                }

                                                else
                                                {
                                                    translateRadMessageBox trs = new translateRadMessageBox();
                                                    trs.translateAllMessageBox("Something went wrong. Please contact your administrator!");
                                                }
                                            }
                                            else
                                            {
                                                translateRadMessageBox trs = new translateRadMessageBox();
                                                trs.translateAllMessageBox("First you have to delete employee from client's notes!");
                                            }
                                        }
                                        else
                                        {
                                            translateRadMessageBox trs = new translateRadMessageBox();
                                            trs.translateAllMessageBox("First you have to delete perons's notes from that employee!");
                                        }
                                    }
                                    else
                                    {
                                        translateRadMessageBox trs = new translateRadMessageBox();
                                        trs.translateAllMessageBox("Some of user used that employee!Please contact your administrator!");
                                    }
                                }
                                else
                                {
                                    translateRadMessageBox trs = new translateRadMessageBox();
                                    trs.translateAllMessageBox("First you have to delete employee's meeting!");
                                }
                            }
                            else
                            {
                                translateRadMessageBox trs = new translateRadMessageBox();
                                trs.translateAllMessageBox("First you have to delete task for that employee!");
                            }
                        }
                        else
                        {
                            translateRadMessageBox trs = new translateRadMessageBox();
                            trs.translateAllMessageBox("First you have to delete that employee's document!");
                        }
                    }
                }
                else
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("You have to have selected at least one employee!");
                }
            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("You have to have at least one employee!");
            }
            Cursor.Current = Cursors.Default;

        }

        private void deleteFilterLabel()
        {
            FiltersLabelsBUS _flBUS = new FiltersLabelsBUS();
            //TypeBUS tBUS = new TypeBUS();
            List<LastIdModel> fModel = new List<LastIdModel>();
            bool cont = true;
            //FiltersLabelsBUS _flBUS = new FiltersLabelsBUS();
            //TypeBUS tBUS = new TypeBUS();
            string tip = gridViewFilterLabel.SelectedRowFiltersLabels.type;
            int a = Convert.ToInt32(gridViewFilterLabel.SelectedRowFiltersLabels.ID);
            if (tip == "Filter")
            {
                if (gridViewFilterLabel != null)
                {
                    if (gridViewFilterLabel.SelectedRowFiltersLabels != null)
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        if (tr.translateAllMessageBoxDialog("Are you sure that you want to delete this filter?", "Delete") == System.Windows.Forms.DialogResult.Yes)
                        {
                            Cursor.Current = Cursors.WaitCursor;
                            if (_flBUS.checkIsInArrangementFilter(gridViewFilterLabel.SelectedRowFiltersLabels.ID) <= 0)
                            {

                                if (_flBUS.checkIsInClientFilter(gridViewFilterLabel.SelectedRowFiltersLabels.ID) <= 0)
                                {
                                    if (_flBUS.checkIsInContactPersonFilter(gridViewFilterLabel.SelectedRowFiltersLabels.ID) <= 0)
                                    {
                                        if (_flBUS.checkIsInEmployeeFilter(gridViewFilterLabel.SelectedRowFiltersLabels.ID) <= 0)
                                        {
                                            if (_flBUS.checkIsInUserFilter(gridViewFilterLabel.SelectedRowFiltersLabels.ID) <= 0)
                                            {
                                                if (_flBUS.DeleteFilter(gridViewFilterLabel.SelectedRowFiltersLabels.ID, this.Name, Login._user.idUser) == true)
                                                {
                                                    gridViewFilterLabel.removeRow(gridViewFilterLabel.SelectedRowFiltersLabels);
                                                    //if (activeGrid != null)
                                                    //{
                                                    //    modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
                                                    //    activeGrid.SetDataPersonBinding(modelData);
                                                    //} 
                                                    translateRadMessageBox trs = new translateRadMessageBox();
                                                    trs.translateAllMessageBox("You have delete successfully!");
                                                }
                                                else
                                                {
                                                    translateRadMessageBox trs = new translateRadMessageBox();
                                                    trs.translateAllMessageBox("Something went wrong. Please contact your administrator!");
                                                }

                                            }
                                            else
                                            {
                                                translateRadMessageBox trs = new translateRadMessageBox();
                                                trs.translateAllMessageBox("You can't delete the filter because some of user have this filter!");
                                            }
                                        }
                                        else
                                        {
                                            translateRadMessageBox trs = new translateRadMessageBox();
                                            trs.translateAllMessageBox("You can't delete the filter because some of employee have this filter!");
                                        }
                                    }
                                    else
                                    {
                                        translateRadMessageBox trs = new translateRadMessageBox();
                                        trs.translateAllMessageBox("You can't delete the filter because some of person have this filter!");
                                    }
                                }
                                else
                                {
                                    translateRadMessageBox trs = new translateRadMessageBox();
                                    trs.translateAllMessageBox("You can't delete the filter because some of client have this filter!");
                                }
                            }

                            else
                            {
                                translateRadMessageBox trs = new translateRadMessageBox();
                                trs.translateAllMessageBox("You can't delete the filter because there is in the calculation!");
                            }
                        }
                    }
                    else
                    {
                        translateRadMessageBox trs = new translateRadMessageBox();
                        trs.translateAllMessageBox("You have to have selected at least one filter!");
                    }
                }
                else
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("You have to have at least one filter!");
                }

            }
            if (tip == "Label")
            {


                if (gridViewFilterLabel != null)
                {
                    int idLabel = -1;
                    int idUnique = -1;
                    if (gridViewFilterLabel.SelectedRowFiltersLabels != null)
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        if (tr.translateAllMessageBoxDialog("Are you sure that you want to delete this label?", "Delete") == System.Windows.Forms.DialogResult.Yes)
                        {
                            Cursor.Current = Cursors.WaitCursor;
                            if (_flBUS.checkIsInArrangementLabel(gridViewFilterLabel.SelectedRowFiltersLabels.IDLabelUnique) <= 0)
                            {
                                if (_flBUS.checkIsInArrangementLabelFirst(gridViewFilterLabel.SelectedRowFiltersLabels.IDLabelUnique) <= 0)
                                {
                                    if (_flBUS.checkIsInClient(gridViewFilterLabel.SelectedRowFiltersLabels.IDLabelUnique) <= 0)
                                    {
                                        if (_flBUS.checkIsInContactPerson(gridViewFilterLabel.SelectedRowFiltersLabels.IDLabelUnique) <= 0)
                                        {
                                            if (_flBUS.checkIsInEmployee(gridViewFilterLabel.SelectedRowFiltersLabels.IDLabelUnique) <= 0)
                                            {
                                                if (_flBUS.checkIsInUser(gridViewFilterLabel.SelectedRowFiltersLabels.IDLabelUnique) <= 0)
                                                {
                                                    if (gridViewFilterLabel.SelectedRowFiltersLabels.IDLabelUnique > 0)
                                                        idUnique = gridViewFilterLabel.SelectedRowFiltersLabels.IDLabelUnique;
                                                    else
                                                        idLabel = gridViewFilterLabel.SelectedRowFiltersLabels.ID;
                                                    if (_flBUS.DeleteLabel(idLabel, idUnique, this.Name, Login._user.idUser) == true)
                                                    {
                                                        gridViewFilterLabel.removeRow(gridViewFilterLabel.SelectedRowFiltersLabels);
                                                        //if (activeGrid != null)
                                                        //{
                                                        //    modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
                                                        //    activeGrid.SetDataPersonBinding(modelData);
                                                        //} 
                                                        translateRadMessageBox trs = new translateRadMessageBox();
                                                        trs.translateAllMessageBox("You have delete successfully!");
                                                    }
                                                    else
                                                    {
                                                        translateRadMessageBox trs = new translateRadMessageBox();
                                                        trs.translateAllMessageBox("Something went wrong. Please contact your administrator!");
                                                    }

                                                }
                                                else
                                                {
                                                    translateRadMessageBox trs = new translateRadMessageBox();
                                                    trs.translateAllMessageBox("You can't delete the label because some of user have this label!");
                                                }
                                            }
                                            else
                                            {
                                                translateRadMessageBox trs = new translateRadMessageBox();
                                                trs.translateAllMessageBox("You can't delete the label because some of employee have this label!");
                                            }
                                        }
                                        else
                                        {
                                            translateRadMessageBox trs = new translateRadMessageBox();
                                            trs.translateAllMessageBox("You can't delete the label because some of person have this label!");
                                        }
                                    }
                                    else
                                    {
                                        translateRadMessageBox trs = new translateRadMessageBox();
                                        trs.translateAllMessageBox("You can't delete the label because some of client have this label!");
                                    }
                                }
                                else
                                {
                                    translateRadMessageBox trs = new translateRadMessageBox();
                                    trs.translateAllMessageBox("You can't delete the label because there is in the first calculation!");
                                }
                            }
                            else
                            {
                                translateRadMessageBox trs = new translateRadMessageBox();
                                trs.translateAllMessageBox("You can't delete the label because there is in the calculation!");
                            }
                        }
                    }
                    else
                    {
                        translateRadMessageBox trs = new translateRadMessageBox();
                        trs.translateAllMessageBox("You have to have selected at least one label!");
                    }
                }
                else
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("You have to have at least one label!");
                }

            }
        }

        private void deleteHotelservice()
        {
            HotelServicesBUS pb = new HotelServicesBUS();
            if (gridViewHotelservices != null)
            {
                if (gridViewHotelservices.SelectedRowHotelServices != null)
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    if (tr.translateAllMessageBoxDialog("Are you sure that you want to delete this hotel service?", "Delete") == System.Windows.Forms.DialogResult.Yes)
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        if (pb.checkIsInArrangemnet(gridViewHotelservices.SelectedRowHotelServices.idHotelService) <= 0)
                        {

                            if (pb.DeleteHotelServicesSript(gridViewHotelservices.SelectedRowHotelServices.idHotelService, this.Name, Login._user.idUser) == true)
                            {
                                gridViewHotelservices.removeRow(gridViewHotelservices.SelectedRowHotelServices);

                                translateRadMessageBox trs = new translateRadMessageBox();
                                trs.translateAllMessageBox("You have delete successfully!");
                            }

                            else
                            {
                                translateRadMessageBox trs = new translateRadMessageBox();
                                trs.translateAllMessageBox("Something went wrong. Please contact your administrator!");
                            }
                        }

                        else
                        {
                            translateRadMessageBox trs = new translateRadMessageBox();
                            trs.translateAllMessageBox("Hotel service have not be deleted because services used in arrangement!");
                        }
                    }

                }
                else
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("You have to have selected at least one hotel service!");
                }
            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("You have to have at least one hotel service!");
            }
            Cursor.Current = Cursors.Default;
        }

        private void deleteType()
        {
            string tip = gridViewType.SelectedRowType.type;
            List<TranslateUstrModel> translate;
            UsersBUS ub = new UsersBUS();
            string prevod = "";
        

            translate = ub.Translate("Acc daily type", Login._user.lngUser);
            if (translate != null)
            {
                if (translate.Count > 0)
                {
                    prevod = translate[0].stringValue;
                    if (tip == prevod)
                    {              
                        acc();
                    }
                }
            }

            translate = ub.Translate("Address type", Login._user.lngUser);
            if (translate != null)
            {
                if (translate.Count > 0)
                {
                    prevod = translate[0].stringValue;
                    if (tip == prevod)
                    {
                      address();
                    }
                }
            }
            translate = ub.Translate("Client type", Login._user.lngUser);
            if (translate != null)
            {
                if (translate.Count > 0)
                {
                    prevod = translate[0].stringValue;
                    if (tip == prevod)
                    {
                        client();
                    }
                }
            }
            translate = ub.Translate("Contact type", Login._user.lngUser);
            if (translate != null)
            {
                if (translate.Count > 0)
                {
                    prevod = translate[0].stringValue;
                    if (tip == prevod)
                    {
                        contactType();
                    }
                }
            }
            translate = ub.Translate("Email type", Login._user.lngUser);
            if (translate != null)
            {
                if (translate.Count > 0)
                {
                    prevod = translate[0].stringValue;
                    if (tip == prevod)
                    {
                        emailType();
                    }
                }
            }
            translate = ub.Translate("Medical answer type", Login._user.lngUser);
            if (translate != null)
            {
                if (translate.Count > 0)
                {
                    prevod = translate[0].stringValue;
                    if (tip == prevod)
                    {
                        medicalAnswerType();
                    }
                }
            }
            translate = ub.Translate("Note type", Login._user.lngUser);
            if (translate != null)
            {
                if (translate.Count > 0)
                {
                    prevod = translate[0].stringValue;
                    if (tip == prevod)
                    {
                        noteType();
                    }
                }
            }
            translate = ub.Translate("Telephone type", Login._user.lngUser);
            if (translate != null)
            {
                if (translate.Count > 0)
                {
                    prevod = translate[0].stringValue;
                    if (tip == prevod)
                    {
                        telephoneType();
                    }
                }
            }
            translate = ub.Translate("To do type", Login._user.lngUser);
            if (translate != null)
            {
                if (translate.Count > 0)
                {

                    prevod = translate[0].stringValue;
                    if (tip == prevod)
                    {
                        toDoType();
                    }
                }
            }
            translate = ub.Translate("Volontary answer type", Login._user.lngUser);
            if (translate != null)
            {
                if (translate.Count > 0)
                {
                    prevod = translate[0].stringValue;
                    if (tip == prevod)
                    {
                        volontaryAnswerType();
                    }
                }
            }
            translate = ub.Translate("Meeting category", Login._user.lngUser);
            if (translate != null)
            {
                if (translate.Count > 0)
                {
                    prevod = translate[0].stringValue;
                    if (tip == prevod)
                    {
                        meetingCategory();
                    }
                }
            }
            translate = ub.Translate("Meeting priority", Login._user.lngUser);
            if (translate != null)
            {
                if (translate.Count > 0)
                {
                    prevod = translate[0].stringValue;
                    if (tip == prevod)
                    {
                        meetingPriority();
                    }
                }
            }
            translate = ub.Translate("To do priority", Login._user.lngUser);
            if (translate != null)
            {
                if (translate.Count > 0)
                {
                    prevod = translate[0].stringValue;
                    if (tip == prevod)
                    {
                        toDoPriority();
                    }
                }
            }
            translate = ub.Translate("Title", Login._user.lngUser);
            if (translate != null)
            {
                if (translate.Count > 0)
                {
                    prevod = translate[0].stringValue;
                    if (tip == prevod)
                    {
                        title();
                    }
                }
            }
            translate = ub.Translate("Employee function", Login._user.lngUser);
            if (translate != null)
            {
                if (translate.Count > 0)
                {
                    prevod = translate[0].stringValue;
                    if (tip == prevod)
                    {
                        employeeFunction();
                    }
                }
            }
            translate = ub.Translate("Employee status", Login._user.lngUser);
            if (translate != null)
            {
                if (translate.Count > 0)
                {
                    prevod = translate[0].stringValue;
                    if (tip == prevod)
                    {
                        employeeStatus();
                    }
                }
            }
            translate = ub.Translate("Meeting status", Login._user.lngUser);
            if (translate != null)
            {
                if (translate.Count > 0)
                {
                    prevod = translate[0].stringValue;
                    if (tip == prevod)
                    {
                        meetingStatus();
                    }
                }
            }
            translate = ub.Translate("To do status", Login._user.lngUser);
            if (translate != null)
            {
                if (translate.Count > 0)
                {
                    prevod = translate[0].stringValue;
                    if (tip == prevod)
                    {
                        toDoStatus();
                    }
                }
            }

            translate = ub.Translate("Contact reason", Login._user.lngUser);
            if (translate != null)
            {
                if (translate.Count > 0)
                {
                    prevod = translate[0].stringValue;
                    if (tip == prevod)
                    {
                       contactReason();
                    }
                }
            }


            modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
            activeGrid.SetDataPersonBinding(modelData);
        }

        private void deleteArticle()
        {
            CanDeleteBUS cd = new CanDeleteBUS();
            if (gridViewArtical != null)
            {
                if (gridViewArtical.SelectedRowArtical != null)
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    if (tr.translateAllMessageBoxDialog("Are you sure that you want to delete this article?", "Delete") == System.Windows.Forms.DialogResult.Yes)
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        if (cd.canDelete("ArrangementBookArticles", "idArticle", gridViewArtical.SelectedRowArtical.codeArtical.ToString()) == false)
                        {
                            if (cd.canDelete("ArrangementCalculationFirstArticles", "idArticle", gridViewArtical.SelectedRowArtical.codeArtical.ToString()) == false)
                            {
                                if (cd.canDelete("ArrangementPrice", "idArticle", gridViewArtical.SelectedRowArtical.codeArtical.ToString()) == false)
                                {
                                    if (cd.canDelete("ArrangementInvoicePrice", "idArticle", gridViewArtical.SelectedRowArtical.codeArtical.ToString()) == false)
                                    {
                                        if (cd.canDelete("ArrangementRooms", "idArticle", gridViewArtical.SelectedRowArtical.codeArtical.ToString()) == false)
                                        {
                                            if (cd.canDelete("InvoiceItems", "idArtical", gridViewArtical.SelectedRowArtical.codeArtical.ToString()) == false)
                                            {
                                                if (cd.canDelete("Multimedia", "idArticle", gridViewArtical.SelectedRowArtical.codeArtical.ToString()) == false)
                                                {
                                                    if (cd.canDelete("PriceListArticles", "idArticle", gridViewArtical.SelectedRowArtical.codeArtical.ToString()) == false)
                                                    {
                                                        ArticalBUS ab = new ArticalBUS();
                                                        if (ab.Delete(gridViewArtical.SelectedRowArtical.codeArtical.ToString(), this.Name, Login._user.idUser) == true)
                                                        {
                                                            modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
                                                            activeGrid.SetDataPersonBinding(modelData);
                                                            translateRadMessageBox trs = new translateRadMessageBox();
                                                            trs.translateAllMessageBox("You have delete successfully!");
                                                        }
                                                        else
                                                        {
                                                            translateRadMessageBox trs = new translateRadMessageBox();
                                                            trs.translateAllMessageBox("Something went wrong with deleting please contact your administrator!");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        translateRadMessageBox trs = new translateRadMessageBox();
                                                        trs.translateAllMessageBox("You can't delete article because it is already added in contracts!");
                                                    }
                                                }
                                                else
                                                {
                                                    translateRadMessageBox trs = new translateRadMessageBox();
                                                    trs.translateAllMessageBox("You can't delete article because it is already added multimedia for web for this article!");
                                                }
                                            }
                                            else
                                            {
                                                translateRadMessageBox trs = new translateRadMessageBox();
                                                trs.translateAllMessageBox("You can't delete article because it is in invoice item already!");
                                            }
                                        }
                                        else
                                        {
                                            translateRadMessageBox trs = new translateRadMessageBox();
                                            trs.translateAllMessageBox("You can't delete article because it is already added room number!");
                                        }
                                    }
                                    else
                                    {
                                        translateRadMessageBox trs = new translateRadMessageBox();
                                        trs.translateAllMessageBox("You can't delete article because it is in invoice price!");
                                    }
                                }
                                else
                                {
                                    translateRadMessageBox trs = new translateRadMessageBox();
                                    trs.translateAllMessageBox("You can't delete article because it is in calculation price!");
                                }
                            }
                            else
                            {
                                translateRadMessageBox trs = new translateRadMessageBox();
                                trs.translateAllMessageBox("You can't delete article because it is in first calculation!");
                            }
                        }
                        else
                        {
                            translateRadMessageBox trs = new translateRadMessageBox();
                            trs.translateAllMessageBox("You can't delete article because it is booked!");
                        }
                    }
                }
            }
        }

        private void deleteArticleGroup()
        {
            CanDeleteBUS cd = new CanDeleteBUS();
            if (gridViewArticalGroups != null)
            {
                if (gridViewArticalGroups.SelectedRowArticalGroups != null)
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    if (tr.translateAllMessageBoxDialog("Are you sure that you want to delete this article group?", "Delete") == System.Windows.Forms.DialogResult.Yes)
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        if (cd.canDelete("Artical", "codeArtikalGroup", gridViewArticalGroups.SelectedRowArticalGroups.codeArticalGroup.ToString()) == false)
                        {

                            ArticalGroupsBUS ab = new ArticalGroupsBUS();
                            if (ab.Delete(gridViewArticalGroups.SelectedRowArticalGroups.codeArticalGroup.ToString(), this.Name, Login._user.idUser) == true)
                            {
                                modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
                                activeGrid.SetDataPersonBinding(modelData);
                                translateRadMessageBox trs = new translateRadMessageBox();
                                trs.translateAllMessageBox("You have delete successfully!");
                            }
                            else
                            {
                                translateRadMessageBox trs = new translateRadMessageBox();
                                trs.translateAllMessageBox("Something went wrong with deleting please contact your administrator!");
                            }
                        }
                        else
                        {
                            translateRadMessageBox trs = new translateRadMessageBox();
                            trs.translateAllMessageBox("You can't delete article group because it has article!");
                        }
                    }
                }
            }
        }

        private void deleteMultimediaServer()
        {
            MultimediaServerBUS pb = new MultimediaServerBUS();
            if (gridViewMultimediaServer != null)
            {
                if (gridViewMultimediaServer.SelectedRowMultimediaServer != null)
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    if (tr.translateAllMessageBoxDialog("Are you sure that you want to delete this record?", "Delete") == System.Windows.Forms.DialogResult.Yes)
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        if (pb.checkIsInMultimedia(gridViewMultimediaServer.SelectedRowMultimediaServer.idServer) <= 0)
                        {

                            if (pb.DeleteMultimediaServerSript(gridViewMultimediaServer.SelectedRowMultimediaServer.idServer) == true)
                            {
                                // gridViewMultimediaServer.removeRow(gridViewMultimediaServer.SelectedRowMultimediaServer);
                                gridViewMultimediaServer.removeRow(gridViewMultimediaServer.SelectedRowMultimediaServer);

                                translateRadMessageBox trs = new translateRadMessageBox();
                                trs.translateAllMessageBox("You have delete successfully!");
                            }

                            else
                            {
                                translateRadMessageBox trs = new translateRadMessageBox();
                                trs.translateAllMessageBox("Something went wrong. Please contact your administrator!");
                            }
                        }
                        else
                        {
                            translateRadMessageBox trs = new translateRadMessageBox();
                            trs.translateAllMessageBox("You can't delete because multimedia using this server!");
                        }
                    }

                }
                else
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("You have to have selected at least one server!");
                }
            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("You have to have at least one server!");
            }

        }

        private void deleteMedical()
        {

            MedicalVoluntaryBUS mvb = new MedicalVoluntaryBUS();
            bool isOneAns = false;
            bool isOneQuest = false;
            string messageAns = "";
            string messageQuest = "";
            string messageGroup = "";
            if (gridViewMedical != null)
            {
                if (gridViewMedical.SelectedRowMedical != null)
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    if (tr.translateAllMessageBoxDialog("Are you sure that you want to delete this record?", "Delete") == System.Windows.Forms.DialogResult.Yes)
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        AnswerModel am = new AnswerModel();
                        QuestModel qg = new QuestModel();
                        if (gridViewMedical.SelectedRowMedical.idAns.ToString() != "")
                        {
                            am.idAns = Convert.ToInt32(gridViewMedical.SelectedRowMedical.idAns);
                            messageAns += gridViewMedical.SelectedRowMedical.txtAns.ToString();
                        }
                        else
                        {
                            am.idAns = -1;
                        }
                        if (gridViewMedical.SelectedRowMedical.idQuest.ToString() != "")
                        {
                            am.idQuest = Convert.ToInt32(gridViewMedical.SelectedRowMedical.idQuest);
                            qg.idQuest = Convert.ToInt32(gridViewMedical.SelectedRowMedical.idQuest);
                            messageQuest += ", " + gridViewMedical.SelectedRowMedical.txtQuest.ToString() + " quest";
                        }
                        else
                        {
                            am.idQuest = -1;
                            qg.idQuest = -1;
                        }
                        am.idQueryType = Convert.ToInt32(gridViewMedical.SelectedRowMedical.idAnsType);
                        if (gridViewMedical.SelectedRowMedical.idQuestGroup.ToString() != "")
                        {
                            qg.idQuestGroup = Convert.ToInt32(gridViewMedical.SelectedRowMedical.idQuestGroup);
                            messageGroup += " and " + gridViewMedical.SelectedRowMedical.nameQuestGroup + " group";
                        }

                        if (mvb.checkIsOneAns(am) <= 1)
                        {
                            isOneAns = true;


                        }
                        else
                        {
                            isOneAns = false;
                        }

                        if (mvb.checkIsOneQuest(qg.idQuestGroup) <= 1)
                        {
                            isOneQuest = true;
                        }
                        else
                        {
                            isOneQuest = false;
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
                                            if (am.idQuest != 169)
                                            {
                                                if (am.idQuest != 219)
                                                {
                                                    if (am.idQuest != 168)
                                                    {
                                                        if (am.idQuest != 177)
                                                        {
                                                            if (am.idQuest != 178)
                                                            {
                                                                if (am.idQuest != 179)
                                                                {
                                                                    if (mvb.checkAnsIsInMedCpr(am.idAns, Convert.ToInt32(am.idQueryType.ToString())) == 0)
                                                                    {
                                                                        if (isOneQuest == true)
                                                                        {
                                                                            if (isOneAns == true)
                                                                            {
                                                                                if (mvb.DeleteAnsSript(am, isOneAns, isOneQuest, qg.idQuestGroup, this.Name, Login._user.idUser) != true)
                                                                                {
                                                                                    translateRadMessageBox trs = new translateRadMessageBox();
                                                                                    trs.translateAllMessageBox("You haven't successfully delete answer");
                                                                                }
                                                                                else
                                                                                {
                                                                                    translateRadMessageBox trs = new translateRadMessageBox();
                                                                                    trs.translateAllMessageBox("You have delete successfully: " + messageAns + messageQuest + messageGroup);
                                                                                    gridViewMedical.removeRow(gridViewMedical.SelectedRowMedical);
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                translateRadMessageBox trs = new translateRadMessageBox();
                                                                                trs.translateAllMessageBox("First, you have to delete answers and questins from htis group");
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            translateRadMessageBox trs = new translateRadMessageBox();
                                                                            trs.translateAllMessageBox("First, you have to delete question from htis group");
                                                                        }
                                                                    }
                                                                    else
                                                                    {

                                                                        translateRadMessageBox trs = new translateRadMessageBox();
                                                                        trs.translateAllMessageBox("You can't delete answer, because some person already filled this answer");
                                                                    }
                                                                }
                                                                else
                                                                {

                                                                    translateRadMessageBox trs = new translateRadMessageBox();
                                                                    trs.translateAllMessageBox("You can't delete this group, because this is question for medication");
                                                                }
                                                            }
                                                            else
                                                            {

                                                                translateRadMessageBox trs = new translateRadMessageBox();
                                                                trs.translateAllMessageBox("You can't delete this group, because this is question for epilepsie");
                                                            }
                                                        }
                                                        else
                                                        {

                                                            translateRadMessageBox trs = new translateRadMessageBox();
                                                            trs.translateAllMessageBox("You can't delete this group, because this is question for diets");
                                                        }
                                                    }
                                                    else
                                                    {

                                                        translateRadMessageBox trs = new translateRadMessageBox();
                                                        trs.translateAllMessageBox("You can't delete this group, because this is question for device");
                                                    }
                                                }
                                                else
                                                {

                                                    translateRadMessageBox trs = new translateRadMessageBox();
                                                    trs.translateAllMessageBox("You can't delete this group, because this is question for rented device");
                                                }
                                            }
                                            else
                                            {

                                                translateRadMessageBox trs = new translateRadMessageBox();
                                                trs.translateAllMessageBox("You can't delete this group, because this is question for airport codes");
                                            }
                                        }
                                        else
                                        {

                                            translateRadMessageBox trs = new translateRadMessageBox();
                                            trs.translateAllMessageBox("You can't delete this answer, because it is answer for anchorage");
                                        }
                                    }
                                    else
                                    {

                                        translateRadMessageBox trs = new translateRadMessageBox();
                                        trs.translateAllMessageBox("You can't delete this answer, because it is answer for arm using sometimes");
                                    }
                                }
                                else
                                {

                                    translateRadMessageBox trs = new translateRadMessageBox();
                                    trs.translateAllMessageBox("You can't delete this answer, because it is answer for arm using always");
                                }
                            }
                            else
                            {

                                translateRadMessageBox trs = new translateRadMessageBox();
                                trs.translateAllMessageBox("You can't delete this answer, because it is answer for wheelchair");
                            }
                        }
                        else
                        {

                            translateRadMessageBox trs = new translateRadMessageBox();
                            trs.translateAllMessageBox("You can't delete this answer, because it is answer for rollator");
                        }
                    }
                    else
                    {
                        //translateRadMessageBox trs = new translateRadMessageBox();
                        //trs.translateAllMessageBox("You haven't successfully delete answer because there is no id");
                    }
                }
                else
                {

                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("You have to have selected at least one group!");
                }
            }

            else
            {

                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("You have to have at least one group.");
            }
        }
        //===========================
        //private void deleteVoluntaryFunc()
        //{

        //    VolontaryFunctionBUS mvb = new VolontaryFunctionBUS();
        //    bool isOneAns = false;
        //    bool isOneQuest = false;
        //    string messageAns = "";
        //    string messageQuest = "";
        //    string messageGroup = "";
        //    if (gridViewVoluntary != null)
        //    {
        //        if (gridViewVoluntaryFunc.SelectedRowMedical != null)
        //        {
        //            translateRadMessageBox tr = new translateRadMessageBox();
        //            if (tr.translateAllMessageBoxDialog("Are you sure that you want to delete this record?", "Delete") == System.Windows.Forms.DialogResult.Yes)
        //            {
        //                Cursor.Current = Cursors.WaitCursor;

        //                AnswerModel am = new AnswerModel();
        //                QuestModel qg = new QuestModel();
        //                if (gridViewVoluntaryFunc.SelectedRowMedical.idAns.ToString() != "")
        //                {
        //                    am.idAns = Convert.ToInt32(gridViewVoluntaryFunc.SelectedRowMedical.idAns);
        //                    messageAns += gridViewVoluntaryFunc.SelectedRowMedical.txtAns.ToString();
        //                }
        //                else
        //                {
        //                    am.idAns = -1;
        //                }
        //                if (gridViewVoluntaryFunc.SelectedRowMedical.idQuest.ToString() != "")
        //                {
        //                    //  am.idQuest = Convert.ToInt32(gridViewVoluntaryFunc.SelectedRowMedical.idQuest);
        //                    qg.idQuest = Convert.ToInt32(gridViewVoluntaryFunc.SelectedRowMedical.idQuest);
        //                    messageQuest += ", " + gridViewVoluntaryFunc.SelectedRowMedical.txtQuest.ToString() + " quest";
        //                }
        //                else
        //                {
        //                    am.idQuest = -1;
        //                    qg.idQuest = -1;
        //                }
        //                am.idQueryType = Convert.ToInt32(gridViewVoluntaryFunc.SelectedRowMedical.idAnsType);
        //                if (gridViewVoluntaryFunc.SelectedRowMedical.idQuestGroup.ToString() != "")
        //                {
        //                    qg.idQuestGroup = Convert.ToInt32(gridViewVoluntaryFunc.SelectedRowMedical.idQuestGroup);
        //                    messageGroup += " and " + gridViewVoluntaryFunc.SelectedRowMedical.nameQuestGroup + " group";
        //                }
        //                if (mvb.checkIsOneAnsVF(am) <= 1)
        //                {
        //                    isOneAns = true;
        //                }
        //                else
        //                {
        //                    isOneAns = false;
        //                }

        //                if (mvb.checkIsOneQuestVF(qg.idQuestGroup) <= 1)
        //                {
        //                    isOneQuest = true;
        //                }
        //                else
        //                {
        //                    isOneQuest = false;
        //                }

        //                if (mvb.checkAnsIsInVFCpr(am.idAns, Convert.ToInt32(am.idQueryType.ToString())) == 0)
        //                {
        //                    if (mvb.checkAnsIsInVFArr(am.idAns, Convert.ToInt32(am.idQueryType.ToString())) == 0)
        //                    {
        //                        if (isOneQuest == true)
        //                        {
        //                            if (isOneAns == true)
        //                            {
        //                                if (mvb.DeleteAnsSriptVF(am, isOneAns, isOneQuest, qg.idQuestGroup) != true)
        //                                {
        //                                    translateRadMessageBox trs = new translateRadMessageBox();
        //                                    trs.translateAllMessageBox("You haven't successfully delete answer");
        //                                }
        //                                else
        //                                {
        //                                    translateRadMessageBox trs = new translateRadMessageBox();
        //                                    trs.translateAllMessageBox("You have delete successfully: " + messageAns + messageQuest + messageGroup);
        //                                    gridViewVoluntaryFunc.removeRow(gridViewVoluntaryFunc.SelectedRowMedical);
        //                                }
        //                            }
        //                            else
        //                            {
        //                                translateRadMessageBox trs = new translateRadMessageBox();
        //                                trs.translateAllMessageBox("First, you have to delete answers and questins from htis group");
        //                            }
        //                        }
        //                        else
        //                        {
        //                            translateRadMessageBox trs = new translateRadMessageBox();
        //                            trs.translateAllMessageBox("First, you have to delete question from htis group");
        //                        }
        //                    }
        //                    else
        //                    {
        //                        translateRadMessageBox trs = new translateRadMessageBox();
        //                        trs.translateAllMessageBox("You can't delete answer, because some arrangement already filled this answer");
        //                    }
        //                }
        //                else
        //                {

        //                    translateRadMessageBox trs = new translateRadMessageBox();
        //                    trs.translateAllMessageBox("You can't delete answer, because some person already filled this answer");
        //                }
        //            }
        //            else
        //            {
        //                //translateRadMessageBox trs = new translateRadMessageBox();
        //                //trs.translateAllMessageBox("You haven't successfully delete answer because there is no id");
        //            }
        //        }
        //        else
        //        {
        //            translateRadMessageBox trs = new translateRadMessageBox();
        //            trs.translateAllMessageBox("You have to have selected at least one group!");
        //        }
        //    }
        //    else
        //    {
        //        translateRadMessageBox trs = new translateRadMessageBox();
        //        trs.translateAllMessageBox("You have to have at least one group.");
        //    }
        //    Cursor.Current = Cursors.Default;
        //}

        private void deleteVoluntaryFunc()
        {

            VolontaryFunctionBUS mvb = new VolontaryFunctionBUS();
            bool isOneAns = false;
            bool isOneQuest = false;
            string messageAns = "";
            string messageQuest = "";
            string messageGroup = "";
            if (gridViewVoluntary != null)
            {
                if (gridViewVoluntaryFunc.SelectedRowMedical != null)
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    if (tr.translateAllMessageBoxDialog("Are you sure that you want to delete this record?", "Delete") == System.Windows.Forms.DialogResult.Yes)
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        AnswerModel am = new AnswerModel();
                        QuestModel qg = new QuestModel();
                        if (gridViewVoluntaryFunc.SelectedRowMedical.idAns.ToString() != "")
                        {
                            am.idAns = Convert.ToInt32(gridViewVoluntaryFunc.SelectedRowMedical.idAns);
                            messageAns += gridViewVoluntaryFunc.SelectedRowMedical.txtAns.ToString();
                        }
                        else
                        {
                            am.idAns = -1;
                        }
                        if (gridViewVoluntaryFunc.SelectedRowMedical.idQuest.ToString() != "")
                        {
                            //  am.idQuest = Convert.ToInt32(gridViewVoluntaryFunc.SelectedRowMedical.idQuest);
                            am.idQuest = Convert.ToInt32(gridViewVoluntaryFunc.SelectedRowMedical.idQuest);
                            messageQuest += ", " + gridViewVoluntaryFunc.SelectedRowMedical.txtQuest.ToString() + " quest";
                        }
                        else
                        {
                            am.idQuest = -1;
                            qg.idQuest = -1;
                        }
                        am.idQueryType = Convert.ToInt32(gridViewVoluntaryFunc.SelectedRowMedical.idAnsType);
                        if (gridViewVoluntaryFunc.SelectedRowMedical.idQuestGroup.ToString() != "")
                        {
                            qg.idQuestGroup = Convert.ToInt32(gridViewVoluntaryFunc.SelectedRowMedical.idQuestGroup);
                            messageGroup += " and " + gridViewVoluntaryFunc.SelectedRowMedical.nameQuestGroup + " group";
                        }
                        if (mvb.checkIsOneAnsVF(am) <= 1)
                        {
                            isOneAns = true;
                        }
                        else
                        {
                            isOneAns = false;
                        }

                        if (mvb.checkIsOneQuestVF(qg.idQuestGroup) <= 1)
                        {
                            isOneQuest = true;
                        }
                        else
                        {
                            isOneQuest = false;
                        }

                        if (mvb.checkAnsIsInVFCpr(am.idAns, Convert.ToInt32(am.idQueryType.ToString())) == 0)
                        {
                            if (mvb.checkAnsIsInVFArr(am.idAns, Convert.ToInt32(am.idQueryType.ToString())) == 0)
                            {
                                if (isOneQuest == true)
                                {
                                    if (isOneAns == true)
                                    {
                                        if (mvb.DeleteAnsSriptVF(am, isOneAns, isOneQuest, qg.idQuestGroup, this.Name, Login._user.idUser) != true)
                                        {
                                            translateRadMessageBox trs = new translateRadMessageBox();
                                            trs.translateAllMessageBox("You haven't successfully delete answer");
                                        }
                                        else
                                        {
                                            translateRadMessageBox trs = new translateRadMessageBox();
                                            trs.translateAllMessageBox("You have delete successfully: " + messageAns + messageQuest + messageGroup);
                                            gridViewVoluntaryFunc.removeRow(gridViewVoluntaryFunc.SelectedRowMedical);
                                        }
                                    }
                                    else
                                    {
                                        translateRadMessageBox trs = new translateRadMessageBox();
                                        trs.translateAllMessageBox("First, you have to delete answers and questins from htis group");
                                    }
                                }
                                else
                                {
                                    translateRadMessageBox trs = new translateRadMessageBox();
                                    trs.translateAllMessageBox("First, you have to delete question from htis group");
                                }
                            }
                            else
                            {
                                translateRadMessageBox trs = new translateRadMessageBox();
                                trs.translateAllMessageBox("You can't delete answer, because some arrangement already filled this answer");
                            }
                        }
                        else
                        {

                            translateRadMessageBox trs = new translateRadMessageBox();
                            trs.translateAllMessageBox("You can't delete answer, because some person already filled this answer");
                        }
                    }
                    else
                    {
                        //translateRadMessageBox trs = new translateRadMessageBox();
                        //trs.translateAllMessageBox("You haven't successfully delete answer because there is no id");
                    }
                }
                else
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("You have to have selected at least one group!");
                }
            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("You have to have at least one group.");
            }
            Cursor.Current = Cursors.Default;
        }


        //private void deleteVoluntaryTrip()
        //{

        //    VolontaryTripBUS mvb = new VolontaryTripBUS();
        //    bool isOneAns = false;
        //    bool isOneQuest = false;
        //    string messageAns = "";
        //    string messageQuest = "";
        //    string messageGroup = "";
        //    if (gridViewVoluntary != null)
        //    {
        //        if (gridViewVoluntaryTrip.SelectedRowMedical != null)
        //        {
        //            translateRadMessageBox tr = new translateRadMessageBox();
        //            if (tr.translateAllMessageBoxDialog("Are you sure that you want to delete this record?", "Delete") == System.Windows.Forms.DialogResult.Yes)
        //            {
        //                Cursor.Current = Cursors.WaitCursor;

        //                AnswerModel am = new AnswerModel();
        //                QuestModel qg = new QuestModel();
        //                if (gridViewVoluntaryTrip.SelectedRowMedical.idAns.ToString() != "")
        //                {
        //                    am.idAns = Convert.ToInt32(gridViewVoluntaryFunc.SelectedRowMedical.idAns);
        //                    messageAns += gridViewVoluntaryTrip.SelectedRowMedical.txtAns.ToString();
        //                }
        //                else
        //                {
        //                    am.idAns = -1;
        //                }
        //                if (gridViewVoluntaryTrip.SelectedRowMedical.idQuest.ToString() != "")
        //                {
        //                    am.idQuest = Convert.ToInt32(gridViewVoluntaryTrip.SelectedRowMedical.idQuest);
        //                    qg.idQuest = Convert.ToInt32(gridViewVoluntaryTrip.SelectedRowMedical.idQuest);
        //                    messageQuest += ", " + gridViewVoluntaryTrip.SelectedRowMedical.txtQuest.ToString() + " quest";
        //                }
        //                else
        //                {
        //                    am.idQuest = -1;
        //                    qg.idQuest = -1;
        //                }
        //                am.idQueryType = Convert.ToInt32(gridViewVoluntaryTrip.SelectedRowMedical.idAnsType);
        //                if (gridViewVoluntaryTrip.SelectedRowMedical.idQuestGroup.ToString() != "")
        //                {
        //                    qg.idQuestGroup = Convert.ToInt32(gridViewVoluntaryTrip.SelectedRowMedical.idQuestGroup);
        //                    messageGroup += " and " + gridViewVoluntaryTrip.SelectedRowMedical.nameQuestGroup + " group";
        //                }
        //                if (mvb.checkIsOneAnsVT(am) <= 1)
        //                {
        //                    isOneAns = true;
        //                }
        //                else
        //                {
        //                    isOneAns = false;
        //                }

        //                if (mvb.checkIsOneQuestVT(qg.idQuestGroup) <= 1)
        //                {
        //                    isOneQuest = true;
        //                }
        //                else
        //                {
        //                    isOneQuest = false;
        //                }

        //                if (mvb.checkAnsIsInVTCpr(am.idAns, Convert.ToInt32(am.idQueryType.ToString())) == 0)
        //                {
        //                    if (mvb.checkAnsIsInVTArr(am.idAns, Convert.ToInt32(am.idQueryType.ToString())) == 0)
        //                    {
        //                        if (isOneQuest == true)
        //                        {
        //                            if (isOneAns == true)
        //                            {
        //                                if (mvb.DeleteAnsSriptVT(am, isOneAns, isOneQuest, qg.idQuestGroup) != true)
        //                                {
        //                                    translateRadMessageBox trs = new translateRadMessageBox();
        //                                    trs.translateAllMessageBox("You haven't successfully delete answer");
        //                                }
        //                                else
        //                                {
        //                                    translateRadMessageBox trs = new translateRadMessageBox();
        //                                    trs.translateAllMessageBox("You have delete successfully: " + messageAns + messageQuest + messageGroup);
        //                                    gridViewVoluntaryTrip.removeRow(gridViewVoluntaryTrip.SelectedRowMedical);
        //                                }
        //                            }
        //                            else
        //                            {
        //                                translateRadMessageBox trs = new translateRadMessageBox();
        //                                trs.translateAllMessageBox("First, you have to delete answers and questins from htis group");
        //                            }
        //                        }
        //                        else
        //                        {
        //                            translateRadMessageBox trs = new translateRadMessageBox();
        //                            trs.translateAllMessageBox("First, you have to delete question from htis group");
        //                        }
        //                    }
        //                    else
        //                    {
        //                        translateRadMessageBox trs = new translateRadMessageBox();
        //                        trs.translateAllMessageBox("You can't delete answer, because some arrangement already filled this answer");
        //                    }
        //                }
        //                else
        //                {

        //                    translateRadMessageBox trs = new translateRadMessageBox();
        //                    trs.translateAllMessageBox("You can't delete answer, because some person already filled this answer");
        //                }
        //            }
        //            else
        //            {
        //                //translateRadMessageBox trs = new translateRadMessageBox();
        //                //trs.translateAllMessageBox("You haven't successfully delete answer because there is no id");
        //            }
        //        }
        //        else
        //        {
        //            translateRadMessageBox trs = new translateRadMessageBox();
        //            trs.translateAllMessageBox("You have to have selected at least one group!");
        //        }
        //    }
        //    else
        //    {
        //        translateRadMessageBox trs = new translateRadMessageBox();
        //        trs.translateAllMessageBox("You have to have at least one group.");
        //    }
        //    Cursor.Current = Cursors.Default;
        //}
        private void deleteVoluntaryTrip()
        {

            VolontaryTripBUS mvb = new VolontaryTripBUS();
            bool isOneAns = false;
            bool isOneQuest = false;
            string messageAns = "";
            string messageQuest = "";
            string messageGroup = "";
            if (gridViewVoluntary != null)
            {
                if (gridViewVoluntaryTrip.SelectedRowMedical != null)
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    if (tr.translateAllMessageBoxDialog("Are you sure that you want to delete this record?", "Delete") == System.Windows.Forms.DialogResult.Yes)
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        AnswerModel am = new AnswerModel();
                        QuestModel qg = new QuestModel();
                        if (gridViewVoluntaryTrip.SelectedRowMedical.idAns.ToString() != "")
                        {
                            am.idAns = Convert.ToInt32(gridViewVoluntaryFunc.SelectedRowMedical.idAns);
                            messageAns += gridViewVoluntaryTrip.SelectedRowMedical.txtAns.ToString();
                        }
                        else
                        {
                            am.idAns = -1;
                        }
                        if (gridViewVoluntaryTrip.SelectedRowMedical.idQuest.ToString() != "")
                        {
                            am.idQuest = Convert.ToInt32(gridViewVoluntaryTrip.SelectedRowMedical.idQuest);
                            qg.idQuest = Convert.ToInt32(gridViewVoluntaryTrip.SelectedRowMedical.idQuest);
                            messageQuest += ", " + gridViewVoluntaryTrip.SelectedRowMedical.txtQuest.ToString() + " quest";
                        }
                        else
                        {
                            am.idQuest = -1;
                            qg.idQuest = -1;
                        }
                        am.idQueryType = Convert.ToInt32(gridViewVoluntaryTrip.SelectedRowMedical.idAnsType);
                        if (gridViewVoluntaryTrip.SelectedRowMedical.idQuestGroup.ToString() != "")
                        {
                            qg.idQuestGroup = Convert.ToInt32(gridViewVoluntaryTrip.SelectedRowMedical.idQuestGroup);
                            messageGroup += " and " + gridViewVoluntaryTrip.SelectedRowMedical.nameQuestGroup + " group";
                        }
                        if (mvb.checkIsOneAnsVT(am) <= 1)
                        {
                            isOneAns = true;
                        }
                        else
                        {
                            isOneAns = false;
                        }

                        if (mvb.checkIsOneQuestVT(qg.idQuestGroup) <= 1)
                        {
                            isOneQuest = true;
                        }
                        else
                        {
                            isOneQuest = false;
                        }

                        if (mvb.checkAnsIsInVTCpr(am.idAns, Convert.ToInt32(am.idQueryType.ToString())) == 0)
                        {
                            if (mvb.checkAnsIsInVTArr(am.idAns, Convert.ToInt32(am.idQueryType.ToString())) == 0)
                            {
                                if (isOneQuest == true)
                                {
                                    if (isOneAns == true)
                                    {
                                        if (mvb.DeleteAnsSriptVT(am, isOneAns, isOneQuest, qg.idQuestGroup, this.Name, Login._user.idUser) != true)
                                        {
                                            translateRadMessageBox trs = new translateRadMessageBox();
                                            trs.translateAllMessageBox("You haven't successfully delete answer");
                                        }
                                        else
                                        {
                                            translateRadMessageBox trs = new translateRadMessageBox();
                                            trs.translateAllMessageBox("You have delete successfully: " + messageAns + messageQuest + messageGroup);
                                            gridViewVoluntaryTrip.removeRow(gridViewVoluntaryTrip.SelectedRowMedical);
                                        }
                                    }
                                    else
                                    {
                                        translateRadMessageBox trs = new translateRadMessageBox();
                                        trs.translateAllMessageBox("First, you have to delete answers and questins from htis group");
                                    }
                                }
                                else
                                {
                                    translateRadMessageBox trs = new translateRadMessageBox();
                                    trs.translateAllMessageBox("First, you have to delete question from htis group");
                                }
                            }
                            else
                            {
                                translateRadMessageBox trs = new translateRadMessageBox();
                                trs.translateAllMessageBox("You can't delete answer, because some arrangement already filled this answer");
                            }
                        }
                        else
                        {

                            translateRadMessageBox trs = new translateRadMessageBox();
                            trs.translateAllMessageBox("You can't delete answer, because some person already filled this answer");
                        }
                    }
                    else
                    {
                        //translateRadMessageBox trs = new translateRadMessageBox();
                        //trs.translateAllMessageBox("You haven't successfully delete answer because there is no id");
                    }
                }
                else
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("You have to have selected at least one group!");
                }
            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("You have to have at least one group.");
            }
            Cursor.Current = Cursors.Default;
        }
        private void deleteVoluntary()
        {

            MedicalVoluntaryBUS mvb = new MedicalVoluntaryBUS();
            bool isOneAns = false;
            bool isOneQuest = false;
            string messageAns = "";
            string messageQuest = "";
            string messageGroup = "";
            if (gridViewVoluntary != null)
            {
                if (gridViewVoluntary.SelectedRowMedical != null)
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    if (tr.translateAllMessageBoxDialog("Are you sure that you want to delete this record?", "Delete") == System.Windows.Forms.DialogResult.Yes)
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        AnswerModel am = new AnswerModel();
                        QuestModel qg = new QuestModel();
                        if (gridViewVoluntary.SelectedRowMedical.idAns.ToString() != "")
                        {
                            am.idAns = Convert.ToInt32(gridViewVoluntary.SelectedRowMedical.idAns);
                            messageAns += gridViewVoluntary.SelectedRowMedical.txtAns.ToString();
                        }
                        else
                        {
                            am.idAns = -1;
                        }
                        if (gridViewVoluntary.SelectedRowMedical.idQuest.ToString() != "")
                        {
                            am.idQuest = Convert.ToInt32(gridViewVoluntary.SelectedRowMedical.idQuest);
                            //   qg.idQuest = Convert.ToInt32(gridViewVoluntary.SelectedRowMedical.idQuest);
                            messageQuest += ", " + gridViewVoluntary.SelectedRowMedical.txtQuest.ToString() + " quest";
                        }
                        else
                        {
                            am.idQuest = -1;
                            qg.idQuest = -1;
                        }
                        am.idQueryType = Convert.ToInt32(gridViewVoluntary.SelectedRowMedical.idAnsType);
                        if (gridViewVoluntary.SelectedRowMedical.idQuestGroup.ToString() != "")
                        {
                            qg.idQuestGroup = Convert.ToInt32(gridViewVoluntary.SelectedRowMedical.idQuestGroup);
                            messageGroup += " and " + gridViewVoluntary.SelectedRowMedical.nameQuestGroup + " group";
                        }
                        if (mvb.checkIsOneAnsVol(am) <= 1)
                        {
                            isOneAns = true;
                        }
                        else
                        {
                            isOneAns = false;
                        }

                        if (mvb.checkIsOneQuestVol(qg.idQuestGroup) <= 1)
                        {
                            isOneQuest = true;
                        }
                        else
                        {
                            isOneQuest = false;
                        }

                        if (mvb.checkAnsIsInVolCpr(am.idAns, Convert.ToInt32(am.idQueryType.ToString())) == 0)
                        {
                            if (mvb.checkAnsIsInVolArr(am.idAns, Convert.ToInt32(am.idQueryType.ToString())) == 0)
                            {
                                if (isOneQuest == true)
                                {
                                    if (isOneAns == true)
                                    {
                                        if (mvb.DeleteAnsSriptVol(am, isOneAns, isOneQuest, qg.idQuestGroup, this.Name, Login._user.idUser) != true)
                                        {
                                            translateRadMessageBox trs = new translateRadMessageBox();
                                            trs.translateAllMessageBox("You haven't successfully delete answer");
                                        }
                                        else
                                        {
                                            translateRadMessageBox trs = new translateRadMessageBox();
                                            trs.translateAllMessageBox("You have delete successfully: " + messageAns + messageQuest + messageGroup);
                                            gridViewVoluntary.removeRow(gridViewVoluntary.SelectedRowMedical);
                                        }
                                    }
                                    else
                                    {
                                        translateRadMessageBox trs = new translateRadMessageBox();
                                        trs.translateAllMessageBox("First, you have to delete answers and questins from htis group");
                                    }
                                }
                                else
                                {
                                    translateRadMessageBox trs = new translateRadMessageBox();
                                    trs.translateAllMessageBox("First, you have to delete question from htis group");
                                }
                            }
                            else
                            {
                                translateRadMessageBox trs = new translateRadMessageBox();
                                trs.translateAllMessageBox("You can't delete answer, because some arrangement already filled this answer");
                            }
                        }
                        else
                        {

                            translateRadMessageBox trs = new translateRadMessageBox();
                            trs.translateAllMessageBox("You can't delete answer, because some person already filled this answer");
                        }
                    }
                    else
                    {
                        //translateRadMessageBox trs = new translateRadMessageBox();
                        //trs.translateAllMessageBox("You haven't successfully delete answer because there is no id");
                    }
                }
                else
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("You have to have selected at least one group!");
                }
            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("You have to have at least one group.");
            }
            Cursor.Current = Cursors.Default;
        }

        private void deleteBookmark()
        {
            LayoutsBUS lay = new LayoutsBUS();
            LayoutsModel lm = new LayoutsModel();

            if (gridViewLayouts != null)
            {
                if (gridViewLayouts.SelectedRowLayout != null)
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    if (tr.translateAllMessageBoxDialog("Are you SHURE that you want to delete this layout?", "Delete") == System.Windows.Forms.DialogResult.Yes)
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        if (gridViewLayouts.SelectedRowLayout.idLayout.ToString() != "")
                        {
                            lm.idLayout = Convert.ToInt32(gridViewLayouts.SelectedRowLayout.idLayout);
                        }
                        else
                        {
                            lm.idLayout = -1;
                        }
                        if (gridViewLayouts.SelectedRowLayout.nameLayout.ToString() != "")
                        {
                            lm.nameLayout = gridViewLayouts.SelectedRowLayout.nameLayout;
                        }
                        else
                        {
                            lm.nameLayout = "";
                        }
                        if (lay.DeleteLayoutByID(lm.idLayout, this.Name, Login._user.idUser) != true)
                        {
                            translateRadMessageBox trs = new translateRadMessageBox();
                            trs.translateAllMessageBox("You haven't delete layout successfully!");
                        }
                        else
                        {
                            translateRadMessageBox trs = new translateRadMessageBox();
                            trs.translateAllMessageBox("You have delete layout successfully!");
                            gridViewLayouts.removeRow(gridViewLayouts.SelectedRowLayout);
                        }
                    }
                }
            }
            Cursor.Current = Cursors.Default;
        }

        //===========================
        private void deleteUser()
        {
            UsersAllBUS pb = new UsersAllBUS();
            CanDeleteBUS cd = new CanDeleteBUS();
            if (gridViewUsers != null)
            {
                if (gridViewUsers.SelectedRowUser != null)
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    if (tr.translateAllMessageBoxDialog("Are you sure that you want to delete this User?", "Delete") == System.Windows.Forms.DialogResult.Yes)
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        if (cd.canDelete("ArrangementBook", "idUserCreated", gridViewUsers.SelectedRowUser.idUser.ToString()) == false &&
                            cd.canDelete("ArrangementBook", "idUserModified", gridViewUsers.SelectedRowUser.idUser.ToString()) == false)
                        {
                            if (cd.canDelete("ArrangementBookArticles", "idUserCreated", gridViewUsers.SelectedRowUser.idUser.ToString()) == false &&
                             cd.canDelete("ArrangementBookArticles", "idUserModified", gridViewUsers.SelectedRowUser.idUser.ToString()) == false)
                            {
                                if (cd.canDelete("ArrangementBookPersons", "idUserCreated", gridViewUsers.SelectedRowUser.idUser.ToString()) == false &&
                                cd.canDelete("ArrangementBookPersons", "idUserModified", gridViewUsers.SelectedRowUser.idUser.ToString()) == false)
                                {
                                    if (cd.canDelete("ArrangementCalculationFirstArticles", "idUserCreated", gridViewUsers.SelectedRowUser.idUser.ToString()) == false &&
                                    cd.canDelete("ArrangementCalculationFirstArticles", "idUserModified", gridViewUsers.SelectedRowUser.idUser.ToString()) == false)
                                    {
                                        if (cd.canDelete("ArrangementPrice", "idUserCreated", gridViewUsers.SelectedRowUser.idUser.ToString()) == false &&
                                         cd.canDelete("ArrangementPrice", "idUserModified", gridViewUsers.SelectedRowUser.idUser.ToString()) == false)
                                        {
                                            if (cd.canDelete("Artical", "idUserCreated", gridViewUsers.SelectedRowUser.idUser.ToString()) == false &&
                                            cd.canDelete("Artical", "idUserModifies", gridViewUsers.SelectedRowUser.idUser.ToString()) == false)
                                            {
                                                if (cd.canDelete("ArticalGroups", "idUserCreated", gridViewUsers.SelectedRowUser.idUser.ToString()) == false &&
                                                 cd.canDelete("ArticalGroups", "idUserModified", gridViewUsers.SelectedRowUser.idUser.ToString()) == false)
                                                {
                                                    if (cd.canDelete("BookmarkDef", "userCreated", gridViewUsers.SelectedRowUser.idUser.ToString()) == false &&
                                                    cd.canDelete("BookmarkDef", "userModified", gridViewUsers.SelectedRowUser.idUser.ToString()) == false)
                                                    {
                                                        if (cd.canDelete("Client", "userCreated", gridViewUsers.SelectedRowUser.idUser.ToString()) == false &&
                                                        cd.canDelete("Client", "userModified", gridViewUsers.SelectedRowUser.idUser.ToString()) == false)
                                                        {
                                                            if (cd.canDelete("ClientNotes", "idUserCreated", gridViewUsers.SelectedRowUser.idUser.ToString()) == false &&
                                                                cd.canDelete("ClientNotes", "idUserModified", gridViewUsers.SelectedRowUser.idUser.ToString()) == false)
                                                            {
                                                                if (cd.canDelete("ContactPerson", "idUserCreated", gridViewUsers.SelectedRowUser.idUser.ToString()) == false &&
                                                                    cd.canDelete("ContactPerson", "idUserModified", gridViewUsers.SelectedRowUser.idUser.ToString()) == false &&
                                                                    cd.canDelete("ContactPerson", "idUserResponsible", gridViewUsers.SelectedRowUser.idUser.ToString()) == false)
                                                                {
                                                                    if (cd.canDelete("ContactPersonNotes", "idUserCreated", gridViewUsers.SelectedRowUser.idUser.ToString()) == false &&
                                                                    cd.canDelete("ContactPersonNotes", "idUserModified", gridViewUsers.SelectedRowUser.idUser.ToString()) == false)
                                                                    {
                                                                        if (cd.canDelete("Country", "userCreated", gridViewUsers.SelectedRowUser.idUser.ToString()) == false &&
                                                                         cd.canDelete("Country", "userModified", gridViewUsers.SelectedRowUser.idUser.ToString()) == false)
                                                                        {
                                                                            if (cd.canDelete("Documents", "userCreated", gridViewUsers.SelectedRowUser.idUser.ToString()) == false &&
                                                                            cd.canDelete("Documents", "userModified", gridViewUsers.SelectedRowUser.idUser.ToString()) == false)
                                                                            {
                                                                                if (cd.canDelete("DocumentType", "idCreatedUser", gridViewUsers.SelectedRowUser.idUser.ToString()) == false &&
                                                                                 cd.canDelete("DocumentType", "idModifiedUser", gridViewUsers.SelectedRowUser.idUser.ToString()) == false)
                                                                                {
                                                                                    if (cd.canDelete("Invoice", "userCreated", gridViewUsers.SelectedRowUser.idUser.ToString()) == false &&
                                                                                    cd.canDelete("Invoice", "userModified", gridViewUsers.SelectedRowUser.idUser.ToString()) == false)
                                                                                    {
                                                                                        if (cd.canDelete("InvoiceItems", "userCreated", gridViewUsers.SelectedRowUser.idUser.ToString()) == false &&
                                                                                         cd.canDelete("InvoiceItems", "userModified", gridViewUsers.SelectedRowUser.idUser.ToString()) == false)
                                                                                        {
                                                                                            if (cd.canDelete("Layouts", "userCreated", gridViewUsers.SelectedRowUser.idUser.ToString()) == false &&
                                                                                            cd.canDelete("Layouts", "userModified", gridViewUsers.SelectedRowUser.idUser.ToString()) == false)
                                                                                            {
                                                                                                if (cd.canDelete("Multimedia", "idUserCreated", gridViewUsers.SelectedRowUser.idUser.ToString()) == false &&
                                                                                                 cd.canDelete("Multimedia", "idUserModified", gridViewUsers.SelectedRowUser.idUser.ToString()) == false)
                                                                                                {
                                                                                                    if (cd.canDelete("Photos", "idUserCreator", gridViewUsers.SelectedRowUser.idUser.ToString()) == false &&
                                                                                                   cd.canDelete("Photos", "idUserModified", gridViewUsers.SelectedRowUser.idUser.ToString()) == false)
                                                                                                    {
                                                                                                        if (cd.canDelete("PriceList", "idUserCreated", gridViewUsers.SelectedRowUser.idUser.ToString()) == false &&
                                                                                                          cd.canDelete("PriceList", "idUserModified", gridViewUsers.SelectedRowUser.idUser.ToString()) == false)
                                                                                                        {
                                                                                                            if (cd.canDelete("PriceListArticles", "idUserCreated", gridViewUsers.SelectedRowUser.idUser.ToString()) == false &&
                                                                                                           cd.canDelete("PriceListArticles", "idUserModified", gridViewUsers.SelectedRowUser.idUser.ToString()) == false)
                                                                                                            {

                                                                                                                if (pb.DeleteUserSript(gridViewUsers.SelectedRowUser.idUser) == true)
                                                                                                                {
                                                                                                                    gridViewUsers.removeRow(gridViewUsers.SelectedRowUser);
                                                                                                                    translateRadMessageBox trs = new translateRadMessageBox();
                                                                                                                    trs.translateAllMessageBox("You have delete successfully!");

                                                                                                                }
                                                                                                                else
                                                                                                                {
                                                                                                                    translateRadMessageBox trs = new translateRadMessageBox();
                                                                                                                    trs.translateAllMessageBox("Something went wrong. Please contact your administrator!");
                                                                                                                }
                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                                translateRadMessageBox trs = new translateRadMessageBox();
                                                                                                                trs.translateAllMessageBox("You have not to delete user because has been created or modified contract of articles!");
                                                                                                            }
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            translateRadMessageBox trs = new translateRadMessageBox();
                                                                                                            trs.translateAllMessageBox("You have not to delete user because he has been created or modified contract!");
                                                                                                        }
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        translateRadMessageBox trs = new translateRadMessageBox();
                                                                                                        trs.translateAllMessageBox("You have not to delete user because he has been created or modified photos!");
                                                                                                    }
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    translateRadMessageBox trs = new translateRadMessageBox();
                                                                                                    trs.translateAllMessageBox("You have not to delete user becausehe has been created or modified multimedia!");
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                translateRadMessageBox trs = new translateRadMessageBox();
                                                                                                trs.translateAllMessageBox("You have not to delete user because he has been created or modified layouts!");
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            translateRadMessageBox trs = new translateRadMessageBox();
                                                                                            trs.translateAllMessageBox("You have not to delete user because he has been created or modified invoice item!");
                                                                                        }
                                                                                    }
                                                                                    else
                                                                                    {

                                                                                        translateRadMessageBox trs = new translateRadMessageBox();
                                                                                        trs.translateAllMessageBox("You have not to delete user because he has been created or modified invoice!");
                                                                                    }
                                                                                }
                                                                                else
                                                                                {
                                                                                    translateRadMessageBox trs = new translateRadMessageBox();
                                                                                    trs.translateAllMessageBox("You have not to delete user because he has been created or modified type of document!");
                                                                                }
                                                                            }

                                                                            else
                                                                            {

                                                                                translateRadMessageBox trs = new translateRadMessageBox();
                                                                                trs.translateAllMessageBox("You have not to delete user because he has been created or modified documents!");
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            translateRadMessageBox trs = new translateRadMessageBox();
                                                                            trs.translateAllMessageBox("You have not to delete user because he has been created or modified country!");
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        translateRadMessageBox trs = new translateRadMessageBox();
                                                                        trs.translateAllMessageBox("You have not to delete user because he has been created or modified person's notes!");
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    translateRadMessageBox trs = new translateRadMessageBox();
                                                                    trs.translateAllMessageBox("You have not to delete user because he has been created or modified person!");
                                                                }
                                                            }
                                                            else
                                                            {
                                                                translateRadMessageBox trs = new translateRadMessageBox();
                                                                trs.translateAllMessageBox("You have not to delete user because he has been created or modified client's notes!");
                                                            }
                                                        }

                                                        else
                                                        {
                                                            translateRadMessageBox trs = new translateRadMessageBox();
                                                            trs.translateAllMessageBox("You have not to delete user because he has been created or modified client!");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        translateRadMessageBox trs = new translateRadMessageBox();
                                                        trs.translateAllMessageBox("You have not to delete user because he has been created or modified bookmark!");
                                                    }
                                                }
                                                else
                                                {
                                                    translateRadMessageBox trs = new translateRadMessageBox();
                                                    trs.translateAllMessageBox("You have not to delete user because he has been created or modified group of articles!");
                                                }
                                            }
                                            else
                                            {
                                                translateRadMessageBox trs = new translateRadMessageBox();
                                                trs.translateAllMessageBox("You have not to delete user because he has been created or modified article!");
                                            }
                                        }
                                        else
                                        {
                                            translateRadMessageBox trs = new translateRadMessageBox();
                                            trs.translateAllMessageBox("You have not to delete user because he has been created or modified articles in arrangement without contracts!");
                                        }
                                    }
                                    else
                                    {
                                        translateRadMessageBox trs = new translateRadMessageBox();
                                        trs.translateAllMessageBox("You have not to delete user because he has been created or modified first calculation!");
                                    }
                                }
                                else
                                {
                                    translateRadMessageBox trs = new translateRadMessageBox();
                                    trs.translateAllMessageBox("You have not to delete user because he has been created or modified person in arrangement book!");
                                }
                            }
                            else
                            {
                                translateRadMessageBox trs = new translateRadMessageBox();
                                trs.translateAllMessageBox("You have not to delete user because he has been created or modified arrangement book article!");
                            }
                        }
                        else
                        {
                            translateRadMessageBox trs = new translateRadMessageBox();
                            trs.translateAllMessageBox("You have not to delete user because he has been created or modified person in arrangement book!");
                        }
                    }
                }
                else
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("You have to have selected at least one user!");
                }
            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("You have to have at least one user!");
            }
            Cursor.Current = Cursors.Default;
        }

        private void deleteTravelData()
        {
            string tip = gridTravelData.SelectedRowType.type;
            int tdid = Convert.ToInt32(gridTravelData.SelectedRowType.ID); // za selektovani red njegov ID

            string ct = gridTravelData.SelectedRowType.code; // code iz reda koji je oznacen

            List<TranslateUstrModel> translate;
            UsersBUS ubus = new UsersBUS();
            string prevodioc = "";
            bool contains = true;
            TravelDataBUS _trBUS = new TravelDataBUS();

            List<LastIdModel> lModel = new List<LastIdModel>();
            List<CodeTrainingFromVolFeaturesModel> CFModel = new List<CodeTrainingFromVolFeaturesModel>();

            translate = ubus.Translate("Arrangement type", Login._user.lngUser);
            if (translate != null)
            {
                if (translate.Count > 0)
                {
                    prevodioc = translate[0].stringValue;
                    prevodioc = translate[0].stringValue;
                    if (tip == prevodioc)
                    {

                        translateRadMessageBox tr = new translateRadMessageBox();
                        if (tr.translateAllMessageBoxDialog("Are you sure that you want to delete Arrangement?", "Arrangement type") == System.Windows.Forms.DialogResult.Yes)
                        {
                            if (_trBUS.DeleteArrType(tdid, this.Name, Login._user.idUser) == true)
                            {
                                translateRadMessageBox trs = new translateRadMessageBox();
                                trs.translateAllMessageBox("You deleted Arrangement type successfully!");
                            }
                        }
                        else
                        {
                            return;
                            translateRadMessageBox trs = new translateRadMessageBox();
                            trs.translateAllMessageBox("Someting went wrong with deleting!");
                        }

                    }
                }
                //===================Arrangement status DELETE===========================================================================
                translate = ubus.Translate("Arrangement status", Login._user.lngUser);
                if (translate != null)
                {
                    if (translate.Count > 0)
                    {
                        prevodioc = translate[0].stringValue;
                        prevodioc = translate[0].stringValue;
                        if (tip == prevodioc)
                        {

                            translateRadMessageBox tr = new translateRadMessageBox();
                            if (tr.translateAllMessageBoxDialog("Are you sure that you want to delete Arrangement status?", "Arrangement status") == System.Windows.Forms.DialogResult.Yes)
                            {
                                if (_trBUS.DeleteArrangementStatus(tdid, this.Name, Login._user.idUser) == true)
                                {
                                    translateRadMessageBox trs = new translateRadMessageBox();
                                    trs.translateAllMessageBox("You deleted Arrangement status successfully!");
                                }
                            }
                            else
                            {
                                return;
                                translateRadMessageBox trs = new translateRadMessageBox();
                                trs.translateAllMessageBox("Someting went wrong with deleting!");
                            }

                        }
                    }
                }
                //===================Arrangement status DELETE===========================================================================
                translate = ubus.Translate("Training type", Login._user.lngUser);

                if (translate != null)
                {
                    if (translate.Count > 0)
                    {
                        prevodioc = translate[0].stringValue;
                        if (tip == prevodioc)
                        {
                            CFModel = _trBUS.isInTrainingInVolFeatures(ct);
                            for (int i = 0; i < CFModel.Count; i++)
                            {
                                if (CFModel[i].code == ct.ToString()) // ako je jednak kodu iz tabele ne moze da brise 
                                {
                                    contains = true;
                                    break;
                                }
                                else contains = false;
                            }
                            if (CFModel.Count == 0) // ispituje ako ne vrati ni jedan red upit 
                            {
                                contains = false;
                            }

                            translateRadMessageBox tr = new translateRadMessageBox();
                            if (tr.translateAllMessageBoxDialog("Are you sure delete this Training type?", "Training type") == System.Windows.Forms.DialogResult.Yes)
                            {
                                if (contains == false)
                                {
                                    if (_trBUS.DeleteTraining(tdid, this.Name, Login._user.idUser) == true)
                                    {
                                        translateRadMessageBox trs = new translateRadMessageBox();
                                        trs.translateAllMessageBox("You deleted Training type successfully!");
                                    }
                                }
                                else
                                {
                                    translateRadMessageBox trs = new translateRadMessageBox();
                                    trs.translateAllMessageBox("This type cannot be deleted!");
                                }
                            }
                        }
                    }
                }

                translate = ubus.Translate("Certificates type", Login._user.lngUser);
                if (translate != null)
                {
                    if (translate.Count > 0)
                    {
                        prevodioc = translate[0].stringValue;
                        if (tip == prevodioc)
                        {
                            CFModel = _trBUS.isInCertificatesInVolFeatures(ct);
                            for (int i = 0; i < CFModel.Count; i++)
                            {
                                if (CFModel[i].code == ct.ToString()) // ako je jednak kodu iz tabele ne moze da brise 
                                {
                                    contains = true;
                                    break;
                                }
                                else
                                    contains = false;
                            }
                            if (CFModel.Count == 0) // ispituje ako ne vrati ni jedan red upit
                            {
                                contains = false;
                            }

                            translateRadMessageBox tr = new translateRadMessageBox();
                            if (tr.translateAllMessageBoxDialog("Are you sure delete this Certificate type?", "Certificate type") == System.Windows.Forms.DialogResult.Yes)
                            {
                                if (contains == false)
                                {
                                    if (_trBUS.DeleteCertificates(tdid, this.Name, Login._user.idUser) == true)
                                    {
                                        translateRadMessageBox trs = new translateRadMessageBox();
                                        trs.translateAllMessageBox("You deleted Certificates type successfully!");
                                    }
                                }
                                else
                                {
                                    translateRadMessageBox trs = new translateRadMessageBox();
                                    trs.translateAllMessageBox("This type cannot be deleted!");
                                }
                            }
                        }
                    }
                }
            }
            modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser); // refresh grida
            activeGrid.SetDataPersonBinding(modelData); // refresh grida
        }

        private void deletefrmArrangementInsurance()
        {
            ArrangementInsuranceBUS pb = new ArrangementInsuranceBUS();
            if (gridviewArrangementInsurance != null)
            {
                if (gridviewArrangementInsurance.SelectedRowArrangementInsurance != null)
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    if (tr.translateAllMessageBoxDialog("Are you sure that you want to delete this insurance?", "Delete") == System.Windows.Forms.DialogResult.Yes)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        if (pb.checkIsInArrangemnet(gridviewArrangementInsurance.SelectedRowArrangementInsurance.labelInsurance, gridviewArrangementInsurance.SelectedRowArrangementInsurance.codeInsurance) <= 0)
                        {


                            if (pb.DeleteArrangementInsuranceSript(gridviewArrangementInsurance.SelectedRowArrangementInsurance.idInsurance, this.Name, Login._user.idUser) == true)
                            {
                                gridviewArrangementInsurance.removeRow(gridviewArrangementInsurance.SelectedRowArrangementInsurance);
                                //if (activeGrid != null)
                                //{
                                //    modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
                                //    activeGrid.SetDataPersonBinding(modelData);
                                //} 
                                translateRadMessageBox trs = new translateRadMessageBox();
                                trs.translateAllMessageBox("You have delete successfully!");
                            }
                            else
                            {
                                translateRadMessageBox trs = new translateRadMessageBox();
                                trs.translateAllMessageBox("Something went wrong. Please contact your administrator!");
                            }

                        }

                        else
                        {
                            translateRadMessageBox trs = new translateRadMessageBox();
                            trs.translateAllMessageBox("You haven't delete because this some of Arrangement use this insurance!");
                        }
                    }

                }
                else
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("You have to have selected at least one insurance!");
                }
            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("You have to have at least one insurance!");
            }
            Cursor.Current = Cursors.Default;

        }

        private void deletefrmArrangementInsurancePremie()
        {
            ArrangementInsurancePremieBUS pb = new ArrangementInsurancePremieBUS();
            if (gridviewArrangementInsurancePremie != null)
            {
                if (gridviewArrangementInsurancePremie.SelectedRowArrangementInsurancePremie != null)
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    if (tr.translateAllMessageBoxDialog("Are you sure that you want to delete this insurance?", "Delete") == System.Windows.Forms.DialogResult.Yes)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        if (pb.checkIsInArrangemnet(gridviewArrangementInsurancePremie.SelectedRowArrangementInsurancePremie.codeInsurance) <= 0)
                        {


                            if (pb.DeleteArrangementInsuranceSript(gridviewArrangementInsurancePremie.SelectedRowArrangementInsurancePremie.idPremie, this.Name, Login._user.idUser) == true)
                            {
                                gridviewArrangementInsurancePremie.removeRow(gridviewArrangementInsurancePremie.SelectedRowArrangementInsurancePremie);
                                //if (activeGrid != null)
                                //{
                                //    modelData = activeGrid.GetData(Convert.ToInt32(selectedFilter), idLabelList, Login._user.lngUser);
                                //    activeGrid.SetDataPersonBinding(modelData);
                                //} 
                                translateRadMessageBox trs = new translateRadMessageBox();
                                trs.translateAllMessageBox("You have delete successfully!");
                            }
                            else
                            {
                                translateRadMessageBox trs = new translateRadMessageBox();
                                trs.translateAllMessageBox("Something went wrong. Please contact your administrator!");
                            }

                        }

                        else
                        {
                            translateRadMessageBox trs = new translateRadMessageBox();
                            trs.translateAllMessageBox("You haven't delete because this some of arrangement use this insurance!");
                        }
                    }

                }
                else
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("You have to have selected at least one insurance!");
                }
            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("You have to have at least one insurance!");
            }
            Cursor.Current = Cursors.Default;
        }
        
        #endregion

        #region delete frmType
        private void acc()
        {
            string tip = gridViewType.SelectedRowType.type;
            int a = Convert.ToInt32(gridViewType.SelectedRowType.ID);
            UsersBUS ub = new UsersBUS();
            bool contain = true;
            TypeBUS _tlBUS = new TypeBUS();
            List<LastIdModel> lModel = new List<LastIdModel>();

            lModel = _tlBUS.isInAccDailyType();
            for (int i = 0; i < lModel.Count; i++)
            {
                if (lModel[i].ID == a)
                {
                    contain = true;
                    break;

                }
                else contain = false;

            }
            if (lModel.Count == 0)
            {
                contain = false;
            }

            if (contain == false)
            {
                if (_tlBUS.DeleteAccDailyType(a, this.Name, Login._user.idUser) == true)
                {

                    RadMessageBox.Show("You have successfully delete acc daily type!");
                }
                else
                {
                    RadMessageBox.Show("Something went wrong. Please contact your administrator!");
                }
            }
            else
            {
                RadMessageBox.Show("This type cannot be deleted");
            }

        }
        private void address()
    {
        string tip = gridViewType.SelectedRowType.type;
        int a = Convert.ToInt32(gridViewType.SelectedRowType.ID);
        UsersBUS ub = new UsersBUS();
        bool contain = true;
        TypeBUS _tlBUS = new TypeBUS();
        List<LastIdModel> lModel = new List<LastIdModel>();

        lModel = _tlBUS.isInAddress();
        for (int i = 0; i < lModel.Count; i++)
        {
            if (lModel[i].ID == a)
            {
                contain = true;
                break;

            }
            else contain = false;

        }
        if (lModel.Count == 0)
        {
            contain = false;
        }
        if (contain == false)
        {
            if (_tlBUS.DeleteTypesAddress(a, this.Name, Login._user.idUser) == true)
            {

                RadMessageBox.Show("You have successfully delete address type!");
            }
            else
            {
                RadMessageBox.Show("Something went wrong. Please contact your administrator!");
            }
        }
        else
        {
            RadMessageBox.Show("This type cannot be deleted");
        }
    }
        private void client()
        {
            string tip = gridViewType.SelectedRowType.type;
            int a = Convert.ToInt32(gridViewType.SelectedRowType.ID);
            UsersBUS ub = new UsersBUS();
            bool contain = true;
            TypeBUS _tlBUS = new TypeBUS();
            List<LastIdModel> lModel = new List<LastIdModel>();
            lModel = _tlBUS.isInClient();
            for (int i = 0; i < lModel.Count; i++)
            {
                if (lModel[i].ID == a)
                {
                    contain = true;
                    break;

                }
                else contain = false;

            }
            if (lModel.Count == 0)
            {
                contain = false;
            }
            if (contain == false)
            {
                if (_tlBUS.DeleteClientTypes(a, this.Name, Login._user.idUser) == true)
                {

                    RadMessageBox.Show("You have successfully delete client type!");
                }
                else
                {
                    RadMessageBox.Show("Something went wrong. Please contact your administrator!");
                }
            }
            else
            {
                RadMessageBox.Show("This type cannot be deleted");
            }
        }

        private void contactType()
        {
            string tip = gridViewType.SelectedRowType.type;
            int a = Convert.ToInt32(gridViewType.SelectedRowType.ID);
            UsersBUS ub = new UsersBUS();
            bool contain = true;
            TypeBUS _tlBUS = new TypeBUS();
            List<LastIdModel> lModel = new List<LastIdModel>();
            lModel = _tlBUS.isInContact();
            for (int i = 0; i < lModel.Count; i++)
            {
                if (lModel[i].ID == a)
                {
                    contain = true;
                    break;

                }
                else contain = false;

            }
            if (lModel.Count == 0)
            {
                contain = false;
            }
            if (contain == false)
            {

                if (_tlBUS.DeleteContactType(a, this.Name, Login._user.idUser) == true)
                {

                    RadMessageBox.Show("You have successfully delete contact type!");
                }
                else
                {
                    RadMessageBox.Show("Something went wrong. Please contact your administrator!");
                }
            }
            else
            {
                RadMessageBox.Show("This type cannot be deleted");
            }
        }
        private void emailType()
        {
            string tip = gridViewType.SelectedRowType.type;
            int a = Convert.ToInt32(gridViewType.SelectedRowType.ID);
            UsersBUS ub = new UsersBUS();
            bool contain = true;
            TypeBUS _tlBUS = new TypeBUS();
            List<LastIdModel> lModel = new List<LastIdModel>();
            lModel = _tlBUS.isInEmail();
            for (int i = 0; i < lModel.Count; i++)
            {
                if (lModel[i].ID == a)
                {
                    contain = true;
                    break;

                }
                else contain = false;

            }
            if (lModel.Count == 0)
            {
                contain = false;
            }
            if (contain == false)
            {
                if (_tlBUS.DeleteTypesEmail(a, this.Name, Login._user.idUser) == true)
                {

                    RadMessageBox.Show("You have successfully delete email type!");
                }
                else
                {
                    RadMessageBox.Show("Something went wrong. Please contact your administrator!");
                }
            }
            else
            {
                RadMessageBox.Show("This type cannot be deleted");
            }
        }
        private void medicalAnswerType()
        {
            string tip = gridViewType.SelectedRowType.type;
            int a = Convert.ToInt32(gridViewType.SelectedRowType.ID);
            UsersBUS ub = new UsersBUS();
            bool contain = true;
            TypeBUS _tlBUS = new TypeBUS();
            List<LastIdModel> lModel = new List<LastIdModel>();
            lModel = _tlBUS.isInMedAnsType();
            for (int i = 0; i < lModel.Count; i++)
            {
                if (lModel[i].ID == a)
                {
                    contain = true;
                    break;

                }
                else contain = false;

            }
            if (lModel.Count == 0)
            {
                contain = false;
            }
            if (contain == false)
            {
                if (_tlBUS.DeleteMedAnsType(a, this.Name, Login._user.idUser) == true)
                {

                    RadMessageBox.Show("You have successfully delete medical answer type!");
                }
                else
                {
                    RadMessageBox.Show("Something went wrong. Please contact your administrator!");
                }
            }
            else
            {
                RadMessageBox.Show("This type cannot be deleted");
            }
        }
        private void noteType()
        {
            string tip = gridViewType.SelectedRowType.type;
            int a = Convert.ToInt32(gridViewType.SelectedRowType.ID);
            UsersBUS ub = new UsersBUS();
            bool contain = true;
            TypeBUS _tlBUS = new TypeBUS();
            List<LastIdModel> lModel = new List<LastIdModel>();
            lModel = _tlBUS.isInNote();
            for (int i = 0; i < lModel.Count; i++)
            {
                if (lModel[i].ID == a)
                {
                    contain = true;
                    break;

                }
                else contain = false;

            }
            if (lModel.Count == 0)
            {
                contain = false;
            }
            if (contain == false)
            {
                if (_tlBUS.DeleteTypesNote(a, this.Name, Login._user.idUser) == true)
                {

                    RadMessageBox.Show("You have successfully delete note type!");
                }
                else
                {
                    RadMessageBox.Show("Something went wrong. Please contact your administrator!");
                }
            }
            else
            {
                RadMessageBox.Show("This type cannot be deleted");
            }
        }
        private void telephoneType()
        {
            string tip = gridViewType.SelectedRowType.type;
            int a = Convert.ToInt32(gridViewType.SelectedRowType.ID);
            UsersBUS ub = new UsersBUS();
            bool contain = true;
            TypeBUS _tlBUS = new TypeBUS();
            List<LastIdModel> lModel = new List<LastIdModel>();
            lModel = _tlBUS.isInTel();
            for (int i = 0; i < lModel.Count; i++)
            {
                if (lModel[i].ID == a)
                {
                    contain = true;
                    break;

                }
                else contain = false;

            }
            if (lModel.Count == 0)
            {
                contain = false;
            }
            if (contain == false)
            {
                if (_tlBUS.DeleteTypesTel(a, this.Name, Login._user.idUser) == true)
                {

                    RadMessageBox.Show("You have successfully delete telephone type!");
                }
                else
                {
                    RadMessageBox.Show("Something went wrong. Please contact your administrator!");
                }
            }
            else
            {
                RadMessageBox.Show("This type cannot be deleted");
            }
        }
        private void toDoType()
        {
            string tip = gridViewType.SelectedRowType.type;
            int a = Convert.ToInt32(gridViewType.SelectedRowType.ID);
            UsersBUS ub = new UsersBUS();
            bool contain = true;
            TypeBUS _tlBUS = new TypeBUS();
            List<LastIdModel> lModel = new List<LastIdModel>();
            lModel = _tlBUS.isInToDoType();
            for (int i = 0; i < lModel.Count; i++)
            {
                if (lModel[i].ID == a)
                {
                    contain = true;
                    break;

                }
                else contain = false;

            }
            if (lModel.Count == 0)
            {
                contain = false;
            }
            if (contain == false)
            {
                if (_tlBUS.DeleteToDoType(a, this.Name, Login._user.idUser) == true)
                {

                    RadMessageBox.Show("You have successfully delete to do type!");
                }
                else
                {
                    RadMessageBox.Show("Something went wrong. Please contact your administrator!");
                }
            }
            else
            {
                RadMessageBox.Show("This type cannot be deleted");
            }
        }
        private void volontaryAnswerType()
        {
            string tip = gridViewType.SelectedRowType.type;
            int a = Convert.ToInt32(gridViewType.SelectedRowType.ID);
            UsersBUS ub = new UsersBUS();
            bool contain = true;
            TypeBUS _tlBUS = new TypeBUS();
            List<LastIdModel> lModel = new List<LastIdModel>();
            lModel = _tlBUS.isInVolAnsType();
            for (int i = 0; i < lModel.Count; i++)
            {
                if (lModel[i].ID == a)
                {
                    contain = true;
                    break;

                }
                else contain = false;

            }
            if (lModel.Count == 0)
            {
                contain = false;
            }
            if (contain == false)
            {
                if (_tlBUS.DeleteVolAnsType(a, this.Name, Login._user.idUser) == true)
                {

                    RadMessageBox.Show("You have successfully delete volontary answer type!");
                }
                else
                {
                    RadMessageBox.Show("Something went wrong. Please contact your administrator!");
                }
            }
            else
            {
                RadMessageBox.Show("This type cannot be deleted");
            }
        }
        private void meetingCategory()
        {
            string tip = gridViewType.SelectedRowType.type;
            int a = Convert.ToInt32(gridViewType.SelectedRowType.ID);
            UsersBUS ub = new UsersBUS();
            bool contain = true;
            TypeBUS _tlBUS = new TypeBUS();
            List<LastIdModel> lModel = new List<LastIdModel>();
            lModel = _tlBUS.isInMeetingsCategory();
            for (int i = 0; i < lModel.Count; i++)
            {
                if (lModel[i].ID == a)
                {
                    contain = true;
                    break;

                }
                else contain = false;

            }
            if (lModel.Count == 0)
            {
                contain = false;
            }
            if (contain == false)
            {
                if (_tlBUS.DeleteMeetingsCategory(a, this.Name, Login._user.idUser) == true)
                {

                    RadMessageBox.Show("You have successfully delete meeting category!");
                }
                else
                {
                    RadMessageBox.Show("Something went wrong. Please contact your administrator!");
                }
            }
            else
            {
                RadMessageBox.Show("This type cannot be deleted");
            }
        }
        private void meetingPriority()
    {
        string tip = gridViewType.SelectedRowType.type;
        int a = Convert.ToInt32(gridViewType.SelectedRowType.ID);
        UsersBUS ub = new UsersBUS();
        bool contain = true;
        TypeBUS _tlBUS = new TypeBUS();
        List<LastIdModel> lModel = new List<LastIdModel>();
        lModel = _tlBUS.isInMeetingsPriority();
                        
            for (int i = 0; i < lModel.Count; i++)                       
            {
                if (lModel[i].ID == a)
                {
                    contain = true;
                    break;

                }
                else contain = false;

            }
            if (lModel.Count == 0)
            {
                contain = false;
            }
            if (contain == false)
            {
                if (_tlBUS.DeleteMeetingsPriority(a, this.Name, Login._user.idUser) == true)
                {

                    RadMessageBox.Show("You have successfully delete meeting priority!");
                }
                else
                {
                    RadMessageBox.Show("Something went wrong. Please contact your administrator!");
                }
            }
            else
            {
                RadMessageBox.Show("This type cannot be deleted");
            }
                           
    }
        private void toDoPriority()
        {
            string tip = gridViewType.SelectedRowType.type;
            int a = Convert.ToInt32(gridViewType.SelectedRowType.ID);
            UsersBUS ub = new UsersBUS();
            bool contain = true;
            TypeBUS _tlBUS = new TypeBUS();
            List<LastIdModel> lModel = new List<LastIdModel>();
            lModel = _tlBUS.isInToDoPriority();
            for (int i = 0; i < lModel.Count; i++)
            {
                if (lModel[i].ID == a)
                {
                    contain = true;
                    break;

                }
                else contain = false;

            }
            if (lModel.Count == 0)
            {
                contain = false;
            }
            if (contain == false)
            {
                if (_tlBUS.DeleteToDoPriority(a, this.Name, Login._user.idUser) == true)
                {

                    RadMessageBox.Show("You have successfully delete to do priority!");
                }
                else
                {
                    RadMessageBox.Show("Something went wrong. Please contact your administrator!");
                }
            }
            else
            {
                RadMessageBox.Show("This type cannot be deleted");
            }
        }
        private void title()
        {
            string tip = gridViewType.SelectedRowType.type;
            int a = Convert.ToInt32(gridViewType.SelectedRowType.ID);
            UsersBUS ub = new UsersBUS();
            bool contain = true;
            TypeBUS _tlBUS = new TypeBUS();
            List<LastIdModel> lModel = new List<LastIdModel>();
            lModel = _tlBUS.isInTitle();
            for (int i = 0; i < lModel.Count; i++)
            {
                if (lModel[i].ID == a)
                {
                    contain = true;
                    break;

                }
                else contain = false;

            }
            if (lModel.Count == 0)
            {
                contain = false;
            }
            if (contain == false)
            {
                if (_tlBUS.DeleteTitle(a, this.Name, Login._user.idUser) == true)
                {

                    RadMessageBox.Show("You have successfully delete title!");
                }
                else
                {
                    RadMessageBox.Show("Something went wrong. Please contact your administrator!");
                }
            }
            else
            {
                RadMessageBox.Show("This type cannot be deleted");
            }
        }
        private void employeeFunction()
        {
            string tip = gridViewType.SelectedRowType.type;
            int a = Convert.ToInt32(gridViewType.SelectedRowType.ID);
            UsersBUS ub = new UsersBUS();
            bool contain = true;
            TypeBUS _tlBUS = new TypeBUS();
            List<LastIdModel> lModel = new List<LastIdModel>();
            lModel = _tlBUS.isInEmployeeFunction();
            for (int i = 0; i < lModel.Count; i++)
            {
                if (lModel[i].ID == a)
                {
                    contain = true;
                    break;

                }
                else contain = false;

            }
            if (lModel.Count == 0)
            {
                contain = false;
            }
            if (contain == false)
            {
                if (_tlBUS.DeleteEmployeeFunction(a, this.Name, Login._user.idUser) == true)
                {

                    RadMessageBox.Show("You have successfully delete employee function!");
                }
                else
                {
                    RadMessageBox.Show("Something went wrong. Please contact your administrator!");
                }
            }
            else
            {
                RadMessageBox.Show("This type cannot be deleted");
            }
        }
        private void employeeStatus()
        {
            string tip = gridViewType.SelectedRowType.type;
            int a = Convert.ToInt32(gridViewType.SelectedRowType.ID);
            UsersBUS ub = new UsersBUS();
            bool contain = true;
            TypeBUS _tlBUS = new TypeBUS();
            List<LastIdModel> lModel = new List<LastIdModel>();
            lModel = _tlBUS.isInEmployeeStatus();
            for (int i = 0; i < lModel.Count; i++)
            {
                if (lModel[i].ID == a)
                {
                    contain = true;
                    break;

                }
                else contain = false;

            }
            if (lModel.Count == 0)
            {
                contain = false;
            }
            if (contain == false)
            {
                if (_tlBUS.DeleteEmployeeStatus(a, this.Name, Login._user.idUser) == true)
                {

                    RadMessageBox.Show("You have successfully delete employee status!");
                }
                else
                {
                    RadMessageBox.Show("Something went wrong. Please contact your administrator!");
                }
            }
            else
            {
                RadMessageBox.Show("This type cannot be deleted");
            }
        }
        private void meetingStatus()
        {
            string tip = gridViewType.SelectedRowType.type;
            int a = Convert.ToInt32(gridViewType.SelectedRowType.ID);
            UsersBUS ub = new UsersBUS();
            bool contain = true;
            TypeBUS _tlBUS = new TypeBUS();
            List<LastIdModel> lModel = new List<LastIdModel>();
            lModel = _tlBUS.isInMeetingsStatus();
            for (int i = 0; i < lModel.Count; i++)
            {
                if (lModel[i].ID == a)
                {
                    contain = true;
                    break;

                }
                else contain = false;

            }
            if (lModel.Count == 0)
            {
                contain = false;
            }
            if (contain == false)
            {
                if (_tlBUS.DeleteMeetingsStatus(a, this.Name, Login._user.idUser) == true)
                {

                    RadMessageBox.Show("You have successfully delete meeting status!");
                }
                else
                {
                    RadMessageBox.Show("Something went wrong. Please contact your administrator!");
                }
            }
            else
            {
                RadMessageBox.Show("This type cannot be deleted");
            }
        }
        private void toDoStatus()
        {
            string tip = gridViewType.SelectedRowType.type;
            int a = Convert.ToInt32(gridViewType.SelectedRowType.ID);
            UsersBUS ub = new UsersBUS();
            bool contain = true;
            TypeBUS _tlBUS = new TypeBUS();
            List<LastIdModel> lModel = new List<LastIdModel>();
            lModel = _tlBUS.isInToDoStatus();
            for (int i = 0; i < lModel.Count; i++)
            {
                if (lModel[i].ID == a)
                {
                    contain = true;
                    break;

                }
                else contain = false;

            }
            if (lModel.Count == 0)
            {
                contain = false;
            }
            if (contain == false)
            {
                if (_tlBUS.DeleteToDoStatus(a, this.Name, Login._user.idUser) == true)
                {

                    RadMessageBox.Show("You have successfully delete to do status!");
                }
                else
                {
                    RadMessageBox.Show("Something went wrong. Please contact your administrator!");
                }
            }
            else
            {
                RadMessageBox.Show("This type cannot be deleted");
            }
        }
        private void contactReason()
        {
            string tip = gridViewType.SelectedRowType.type;
            int a = Convert.ToInt32(gridViewType.SelectedRowType.ID);
            UsersBUS ub = new UsersBUS();
            bool contain = true;
            TypeBUS _tlBUS = new TypeBUS();
            List<LastIdModel> lModel = new List<LastIdModel>();
           
            lModel = _tlBUS.isInContactReason();
            for (int i = 0; i < lModel.Count; i++)
            {
                if (lModel[i].ID == a)
                {
                    contain = true;
                    break;

                }
                else contain = false;

            }
            if (lModel.Count == 0)
            {
                contain = false;
            }
            if (contain == false)
            {
                if (_tlBUS.DeleteContactReason(a, this.Name, Login._user.idUser) == true)
                {

                    RadMessageBox.Show("You have successfully delete Contact reason!");
                }
                else
                {
                    RadMessageBox.Show("Something went wrong. Please contact your administrator!");
                }
            }
            else
            {
                RadMessageBox.Show("This type cannot be deleted");
            }
        }
     # endregion

        #region Notifications

        private Timer tmNotifications = new Timer();
        private RadReminder rsrReminder = new RadReminder();
        private Appointment[] nApp = null;

        private void tmNotifications_Tick(object sender, EventArgs e)
        {
            rsrReminder.StopReminder();
            foreach (Appointment a in nApp)
                if (a.Reminder.ToString() != "00:00:00")
                    if (DateTime.Now < a.Start)
                        if (DateTime.Now > a.Start.Subtract((TimeSpan)a.Reminder))
                        {
                            rsrReminder.ClearRemindObjects();
                            rsrReminder.AddRemindObject(a);
                            rsrReminder.StartReminder();
                            a.Reminder = new TimeSpan(0);
                            break;
                        }
        }

        private void LoadAppointmentsToControl()
        {
            List<MeetingsModel> lst = new MeetingsBUS().GetMeetings(Login._user.idUser, Login._user.lngUser);

            Appointment app = null;
            Int32 i = 0;
            nApp = new Appointment[lst.Count];

            foreach (MeetingsModel m in lst)
            {
                System.TimeSpan tsDuration = new System.TimeSpan(), tsReminder = new System.TimeSpan();
                DateTime datEnd = new DateTime();

                try
                {
                    tsDuration = (System.TimeSpan)m.durationMeeting;
                    datEnd = ((DateTime)m.openDateMeeting).Add((System.TimeSpan)tsDuration);
                    tsReminder = new TimeSpan(((DateTime)m.openDateMeeting).Subtract((DateTime)m.timeRemind).Ticks);
                }
                catch
                { }

                MeetingDescModel obj = new MeetingsBUS().GetMeetingDesc(m.idMeeting, Login._user.lngUser);

                app = new Appointment
                {
                    AllDay = m.isAllDayMeeting,
                    Start = (DateTime)m.openDateMeeting,
                    End = datEnd,
                    Duration = tsDuration,
                    Description = m.descriptionMeeting,
                    Location = "",
                    Summary = obj.noteMeeting + "\n" + obj.CategoryMeeting + "\n" + obj.PriorityMeeting + "\n" + obj.StatusMeeting + "\n" + obj.Client + "\n" + obj.ContactPerson + "\n" + obj.Project,
                    Reminder = tsReminder
                };

                nApp[i++] = app;
            }
        }

        private void f_AppointmentRefresh(Object sender, EventArgs e)
        {
            LoadAppointmentsToControl();
        }

        #endregion

        private void radButtonCreateBookmark_Click(object sender, EventArgs e)
        {
            frmBookmark2 frmBookmarks = new frmBookmark2();
            frmBookmarks.ShowDialog();

            if (gridViewLayouts != null)
            {
                modelData = activeGrid.GetData(0, null, "");
                activeGrid.SetDataPersonBinding(modelData);
                gridViewLayouts.SetDataPersonBinding(modelData);
            }
        }

        private void splitPanel5_ControlRemoved(object sender, ControlEventArgs e)
        {
            noGridSituation();
        }
        public void noGridSituation()
        {
            radRibbonCustomFilters.Visibility = ElementVisibility.Collapsed;
            //radRibbonBarGroup1.Visibility = ElementVisibility.Collapsed;            
            treeViewFilters.Nodes.Clear();
            treeViewLabels.Nodes.Clear();
            treeViewFilters.Visible = false;
            treeViewLabels.Visible = false;
        }

       
    //==================================================================
        private void setStandardFilterIfExist()
        {
            idLabelList = new List<int>();
            if (File.Exists(activeGrid.FilterFolder + "\\Standard.xml"))
            {
                for (int i = 0; i < treeViewFilters.Nodes[lngCustomFilters].Nodes.Count; i++)
                {
                    if (treeViewFilters.Nodes[lngCustomFilters].Nodes[i].Find(s => s.Text == "Standard") != null)
                    {
                        this.activeGrid.LoadLayout("Standard.xml");
                    }
                }
                if (File.Exists(activeGrid.LabelFolder + "\\Standard.xml") == true)
                {
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.Load(Path.Combine(activeGrid.LabelFolder, "Standard.xml"));
                    XmlNodeList xList = xmldoc.SelectNodes("/Root");
                    foreach (XmlNode xn in xList)
                    {
                        if (xn.SelectSingleNode("Labels") != null)
                        {
                            String[] nn = xn.SelectSingleNode("Labels").InnerText.Split(',');
                            for (int i = 0; i < nn.Length; i++)
                            {
                                idLabelList.Add(Convert.ToInt32(nn[i]));
                                foreach (var node in treeViewLabels.Nodes)
                                {
                                    if (node.Name == nn[i]) node.Checked = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void selectStandardFilterIfExist()
        {
            setStandardFilterIfExist();
            if (File.Exists(activeGrid.FilterFolder + "\\Standard.xml"))
            {
                for (int i = 0; i < treeViewFilters.Nodes[lngCustomFilters].Nodes.Count; i++)
                {
                    if (treeViewFilters.Nodes[lngCustomFilters].Nodes[i].Find(s => s.Text == "Standard") != null)
                    {
                        treeViewFilters.Nodes[lngCustomFilters].Nodes[i].Selected = true;
                        treeViewFilters.Nodes[lngCustomFilters].Nodes[i].Current = true;
                    }
                }

                if (File.Exists(activeGrid.LabelFolder + "\\Standard.xml") == true)
                {
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.Load(Path.Combine(activeGrid.LabelFolder, "Standard.xml"));
                    XmlNodeList xList = xmldoc.SelectNodes("/Root");
                    foreach (XmlNode xn in xList)
                    {
                        if (xn.SelectSingleNode("Labels") != null)
                        {
                            String[] nn = xn.SelectSingleNode("Labels").InnerText.Split(',');
                            for (int i = 0; i < nn.Length; i++)
                            {
                                foreach (var node in treeViewLabels.Nodes)
                                {
                                    if (node.Name == nn[i]) node.Checked = true;
                                }
                            }
                        }
                    }
                }

            }
        }

        //==================================================================
 

        private void radButtonDeleteBookmark_Click(object sender, EventArgs e)
        {
            if (gridViewLayouts != null)
            {
                if (gridViewLayouts.SelectedRowLayout != null)
                {
                    LayoutsModel lModel = gridViewLayouts.SelectedRowLayout;


                    DialogResult dr = MessageBox.Show("Delete " + lModel.nameLayout + " ?", "Delete Template", MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        LayoutsBUS lbus = new LayoutsBUS();
                        bool b = lbus.DeleteLayoutByID(lModel.idLayout, this.Name, Login._user.idUser);
                        if (b == true)
                        {
                            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Templates" + "\\" + lModel.fileLayout) == true)
                            {
                                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "Templates" + "\\" + lModel.fileLayout);
                            }
                        }


                        modelData = activeGrid.GetData(0, null, "");
                        activeGrid.SetDataPersonBinding(modelData);
                        gridViewLayouts.SetDataPersonBinding(modelData);
                    }
                }
            }
        }

        private void radButtonElementPurchaseReport_Click(object sender, EventArgs e)
        {
            frmPurchaseReport frmm = new frmPurchaseReport();
            frmm.Show();
        }

        private void radButtonElementSellReport_Click(object sender, EventArgs e)
        {
            frmSalesReport frms = new frmSalesReport();
            frms.Show();
        }

        private void radButtonVolReports_Click(object sender, EventArgs e)
        {
            //frmVolunteerReport ffr = new frmVolunteerReport();
            //ffr.Show();

            frmVolAvailabilityPreselection ffr = new frmVolAvailabilityPreselection();
            ffr.Show();
        }
        public void ddlBook_SelectedIndexChanged()
        {
            OnAccSettingsClick();
        }

        private void radButtonInvoiceSelection_Click(object sender, EventArgs e)
        {
            frmInvoiceSelectionForBooking frm = new frmInvoiceSelectionForBooking();
            frm.ShowDialog();
        }

        private void radButtonSearchAndBooking_Click(object sender, EventArgs e)
        {
            frmSearchAndBook frm = new frmSearchAndBook();
            frm.Show();
        }

        //public void BookYear_HandleBookYearChanged(object sender, BookingYearChangedEvent args)
        //{
        //    // Ukolio se promeni selekcija na Menus Gridu 
        //    if (args != null)
        //    {
        //        Login._bookyear = args.bookingYear;
        //        Type t = this.activeGrid.GetType();

              

        //        if (t == typeof(GridViewAccSettings))
        //        {
        //            MessageBox.Show(args.bookingYear);
        //            AccSettingsBUS acb = new AccSettingsBUS();
        //            List<AccSettingsModel> acm = new List<AccSettingsModel>();

        //            acm = acb.GetAllAccSettings(Login._bookyear);
        //            gridViewAccSettings.SetDataPersonBinding1(acm); 
        //        }

        //        if (t == typeof(GridViewLedger))
        //        {
        //            MessageBox.Show(args.bookingYear);
        //            List<IModel> lam = new List<IModel>();
        //            lam = new LedgerAccountBUS(Login._bookyear).GetAllAccounts();
        //            gridViewLedger.SetDataPersonBinding(lam);
        //        }
        //        if (t == typeof(GridViewDaily))
        //        {
        //            List<IModel> dym = new List<IModel>();
        //            dym = new AccDailyBUS(Login._bookyear).GetAllDailys();
        //            gridViewDaily.SetDataPersonBinding1(dym);
        //        }
        //        if (t == typeof(GridViewDailyEntry))
        //        {
        //            List<IModel> dem = new List<IModel>();
        //            dem = new AccDailyBUS(Login._bookyear).GetAllDailys();
        //            gridViewDailyEntry.SetDataPersonBinding(dem);

        //        }



        //    }

        //}
        private void logout(object sender, EventArgs e)
        {
            new UsersBUS().loginLogOut(DateTime.Now, Login._user.idUser, false);
        }

        private void radButtonElementAccountReports_Click(object sender, EventArgs e)
        {
            using(frmAccountReports frm = new frmAccountReports())
            {
                frm.ShowDialog();
            }
        }
        private void accountButton()  //popunjava dugme za godinu sa godinama iz setings-a
        {
            AccSettingsBUS asb = new AccSettingsBUS();
            List<AccSettingsModel> asm = new List<AccSettingsModel>();
            asm = asb.GetAllAccS();
            if (asm != null)
            {
                int i = 0;
                ddbeYear.Items.Clear();
               // List<RadMenuItem> myRadMenuItem = new List<RadMenuItem>();
                foreach (AccSettingsModel am in asm)
                {
                    RadMenuItem item = new RadMenuItem();
                    item.Text = am.yearSettings;
                    item.Click += new EventHandler(ddbeYear_Click);
                    ddbeYear.Items.Add(item);
                    
                  
                }
                
                
            }
        
        }

        private void ddbeYear_Click(object sender, EventArgs e)
        {
            if ((sender as RadMenuItem) != null)
            {
                btnElementYear.Text = (sender as RadMenuItem).Text;
                Login._bookyear = (sender as RadMenuItem).Text; //txtElementYear.Text;

                //=== menja boju buttona u zavisnosti da li je tekuca godina ili neka druga
                if (Login._bookyear != DateTime.Now.Year.ToString())
                {
                    btnElementYear.ButtonFillElement.BackColor = Color.Red;
                    btnElementYear.ForeColor = Color.White;
                }
                else
                {
                    btnElementYear.ButtonFillElement.BackColor = Color.Transparent;
                    btnElementYear.ForeColor = Color.Black;
                }
                //==========================================================================

                Type t = this.activeGrid.GetType();

                if (t == typeof(GridViewAccSettings))
                {
                    AccSettingsBUS acb = new AccSettingsBUS();
                    List<AccSettingsModel> acm = new List<AccSettingsModel>();

                    acm = acb.GetAllAccSettings(Login._bookyear);
                    gridViewAccSettings.SetDataPersonBinding1(acm);
                    accountButton();
                }

                if (t == typeof(GridViewLedger))
                {
                    List<IModel> lam = new List<IModel>();
                    lam = new LedgerAccountBUS(Login._bookyear).GetAllAccounts();
                    gridViewLedger.SetDataPersonBinding(lam);
                }
                if (t == typeof(GridViewDaily))
                {
                    List<IModel> dym = new List<IModel>();
                    dym = new AccDailyBUS(Login._bookyear).GetAllDailys();
                    gridViewDaily.SetDataPersonBinding1(dym);
                }
                if (t == typeof(GridViewDailyEntry))
                {
                    List<IModel> dem = new List<IModel>();
                    dem = new AccDailyBUS(Login._bookyear).GetAllDailys();
                    gridViewDailyEntry.SetDataPersonBinding(dem);

                }
               
            }


        }

        private void split_cont_Main_Click(object sender, EventArgs e)
        {

        }

       
            

    }
}