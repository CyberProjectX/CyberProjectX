namespace Scripts.Common.ObjectPools
{
    public interface IObjectPoolCreatable
    {
        /// <summary>
        /// This function is called when the object becomes enabled and active.
        /// </summary>
        void OnEnable();

        /// <summary>
        /// This function is called when the behaviour becomes disabled () or inactive.
        /// </summary>
        void OnDisable();
    }
}
