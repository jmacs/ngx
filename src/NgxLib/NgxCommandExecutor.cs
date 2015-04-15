using System.Collections.Generic;

namespace NgxLib
{
    public class NgxCommandExecutor
    {
        protected Dictionary<long, List<INgxCommand>> CommandHash = new Dictionary<long, List<INgxCommand>>();

        public void Add(long messageKey, INgxCommand command)
        {
            List<INgxCommand> commands;
            if (!CommandHash.TryGetValue(messageKey, out commands))
            {
                commands = new List<INgxCommand>();
                CommandHash.Add(messageKey, commands);
            }
            commands.Add(command);
        }

        public void Execute(NgxContext context, NgxMessage message)
        {
            List<INgxCommand> commands;
            if (CommandHash.TryGetValue(message.MessageKey, out commands))
            {
                for (var i = 0; i < commands.Count; i++)
                {
                    commands[i].Execute(context, message);
                }
            }
        }

        public void Remove(long messageKey, INgxCommand command)
        {
            List<INgxCommand> commands;
            if (CommandHash.TryGetValue(messageKey, out commands))
            {                
                CommandHash[messageKey].Remove(command);
                if (CommandHash[messageKey].Count == 0)
                {
                    CommandHash.Remove(messageKey);
                }
            }
        }

        public void RemoveAll()
        {
            CommandHash.Clear();
        }
    }
}