using ApoloDictionary.Commands;
using System.CommandLine;
namespace ApoloDictionary
{
    public class Program
    {
        static async Task<int> Main(string[] args)
        {            
            RootCommand rootCommand = new RootCommand("Apolo Dictionary - Your CLI dictionary");
            TranslateCommandFactory translateCommand = new TranslateCommandFactory();
            CacheCommandFactory cacheCommand = new CacheCommandFactory();
            rootCommand.AddCommand(translateCommand.CreateCommand());
            rootCommand.AddCommand(cacheCommand.CreateCommand());
            return await rootCommand.InvokeAsync(args);
        }
    }
}
