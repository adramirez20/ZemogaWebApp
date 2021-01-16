using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebPosterDomain.Entities
{
    [Table("Poster", Schema = "Core")]
    public class Poster
    {
        [Display(Name = "Code")]
        public int ID { get; set; }

        [Display(Name = "Status")]
        public int StatusID { get; set; }

        [Display(Name = "CreatedUser")]
        public int CreatedByUserID { get; set; }

        [Display(Name = "ModifiedUser")]
        public int? ModifiedByUserID { get; set; }

        [Display(Name = "Content")]
        public string Content { get; set; }

        [Display(Name = "Active")]
        public Boolean Active { get; set; }

        [Display(Name = "Created")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Approval Date")]
        public DateTime? ModifiedDate { get; set; }

        [Display(Name = "Comments")]
        public  List<Comment> Comments { get; set; }

        [NotMapped]
        [Display(Name = "Writer")]
        public string Writer { get; set; }



    }
}
