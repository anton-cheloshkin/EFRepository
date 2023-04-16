using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Example.Domain.Models;

namespace Example.Persistance.Models
{
    [Table("OnePK")]
    public class OnePKEntity
    {
        [Key]
        public int Id { get; set; }
        [NotMapped]
        public string? Name { get; set; }
        [NotMapped]
        public virtual TestObject Data { get; set; }
    }
}
