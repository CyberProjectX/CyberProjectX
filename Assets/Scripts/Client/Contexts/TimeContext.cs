using Scripts.Client.Controllers;
using Scripts.Common;
using Scripts.Common.ObjectPools;
using System;
using UnityEngine;

namespace Scripts.Client.Contexts
{
    public class TimeContext
    {
        private readonly TimeController timeController;

        public TimeContext()
        {
            var timePrefab = GameObject.Find(Consts.DefaultSceneObjects.Time);

            if (timePrefab == null)
            {
                timePrefab = CreateTimePrefab();
            }

            timeController = timePrefab.GetComponent<TimeController>();
        }

        public void Invoke(Action action, float time)
        {
            if (timeController == null)
            {
                Log.Error("TimeController is null");
                return;
            }

            timeController.AddAction(action, time);
        }

        private GameObject CreateTimePrefab()
        {
            return ObjectPoolManager.Local.CreateSingle(Consts.Prefab.Common.Time);
        }
    }
}

