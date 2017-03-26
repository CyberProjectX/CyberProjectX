using Scripts.Client.Contexts;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Common.ObjectPools
{
    public class NetworkObjectPoolManager
    {
        protected Dictionary<string, IObjectPool> Cache;

        public NetworkObjectPoolManager()
        {
            Cache = new Dictionary<string, IObjectPool>();
        }

        public GameObject CreateSingle(string resourceName, Vector3 position, Quaternion rotation)
        {
            return PhotonNetwork.Instantiate(resourceName, position, rotation, Consts.Network.DefaultGroup);
        }

        public GameObject Create(string resourceName, Vector3 position, Quaternion rotation)
        {
            //if (!Cache.ContainsKey(resourceName))
            //{
            //    Cache.Add(resourceName, NetworkArrayObjectPool.Create(ObjectPoolManager.InitialSize, resourceName));
            //}

            //var result = Cache[resourceName].Get();

            //result.transform.position = position;
            //result.transform.rotation = rotation;

            var result = CreateSingle(resourceName, position, rotation);

            return result;
        }

        /// <summary>
        /// Name of the game object should contain object pool name.
        /// If name does not contains object pool name than object will be destroyed instead of returning to object pool.
        /// </summary>
        /// <param name="gameObject"></param>
        public void Return(GameObject gameObject)
        {
            //var name = gameObject.name.Replace("(Clone)", "");
            //if (Cache.ContainsKey(name))
            //{
            //    Cache[name].Return(gameObject);
            //}
            //else
            //{
            //    PhotonNetwork.Destroy(gameObject);
            //    Log.Info(string.Format("GameObject {0} has been destroyed", gameObject.name));
            //}

            
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
