using Scripts.Common.ObjectPools;

namespace Scripts.Client.Components.TargetableComponents
{
    public class NetworkDestroyOnDieComponent : BaseDieComponent
    {        
        public override void OnDie()
        {
            var photonView = GetComponent<PhotonView>();

            if(photonView != null)
            {
                photonView.RPC("Destroy", PhotonTargets.MasterClient, null);
            }            
        }

        [PunRPC]
        protected void Destroy()
        {
            ObjectPoolManager.Network.Return(gameObject);
        }
    }
}
