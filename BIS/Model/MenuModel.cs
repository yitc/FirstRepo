using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class MenuModel
    {
        [DisplayName("ID menu")]
        public int idMenu { get; set; }

        [DisplayName("ID menu superior")]
        public int? idMenuSuperior { get; set; }

        [DisplayName("Menu")]
        public string nameMenu { get; set; }

        [DisplayName("Language code")]
        public string lngCode { get; set; }

        [DisplayName("Sort")]
        public int? sortMenu { get; set; }

        
        public string initMethod { get; set; }

        public string onClickMenu { get; set; }

        [DisplayName("ID security")]
        public int? idSecurity { get; set; }

        [DisplayName("ID form")]
        public int? idForm { get; set; }

        [DisplayName("Form")]
        public string nameForm { get; set; }

        [DisplayName("Image")]
        public string imageMenu { get; set; }

        public string imageNew { get; set; }

        public string imageDelete { get; set; }

        public List<MenuModel> subMenus { get; set; }

        public bool isGrid { get; set; }

        public MenuModel()
        {
            this.idMenu = 0;
            this.idMenuSuperior = 0;
            this.nameMenu = String.Empty;
            this.lngCode = String.Empty;
            this.sortMenu = 0;
            this.initMethod = String.Empty;
            this.onClickMenu = String.Empty;
            this.idSecurity = 0;
            this.idForm = 0;
            this.nameForm = String.Empty;
            this.imageMenu = String.Empty;
            this.imageNew = String.Empty;
            this.imageDelete = String.Empty;
            this.isGrid = false;

        }
    }    
}
