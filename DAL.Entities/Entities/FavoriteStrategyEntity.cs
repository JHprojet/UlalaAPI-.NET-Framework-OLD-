namespace DAL.Entities
{
    public class FavoriteStrategyEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int StrategyId { get; set; }
        public int Active { get; set; }
    }
}