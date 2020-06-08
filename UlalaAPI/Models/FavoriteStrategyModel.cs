namespace UlalaAPI.Models
{
    public class FavoriteStrategyModel
    {
        public int Id { get; set; }
        public UserModel User { get; set; }
        public StrategyModel Strategy { get; set; }
        public int Active { get; set; }
    }
}