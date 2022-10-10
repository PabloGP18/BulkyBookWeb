using Microsoft.AspNetCore.Razor.Language.Extensions;
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
        public int DisplayOrder { get; set; }

        // This way you can give default time the datetime now
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
