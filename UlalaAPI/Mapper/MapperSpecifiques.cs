using DAL.Entities;
using DAL.Services;
using UlalaAPI.Models;

namespace UlalaAPI.Mapper
{
    public static class MapperSpecifiques
    {
        #region Map BossZone E to M
        public static BossZoneModel ToModel(this BossZoneEntity BZE)
        {
            if (BZE != null)
            {
                BossRepository repoBoss = new BossRepository();
                ZoneRepository repoZone = new ZoneRepository();
                BossZoneModel BZM = new BossZoneModel();
                BZM.Boss = repoBoss.GetOne(BZE.BossId).MapTo<BossModel>();
                BZM.Zone = repoZone.GetOne(BZE.ZoneId).MapTo<ZoneModel>();
                BZM.Actif = BZE.Actif;
                BZM.Id = BZE.Id;
                return BZM;
            }
            else return null;
        }
        #endregion

        #region Map BossZone M to E
        public static BossZoneEntity ToEntity(this BossZoneModel BZM)
        {
            if (BZM != null)
            {
                BossZoneEntity BZE = new BossZoneEntity();
                BZE.BossId = BZM.Zone.Id;
                BZE.ZoneId = BZM.Boss.Id;
                BZE.Id = BZM.Id;
                BZE.Actif = BZM.Actif;
                return BZE;
            }
            else return null;
        }
        #endregion

        #region Map Enregistrement E to M
        public static EnregistrementModel ToModel(this EnregistrementEntity EE)
        {
            if (EE != null)
            {
                BossZoneRepository repoBZ = new BossZoneRepository();
                TeamRepository repoTeam = new TeamRepository();
                UtilisateurRepository repoU = new UtilisateurRepository();
                EnregistrementModel EM = new EnregistrementModel();
                EM.Utilisateur = repoU.GetOne(EE.UtilisateurId).MapTo<UtilisateurModel>();
                EM.Team = repoTeam.GetOne(EE.TeamId).ToModel();
                EM.BossZone = repoBZ.GetOne(EE.BossZoneId).ToModel();
                EM.ImagePath1 = EE.ImagePath1;
                EM.ImagePath2 = EE.ImagePath2;
                EM.ImagePath3 = EE.ImagePath3;
                EM.ImagePath4 = EE.ImagePath4;
                EM.Note = EE.Note;
                EM.Id = EE.Id;
                EM.Actif = EE.Actif;
                return EM;
            }
            else return null;
        }
        #endregion

        #region Map Enregistrement M to E
        public static EnregistrementEntity ToEntity(this EnregistrementModel EM)
        {
            if (EM != null)
            {
                EnregistrementEntity EE = new EnregistrementEntity();
                EE.UtilisateurId = EM.Utilisateur.Id;
                EE.TeamId = EM.Team.Id;
                EE.BossZoneId = EM.BossZone.Id;
                EE.ImagePath1 = EM.ImagePath1;
                EE.ImagePath2 = EM.ImagePath2;
                EE.ImagePath3 = EM.ImagePath3;
                EE.ImagePath4 = EM.ImagePath4;
                EE.Note = EM.Note;
                EE.Id = EM.Id;
                EE.Actif = EM.Actif;
                return EE;
            }
            else return null;
        }
        #endregion

        #region Map Favori E to M
        public static FavoriModel ToModel(this FavoriEntity FE)
        {
            if (FE != null)
            {
                EnregistrementRepository repoEnre = new EnregistrementRepository();
                UtilisateurRepository repoUtil = new UtilisateurRepository();
                FavoriModel FM = new FavoriModel();
                FM.Enregistrement = repoEnre.GetOne(FE.EnregistrementId).ToModel();
                FM.Utilisateur = repoUtil.GetOne(FE.UtilisateurId).MapTo<UtilisateurModel>();
                FM.Id = FE.Id;
                FM.Actif = FE.Actif;
                return FM;
            }
            else return null;
        }
        #endregion

        #region Map Favori M to E
        public static FavoriEntity ToEntity(this FavoriModel FM)
        {
            if (FM != null)
            {
                FavoriEntity FE = new FavoriEntity();
                FE.EnregistrementId = FM.Enregistrement.Id;
                FE.UtilisateurId = FM.Utilisateur.Id;
                FE.Id = FM.Id;
                FE.Actif = FM.Actif;
                return FE;
            }
            else return null;
        }
        #endregion

        #region Map Vote E to M
        public static VoteModel ToModel(this VoteEntity VE)
        {
            if (VE != null)
            {
                EnregistrementRepository repoEnre = new EnregistrementRepository();
                UtilisateurRepository repoUtil = new UtilisateurRepository();
                VoteModel VM = new VoteModel();
                VM.Enregistrement = repoEnre.GetOne(VE.EnregistrementId).ToModel();
                VM.Id = VE.Id;
                VM.Utilisateur = repoUtil.GetOne(VE.UtilisateurId).MapTo<UtilisateurModel>();
                VM.Vote = VE.Vote;
                VM.Actif = VE.Actif;
                return VM;
            }
            else return null;
        }
        #endregion

        #region Map Vote M to E
        public static VoteEntity ToEntity(this VoteModel VM)
        {
            if (VM != null)
            {
                VoteEntity VE = new VoteEntity();
                VE.EnregistrementId = VM.Enregistrement.Id;
                VE.UtilisateurId = VM.Utilisateur.Id;
                VE.Id = VM.Id;
                VE.Vote = VM.Vote;
                VE.Actif = VM.Actif;
                return VE;
            }
            else return null;
        }
        #endregion

        #region Map Skill E to M
        public static SkillModel ToModel(this SkillEntity SE)
        {
            if (SE != null)
            {
                ClasseRepository repoCl = new ClasseRepository();
                SkillModel SM = new SkillModel();
                SM.Id = SE.Id;
                SM.NomEN = SE.NomEN;
                SM.NomFR = SE.NomFR;
                SM.ImagePath = SE.ImagePath;
                SM.Classe = repoCl.GetOne(SE.ClasseId).MapTo<ClasseModel>();
                SM.Actif = SE.Actif;
                return SM;
            }
            else return null;
        }
        #endregion

        #region Map Skill M to E
        public static SkillEntity ToEntity(this SkillModel SM)
        {
            if (SM != null)
            {
                SkillEntity SE = new SkillEntity();
                SE.Id = SM.Id;
                SE.NomEN = SM.NomEN;
                SE.NomFR = SM.NomFR;
                SE.ImagePath = SM.ImagePath;
                SE.ClasseId = SM.Classe.Id;
                SE.Actif = SM.Actif;
                return SE;
            }
            else return null;
        }
        #endregion

        #region Map Team E to M
        public static TeamModel ToModel(this TeamEntity TE)
        {
            if (TE != null)
            {
                ClasseRepository repoCl = new ClasseRepository();
                TeamModel TM = new TeamModel();
                TM.Id = TE.Id;
                TM.Classe1 = repoCl.GetOne(TE.ClasseId1).MapTo<ClasseModel>();
                TM.Classe2 = repoCl.GetOne(TE.ClasseId2).MapTo<ClasseModel>();
                TM.Classe3 = repoCl.GetOne(TE.ClasseId3).MapTo<ClasseModel>();
                TM.Classe4 = repoCl.GetOne(TE.ClasseId4).MapTo<ClasseModel>();
                TM.Actif = TE.Actif;
                return TM;
            }
            else return null;
        }
        #endregion

        #region Map Team M to E
        public static TeamEntity ToEntity(this TeamModel TM)
        {
            if (TM != null)
            {
                TeamEntity TE = new TeamEntity();
                TE.Id = TM.Id;
                TE.ClasseId1 = TM.Classe1.Id;
                TE.ClasseId2 = TM.Classe2.Id;
                TE.ClasseId3 = TM.Classe3.Id;
                TE.ClasseId4 = TM.Classe4.Id;
                TE.Actif = TM.Actif;
                return TE;
            }
            else return null;
        }
        #endregion

        #region Map MaTeam E to M
        public static MesTeamsModel ToModel(this MesTeamsEntity TE)
        {
            if (TE != null)
            {
                ZoneRepository repoZ = new ZoneRepository();
                UtilisateurRepository repoU = new UtilisateurRepository();
                TeamRepository repoT = new TeamRepository();
                MesTeamsModel TM = new MesTeamsModel();
                TM.Id = TE.Id;
                TM.Team = repoT.GetOne(TE.TeamId).ToModel();
                TM.NomTeam = TE.NomTeam;
                TM.Zone = repoZ.GetOne(TE.ZoneId).MapTo<ZoneModel>();
                TM.Utilisateur = repoU.GetOne(TE.UtilisateurId).MapTo<UtilisateurModel>();
                TM.Actif = TE.Actif;
                return TM;
            }
            else return null;
        }
        #endregion

        #region Map MaTeam M to E
        public static MesTeamsEntity ToEntity(this MesTeamsModel TM)
        {
            if (TM != null)
            {
                MesTeamsEntity TE = new MesTeamsEntity();
                TE.Id = TM.Id;
                TE.TeamId = TM.Team.Id;
                TE.NomTeam = TM.NomTeam;
                TE.ZoneId = TM.Zone.Id;
                TE.UtilisateurId = TM.Utilisateur.Id;
                TE.Actif = TM.Actif;
                return TE;
            }
            else return null;
        }
        #endregion
    }
}