namespace WebGames.Entity
{
    public class Cart
    {
        public int Mahh { get; set; }
        public string tieude { get; set; }
        public string hinhanh { get; set; }
        public int soluong { get; set; }
        public double gia { get; set; }
        public double tonggia => soluong * gia;
    }
}
