using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class BookmarksModel : IModel
    {
        [DisplayName("ID field")]
        public int idField { get; set; }

        [DisplayName("ID bookmark")]
        public Guid? idBookmark { get; set; }

        [DisplayName("Bookmark name")]
        public string nameBookmark { get; set; }

        [DisplayName("Table name")]
        public string tableName { get; set; }

        [DisplayName("Field name")]
        public string fieldName { get; set; }

        [DisplayName("Field bookmark")]
        public string fieldBookmark { get; set; }

        [DisplayName("Field value")]
        public string fieldValue { get; set; }

        [DisplayName("Creation date")]
        public DateTime? dtCreated { get; set; }

        [DisplayName("Modification date")]
        public DateTime? dtModified { get; set; }

        [DisplayName("Modified user")]
        public string nameUserModified { get; set; }

        [DisplayName("ID Modified user")]
        public int? userModifiel { get; set; }

        [DisplayName("Creator user")]
        public string nameUserCreated { get; set; }

        [DisplayName("ID Creator user")]
        public int? userCreated { get; set; }


        public BookmarksModel()
        {
        }

        public BookmarksModel(Guid? idBookmark, string nameBookmark, string tableName, string fieldName, string fieldBookmark, string fieldValue,
            DateTime? dtCreated, DateTime? dtModified, int? userModifiel, int? userCreated)
        {

            this.idBookmark = idBookmark;
            this.nameBookmark = nameBookmark;
            this.tableName = tableName;
            this.fieldName = fieldName;
            this.fieldBookmark = fieldBookmark;
            this.fieldValue = fieldValue;
            this.dtCreated = dtCreated;
            this.dtModified = dtModified;
            this.userModifiel = userModifiel;
            this.userCreated = userCreated;

        }
    }
    public class BookmarkSpecModel : IModel
    {
        public string table { get; set; }
        public string field { get; set; }
        public string value { get; set; }
    }
}
