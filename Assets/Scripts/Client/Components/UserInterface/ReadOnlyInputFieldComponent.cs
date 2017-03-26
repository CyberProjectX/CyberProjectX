using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Scripts.Client.Components.UserInterface
{
    [AddComponentMenu("UI/Read Only Input Field", 32)]
    public class ReadOnlyInputFieldComponent : InputField
    {
        private Event rocessingEvent = new Event();

        public override void OnUpdateSelected(BaseEventData eventData)
        {
            if (!isFocused)
                return;

            bool consumedEvent = false;
            while (Event.PopEvent(rocessingEvent))
            {
                if (rocessingEvent.rawType == EventType.KeyDown)
                {
                    if (!IsAllowedCombination(rocessingEvent))
                    {
                        continue;
                    }

                    consumedEvent = true;

                    var shouldContinue = KeyPressed(rocessingEvent);
                    if (shouldContinue == EditState.Finish)
                    {
                        DeactivateInputField();
                        break;
                    }
                }
            }

            if (consumedEvent)
            {
                UpdateLabel();
            }

            eventData.Use();
        }

        private bool IsAllowedCombination(Event evt)
        {
            var currentEventModifiers = evt.modifiers;
            RuntimePlatform rp = Application.platform;
            bool isMac = (rp == RuntimePlatform.OSXEditor || rp == RuntimePlatform.OSXPlayer || rp == RuntimePlatform.OSXWebPlayer);
            bool ctrl = isMac ? (currentEventModifiers & EventModifiers.Command) != 0 : (currentEventModifiers & EventModifiers.Control) != 0;

            switch (evt.keyCode)
            {
                case KeyCode.Home:
                case KeyCode.End:
                case KeyCode.LeftControl:
                case KeyCode.RightControl:
                {
                    return true;
                }

                // Select All
                case KeyCode.A:
                {
                    if (ctrl)
                    {
                        return true;
                    }
                    break;
                }

                // Copy
                case KeyCode.C:
                {
                    if (ctrl)
                    {
                        return true;
                    }
                    break;
                }

                case KeyCode.LeftArrow:
                case KeyCode.RightArrow:
                case KeyCode.UpArrow:
                case KeyCode.DownArrow:
                {
                    return true;
                }
            }

            return false;
        }
    }
}
