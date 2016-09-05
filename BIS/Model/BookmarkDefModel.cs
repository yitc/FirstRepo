using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class BookmarkDefModel
    {
        public int Id { get; set; }

        public string tableDisplayName { get; set; }

        public string tableName { get; set; }

        public string fieldBookmark { get; set; }

        public string nameBookmark { get; set; }

        public string displayNameBookmark { get; set; }

        public bool? isRelationBmk { get; set; }

        public string relationTableName { get; set; }

        public string relationFieldName { get; set; }

        public bool? hasType { get; set; }

        public string typeTableName { get; set; }

        public string typeFieldName { get; set; }

        public int? typeFieldValue { get; set; }

        public bool? isActive { get; set; }

        public DateTime? dtCreated { get; set; }

        public DateTime? dtModified { get; set; }

        public int? userModified { get; set; }

        public int? userCreated { get; set; }

    }

    public class BookmarkTableModel
    {
        public int idTable { get; set; }

        public string nameTable { get; set; }

        public string displayNameTable { get; set; }

        public bool active { get; set; }        

        public int fontSizeFields { get; set; }        

        public float leftMargin { get; set; }

        public float rightMargin { get; set; }        
                                
        public BookmarkTableModel()
        {
            this.idTable = 0;
            this.nameTable = String.Empty;
            this.displayNameTable = String.Empty;
            this.active = true;
            this.fontSizeFields = 10;
            this.leftMargin = 0.1f;
            this.rightMargin = 0.1f;

        }
    }

    public class BookmarkTableFieldsModel
    {
        public int idTableField { get; set; }

        public int idTable { get; set; }

        public string nameField { get; set; }

        public float widthField { get; set; }

        public string displayNameField { get; set; }

        public bool visible { get; set; }

        public int rbr_sort { get; set; }

        public bool isTotal { get; set; }

        public BookmarkTableFieldsModel()
        {
            this.idTableField = 0;
            this.idTable = 0;
            this.nameField = String.Empty;
            this.displayNameField = String.Empty;
            this.visible = true;
            this.widthField= 10;            
            this.rbr_sort = 0;
            this.isTotal = false;

        }
    }
}
