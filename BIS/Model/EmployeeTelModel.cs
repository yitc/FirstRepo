using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BIS.Model
{
    public class EmployeeTelModel

    {
        [DisplayName("ID Tel")]
        public int idtelemp { get; set; }

        [DisplayName("ID Employee")]
        public int idEmployee { get; set; }

        [DisplayName("Telephone")]
        public string telephone { get; set; }

        [DisplayName("Type")]
        public int? telephoneType { get; set; }

        [DisplayName("Default")]
        public bool isDefault { get; set; }

        [DisplayName("Description")]
        public string description { get; set; }

        public EmployeeTelModel()
        {
            this.idtelemp = 0;
            this.idEmployee = 0;
            this.telephone = String.Empty;
            this.telephoneType = 0;
            this.isDefault = false;
            this.description = String.Empty;
        }
        public EmployeeTelModel(EmployeeTelModel sourceModel)
        {
            this.idtelemp = sourceModel.idtelemp;
            this.idEmployee = sourceModel.idEmployee;
            this.telephone = sourceModel.telephone;
            this.telephoneType = sourceModel.telephoneType;
            this.isDefault = sourceModel.isDefault;
            this.description = sourceModel.description;
        }

        public EmployeeTelModel ReturnCopy()
        {
            EmployeeTelModel newItem = new EmployeeTelModel();

            newItem.idtelemp = this.idtelemp;
            newItem.idEmployee = this.idEmployee;
            newItem.telephone = this.telephone;
            newItem.telephoneType = this.telephoneType;
            newItem.isDefault = this.isDefault;
            newItem.description = this.description;            

            return newItem;
        }  
    }

    public class EmployeeTelModelComparer : IEqualityComparer<EmployeeTelModel>
    {
        public bool Equals(EmployeeTelModel x, EmployeeTelModel y)
        {

            return (x.idtelemp == y.idtelemp || x.idEmployee == y.idEmployee || x.telephone == y.telephone || x.telephoneType == y.telephoneType || x.isDefault == y.isDefault || x.description == y.description );
        }

        public int GetHashCode(EmployeeTelModel obj)
        {
            return obj.idtelemp.GetHashCode() ^ obj.idEmployee.GetHashCode() ^ obj.telephone.GetHashCode() ^ obj.telephoneType.GetHashCode() ^ obj.isDefault.GetHashCode() ^ obj.description.GetHashCode();
        }
    }
}