using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections;

namespace BIS.Model
{
    public class PersonTelModel 
    {
        [DisplayName("ID tel")]
        public int idTel { get; set; }
        
        [DisplayName("Person")]
        public int idContPers { get; set; }

        [DisplayName("Telephone")]
        public string numberTel { get; set; }

        [DisplayName("Default")]
        public bool isDefaultTel { get; set; }

        [DisplayName("Description")]
        public string descriptionTel { get; set; }

        [DisplayName("ID tel type")]
        public int? idTelType { get; set; }

        public PersonTelModel()
        {
            this.idTel = 0;
            this.idContPers = 0;
            this.numberTel = String.Empty;
            this.isDefaultTel = false;
            this.descriptionTel = String.Empty;
            this.idTelType = 0;

        }
        public PersonTelModel(PersonTelModel sourceModel)
        {
            this.idTel = sourceModel.idTel;
            this.idContPers = sourceModel.idContPers;
            this.numberTel = sourceModel.numberTel;
            this.isDefaultTel = sourceModel.isDefaultTel;
            this.descriptionTel = sourceModel.descriptionTel;
            this.idTelType = sourceModel.idTelType;
        }

        public  PersonTelModel ReturnCopy()
        {
            PersonTelModel newItem = new PersonTelModel();

            newItem.idTel = this.idTel;
            newItem.idContPers = this.idContPers;
            newItem.numberTel = this.numberTel;
            newItem.isDefaultTel = this.isDefaultTel;
            newItem.descriptionTel =  this.descriptionTel;
            newItem.idTelType = this.idTelType;

            return newItem;
        }

    }

    // insert, modify changes
    public class PersonTelModelComparer : IEqualityComparer<PersonTelModel>
    {
        public bool Equals (PersonTelModel x, PersonTelModel y)
        {            
            return (x.idTel == y.idTel || x.idContPers == y.idContPers || x.numberTel == y.numberTel || x.isDefaultTel == y.isDefaultTel || x.descriptionTel == y.descriptionTel || x.idTelType == y.idTelType);
        }

        public int GetHashCode(PersonTelModel obj)
        {
            return obj.idTel.GetHashCode() ^ obj.idContPers.GetHashCode() ^ obj.numberTel.GetHashCode() ^ obj.isDefaultTel.GetHashCode() ^ obj.descriptionTel.GetHashCode() ^ obj.idTelType.GetHashCode();
        }
    }
    // Vraca u codu insert, modify, delete changes. ali kako mi brisemo odmah iz liste nema potrebe da vraca delete
    //var DifferencesList = persTel.Where(x => !persTelFirst.Any(x1 => x1.numberTel == x.numberTel))
    //        .Union(persTelFirst.Where(x => !persTel.Any(x1 => x1.numberTel == x.numberTel)));
}