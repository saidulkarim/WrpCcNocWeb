using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models
{
    public class ProjectLocationTemp
    {
        public long LocationId { get; set; }
        public long ProjectId { get; set; }
        public string DistrictGeoCode { get; set; }
        public string DistrictName { get; set; }
        public string DistrictNameBn { get; set; }
        public string UpazilaGeoCode { get; set; }
        public string UpazilaName { get; set; }
        public string UpazilaNameBn { get; set; }
        public string UnionGeoCode { get; set; }
        public string UnionName { get; set; }
        public string UnionNameBn { get; set; }
        public string Latitude { get; set; }
        public string LatitudeBn { get; set; }
        public string Longitude { get; set; }
        public string LongitudeBn { get; set; }
        public string ImageFileName { get; set; }
        public string OnlyImageFileName { get; set; }
        public string Error { get; set; }
    }

    public class ProjectLocationsTemp
    {
        public long LocationId { get; set; }       
        public string DistrictGeoCode { get; set; }       
        public string UpazilaGeoCode { get; set; }       
        public string UnionGeoCode { get; set; }       
        public string Latitude { get; set; }      
        public string Longitude { get; set; }    
        public string ImageFile { get; set; }      
        public string Error { get; set; }
    }
}