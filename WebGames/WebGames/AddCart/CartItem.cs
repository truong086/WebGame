using System.Text.Json;
namespace WebGames.AddCart
{
    public static class CartItem
    {
        public static string ChuyenDoiGia(this double gia)
        {
            return $"{gia:#.##0.00} Đ";
        }

        public static void Set<T>(this ISession session, String key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T? Get<T> (this ISession session, String key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}
