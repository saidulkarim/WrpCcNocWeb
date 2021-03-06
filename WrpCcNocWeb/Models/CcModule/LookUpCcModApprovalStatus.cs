﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models.CcModule
{
    public class LookUpCcModApprovalStatus
    {
        [Key]
        [Column("ApprovalStatusId", Order = 0)]
        public int ApprovalStatusId { get; set; }

                
        [Column("ApprovalStatus", Order = 1)]
        [MaxLength(50)]
        [Display(Name = "Approval Status")]
        public string ApprovalStatus { get; set; }

        
        [Column("ApprovalStatusBn", Order = 2)]
        [MaxLength(100)]
        [Display(Name = "Approval Status")]
        public string ApprovalStatusBn { get; set; }
    }
}