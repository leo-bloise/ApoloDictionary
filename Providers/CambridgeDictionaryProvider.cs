using ApoloDictionary.Domain;
using HtmlAgilityPack;
using System.Net;
using System.Text.RegularExpressions;

namespace ApoloDictionary.Providers
{
    public class CambridgeDictionaryProvider : ITranslatorProvider
    {
        private HtmlWeb _htmlWeb = new HtmlWeb();
        private string GetClassificationGrammar(HtmlNode node)
        {
            HtmlNode? classification = node.SelectSingleNode(".//div[contains(@class, 'posgram') and contains(@class, 'dpos-g')]");
            ArgumentNullException.ThrowIfNull(classification, nameof(classification));
            return classification.InnerText.Trim();
        }
        private IEnumerable<HtmlNode> GetDefinitions(HtmlNode node)
        {
            HtmlNode? senseBody = node.SelectSingleNode(".//div[@class='sense-body dsense_b']");
            ArgumentNullException.ThrowIfNull(senseBody, nameof(senseBody));
            IEnumerable<HtmlNode>? definitions = senseBody.SelectNodes(".//div[@class='def-block ddef_block ']");
            ArgumentNullException.ThrowIfNull(definitions, nameof(definitions));
            if (definitions.Count() == 0)
            {
                throw new ArgumentNullException("No definitions found.");
            }
            return definitions;
        }
        private (string, string) GetDefinition(HtmlNode node)
        {
            HtmlNode? definitionNode = node.SelectSingleNode(".//div[@class='ddef_h']");
            ArgumentNullException.ThrowIfNull(definitionNode, nameof(definitionNode));
            HtmlNode? meaning = definitionNode.SelectSingleNode(".//div[@class='def ddef_d db']");
            ArgumentNullException.ThrowIfNull(meaning, nameof(meaning));
            HtmlNode? cefrNode = definitionNode.SelectSingleNode(".//span[@class='def-info ddef-info']");
            ArgumentNullException.ThrowIfNull(cefrNode, nameof(cefrNode));
            return (Regex.Replace(cefrNode.InnerText, @"\:", "").Trim(), Regex.Replace(meaning.InnerText, @"\:", "").Trim());
        }
        private IEnumerable<string> GetExamples(HtmlNode node)
        {
            HtmlNode? examplesNodeContainer = node.SelectSingleNode(".//div[@class='def-body ddef_b']");
            ArgumentNullException.ThrowIfNull(examplesNodeContainer, nameof(examplesNodeContainer));
            HtmlNodeCollection? examplesNode = examplesNodeContainer.SelectNodes(".//div[@class='examp dexamp']");
            ArgumentNullException.ThrowIfNull(examplesNode, nameof(examplesNode));
            return examplesNode.Select(exampleNode => exampleNode.InnerText.Trim());
        }
        private HtmlNode GetContent(string text)
        {
            HtmlDocument? htmlDoc = _htmlWeb.Load($"https://dictionary.cambridge.org/dictionary/english/{WebUtility.UrlEncode(text)}");
            ArgumentNullException.ThrowIfNull(htmlDoc, nameof(htmlDoc));

            HtmlNode? content = htmlDoc.DocumentNode.SelectSingleNode("//article[@id='page-content']");

            ArgumentNullException.ThrowIfNull(content, nameof(content));
            return content;
        }
        public IEnumerable<WordDefinition> Translate(string text)
        {
            HtmlNode content = GetContent(text);
            string classificationGrammar = GetClassificationGrammar(content);
            IEnumerable<HtmlNode> definitions =  GetDefinitions(content);
            List<WordDefinition> wordDefinitions = new List<WordDefinition>();
            foreach(HtmlNode definition in definitions)
            {
                (string cefr2, string meaning2) = GetDefinition(definition);                
                wordDefinitions.Add(new WordDefinition(
                    "Dictionary Cambridge",
                    text,
                    classificationGrammar,
                    cefr2.Count() == 0 ? "None": cefr2,
                    meaning2,
                    GetExamples(definition)
                ));
            }
            if (wordDefinitions.Count() == 0)
            {
                throw new ArgumentNullException("No definitions found.");
            }
            return wordDefinitions.AsEnumerable();
        }
        public override string ToString()
        {
            return "Cambridge Dictionary Provider";
        }
    }
}
