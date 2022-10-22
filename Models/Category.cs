using Microsoft.AspNetCore.Razor.Language.Extensions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyBookWeb.Models
{
    public class Category
    {
        // Data annotations for primary key
        [Key]
        public int Id { get; set; }

        // data annotation for a requirment 
        [Required]
        public string Name { get; set; }
        // DisplayName attribute is needed to display a porpety that is not the same as the property name
        [DisplayName("Display Order")]
        // Range attribute is used for choosing a range between min num and max num
        // The ErrorMessage is to give a custom error message by choice
        [Range(1,100, ErrorMessage ="Display Order must be between 1 and 100 only!!")]
        public int DisplayOrder { get; set; }

        // This way you can give default time the datetime now
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
