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
                if (!sender.CheckPermission("settag.settagcolor")) {
                    response = "You don't have permission to execute this command. If you believe this is an error, please DM the server's admin. Required permission: \"settag.settagcolor\"";
                    return false;
                }

                if (arguments.Count != 2) {
                    response = "Not enough arguments! Usage:\n" + SetColorCommandUsage;
                    return false;
                }

                string newRankColor = arguments.At(1);
                switch (arguments.At(0)) {
                    case "*":
                    case "all":
                        foreach (Player target in Player.List)
                        {
                            target.RankColor = newRankColor;
                        }
                        response = $"<color=#{newRankColor}>{Player.List.Count()} players were issued <b>{newRankColor}</b> rank color</color>";
                        return true;
                    default:
                        Player singleTarget = Player.Get(arguments.At(0));
                        singleTarget.RankColor = newRankColor;
                        response = $"<color=#{newRankColor}>Player {singleTarget.Nickname} was issued <b>{newRankColor}</b> rank color</color>";
                        return true;
                }
            }
            catch(Exception e) {
                response = $"Something went wrong when executing the command!\nUsage: {SetColorCommandUsage}\nError: {e}";
                return false;
            }
        }
    }
}