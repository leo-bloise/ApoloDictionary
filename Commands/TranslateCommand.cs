using ApoloDictionary.Domain;
using ApoloDictionary.Providers;
using System.CommandLine;

namespace ApoloDictionary.Commands
{
    public class TranslateCommandFactory : CommandFactory
    {
        private Option<string> _textOption;
        private CacheManager _cacheManager;
        public TranslateCommandFactory() : base("translate", "Translate a word or phrase") {
            _cacheManager = CacheManager.Instance;
        }
        protected override void AddOptions(Command command)
        {
            _textOption = new Option<string>("--text", "The text to translate") { IsRequired = true };
            command.AddOption(_textOption);
        }
        private IEnumerable<WordDefinition>? GetFromCache(string textValue)
        {
            return _cacheManager.GetFromCache(textValue);
        }
        private void SaveToCache(string textValue, IEnumerable<WordDefinition> definitions)
        {
            _cacheManager.SaveToCache(textValue, definitions);
        }
        private void show(IEnumerable<WordDefinition> definitions)
        {
            foreach (WordDefinition de in definitions)
            {
                Console.WriteLine(de);
                Console.WriteLine("--------------");
            }
        }
        protected override void DefineHandler(Command command)
        {
            command.SetHandler((textValue) =>
            {
                IEnumerable<WordDefinition>? cached = GetFromCache(textValue);
                if(cached != null && cached.Count() > 0)
                {
                    show(cached);
                    return;
                }
                ITranslatorProvider provider = new CambridgeDictionaryProvider();
                try
                {
                    IEnumerable<WordDefinition> definitions = provider.Translate(textValue);
                    show(definitions);
                    SaveToCache(textValue, definitions);
                } catch(Exception ex)
                {
                    Console.WriteLine($"Provider {provider} could not translate properly {textValue}.");
                }
            }, _textOption);
        }
    }
}
