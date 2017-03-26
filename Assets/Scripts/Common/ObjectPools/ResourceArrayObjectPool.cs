using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace Scripts.Common.ObjectPools
{
    public class ResourceArrayObjectPool : ArrayObjectPool
    {
        private readonly string resourceName;

        private UnityObject prefab;

        protected ResourceArrayObjectPool(string resourceName, ObjectPoolType poolType = ObjectPoolType.IncreaseByOne) : base(poolType)
        {
            this.resourceName = resourceName;
        }

        public ResourceArrayObjectPool Create(int capacity, string resourceName, ObjectPoolType poolType = ObjectPoolType.IncreaseByOne)
        {
            var pool = new ResourceArrayObjectPool(resourceName, poolType);
            pool.Initialize(capacity);

            return pool;
        }

        protected override Transform CreatePrefab()
        {
            if (prefab == null)
            {
                prefab = Resources.Load(resourceName);
            }

            var gameObject = (GameObject)UnityObject.Instantiate(prefab);

            return gameObject.transform;
        }
    }
}
