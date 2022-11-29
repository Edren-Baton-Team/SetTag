using Exiled.API.Features;
using Exiled.Events;

namespace SetTag.Plugin
{
    public class Plugin : Plugin<Config> {
        public override string Author => "Rysik5318";
        public override string Name => "SetTag";
        public override string Prefix => "SetTag";
        public override System.Version Version => new System.Version(1,0,0);
        public override void OnEnabled() {
            base.OnEnabled();
        }
        public override void OnDisabled() {
            base.OnDisabled();
        }
    }
}
