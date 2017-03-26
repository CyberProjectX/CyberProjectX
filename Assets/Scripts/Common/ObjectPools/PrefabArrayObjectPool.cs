using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace Scripts.Common.ObjectPools
{
    public class PrefabArrayObjectPool : ArrayObjectPool
    {
        private readonly Transform prefab;

        protected PrefabArrayObjectPool(Transform prefab, ObjectPoolType poolType = ObjectPoolType.IncreaseByOne) : base(poolType)
        {
            this.prefab = prefab;
        }

        public static PrefabArrayObjectPool Create(int capacity, Transform prefab, ObjectPoolType poolType = ObjectPoolType.IncreaseByOne)
        {
            var pool = new PrefabArrayObjectPool(prefab, poolType);
            pool.Initialize(capacity);

            return pool;
        }

        protected override Transform CreatePrefab()
        {
            return (Transform)UnityObject.Instantiate(prefab);
        }
    }    
}
