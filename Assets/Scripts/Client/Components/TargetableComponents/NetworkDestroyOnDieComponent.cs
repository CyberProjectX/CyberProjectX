namespace Scripts.Client.Components.TargetableComponents
{
    public class NetworkDestroyOnDieComponent : BaseDieComponent
    {
        public override void OnDie()
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
