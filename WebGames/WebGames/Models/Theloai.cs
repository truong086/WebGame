using System;
using System.Collections.Generic;

namespace WebGames.Models
{
    public partial class Theloai
    {
        public Theloai()
        {
            Sanphams = new HashSet<Sanpham>();
        }

        public int Id { get; set; }
        public string? Ten { get; set; }
        public DateTime? Ngaytao { get; set; }

        public virtual ICollection<Sanpham> Sanphams { get; set; }
    }
}
