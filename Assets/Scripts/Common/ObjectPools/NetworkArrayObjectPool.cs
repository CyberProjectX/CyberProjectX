using UnityEngine;

namespace Scripts.Common.ObjectPools
{
    public class NetworkArrayObjectPool : ArrayObjectPool
    {
        private readonly string resourceName;

        protected NetworkArrayObjectPool(string resourceName, ObjectPoolType poolType = ObjectPoolType.IncreaseByOne) : base(poolType)
        {
            this.resourceName = resourceName;
        }

        public static NetworkArrayObjectPool Create(int capacity, string resourceName, ObjectPoolType poolType = ObjectPoolType.IncreaseByOne)
        {
            var pool = new NetworkArrayObjectPool(resourceName, poolType);
            pool.Initialize(capacity);

            return pool;
        }

        protected override Transform CreatePrefab()
        {
            var gameObject = PhotonNetwork.Instantiate(resourceName, Vector3.zero, Quaternion.identity, Consts.Network.DefaultGroup);

            return gameObject.transform;
        }
    }
}
