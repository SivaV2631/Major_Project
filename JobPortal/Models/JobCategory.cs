using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobPortal.Models
{
    [Table("JobCategories")]
 
    public class JobCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int JobCategoryId { get; set; }


        [Required(ErrorMessage = "Cannot be Empty"), StringLength(100)]
        public string JobCategoryName { get; set; }


        [MinLength(5), MaxLength(1000)]
        public string Description { get; set; }



        #region Navigation Properties to the transaction model - ApplyJob

        public ICollection<ApplyJob> ApplyJobs { get; set; }

        #endregion


        #region Navigation Properties to the transaction model - ApplyJob

        public ICollection<PostJob> PostJobs { get; set; }

        #endregion











    }
}
