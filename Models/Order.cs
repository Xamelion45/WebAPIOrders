using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI01.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public DateTime OrderDateTime { get; set; }
        public Customer Customer { get; set; }
        public Enums.OrderStatus Status { get; set; }
        public Address DeliveryAddress { get; set; }

        public override string ToString()
        {
            return $"{Id} {Status} {OrderDateTime}";
        }

    }
}
