using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WrpCcNocWeb.Models.CcModule;

namespace WrpCcNocWeb.Models
{
    public class CcModAppProject_37_IndvDetail
    {
        [Key]
        [Column("Project37IndvId", Order = 0)]
        public long Project37IndvId { get; set; }

        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "Project")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }
        
		[Column("DiscussAboutBaselineYesNoId", Order = 2)]
        [Display(Name = "Discussion about Baseline?")]
        public int? DiscussAboutBaselineYesNoId { get; set; }
        [ForeignKey("DiscussAboutBaselineYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoDABYN_37 { get; set; }

        [Column("DabApplicantComments", Order = 3)]
        [Display(Name = "Applicant Comments")]
        [MaxLength(150)]
        public string DabApplicantComments { get; set; }

        [Column("DabAuthorityComments", Order = 4)]
        [Display(Name = "Authority Comments")]
        [MaxLength(150)]
        public string DabAuthorityComments { get; set; }
                
        [Column("WaterBodyId", Order = 5)]
        [Display(Name = "Name of Water Bodies")]
        public int? WaterBodyId { get; set; }
        [ForeignKey("WaterBodyId")]
        public virtual LookUpCcModWaterBody LookUpCcModWaterBody { get; set; }
                
		[Column("WaterBodyTypeId", Order = 6)]
        [Display(Name = "Type of Water Bodies")]
        public int? WaterBodyTypeId { get; set; }
        [ForeignKey("WaterBodyTypeId")]
        public virtual LookUpCcModTypeOfWaterBody LookUpCcModTypeOfWaterBody { get; set; }	
		
		[Column("Offtake", Order = 7)]
        [Display(Name = "Offtake")]
        [MaxLength(250)]
        public string Offtake { get; set; }

        [Column("Outfall", Order = 8)]
        [Display(Name = "Outfall")]
        [MaxLength(250)]
        public string Outfall { get; set; }

        [Column("Connectivity", Order = 9)]
        [Display(Name = "Connectivity")]
        [MaxLength(250)]
        public string Connectivity { get; set; }
		
		[Column("WaterLevelDryMax", Order = 10)]
        [Display(Name = "Water Level Dry Max")]
        public double? WaterLevelDryMax { get; set; }

        [Column("WaterLevelDryMin", Order = 11)]
        [Display(Name = "Water Level Dry Min")]
        public double? WaterLevelDryMin { get; set; }

        [Column("WaterLevelWetMax", Order = 12)]
        [Display(Name = "Water Level Wet Max")]
        public double? WaterLevelWetMax { get; set; }

        [Column("WaterLevelWetMin", Order = 13)]
        [Display(Name = "Water Level WetMin")]
        public double? WaterLevelWetMin { get; set; }

        [Column("DischargeDryMax", Order = 14)]
        [Display(Name = "Discharge Dry Max")]
        public double? DischargeDryMax { get; set; }

        [Column("DischargeDryMin", Order = 15)]
        [Display(Name = "Discharge Dry Min")]
        public double? DischargeDryMin { get; set; }

        [Column("DischargeWetMax", Order = 16)]
        [Display(Name = "Discharge Wet Max")]
        public double? DischargeWetMax { get; set; }

        [Column("DischargeWetMin", Order = 17)]
        [Display(Name = "Discharge Wet Min")]
        public double? DischargeWetMin { get; set; }
		
		[Column("CrossSectionDepth", Order = 18)]
        [Display(Name = "Cross Section Depth")]
        public double? CrossSectionDepth { get; set; }

        [Column("CrossSectionWidth", Order = 19)]
        [Display(Name = "Cross Section Width")]
        public double? CrossSectionWidth { get; set; }
		
		[Column("WaterSalinity", Order = 20)]
        [Display(Name = "Salinity (ppm)")]
        [MaxLength(50)]
        public string WaterSalinity { get; set; }        
		
		[Column("WaterDO", Order = 21)]
        [Display(Name = "DO")]
        [MaxLength(50)]
        public string WaterDO { get; set; }
		
        [Column("WaterTDS", Order = 22)]
        [Display(Name = "TDS")]
        [MaxLength(50)]
        public string WaterTDS { get; set; }

        [Column("WaterPhLevel", Order = 23)]
        [Display(Name = "pH")]
        [MaxLength(50)]
        public string WaterPhLevel { get; set; }
		
		[Column("LandCadastre", Order = 24)]
        [Display(Name = "Land Cadastre of Existing/ Proposed Industrial Area")]
        [MaxLength(150)]
        public string LandCadastre  { get; set; }
		
		[Column("ProofOfLandOwnership", Order = 25)]
        [Display(Name = "Proof of Land Ownership")]
        [MaxLength(150)]
        public string ProofOfLandOwnership  { get; set; }
				
		[Column("RoofAreaOfBuilding", Order = 26)]
        [Display(Name = "Roof Area of Building")]
        public double? RoofAreaOfBuilding { get; set; }
		
		[Column("RoadLength", Order = 27)]
        [Display(Name = "Road Length")]
        public double? RoadLength { get; set; }
		
		[Column("GreenArea", Order = 28)]
        [Display(Name = "Green Area")]
        public double? GreenArea { get; set; }
		
		[Column("OpenLandArea", Order = 29)]
        [Display(Name = "Open Land Area")]
        public double? OpenLandArea { get; set; }
		
		[Column("AdjacentWetland", Order = 30)]
        [Display(Name = "Adjacent Wetland")]
        public double? AdjacentWetland { get; set; }
		
		[Column("MainManufactureProduct", Order = 31)]
        [Display(Name = "Main Manufacture Product")]
        [MaxLength(150)]
        public string MainManufactureProduct { get; set; }
		
		[Column("ByProductItem", Order = 32)]
        [Display(Name = "By Product Item")]
        [MaxLength(150)]
        public string ByProductItem { get; set; }
		
		[Column("WasteWaterGeneration", Order = 33)]
        [Display(Name = "Waste Water Generation (m3)")]
        public double? WasteWaterGeneration { get; set; }
		
		[Column("WasteWaterDischarge", Order = 34)]
        [Display(Name = "Waste Water Discharge Procedure and Discharge Point")]
        [MaxLength(150)]
        public string WasteWaterDischarge { get; set; }
		
		[Column("DischargeCapacity", Order = 35)]
        [Display(Name = "Discharge Capacity (m3/hr)")]
        public double? DischargeCapacity  { get; set; }
		
		[Column("CPLDLYesNoId", Order = 36)]
        [Display(Name = "Compatibility with Production Layout with Discharge Layout")]
        public int? CPLDLYesNoId { get; set; }
        [ForeignKey("CPLDLYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoCPLDL_37 { get; set; }
		
		[Column("ADEACYesNoId", Order = 37)]
        [Display(Name = "Appropriate Design for Easy and Active Cleaning")]
        public int? ADEACYesNoId { get; set; }
        [ForeignKey("ADEACYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoADEAC_37 { get; set; }
		
		[Column("IDSEOSYesNoId", Order = 38)]
        [Display(Name = "Integration of Discharge System with Entire Operation System")]
        public int? IDSEOSYesNoId { get; set; }
        [ForeignKey("IDSEOSYesNoId")]
        public virtual LookUpCcModYesNo LookUpYesNoDSEOS_37 { get; set; }
		
		[Column("WaterDemandDomestic", Order = 39)]
        [Display(Name = "Domestic Water Demand within 2 km")]
        public double? WaterDemandDomestic { get; set; }
		
		[Column("WaterDemandIrrigation", Order = 40)]
        [Display(Name = "Irrigation Water Demand within 2 km")]
        public double? WaterDemandIrrigation { get; set; }
		
		[Column("WaterDemandOther", Order = 41)]
        [Display(Name = "Other Water Demand within 2 km")]
        public double? WaterDemandOther { get; set; }
		
		[Column("WaterUseSectorId", Order = 42)]
        [Display(Name = "Water Use Sector")]
        public int? WaterUseSectorId { get; set; }
        [ForeignKey("WaterUseSectorId")]
        public virtual LookUpCcModWaterUseSector LookUpCcModWaterUseSector { get; set; }	
		
		[Column("StepsTakenWasteWaterTreatment", Order = 43)]
        [Display(Name = "Steps Taken for Waste Water Treatment")]
        [MaxLength(150)]
        public string StepsTakenWasteWaterTreatment { get; set; }
		
		[Column("TotalWasteWaterGeneration", Order = 44)]
        [Display(Name = "Total Waste Water Generation (m3/day)")]
        public double? TotalWasteWaterGeneration  { get; set; }
		
		[Column("UsableTreatedWaterQuantity", Order = 45)]
        [Display(Name = "Usable Treated Water Quantity (m3/day)")]
        public double? UsableTreatedWaterQuantity  { get; set; }

        [Column("DivertedWaterRtnSrcYNId", Order = 46)]
        [Display(Name = "Water Diversion from Source")]
        public int? DivertedWaterRtnSrcYNId { get; set; }
        [ForeignKey("DivertedWaterRtnSrcYNId")]
        public virtual LookUpCcModYesNo LookUpCcModYesNoDivertWtrRtnSrc_37 { get; set; }

        [Column("WaterWithdrawQuantityPerDay", Order = 47)]
        [Display(Name = "Water Withdraw Quantity Per Day")]        
        public double? WaterWithdrawQuantityPerDay { get; set; }	        
        
        [Column("UseOfFlowMeterMeasrYNId", Order = 48)]
        [Display(Name = "Use of Flow Meter to Measure Water Withdrawal")]
        public int? UseOfFlowMeterMeasrYNId { get; set; }
        [ForeignKey("UseOfFlowMeterMeasrYNId")]
        public virtual LookUpCcModYesNo LookUpCcModYesNoUseOfFlowMeter_37 { get; set; }
        
        [Column("NoOfPump", Order = 49)]
        [Display(Name = "No. of Pump")]        
        public int? NoOfPump { get; set; }
        
        [Column("PumpCapacity", Order = 50)]
        [Display(Name = "Pump Capacity")]        
        public double? PumpCapacity { get; set; }

        [Column("PipeDiameter", Order = 51)]
        [Display(Name = "Pipe Diameter")]       
        public double? PipeDiameter { get; set; }
                        			
		[Column("DischargeQuantity", Order = 52)]
        [Display(Name = "Discharge Quantity (m3/s)")]       
        public double? DischargeQuantity { get; set; }
		
		[Column("DischargePoint", Order = 53)]
        [Display(Name = "Discharge Point")]
        [MaxLength(150)]
        public string DischargePoint { get; set; }

        [Column("IsSpecializedIndustrialZone", Order = 54)]
        [Display(Name = "Whether the Proposed Industry Falls Under Any EPZ or Gov. Declared Specialized Industrial Zone?")]
        public int? IsSpecializedIndustrialZone { get; set; }
        [ForeignKey("IsSpecializedIndustrialZone")]
        public virtual LookUpCcModYesNo LookUpYesNo_IsSpecIndusZone { get; set; }
    }
}