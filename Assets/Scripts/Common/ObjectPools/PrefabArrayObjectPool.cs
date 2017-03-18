using System;
using System.Collections.Generic;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace Scripts.Common.ObjectPools
{
    public class PrefabArrayObjectPool : IObjectPool
    {
        private List<GameObject> pool;
        private int lastActiveIndex;
        private Transform prefab;

        public int FreeObjectsCount
        {
            get;
            private set;
        }

        public bool HasFreeObject
        {
            get
            {
                return FreeObjectsCount > 0;
            }
        }

        public int IncreaseBatchSize
        {
            get;
            set;
        }

        public ObjectPoolType ObjectPoolType
        {
            get;
            set;
        }

        public int TotalObjectsCount
        {
            get
            {
                return pool.Count;
            }
        }

        public PrefabArrayObjectPool(int capacity, Transform prefab, ObjectPoolType poolType = ObjectPoolType.IncreaseByOne)
        {
            this.prefab = prefab;
            IncreaseBatchSize = ObjectPoolManager.IncreaseBatchSize;
            ObjectPoolType = poolType;
            lastActiveIndex = -1;
            FreeObjectsCount = 0;
            pool = new List<GameObject>(capacity);
            InitializeObjects(capacity);
        }

        public GameObject Get()
        {
            if (HasFreeObject)
            {
                for (int i = lastActiveIndex + 1; i < pool.Count; i++)
                {
                    var item = pool[i];
                    if (!item.activeSelf)
                    {
                        lastActiveIndex = i;
                        FreeObjectsCount--;
                        item.SetActive(true);
                        return item;
                    }
                }

                for (int i = 0; i <= lastActiveIndex; i++)
                {
                    var item = pool[i];
                    if (!item.activeSelf)
                    {
                        lastActiveIndex = i;
                        FreeObjectsCount--;
                        item.SetActive(true);
                        return item;
                    }
                }                
            }

            lastActiveIndex = pool.Count - 1;
            IncreaseSize();

            return Get();
        }

        public void Return(GameObject gameObject)
        {
            gameObject.SetActive(false);
            FreeObjectsCount++;
        }

        private void IncreaseSize()
        {
            switch (ObjectPoolType)
            {
                case ObjectPoolType.IncreaseByOne:
                    IncreaseSizeByOne();
                    break;
                case ObjectPoolType.IncreaseByBatch:
                    IncreaseSizeByBatch();
                    break;
                case ObjectPoolType.DoubleIncrease:
                    DoubleIncreaseSize();
                    break;
                default:
                    throw new NotSupportedException(string.Format("Method does not supporte {0} = {1}", typeof(ObjectPoolType).FullName, ObjectPoolType));
            }
        }

        private void IncreaseSizeByOne()
        {
            InitializeObjects(1);
        }

        private void IncreaseSizeByBatch()
        {
            InitializeObjects(IncreaseBatchSize);
        }

        private void DoubleIncreaseSize()
        {
            InitializeObjects(TotalObjectsCount);
        }

        private void InitializeObjects(int count)
        {
            for(int i = 0; i < count; i++)
            {
                var transform = (Transform)UnityObject.Instantiate(prefab);
                transform.gameObject.SetActive(false);
                pool.Add(transform.gameObject);
                FreeObjectsCount++;
            }
        }
    }
}
