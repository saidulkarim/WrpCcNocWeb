using System.ComponentModel;

namespace WrpCcNocWeb.Models
{
    public class ProjectHydrologicalRegionTemp
    {
        public int HydroRegionId { get; set; }
        public long ProjectId { get; set; }
        public string HydroRegionShortName { get; set; }
        public string HydroRegionShortNameBn { get; set; }
        public string HydroRegionFullName { get; set; }
        public string HydroRegionFullNameBn { get; set; }

        [DefaultValue(false)]
        public bool IsSelected { get; set; }
    }

    public class BDP2100HotSpotDetailTemp
    {
        public int DeltaPlanHotSpotId { get; set; }
        public string PlanName { get; set; }
        public string PlanNameBn { get; set; }
    }

    public class HydroSystemDetailTemp
    {
        public long HydroSysDetailId { get; set; }
        public long ProjectId { get; set; }
        public int HydroSystemCategoryId { get; set; }
        public string HydroSystemCategory { get; set; }
        public string HydroSystemCategoryBn { get; set; }
        public string NameOfHydroSystem { get; set; }
        public string HydroSystemLengthArea { get; set; }
        public string HydroSystemUnit { get; set; }
        public string HydroSystemUnitBn { get; set; }
    }

    public class TypesOfFloodTemp
    {
        public long FloodTypeDetailId { get; set; }
        public long ProjectId { get; set; }
        public int FloodTypeId { get; set; }
        public string FloodTypeName { get; set; }
        public string FloodTypeNameBn { get; set; }
    }

    public class FloodFrequencyDetailTemp
    {
        public long FloodFrequencyDetailId { get; set; }
        public long ProjectId { get; set; }
        public int FloodFrequencyId { get; set; }
        public string FloodFrequency { get; set; }
        public string FloodFrequencyBn { get; set; }
        public string FloodFrequencyLevel { get; set; }        
    }
}
