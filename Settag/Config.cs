using Exiled.API.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;

namespace SetTag.Plugin
{
    public class Config : IConfig
    {
        [Description("Is the plugin enabled?")]
        public bool IsEnabled { get; set; } = true;
        [Description("All allowed colors")]
        public List<string> AllowedColors { get; set; } = new List<string>() { "red", "pink", "brown", "silver", "light_green", "crimson", "cyan", "aqua", "deep_pink", "tomato", "yellow", "magenta", "blue_green", "orange", "lime", "green", "emerald", "carmine", "nickel", "mint", "army_green", "pumpkin" };
    }
}