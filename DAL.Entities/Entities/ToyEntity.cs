namespace DAL.Entities
{
    public class ToyEntity
    {
        public int Id { get; set; }
        public string NameFR { get; set; }
        public string NameEN { get; set; }
        public string ImagePath { get; set; }
        public int Active { get; set; }
    }
}