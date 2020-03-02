using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WrpCcNocWeb.Models
{
    public class CcModPrjCompatNWMPDetail
    {
        [Key]
        [Column("PrjCompatNWMPId", Order = 0)]
        public long PrjCompatNWMPId { get; set; }


        [Required]
        [Column("ProjectId", Order = 1)]
        [Display(Name = "ProjectId")]
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual CcModAppProjectCommonDetail CcModAppProjectCommonDetail { get; set; }


        [Required]
        [Column("NWMPProgrammeId", Order = 2)]
        [Display(Name = "NWMP Programme")]
        public int NWMPProgrammeId { get; set; }
        [ForeignKey("NWMPProgrammeId")]
        public virtual LookUpCcModNWMPProgramme LookUpCcModNWMPProgramme { get; set; }
    }
}
