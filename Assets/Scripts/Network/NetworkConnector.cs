using Invector.CharacterController;
using Scripts.Common;
using Scripts.Common.ObjectPools;
using UnityEngine;

namespace Scritps.Network
{
    public class NetworkConnector : Photon.MonoBehaviour
    {
        public bool OfflineMode = false;

        void Start()
        {
            if (OfflineMode)
            {
                PhotonNetwork.offlineMode = true;
                OnJoinedLobby();
            }
            else
            {
                Debug.Log("Connecting...");
                PhotonNetwork.autoJoinLobby = true;
                PhotonNetwork.ConnectUsingSettings(Consts.Network.ApplicationVersion);
                Debug.Log("Connected.");
            }
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
            var person = ObjectPoolManager.Network.CreateSingle(Consts.Network.NetworkPersonController, new Vector3(25, 0, 30), Quaternion.identity);
            //var person = PhotonNetwork.Instantiate(Consts.Network.NetworkPersonController, new Vector3(25, 0, 30), Quaternion.identity, Consts.Network.DefaultGroup);

            //var cameraObject = GameObject.Find(Consts.DefaultSceneObjects.Camera);
            //var camera = cameraObject.GetComponent<v3rdPersonCamera>();
            //camera.SetTarget(person.transform);

            person.AddComponent<vThirdPersonController>();
            person.AddComponent<vThirdPersonInput>();

            Debug.Log("Instantiated.");
        }
    }
}
