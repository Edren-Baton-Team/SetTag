using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpawnAlgo
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    class settag : ICommand
    {
        public string Command => "settag";

        public string[] Aliases => new string[] { };

        public string Description => "settag [игрок] [префикс]";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            try
            {
                if (!sender.CheckPermission("spawnalgo.settag"))
                {
                    response = "У вас нет прав на использование этой команды. Если вы считаете, что у вас должно быть право на это, напишите об этом moseechev#8315.";
                    return false;
                }
                if (arguments.Count < 2)
                {
                    response = "Использование: settag [игрок] [префикс]";
                    return false;
                }
                Player player = Player.Get(arguments.At(0));
                player.RankName = string.Join(" ", arguments.Where(x => arguments.ToList().IndexOf(x) != 0));
                response = $"Успешно поставлен тег игрока {player.Nickname}!";
                return true;
            }
            catch
            {
                response = "Использование: settag [игрок] [сам префикс]";
                return false;
            }
        }
    }
}