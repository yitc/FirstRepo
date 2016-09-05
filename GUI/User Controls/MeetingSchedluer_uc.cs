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
using Telerik.WinControls.UI;
using System.Resources;

namespace GUI
{    
    public partial class MeetingSchedluer_uc : UserControl
    {
        public int perosnId = -1;
        public string perosnName = "";

        public event EventHandler<RadioAppointmentSelectChanged> StatusRadioAppointmentSelectChanged = delegate { };
        public void RaiseStatusChanged(int id)
        {
            StatusRadioAppointmentSelectChanged(this, new RadioAppointmentSelectChanged { id = id });
        }

        MeetingEditAppointment appointmentDialog = null;
        private List<BISAppointment> appointments = new List<BISAppointment>();
        RadSchedulerReminder schedulerReminder;
        bool pageLoaded = false;
        public MeetingSchedluer_uc()
        {
            InitializeComponent();
            
            schedulerReminder = new RadSchedulerReminder();
            schedulerReminder.AssociatedScheduler = this.radScheduler1;
            schedulerReminder.StartReminderInterval = DateTime.Now;
            schedulerReminder.EndReminderInterval = DateTime.Now.AddDays(1);
            
        }

        private void radScheduler1_AppointmentEditDialogShowing(object sender, Telerik.WinControls.UI.AppointmentEditDialogShowingEventArgs e)
        {
           
            if (this.appointmentDialog == null)
            {
                if(perosnId == -1)
                    this.appointmentDialog = new MeetingEditAppointment();
                else
                    this.appointmentDialog = new MeetingEditAppointment(perosnId, perosnName);
            }
            e.AppointmentEditDialog = this.appointmentDialog;
        }

        private void MeetingSchedluer_uc_Load(object sender, EventArgs e)
        {
            radScheduler1.FocusedDate = DateTime.Today;
            SchedulerDayView dayView = this.radScheduler1.GetDayView();
            dayView.DayCount = 5;
            //radScheduler1.AccessibleInterval.Start = DateTime.Today;
            //radScheduler1.AccessibleInterval.End = radScheduler1.AccessibleInterval.Start.AddDays(5);
            SchedulerNavigatorLocalizationProvider.CurrentProvider = new CustomSchedulerNavigatorLocalizationProvider();

            if(Login._user.isUserManager == true)
            {
                radPanelSchedulerOptions.Visible = true;

                EmployeeBUS eBUS = new EmployeeBUS();
                List<EmployeeModel> emodels = eBUS.GetAllEmpl1(Login._user.lngUser);
                //comboEmployees.DataSource = emodel;

                foreach(EmployeeModel m in emodels)
                {
                    RadListDataItem dataItem = new RadListDataItem();                    
                    dataItem.Value = m.idEmployee;
                    dataItem.Text = m.firstNameEmployee + " " + m.lastNameEmployee;
                    comboEmployees.Items.Add(dataItem);
                }
                
                if(comboEmployees.Items.Count > 0)
                {
                    comboEmployees.SelectedIndex = 0;
                }
                
            }
            else
            {
                radPanelSchedulerOptions.Visible = false;
            }

            ReloadAppointments(1, Login._user.idEmployee);
                            
            pageLoaded = true;
        }

        public void ReloadAppointments(int appointment_to_get, int owner)

        {

            setTranslation();


            //base.OnLoad(e);
            this.radScheduler1.AppointmentFactory = new CustomAppointmentFactory();
            this.radScheduler1.AppointmentEditDialogShowing += new EventHandler<AppointmentEditDialogShowingEventArgs>(radScheduler1_AppointmentEditDialogShowing);

            if (appointment_to_get == 0)
            {
                AppointmentsBUS appBUS = new AppointmentsBUS();
                appointments = appBUS.GetALLAppointment_BISModel(Login._user.lngUser);
            }
            if (appointment_to_get == 1)
            {
                // by owner
                AppointmentsBUS appBUS = new AppointmentsBUS();
                appointments = appBUS.GetAppointmentsByOwner(owner, Login._user.lngUser);
            }
        
            if (appointments != null)
            {
                // create and configure a scheduler binding source
                SchedulerBindingDataSource dataSource = new SchedulerBindingDataSource();
                dataSource.EventProvider.AppointmentFactory = this.radScheduler1.AppointmentFactory;

                // map the MyAppointment properties to the scheduler
                AppointmentMappingInfo appointmentMappingInfo = (AppointmentMappingInfo)dataSource.EventProvider.Mapping;

                appointmentMappingInfo.Mappings.Add(new SchedulerMapping("Category", "Category"));
                appointmentMappingInfo.Mappings.Add(new SchedulerMapping("CategoryName", "CategoryName"));
                appointmentMappingInfo.Mappings.Add(new SchedulerMapping("Priority", "Priority"));
                appointmentMappingInfo.Mappings.Add(new SchedulerMapping("PriorityName", "PriorityName"));
                appointmentMappingInfo.Mappings.Add(new SchedulerMapping("Status", "Status"));
                appointmentMappingInfo.Mappings.Add(new SchedulerMapping("StatusName", "StatusName"));
                appointmentMappingInfo.Mappings.Add(new SchedulerMapping("Owner", "Owner"));
                appointmentMappingInfo.Mappings.Add(new SchedulerMapping("OwnerName", "OwnerName"));
                appointmentMappingInfo.Mappings.Add(new SchedulerMapping("Client", "Client"));
                appointmentMappingInfo.Mappings.Add(new SchedulerMapping("ClientName", "ClientName"));
                appointmentMappingInfo.Mappings.Add(new SchedulerMapping("Contact", "Contact"));
                appointmentMappingInfo.Mappings.Add(new SchedulerMapping("ContactName", "ContactName"));
                appointmentMappingInfo.Mappings.Add(new SchedulerMapping("Project", "Project"));
                appointmentMappingInfo.Mappings.Add(new SchedulerMapping("ProjectName", "ProjectName"));
                appointmentMappingInfo.Mappings.Add(new SchedulerMapping("Responsible", "Responsible"));
                appointmentMappingInfo.Mappings.Add(new SchedulerMapping("ResponsibleName", "ResponsibleName"));
                appointmentMappingInfo.Mappings.Add(new SchedulerMapping("BackgroundId", "Background"));
                appointmentMappingInfo.Mappings.Add(new SchedulerMapping("AllDay", "IsAllDay"));
                appointmentMappingInfo.Mappings.Add(new SchedulerMapping("StatusId", "ShowTime"));
                appointmentMappingInfo.Mappings.Add(new SchedulerMapping("Reminder", "Reminder"));
                appointmentMappingInfo.Mappings.Add(new SchedulerMapping("Snoozed", "ReminderSnoozed"));
                appointmentMappingInfo.Mappings.Add(new SchedulerMapping("Dismissed", "ReminderDismissed"));
                

                SchedulerMapping backgroundIdSchedulerMapping = appointmentMappingInfo.FindBySchedulerProperty("BackgroundId");
                backgroundIdSchedulerMapping.ConvertToScheduler = new ConvertCallback(this.ConvertBackgrounIdToScheduler);
                backgroundIdSchedulerMapping.ConvertToDataSource = new ConvertCallback(this.ConvertBackgrounIdToDataSource);

                SchedulerMapping ShowTimeSchedulerMapping = appointmentMappingInfo.FindBySchedulerProperty("StatusId");
                ShowTimeSchedulerMapping.ConvertToScheduler = new ConvertCallback(this.ConvertSHOWTIMEToScheduler);
                ShowTimeSchedulerMapping.ConvertToDataSource = new ConvertCallback(this.ConvertSHOWTIMEToDataSource);

                appointmentMappingInfo.Start = "Start";
                appointmentMappingInfo.End = "End";
                appointmentMappingInfo.Summary = "Subject";
                appointmentMappingInfo.Description = "Description";
                appointmentMappingInfo.Location = "Location";
                appointmentMappingInfo.UniqueId = "Id";
                appointmentMappingInfo.Exceptions = "Exceptions";
                //appointmentMappingInfo.Reminder = "Reminder";
                

                //appointmentMappingInfo.Reminder
                //dataSource.EventProvider.Mapping = appointmentMappingInfo;
                // assign the generic List of CustomAppointment as the EventProvider data source
                dataSource.EventProvider.DataSource = appointments;
                
                this.radScheduler1.DataSource = dataSource;

                schedulerReminder.StartReminder();
            }     
        }
        private void setTranslation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(radioMyAppointments.Text) != null)
                    radioMyAppointments.Text = resxSet.GetString(radioMyAppointments.Text);

                if (resxSet.GetString(radioAllAppointments.Text) != null)
                    radioAllAppointments.Text = resxSet.GetString(radioAllAppointments.Text);

                if (resxSet.GetString(radioEmployeeAppointments.Text) != null)
                    radioEmployeeAppointments.Text = resxSet.GetString(radioEmployeeAppointments.Text);

                //radioMyAppointments.Text = resxSet.GetString("My Appointments");
                //radioAllAppointments.Text = resxSet.GetString("All Appointments");
                //radioEmployeeAppointments.Text = resxSet.GetString("Employee");


                //labelCategory.Text = resxSet.GetString("Category:");
                //labelClient.Text = resxSet.GetString("Client");
                //labelContact.Text = resxSet.GetString("Person");
                //labelProject.Text = resxSet.GetString("Project");
                //labelResponsible.Text = resxSet.GetString("Responible");
                //labelPriority.Text = resxSet.GetString("Priority");
                //labelStatusC.Text = resxSet.GetString("Status");
                //labelOwner.Text = resxSet.GetString("Owner");
            }
        }
        object ConvertBackgrounIdToScheduler(object obj)
        {                       
            if (!(obj is DBNull))
            {
                string appointmentBackground = (string)obj;
                appointmentBackground = appointmentBackground.Trim();
                switch (appointmentBackground)
                {
                    case "Business":
                        return AppointmentBackground.Business;
                    case "Important":
                        return AppointmentBackground.Important;
                    case "MustAttend":
                        return AppointmentBackground.MustAttend;
                    case "PhoneCall":
                        return AppointmentBackground.PhoneCall;
                    case "Personal":
                        return AppointmentBackground.Personal;
                    case "Vacation":
                        return AppointmentBackground.Vacation;
                    case "Travel Required":
                        return AppointmentBackground.TravelRequired;
                    case "Needs Preparation":
                        return AppointmentBackground.NeedsPreparation;
                    case "Birthday":
                        return AppointmentBackground.Birthday;
                    case "Anniversary":
                        return AppointmentBackground.Anniversary;
                    default:
                        return AppointmentBackground.None;
                }
            }
            
            return AppointmentBackground.None;
        }
        object ConvertBackgrounIdToDataSource(object obj)
        {
            AppointmentBackground appointmentBackground = (AppointmentBackground)obj;
            switch (appointmentBackground)
            {
                case AppointmentBackground.Business:
                    return "Business";
                case AppointmentBackground.Important:
                    return "Important";
                case AppointmentBackground.MustAttend:
                    return "MustAttend";
                case AppointmentBackground.PhoneCall:
                    return "PhoneCall";
                case AppointmentBackground.Personal:
                    return "Personal";
                case AppointmentBackground.Vacation:
                    return "Vacation";
                case AppointmentBackground.TravelRequired:
                    return "Travel Required";
                case AppointmentBackground.NeedsPreparation:
                    return "Needs Preparation";
                case AppointmentBackground.Birthday:
                    return "Birthday";
                case AppointmentBackground.Anniversary:
                    return "Anniversary";
                default:
                    return "None";
            }
            return "None";
        }

        object ConvertSHOWTIMEToScheduler(object obj)
        {
            if (!(obj is DBNull))
            {
                string showtime = (string)obj;
                showtime = showtime.Trim();
                switch (showtime)
                {
                    case "Busy":
                        return AppointmentStatus.Busy;
                    case "Free":
                        return AppointmentStatus.Free;
                    case "Tentative":
                        return AppointmentStatus.Tentative;
                    case "Unavailable":
                        return AppointmentStatus.Unavailable;                                         
                    default:
                        return AppointmentStatus.Free;
                }
            }

            return AppointmentStatus.Free;
        }

        object ConvertSHOWTIMEToDataSource(object obj)
        {
            if (!(obj is DBNull))
            {
                AppointmentStatus showtime = (AppointmentStatus)obj;
                
                switch (showtime)
                {
                    case AppointmentStatus.Busy:
                        return "Busy";
                    case AppointmentStatus.Free:
                        return "Free";
                    case AppointmentStatus.Tentative:
                        return "Tentative";
                    case AppointmentStatus.Unavailable:
                        return "Unavailable";
                    default:
                        return "Free";
                }
            }

            return "Free";
        }

       
        private void radScheduler1_AppointmentResized(object sender, AppointmentResizedEventArgs e)
        {
            //MessageBox.Show(e.Appointment.Start.ToString());
        }

        private void radScheduler1_AppointmentResizeEnd(object sender, SchedulerAppointmentEventArgs e)
        {
            //MessageBox.Show(e.Appointment.UniqueId.ToString() + " - " + e.Appointment.Start.ToString());
            try
            {
                if (e.Appointment != null)
                {
                    AppointmentsBUS appBus = new AppointmentsBUS();
                    Guid guid = Guid.Parse(e.Appointment.UniqueId.ToString());
                    appBus.UpdateTime(guid, e.Appointment.Start, e.Appointment.End, e.Appointment.AllDay, this.Name, Login._user.idUser);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void radScheduler1_AppointmentSelected(object sender, SchedulerAppointmentSelectedEventArgs e)
        {
            //if(e.Appointment != null)
            //{   
            //    MessageBox.Show(e.Appointment.UniqueId.ToString());
            //}
            //else
            //{
            //    MessageBox.Show("null");
            //}
        }

        private void radScheduler1_CellClick(object sender, SchedulerCellEventArgs e)
        {
            
        }

        private void radScheduler1_AppointmentAdded(object sender, AppointmentAddedEventArgs e)
        {
            if(radioAllAppointments.IsChecked == true)
                ReloadAppointments(0,0);

            if (radioMyAppointments.IsChecked == true)
                ReloadAppointments(1, Login._user.idEmployee);

            if (radioEmployeeAppointments.IsChecked == true)
                ReloadAppointments(1, (int) comboEmployees.SelectedItem.Value);
        }

        private void radScheduler1_LocationChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("lala");
        }

        private void radScheduler1_AppointmentDropped(object sender, AppointmentMovedEventArgs e)
        {
            try
            {
                if (e.Appointment != null)
                {
                    AppointmentsBUS appBus = new AppointmentsBUS();
                    Guid guid = Guid.Parse(e.Appointment.UniqueId.ToString());
                    appBus.UpdateTime(guid, e.Appointment.Start, e.Appointment.End, e.Appointment.AllDay, this.Name, Login._user.idUser);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void radScheduler1_ContextMenuOpening(object sender, SchedulerContextMenuOpeningEventArgs e)
        {
            e.Menu.Items[2].Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
        }

        private void radioEmployeeAppointments_Click(object sender, EventArgs e)
        {
            comboEmployees.Enabled = true;
            ReloadAppointments(1, (int) comboEmployees.SelectedItem.Value);

            // event za punjene task list view
            RaiseStatusChanged((int)comboEmployees.SelectedItem.Value);
        }

        private void radioAllAppointments_Click(object sender, EventArgs e)
        {
            comboEmployees.Enabled = false;

            ReloadAppointments(0, 0);
            
            // event za punjene task list view
            RaiseStatusChanged(-1);
        }

        private void radioMyAppointments_Click(object sender, EventArgs e)
        {
            
            comboEmployees.Enabled = false;
            ReloadAppointments(1, Login._user.idEmployee);

            // event za punjene task list view
            RaiseStatusChanged(Login._user.idEmployee);
        }

        private void comboEmployees_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (pageLoaded == true)
            {
                ReloadAppointments(1, (int)comboEmployees.SelectedItem.Value);
                
                // event za punjene task list view
                RaiseStatusChanged((int)comboEmployees.SelectedItem.Value);

            }
        }
    }

    public class RadioAppointmentSelectChanged : EventArgs
    {
        public int id { get; set; }
    }
    public class CustomSchedulerNavigatorLocalizationProvider : SchedulerNavigatorLocalizationProvider
    {
        public override string GetLocalizedString(string id)
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                switch (id)
                {


                    case SchedulerNavigatorStringId.DayViewButtonCaption:
                        {
                            return resxSet.GetString("Day view"); // return "Day View";
                        }
                    case SchedulerNavigatorStringId.WeekViewButtonCaption:
                        {
                            return resxSet.GetString("Week view");        //return "Week View";
                        }
                    case SchedulerNavigatorStringId.MonthViewButtonCaption:
                        {
                            return resxSet.GetString("Month View");      //return "Month View";
                        }
                    case SchedulerNavigatorStringId.TimelineViewButtonCaption:
                        {
                            return resxSet.GetString("Timeline view");     //return "Timeline View";
                        }
                    case SchedulerNavigatorStringId.ShowWeekendCheckboxCaption:
                        {
                            return resxSet.GetString("Show weekend");   //return "Show Weekend";
                        }
                    case SchedulerNavigatorStringId.TodayButtonCaptionToday:
                        {
                            return resxSet.GetString("Today");   //return "Today";
                        }
                    case SchedulerNavigatorStringId.TodayButtonCaptionThisWeek:
                        {
                            return resxSet.GetString("This week");   //return "This week";
                        }
                    case SchedulerNavigatorStringId.TodayButtonCaptionThisMonth:
                        {
                            return resxSet.GetString("This month");   //return "This month";
                        }

                }

            }
            return String.Empty;
        }
    }
}
