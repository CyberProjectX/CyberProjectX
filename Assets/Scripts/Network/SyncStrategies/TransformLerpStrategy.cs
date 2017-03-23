using UnityEngine;

namespace Scripts.Network.SyncStrategies
{
    public class TransformLerpStrategy : ISyncStrategy
    {
        private Vector3 nextPosition;
        private Quaternion nextRotation;        

        private Transform transform;

        public TransformLerpStrategy(Transform transform)
        {
            this.transform = transform;
            Initialize();
        }

        public void Initialize()
        {
            nextPosition = transform.position;
            nextRotation = transform.rotation;            
        }

        public void ReceiveData(PhotonStream stream, PhotonMessageInfo info)
        {
            nextPosition = (Vector3)stream.ReceiveNext();
            nextRotation = (Quaternion)stream.ReceiveNext();            
        }

        public void SyncData()
        {
            transform.position = Vector3.Lerp(transform.position, nextPosition, Time.deltaTime * 20);
            transform.rotation = Quaternion.Slerp(transform.rotation, nextRotation, Time.deltaTime * 20);            
        }
    }
}
