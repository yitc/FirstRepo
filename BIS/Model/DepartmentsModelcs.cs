using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BIS.Model
{
    public class DepartmentsModel:IModel
    {
        [DisplayName("ID department")]
        public int idDepartment { get; set; }

        [DisplayName("Department")]
        public string nameDepartment { get; set; }

        [DisplayName("Telephone")]
        public string telephoneDepartment { get; set; }

        [DisplayName("Email")]
        public string emailDepartment { get; set; }
    }
}