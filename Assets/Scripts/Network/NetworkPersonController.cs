using Invector.CharacterController;
using Scripts.Common;
using UnityEngine;

namespace Scripts.Network
{
    //todo: use interpolation from CubeInter.cs
    public class NetworkPersonController : Photon.MonoBehaviour
    {
        private Vector3 nextPosition;
        private Quaternion nextRotation;

        private Animator animator;

        void Start()
        {
            nextPosition = transform.position;
            nextRotation = transform.rotation;            
        }

        void Update()
        {
            if (photonView.isMine)
            {
                return;
            }

            transform.position = Vector3.Lerp(transform.position, nextPosition, Time.deltaTime * 20);
            transform.rotation = Quaternion.Slerp(transform.rotation, nextRotation, Time.deltaTime * 20);
        }

        void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo messsageInfo)
        {
            if(animator == null)
            {
                animator = GetComponent<Animator>();
            }

            if (stream.isWriting)
            {
                SendTransformn(stream);
                SendAnimator(stream);
            }
            else if (stream.isReading)
            {
                ReceiveTransform(stream);
                ReceiveAnimator(stream);
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

        private void ReceiveTransform(PhotonStream stream)
        {
            nextPosition = (Vector3)stream.ReceiveNext();
            nextRotation = (Quaternion)stream.ReceiveNext();
        }

        private void ReceiveAnimator(PhotonStream stream)
        {
            var isStrafing = false;
            stream.Serialize(ref isStrafing);
            animator.SetBool(Consts.AnimatorParameters.PersonController.IsStrafing, isStrafing);

            var isGrounded = false;
            stream.Serialize(ref isGrounded);
            animator.SetBool(Consts.AnimatorParameters.PersonController.IsGrounded, isGrounded);

            var groundDistance = 0f;
            stream.Serialize(ref groundDistance);
            animator.SetFloat(Consts.AnimatorParameters.PersonController.GroundDistance, groundDistance);

            var verticalVelocity = 0f;
            stream.Serialize(ref verticalVelocity);
            animator.SetFloat(Consts.AnimatorParameters.PersonController.VerticalVelocity, verticalVelocity);

            var inputHorizontal = 0f;
            stream.Serialize(ref inputHorizontal);
            animator.SetFloat(Consts.AnimatorParameters.PersonController.InputHorizontal, inputHorizontal);

            var inputVertical = 0f;
            stream.Serialize(ref inputVertical);
            animator.SetFloat(Consts.AnimatorParameters.PersonController.InputVertical, inputVertical);
        }
    }
}
