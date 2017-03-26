using Scripts.Common;
using Scripts.Common.ObjectPools;
using UnityEngine;

namespace Scripts.Client.Contexts
{
    public class UserInterfaceContext
    {
        private GameObject userInterfaceObject;

        public ConsoleContext Console
        {
            get;
            private set;
        }

        public UserInterfaceContext()
        {
            userInterfaceObject = GameObject.Find(Consts.DefaultSceneObjects.UserInterface);

            if(userInterfaceObject == null)
            {
                userInterfaceObject = ObjectPoolManager.Local.CreateSingle(Consts.Prefab.Common.UserInterface);
            }

            Console = new ConsoleContext(userInterfaceObject);
        }
    }
}

