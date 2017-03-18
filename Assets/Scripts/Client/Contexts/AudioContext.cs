﻿using Assets.Scripts.Client.Controllers.Audio;
using Scripts.Client.Controllers.Audio;
using Scripts.Common;
using Scripts.Common.ObjectPools;
using UnityEngine;

namespace Scripts.Client.Contexts
{
    public class AudioContext
    {
        private readonly AudioController audioController;
        private readonly WeaponAudioController weaponAudioController;

        public AudioContext()
        {
            var audioPrefab = GameObject.Find(Consts.DefaultSceneObjects.Audio);

            if (audioPrefab == null)
            {
                audioPrefab = CreateAudioPrefab();
            }

            audioController = audioPrefab.GetComponent<AudioController>();
            weaponAudioController = audioPrefab.GetComponent<WeaponAudioController>();
        }

        public WeaponAudioController Weapon
        {
            get
            {
                return weaponAudioController;
            }
        }

        private GameObject CreateAudioPrefab()
        {
            return ObjectPoolManager.Instance.CreateSingle(Consts.Prefab.Common.Audio);
        }
    }
}