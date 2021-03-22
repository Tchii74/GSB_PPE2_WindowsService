using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace ProjetGSB
{
    /// <summary>
    /// classe de gestion de connexion à la base de données
    /// </summary>
    class PdoGsb
    {
        /// <summary>
        /// chaine de connexion au serveur MySql
        /// </summary>
        private string ChaineConnexion { get; set; }

        /// <summary>
        /// nom du serveur MySql
        /// </summary>
        private static readonly string server = "localhost";

        /// <summary>
        /// identifiant utilisateur
        /// </summary>
        private static readonly string user = "root";

        /// <summary>
        /// mot de passe utilisateur
        /// </summary>
        private static readonly string mdp = "";

        /// <summary>
        /// nom de la base de donnée à laquelle se connecter
        /// </summary>
        private static readonly string database = "gsb_frais";

        /// <summary>
        /// instance unique de connexion à la base de données
        /// </summary>
        private static PdoGsb instancePdoGsb = null;

        /// <summary>
        /// connexion mysql
        /// </summary>
        private MySqlConnection connexion;


        /// <summary>
        /// Constructeur privé, sert à créer l'instance Pdo utilisé par les autres méthodes de la classe
        /// </summary>
        private PdoGsb()
        {
            this.InitConnexion();
        }

        /// <summary>
        /// Méthode pour initialiser une connexion
        /// </summary>
        private void InitConnexion()
        {
            MySqlConnectionStringBuilder connectionString = new MySqlConnectionStringBuilder
            {
                Server = server,
                UserID = user,
                Password = mdp,
                Database = database
            };

            this.ChaineConnexion = connectionString.ToString();
            this.connexion = new MySqlConnection(this.ChaineConnexion);
        }

        /// <summary>
        /// Destructeur de la classe Pdo
        /// </summary>
        public void PdoDestructeur()
        {
            this.ChaineConnexion = null;
        }

        /// <summary>
        /// (singleton) méthode qui verifie si une instance existe déjà, si ce n'est pas le cas elle en crée une
        /// </summary>
        /// <returns>retourne l'unique instance PdoGsb</returns>
        public static PdoGsb GetInstancePdoGsb()
        {
            if (instancePdoGsb == null)
            {
                instancePdoGsb = new PdoGsb();
            }
            return instancePdoGsb;
        }

        /// <summary>
        /// méthode qui ouvre une connexion mysql
        /// </summary>
        public void OpenMySqlConnexion()
        {
            try
            {
                this.connexion.Open();
            }

            catch
            (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Impossible de se connecter au serveur.");
                        break;
                    case 1045:
                        Console.WriteLine("Identifiant/Mot de passe invalide");
                        break;
                }
            }
        }

        

        /// <summary>
        /// Méthode qui execute une requete Update, Delete ou Insert
        /// </summary>
        /// <param name="requete"></param>
        public void ExecuteRequeteAdministration(string requete) 
        {
            //ouvre la connexion
            OpenMySqlConnexion();

            //tente execute la requete
            try
            {
                using MySqlCommand commande = this.connexion.CreateCommand();
                commande.CommandText = requete;

                commande.ExecuteNonQuery();

            }
            catch
            {
                
            }

            //ferme la connexion
            this.connexion.Close();

        }

        /// <summary>
        /// méthode qui éxécute une requète SQL de type select
        /// </summary>
        /// <param name="requete">chaine de requete de type SQL</param>
        /// <returns>une liste d'ojets des éléments de la requête</returns>
        public List<object[]> ExecuteSelect(string requete)
        {
            // crée une liste d'ojbet
           List<object[]> result = new List<object[]>();

            //ouvre la connexion mySql
            OpenMySqlConnexion();

            try
            {
                //execute la requête SELECT
                var commande = new MySqlCommand(requete, connexion);
                using (MySqlDataReader lignesResult = commande.ExecuteReader())
                {
                    while (lignesResult.Read())

                    {
                        //crée un tableau d'objets de la taille du nombre de colones/champs de la requête
                        object[] ligne = new object[lignesResult.FieldCount];
                        // remplis le tableau
                        lignesResult.GetValues(ligne);

                        // ajoute la ligne à la liste d'objets
                        result.Add(ligne);
                    }
                }
                //ferme la connexion
                this.connexion.Close();

                return result;
            }
            catch (MySqlException)
            {
                //ferme la connexion
                this.connexion.Close();

                return result;
            }
        }


        /// <summary>
        /// méthode retourne la ligne d'une liste d'objet dont l'index est passé en paramètre
        /// </summary>
        /// <param name="uneListe">une liste de tableaux d'objets</param>
        /// <param name="index">numéro d'index de la ligne recherchée</param>
        /// <returns>retourne un tableau d'objet</returns>
        public object[] GetUneLigne(List<object[]> uneListe, int index)
        {
            //crée un tableau d'objets de la taille du nombre de colones/champs de la requête
            return uneListe[index];
        }

        /// <summary>
        /// méthode retourne la valeur d'un champs dans un tableau dont l'index est passé en paramètre
        /// </summary>
        /// <param name="tabObjets">tableau d'objets</param>
        /// <param name="index">numéro d'index du champs recherché</param>
        /// <returns>retourne un objet</returns>
        public object GetUnChamps(object[]tabObjets, int index)
        {
            return tabObjets[index];
        }

        /// <summary>
        /// méthode retourne la valeur d'un champs dans une liste de tableau d'objet, dont l'index de la liste et du tableau sont passés en paramètre
        /// </summary>
        /// <param name="uneliste">une liste de tableaux d'objets</param>
        /// <param name="indexListe">numéro d'index de la ligne recherchée</param>
        /// <param name="indexChamps">numéro d'index du champs recherché</param>
        /// <returns>retourne un objet</returns>
        public object GetUnChampsduneLigne(List<object[]> uneliste, int indexListe, int indexChamps)
        {
            return GetUnChamps(GetUneLigne(uneliste, indexListe), indexChamps);
        }

    }
}
