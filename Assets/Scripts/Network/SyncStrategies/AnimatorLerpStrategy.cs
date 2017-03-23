using Scripts.Common;
using UnityEngine;

namespace Scripts.Network.SyncStrategies
{
    public class AnimatorLerpStrategy : ISyncStrategy
    {
        private bool nextIsStrafing;
        private bool nextIsGrounded;
        private float nextGroundDistance;
        private float nextVerticalVelocity;
        private float nextInputHorizontal;
        private float nextInputVertical;

        private Animator animator;

        public AnimatorLerpStrategy(Animator animator)
        {
            this.animator = animator;
            Initialize();
        }

        public void Initialize()
        {
            nextIsStrafing = animator.GetBool(Consts.AnimatorParameters.PersonController.IsStrafing);
            nextIsGrounded = animator.GetBool(Consts.AnimatorParameters.PersonController.IsGrounded);
            nextGroundDistance = animator.GetFloat(Consts.AnimatorParameters.PersonController.GroundDistance);
            nextVerticalVelocity = animator.GetFloat(Consts.AnimatorParameters.PersonController.VerticalVelocity);
            nextInputHorizontal = animator.GetFloat(Consts.AnimatorParameters.PersonController.InputHorizontal);
            nextInputVertical = animator.GetFloat(Consts.AnimatorParameters.PersonController.InputVertical);
        }

        public void ReceiveData(PhotonStream stream, PhotonMessageInfo info)
        {
            stream.Serialize(ref nextIsStrafing);
            stream.Serialize(ref nextIsGrounded);
            stream.Serialize(ref nextGroundDistance);
            stream.Serialize(ref nextVerticalVelocity);
            stream.Serialize(ref nextInputHorizontal);
            stream.Serialize(ref nextInputVertical);
        }

        public void SyncData()
        {
            animator.SetBool(Consts.AnimatorParameters.PersonController.IsStrafing, nextIsStrafing);
            animator.SetBool(Consts.AnimatorParameters.PersonController.IsGrounded, nextIsGrounded);

            var groundDistance = Mathf.Lerp(animator.GetFloat(Consts.AnimatorParameters.PersonController.GroundDistance), nextGroundDistance, Time.deltaTime * 20);
            animator.SetFloat(Consts.AnimatorParameters.PersonController.GroundDistance, groundDistance);

            var verticalVelocity = Mathf.Lerp(animator.GetFloat(Consts.AnimatorParameters.PersonController.VerticalVelocity), nextVerticalVelocity, Time.deltaTime * 20);
            animator.SetFloat(Consts.AnimatorParameters.PersonController.VerticalVelocity, verticalVelocity);

            var inputHorizontal = Mathf.Lerp(animator.GetFloat(Consts.AnimatorParameters.PersonController.InputHorizontal), nextInputHorizontal, Time.deltaTime * 20);
            animator.SetFloat(Consts.AnimatorParameters.PersonController.InputHorizontal, inputHorizontal);

            var inputVertical = Mathf.Lerp(animator.GetFloat(Consts.AnimatorParameters.PersonController.InputVertical), nextInputVertical, Time.deltaTime * 20);
            animator.SetFloat(Consts.AnimatorParameters.PersonController.InputVertical, inputVertical);
        }
    }
}
