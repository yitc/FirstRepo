using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data.Linq.Mapping;
using BIS.DAO;

namespace BIS.Model
{
    public class ToDoModel 
    {
         [DisplayName("Task id")]
        public int idToDo { get; set; }

        [DisplayName("Task type")]
         public string descriptionToDoType { get; set; }

        [DisplayName("ID Task type")]
        public int idToDoType { get; set; }

         [DisplayName("Open date")]
        public DateTime dtOpenDate { get; set; }
         
        [DisplayName("Close date")]
        public DateTime dtCloseDate { get; set; }

         [DisplayName("End date")]
        public DateTime dtEndDate { get; set; }

         [DisplayName("Planed time")]
        public decimal? planedTime { get; set; }

         [DisplayName("Actial time")]
         public decimal? actualTime { get; set; }

         [DisplayName("Status")]
         public string descriptionStatus { get; set; }

        [DisplayName("ID Status")]
        public int idStatusToDo { get; set; }

         [DisplayName("Priority")]
        public string descriptionPriority { get; set; }

         [DisplayName("ID Priority")]
        public int idPriorityToDo { get; set; }

         [DisplayName("ID Contact relation")]
        public int idContact { get; set; }

         [DisplayName("Contact relation")]
         public string reasonContact { get; set; }

         [DisplayName("Description")]
        public string descriptionToDo { get; set; }

         [DisplayName("ID Client")]
        public int idClient { get; set; }

         [DisplayName("Client")]
         public string nameClient { get; set; }

         [DisplayName("ID person")]
        public int idContPers { get; set; }

         [DisplayName("Person")]
         public string nameContPers { get; set; }

         [DisplayName("ID Project")]
        public int idProject { get; set; }

         [DisplayName("Project")]
         public string nameProject { get; set; }

         [DisplayName("ID Creator user")]
        public int idOwner { get; set; }

         [DisplayName("Creator user")]
         public string nameOwner { get; set; }

         [DisplayName("ID Responsible")]
        public int idEmployee { get; set; }

         [DisplayName("Responible")]
         public string nameEmployee { get; set; }

        // [DisplayName("Remind")]
        //public bool isRemider { get; set; }
         //jelena
         [DisplayName("Id arrangement")]
         public int idArrangement { get; set; }

         [DisplayName("Name arrangement")]
         public string nameArrangement { get; set; }
        // end jelena

        public ToDoModel()
         {          
            this.idToDo = 0;
            this.descriptionToDoType = String.Empty;
            this.idToDoType = 0;
            this.dtOpenDate = DateTime.Now;
            this.dtCloseDate = DateTime.Now;
            this.dtEndDate = DateTime.Now;
            this.planedTime = 0;
            this.actualTime = 0;
            this.descriptionStatus = String.Empty;
            this.idStatusToDo = 0;
            this.descriptionPriority = String.Empty;
            this.idPriorityToDo = 0;
            this.idContact = 0;
            this.reasonContact = String.Empty;
            this.descriptionToDo = String.Empty;
            this.idClient = 0;
            this.nameClient = String.Empty;
            this.idContPers = 0;
            this.nameContPers = String.Empty;
            this.idProject = 0;
            this.nameProject = String.Empty;
            this.idOwner = 0;
            this.nameOwner = String.Empty;
            this.idEmployee = 0;
            this.nameEmployee = String.Empty;
            this.idArrangement = 0;
            this.nameArrangement = String.Empty;
        }
    }
}