using Scripts.Common;
using Scripts.Common.ObjectPools;
using System;
using UnityEngine;

namespace Scripts.Client.Controllers.Audio
{
    public class WeaponAudioController : BaseController
    {
        public Transform AudioSource;
        public int removeAudioSourceAfter = 3;

        [Header("Vulcan")]
        public AudioClip[] VulcanHit;
        public AudioClip VulcanShot;
        public float VulcanShotDelay;
        public float VulcanHitDelay;

        [Header("Solo Gun")]
        public AudioClip[] SoloGunHit;
        public AudioClip SoloGunShot;
        public float SoloGunShotDelay;
        public float SoloGunHitDelay;

        private float lastShotTime;
        private float lastHitTime;

        public void ShotVulcan(Vector3 position)
        {
            if (lastShotTime + VulcanShotDelay <= Time.time)
            {
                var audioSourceObject = ObjectPoolManager.Local.Create(Consts.Prefab.Common.AudioSource, AudioSource, position);
                ObjectPoolManager.Local.Return(audioSourceObject, removeAudioSourceAfter);

                var audioSource = audioSourceObject.GetComponent<AudioSource>();

                if (audioSource != null)
                {
                    audioSource.pitch = UnityEngine.Random.Range(0.95f, 1f);
                    audioSource.volume = UnityEngine.Random.Range(0.8f, 1f);
                    audioSource.minDistance = 5f;
                    audioSource.loop = false;
                    audioSource.clip = VulcanShot;
                    audioSource.Play();

                    lastShotTime = Time.time;
                }
            }
        }

        public void HitVulcan(Vector3 position)
        {
            if (lastHitTime + VulcanHitDelay <= Time.time)
            {
                var audioSourceObject = ObjectPoolManager.Local.Create(Consts.Prefab.Common.AudioSource, AudioSource, position);
                ObjectPoolManager.Local.Return(audioSourceObject, removeAudioSourceAfter);

                var audioSource = audioSourceObject.GetComponent<AudioSource>();
                if (audioSource != null)
                {
                    audioSource.pitch = UnityEngine.Random.Range(0.95f, 1f);
                    audioSource.volume = UnityEngine.Random.Range(0.6f, 1f);
                    audioSource.minDistance = 7f;
                    audioSource.loop = false;
                    audioSource.clip = VulcanHit[UnityEngine.Random.Range(0, VulcanHit.Length)];
                    audioSource.Play();

                    lastHitTime = Time.time;
                }
            }
        }

        public void ShotSoloGun(Vector3 position)
        {
            if (lastShotTime + SoloGunShotDelay <= Time.time)
            {
                var audioSourceObject = ObjectPoolManager.Local.Create(Consts.Prefab.Common.AudioSource, AudioSource, position);
                ObjectPoolManager.Local.Return(audioSourceObject, removeAudioSourceAfter);

                var audioSource = audioSourceObject.GetComponent<AudioSource>();

                if (audioSource != null)
                {
                    audioSource.pitch = UnityEngine.Random.Range(0.95f, 1f);
                    audioSource.volume = UnityEngine.Random.Range(0.8f, 1f);
                    audioSource.minDistance = 30f;
                    audioSource.loop = false;
                    audioSource.clip = SoloGunShot;
                    audioSource.Play();

                    lastShotTime = Time.time;
                }
            }
        }

        public void HitSoloGun(Vector3 position)
        {
            if (lastHitTime + SoloGunHitDelay <= Time.time)
            {
                var audioSourceObject = ObjectPoolManager.Local.Create(Consts.Prefab.Common.AudioSource, AudioSource, position);
                ObjectPoolManager.Local.Return(audioSourceObject, removeAudioSourceAfter);

                var audioSource = audioSourceObject.GetComponent<AudioSource>();
                if (audioSource != null)
                {
                    audioSource.pitch = UnityEngine.Random.Range(0.95f, 1f);
                    audioSource.volume = UnityEngine.Random.Range(0.8f, 1f);
                    audioSource.minDistance = 50f;
                    audioSource.loop = false;
                    audioSource.clip = SoloGunHit[UnityEngine.Random.Range(0, SoloGunHit.Length)];
                    audioSource.Play();

                    lastHitTime = Time.time;
                }
            }
        }
    }
}
