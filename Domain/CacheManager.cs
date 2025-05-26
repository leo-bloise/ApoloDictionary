using System.Net;
using System.Text.Json;

namespace ApoloDictionary.Domain
{
    public class CacheManager
    {
        private string _cacheFilePath;
        private static CacheManager? _instance;
        public static CacheManager Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new CacheManager();
                }
                return _instance;
            }
        }
        private CacheManager()
        {
            _cacheFilePath = Path.GetFullPath("./cache");
            LoadCache();
        }
        private void LoadCache()
        {
            if (!Directory.Exists(_cacheFilePath))
            {
                Directory.CreateDirectory(_cacheFilePath);
            }
        }
        public IEnumerable<WordDefinition>? GetFromCache(string key)
        {
            string pathToFile = Path.Combine(_cacheFilePath, $"{WebUtility.UrlEncode(key)}.json");
            if(!File.Exists(pathToFile)) return null;
            List<WordDefinition>? wordDefinitions = JsonSerializer.Deserialize<List<WordDefinition>>(File.ReadAllText(pathToFile));
            if(wordDefinitions == null || wordDefinitions.Count == 0) return null;
            return wordDefinitions.AsEnumerable();
        }
        public void SaveToCache(string key, IEnumerable<WordDefinition> definition)
        {
            List<WordDefinition> definitionsList = definition.ToList();
            string pathToFile = Path.Combine(_cacheFilePath, $"{WebUtility.UrlEncode(key)}.json");
            string json = JsonSerializer.Serialize(definitionsList);
            File.WriteAllText(pathToFile, json);
        }
    }
}
