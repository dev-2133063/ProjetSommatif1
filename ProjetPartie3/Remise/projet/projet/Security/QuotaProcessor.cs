using ProjetISDP1.DataAccessLayer;
using projet.Models;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Runtime.CompilerServices;
using Mysqlx.Crud;

namespace projet.Security
{
    public class QuotaProcessor
    {
        DAL dal;
        public QuotaProcessor()
        {
            dal = new DAL();
        }
        public bool LogQuota(string apikey)
        {
            QuotaApi? quota = dal.QuotaApiFactory.GetQuotaViaKey(apikey);

            if (quota is null)
            {
                dal.QuotaApiFactory.New(new QuotaApi(0, dal.MembreFactory.GetViaKey(apikey).Id, DateTime.Now, 20, 1));
            }
            else if (DateTime.Now - quota.DateMAJ > TimeSpan.FromHours(24))
            {
                dal.QuotaApiFactory.Reset(quota);
            }
            else if (quota.RequetesUtilisees >= quota.MaxReq)
            {
                //Limit atteinte
                return false;
            }
            else
            {
                dal.QuotaApiFactory.Log(quota);
            }

            return true;
        }
    }
}
