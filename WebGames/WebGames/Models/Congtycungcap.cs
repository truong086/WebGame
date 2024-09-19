using System;
using System.Collections.Generic;

namespace WebGames.Models
{
    public partial class Congtycungcap
    {
        public Congtycungcap()
        {
            Phieuxuats = new HashSet<Phieuxuat>();
        }

        public int Id { get; set; }
        public string? Ten { get; set; }
        public string? Diachi { get; set; }
        public string? Email { get; set; }
        public DateTime? Ngaytao { get; set; }

        public virtual ICollection<Phieuxuat> Phieuxuats { get; set; }
    }
}
