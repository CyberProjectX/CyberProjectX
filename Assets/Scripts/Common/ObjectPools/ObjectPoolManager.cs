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

        public GameObject CreateSingle(UnityObject prefab)
        {
            return (GameObject)UnityObject.Instantiate(prefab);
        }

        public GameObject CreateSingle(Transform transform, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            var result = (GameObject)UnityObject.Instantiate(transform.gameObject);

            result.transform.position = position;
            result.transform.rotation = rotation;

            result.transform.SetParent(parent);

            return result;
        }
    }
}
