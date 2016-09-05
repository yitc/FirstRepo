using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
  public  class UsersAllModel : IModel
    {
        [DisplayName("ID")]
        public int idUser { get; set; }

        public int? idRole { get; set; }

        [DisplayName("Role")]
        public string nameRole { get; set; }

        [DisplayName("User name")]
        public string username { get; set; }

        [DisplayName("Password")]
        public string password { get; set; }

        [DisplayName("User full name")]
        public string nameUser { get; set; }

        [DisplayName("Is user login")]
        public bool? isUserLogin { get; set; }

        [DisplayName("Log on time")]
        public DateTime? dtUserLogin { get; set; }

        [DisplayName("Log off time")]
        public DateTime? dtUserLogout { get; set; }

        public int? idEmployee { get; set; }

        public int? idCompany { get; set; }

        [DisplayName("Employee")]
        public string nameEmployee { get; set; }

       
        [DisplayName("Manager")]
        public bool? isUserManager { get; set; }


        [DisplayName("Date of changing password")]
        public DateTime? dtPassChanged { get; set; }

        [DisplayName("Password duration days")]
        public decimal? numDaysPassValid { get; set; }

        [DisplayName("Number of warning days")]
        public decimal? numDaysStartWarn { get; set; }

        [DisplayName("Email")]
        public string emailUser { get; set; }

        [DisplayName("Language code")]
        public string lngUser { get; set; }

        //dodato zbog accountinga
        [DisplayName("Acount user")]
        public bool isAccountUser { get; set; }

        [DisplayName("Not Active")]
        public bool? isNotActive { get; set; }

        [DisplayName("First start app")]
        public bool? isFirstTimeStarted { get; set; }

        [DisplayName("Allow finish calculation")]
        public bool? isFinishCalculation { get; set; }

        [DisplayName("Dont see med/vol")]
        public bool? isDontSeeMedVol { get; set; }

        [DisplayName("Account manager")]
        public bool? isAccountManager { get; set; }

        // isDontSeeMedVol is dont see Medical and Memo. 

      public UsersAllModel()
        {
            this.idUser = 0;
            this.idRole = 0;
            this.nameRole = String.Empty;
            this.username = String.Empty;
            this.password = String.Empty;
            this.nameUser = String.Empty;
            this.isUserLogin = false;
            this.dtUserLogin = DateTime.Now;
            this.dtUserLogout = DateTime.Now;
            this.idEmployee = 0;
            this.idCompany = 0;
            this.nameEmployee = String.Empty;
            this.isUserManager = false;
            this.dtPassChanged = DateTime.Now;
            this.numDaysPassValid = 0;
            this.numDaysStartWarn = 0;
            this.emailUser = String.Empty;
            this.lngUser = String.Empty;
            this.isAccountUser = false;
            this.isNotActive = false;
            this.isFirstTimeStarted = false;
            this.isFinishCalculation = false;
            this.isDontSeeMedVol = false;
            this.isAccountManager = false;
        }
    }    
}
