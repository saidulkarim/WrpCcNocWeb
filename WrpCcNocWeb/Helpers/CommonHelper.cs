using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WrpCcNocWeb.DatabaseContext;
using WrpCcNocWeb.Models;

namespace WrpCcNocWeb.Helpers
{
    public class CommonHelper
    {
        #region Initialization        
        private readonly WrpCcNocDbContext _db = new WrpCcNocDbContext();
        #endregion

        public CommonHelper()
        {

        }

        //public SelectList GetWaterQualityParams()
        //{
        //    SelectList origList = new SelectList(db.WaterQualityParams.Where(w => w.IsActive == true && w.IsDelete == false), "WaterQualityParamID", "ParameterName", "");

        //    List<SelectListItem> newList = origList.ToList();
        //    newList.Insert(0, new SelectListItem() { Value = "", Text = "Select an option..." });

        //    var selectedItem = newList.FirstOrDefault(item => item.Selected);
        //    var selectedItemValue = String.Empty;
        //    if (selectedItem != null)
        //    {
        //        selectedItemValue = selectedItem.Value;
        //    }

        //    return new SelectList(newList, "Value", "Text", selectedItemValue);
        //}

        public enum Month
        {
            January,
            February,
            March,
            April,
            May,
            June,
            July,
            August,
            September,
            October,
            November,
            December
        }

        public enum Sign
        {
            Danger,
            Dark,
            Default,
            Error,
            Info,
            Primary,
            Success,
            Warning
        }

        public enum OperationMessage
        {
            [DisplayText("Information has been deleted.")]
            Delete,

            [DisplayText("Information not deleted.")]
            NotDelete,

            [DisplayText("An error has been occured!")]
            Error,

            [DisplayText("Data has been saved successfully.")]
            Success,

            [DisplayText("Data not save.")]
            NotSuccess,

            [DisplayText("Data has been updated successfully.")]
            Update,

            [DisplayText("Data not updated.")]
            NotUpdate,

            [DisplayText("Data has been imported successfully.")]
            ImportSuccess,

            [DisplayText("Data not imported successfully.")]
            ImportNotSuccess
        }

        public string ShowMessage(Enum sign, string msg)
        {
            return sign + ": " + msg;
        }

        public string ShowMessage(Enum sign, string title, string msg)
        {
            return sign + ": " + msg + "###" + title;
        }

        public string ShowMessage(string sign, string title, string msg)
        {
            return sign + ": " + msg + "###" + title;
        }

        public string ExtractInnerException(Exception ex)
        {
            string message = string.Empty;
            if (ex.Message.Contains("See the inner exception"))
            {
                if (ex.InnerException.Message.Contains("See the inner exception"))
                {
                    message = ex.InnerException.InnerException.Message;
                }
                else
                {
                    message = ex.InnerException.Message.ToString();
                }
            }
            else
            {
                message = ex.Message.ToString();
            }

            return message.Replace("'", "").Replace("\"", "").Replace("\r\n", "").Replace("\n", "");
        }

        /// <summary>
        /// Get Form 3.1: Flood Control Management Data Analysis Control Header Group List
        /// </summary>
        /// <returns>List<DataAnalysisControlHeaderGroup></returns>
        public List<DataAnalysisControlHeaderGroup> GetF31ControlHeaderList()
        {
            List<DataAnalysisControlHeaderGroup> chg = new List<DataAnalysisControlHeaderGroup>
            {
                new DataAnalysisControlHeaderGroup { ControlTitle = "Project Name", ControlId = "ProjectName" },
                new DataAnalysisControlHeaderGroup { ControlTitle = "Project Background and Rationale", ControlId = "BackgroundAndRationale" },
                new DataAnalysisControlHeaderGroup { ControlTitle = "Project Target", ControlId = "ProjectTarget" },
                new DataAnalysisControlHeaderGroup { ControlTitle = "Project Objective", ControlId = "ProjectObjective" },
                new DataAnalysisControlHeaderGroup { ControlTitle = "Project Location", ControlId = "ProjectLocation" },
                new DataAnalysisControlHeaderGroup { ControlTitle = "Project Estimated Cost", ControlId = "ProjectEstimatedCost" },
                new DataAnalysisControlHeaderGroup { ControlTitle = "Project Period", ControlId = "ProjectPeriod" },
                new DataAnalysisControlHeaderGroup { ControlTitle = "Hydrological Region", ControlId = "HydrologicalRegion" },
                new DataAnalysisControlHeaderGroup { ControlTitle = "Bangladesh Delta Plan 2100 Hot Spot", ControlId = "BDP2100HotSpot" },
                new DataAnalysisControlHeaderGroup { ControlTitle = "Hydrological System", ControlId = "HydrologicalSystem" },
                new DataAnalysisControlHeaderGroup { ControlTitle = "Connectivity Amid Khals, River and Wetland", ControlId = "ConnectivityAmidWaterland" },
                new DataAnalysisControlHeaderGroup { ControlTitle = "Catchment Area (ha)", ControlId = "CatchmentArea" },
                new DataAnalysisControlHeaderGroup { ControlTitle = "Annual Rainfall (last five years)", ControlId = "AnnualRainfallLastFiveYears" },
                new DataAnalysisControlHeaderGroup { ControlTitle = "Highest Flood Level (m PWD)", ControlId = "HighestFloodLevel" },
                new DataAnalysisControlHeaderGroup { ControlTitle = "Maximum Discharge", ControlId = "MaximumDischarge" },
                new DataAnalysisControlHeaderGroup { ControlTitle = "Types of Flood", ControlId = "TypesOfFlood" },
                new DataAnalysisControlHeaderGroup { ControlTitle = "Flood Frequency Analysis (Maximum Water Level in m, PWD)", ControlId = "FloodFrequencyAnalysis" },
                new DataAnalysisControlHeaderGroup { ControlTitle = "Drainage Condition", ControlId = "DrainageCondition" },
                new DataAnalysisControlHeaderGroup { ControlTitle = "Surface Water Quality", ControlId = "SurfaceWaterQuality" },
                new DataAnalysisControlHeaderGroup { ControlTitle = "Land Type (Percent)", ControlId = "LandTypePercent" },
                new DataAnalysisControlHeaderGroup { ControlTitle = "Cultivable Crops", ControlId = "CultivableCrops" },
                new DataAnalysisControlHeaderGroup { ControlTitle = "Irrigated Crop Area (ha)", ControlId = "IrrigatedCropAreaHa" },
                new DataAnalysisControlHeaderGroup { ControlTitle = "Crop production (Ton)", ControlId = "CropProduction" },
                new DataAnalysisControlHeaderGroup { ControlTitle = "Fish Production, Diversity and Migration", ControlId = "FishProductionDiversityMigration" },
                new DataAnalysisControlHeaderGroup { ControlTitle = "Flora and Fauna", ControlId = "FloraAndFauna" },
                new DataAnalysisControlHeaderGroup { ControlTitle = "Socio Economic Condition", ControlId = "SocioEconomicCondition" },
                new DataAnalysisControlHeaderGroup { ControlTitle = "Issue, Challenge or Problem", ControlId = "IssueChallageProblem" },
                new DataAnalysisControlHeaderGroup { ControlTitle = "Discussion with Stakeholders During Starting of the Project", ControlId = "DiscussionWithstakeholders" },
                new DataAnalysisControlHeaderGroup { ControlTitle = "Analyze Options to fulfill objective", ControlId = "AnalyzeOptionsFulfillObjective" },
                new DataAnalysisControlHeaderGroup { ControlTitle = "Design Submitted with Project Document", ControlId = "DesignSubmittedWithProjectDocument" },
                new DataAnalysisControlHeaderGroup { ControlTitle = "Environmental and Social Impact Assessment", ControlId = "EnvironmentalAndSocialImpactAssessment" },
                new DataAnalysisControlHeaderGroup { ControlTitle = "Environmental Impact Assessment", ControlId = "EnvironmentalImpactAssessment" },
                new DataAnalysisControlHeaderGroup { ControlTitle = "Social Impact Assessment", ControlId = "SocialImpactAssessment" },
                new DataAnalysisControlHeaderGroup { ControlTitle = "Use of Appropriate Tools, Technics, Analytical Procedures", ControlId = "UseOfAppropriateToolsTechnics" },
                new DataAnalysisControlHeaderGroup { ControlTitle = "Economical & Financial Analysis", ControlId = "EconomicalFinancialAnalysis" },
                new DataAnalysisControlHeaderGroup { ControlTitle = "Compatibility with National Water Policy", ControlId = "CompatibilityNationalWaterPolicy" },
                new DataAnalysisControlHeaderGroup { ControlTitle = "Compatibility with National Water Management Plan (NWMP)", ControlId = "CompatibilityNationalWaterManagementPlan" },
                new DataAnalysisControlHeaderGroup { ControlTitle = "Compatibility with Current Fifth Year Plan (FYP)", ControlId = "CompatibilityCurrentFifthYearPlan" },
                new DataAnalysisControlHeaderGroup { ControlTitle = "Compatibility with Sustainable Development Goal (SDG)", ControlId = "CompatibilitySustainableDevelopmentGoal" },
                new DataAnalysisControlHeaderGroup { ControlTitle = "Compatibility with Bangladesh Delta Plan 2100", ControlId = "CompatibilityBangladeshDeltaPlan2100" },
                new DataAnalysisControlHeaderGroup { ControlTitle = "Compatibility with Coastal Zone Policy", ControlId = "CompatibilityCoastalZonePolicy" },
                new DataAnalysisControlHeaderGroup { ControlTitle = "Compatibility with Agricultural Policy", ControlId = "CompatibilityAgriculturalPolicy" },
                new DataAnalysisControlHeaderGroup { ControlTitle = "Compatibility with Fisheries Policy", ControlId = "CompatibilityFisheriesPolicy" },
                new DataAnalysisControlHeaderGroup { ControlTitle = "Compatibility with Integrated Water Resource Management", ControlId = "CompatibilityIntegratedWaterResourceManagement" },
                new DataAnalysisControlHeaderGroup { ControlTitle = "Compatibility with GPWM", ControlId = "CompatibilityGPWM" },
                new DataAnalysisControlHeaderGroup { ControlTitle = "Feasibility Study", ControlId = "FeasibilityStudy" },
                new DataAnalysisControlHeaderGroup { ControlTitle = "Duplication of the Project", ControlId = "DuplicationOfTheProject" },
                new DataAnalysisControlHeaderGroup { ControlTitle = "Project Development by Emphasizing on Underprivileged People and Others", ControlId = "ProjectDevelopmentEmphasizingPeople" },
                new DataAnalysisControlHeaderGroup { ControlTitle = "Administrative", ControlId = "Administrative" },
                new DataAnalysisControlHeaderGroup { ControlTitle = "Recommendation", ControlId = "Recommendation" }
            };

            return chg;
        }

        public LookUpCcModGeneralSetting GetAppGenInfo()
        {
            LookUpCcModGeneralSetting generalSetting = new LookUpCcModGeneralSetting();

            try
            {
                generalSetting = _db.LookUpCcModGeneralSetting.Find(1);
            }
            catch (Exception)
            {
                generalSetting = new LookUpCcModGeneralSetting();
                throw new NullReferenceException("Configuration object is null. Could not get configuration data from database.");
            }

            return generalSetting;
        }
    }
}
