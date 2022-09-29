using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobPortal.Models
{
    [Table("JobRequirements")]
    public class JobRequirement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int JobRequirementId { get; set; }

        [Required]
        [StringLength(100)]
        public string JobRequirementTitle { get; set; }

        [Required]
        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        public string JobRequirementDescription { get; set; }

    }
}
