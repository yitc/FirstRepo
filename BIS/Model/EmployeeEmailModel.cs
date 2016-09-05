using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BIS.Model
{
    public class EmployeeEmailModel
    {
        [DisplayName("ID email")]
        public int idEmpEmail { get; set; }

        [DisplayName("ID Employee")]
        public int idEmployee { get; set; }

        [DisplayName("Email")]
        public string email { get; set; }

        [DisplayName("Type")]
        public int? emailType { get; set; }

        public EmployeeEmailModel()
        {
            this.idEmpEmail = 0;
            this.idEmployee = 0;
            this.email = String.Empty;
            this.emailType = 0;
        }

        public EmployeeEmailModel(EmployeeEmailModel sourceModel)
        {
            this.idEmpEmail = sourceModel.idEmpEmail;
            this.idEmployee = sourceModel.idEmployee;
            this.email = sourceModel.email;
            this.emailType = sourceModel.emailType;
        }

        public EmployeeEmailModel ReturnCopy()
        {
            EmployeeEmailModel newItem = new EmployeeEmailModel();

            newItem.idEmpEmail = this.idEmpEmail;
            newItem.idEmployee = this.idEmployee;
            newItem.email = this.email;
            newItem.emailType = this.emailType;

            return newItem;
        }
    }

    public class EmployeeEmailModelComparer : IEqualityComparer<EmployeeEmailModel>
    {
        public bool Equals(EmployeeEmailModel x, EmployeeEmailModel y)
        {

            return (x.idEmpEmail == y.idEmpEmail || x.idEmployee == y.idEmployee || x.email == y.email || x.email == y.email || x.emailType == y.emailType);
        }

        public int GetHashCode(EmployeeEmailModel obj)
        {
            return obj.idEmpEmail.GetHashCode() ^ obj.idEmployee.GetHashCode() ^ obj.email.GetHashCode() ^ obj.emailType.GetHashCode();
        }
    }
    

}