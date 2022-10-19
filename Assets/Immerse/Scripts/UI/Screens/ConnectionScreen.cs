namespace Immerse.Brodsky.UI
{
    public class ConnectionScreen : BrodskyScreen
    {
        public override void Init()
        {
            BrodskyEvents.MultiplayerConnected += Exit;
        }
    }
}