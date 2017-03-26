using Scripts.Common;
using Scripts.Common.ObjectPools;
using Scripts.Network;
using UnityEngine;

namespace Scripts.Client.Contexts
{
    public class NetworkContext
    {
        public NetworkContext()
        {
            var networkPrefab = GameObject.Find(Consts.DefaultSceneObjects.Network);

            if (networkPrefab == null)
            {
                networkPrefab = CreateNetworkPrefab();
            }
        }

        private GameObject CreateNetworkPrefab()
        {
            return ObjectPoolManager.Local.CreateSingle(Consts.Prefab.Common.Network);
        }
    }
}
