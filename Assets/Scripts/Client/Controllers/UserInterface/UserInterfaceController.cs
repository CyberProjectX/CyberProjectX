using Scripts.Client.Contexts;
using UnityEngine;

namespace Scripts.Client.Controllers.UserInterface
{
    public class UserInterfaceController : BaseController
    {
        // Update is called once per frame
        protected void Update()
        {
            TrackKeyPress();
            UpdateConsole();
        }

        private void TrackKeyPress()
        {
            if (Input.GetKeyDown(KeyCode.BackQuote))
            {
                var console = GameContext.Current.UserInterface.Console;
                if (console.IsOpened)
                {
                    console.Close();
                }
                else
                {
                    console.Open();
                }
            }
        }

        private void UpdateConsole()
        {
            var console = GameContext.Current.UserInterface.Console;
            if (console.IsOpened && console.IsContextChanged)
            {
                console.Update();
            }
        }
    }
}
