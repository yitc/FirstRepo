using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.WinControls.UI.Localization;

namespace GUI
{
    public class CustomSchedulerLocalizationProvider : RadSchedulerLocalizationProvider
    {
        public override string GetLocalizedString(string id)
        {
            switch (id)
            {
                case RadSchedulerStringId.NextAppointment: return "Volgende afspraak"; //"Next Appointment";
                case RadSchedulerStringId.PreviousAppointment: return "Vorige afspraak"; // "Previous Appointment";
                case RadSchedulerStringId.AppointmentDialogTitle: return "Afspraak bewerken";  // "Edit Appointment";
                case RadSchedulerStringId.AppointmentDialogSubject: return "Onderwerp:"; //"Subject:";
                case RadSchedulerStringId.AppointmentDialogLocation: return "Locatie:";  // "Location:";
                case RadSchedulerStringId.AppointmentDialogBackground: return "Achtergrond:";  // "Background:";
                case RadSchedulerStringId.AppointmentDialogDescription: return "Omschrijving:";  // "Description:";
                case RadSchedulerStringId.AppointmentDialogStartTime: return "Starttijd:"; // "Start time:";
                case RadSchedulerStringId.AppointmentDialogEndTime: return "Eindtijd:";   // "End time:";
                case RadSchedulerStringId.AppointmentDialogAllDay: return "All day event";
                case RadSchedulerStringId.AppointmentDialogResource: return "Resource:";
                case RadSchedulerStringId.AppointmentDialogStatus: return "Laat tijd als:";  // "Show time as:";
                case RadSchedulerStringId.AppointmentDialogOK: return "OK";
                case RadSchedulerStringId.AppointmentDialogCancel: return "Annuleren";  // "Cancel";
                case RadSchedulerStringId.AppointmentDialogDelete: return "Verwijder";  // "Delete";
                case RadSchedulerStringId.AppointmentDialogRecurrence: return "Herhaling"; // "Recurrence";

                case RadSchedulerStringId.OpenRecurringDialogTitle: return "Open Recurring Item";
                case RadSchedulerStringId.DeleteRecurrenceDialogOK: return "OK";
                case RadSchedulerStringId.OpenRecurringDialogOK: return "OK";

                case RadSchedulerStringId.DeleteRecurrenceDialogCancel: return "Annuleren";  //"Cancel";
                case RadSchedulerStringId.OpenRecurringDialogCancel: return "Annuleren"; // "Cancel";
                case RadSchedulerStringId.OpenRecurringDialogLabel: return "\"{0}\" is a recurring\nappointment. Do you want to open\nonly this occurrence or the series?";
                case RadSchedulerStringId.OpenRecurringDialogRadioOccurrence: return "Open this occurrence.";
                case RadSchedulerStringId.OpenRecurringDialogRadioSeries: return "Open the series.";

                case RadSchedulerStringId.DeleteRecurrenceDialogTitle: return "Bevestig Verwijder"; // "Confirm Delete";
                case RadSchedulerStringId.DeleteRecurrenceDialogRadioOccurrence: return "Delete this occurrence.";
                case RadSchedulerStringId.DeleteRecurrenceDialogRadioSeries: return "Delete the series.";
                case RadSchedulerStringId.DeleteRecurrenceDialogLabel: return "Do you want to delete all occurrences of the recurring appointment \"{0}\", or just this one?";

                case RadSchedulerStringId.RecurrenceDragDropCreateExceptionDialogText: return "You changed the date of a single occurrence of a recurring appointment. To change all the dates, open the series.\nDo you want to change just this one?";
                case RadSchedulerStringId.RecurrenceDragDropValidationSameDateText: return "Two occurrences of the same series cannot occur on the same day.";
                case RadSchedulerStringId.RecurrenceDragDropValidationSkipOccurrenceText: return "Cannot reschedule an occurrence of a recurring appointment if it skips over a later occurrence of the same appointment.";

                case RadSchedulerStringId.RecurrenceDialogMessageBoxText: return "Start date should be before EndBy date.";
                case RadSchedulerStringId.RecurrenceDialogMessageBoxWrongRecurrenceRuleText: return "The recurrence pattern is not valid.";
                case RadSchedulerStringId.RecurrenceDialogMessageBoxTitle: return "Validation error";
                case RadSchedulerStringId.RecurrenceDialogTitle: return "Edit Recurrence";
                case RadSchedulerStringId.RecurrenceDialogAppointmentTimeGroup: return "Afspraak tijd";  // "Appointment time";
                case RadSchedulerStringId.RecurrenceDialogDuration: return "Duur:"; // "Duration:";
                case RadSchedulerStringId.RecurrenceDialogAppointmentEnd: return "Einde";  // "End:";
                case RadSchedulerStringId.RecurrenceDialogAppointmentStart: return "Begin";  // "Start:";
                case RadSchedulerStringId.RecurrenceDialogRecurrenceGroup: return "Recurrence pattern";
                case RadSchedulerStringId.RecurrenceDialogRangeGroup: return "Range of recurrence";
                case RadSchedulerStringId.RecurrenceDialogOccurrences: return "occurrences";
                case RadSchedulerStringId.RecurrenceDialogRecurrenceStart: return "Begin";  //"Start:";
                case RadSchedulerStringId.RecurrenceDialogYearly: return "Yearly";
                case RadSchedulerStringId.RecurrenceDialogHourly: return "Hourly";
                case RadSchedulerStringId.RecurrenceDialogMonthly: return "Monthly";
                case RadSchedulerStringId.RecurrenceDialogWeekly: return "Weekly";
                case RadSchedulerStringId.RecurrenceDialogDaily: return "Daily";
                case RadSchedulerStringId.RecurrenceDialogEndBy: return "End by:";
                case RadSchedulerStringId.RecurrenceDialogEndAfter: return "End after:";
                case RadSchedulerStringId.RecurrenceDialogNoEndDate: return "No end date";
                case RadSchedulerStringId.RecurrenceDialogAllDay: return "All day event";
                case RadSchedulerStringId.RecurrenceDialogDurationDropDown1Day: return "1 day";
                case RadSchedulerStringId.RecurrenceDialogDurationDropDown2Days: return "2 days";
                case RadSchedulerStringId.RecurrenceDialogDurationDropDown3Days: return "3 days";
                case RadSchedulerStringId.RecurrenceDialogDurationDropDown4Days: return "4 days";
                case RadSchedulerStringId.RecurrenceDialogDurationDropDown1Week: return "1 week";
                case RadSchedulerStringId.RecurrenceDialogDurationDropDown2Weeks: return "2 weeks";

                case RadSchedulerStringId.RecurrenceDialogOK: return "OK";
                case RadSchedulerStringId.RecurrenceDialogCancel: return "Begin"; //"Cancel";
                case RadSchedulerStringId.RecurrenceDialogRemoveRecurrence: return "Remove Recurrence";

                case RadSchedulerStringId.HourlyRecurrenceEvery: return  "Ieder";  //"Every";
                case RadSchedulerStringId.HourlyRecurrenceHours: return "hour(s)";

                case RadSchedulerStringId.DailyRecurrenceEveryDay: return  "Ieder";  //"Every";
                case RadSchedulerStringId.DailyRecurrenceEveryWeekday: return "Every weekday";
                case RadSchedulerStringId.DailyRecurrenceDays: return "day(s)";

                case RadSchedulerStringId.WeeklyRecurrenceRecurEvery: return "Recur every";
                case RadSchedulerStringId.WeeklyRecurrenceWeeksOn: return "week(s) on:";
                case RadSchedulerStringId.WeeklyRecurrenceSunday: return "Zondag";  //"Sunday";
                case RadSchedulerStringId.WeeklyRecurrenceMonday: return "Maandag";  //"Monday";
                case RadSchedulerStringId.WeeklyRecurrenceTuesday: return "Dinsdag";  //"Tuesday";
                case RadSchedulerStringId.WeeklyRecurrenceWednesday: return "Woensdag";  //"Wednesday";
                case RadSchedulerStringId.WeeklyRecurrenceThursday: return "Donderdag";  //"Thursday";
                case RadSchedulerStringId.WeeklyRecurrenceFriday: return "Vrijdag";  //"Friday";
                case RadSchedulerStringId.WeeklyRecurrenceSaturday: return "Zaterdag";  //"Saturday";

                case RadSchedulerStringId.WeeklyRecurrenceDay: return "Dag";  //"Day";
                case RadSchedulerStringId.WeeklyRecurrenceWeekday: return "Weekday";
                case RadSchedulerStringId.WeeklyRecurrenceWeekendDay: return "Weekend day";

                case RadSchedulerStringId.MonthlyRecurrenceDay: return "Dag";  //"Day";
                case RadSchedulerStringId.MonthlyRecurrenceWeek: return "The";
                case RadSchedulerStringId.MonthlyRecurrenceDayOfMonth: return "of every";
                case RadSchedulerStringId.MonthlyRecurrenceMonths: return "month(s)";
                case RadSchedulerStringId.MonthlyRecurrenceWeekOfMonth: return "of every";
                case RadSchedulerStringId.MonthlyRecurrenceFirst: return "Eerste";  //"First";
                case RadSchedulerStringId.MonthlyRecurrenceSecond: return "Tweede";  //"Second";
                case RadSchedulerStringId.MonthlyRecurrenceThird: return "Derde";  //"Third";
                case RadSchedulerStringId.MonthlyRecurrenceFourth: return "Vierde";  //"Fourth";
                case RadSchedulerStringId.MonthlyRecurrenceLast: return "Laatste";  //"Last";

                case RadSchedulerStringId.YearlyRecurrenceDayOfMonth: return  "Ieder";  // "Every";
                case RadSchedulerStringId.YearlyRecurrenceWeekOfMonth: return "The";
                case RadSchedulerStringId.YearlyRecurrenceOfMonth: return "of";
                case RadSchedulerStringId.YearlyRecurrenceJanuary: return "Januari"; // "January";
                case RadSchedulerStringId.YearlyRecurrenceFebruary: return "Februari";  //"February";
                case RadSchedulerStringId.YearlyRecurrenceMarch: return "Mars";  //"March";
                case RadSchedulerStringId.YearlyRecurrenceApril: return "April"; //"April";
                case RadSchedulerStringId.YearlyRecurrenceMay: return "Mei";  //"May";
                case RadSchedulerStringId.YearlyRecurrenceJune: return "Juni"; //"June";
                case RadSchedulerStringId.YearlyRecurrenceJuly: return "Juli"; //"July";
                case RadSchedulerStringId.YearlyRecurrenceAugust: return "Augustus";  //"August";
                case RadSchedulerStringId.YearlyRecurrenceSeptember: return "September";  //"September";
                case RadSchedulerStringId.YearlyRecurrenceOctober: return "Oktober"; //"October";
                case RadSchedulerStringId.YearlyRecurrenceNovember: return "November" ; //"November";
                case RadSchedulerStringId.YearlyRecurrenceDecember: return "December";  //"December";

                case RadSchedulerStringId.BackgroundNone: return "None";
                case RadSchedulerStringId.BackgroundImportant: return "Important";
                case RadSchedulerStringId.BackgroundBusiness: return "Business";
                case RadSchedulerStringId.BackgroundPersonal: return "Personal";
                case RadSchedulerStringId.BackgroundVacation: return "Vacation";
                case RadSchedulerStringId.BackgroundMustAttend: return "Must Attend";
                case RadSchedulerStringId.BackgroundTravelRequired: return "Travel Required";
                case RadSchedulerStringId.BackgroundNeedsPreparation: return "Needs Preparation";
                case RadSchedulerStringId.BackgroundBirthday: return "Birthday";
                case RadSchedulerStringId.BackgroundAnniversary: return "Anniversary";
                case RadSchedulerStringId.BackgroundPhoneCall: return "Phone Call";

                case RadSchedulerStringId.StatusBusy: return "Druk";  //"Busy";
                case RadSchedulerStringId.StatusFree: return "Free";
                case RadSchedulerStringId.StatusTentative: return "Tentative";
                case RadSchedulerStringId.StatusUnavailable: return "Niet beschikbaar";  //"Unavailable";

                case RadSchedulerStringId.ReminderNone: return "Geen";  //"None";
                case RadSchedulerStringId.ReminderZeroMinutes: return "0 notulen";
                case RadSchedulerStringId.ReminderFiveMinutes: return "5 notulen";
                case RadSchedulerStringId.ReminderTenMinutes: return "10 notulen";
                case RadSchedulerStringId.ReminderFifteenMinutes: return "15 notulen";
                case RadSchedulerStringId.ReminderThirtyMinutes: return "30 notulen";
                case RadSchedulerStringId.ReminderOneHour: return "1 uur";
                case RadSchedulerStringId.ReminderTwoHours: return "2 uur";
                case RadSchedulerStringId.ReminderThreeHours: return "3 uur";
                case RadSchedulerStringId.ReminderFourHours: return "4 uur";
                case RadSchedulerStringId.ReminderFiveHours: return "5 uur";
                case RadSchedulerStringId.ReminderSixHours: return "6 uur";
                case RadSchedulerStringId.ReminderSevenHours: return "7 uur";
                case RadSchedulerStringId.ReminderEightHours: return "8 uur";
                case RadSchedulerStringId.ReminderNineHours: return "9 uur";
                case RadSchedulerStringId.ReminderTenHours: return "10 uur";
                case RadSchedulerStringId.ReminderElevenHours: return "11 uur";
                case RadSchedulerStringId.ReminderTwelveHours: return "12 uur";
                case RadSchedulerStringId.ReminderEighteenHours: return "18 uur";
                case RadSchedulerStringId.ReminderOneDay: return "1 dag";
                case RadSchedulerStringId.ReminderTwoDays: return "2 dagen";
                case RadSchedulerStringId.ReminderThreeDays: return "3 dagen";
                case RadSchedulerStringId.ReminderFourDays: return "4 dagen";
                case RadSchedulerStringId.ReminderOneWeek: return "1 week";
                case RadSchedulerStringId.ReminderTwoWeeks: return "2 weeks";
                case RadSchedulerStringId.Reminder: return "Herinnering"; //"Reminder";

                case RadSchedulerStringId.ContextMenuNewAppointment: return "Nieuwe afspraak";
                case RadSchedulerStringId.ContextMenuEditAppointment: return "Afspraak bewerken"; // "Edit Appointment";
                case RadSchedulerStringId.ContextMenuNewRecurringAppointment: return "New Recurring Appointment";
                case RadSchedulerStringId.ContextMenu60Minutes: return "60 notulen";
                case RadSchedulerStringId.ContextMenu30Minutes: return "30 notulen";
                case RadSchedulerStringId.ContextMenu15Minutes: return "15 notulen";
                case RadSchedulerStringId.ContextMenu10Minutes: return "10 notulen";
                case RadSchedulerStringId.ContextMenu6Minutes: return "6 notulen";
                case RadSchedulerStringId.ContextMenu5Minutes: return "5 notulen";
                case RadSchedulerStringId.ContextMenuNavigateToNextView: return "Next View";
                case RadSchedulerStringId.ContextMenuNavigateToPreviousView: return "Vorige View";  //"Previous View";
                case RadSchedulerStringId.ContextMenuTimescales: return "Tijdschalen";  //"Time Scales";
                case RadSchedulerStringId.ContextMenuTimescalesYear: return "Jaar" ;  //"Year";
                case RadSchedulerStringId.ContextMenuTimescalesMonth: return "Maand";
                case RadSchedulerStringId.ContextMenuTimescalesWeek: return "Week";
                case RadSchedulerStringId.ContextMenuTimescalesDay: return "Dag";  //"Day";
                case RadSchedulerStringId.ContextMenuTimescalesHour: return "Hour";
                case RadSchedulerStringId.ContextMenuTimescalesHalfHour: return "30 notulen";
                case RadSchedulerStringId.ContextMenuTimescalesFifteenMinutes: return "15 notulen";

                case RadSchedulerStringId.ErrorProviderWrongAppointmentDates: return "Appointment end time is less or equal to start time!";
                case RadSchedulerStringId.ErrorProviderWrongExceptionDuration: return "Recurrence interval must be greater or equal to appointment duration!";
                case RadSchedulerStringId.ErrorProviderExceptionSameDate: return "Two occurrences of the same series cannot occur on the same day.";
                case RadSchedulerStringId.ErrorProviderExceptionSkipOverDate: return "Recurrence exception cannot skip over a later occurrence of the same appointment.";
                case RadSchedulerStringId.TimeZoneLocal: return "Locale" ;  //"Local";
            }

            return string.Empty;
        }
    }
}
