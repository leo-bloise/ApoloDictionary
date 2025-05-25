using ApoloDictionary.Domain;
using ApoloDictionary.Providers;
using System.CommandLine;

namespace ApoloDictionary.Commands
{
    public class TranslateCommandFactory : CommandFactory
    {
        private Option<string> _textOption;
        public TranslateCommandFactory() : base("translate", "Translate a word or phrase") { }
        protected override void AddOptions(Command command)
        {
            _textOption = new Option<string>("--text", "The text to translate") { IsRequired = true };
            command.AddOption(_textOption);
        }
        protected override void DefineHandler(Command command)
        {
            command.SetHandler((textValue) =>
            {
                ITranslatorProvider provider = new CambridgeDictionaryProvider();
                try
                {
                    IEnumerable<WordDefinition> definitions = provider.Translate(textValue);
                    foreach(WordDefinition de in definitions)
                    {
                        Console.WriteLine(de);
                        Console.WriteLine("--------------");
                    }                    
                } catch(Exception ex)
                {
                    Console.WriteLine(ex);
                    Console.WriteLine($"Provider {provider} could not translate properly {textValue}.");
                }
            }, _textOption);
        }
    }
}
