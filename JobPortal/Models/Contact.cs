using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobPortal.Models
{
    [Table("Contacts")]
    public class Contact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContactId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Email is InValid")]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(100)]
        [DataType(DataType.Text)]
        public string Subject { get; set; }


        [Required]
        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }


    }
}
