using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace JobPortal.Models
{
    [Table("Companies")]
    public class Company
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CompanyId { get; set; }


        [Display(Name = "Company Name")]
        [Required(ErrorMessage = "Company Name Cannot be Empty")]
        public string CompanyName { get; set; }


        [Display(Name = "Contact No")]
        [Required(ErrorMessage = "Contact No is required")]
        [RegularExpression(@"^[0-9]{10}$",ErrorMessage ="Invalid")]
        public string ContactNo { get; set; }


        [Display(Name ="Email Id")]
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Email is InValid")]
        public string EmailAddress { get; set; }


        [DataType(DataType.Url)]
        [Display(Name ="Company Website")]
        public string CompanyUrl { get; set; }

        [Column(TypeName ="nvarchar(100)")]
        [Display(Name ="Image Name")]
        public string CompanyImage { get; set; }
 
        
        [NotMapped]
        [Display(Name ="Company Image")]
        public IFormFile ImageFile { get; set; }


        [MinLength(10), MaxLength(500)]
        [DataType(DataType.MultilineText)]

        public string Description { get; set; }

        #region Navigation Properties to the Master Model - UserTable

        [Required]
        public int UserId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(Company.UserId))]
        [Display(Name ="User Name")]
        public UserTable UserTable { get; set; }

        #endregion

        #region Navigation Properties to the  Model - ApplyJob

        public ICollection<ApplyJob> ApplyJobs { get; set; }

        #endregion



    }
}
