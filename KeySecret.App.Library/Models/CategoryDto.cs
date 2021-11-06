using System;

namespace KeySecret.App.Library.Models
{
    public class CategoryDto
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}