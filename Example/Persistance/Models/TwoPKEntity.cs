using System.ComponentModel.DataAnnotations;
using Example.Domain.Models;

namespace Example.Persistance.Models
{
    public enum TestEnum
    {
        One, Two
    }
    public class TwoPKEntity
    {
        [Key]
        public int Id { get; set; }
        [Key]
        public string Second { get; set; }
        public string? Name { get; set; }
        public virtual List<TestObject> Data { get; set; }
    }
}
