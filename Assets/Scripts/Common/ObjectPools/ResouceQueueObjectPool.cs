using System;
using System.Collections.Generic;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace Scripts.Common.ObjectPools
{
    public class ResouceQueueObjectPool : IObjectPool
    {
        private readonly string resourceName;

        private UnityObject prefab;

        protected Queue<GameObject> Queue;

        public int FreeObjectsCount
        {
            get
            {
                return Queue.Count;
            }
        }

        public int TotalObjectsCount
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

        public ObjectPoolType ObjectPoolType
        {
            get;
            set;
        }

        public int IncreaseBatchSize
        {
            get;
            set;
        }

        public ResouceQueueObjectPool(int capacity, string resourceName, ObjectPoolType poolType = ObjectPoolType.IncreaseByOne)
        {
            ObjectPoolType = poolType;
            IncreaseBatchSize = ObjectPoolManager.IncreaseBatchSize;
            TotalObjectsCount = capacity;
            Queue = new Queue<GameObject>(capacity);
            this.resourceName = resourceName;
            InitializeObjects(capacity);
        }

        public GameObject Get()
        {
            if (!HasFreeObject)
            {
                IncreaseSize();                
            }

            var result = Queue.Dequeue();
            result.SetActive(true);
            return result;
        }

        public void Return(GameObject gameObject)
        {
            gameObject.SetActive(false);
            Queue.Enqueue(gameObject);
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
            if (prefab == null)
            {
                prefab = Resources.Load(resourceName);
            }

            for (int i = 0; i < count; i++)
            {
                var gameObject = (GameObject)UnityObject.Instantiate(prefab);
                gameObject.SetActive(false);
                Queue.Enqueue(gameObject);
                TotalObjectsCount++;
            }
        }
    }
}
