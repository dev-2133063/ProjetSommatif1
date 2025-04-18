using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using projet.Models;
using ProjetISDP1.DataAccessLayer;

namespace projet.DataAccessLayer.Factories
{
    public class QuotaApiFactory
    {
        private QuotaApi CreateFromReader(MySqlDataReader mySqlDataReader)
        {
            DAL dal = new DAL();

            int id = (int)mySqlDataReader["Id"];
            int membreId = (int)mySqlDataReader["MembreId"];
            DateTime dateTime = (DateTime)mySqlDataReader["DateMAJ"];
            int maxRequest = (int)mySqlDataReader["MaxRequetes"];
            int useRequest = (int)mySqlDataReader["RequetesUtilisees"];

            return new QuotaApi(id, membreId, dateTime, maxRequest, useRequest);
        }

        public QuotaApi CreateEmpty()
        {
            return new QuotaApi(0, 0, DateTime.Now, 0, 0);
        }

        public void Reset(QuotaApi quotaApi)
        {
            MySqlConnection? mySqlCnn = null;
            MySqlDataReader? mySqlDataReader = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "UPDATE projet_quota_api " +
                                       "SET DateMAJ = NOW(), MaxRequetes = @MaxReq, RequetesUtilisees = 1 " +
                                       "WHERE Id = @Id;";

                mySqlCmd.Parameters.AddWithValue("@MaxReq", quotaApi.MaxReq);
                mySqlCmd.Parameters.AddWithValue("@Id", quotaApi.Id);

                mySqlCmd.ExecuteNonQuery();
            }
            finally
            {
                mySqlCnn?.Close();
            }
        }

        public void Log(QuotaApi quota)
        {
            MySqlConnection? mySqlCnn = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                using (MySqlCommand mySqlCmd = mySqlCnn.CreateCommand())
                {
                    mySqlCmd.CommandText = "UPDATE projet_quota_api " +
                                           "SET RequetesUtilisees = RequetesUtilisees + 1 " +
                                           "WHERE Id=@Id";

                    mySqlCmd.Parameters.AddWithValue("@Id", quota.Id);

                    mySqlCmd.ExecuteNonQuery();

                    if (quota.Id == 0)
                    {
                        quota.Id = (int)mySqlCmd.LastInsertedId;
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

        public void New(QuotaApi quota)
        {
            MySqlConnection? mySqlCnn = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                using (MySqlCommand mySqlCmd = mySqlCnn.CreateCommand())
                {
                    mySqlCmd.CommandText = "INSERT INTO projet_quota_api(MembreId, DateMAJ, MaxRequetes, RequetesUtilisees)" +
                                           "VALUES(@MembreId, NOW(), @MaxRequetes, 1)";

                    mySqlCmd.Parameters.AddWithValue("@MembreId", quota.MembreId);
                    mySqlCmd.Parameters.AddWithValue("@MaxRequetes", quota.MaxReq);

                    // Exécution de la requête
                    mySqlCmd.ExecuteNonQuery();

                    if (quota.Id == 0)
                    {
                        quota.Id = (int)mySqlCmd.LastInsertedId;
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
        public QuotaApi? GetQuotaViaKey(string apikey)
        {
            QuotaApi? quota = null;
            MySqlConnection? mySqlCnn = null;
            MySqlDataReader? mySqlDataReader = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "SELECT * " +
                                       "FROM projet_quota_api pq " +
                                       "JOIN projet_membre pm ON pq.MembreId = pm.Id " +
                                       "WHERE pm.Apikey = @Apikey";

                mySqlCmd.Parameters.AddWithValue("@Apikey", apikey);

                mySqlDataReader = mySqlCmd.ExecuteReader();
                if (mySqlDataReader.Read())
                {
                    quota = CreateFromReader(mySqlDataReader);
                }
            }
            finally
            {
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }

            return quota;
        }
    }
}
