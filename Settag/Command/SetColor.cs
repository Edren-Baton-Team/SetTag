using CommandSystem;
using Exiled.API.Features;
using System;
using Exiled.Permissions.Extensions;
using System.Linq;

namespace AdminTools.Commands.Settag
{

    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class SettagColor : ICommand
    {

        public string Command { get; } = "setcolor";

        public string[] Aliases { get; } = new string[] { };
        public const string Usage = "setcolor (player id or \"*\" or \"all\") (tag color)";
        public string Description { get; } = "Sets players' rank color. Usage:\n" + Usage;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            try
            {
                if (!sender.CheckPermission("settag.settagcolor"))
                {
                    response = "You don't have permission to execute this command. If you believe this is an error, please DM the server's admin. Required permission: \"settag.settagcolor\"";
                    return false;
                }
                if (arguments.Count != 2)
                {
                    response = "Not enough arguments! Usage:\n" + Usage;
                    return false;
                }
                string newRankColor = arguments.At(1);
                switch (arguments.At(0))
                {
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
            catch (Exception e)
            {
                response = $"Something went wrong when executing the command!\nUsage: {Usage}\nError: {e}";
                return false;
            }
        }
    }
}
