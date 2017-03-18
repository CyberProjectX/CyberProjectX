using UnityEngine;

namespace Scripts.Client.Components
{
    public class RandomScaleComponent : BaseComponent
    {
        public float MinScale;
        public float MaxScale;

        private Vector3 defaultScale;

        protected override void Awake()
        {
            base.Awake();
            defaultScale = transform.localScale;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            transform.localScale = defaultScale * Random.Range(MinScale, MaxScale);
        }
    }
}
