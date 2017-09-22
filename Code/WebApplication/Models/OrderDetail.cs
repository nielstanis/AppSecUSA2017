using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class OrderDetail
    {
        public int ID { get; set; }
        public Order Order { get; set; }
        [Range(0,1000)]
        public int Ammount { get; set; }
        public decimal PricePerUnit { get; set; }
        [StringLength(255)]
        public string Remark { get; set; }
    }
}
