﻿namespace DAL.Entities
{
    public class SkillEntity
    {
        public int Id { get; set; }
        public string NomFR { get; set; }
        public string NomEN { get; set; }
        public int ClasseId { get; set; }
        public string ImagePath { get; set; }
        public int Actif { get; set; }
    }
}