using System;

namespace ProjetGSB
{
    /// <summary>
    /// Classe qui regroupe les méthodes de gestion des dates 
    /// </summary>
    public class GestionDates
    {
        /// <summary>
        /// constructeur qui initialise une nouvelle instance de la classe GestionDates
        /// </summary>
        public GestionDates()
        {

        }

        /// <summary>
        /// méthode qui définie le mois précedent au mois actuel sous la forme: 01 pour janvier, 02 pour février...
        /// </summary>
        /// <returns>une chaine de 2 chiffres sous la forme: 01 pour janvier</returns>
        public static string GetMoisPrecedent ()
        {
            return GetMoisPrecedent(DateTime.Today);
        }

        /// <summary>
        /// méthode qui définie le mois précedent au mois passé en paramètre sous la forme: 01 pour janvier, 02 pour février...
        /// </summary>
        /// <param name="date">un datetime sous la forme (2008/11/21) </param>
        /// <returns>une chaine de 2 chiffres sous la forme: 01 pour janvier</returns>
        public static string GetMoisPrecedent(DateTime date)
        {
            return RetireUnAuMois(date.Month);
        }

        /// <summary>
        /// Méthode qui retire 1 au mois passé en paramètre
        /// </summary>
        /// <param name="mois"> mois auquel il faut soustraire 1</param>
        /// <returns>le mois précedent au format chaine 01 pour janvier...</returns>
        private static string RetireUnAuMois (int mois)
        {
            string moisPrecedent;

            if (mois != 1)
            {
                if (mois < 11)
                {
                    moisPrecedent = $"0{mois - 1}";
                }
                else
                {
                    moisPrecedent = $"{mois - 1}";
                }
            }
            else
            {
                moisPrecedent = "12";
            }
            return moisPrecedent;
        }


        /// <summary>
        /// méthode qui définie le mois suivant au mois actuel sous la forme: 01 pour janvier, 02 pour février...
        /// </summary>
        /// <returns>une chaine de 2 chiffres sous la forme: 01 pour janvier</returns>
        public static string GetMoisSuivant()
        {
            return GetMoisSuivant(DateTime.Today);
        }

        /// <summary>
        /// méthode qui définit le mois suivant au mois passé en paramètre sous la forme: 01 pour janvier, 02 pour février...
        /// </summary>
        /// <param name="mois">un mois sous la forme 01, 02...</param>
        /// <returns>une chaine de 2 chiffres sous la forme: 01 pour janvier</returns>
        public static string GetMoisSuivant(DateTime mois)
        {
            return AjouteUnAuMois(mois.Month);
        }

        /// <summary>
        /// Méthode qui ajoute 1 au mois passé en paramètre
        /// </summary>
        /// <param name="mois"> mois auquel il faut ajouter 1</param>
        /// <returns>le mois suivant au format chaine 01 pour janvier...</returns>
        private static string AjouteUnAuMois(int mois)
        {
            string moisSuivant;

            if (mois != 12)
            {
                if (mois < 9)
                {
                    moisSuivant = $"0{mois + 1}";
                }
                else
                {
                    moisSuivant = $"{mois + 1}";
                }
            }
            else
            {
                moisSuivant = "01";
            }

            return moisSuivant;
        }

        /// <summary>
        /// Méthode qui teste si le jour actuel est compris entre les jours passés en paramètres
        /// </summary>
        /// <param name="numJour1">numero du jour 1</param>
        /// <param name="numJour2">numéro du jour 2</param>
        /// <returns> Vrai si le jour actuel est compris entre les jours 1 et 2</returns>
        public static bool Entre (int numJour1, int numJour2)
        {
            return VerifieEntre(numJour1, numJour2, DateTime.Now.Day);
        }

        /// <summary>
        /// Méthode qui teste si le jour de la datetime passée en paramètre est compris entre les jours passés en paramètres
        /// </summary>
        /// <param name="numJour1">numero du jour 1</param>
        /// <param name="numJour2">numéro du jour 2</param>
        /// <param name="date"> date au forùat datetime</param>
        /// <returns>Vrai si le jour de la datetime passée en paramètre est compris entre les jours 1 et 2</returns>
        public static bool Entre(int numJour1, int numJour2, DateTime date)
        {
            int jour = date.Day;
            return VerifieEntre(numJour1, numJour2, jour);
        }

        /// <summary>
        /// Méthode static privée qui vérifie si le numaTester est compris entre le numPlusPetit et le numPlusGrand
        /// </summary>
        /// <param name="nbPlusPetit">nb qui doit être plus petit que le nb à tester</param>
        /// <param name="nbPlusgrand">nb qui doit être plus grand que le nb à tester</param>
        /// <param name="nbaTester">nombre à tester</param>
        /// <returns>vrai si le numaTester est compris entre le numPlusPetit et le numPlusGrand </returns>
        private static bool VerifieEntre(int nbPlusPetit, int nbPlusgrand, int nbaTester)
        {
            if (nbPlusPetit <= nbaTester && nbaTester <= nbPlusgrand)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
