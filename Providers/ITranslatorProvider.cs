using ApoloDictionary.Domain;

namespace ApoloDictionary.Providers
{
    public interface ITranslatorProvider
    {
        IEnumerable<WordDefinition> Translate(string text);   
    }
}
