using System;
using System.Collections.Generic;

namespace WebGames.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetais = new HashSet<OrderDetai>();
        }

        public int Id { get; set; }
        public string? Ten { get; set; }
        public int? Statuc { get; set; }
        public DateTime? Ngaytao { get; set; }
        public int? UsersId { get; set; }

        public virtual Takhoan? Users { get; set; }
        public virtual ICollection<OrderDetai> OrderDetais { get; set; }
    }
}
