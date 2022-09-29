using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace JobPortal.Models
{
    [Table("PostJobs")]
    public class PostJob
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PostJobId { get; set; }


        #region Navigation Properties for model - Company
        [Required]
        public int CompanyId { get; set; }
        [ForeignKey(nameof(PostJob.CompanyId))]

        public Company Company { get; set; }
        #endregion


        [Required]
        [StringLength(100)]
        [DisplayName("Job Title")]
        public string JobTitle { get; set; }


        public int Vacancies { get; set; }

        [DataType(DataType.Date)]
        public DateTime PostCreatedAt { get; set; }

        [DataType(DataType.Date)]
        public DateTime LastDate { get; set; }



        [StringLength(50)]
        [Required]
        public string Qualification { get; set; }

        [DataType(DataType.Currency)]
        public int MinSalary { get; set; }

        [DataType(DataType.Currency)]
        public int MaxSalary { get; set; }

        [StringLength(100)]
        public string Location { get; set; }


        [DataType(DataType.Url)]
        public string WebUrl { get; set; }



        #region Navigation Properties to the Master Model - JOBREQUIREMENT

        [Required]
        public int JoRequirementId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(PostJob.JoRequirementId))]
        public JobRequirement JobRequirement { get; set; }

        #endregion

       

        [DataType(DataType.MultilineText)]
        [StringLength(500)]
        [DisplayName("Job Description")]

        public string JobDescription { get; set; }


        #region Navigation Properties for model - Jobnature
        [Required]
        public int JobNatureId { get; set; }

        [ForeignKey(nameof(PostJob.JobNatureId))]
        [DisplayName("Job Type")]
        public JobNature JobNature { get; set; }
        #endregion

        #region Navigation Properties for model - JobCategory
        [Required]
        public int JobCategoryId { get; set; }
        [ForeignKey(nameof(PostJob.JobCategoryId))]
        [DisplayName("Job Category")]
        public JobCategory JobCategory { get; set; }
        #endregion



        #region Navigation Properties to the  Model - ApplyJob

        public ICollection<ApplyJob> ApplyJobs { get; set; }

        #endregion






    }
}
