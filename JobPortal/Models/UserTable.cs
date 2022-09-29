using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace JobPortal.Models
{
    [Table("UserTables")]
    public class UserTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }


        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "${0} Cannot be empty")]
        [StringLength(100, ErrorMessage = "{0} cannot have more than {1} Characters")]
        public string UserName { get; set; }


        [Required, DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "password needs mininmum 8 characters")]
        public string Password { get; set; }


        [DataType(DataType.Password), Compare(nameof(Password), ErrorMessage = "Not Matched with Password")]
        [MinLength(8, ErrorMessage = "password needs mininmum 8 characters")]
        public string ConfirmPassword { get; set; }


        [Required, DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Email is InValid")]
        public string EmailAddress { get; set; }


        [Required, DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Invalid Phone Number")]
        [MinLength(10, ErrorMessage = "Invalid Phone Number"), MaxLength(10, ErrorMessage = "Invalid Phone Number")]
        public string ContactNo { get; set; }



        [DataType(DataType.MultilineText)]
        [StringLength(500)]
        public string Description { get; set; }


        [DataType(DataType.Text)]
        [StringLength(200)]
        public string UniversityName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public string PassOutYear { get; set; }

        public string Branch { get; set; }

        public decimal Percentage { get; set; }

        public string Gender { get; set; }





        [DataType(DataType.MultilineText)]
        [StringLength(500)]
        public string EducationDetails { get; set; }



        #region Navigation Properties to the Master Model - UserType

        [Required]
        public int UserTypeId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(UserTable.UserTypeId))]
        public UserType UserType { get; set; }

        #endregion





        #region  #region Navigation Properties to the  Model - Company

        public ICollection<Company> Companies { get; set; }

        #endregion


        #region Navigation Properties to the  Model - PostJob

        public ICollection<PostJob> PostJobs { get; set; }

        #endregion

        #region Navigation Properties to the  Model - ApplyJob

        public ICollection<ApplyJob> ApplyJobs { get; set; }

        #endregion



    }
}
