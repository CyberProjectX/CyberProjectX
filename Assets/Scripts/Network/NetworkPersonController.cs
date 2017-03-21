namespace Scripts.Network
{
    public class NetworkPersonController : Photon.MonoBehaviour
    {
        void Start()
        {

        }

        void Update()
        {

        }

        void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo messsageInfo)
        {
            var position = transform.position;
            var rotation = transform.rotation;

            stream.Serialize(ref position);
            stream.Serialize(ref rotation);

            if (stream.isReading)
            {
                transform.position = position;
                transform.rotation = rotation;
            }
        }
    }
}
