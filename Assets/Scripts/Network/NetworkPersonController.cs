using Scripts.Common;
using Scripts.Network.SyncStrategies;
using System;
using UnityEngine;

namespace Scripts.Network
{
    public class NetworkPersonController : Photon.MonoBehaviour
    {       
        private ISyncStrategy[] strategies;

        private Animator animator;

        public SyncType SyncType = SyncType.Inter;

        void Start()
        {
            if (animator == null)
            {
                animator = GetComponent<Animator>();
            }

            strategies = new ISyncStrategy[2];

            switch (SyncType)
            {
                case SyncType.Lerp:
                    strategies[0] = new TransformLerpStrategy(transform);
                    break;
                case SyncType.LerpWithFraction:
                    strategies[0] = new TransformLertWithFractionStrategy(transform);
                    break;
                case SyncType.Inter:
                    strategies[0] = new TransformInterStrategy(transform);
                    break;
                default:
                    throw new NotSupportedException();
            }

            strategies[1] = new AnimatorLerpStrategy(animator);
        }

        void Update()
        {
            if (photonView.isMine)
            {
                return;
            }

            foreach(var strategy in strategies)
            {
                strategy.SyncData();
            }
        }

        void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo messsageInfo)
        {            

            if (stream.isWriting)
            {
                SendTransformn(stream);
                SendAnimator(stream);
            }
            else if (stream.isReading)
            {
                foreach (var strategy in strategies)
                {
                    strategy.ReceiveData(stream, messsageInfo);
                }                
            }
        }

        private void SendTransformn(PhotonStream stream)
        {
            var position = transform.position;
            var rotation = transform.rotation;
            stream.Serialize(ref position);
            stream.Serialize(ref rotation);
        }

        private void SendAnimator(PhotonStream stream)
        {
            var isStrafing = animator.GetBool(Consts.AnimatorParameters.PersonController.IsStrafing);            
            stream.Serialize(ref isStrafing);

            var isGrounded = animator.GetBool(Consts.AnimatorParameters.PersonController.IsGrounded);
            stream.Serialize(ref isGrounded);

            var groundDistance = animator.GetFloat(Consts.AnimatorParameters.PersonController.GroundDistance);
            stream.Serialize(ref groundDistance);

            var verticalVelocity = animator.GetFloat(Consts.AnimatorParameters.PersonController.VerticalVelocity);
            stream.Serialize(ref verticalVelocity);

            var inputHorizontal = animator.GetFloat(Consts.AnimatorParameters.PersonController.InputHorizontal);
            stream.Serialize(ref inputHorizontal);

            var inputVertical = animator.GetFloat(Consts.AnimatorParameters.PersonController.InputVertical);
            stream.Serialize(ref inputVertical);
        }
    }
}
