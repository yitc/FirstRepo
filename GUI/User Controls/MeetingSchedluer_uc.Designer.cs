namespace GUI
{
    partial class MeetingSchedluer_uc
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Telerik.WinControls.UI.SchedulerDailyPrintStyle schedulerDailyPrintStyle1 = new Telerik.WinControls.UI.SchedulerDailyPrintStyle();
            Telerik.WinControls.UI.AppointmentMappingInfo appointmentMappingInfo1 = new Telerik.WinControls.UI.AppointmentMappingInfo();
            Telerik.WinControls.UI.ResourceMappingInfo resourceMappingInfo1 = new Telerik.WinControls.UI.ResourceMappingInfo();
            this.radScheduler1 = new Telerik.WinControls.UI.RadScheduler();
            this.radSchedulerNavigator1 = new Telerik.WinControls.UI.RadSchedulerNavigator();
            this.radPanelSchedulerOptions = new Telerik.WinControls.UI.RadPanel();
            this.comboEmployees = new Telerik.WinControls.UI.RadDropDownList();
            this.radioEmployeeAppointments = new Telerik.WinControls.UI.RadRadioButton();
            this.radioAllAppointments = new Telerik.WinControls.UI.RadRadioButton();
            this.radioMyAppointments = new Telerik.WinControls.UI.RadRadioButton();
            this.schedulerBindingDataSource1 = new Telerik.WinControls.UI.SchedulerBindingDataSource();
            this.radSplitContainer1 = new Telerik.WinControls.UI.RadSplitContainer();
            this.splitPanel1 = new Telerik.WinControls.UI.SplitPanel();
            this.splitSchedluer = new Telerik.WinControls.UI.SplitPanel();
            this.radSchedulerReminder1 = new Telerik.WinControls.UI.RadSchedulerReminder(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.radScheduler1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radSchedulerNavigator1)).BeginInit();
            this.radSchedulerNavigator1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radPanelSchedulerOptions)).BeginInit();
            this.radPanelSchedulerOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboEmployees)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioEmployeeAppointments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioAllAppointments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioMyAppointments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.schedulerBindingDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.schedulerBindingDataSource1.EventProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.schedulerBindingDataSource1.ResourceProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radSplitContainer1)).BeginInit();
            this.radSplitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitSchedluer)).BeginInit();
            this.splitSchedluer.SuspendLayout();
            this.SuspendLayout();
            // 
            // radScheduler1
            // 
            this.radScheduler1.Culture = new System.Globalization.CultureInfo("nl-NL");
            this.radScheduler1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radScheduler1.FocusedDate = new System.DateTime(2015, 6, 30, 0, 0, 0, 0);
            this.radScheduler1.Location = new System.Drawing.Point(0, 0);
            this.radScheduler1.Name = "radScheduler1";
            schedulerDailyPrintStyle1.AppointmentFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            schedulerDailyPrintStyle1.DateEndRange = new System.DateTime(2015, 7, 2, 0, 0, 0, 0);
            schedulerDailyPrintStyle1.DateHeadingFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            schedulerDailyPrintStyle1.DateStartRange = new System.DateTime(2015, 6, 27, 0, 0, 0, 0);
            schedulerDailyPrintStyle1.PageHeadingFont = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Bold);
            this.radScheduler1.PrintStyle = schedulerDailyPrintStyle1;
            this.radScheduler1.Size = new System.Drawing.Size(862, 541);
            this.radScheduler1.TabIndex = 0;
            this.radScheduler1.Text = "radScheduler1";
            this.radScheduler1.ThemeName = "Office2013Dark";
            this.radScheduler1.AppointmentDropped += new System.EventHandler<Telerik.WinControls.UI.AppointmentMovedEventArgs>(this.radScheduler1_AppointmentDropped);
            this.radScheduler1.AppointmentResized += new System.EventHandler<Telerik.WinControls.UI.AppointmentResizedEventArgs>(this.radScheduler1_AppointmentResized);
            this.radScheduler1.AppointmentResizeEnd += new System.EventHandler<Telerik.WinControls.UI.SchedulerAppointmentEventArgs>(this.radScheduler1_AppointmentResizeEnd);
            this.radScheduler1.AppointmentAdded += new System.EventHandler<Telerik.WinControls.UI.AppointmentAddedEventArgs>(this.radScheduler1_AppointmentAdded);
            this.radScheduler1.AppointmentSelected += new System.EventHandler<Telerik.WinControls.UI.SchedulerAppointmentSelectedEventArgs>(this.radScheduler1_AppointmentSelected);
            this.radScheduler1.CellClick += new System.EventHandler<Telerik.WinControls.UI.SchedulerCellEventArgs>(this.radScheduler1_CellClick);
            this.radScheduler1.ContextMenuOpening += new Telerik.WinControls.UI.SchedulerContextMenuOpeningEventHandler(this.radScheduler1_ContextMenuOpening);
            this.radScheduler1.AppointmentEditDialogShowing += new System.EventHandler<Telerik.WinControls.UI.AppointmentEditDialogShowingEventArgs>(this.radScheduler1_AppointmentEditDialogShowing);
            this.radScheduler1.LocationChanged += new System.EventHandler(this.radScheduler1_LocationChanged);
            // 
            // radSchedulerNavigator1
            // 
            this.radSchedulerNavigator1.AssociatedScheduler = this.radScheduler1;
            this.radSchedulerNavigator1.Controls.Add(this.radPanelSchedulerOptions);
            this.radSchedulerNavigator1.DateFormat = "yyyy/MM/dd";
            this.radSchedulerNavigator1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radSchedulerNavigator1.Location = new System.Drawing.Point(0, 0);
            this.radSchedulerNavigator1.Name = "radSchedulerNavigator1";
            this.radSchedulerNavigator1.NavigationStepType = Telerik.WinControls.UI.NavigationStepTypes.Day;
            // 
            // 
            // 
            this.radSchedulerNavigator1.RootElement.StretchVertically = false;
            this.radSchedulerNavigator1.Size = new System.Drawing.Size(862, 77);
            this.radSchedulerNavigator1.TabIndex = 1;
            this.radSchedulerNavigator1.Text = "radSchedulerNavigator1";
            this.radSchedulerNavigator1.ThemeName = "Office2013Dark";
            // 
            // radPanelSchedulerOptions
            // 
            this.radPanelSchedulerOptions.Controls.Add(this.comboEmployees);
            this.radPanelSchedulerOptions.Controls.Add(this.radioEmployeeAppointments);
            this.radPanelSchedulerOptions.Controls.Add(this.radioAllAppointments);
            this.radPanelSchedulerOptions.Controls.Add(this.radioMyAppointments);
            this.radPanelSchedulerOptions.Location = new System.Drawing.Point(295, 40);
            this.radPanelSchedulerOptions.Margin = new System.Windows.Forms.Padding(2);
            this.radPanelSchedulerOptions.Name = "radPanelSchedulerOptions";
            this.radPanelSchedulerOptions.Size = new System.Drawing.Size(491, 42);
            this.radPanelSchedulerOptions.TabIndex = 2;
            this.radPanelSchedulerOptions.ThemeName = "Office2013Dark";
            ((Telerik.WinControls.UI.RadPanelElement)(this.radPanelSchedulerOptions.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0);
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radPanelSchedulerOptions.GetChildAt(0).GetChildAt(1))).AutoSize = false;
            // 
            // comboEmployees
            // 
            this.comboEmployees.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboEmployees.Enabled = false;
            this.comboEmployees.Location = new System.Drawing.Point(332, 8);
            this.comboEmployees.Name = "comboEmployees";
            this.comboEmployees.Size = new System.Drawing.Size(154, 20);
            this.comboEmployees.TabIndex = 5;
            this.comboEmployees.ThemeName = "Windows8";
            this.comboEmployees.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.comboEmployees_SelectedIndexChanged);
            // 
            // radioEmployeeAppointments
            // 
            this.radioEmployeeAppointments.Location = new System.Drawing.Point(232, 10);
            this.radioEmployeeAppointments.Name = "radioEmployeeAppointments";
            this.radioEmployeeAppointments.Size = new System.Drawing.Size(69, 18);
            this.radioEmployeeAppointments.TabIndex = 4;
            this.radioEmployeeAppointments.TabStop = false;
            this.radioEmployeeAppointments.Text = "Employee";
            this.radioEmployeeAppointments.Click += new System.EventHandler(this.radioEmployeeAppointments_Click);
            // 
            // radioAllAppointments
            // 
            this.radioAllAppointments.Location = new System.Drawing.Point(119, 10);
            this.radioAllAppointments.Name = "radioAllAppointments";
            this.radioAllAppointments.Size = new System.Drawing.Size(107, 18);
            this.radioAllAppointments.TabIndex = 3;
            this.radioAllAppointments.TabStop = false;
            this.radioAllAppointments.Text = "All Appointments";
            this.radioAllAppointments.Click += new System.EventHandler(this.radioAllAppointments_Click);
            // 
            // radioMyAppointments
            // 
            this.radioMyAppointments.CheckState = System.Windows.Forms.CheckState.Checked;
            this.radioMyAppointments.Location = new System.Drawing.Point(3, 10);
            this.radioMyAppointments.Name = "radioMyAppointments";
            this.radioMyAppointments.Size = new System.Drawing.Size(110, 18);
            this.radioMyAppointments.TabIndex = 2;
            this.radioMyAppointments.Text = "My Appointments";
            this.radioMyAppointments.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            this.radioMyAppointments.Click += new System.EventHandler(this.radioMyAppointments_Click);
            // 
            // schedulerBindingDataSource1
            // 
            // 
            // 
            // 
            this.schedulerBindingDataSource1.EventProvider.Mapping = appointmentMappingInfo1;
            // 
            // 
            // 
            this.schedulerBindingDataSource1.ResourceProvider.Mapping = resourceMappingInfo1;
            // 
            // radSplitContainer1
            // 
            this.radSplitContainer1.Controls.Add(this.splitPanel1);
            this.radSplitContainer1.Controls.Add(this.splitSchedluer);
            this.radSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radSplitContainer1.Location = new System.Drawing.Point(0, 0);
            this.radSplitContainer1.Name = "radSplitContainer1";
            this.radSplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // 
            // 
            this.radSplitContainer1.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.radSplitContainer1.Size = new System.Drawing.Size(862, 613);
            this.radSplitContainer1.TabIndex = 2;
            this.radSplitContainer1.TabStop = false;
            this.radSplitContainer1.Text = "radSplitContainer1";
            this.radSplitContainer1.ThemeName = "Office2013Dark";
            // 
            // splitPanel1
            // 
            this.splitPanel1.Location = new System.Drawing.Point(0, 0);
            this.splitPanel1.Name = "splitPanel1";
            // 
            // 
            // 
            this.splitPanel1.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.splitPanel1.Size = new System.Drawing.Size(862, 68);
            this.splitPanel1.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0F, -0.3883415F);
            this.splitPanel1.SizeInfo.SplitterCorrection = new System.Drawing.Size(0, -236);
            this.splitPanel1.TabIndex = 0;
            this.splitPanel1.TabStop = false;
            this.splitPanel1.Text = "splitPanel1";
            this.splitPanel1.ThemeName = "Office2013Dark";
            // 
            // splitSchedluer
            // 
            this.splitSchedluer.Controls.Add(this.radScheduler1);
            this.splitSchedluer.Location = new System.Drawing.Point(0, 72);
            this.splitSchedluer.Name = "splitSchedluer";
            // 
            // 
            // 
            this.splitSchedluer.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.splitSchedluer.Size = new System.Drawing.Size(862, 541);
            this.splitSchedluer.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0F, 0.362069F);
            this.splitSchedluer.SizeInfo.SplitterCorrection = new System.Drawing.Size(0, 220);
            this.splitSchedluer.TabIndex = 1;
            this.splitSchedluer.TabStop = false;
            this.splitSchedluer.Text = "splitPanel2";
            this.splitSchedluer.ThemeName = "Office2013Dark";
            // 
            // radSchedulerReminder1
            // 
            this.radSchedulerReminder1.AssociatedScheduler = this.radScheduler1;
            this.radSchedulerReminder1.ThemeName = null;
            this.radSchedulerReminder1.TimeInterval = 60000;
            // 
            // MeetingSchedluer_uc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radSchedulerNavigator1);
            this.Controls.Add(this.radSplitContainer1);
            this.Name = "MeetingSchedluer_uc";
            this.Size = new System.Drawing.Size(862, 613);
            this.Load += new System.EventHandler(this.MeetingSchedluer_uc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radScheduler1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radSchedulerNavigator1)).EndInit();
            this.radSchedulerNavigator1.ResumeLayout(false);
            this.radSchedulerNavigator1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radPanelSchedulerOptions)).EndInit();
            this.radPanelSchedulerOptions.ResumeLayout(false);
            this.radPanelSchedulerOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboEmployees)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioEmployeeAppointments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioAllAppointments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioMyAppointments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.schedulerBindingDataSource1.EventProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.schedulerBindingDataSource1.ResourceProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.schedulerBindingDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radSplitContainer1)).EndInit();
            this.radSplitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitSchedluer)).EndInit();
            this.splitSchedluer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public Telerik.WinControls.UI.RadScheduler radScheduler1;
        private Telerik.WinControls.UI.RadSchedulerNavigator radSchedulerNavigator1;
        private Telerik.WinControls.UI.SchedulerBindingDataSource schedulerBindingDataSource1;
        private Telerik.WinControls.UI.RadSplitContainer radSplitContainer1;
        private Telerik.WinControls.UI.SplitPanel splitPanel1;
        private Telerik.WinControls.UI.SplitPanel splitSchedluer;
        private Telerik.WinControls.UI.RadSchedulerReminder radSchedulerReminder1;
        private Telerik.WinControls.UI.RadPanel radPanelSchedulerOptions;
        private Telerik.WinControls.UI.RadRadioButton radioEmployeeAppointments;
        private Telerik.WinControls.UI.RadRadioButton radioAllAppointments;
        private Telerik.WinControls.UI.RadRadioButton radioMyAppointments;
        private Telerik.WinControls.UI.RadDropDownList comboEmployees;
    }
}
