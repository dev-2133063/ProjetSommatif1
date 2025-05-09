using projet.DataAccessLayer.Factories;
using ProjetISDP1.DataAccessLayer.Factories;
using ProjetISDP2.DataAccessLayer.Factories;

namespace ProjetISDP1.DataAccessLayer
{
    public class DAL
    {
        private AuteurFactory? _auteurFactory = null;
        private LivreFactory? _livreFactory = null;
        private CategorieFactory? _categorieFactory = null;
        private EmpruntFactory? _empruntFactory = null;
        private MembreFactory? _membreFactory = null;
        private QuotaApiFactory? _quotaApiFactory = null;
        private RolesFactory? _rolesFactory = null;
        private LoginFactory? _loginFactory = null;
        private UtilityFactory? _utilityFactory = null;

        public static string ConnectionString
        {
            //J'ai pas trop compris comment gerer les differents connexion strings alors j'ai change directement dans default lors de mes tests...
            get
            {
                var config = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
                return config.GetSection("ConnectionStrings").GetSection("Default").Value ?? string.Empty;
            }
        }
        public MembreFactory MembreFactory
        {
            get
            {
                if (_membreFactory == null)
                {
                    _membreFactory = new MembreFactory();
                }
                return _membreFactory;
            }
        }
        public EmpruntFactory EmpruntFactory
        {
            get
            {
                if (_empruntFactory == null)
                {
                    _empruntFactory = new EmpruntFactory();
                }
                return _empruntFactory;
            }
        }
        public AuteurFactory AuteurFactory
        {
            get
            {
                if(_auteurFactory == null)
                {
                    _auteurFactory= new AuteurFactory();
                }
                return _auteurFactory;
            }
        }
        public LivreFactory LivreFactory
        {
            get
            {
                if (_livreFactory == null)
                {
                    _livreFactory = new LivreFactory();
                }
                return _livreFactory;
            }
        }
        public CategorieFactory CategoryFactory
        {
            get
            {
                if (_categorieFactory == null)
                {
                    _categorieFactory = new CategorieFactory();
                }
                return _categorieFactory;
            }
        }
        public QuotaApiFactory QuotaApiFactory
        {
            get
            {
                if (_quotaApiFactory == null)
                {
                    _quotaApiFactory = new QuotaApiFactory();
                }
                return _quotaApiFactory;
            }
        }
        public RolesFactory RolesFactory
        {
            get
            {
                if (_rolesFactory == null)
                {
                    _rolesFactory = new RolesFactory();
                }
                return _rolesFactory;
            }
        }
        public LoginFactory LoginFactory
        {
            get
            {
                if (_loginFactory == null)
                {
                    _loginFactory = new LoginFactory();
                }
                return _loginFactory;
            }
        }
        public UtilityFactory UtilityFactory
        {
            get
            {
                if (_utilityFactory == null)
                {
                    _utilityFactory = new UtilityFactory();
                }
                return _utilityFactory;
            }
        }
    }
}
