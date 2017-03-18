using UnityEngine;

namespace Scripts.Common.ObjectPools
{
    public interface IObjectPool
    {
        GameObject Get();

        void Return(GameObject gameObject);

        int FreeObjectsCount
        {
            get;
        }

        int TotalObjectsCount
        {
            get;
        }

        bool HasFreeObject
        {
            get;
        }

        int IncreaseBatchSize
        {
            get;
            set;
        }

        ObjectPoolType ObjectPoolType
        {
            get;
            set;
        }
    }
}
