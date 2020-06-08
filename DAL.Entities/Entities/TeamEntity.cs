namespace DAL.Entities
{
    public class TeamEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CharactersConfigurationId { get; set; }
        public int ZoneId { get; set; }
        public string TeamName { get; set; }
        public int Active { get; set; }
    }
}