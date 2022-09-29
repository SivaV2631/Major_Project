using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobPortal.Models
{
    [Table("JobNatures")]
    public class JobNature
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int JobNatureId { get; set; }

        [Required]
        [StringLength(50)]
        public string JobNatureName { get; set; }


        #region Navigation properties from model - WORK EXPERIENCE

        public ICollection<PostJob> PostJobs { get; set; }
        #endregion


        #region Navigation Properties to the  Model - ApplyJob

        public ICollection<ApplyJob> ApplyJobs { get; set; }

        #endregion
    }
}
