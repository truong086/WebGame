using System;
using System.Collections.Generic;

namespace WebGames.Models
{
    public partial class Sanpham
    {
        public Sanpham()
        {
            OrderDetais = new HashSet<OrderDetai>();
            Phieunhaps = new HashSet<Phieunhap>();
            Phieuxuats = new HashSet<Phieuxuat>();
        }

        public int Id { get; set; }
        public string? Ten { get; set; }
        public string? Noidung { get; set; }
        public int? Solanclick { get; set; }
        public int? Gia { get; set; }
        public DateTime? Ngaytao { get; set; }
        public int? TheloaiId { get; set; }
        public string? Hinhanh { get; set; }
        public int? Soluong { get; set; }

        public virtual Theloai? Theloai { get; set; }
        public virtual ICollection<OrderDetai> OrderDetais { get; set; }
        public virtual ICollection<Phieunhap> Phieunhaps { get; set; }
        public virtual ICollection<Phieuxuat> Phieuxuats { get; set; }
    }
}
