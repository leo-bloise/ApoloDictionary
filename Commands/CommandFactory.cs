using System.CommandLine;


namespace ApoloDictionary.Commands
{
    public abstract class CommandFactory : ICommandFactory
    {
        private string _name;
        private string _description;
        public CommandFactory(string name, string description)
        {
            _name = name;
            _description = description;
        }
        public Command CreateCommand()
        {
            Command command = new Command(_name, _description);
            AddOptions(command);
            DefineHandler(command);
            return command;
        }
        protected abstract void AddOptions(Command command);
        protected abstract void DefineHandler(Command command);
    }
}
