using System;

using Suplex.Security.DataAccess;
using Suplex.Utilities;
using Suplex.Utilities.Serialization;

namespace Suplex.Security.WebApi
{
    public class SuplexWebApiConfig
    {
        public SuplexDalConfig Dal { get; set; }
    }
    public class SuplexDalConfig
    {
        public SuplexDalConfig()
        {
        }

        internal string DefaultType { get { return "Suplex.Security.LiteDBDal:LiteDBDal"; } }


        public string Type { get; set; } = "Suplex.Security.LiteDBDal:LiteDBDal";
        public bool HasType { get { return !string.IsNullOrWhiteSpace( Type ); } }

        public object Config { get; set; }
        public bool HasConfig { get { return Config != null; } }


        public ISuplexDal GetDalInstance(string config = null)
        {
            Configure( config );
            ISuplexDalHost dalHost = AssemblyLoader.Load<ISuplexDalHost>( Type, DefaultType );
            dalHost.Configure( Config );
            return dalHost.Dal;
        }

        public void Configure(string config)
        {
            if( config != null )
            {
                SuplexDalConfig c = FromYaml( config );
                Type = c.Type;
                Config = c.Config;
            }
        }



        public string ToYaml(SuplexDalConfig config)
        {
            return YamlHelpers.Serialize( this );
        }

        public static SuplexDalConfig FromYaml(string config)
        {
            return YamlHelpers.Deserialize<SuplexDalConfig>( config );
        }

        public static SuplexDalConfig FromObject(object config)
        {
            string yaml = YamlHelpers.Serialize( config );
            return YamlHelpers.Deserialize<SuplexDalConfig>( yaml );
        }
    }
}