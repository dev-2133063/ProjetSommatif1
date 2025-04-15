namespace projet.Models
{
    public class QuotaApi
    {
        public int Id { get; set; }
        public int MembreId { get; set; }
        public DateTime DateMAJ { get; set; }
        public int MaxReq { get; set; }
        public int RequetesUtilisees { get; set; }

        public QuotaApi() { }

        public QuotaApi(int id, int membreId, DateTime dateMAJ, int maxReq, int reqUsed)
        {
            Id = id;
            MembreId = membreId;
            DateMAJ = dateMAJ;
            MaxReq = maxReq;
            RequetesUtilisees = reqUsed;
        }
    }
}
