using Scripts.Client.Contexts;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace Scripts.Common.ObjectPools
{
    public class ObjectPoolManager
    {
        public static ObjectPoolManager Instance = new ObjectPoolManager();

        public static int IncreaseBatchSize = 10;

        public static int InitialSize = 1;

        protected Dictionary<string, IObjectPool> Cache;

        protected ObjectPoolManager()
        {
            Cache = new Dictionary<string, IObjectPool>();
        }

        public GameObject CreateSingle(string resourceName)
        {
            var prefab = Resources.Load(resourceName);
            return (GameObject)UnityObject.Instantiate(prefab);
        }

        public GameObject CreateSingle(UnityObject prefab)
        {
            return (GameObject)UnityObject.Instantiate(prefab);
        }

        public GameObject CreateSingle(Transform transform, Vector3 position, Transform parent = null)
        {
            return CreateSingle(transform, position, Quaternion.identity, parent);
        }

        public GameObject CreateSingle(Transform transform, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            var result = (GameObject)UnityObject.Instantiate(transform.gameObject);

            result.transform.position = position;
            result.transform.rotation = rotation;

            result.transform.SetParent(parent);

            return result;
        }

        public GameObject Create(string resourceName)
        {
            if (!Cache.ContainsKey(resourceName))
            {
                Cache.Add(resourceName, new ResouceQueueObjectPool(InitialSize, resourceName));
            }

            return Cache[resourceName].Get();
        }

        public GameObject Create(string poolName, Transform prefab)
        {       
            return Create(poolName, prefab, Vector3.zero);
        }

        public GameObject Create(string poolName, Transform prefab, Vector3 position, Transform parent = null)
        {
            return Create(poolName, prefab, position, Quaternion.identity, parent);
        }

        public GameObject Create(string poolName, Transform prefab, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            if (!Cache.ContainsKey(poolName))
            {
                Cache.Add(poolName, new PrefabArrayObjectPool(InitialSize, prefab));
            }

            var result = Cache[poolName].Get();

            result.transform.position = position;
            result.transform.rotation = rotation;

            result.transform.SetParent(parent);

            return result;
        }

        /// <summary>
        /// Name of the game object should contain object pool name.
        /// If name does not contains object pool name than object will be destroyed instead of returning to object pool.
        /// </summary>
        /// <param name="gameObject"></param>
        public void Return(GameObject gameObject)
        {
            var name = gameObject.name.Replace("(Clone)", "");
            if (Cache.ContainsKey(name))
            {
                Cache[name].Return(gameObject);
            }
            else
            {
                UnityObject.Destroy(gameObject);
                Debug.Log(string.Format("GameObject {0} has been destroyed", gameObject.name));
            }            
        }

        /// <summary>
        /// Returns object to pool.
        /// </summary>
        /// <param name="gameObject">Object to return.</param>
        /// <param name="time">After time in seconds.</param>
        public void Return(GameObject gameObject, float time)
        {
            GameContext.Current.Time.Invoke(CreateReturnAction(gameObject), time);
        }

        private Action CreateReturnAction(GameObject gameObject)
        {
            return () =>
            {
                Return(gameObject);
            };
        }
    }
}
