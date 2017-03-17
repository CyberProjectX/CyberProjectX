using UnityEngine;

namespace Scripts.Client.Contexts
{
    public class PlayerContext : ChangableContext
    {
        private Vector3 position;

        private Quaternion rotation;

        public Vector3 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
                MarkAsChanged();
            }
        }

        public Quaternion Rotation
        {
            get
            {
                return rotation;
            }
            set
            {
                rotation = value;
                MarkAsChanged();
            }
        }
    }
}
