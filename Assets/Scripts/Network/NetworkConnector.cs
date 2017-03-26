using Invector.CharacterController;
using Scripts.Client.Contexts;
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
                Log.Info("Connecting...");
                PhotonNetwork.autoJoinLobby = true;
                PhotonNetwork.ConnectUsingSettings(Consts.Network.ApplicationVersion);
                Log.Info("Connected.");
            }
        }    
        
        void OnJoinedLobby()
        {
            Log.Info("Joining...");
            PhotonNetwork.JoinOrCreateRoom(Consts.Network.DefaultRoom, new RoomOptions(), TypedLobby.Default);
            Log.Info("Joined.");
        }    

        void OnJoinedRoom()
        {
            Log.Info("Instantiating...");
            var person = ObjectPoolManager.Network.CreateSingle(Consts.Network.NetworkPersonController, new Vector3(25, 0, 30), Quaternion.identity);
            
            person.AddComponent<vThirdPersonController>();
            person.AddComponent<vThirdPersonInput>();

            Log.Info("Instantiated.");
        }
    }
}
