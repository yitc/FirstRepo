using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{

    public class PriceListArticlesModel : INotifyPropertyChanged
    {        
        public bool isDirty = false;

        [DisplayName("ID pricelistArticle")]
        public int idPriceListArticle { get; set; }

        [DisplayName("ID pricelist")]
        public int idPriceList { get; set; }

        private int idClient;

        [DisplayName("ID Client")]
        public int IdClient
        {
            get { return idClient; }
            set
            {
                if (value != idClient)
                    isDirty = true;

                idClient = value;
                InvokePropertyChanged(new PropertyChangedEventArgs("IdClient"));
            }
        }

        [DisplayName("Client")]
        public string nameClient { get; set; }


        private string idArticle;

        [DisplayName("Article code")]
        public string IdArticle
        {
            get { return idArticle; }
            set
            {
                if (value != idArticle )
                    isDirty = true;

                idArticle = value;
                InvokePropertyChanged(new PropertyChangedEventArgs("IdArticle"));
            }
        }


        [DisplayName("Article name")]
        public string nameArtical { get; set; }


        private int nrArticle;

        [DisplayName("Nr. article")]
        public int NrArticle
        {
            get { return nrArticle; }
            set
            {
                if (value != nrArticle)
                    isDirty = true;

                nrArticle = value;
                InvokePropertyChanged(new PropertyChangedEventArgs("NrArticle"));
            }
        }

        [DisplayName("Quantity")]
        public int quantity { get; set; }

        private decimal pricePerArticle { get; set; }

        [DisplayName("Article purchase price")]
        public decimal PricePerArticle
        {
            get { return pricePerArticle; }
            set
            {
                if (value != pricePerArticle)
                    isDirty = true;

                pricePerArticle = value;
                InvokePropertyChanged(new PropertyChangedEventArgs("PricePerArticle"));
            }
        }

        
        private decimal pricePerQuantity { get; set; }

        [DisplayName("Nr")]
        public decimal PricePerQuantity
        {
            get { return pricePerQuantity; }
            set
            {
                if (value != pricePerQuantity)
                    isDirty = true;

                pricePerQuantity = value;
                InvokePropertyChanged(new PropertyChangedEventArgs("PricePerQuantity"));
            }
        }

        [DisplayName("Calculation price")]
        public decimal priceTotal { get; set; }


        private decimal commission { get; set; }

        [DisplayName("Commission")]
        public decimal Commission
        {
            get { return commission; }
            set
            {
                if (value != commission)
                    isDirty = true;

                commission = value;
                InvokePropertyChanged(new PropertyChangedEventArgs("Commission"));
            }
        }
        
        private DateTime dtFrom { get; set; }

        [DisplayName("Date from")]
        public DateTime DtFrom
        {
            get { return dtFrom; }
            set
            {
                if (value != dtFrom)
                    isDirty = true;

                dtFrom = value;
                InvokePropertyChanged(new PropertyChangedEventArgs("DtFrom"));
            }
        }

        private DateTime dtTo { get; set; }

        [DisplayName("Date to")]
        public DateTime DtTo
        {
            get { return dtTo; }
            set
            {
                if (value != dtTo)
                    isDirty = true;

                dtTo = value;
                InvokePropertyChanged(new PropertyChangedEventArgs("DtTo"));
            }
        }

        private int nrDays { get; set; }

        [DisplayName("Nr. days")]
        public int NrDays
        {
            get { return nrDays; }
            set
            {
                if (value != nrDays)
                    isDirty = true;

                nrDays = value;
                InvokePropertyChanged(new PropertyChangedEventArgs("NrDays"));
            }
        }

        private bool isExtra { get; set; }

        [DisplayName("Extra")]
        public Boolean IsExtra
        {
            get { return isExtra; }
            set
            {
                if (value != isExtra)
                    isDirty = true;

                isExtra = value;
                InvokePropertyChanged(new PropertyChangedEventArgs("IsExtra"));
            }
        }
        
        private bool isAway { get; set; }

        [DisplayName("Away")]
        public Boolean IsAway
        {
            get { return isAway; }
            set
            {
                if (value != isAway)
                    isDirty = true;

                isAway = value;
                InvokePropertyChanged(new PropertyChangedEventArgs("IsAway"));
            }
        }
        
        private bool isBack { get; set; }

        [DisplayName("Back")]
        public Boolean IsBack
        {
            get { return isBack; }
            set
            {
                if (value != isBack)
                    isDirty = true;

                isBack = value;
                InvokePropertyChanged(new PropertyChangedEventArgs("IsBack"));
            }
        }
       
        private bool isAccomodation { get; set; }

        [DisplayName("Accomodation")]
        public Boolean IsAccomodation
        {
            get { return isAccomodation; }
            set
            {
                if (value != isAccomodation)
                    isDirty = true;

                isAccomodation = value;
                InvokePropertyChanged(new PropertyChangedEventArgs("IsAccomodation"));
            }
        }

        private bool isNotInAccompaniment { get; set; }

        [DisplayName("Not in acompaniment")]
        public Boolean IsNotInAccompaniment
        {
            get { return isNotInAccompaniment; }
            set
            {
                if (value != isNotInAccompaniment)
                    isDirty = true;

                isNotInAccompaniment = value;
                InvokePropertyChanged(new PropertyChangedEventArgs("IsNotInAccompaniment"));
            }
        }


        private bool isNotForTraveler { get; set; }

        [DisplayName("Not for travelers")]
        public Boolean IsNotForTraveler
        {
            get { return isNotForTraveler; }
            set
            {
                if (value != isNotForTraveler)
                    isDirty = true;

                isNotForTraveler = value;
                InvokePropertyChanged(new PropertyChangedEventArgs("IsNotForTraveler"));
            }
        }

         

        [DisplayName("ID Creator user")]
        public int idUserCreated { get; set; }

        [DisplayName("Creator user")]
        public string nameUserCreated { get; set; }

        [DisplayName("Creation date")]
        public DateTime dtUserCreated { get; set; }

        [DisplayName("ID Modified user")]
        public int idUserModified { get; set; }

        [DisplayName("Modified user")]
        public string nameUserModified { get; set; }

        [DisplayName("Modification date")]
        public DateTime dtUserModified { get; set; }

        public PriceListArticlesModel()
        {
            this.idPriceListArticle = 0;
            this.idPriceList = 0;
            this.idArticle = String.Empty;
            this.nameArtical = String.Empty;
            this.idClient = 0;
            this.nameClient = String.Empty;
            this.nrArticle = 0;
            this.quantity = 0;
            this.pricePerArticle = 0;
            this.pricePerQuantity = 0;
            this.priceTotal = 0;
            this.dtFrom = DateTime.Now;
            this.dtTo = DateTime.Now;
            this.nrDays = 0;
            this.commission = 0;
            this.isExtra = false;
            this.isAway = false;
            this.isAccomodation = false;
            this.isBack = false;
            this.isNotInAccompaniment = false;
            this.isNotForTraveler = false;
            this.idUserCreated = 0;
            this.nameUserCreated = String.Empty;
            this.dtUserCreated = DateTime.Now;
            this.idUserModified = 0;
            this.nameUserModified = String.Empty;
            this.dtUserModified = DateTime.Now;

        }

        public PriceListArticlesModel(PriceListArticlesModel model)
        {
            this.idPriceListArticle = model.idPriceListArticle;
            this.idPriceList = model.idPriceList;
            this.idArticle = model.IdArticle;
            this.nameArtical = model.nameArtical;
            this.idClient = model.idClient;
            this.nameClient = model.nameClient;
            this.nrArticle = model.nrArticle;
            this.quantity = model.quantity;
            this.pricePerArticle = model.pricePerArticle;
            this.pricePerQuantity = model.pricePerQuantity;
            this.priceTotal = model.priceTotal;
            this.dtFrom = model.dtFrom;
            this.dtTo = model.dtTo;
            this.nrDays = model.nrDays;
            this.commission = model.commission;
            this.isExtra = model.isExtra;
            this.isAway = model.isAway;
            this.isAccomodation = model.isAccomodation;
            this.isBack = model.isBack;            
            this.isNotInAccompaniment = model.isNotInAccompaniment;
            this.isNotForTraveler = model.isNotForTraveler;
            this.idUserCreated = model.idUserCreated;
            this.nameUserCreated = model.nameUserCreated;
            this.dtUserCreated = model.dtUserCreated;
            this.idUserModified = model.idUserModified;
            this.nameUserModified = model.nameUserModified;
            this.dtUserModified = model.dtUserCreated;

        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void InvokePropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, e);
        }

     
      


    }
    public class PriceListArticlesControlModel
    {
        public int idPricelist { get; set; }
        public int idClient { get; set; }
        public int idArrangement { get; set; }
        public string idArticle { get; set; }
        public string codeArtical { get; set; }
        public int nrArticle { get; set; }
        public int quantity { get; set; }
        public decimal priceperarticle { get; set; }
        public decimal priceperquantity { get; set; }
        public decimal priceTotal { get; set; }
        public string nameArtical { get; set; }
    }
}