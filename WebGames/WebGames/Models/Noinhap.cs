using System;
using System.Collections.Generic;

namespace WebGames.Models
{
    public partial class Noinhap
    {
        public Noinhap()
        {
            Phieunhaps = new HashSet<Phieunhap>();
        }

        public int Id { get; set; }
        public string? Ten { get; set; }
        public DateTime? Ngaytao { get; set; }

        public virtual ICollection<Phieunhap> Phieunhaps { get; set; }
    }
}
