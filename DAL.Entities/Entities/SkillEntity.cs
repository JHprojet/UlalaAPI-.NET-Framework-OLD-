namespace DAL.Entities
{
    public class SkillEntity
    {
        public int Id { get; set; }
        public string NameFR { get; set; }
        public string NameEN { get; set; }
        public string DescriptionFR { get; set; }
        public string DescriptionEN { get; set; }
        public int Cost { get; set; }
        public string Location { get; set; }
        public int ClasseId { get; set; }
        public string ImagePath { get; set; }
        public int Active { get; set; }
    }
}