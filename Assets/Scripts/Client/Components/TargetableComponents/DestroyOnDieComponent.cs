namespace Scripts.Client.Components.TargetableComponents
{
    public class DestroyOnDieComponent : BaseDieComponent
    {
        public override void OnDie()
        {
            Destroy(gameObject);
        }
    }
}
