using UnityEngine;

namespace Scripts.Network.SyncStrategies
{
    public class TransformLertWithFractionStrategy : ISyncStrategy
    {
        private Vector3 latestCorrectPosition;
        private Vector3 onUpdatePosition;

        private Quaternion latestCorrectRotation;
        private Quaternion onUpdateRotation;

        private float fraction;
        private Transform transform;

        public TransformLertWithFractionStrategy(Transform transform)
        {
            this.transform = transform;
            Initialize();
        }

        public void Initialize()
        {
            latestCorrectPosition = transform.position;
            onUpdatePosition = transform.position;

            latestCorrectRotation = transform.rotation;
            onUpdateRotation = transform.rotation;
        }

        public void ReceiveData(PhotonStream stream, PhotonMessageInfo info)
        {
            var position = Vector3.zero;
            var rotation = Quaternion.identity;

            stream.Serialize(ref position);
            stream.Serialize(ref rotation);

            latestCorrectPosition = position;
            onUpdatePosition = transform.position;

            latestCorrectRotation = rotation;
            onUpdateRotation = transform.rotation;

            fraction = 0;
        }

        public void SyncData()
        {
            fraction += Time.deltaTime * 9;
            transform.position = Vector3.Lerp(onUpdatePosition, latestCorrectPosition, fraction);
            transform.rotation = Quaternion.Slerp(onUpdateRotation, latestCorrectRotation, fraction);
        }
    }
}
