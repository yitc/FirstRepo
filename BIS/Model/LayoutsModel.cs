using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class LayoutsModel : IModel
    {
        [DisplayName("ID layout")]
        public int idLayout { get; set; }

        [DisplayName("Layout")]
        public string nameLayout { get; set; }

        [DisplayName("Document type")]
        public string typeDocument { get; set; }

        [DisplayName("Language")]
        public string languageLayout { get; set; }

        [DisplayName("File layout")]
        public string fileLayout { get; set; }
        
        [DisplayName("Bookmarks")]
        public string bookmarks { get; set; }

        [DisplayName("Template Table")]
        public string templateTable { get; set; }

        [DisplayName("ID Creator user")]
        public int? userCreated { get; set; }

        [DisplayName("Creator user")]
        public string nameUserCreated { get; set; }

        [DisplayName("Creation date")]
        public DateTime? dtCreated { get; set; }

        [DisplayName("ID Modified user")]
        public int? userModified { get; set; }

        [DisplayName("Modified user")]
        public string nameUserModified { get; set; }

        [DisplayName("Modification date")]
        public DateTime? dtModified { get; set; }

        public LayoutsModel()
        {

        }

        public LayoutsModel(string nameLayout, string typeDocument, string languageLayout, string fileLayout, string bookmarks, string tableName, 
            int? userCreated, DateTime? dtCreated, int? userModified, DateTime? dtModified)
        {
            this.nameLayout = nameLayout;
            this.typeDocument = typeDocument;
            this.languageLayout = languageLayout;
            this.fileLayout = fileLayout;
            this.bookmarks = bookmarks;
            this.templateTable = tableName;
            this.userCreated = userCreated;
            this.dtCreated = dtCreated;            
            this.userModified = userModified;
            this.dtModified = dtModified;
        }
    }
}
