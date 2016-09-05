using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BIS.Model
{
    public class EmployeeModel : IModel
    {
        [DisplayName("ID Employee")]
        public int idEmployee { get; set; }

         [DisplayName("First name")]
        public string firstNameEmployee { get; set; }

        [DisplayName("Initials")]
        public string initialsEmployee { get; set; }

         [DisplayName("ID Title")]
        public int? titleEmployee { get; set; }

         [DisplayName("Title")]
         public string nameTitle { get; set; }

        [DisplayName("Middle name")]
        public string midNameEmployee { get; set; }

        [DisplayName("Last name")]
        public string lastNameEmployee { get; set; }

        [DisplayName("Maiden name")]
        public string maidenEmployee { get; set; }

        [DisplayName("ID Gender")]

        public decimal? genderEmployee { get; set; }

        [DisplayName("Gender")]
        public string nameGender { get; set; }

         [DisplayName("Address")]
        public string addressEmployee { get; set; }

        [DisplayName("House nr")]
        public string houseNumberEmployee { get; set; }

        [DisplayName("Extension")]
        public string extensionEmployee { get; set; }

        [DisplayName("Zip code")]
        public string zipCodeEmployee { get; set; }

        [DisplayName("City")]
        public string cityEmployee { get; set; }

        [DisplayName("ID country")]
        public int? idCountry { get; set; }

        [DisplayName("Country")]
        public string internationalCode { get; set; }

        [DisplayName("Birth date")]
        public DateTime dtBirthDateEmployee { get; set; }

        [DisplayName("Married")]
        public bool isMariedEmployee { get; set; }

        [DisplayName("IdentBSN")]
        public string isentBsnEmploee { get; set; }

        [DisplayName("Bic")]
        public string bicEmployee { get; set; }

        [DisplayName("Iban")]
        public string ibanEmployee { get; set; }

        [DisplayName("Emergency person")]
        public string emergencyPersonEmployee { get; set; }

        [DisplayName("Emergency telephone")]
        public string emergencyTelEmployee { get; set; }

        [DisplayName("Hire date")]
        public DateTime dtHireDateEmployee { get; set; }

        [DisplayName("Contract number")]
        public string contractNumberEmployee { get; set; }

        [DisplayName("ID Department")]
        public int? Department { get; set; }

        [DisplayName("Department")]
        public string nameDepartment { get; set; }

        [DisplayName("ID function")]
        public int? Function { get; set; }

        [DisplayName("Function")]
        public string nameFunction { get; set; }

        [DisplayName("ID wish function")]
        public int? WishFunction { get; set; }

        [DisplayName("Wish function")]
        public string nameWishFunction { get; set; }

        [DisplayName("ID Status")]
        public int? statusEmployee { get; set; }

        [DisplayName("Status")]
        public string descriptionEmployee { get; set; }

        [DisplayName("Image")]
        public string imageEmployee { get; set; }

        [DisplayName("Application user")]
        public bool isAplicationUser { get; set; }

        public EmployeeModel()
        {            
            this.idEmployee = -1;
            this.firstNameEmployee = String.Empty;
            this.initialsEmployee = String.Empty;
            this.titleEmployee = 0;
            this.nameTitle = String.Empty;
            this.midNameEmployee = String.Empty;
            this.lastNameEmployee = String.Empty;
            this.maidenEmployee = String.Empty;
            this.genderEmployee = 0;
            this.nameGender = String.Empty;
            this.addressEmployee = String.Empty;
            this.houseNumberEmployee = String.Empty;
            this.extensionEmployee = String.Empty;
            this.zipCodeEmployee = String.Empty;
            this.cityEmployee = String.Empty;
            this.idCountry = 0;
            this.internationalCode = String.Empty;
            this.dtBirthDateEmployee = DateTime.Now;
            this.isMariedEmployee = false;
            this.isentBsnEmploee = String.Empty;
            this.bicEmployee = String.Empty;
            this.ibanEmployee = String.Empty;
            this.emergencyPersonEmployee = String.Empty;
            this.emergencyTelEmployee = String.Empty;
            this.dtHireDateEmployee = DateTime.Now;
            this.contractNumberEmployee = String.Empty;
            this.Department = 0;
            this.nameDepartment = String.Empty;
            this.Function = 0;
            this.nameFunction = String.Empty;
            this.WishFunction = 0;
            this.nameWishFunction = String.Empty;
            this.statusEmployee = 0;
            this.descriptionEmployee = String.Empty;
            this.isAplicationUser = false;
            this.imageEmployee = String.Empty;
        }

        public EmployeeModel(EmployeeModel model)
        {
            this.idEmployee = model.idEmployee;
            this.firstNameEmployee = model.firstNameEmployee;
            this.initialsEmployee = model.initialsEmployee;
            this.titleEmployee = model.titleEmployee;
            this.nameTitle = model.nameTitle;
            this.midNameEmployee = model.midNameEmployee;
            this.lastNameEmployee = model.lastNameEmployee;
            this.maidenEmployee = model.maidenEmployee;
            this.genderEmployee = model.genderEmployee;
            this.nameGender = model.nameGender;
            this.addressEmployee = model.addressEmployee;
            this.houseNumberEmployee = model.houseNumberEmployee;
            this.extensionEmployee = model.extensionEmployee;
            this.zipCodeEmployee = model.zipCodeEmployee;
            this.cityEmployee = model.cityEmployee;
            this.idCountry = model.idCountry;
            this.internationalCode = model.internationalCode;
            this.dtBirthDateEmployee = model.dtBirthDateEmployee;
            this.isMariedEmployee = model.isMariedEmployee;
            this.isentBsnEmploee = model.isentBsnEmploee;
            this.bicEmployee = model.bicEmployee;
            this.ibanEmployee = model.ibanEmployee;
            this.emergencyPersonEmployee = model.emergencyPersonEmployee;
            this.emergencyTelEmployee = model.emergencyTelEmployee;
            this.dtHireDateEmployee = model.dtHireDateEmployee;
            this.contractNumberEmployee = model.contractNumberEmployee;
            this.Department = model.Department;
            this.nameDepartment = model.nameDepartment;
            this.Function = model.Function;
            this.nameFunction = model.nameFunction;
            this.WishFunction = model.WishFunction;
            this.nameWishFunction = model.nameWishFunction;
            this.statusEmployee = model.statusEmployee;
            this.descriptionEmployee = model.descriptionEmployee;
            this.isAplicationUser = model.isAplicationUser;
            this.imageEmployee = model.imageEmployee;
        }

        public bool CompareWith(EmployeeModel compareModel)
        {
            // compare all fields. if there is a change return true
            bool returnResult = false;

            if (this.idEmployee != compareModel.idEmployee)
                returnResult = true;

            if(this.firstNameEmployee != compareModel.firstNameEmployee)
                returnResult = true;

            if(this.initialsEmployee != compareModel.initialsEmployee)
                returnResult = true;

            if(this.titleEmployee != compareModel.titleEmployee)
                returnResult = true;

            if(this.midNameEmployee != compareModel.midNameEmployee)
                returnResult = true;

            if(this.lastNameEmployee != compareModel.lastNameEmployee)
                returnResult = true;

            if(this.maidenEmployee != compareModel.maidenEmployee)
                returnResult = true;

            if(this.genderEmployee != compareModel.genderEmployee)
                returnResult = true;


            if(this.dtBirthDateEmployee.ToShortDateString() != compareModel.dtBirthDateEmployee.ToShortDateString())
                returnResult = true;

            if(this.isMariedEmployee != compareModel.isMariedEmployee)
                returnResult = true;

            if(this.isentBsnEmploee != compareModel.isentBsnEmploee)
                returnResult = true;

            if(this.bicEmployee != compareModel.bicEmployee)
                returnResult = true;

            if(this.ibanEmployee != compareModel.ibanEmployee)
                returnResult = true;

            if(this.emergencyPersonEmployee != compareModel.emergencyPersonEmployee)
                returnResult = true;

            if(this.emergencyTelEmployee != compareModel.emergencyTelEmployee)
                returnResult = true;

            if(this.dtHireDateEmployee.ToShortDateString() != compareModel.dtHireDateEmployee.ToShortDateString())
                returnResult = true;

            if(this.contractNumberEmployee != compareModel.contractNumberEmployee)
                returnResult = true;

            if(this.Department != compareModel.Department)
                returnResult = true;

            if(this.Function != compareModel.Function)
                returnResult = true;

            if(this.WishFunction != compareModel.WishFunction)
                returnResult = true;

            if(this.statusEmployee != compareModel.statusEmployee)
                returnResult = true;

            if(this.descriptionEmployee != compareModel.descriptionEmployee)
                returnResult = true;

            if(this.imageEmployee != compareModel.imageEmployee)
                returnResult = true;

            if(this.isAplicationUser != compareModel.isAplicationUser)
                returnResult = true;

            return returnResult;
        }

        public void CopyValues(EmployeeModel source)
        {
            this.idEmployee = source.idEmployee;
            this.firstNameEmployee = source.firstNameEmployee;
            this.initialsEmployee = source.initialsEmployee;
            this.titleEmployee = source.titleEmployee;
            this.nameTitle = source.nameTitle;
            this.midNameEmployee = source.midNameEmployee;
            this.lastNameEmployee = source.lastNameEmployee;
            this.maidenEmployee = source.maidenEmployee;
            this.genderEmployee = source.genderEmployee;
            this.nameGender = source.nameGender;
            this.addressEmployee = source.addressEmployee;
            this.houseNumberEmployee = source.houseNumberEmployee;
            this.extensionEmployee = source.extensionEmployee;
            this.zipCodeEmployee = source.zipCodeEmployee;
            this.cityEmployee = source.cityEmployee;
            this.idCountry = source.idCountry;
            this.internationalCode = source.internationalCode;
            this.dtBirthDateEmployee = source.dtBirthDateEmployee;
            this.isMariedEmployee = source.isMariedEmployee;
            this.isentBsnEmploee = source.isentBsnEmploee;
            this.bicEmployee = source.bicEmployee;
            this.ibanEmployee = source.ibanEmployee;
            this.emergencyPersonEmployee = source.emergencyPersonEmployee;
            this.emergencyTelEmployee = source.emergencyTelEmployee;
            this.dtHireDateEmployee = source.dtHireDateEmployee;
            this.contractNumberEmployee = source.contractNumberEmployee;
            this.Department = source.Department;
            this.nameDepartment = source.nameDepartment;
            this.Function = source.Function;
            this.nameFunction = source.nameFunction;
            this.WishFunction = source.WishFunction;
            this.nameWishFunction = source.nameWishFunction;
            this.statusEmployee = source.statusEmployee;
            this.descriptionEmployee = source.descriptionEmployee;
            this.isAplicationUser = source.isAplicationUser;
            this.imageEmployee = source.imageEmployee;
        }

    }
}