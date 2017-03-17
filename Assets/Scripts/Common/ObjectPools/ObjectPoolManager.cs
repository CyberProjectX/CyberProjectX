using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace Scripts.Common.ObjectPools
{
    public class ObjectPoolManager
    {
        public static ObjectPoolManager Instance = new ObjectPoolManager();

        public GameObject CreateSingle(string resourceName)
        {
            var prefab = Resources.Load(resourceName);
            return (GameObject)UnityObject.Instantiate(prefab);
        }
    }
}
