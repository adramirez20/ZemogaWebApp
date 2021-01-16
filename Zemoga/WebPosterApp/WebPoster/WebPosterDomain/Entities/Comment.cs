using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebPosterDomain.Entities
{
    [Table("Comment", Schema = "Core")]
    public class Comment
    {

        [Display(Name = "Code")]
        public int ID { get; set; }

        [Display(Name = "Code Post")]
        public int PosterID { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "AddedDate")]
        public DateTime AddedDate { get; set; }
    }
}
