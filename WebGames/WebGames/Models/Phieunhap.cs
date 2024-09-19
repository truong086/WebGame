using System;
using System.Collections.Generic;

namespace WebGames.Models
{
    public partial class Phieunhap
    {
        public int Id { get; set; }
        public int? SanphamId { get; set; }
        public int? Soluong { get; set; }
        public int? NoinhapId { get; set; }
        public int? NguoinhapId { get; set; }
        public int? Tongtien { get; set; }
        public DateTime? Ngaynhap { get; set; }

        public virtual Takhoan? Nguoinhap { get; set; }
        public virtual Noinhap? Noinhap { get; set; }
        public virtual Sanpham? Sanpham { get; set; }
    }
}
