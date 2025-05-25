using ApoloDictionary.Commands;
using System.CommandLine;
namespace ApoloDictionary
{
    public class Program
    {
        static async Task<int> Main(string[] args)
        {            
            RootCommand rootCommand = new RootCommand("Apolo Dictionary - Your CLI dictionary");
            var translateCommand = new TranslateCommandFactory();
            rootCommand.AddCommand(translateCommand.CreateCommand());
            return await rootCommand.InvokeAsync(args);
        }
    }
}
