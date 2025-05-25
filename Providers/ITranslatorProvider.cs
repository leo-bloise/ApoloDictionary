using ApoloDictionary.Domain;

namespace ApoloDictionary.Providers
{
    public interface ITranslatorProvider
    {
        WordDefinition Translate(string text);   
    }
}
