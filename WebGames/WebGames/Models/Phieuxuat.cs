using System;
using System.Collections.Generic;

namespace WebGames.Models
{
    public partial class Phieuxuat
    {
        public int Id { get; set; }
        public int? SanphamId { get; set; }
        public int? Soluong { get; set; }
        public int? CongtycungcapId { get; set; }
        public string? Ghichu { get; set; }
        public int? NguoicapId { get; set; }
        public int? Tongtien { get; set; }
        public DateTime? Ngaycap { get; set; }

        public virtual Congtycungcap? Congtycungcap { get; set; }
        public virtual Takhoan? Nguoicap { get; set; }
        public virtual Sanpham? Sanpham { get; set; }
    }
}
