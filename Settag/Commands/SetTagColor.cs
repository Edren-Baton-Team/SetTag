using CommandSystem;
using Exiled.API.Features;
using System;
using Exiled.Permissions.Extensions;
using System.Linq;

namespace SetTag.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class SetColor : ICommand
    {
        public string Command { get; } = "setcolor";
        public string[] Aliases { get; } = new string[] { "scolor" };
        public string Description { get; } = "Sets players' rank color. Usage:\n" + SetColorCommandUsage;
        public const string SetColorCommandUsage = "setcolor (player id or \"*\" or \"all\") (rank color)";
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            try
            {
                Plugin.Config Config = Plugin.Plugin.Singleton.Config;

                if (!sender.CheckPermission("settag.settagcolor")) {
                    response = "You don't have permission to execute this command. If you believe this is an error, please DM the server's admin. Required permission: \"settag.settagcolor\"";
                    return false;
                }

                if (arguments.Count != 2) {
                    response = "Not enough arguments! Usage:\n" + SetColorCommandUsage;
                    return false;
                }

                string newRankColor = arguments.At(1).ToLower();
                if (!Config.AllowedColors.Contains(newRankColor))
                {
                    response = $"This color does not exist or is restricted!\nAll available colors: ";
                    foreach (string allowedColor in Config.AllowedColors)
                        response += $"{allowedColor}, ";
                    response = response.Substring(0, response.Count() - 2) + '.';
                    return false;
                }

                switch (arguments.At(0)) {
                    case "*":
                    case "all":
                        foreach (Player target in Player.List)
                        {
                            target.RankColor = newRankColor;
                        }
                        response = $"{Player.List.Count()} players were issued <b>{newRankColor}</b> rank color";
                        return true;
                    default:
                        Player singleTarget = Player.Get(arguments.At(0));
                        if (singleTarget is null)
                        {
                            response = $"This player does not exist! Please enter a valid id or a nickname.\nUsage: {SetColorCommandUsage}\nExample: setcolor 2 red <i>or</i> setcolor sanes green";
                            return false;
                        }
                        singleTarget.RankColor = newRankColor;
                        response = $"Player {singleTarget.Nickname} was issued <b>{newRankColor}</b> rank color";
                        return true;
                }
            }
            catch {
                response = $"Something went wrong when executing the command!\nUsage: {SetColorCommandUsage}\nExample: setcolor 2 red <i>or</i> setcolor sanes green";
                return false;
            }
        }
    }
}