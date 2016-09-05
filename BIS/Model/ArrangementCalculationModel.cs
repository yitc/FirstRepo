using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Model
{
    public class ArrangementCalculationModel : IModel
    {
        public int idArrangement { get; set; }


        public decimal? provision { get; set; }

        public decimal? calamiteitenFonds { get; set; }

        public decimal? correction { get; set; }


        public decimal? travelInsurace { get; set; }

        public decimal? travelInsurance2 { get; set; }

        public decimal? polisCosts { get; set; }

        public decimal? price { get; set; }

        public decimal? moneyGroup { get; set; }

        public decimal? insuranceVolontary { get; set; }

        public decimal? singleRoomPrice { get; set; }

        public decimal? discount { get; set; }

        public string txt { get; set; }

        public decimal? txtAmount { get; set; }

        public int? numberLeader { get; set; }

        public decimal? premie1 { get; set; }

        public decimal? premie2 { get; set; }

        public int? numberCO { get; set; }

        public int? minNumberTravelers { get; set; }

        public int? volontaryDays { get; set; }

        public bool isSport { get; set; }

        public int nrVoluntary { get; set; }


        public ArrangementCalculationModel()
        {
            this.idArrangement = 0;
            this.provision = 0;
            this.calamiteitenFonds = 0;
            this.correction = 0;
            this.travelInsurace = 0;
            this.travelInsurance2 = 0;
            this.polisCosts = 0;
            this.price = 0;
            this.moneyGroup = 0;
            this.insuranceVolontary = 0;
            this.singleRoomPrice = 0;
            this.discount = 0;
            this.txt = "Buitenhof Club";
            this.txtAmount = 25;
            this.numberLeader = 0;
            this.premie1 = 0;
            this.premie2 = 0;
            this.numberCO = 0;
            this.minNumberTravelers = 0;
            this.volontaryDays = 0;
            this.isSport = false;
            this.nrVoluntary = 0;

        }

        public ArrangementCalculationModel(ArrangementCalculationModel model)
        {
            this.idArrangement = model.idArrangement;
            this.provision = model.provision;
            this.calamiteitenFonds = model.calamiteitenFonds;
            this.correction = model.correction;
            this.travelInsurace = model.travelInsurace;
            this.travelInsurance2 = model.travelInsurance2;
            this.polisCosts = model.polisCosts;
            this.price = model.price;
            this.moneyGroup = model.moneyGroup;
            this.insuranceVolontary = model.insuranceVolontary;
            this.singleRoomPrice = model.singleRoomPrice;
            this.discount = model.discount;
            this.txt = model.txt;
            this.txtAmount = model.txtAmount;
            this.numberLeader = model.numberLeader;
            this.premie1 = model.premie1;
            this.premie2 = model.premie2;
            this.numberCO = model.numberCO;
            this.minNumberTravelers = model.minNumberTravelers;
            this.volontaryDays = model.volontaryDays;
            this.isSport = model.isSport;
            this.nrVoluntary = model.nrVoluntary;
        }

    }

    public class ArrangementCalculationSecondModel : IModel
    {
        public int idArrangement { get; set; }

        public decimal? tax { get; set; }

        public decimal? provision { get; set; }

        public decimal? calamiteitenFonds { get; set; }

        public decimal? correction { get; set; }

        public decimal? travelInsurace { get; set; }

        public decimal? travelInsurance2 { get; set; }

        public decimal? polisCosts { get; set; }

        public decimal? price { get; set; }

        public decimal? moneyGroup { get; set; }

        public decimal? insuranceVolontary { get; set; }

        public decimal? singleRoomPrice { get; set; }

        public decimal? discount { get; set; }

        public string txt { get; set; }

        public decimal? txtAmount { get; set; }

        public decimal? serviceCharVol { get; set; }

        public decimal? extrasVol { get; set; }

        public int? numberLeader { get; set; }

        public decimal? premie1 { get; set; }

        public decimal? premie2 { get; set; }

        public int? numberCO { get; set; }

        public int? minNumberTravelers { get; set; }

        public int? volontaryDays { get; set; }

        public bool isSport { get; set; }

        public int? nrTraveler { get; set; }

        public int? nrVoluntaryHelper { get; set; }

        public int nrVoluntary { get; set; }

        public string accomSerChargVol { get; set; }


        public ArrangementCalculationSecondModel()
        {
            this.idArrangement = 0;
            this.tax = 0;
            this.provision = 0;
            this.calamiteitenFonds = 0;
            this.correction = 0;
            this.travelInsurace = 0;
            this.travelInsurance2 = 0;
            this.polisCosts = 0;
            this.price = 0;
            this.moneyGroup = 0;
            this.insuranceVolontary = 0;
            this.singleRoomPrice = 0;
            this.discount = 0;
            this.txt = "Buitenhof Club";
            this.txtAmount = 25;
            this.serviceCharVol = 0;
            this.extrasVol = 0;
            this.numberLeader = 0;
            this.premie1 = 0;
            this.premie2 = 0;
            this.numberCO = 0;
            this.minNumberTravelers = 0;
            this.volontaryDays = 0;
            this.isSport = false;
            this.nrTraveler = 0;
            this.nrVoluntaryHelper = 0;
            this.nrVoluntary = 0;
            this.accomSerChargVol = "";

        }

    }
}
