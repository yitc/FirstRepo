using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class AccPageModel : IModel
    {
        [DisplayName("Id Page")]
        public int idAccPage { get; set; }

        [DisplayName("Dbk.Code")]
        public int idDaily { get; set; }

        [DisplayName("Daily entry")]
        public string codeDaily { get; set; }

        [DisplayName("Page nr")]
        public int numberPage { get; set; }

        [DisplayName("Period")]
        public int periodPage { get; set; }

        [DisplayName("Description")]
        public string descPage { get; set; }

        [DisplayName("Previus Debit Amount")]
        public decimal prevDebAmtPage { get; set; }

        [DisplayName("Previus Credit Amount")]
        public decimal prevCreAmtPage { get; set; }

        [DisplayName("Previus Debit Vat")]
        public decimal prevDVatPage { get; set; }

        [DisplayName("Previus Credit Vat")]
        public decimal prevCVatPage { get; set; }

        [DisplayName("Debit Amount")]
        public decimal amountDebPage { get; set; }

        [DisplayName("Credit Amount")]
        public decimal amountCrePage { get; set; }

        [DisplayName("Debit Vat")]
        public decimal vatDebPage { get; set; }

        [DisplayName("Credit Vat")]
        public decimal vatCrePage { get; set; }

        [DisplayName("Status")]
        public bool statusPage { get; set; }

    }
}