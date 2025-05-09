using MySql.Data.MySqlClient;
using projet.Models;

namespace ProjetISDP1.DataAccessLayer.Factories
{
    public class LivreFactory
    {
        private Livre CreateFromReader(MySqlDataReader mySqlDataReader)
        {
            DAL dal = new DAL();

            int id = (int)mySqlDataReader["Id"];
            string isbn = mySqlDataReader["ISBN"].ToString() ?? string.Empty;
            string titre = mySqlDataReader["Titre"].ToString() ?? string.Empty;
            int nbPages = (int)mySqlDataReader["NbPages"];
            int auteurId = (int)mySqlDataReader["AuteurId"];
            int categorieId = (int)mySqlDataReader["CategorieId"];

            Livre livre = new Livre(id, isbn, titre, nbPages, auteurId, categorieId);

            //amélioration interne
            //livre.Auteur = dal.AuteurFactory.Get(auteurId);
            //livre.Categorie = dal.CategoryFactory.Get(categorieId);

            return livre;
        }

        public Livre CreateEmpty()
        {
            return new Livre(0, string.Empty, string.Empty, 0, 0, 0);
        }

        public Livre[] GetAll()
        {
            DAL dal = new DAL();
            List<Livre> livres = new List<Livre>();
            MySqlConnection mySqlCnn = null;
            MySqlDataReader mySqlDataReader = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "SELECT * FROM projet_livre ORDER BY ISBN ASC";

                //Toutes les categories
                Dictionary<int, Categorie> catDictionnary = new Dictionary<int, Categorie>();
                Categorie[] cats = dal.CategoryFactory.GetAll();
                foreach (Categorie cat in cats)
                {
                    if (cat.Id > 0)
                    catDictionnary.Add(cat.Id, cat);
                }

                //toutes les auteurs
                Dictionary<int, Auteur> autDictionnary = new Dictionary<int, Auteur>();
                Auteur[] auts = dal.AuteurFactory.GetAll();
                foreach (Auteur aut in auts)
                {
                    if (aut.Id > 0)
                    autDictionnary.Add(aut.Id, aut);
                }

                mySqlDataReader = mySqlCmd.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    Livre livre = CreateFromReader(mySqlDataReader);

                    if (catDictionnary.TryGetValue(livre.CategorieId, out Categorie cat))
                        livre.Categorie = cat;
                    else livre.Categorie = null;

                    if (autDictionnary.TryGetValue(livre.AuteurId, out Auteur aut))
                        livre.Auteur = aut;
                    else livre.Auteur = null;

                    livres.Add(livre);
                }
            }
            finally
            {
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }

            return livres.ToArray();
        }
        
        public Livre? Get(int id)
        {
            Livre? livre = null;
            MySqlConnection? mySqlCnn = null;
            MySqlDataReader? mySqlDataReader = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "SELECT * FROM projet_livre WHERE Id = @Id";
                mySqlCmd.Parameters.AddWithValue("@Id", id);

                mySqlDataReader = mySqlCmd.ExecuteReader();
                if (mySqlDataReader.Read())
                {
                    livre = CreateFromReader(mySqlDataReader);
                }
            }
            finally
            {
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }

            return livre;
        }

        /// <summary>
        /// Trouve les livre qui sont emprunté ou pas selon le choix.
        /// True -> dispo
        /// False -> Emprunté
        /// </summary>
        /// <param name="disponibilite"></param>
        /// <returns>Las liste de livre correspondant</returns>
        public Livre[] GetDispo(bool disponibilite = true)
        {
            List<Livre> livres = new List<Livre>();
            MySqlConnection? mySqlCnn = null;
            MySqlDataReader? mySqlDataReader = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();

                if (disponibilite) mySqlCmd.CommandText = "SELECT liv.Id, liv.ISBN, liv.Titre, liv.NbPages, liv.AuteurId, liv.CategorieId " +
                                       "FROM projet_livre liv " +
                                       "LEFT JOIN projet_emprunt emp ON liv.Id = emp.LivreId " +
                                       "WHERE emp.LivreId IS NULL;";
                else mySqlCmd.CommandText = "SELECT liv.Id, liv.ISBN, liv.Titre, liv.NbPages, liv.AuteurId, liv.CategorieId " +
                                       "FROM projet_livre liv " +
                                       "LEFT JOIN projet_emprunt emp ON liv.Id = emp.LivreId " +
                                       "WHERE emp.LivreId IS NOT NULL;";

                mySqlDataReader = mySqlCmd.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    livres.Add(CreateFromReader(mySqlDataReader));
                }
            }
            finally
            {
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }

            return livres.ToArray();
        }

        public Livre[] GetInDispoMembre(int id)
        {
            List<Livre> livres = new List<Livre>();
            MySqlConnection? mySqlCnn = null;
            MySqlDataReader? mySqlDataReader = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "SELECT liv.Id, liv.ISBN, liv.Titre, liv.NbPages, liv.AuteurId, liv.CategorieId " +
                                       "FROM projet_livre liv " +
                                       "LEFT JOIN projet_emprunt emp ON liv.Id = emp.LivreId " +
                                       "WHERE emp.LivreId IS NOT NULL " +
                                       "AND emp.MembreId = @Id;";

                mySqlCmd.Parameters.AddWithValue("@Id", id);
                mySqlDataReader = mySqlCmd.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    livres.Add(CreateFromReader(mySqlDataReader));
                }
            }
            finally
            {
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }

            return livres.ToArray();
        }

        /// <summary>
        /// Cherche tout les livres qui ont une historique d'emprunt (si membreId == -1)
        /// sinon retourne l'historique des emprunts d'un membre
        /// </summary>
        /// <returns>La liste de livres</returns>
        public Livre[] GetAllDejaEmprunte(int membreId = -1)
        {
            List<Livre> livres = new List<Livre>();
            MySqlConnection? mySqlCnn = null;
            MySqlDataReader? mySqlDataReader = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                if (membreId != -1)
                {
                    mySqlCmd.CommandText = "SELECT DISTINCT liv.Id, liv.ISBN, liv.Titre, liv.NbPages, liv.AuteurId, liv.CategorieId " +
                                           "FROM projet_emprunt emp " +
                                           "LEFT JOIN projet_livre liv ON emp.LivreId = liv.Id" +
                                           "WHERE emp.MembreId = @membreId;";
                }
                else
                {
                    mySqlCmd.CommandText = "SELECT DISTINCT liv.Id, liv.ISBN, liv.Titre, liv.NbPages, liv.AuteurId, liv.CategorieId " +
                                           "FROM projet_emprunt emp " +
                                           "LEFT JOIN projet_livre liv ON emp.LivreId = liv.Id;";
                }

                mySqlCmd.Parameters.AddWithValue("@membreId", membreId);
                mySqlDataReader = mySqlCmd.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    livres.Add(CreateFromReader(mySqlDataReader));
                }
            }
            finally
            {
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }

            return livres.ToArray();
        }
        public void Save(Livre livre)
        {
            MySqlConnection? mySqlCnn = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                using (MySqlCommand mySqlCmd = mySqlCnn.CreateCommand())
                {
                    if (livre.Id == 0)
                    {
                        // Nouveau livre avec Id == 0
                        mySqlCmd.CommandText = "INSERT INTO projet_livre(ISBN, Titre, NbPages, AuteurId, CategorieId) " +
                                               "VALUES (@ISBN, @Titre, @NbPages, @AuteurId, @CategorieId)";
                    }
                    else
                    {
                        // Mise à jour d'un livre existant
                        mySqlCmd.CommandText = "UPDATE projet_livre " +
                                               "SET ISBN=@ISBN, Titre=@Titre, NbPages=@NbPages, AuteurId=@AuteurId, CategorieId=@CategorieId " +
                                               "WHERE Id=@Id";

                        mySqlCmd.Parameters.AddWithValue("@Id", livre.Id);
                    }

                    // Ajout des paramètres communs
                    mySqlCmd.Parameters.AddWithValue("@ISBN", livre.Isbn.Trim());
                    mySqlCmd.Parameters.AddWithValue("@Titre", livre.Titre.Trim());
                    mySqlCmd.Parameters.AddWithValue("@NbPages", livre.NbPages);
                    mySqlCmd.Parameters.AddWithValue("@AuteurId", livre.AuteurId);
                    mySqlCmd.Parameters.AddWithValue("@CategorieId", livre.CategorieId);

                    // Exécution de la requête
                    mySqlCmd.ExecuteNonQuery();

                    if (livre.Id == 0)
                    {
                        // Si c'était un nouveau livre (requête INSERT), on récupère l'ID généré
                        livre.Id = (int)mySqlCmd.LastInsertedId;
                    }
                }
            }
            finally
            {
                if (mySqlCnn != null)
                {
                    mySqlCnn.Close();
                }
            }
        }

        public void Delete(int id)
        {
            MySqlConnection? mySqlCnn = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                using (MySqlCommand mySqlCmd = mySqlCnn.CreateCommand())
                {
                    // Suppression d'un livre par ID
                    mySqlCmd.CommandText = "DELETE FROM projet_livre WHERE Id=@Id";
                    mySqlCmd.Parameters.AddWithValue("@Id", id);
                    mySqlCmd.ExecuteNonQuery();
                }
            }
            finally
            {
                if (mySqlCnn != null)
                {
                    mySqlCnn.Close();
                }
            }
        }
    }
}
