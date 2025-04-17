using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X509;
using projet.Models;
using projet.Security.Authorization;
using ProjetISDP1.DataAccessLayer;

namespace projet.DataAccessLayer.Factories
{
    public class RolesFactory
    {
        public List<string> GetRolesFromApiKey(string key)
        {
            MySqlConnection? mySqlCnn = null;
            MySqlDataReader? mySqlDataReader = null;
            List<string> roles = new List<string>();

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = @"SELECT pmr.Role 
                                        FROM projet_membre_roles pmr
                                        JOIN projet_membre pm ON pmr.MembreId = pm.Id
                                        WHERE pm.Apikey = @Apikey";

                mySqlCmd.Parameters.AddWithValue("@Apikey", key);
                mySqlDataReader = mySqlCmd.ExecuteReader();
                roles = CreateFromRead(mySqlDataReader);
            }
            finally
            {
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }

            return roles;
        }

        private List<string> CreateFromRead(MySqlDataReader mySqlDataReader)
        {
            List<string> roles = new();

            while (mySqlDataReader.Read())
            {
                string? role = mySqlDataReader["Role"].ToString() ?? string.Empty;

                if (role != null && role != "") roles.Add(role);
            }
            return roles;
        }
    }
}
