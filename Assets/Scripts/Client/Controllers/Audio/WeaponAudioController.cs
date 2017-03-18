using Forge3D;
using Scripts.Client.Controllers;
using Scripts.Common.ObjectPools;
using UnityEngine;

namespace Assets.Scripts.Client.Controllers.Audio
{
    // todo: rework public Transform AudioSource, return to pool after 3 sec
    public class WeaponAudioController : BaseController
    {
        public Transform AudioSource;

        [Header("Vulcan")]
        public AudioClip[] VulcanHit;
        public AudioClip VulcanShot;
        public float VulcanShotDelay;
        public float VulcanHitDelay;

        private float lastShotTime;
        private float lastHitTime;

        public void ShotVulcan(Vector3 position)
        {
            if (lastShotTime + VulcanShotDelay <= Time.time)
            {
                var audioSource = ObjectPoolManager.Instance.CreateSingle(AudioSource, position).GetComponent<AudioSource>();

                if (audioSource != null)
                {
                    audioSource.pitch = Random.Range(0.95f, 1f);
                    audioSource.volume = Random.Range(0.8f, 1f);
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
                
                var audioSource = ObjectPoolManager.Instance.CreateSingle(AudioSource, position).GetComponent<AudioSource>();
                if (audioSource != null)
                {
                    audioSource.pitch = Random.Range(0.95f, 1f);
                    audioSource.volume = Random.Range(0.6f, 1f);
                    audioSource.minDistance = 7f;
                    audioSource.loop = false;
                    audioSource.clip = VulcanHit[Random.Range(0, VulcanHit.Length)];
                    audioSource.Play();

                    lastHitTime = Time.time;
                }
            }
        }
    }
}
