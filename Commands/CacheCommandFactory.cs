using ApoloDictionary.Domain;
using System.CommandLine;

namespace ApoloDictionary.Commands
{
    public class CacheCommandFactory : CommandFactory
    {
        private Option<bool> _cleanOption;
        private CacheManager _cacheManager;
        public CacheCommandFactory() : base("cache", "Manage the cache") 
        { 
            _cacheManager = CacheManager.Instance;
        }
        protected override void AddOptions(Command command)
        {
            _cleanOption = new Option<bool>(name: "--clean", description: "Clean the cache", getDefaultValue: () => false) { IsRequired = false, Description = "Remove all data persisted in the cache" };
            command.AddOption(_cleanOption);
        }

        protected override void DefineHandler(Command command)
        {
            command.SetHandler((clean) =>
            {                
                if(clean)
                {
                    _cacheManager.DeleteCache();
                    Console.WriteLine("Cache cleaned successfully.");
                    return;
                }
                string cacheInfo = _cacheManager.GetCacheBasicInfo();
                Console.WriteLine(cacheInfo);
            }, _cleanOption);
        }
    }
}
