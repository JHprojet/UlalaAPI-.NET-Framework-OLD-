namespace UlalaAPI.Models
{
    public class BossZoneModel
    {
        public int Id { get; set; }
        public ZoneModel Zone { get; set; }
        public BossModel Boss { get; set; }
        public int Actif { get; set; }
    }
}