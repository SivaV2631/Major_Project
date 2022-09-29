using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace JobPortal.Models
{
    [Table("UserTypes")]
    public class UserType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserTypeId { get; set; }



        [Required(ErrorMessage = "UserType cannot be Empty")]
        [StringLength(50)]
        public string UserTypeName { get; set; }



        #region Navigation Properties to the transaction model - UserTable

        public ICollection<UserTable> UserTables { get; set; }

        #endregion
    }
}
