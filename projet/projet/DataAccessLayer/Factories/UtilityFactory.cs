using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X509;
using projet.Models;
using projet.Security.Authorization;
using ProjetISDP1.DataAccessLayer;

namespace projet.DataAccessLayer.Factories
{
    public class UtilityFactory
    {
        public void Execute(string commandText)
        {
            MySqlConnection? mySqlCnn = null;
            MySqlDataReader? mySqlDataReader = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = commandText;

                mySqlDataReader = mySqlCmd.ExecuteReader();
                var temp = mySqlDataReader.Read();
            }
            finally
            {
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }
        }
    }
}
