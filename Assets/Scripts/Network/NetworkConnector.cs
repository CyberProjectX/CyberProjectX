using Scripts.Common;
using UnityEngine;

namespace Scritps.Network
{
    public class NetworkConnector : Photon.MonoBehaviour
    {
        void Start()
        {
            Debug.Log("Connecting...");
            PhotonNetwork.autoJoinLobby = true;
            PhotonNetwork.ConnectUsingSettings(Consts.Network.ApplicationVersion);
            Debug.Log("Connected.");
        }    
        
        void OnJoinedLobby()
        {
            Debug.Log("Joining...");
            PhotonNetwork.JoinOrCreateRoom(Consts.Network.DefaultRoom, new RoomOptions(), TypedLobby.Default);
            Debug.Log("Joined.");
        }    

        void OnJoinedRoom()
        {
            Debug.Log("Instantiating...");
            var person = PhotonNetwork.Instantiate(Consts.Network.PersonController, new Vector3(25, 0, 30), Quaternion.identity, 0);

            var cameraObject = GameObject.Find(Consts.DefaultSceneObjects.Camera);
            var camera = cameraObject.GetComponent<v3rdPersonCamera>();
            camera.SetTarget(person.transform);
            Debug.Log("Instantiated.");
        }
    }
}
