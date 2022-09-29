using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobPortal.Models
{
    [Table("Admins")]
    public class Admin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdminId { get; set; }


        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "${0} Cannot be empty")]
        [StringLength(100, ErrorMessage = "{0} cannot have more than {1} Characters")]
        public string AdminUserName { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Email is InValid")]
        public string EmailAddress { get; set; }


        [Required, DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Invalid Phone Number")]
        [MinLength(10, ErrorMessage = "Invalid Phone Number"), MaxLength(10, ErrorMessage = "Invalid Phone Number")]
        public string ContactNo { get; set; }


        [Required, DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "password needs mininmum 8 characters")]
        public string AdminPassword { get; set; }


        [DataType(DataType.Password), Compare(nameof(AdminPassword), ErrorMessage = "Not Matched with Password")]
        [MinLength(8, ErrorMessage = "password needs mininmum 8 characters")]
        public string ConfirmPassword { get; set; }


   
    }
}
