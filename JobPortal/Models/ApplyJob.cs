using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace JobPortal.Models
{
    [Table("ApplyJobs")]
    public class ApplyJob
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ApplyJobId { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Candidate Name")]
        public string ApplicantName { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Invalid Phone")]
        [DisplayName("Contact Number")]
        public string ContactNo { get; set; }



        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Email is InValid")]
        public string Email { get; set; }


        [DataType(DataType.DateTime)]
        [DisplayName("Applied On")]
        public DateTime AppliedAt { get; set; } = DateTime.Now;





        #region Navigation Properties to the Master Model - PostJob

        [Required]
        public int PostJobId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(ApplyJob.PostJobId))]
        [DisplayName("Job Title")]
        public PostJob PostJob { get; set; }

        #endregion

     
    }
}
