namespace UlalaAPI.Models
{
    public class SkillModel
    {
        public int Id { get; set; }
        public string NomFR { get; set; }
        public string NomEN { get; set; }
        public ClasseModel Classe { get; set; }
        public string ImagePath { get; set; }
        public int Actif { get; set; }
    }
}