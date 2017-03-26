namespace Scripts.Client.Contexts
{
    public class GameContext
    {
        public static GameContext Current = new GameContext();

        public UserInterfaceContext UserInterface
        {
            get;
            private set;
        }

        public PlayerContext Player
        {
            get;
            private set;
        }

        public TimeContext Time
        {
            get;
            private set;
        }

        public AudioContext Audio
        {
            get;
            private set;
        }

        public NetworkContext Network
        {
            get;
            private set;
        }

        private GameContext()
        {
            UserInterface = new UserInterfaceContext();
            Player = new PlayerContext();
            Time = new TimeContext();
            Audio = new AudioContext();
            Network = new NetworkContext();
        }
    }
}
