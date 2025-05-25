using System.Text;

namespace ApoloDictionary.Domain
{
    public record WordDefinition(
        string provider,
        string text,
        string classificationGrammar,
        string cefr,
        string meaning,
        IEnumerable<string> examples
    ) {
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Provider: {provider.Trim()}");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"Text: {text.Trim()}");
            stringBuilder.AppendLine($"Grammar Classification: {classificationGrammar.Trim()}");
            stringBuilder.AppendLine($"CEFR: {cefr.Trim()}");
            stringBuilder.AppendLine($"Meaning: {meaning.Trim()}");
            stringBuilder.AppendLine();
            examples.ToList().ForEach(example => stringBuilder.AppendLine($"Example: {example.Trim()}"));
            return stringBuilder.ToString();
        }
    }
}
