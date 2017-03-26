using Scripts.Common;
using Scripts.Common.ObjectPools;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Client.Contexts
{
    public class ConsoleContext : ChangableContext
    {
        private GameObject consoleObject;

        private Text textComponent;

        private bool isOpened;

        private string buffer;
        private int lineNumbers;

        public ConsoleContext(GameObject userInterfaceObject)
        {
            buffer = string.Empty;

            consoleObject = GameObject.Find(Consts.DefaultSceneObjects.Console);

            if(consoleObject == null)
            {
                consoleObject = ObjectPoolManager.Local.CreateSingle(Consts.Prefab.UserInterface.Console);
            }

            Close();
            CacheTextComponent();
        }

        public bool IsOpened
        {
            get
            {
                return isOpened;
            }
            private set
            {
                isOpened = value;
                consoleObject.SetActive(value);
            }
        }

        public void Open()
        {
            IsOpened = true;
        }

        public void Close()
        {
            IsOpened = false;            
        }

        public void WriteLine(string text)
        {
            buffer += text + "\n";
            lineNumbers++;

            if(lineNumbers > 12)
            {
                lineNumbers = 12;
                buffer = buffer.Substring(buffer.IndexOf("\n") + 1);
            }

            MarkAsChanged();          
        }

        public void Update()
        {
            textComponent.text = buffer;
            MarkAsVisited();
        }

        private void CacheTextComponent()
        {
            var outputTransform = consoleObject.transform.FindChild(Consts.Prefab.UserInterface.ConsoleOutput);
            var textTransform = outputTransform.FindChild(Consts.Prefab.Common.Text);
            textComponent = textTransform.GetComponent<Text>();
        }
    }
}
