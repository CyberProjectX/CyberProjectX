using UnityEngine;

namespace Scripts.Client.Components
{
    public class RandomRotationComponent : BaseComponent
    {
        public float MinRotation;
        public float MaxRotation;

        private Quaternion defaultRotation;

        protected override void Awake()
        {
            base.Awake();
            defaultRotation = transform.rotation;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            transform.rotation = defaultRotation * Quaternion.Euler(0, 0, Random.Range(MinRotation, MaxRotation));
        }
    }
}
