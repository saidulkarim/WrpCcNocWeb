using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WrpCcNocWeb.Models
{
    public class AdminModErrorLogDetail
    {
        [Key]        
        [Column("ErrorLogId", Order = 0)]        
        public long ErrorLogId { get; set; }
        
        [Required]
        [Column("UserId", Order = 1)]    
        [MaxLength(20)]
        [Display(Name = "User Name")]
        public long UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual AdminModUsersDetail adminModUsersDetails { get; set; }         

        [Required]        
        [Column("ErrorTitle", Order = 2)]
        [MaxLength(100)]
        [Display(Name = "Error Title")]
        public string ErrorTitle { get; set; }
        
         
        [Column("ErrorDescription", Order = 3)]
        [MaxLength(500)]
        [Display(Name = "Error Description")]
        public string ErrorDescription { get; set; }

        
        [Column("ErrorOccuredTime", Order = 4)]
        [Display(Name = "Error Occured Time")]        
        public DateTime ErrorOccuredTime { get; set; }
        
        
        [Column("ErrorSolvingStatusId", Order = 5)]
        [Display(Name = "Error Solving Status")]        
        public int ErrorSolvingStatusId { get; set; }
        [ForeignKey("ErrorSolvingStatusId")]
        public virtual LookUpAdminModErrorSlvStatus lookUpAdminModErrorSlvStatus { get; set; }
    }
}
