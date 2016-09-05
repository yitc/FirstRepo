using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class VoluntaryReasonInModel : IModel
    {
        //Za lookup
        [DisplayName("Add/Not")]
        public bool select { get; set; }

        [DisplayName("Id reason In")]
        public int idReasonIn { get; set; }

        [DisplayName("Reason In")]
        public string nameReasonIn { get; set; }


        public VoluntaryReasonInModel()
        {
            this.select = false;
            this.idReasonIn = 0;
            this.nameReasonIn = String.Empty;
        }

    }

    //ReasonIn Preselection
    public class VoluntaryContPersReasonInModel:IModel
    {
        [DisplayName("Number")]
        public int idContPers { get; set; }

        [DisplayName("Title")]
        public string title { get; set; }

        [DisplayName("First name")]
        public string firstname { get; set; }

        [DisplayName("Reason In")]
        public string nameReasonIn { get; set; }       

        [DisplayName("Email")]
        public string email { get; set; }

        [DisplayName("Phone")]
        public string numberTel { get; set; }

        [DisplayName("Age")]
        public string Age { get; set; }

        [DisplayName("Last name")]
        public string lastname { get; set; }

        [DisplayName("Middle name")]
        public string midname { get; set; }

        [DisplayName("Function")]
        public string function { get; set; }
               

        public VoluntaryContPersReasonInModel()
        {
            this.idContPers = 0;
            this.title = String.Empty;
            this.firstname = String.Empty;
            this.nameReasonIn = String.Empty;           
            this.email = String.Empty;
            this.numberTel = String.Empty;
            this.Age = String.Empty;          
            this.lastname = String.Empty;
            this.midname = String.Empty;
            this.function = String.Empty;
          
        }
    }




    public class VoluntaryReasonOutModel : IModel
    {

        [DisplayName("Add/Not")]
        public Boolean select { get; set; }

        [DisplayName("Id reason Out")]
        public int idReasonOut { get; set; }

        [DisplayName("Reason Out")]
        public string nameReasonOut { get; set; }


        public VoluntaryReasonOutModel()
        {
            this.select = false;
            this.idReasonOut = 0;
            this.nameReasonOut = String.Empty;
        }


    }
}