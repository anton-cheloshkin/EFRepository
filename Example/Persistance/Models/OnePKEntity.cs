using System.ComponentModel.DataAnnotations;
using Example.Domain.Models;

namespace Example.Persistance.Models
{
    public class OnePKEntity
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public virtual TestObject Data { get; set; }
    }
}
