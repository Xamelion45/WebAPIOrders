using System.ComponentModel.DataAnnotations;

namespace WebAPI01.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public string Room { get; set; }
        public int Index { get; set; }

        public override string ToString()
        {
            return $"{Id} {Index}";
        }
    }
}