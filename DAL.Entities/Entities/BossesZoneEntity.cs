namespace DAL.Entities
{
    public class BossesZoneEntity
    {
        public int Id { get; set; }
        public int ZoneId { get; set; }
        public int BossId { get; set; }
        public int Active { get; set; }
    }
}