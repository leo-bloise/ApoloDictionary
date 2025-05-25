using System.CommandLine;

namespace ApoloDictionary.Commands
{
    public interface ICommandFactory
    {
        Command CreateCommand();
    }
}
