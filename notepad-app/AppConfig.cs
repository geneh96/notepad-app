namespace notepad_app
{
    public class AppConfig : IAppConfig
    {
        public readonly string _configVal = string.Empty;

        public IConfiguration Configuration { get; }
        public AppConfig(IConfiguration configuration)
        {
            Configuration = configuration;
            _configVal = Configuration["ConnectionStrings:NotepadConnectionString"];
        }

        public string GetConfigValue()
        {
            return _configVal;
        }
    }
    public interface IAppConfig
    {
        string GetConfigValue();
    }
}
