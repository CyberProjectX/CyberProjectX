namespace Scripts.Network.SyncStrategies
{
    public interface ISyncStrategy
    {
        void Initialize();

        void ReceiveData(PhotonStream stream, PhotonMessageInfo info);

        void SyncData();
    }
}
