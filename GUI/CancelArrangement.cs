using BIS.Business;
using BIS.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    public class CancelArrangement
    {

        Boolean withCost = false;
        public void cancelArrangement()
        {

        }

        public Boolean cancel(PersonBookModel selectedModel, ArrangementModel arrange,string nameForm,int idUser)
        {
            Boolean res = false;
           
            ArrangementBookBUS ab = new ArrangementBookBUS();
            DataTable dt = ab.checkIfPersonsIsExtraAndStatus(selectedModel.idArrangementBook,arrange.idArrangement);
            int idStatus = 0;
            string nameContPers = "";
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["idStatus"].ToString() != "")
                        idStatus = Convert.ToInt32(dt.Rows[0]["idStatus"].ToString());
                    if (dt.Rows[0]["nameContPers"].ToString() != "")
                        nameContPers = dt.Rows[0]["nameContPers"].ToString();
                }
            }
            if (dt != null)
                if (selectedModel.idStatus == 1 && dt.Rows.Count > 0)
                {
                    translateRadMessageBox trr = new translateRadMessageBox();
                    trr.translatePartAndNonTranslatedPart("You can't cancel this person because it's extra person for other optional booking on this arrangement of", nameContPers);
                }
                else if (selectedModel.idStatus == 2 && dt.Rows.Count > 0)
                {
                    translateRadMessageBox trr = new translateRadMessageBox();
                    trr.translatePartAndNonTranslatedPart("You can't cancel this person because it's extra person for other final booking on this arrangement of", nameContPers);
                }
                else if (ab.checkIsInTravelersNotPaidFor(selectedModel.idArrangementBook) != "")
                {
                    translateRadMessageBox trr = new translateRadMessageBox();
                    trr.translatePartAndNonTranslatedPart("You can't cancel arrangement book because this person is added as traveler with ", ab.checkIsInTravelers(selectedModel.idArrangementBook));
                }
                else
                {

                  
                    if (selectedModel.type.ToString() == "Traveler" && selectedModel.idStatus.ToString() != "4")
                    {

                        translateRadMessageBox tr = new translateRadMessageBox();
                        MessageBoxCalendar cancelMessageBox = new MessageBoxCalendar();
                        if (cancelMessageBox.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
                        {
                            ArrangementBookBUS bus = new ArrangementBookBUS();
                            bool c = false;
                            if (ab.checkIfPersonsHasExtraAndStatus(selectedModel.idArrangementBook) == true)
                            {
                                List<PersonModel> arrangementBookPerson = new List<PersonModel>();
                                int idArrangementBook = 0;
                                idArrangementBook = selectedModel.idArrangementBook;
                                arrangementBookPerson = new PersonBUS().GetArrangementPersons(idArrangementBook, Login._user.lngUser);
                                foreach (PersonModel mm in arrangementBookPerson)
                                {
                                    ArrangementBookModel abmm = new ArrangementBookModel();
                                    int idArrangementBook2 = 0;
                                    idArrangementBook2 = bus.GetIdArrangementBook(arrange.idArrangement, mm.idContPers);

                                    abmm = ab.GetArrangementBook(idArrangementBook2);
                                    if (abmm.idStatus == 4)
                                    {
                                        tr = new translateRadMessageBox();
                                        tr.translateAllMessageBox("Already canceled ");
                                    }
                                    else if (abmm.idStatus == 1 && dt.Rows.Count > 0)
                                    {
                                        translateRadMessageBox trr = new translateRadMessageBox();
                                        trr.translatePartAndNonTranslatedPart("You can't cancel this person because it's extra person for other optional booking on this arrangement of", nameContPers);
                                    }
                                    else if (abmm.idStatus == 2 && dt.Rows.Count > 0)
                                    {
                                        translateRadMessageBox trr = new translateRadMessageBox();
                                        trr.translatePartAndNonTranslatedPart("You can't cancel this person because it's extra person for other final booking on this arrangement of", nameContPers);
                                    }
                                    else if (ab.checkIsInTravelers(selectedModel.idArrangementBook) != "")
                                    {
                                        translateRadMessageBox trr = new translateRadMessageBox();
                                        trr.translatePartAndNonTranslatedPart("You can't cancel arrangement book because this person is added as traveler with ", ab.checkIsInTravelers(selectedModel.idArrangementBook));
                                    }
                                    else
                                    {
                                        c = bus.CancelArrangament(idArrangementBook2, cancelMessageBox.selectedDate);
                                        if (c == true)
                                        {
                                            cancelInvoice(idArrangementBook2, cancelMessageBox.selectedDate, nameForm, idUser);
                                            recalculateVolArr(arrange.idArrangement, mm.idContPers);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                c = true;
                            }


                            if (c == true)
                            {
                                bool b = bus.CancelArrangament(selectedModel.idArrangementBook, cancelMessageBox.selectedDate);
                                if (b == true)
                                {


                                    cancelInvoice(selectedModel.idArrangementBook, cancelMessageBox.selectedDate, nameForm, idUser);
                                    tr = new translateRadMessageBox();
                                    tr.translateAllMessageBox("Arrangement is canceled.");
                                    res = true;
                                    recalculateVolArr(arrange.idArrangement, selectedModel.idContPers);
                                }
                            }
                            else
                            {
                                tr = new translateRadMessageBox();
                                tr.translateAllMessageBox("Error occured during cancelation proces. Please contact your administator.");
                            }
                        }

                    }
                    else
                    {
                        if (selectedModel.idStatus.ToString() != "4")
                        {
                            translateRadMessageBox tr = new translateRadMessageBox();
                            MessageBoxCalendar cancelMessageBox = new MessageBoxCalendar();
                            if (cancelMessageBox.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
                            {
                                ArrangementBookBUS bus = new ArrangementBookBUS();

                                bool b = bus.CancelArrangament(selectedModel.idArrangementBook, cancelMessageBox.selectedDate);
                                if (b == true)
                                {
                                    tr = new translateRadMessageBox();
                                    tr.translateAllMessageBox("Arrangement is canceled.");
                                    res = true;

                                }
                                else
                                {
                                    tr = new translateRadMessageBox();
                                    tr.translateAllMessageBox("Error occured during cancelation proces. Please contact your administator.");
                                }
                            }
                        }
                        else
                        {
                            translateRadMessageBox tr = new translateRadMessageBox();
                            tr.translateAllMessageBox("Already canceled ");
                        }
                    }
                }
            return res;
        }

        private void cancelInvoice(int idArrangementBook, DateTime cancelDate,string nameForm, int idUser)
        {
            InvoiceBUS iib = new InvoiceBUS();
            if (iib.checkIfThereIsInvoiceForCancelingWithBasicNotForCanceling(idArrangementBook) == false)
            {
                BIS.Business.ArrangementBookBUS arrBUS = new BIS.Business.ArrangementBookBUS();
                iib = new BIS.Business.InvoiceBUS();
                List<InvoiceModel> iim = new List<InvoiceModel>();

                idArrangementBook = arrBUS.GetIdArrangementBookByIdBookArrangement(idArrangementBook);

                
                iim = iib.GetInvoiceCustomerAndVoucher(idArrangementBook);

                List<InvoiceModel> iimCancel = new List<InvoiceModel>();

                if (iim != null && iim.Count > 0)
                {
                    foreach (InvoiceModel m in iim)
                    {
                        if (m.invoiceRbr != null)
                        {
                            int iid = Int32.Parse(m.invoiceRbr);
                            if (iid < 900)
                            {
                                
                                MakeInvoice mi = new MakeInvoice();

                                if (m.idInvoiceStatus == 1 || m.idInvoiceStatus == 6)
                                    mi.DoDelete(m.idInvoice, nameForm,idUser);
                                else
                                    iimCancel.Add(m);
                                   
                            }

                        }
                    }
                    if(iimCancel!=null)
                        if (iimCancel.Count > 0)
                        {
                            //=== kreiranje cancel fakture ===
                           
                            translateRadMessageBox tr = new translateRadMessageBox();
                          
                            if (tr.translateAllMessageBoxDialogYesNo("Do you want to cancel with costs?","Cancel") == System.Windows.Forms.DialogResult.Yes)
                            {
                                withCost = true;
                                MakeInvoice canci = new MakeInvoice();
                                canci.CancelationInvoice(idArrangementBook, cancelDate, nameForm, idUser);
                            }
                          

                        }
                    foreach (InvoiceModel mm in iimCancel)
                    {
                        if (mm.invoiceRbr != null)
                        {
                            int iid = Int32.Parse(mm.invoiceRbr);
                            if (iid < 900)
                            {

                                MakeInvoice mi = new MakeInvoice();

                                if (mm.idInvoiceStatus != 1 && mm.idInvoiceStatus != 6)
                                    mi.DoCancel(mm.idInvoice, nameForm, idUser);
                            }
                        }
                    }
                    if(iimCancel!=null)
                        if (iimCancel.Count > 0)
                        {
                            if (withCost == true)
                            {
                                InvoiceModel nn = new InvoiceModel();
                                nn = new InvoiceBUS().GetInvoiceByInvoiceAndExtension(iimCancel[0].invoiceNr, "999");
                                frmInvoice fi = new frmInvoice(nn);
                                fi.ShowDialog();
                            }
                        }
                   
                }
               // updateStatus(arrange);
            }
            else
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("You cant cancel invoices because you have some non basic invoices with status different than in progress or ready to print and basic invoices with this statuses!");
            }
        }



        //private bool allCanceled(ArrangementModel arrange)
        //{
        //    PersonBUS pb = new PersonBUS();
        //    List<PersonBookModel> list = pb.GetAllPersonsForArrangement(arrange.idArrangement, Login._user.lngUser);
        //    foreach (PersonBookModel m in list)
        //    {
        //        if (m.idStatus != 4)
        //            return false;
        //    }
        //    return true;
        //}

        //private void updateStatus(ArrangementModel arrange)
        //{
        //    List<ArrangementStatusModel> statuslist = new ArrangementStatusBUS().GetAllArrangementStatus();
        //    if (allCanceled(arrange))
        //    {
        //        arrange.statusArrangement = statuslist.SingleOrDefault(s => s.idArrangementStatus == 4).nameArrangementStatus;
        //    }
        //    ArrangementBUS ab = new ArrangementBUS();
        //    ab.Update(arrange);
        //}

        private void recalculateVolArr(int idArrangement, int idContPers)
        {

            ArrangementBookBUS ab = new ArrangementBookBUS();

            // broj skilova iz tabele medLookup
            List<MedicalVoluntaryQuestModel> listNrSkill = new List<MedicalVoluntaryQuestModel>();
            // svi skilovi iz tabele volArr za taj arrangement
            List<MedicalVoluntaryQuestModel> listSkillVolArr = new List<MedicalVoluntaryQuestModel>();
            //svi skilovi koji su izbrisani za posmatranu osobu i aranzman iz MedLookup-a
            List<MedicalVoluntaryQuestModel> listDeleteSkillMedLookup = new List<MedicalVoluntaryQuestModel>();


            // listSkill = ab.GetSkillForPerson(arrBookModel.idContPers);
            listDeleteSkillMedLookup = ab.GetSkillsForArrAndPerson(idArrangement, idContPers);
            ab.DeletePrsonFromMedLookup(idArrangement, idContPers);
            listNrSkill = ab.GetNrForSkillsArrangement(idArrangement);
            listSkillVolArr = ab.GetAllSkillsVolArr(idArrangement);

            if (listDeleteSkillMedLookup != null)
            {
                for (int i = 0; i < listDeleteSkillMedLookup.Count; i++)
                {
                    int a = listDeleteSkillMedLookup[i].idQuest;

                    var nrSkillIsOne = listSkillVolArr.Find(s => s.idQuest == listDeleteSkillMedLookup[i].idQuest && (s.nameQuestGroup) == "1");

                    if (nrSkillIsOne != null)
                    {
                        ab.DeleteVolArr(idArrangement, listDeleteSkillMedLookup[i].idQuest);
                    }
                }
            }

            if (listSkillVolArr != null)
            {

                if (listNrSkill != null)
                {
                    for (int i = 0; i < listNrSkill.Count; i++)
                    {
                        // List<MedicalVoluntaryQuestModel> telForPerson = new List<MedicalVoluntaryQuestModel>(); 
                        var skillExistInVolarr = listSkillVolArr.Find(s => s.idQuest == listNrSkill[i].idQuest);
                        //insert                             
                        if (skillExistInVolarr != null)
                        {
                            string txt = listNrSkill[i].nameQuestGroup;
                            int idQuest = listNrSkill[i].idQuest;


                            if (ab.UpdateVolArr(listNrSkill[i], txt, idQuest, idArrangement) == false)
                            {
                                translateRadMessageBox tr = new translateRadMessageBox();
                                tr.translateAllMessageBox("You have not succesufully update skill. Please check!");
                            }

                        }

                    }
                }


            }


        }
    }
}
