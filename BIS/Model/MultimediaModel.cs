using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class MultimediaModel : IModel
    {

        [DisplayName("ID multimedia")]
        public int idMultimedia { get; set; }

        [DisplayName("Article code")]
        public string idArticle { get; set; }

        [DisplayName("Article name")]
        public string nameArtical{ get; set; }

        [DisplayName("ID client")]
        public int idClient { get; set; }

        [DisplayName("ID server")]
        public int idServer { get; set; }

        [DisplayName("Client")]
        public string nameClient { get; set; }

        [DisplayName("ID period")]
        public int idPeriod { get; set; }

        [DisplayName("Period")]

        public string namePeriod { get; set; }

        [DisplayName("Description")]
        public string description { get; set; }

        [DisplayName("ID Creator user")]
        public int idUserCreated { get; set; }

        [DisplayName("Creation date")]
        public DateTime dtUserCreated { get; set; }

        [DisplayName("ID Modified user")]
        public int idUserModified { get; set; }

        [DisplayName("Modification date")]
        public DateTime dtUserModified { get; set; }

        public MultimediaModel()
        {
            this.idMultimedia = 0;
            this.idArticle = String.Empty;
            this.nameArtical = String.Empty;
            this.idClient = 0;
            this.nameClient = String.Empty;
            this.idPeriod = 0;
            this.namePeriod = String.Empty;
            this.description = string.Empty;
            this.idUserCreated = 0;
            this.dtUserCreated = DateTime.Now;
            this.idUserModified = 0;
            this.dtUserModified = DateTime.Now;
        }
    }

    public class PhotosModel : IModel
    {
        [DisplayName("ID photos")]
        public int idPhotos { get; set; }

        [DisplayName("Photos name")]
        public string namePhotos { get; set; }

        [DisplayName("Picture")]
        public System.Drawing.Image  imagePhoto {get; set;}

        [DisplayName("ID multimedia")]
        public int idMultimedia { get; set; }

        [DisplayName("Multimedia")]
        public string descMultimedia { get; set; }
     
        [DisplayName("Active")]
        public bool? isActive { get; set; }

        [DisplayName("ID Creator user")]
        public int? idUserCreator { get; set; }

        [DisplayName("Creation date")]
        public DateTime dtUserCreator { get; set; }

        [DisplayName("ID Modified user")]
        public int? idUserModified { get; set; }

        [DisplayName("Modification date")]
        public DateTime dtUserModified { get; set; }

        public PhotosModel()
        {
            this.idPhotos = 0;
            this.namePhotos = String.Empty;
            this.imagePhoto = System.Drawing.Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Images\\no-image.png");
            this.idMultimedia = 0;
            this.descMultimedia = String.Empty;           
            this.isActive = false;
            this.idUserCreator = 0;
            this.dtUserCreator = DateTime.Now;
            this.idUserModified = 0;
            this.dtUserModified = DateTime.Now;
             
        }

    }

    public class MultimediaServerModel : IModel
    {
        [DisplayName("ID server")]
        public int idServer { get; set; }

        [DisplayName("Path")]
        public string path { get; set; }

        [DisplayName("Folder")]
        public string folder { get; set; }
        
        public MultimediaServerModel()
        {
            this.idServer = 0;
            this.path = String.Empty;
            this.folder = String.Empty;            
        }
    }

    public class MultimediaServerCredentialsModel : IModel
    {
        [DisplayName("Username")]
        public string username { get; set; }

        [DisplayName("Password")]
        public string password { get; set; }

        public MultimediaServerCredentialsModel()
        {
            this.username = String.Empty;
            this.password = String.Empty;
        }          
    }
}
