namespace UlalaAPI.Models
{
    public class BossesZoneModel
    {
        public int Id { get; set; }
        public ZoneModel Zone { get; set; }
        public BossModel Boss { get; set; }
        public int Active { get; set; }
    }
}