using CommandSystem;
using Exiled.API.Features;
using System;
using Exiled.Permissions.Extensions;
using System.Linq;

namespace SetTag.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class SetRank: ICommand
    {
        public string Command { get; } = "setrank";
        public string[] Aliases { get; } = new string[] { "srank" };
        public string Description { get; } = "Sets players' rank name. Usage:\n" + SetRankCommandUsage;
        public const string SetRankCommandUsage = "setrank (player id or \"*\" or \"all\") (rank name)";
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response) {
            try
            {
                if (!sender.CheckPermission("settag.settagcolor")) {
                    response = "You don't have permission to execute this command. If you believe this is an error, please DM the server's admin. Required permission: \"settag.settagcolor\"";
                    return false;
                }

                if (arguments.Count != 2) {
                    response = "Not enough arguments! Usage:\n" + SetRankCommandUsage;
                    return false;
                }

                string newRankName = string.Join(" ", arguments.Where(x=>x!=arguments.At(0)));
                switch (arguments.At(0)) {
                    case "*":
                    case "all":
                        foreach (Player target in Player.List) {
                            target.RankName = newRankName;
                        }
                        response = $"<color=#{newRankName}>{Player.List.Count()} players were issued the <b>{newRankName}</b> rank name</color>";
                        return true;

                    default:
                        Player singleTarget = Player.Get(arguments.At(0));
                        singleTarget.RankName = newRankName;
                        response = $"<color=#{newRankName}>Player {singleTarget.Nickname} was issued <b>{newRankName}</b> rank name</color>";
                        return true;
                }
            }
            catch (Exception e) {
                response = $"Something went wrong when executing the command!\nUsage: {SetRankCommandUsage}\nError: {e}";
                return false;
            }
        }
    }
}