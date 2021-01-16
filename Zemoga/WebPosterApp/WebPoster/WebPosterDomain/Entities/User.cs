using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebPosterDomain.Entities
{
    [Table("User", Schema = "Admin")]
    public class User
    {

        [Display(Name = "Code")]
        public int ID { get; set; }

        [Display(Name = "ID Role")]
        public int IDRole { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Login")]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; }

        [NotMapped]
        [Display(Name = "Role")]
        public string Role { get; set; }

    }
}
