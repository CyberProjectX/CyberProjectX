using Scripts.Client.Components;
using Scripts.Client.Contexts;
using Scripts.Common.ObjectPools;
using UnityEngine;

namespace Scripts.Common
{
    public class Log : BaseComponent
    {
        public bool LogToGameConsole = true;

        public bool LogToDebug = true;

        protected static Log Instance;

        public static void Info(string message)
        {
            if(Instance == null)
            {
                var log = Create();
                log.InfoInternal(message);
            }
            else
            {
                Instance.InfoInternal(message);
            }            
        }

        public static void Warning(string message)
        {
            if (Instance == null)
            {
                var log = Create();
                log.WarningInternal(message);
            }
            else
            {
                Instance.WarningInternal(message);
            }
        }

        public static void Error(string message)
        {
            if (Instance == null)
            {
                var log = Create();
                log.ErrorInternal(message);
            }
            else
            {
                Instance.ErrorInternal(message);
            }
        }

        private static Log Create()
        {
            var logObject = ObjectPoolManager.Local.CreateSingle(Consts.DefaultSceneObjects.Log);
            var log = logObject.GetComponent<Log>();
            return log;
        }

        protected void Start()
        {
            Instance = this;
        }

        protected void InfoInternal(string message)
        {
            if (Instance.LogToGameConsole)
            {
                GameContext.Current.UserInterface.Console.WriteLine(message);
            }

            if (Instance.LogToDebug)
            {
                Debug.Log(message);
            }
        }

        protected void WarningInternal(string message)
        {
            if (Instance.LogToGameConsole)
            {
                GameContext.Current.UserInterface.Console.WriteLine(message);
            }

            if (Instance.LogToDebug)
            {
                Debug.LogWarning(message);
            }
        }

        protected void ErrorInternal(string message)
        {
            if (Instance.LogToGameConsole)
            {
                GameContext.Current.UserInterface.Console.WriteLine(message);
            }

            if (Instance.LogToDebug)
            {
                Debug.LogError(message);
            }
        }
    }
}
