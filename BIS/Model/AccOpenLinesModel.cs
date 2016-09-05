using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BIS.Model
{
    public class AccOpenLinesModel : IModel
    {
        [DisplayName("Id Open Line")]
        public int idOpenLine { get; set; }

        [DisplayName("Id Debit Credit")]
        public string idDebCre { get; set; }

        [DisplayName("Type Open Line")]
        public string typeOpenLine { get; set; }

        [DisplayName("Invoice Open Line")]
        public string invoiceOpenLine { get; set; }

        [DisplayName("Date Open Line")]
        public DateTime dtOpenLine { get; set; }

        [DisplayName("Date Pay Line")]
        public DateTime dtPayOpenLine { get; set; }

        [DisplayName("Description Open Line")]
        public string descOpenLine { get; set; }

        [DisplayName("Debit Open Line")]
        public decimal? debitOpenLine { get; set; }

        [DisplayName("Credit Open Line")]
        public decimal? creditOpenLine { get; set; }

        [DisplayName("Code Cost")]
        public string codeCost { get; set; }

        [DisplayName("Code Arrangement")]
        public string codeArr { get; set; }

        [DisplayName("Id Project")]
        public string idProject { get; set; }

        [DisplayName("Id Pay Condition")]
        public int? idPayCondition { get; set; }

        [DisplayName("Credit Days")]
        public int? creditDays { get; set; }

        [DisplayName("Discaunt Days")]
        public int? discauntDays { get; set; }

        [DisplayName("Period One Lines")]
        public int? periodOnenLines { get; set; }

        [DisplayName("Account")]
        public string account { get; set; }

        [DisplayName("Selected")]
        public bool iselected { get; set; }

        [DisplayName("Reference")]
        public string referencePay { get; set; }

        [DisplayName("Option")]
        public int idOption { get; set; }

        [DisplayName("Iban")]
        public string iban { get; set; }
        [DisplayName("Name")]
        public string name { get; set; }
        [DisplayName("Term")]
        public int term { get; set; }
        public string bookingYear { get; set; }
        public int idSepa { get; set; }
        public bool isFirstWarrningSent { get; set; }
        public DateTime dtFirstWarrning { get; set; }
        public bool isSecondWarrningSent { get; set; }
        public DateTime dtSecondWarrning { get; set; }
        public DateTime dtCreationLine { get; set; }

        public AccOpenLinesModel()
        {

            this.account = String.Empty;
            this.iselected = false;
            this.idPayCondition = 0;
            this.idProject = String.Empty;
            this.codeCost = String.Empty;
            this.idDebCre = String.Empty;
            this.discauntDays = 0;
            this.creditDays = 0;
            this.idPayCondition = 0;
            this.idProject = String.Empty;
            this.codeArr = String.Empty;
            //   this.idProject = String.Empty;
            this.discauntDays = 0;
            this.iselected = false;
            this.referencePay = String.Empty;
            this.creditOpenLine = 0;
            this.debitOpenLine = 0;
            this.periodOnenLines = 0;
            this.dtPayOpenLine = DateTime.Parse("1900-01-01");
            this.dtOpenLine = DateTime.Parse("1900-01-01");
            this.idOption = 0;
            this.typeOpenLine = String.Empty;
            this.invoiceOpenLine = String.Empty;
            this.iban = String.Empty;
            this.name = String.Empty;
            this.term = 0;
            this.bookingYear = DateTime.Now.Year.ToString();
            this.idSepa = 0;
            this.isFirstWarrningSent = false;
            this.dtFirstWarrning = DateTime.Parse("1900-01-01");
            this.isSecondWarrningSent = false;
            this.dtSecondWarrning = DateTime.Parse("1900-01-01");
            this.dtCreationLine = DateTime.Parse("1900-01-01");
        }
    }


    public class AccOpenLinesReportModel
    {
        [DisplayName("Print")]
        public bool chk { get; set; }

        [DisplayName("Id Open Line")]
        public int idOpenLine { get; set; }

        [DisplayName("Person Id")]
        public int idContPers { get; set; }

        [DisplayName("Client Id")]
        public int idClient { get; set; }


        [DisplayName("Id Debit Credit")]
        public string idDebCre { get; set; }

        [DisplayName("Name")]
        public string name { get; set; }

        [DisplayName("Type Open Line")]
        public string typeOpenLine { get; set; }

        [DisplayName("Invoice Open Line")]
        public string invoiceOpenLine { get; set; }

        [DisplayName("Date Open Line")]
        public DateTime dtOpenLine { get; set; }

        [DisplayName("Date Pay Line")]
        public DateTime dtPayOpenLine { get; set; }

        [DisplayName("Description Open Line")]
        public string descOpenLine { get; set; }

        [DisplayName("Debit Open Line")]
        public decimal? debitOpenLine { get; set; }

        [DisplayName("Credit Open Line")]
        public decimal? creditOpenLine { get; set; }

        [DisplayName("Days")]
        public int days { get; set; }

        [DisplayName("Difference")]
        public decimal dif { get; set; }

        [DisplayName("Term")]
        public int term { get; set; }
        public string bookingYear { get; set; }

        public bool isInvoicing { get; set; }

        public string email { get; set; }

        [DisplayName("First warrning")]
        public bool isFirstWarrningSent { get; set; }

        [DisplayName("Date first warrning")]
        public DateTime dtFirstWarrning { get; set; }

        [DisplayName("Second warrning")]
        public bool isSecondWarrningSent { get; set; }

        [DisplayName("Date second warrning")]
        public DateTime dtSecondWarrning { get; set; }

        [DisplayName("Date line")]
        public DateTime dtCreationLine { get; set; }

        [DisplayName("Debitor")]
        public bool isDebitor { get; set; }

        [DisplayName("Creditor")]
        public bool isCreditor { get; set; }

        [DisplayName("Debitor/Creditor")]
        public string DebitorCreditor { get; set; }




        public AccOpenLinesReportModel()
        {
            this.chk = false;
            this.idOpenLine = 0;
            this.idContPers = 0;
            this.idClient = 0;
            this.idDebCre = String.Empty;
            this.name = String.Empty;
            this.typeOpenLine = String.Empty;
            this.invoiceOpenLine = String.Empty;
            this.dtOpenLine = DateTime.Parse("1900-01-01");
            this.dtPayOpenLine = DateTime.Parse("1900-01-01");
            this.descOpenLine = String.Empty;
            this.debitOpenLine = 0;
            this.creditOpenLine = 0;
            this.days = 0;
            this.dif = 0;
            this.term = 0;
            this.bookingYear = DateTime.Now.Year.ToString();
            this.isFirstWarrningSent = false;
            this.dtFirstWarrning = DateTime.Parse("1900-01-01");
            this.isSecondWarrningSent = false;
            this.dtSecondWarrning = DateTime.Parse("1900-01-01");
            this.dtCreationLine = DateTime.Parse("1900-01-01");
            this.isInvoicing = false;
            this.email = String.Empty;
            this.isDebitor = false;
            this.isCreditor = false;
            this.DebitorCreditor = String.Empty;

        }
    }
    public class OpenLinesWithDates : IModel
    {
        [DisplayName("Print")]
        public bool chk { get; set; }

        [DisplayName("Id Open Line")]
        public int idOpenLine { get; set; }

        [DisplayName("Person Id")]
        public int idContPers { get; set; }

        [DisplayName("Client Id")]
        public int idClient { get; set; }


        [DisplayName("Id Debit Credit")]
        public string idDebCre { get; set; }

        [DisplayName("Name")]
        public string name { get; set; }

        [DisplayName("Type Open Line")]
        public string typeOpenLine { get; set; }

        [DisplayName("Invoice Open Line")]
        public string invoiceOpenLine { get; set; }

        [DisplayName("Date Open Line")]
        public DateTime dtOpenLine { get; set; }

        [DisplayName("Date Pay Line")]
        public DateTime dtPayOpenLine { get; set; }

        [DisplayName("Description Open Line")]
        public string descOpenLine { get; set; }

        [DisplayName("Debit Open Line")]
        public decimal? debitOpenLine { get; set; }

        [DisplayName("Credit Open Line")]
        public decimal? creditOpenLine { get; set; }

        [DisplayName("Days")]
        public int days { get; set; }

        [DisplayName("Difference")]
        public decimal dif { get; set; }

        [DisplayName("Term")]
        public int term { get; set; }
        public string bookingYear { get; set; }

        public bool isInvoicing { get; set; }

        public string email { get; set; }

        [DisplayName("First warrning")]
        public bool isFirstWarrningSent { get; set; }

        [DisplayName("Date first warrning")]
        public DateTime dtFirstWarrning { get; set; }

        [DisplayName("Second warrning")]
        public bool isSecondWarrningSent { get; set; }

        [DisplayName("Date second warrning")]
        public DateTime dtSecondWarrning { get; set; }

        [DisplayName("Date line")]
        public DateTime dtCreationLine { get; set; }

        [DisplayName("Debitor")]
        public bool isDebitor { get; set; }

        [DisplayName("Creditor")]
        public bool isCreditor { get; set; }

        [DisplayName("DaySelected")]
        public DateTime DaySelected { get; set; }

        [DisplayName("DebCre")]
        public string DebCre { get; set; }

        [DisplayName("UserName")]
        public string UserName { get; set; }
        public OpenLinesWithDates()
        {
            this.chk = false;
            this.idOpenLine = 0;
            this.idContPers = 0;
            this.idClient = 0;
            this.idDebCre = String.Empty;
            this.name = String.Empty;
            this.typeOpenLine = String.Empty;
            this.invoiceOpenLine = String.Empty;
            this.dtOpenLine = DateTime.Parse("1900-01-01");
            this.dtPayOpenLine = DateTime.Parse("1900-01-01");
            this.descOpenLine = String.Empty;
            this.debitOpenLine = 0;
            this.creditOpenLine = 0;
            this.days = 0;
            this.dif = 0;
            this.term = 0;
            this.bookingYear = DateTime.Now.Year.ToString();
            this.isFirstWarrningSent = false;
            this.dtFirstWarrning = DateTime.Parse("1900-01-01");
            this.isSecondWarrningSent = false;
            this.dtSecondWarrning = DateTime.Parse("1900-01-01");
            this.dtCreationLine = DateTime.Parse("1900-01-01");
            this.isInvoicing = false;
            this.email = String.Empty;
            this.isDebitor = false;
            this.isCreditor = false;
            this.DaySelected = DateTime.Now;
            this.DebCre = String.Empty;
            this.UserName = String.Empty;

        }
    }
}
