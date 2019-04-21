using Discord.Commands;
using System.Linq;
using UnmaxedBot.Core.Permissions;

namespace UnmaxedBot.Core.Extensions
{
    public static class CommandExtensions
    {
        public static bool IsAdminCommand(this CommandInfo command)
        {
            return command.Preconditions.Any(p => p.Group == BotPermission.Admin);
        }
    }
}
