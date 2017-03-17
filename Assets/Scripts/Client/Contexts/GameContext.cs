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

        private GameContext()
        {
            UserInterface = new UserInterfaceContext();
            Player = new PlayerContext();
            Time = new TimeContext();
        }
    }
}
