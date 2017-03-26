using Scripts.Common;
using Scripts.Network.SyncStrategies;
using System;
using UnityEngine;

namespace Scripts.Network
{
    public class NetworkCubeController : Photon.MonoBehaviour
    {
        private ISyncStrategy strategy;

        public SyncType SyncType = SyncType.Inter;

        private ISyncStrategy Strategy
        {
            get
            {
                switch (SyncType)
                {
                    case SyncType.Lerp:
                        strategy = strategy is TransformLerpStrategy ? strategy : new TransformLerpStrategy(transform);
                        break;
                    case SyncType.LerpWithFraction:
                        strategy = strategy is TransformLertWithFractionStrategy ? strategy : new TransformLertWithFractionStrategy(transform);
                        break;
                    case SyncType.Inter:
                        strategy = strategy is TransformInterStrategy ? strategy : new TransformInterStrategy(transform);
                        break;
                    default:
                        throw new NotSupportedException();
                }

                return strategy;
            }
        }

        void Update()
        {
            if (photonView.isMine)
            {
                return;
            }

            Strategy.SyncData();
        }

        void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo messsageInfo)
        {

            if (stream.isWriting)
            {
                SendTransformn(stream);
            }
            else if (stream.isReading)
            {
                Strategy.ReceiveData(stream, messsageInfo);
            }
        }

        private void SendTransformn(PhotonStream stream)
        {
            var position = transform.position;
            var rotation = transform.rotation;
            stream.Serialize(ref position);
            stream.Serialize(ref rotation);
        }
    }
}
