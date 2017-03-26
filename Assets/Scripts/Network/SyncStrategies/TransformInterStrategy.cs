using Scripts.Common;
using System;
using UnityEngine;

namespace Scripts.Network.SyncStrategies
{
    public class TransformInterStrategy : ISyncStrategy
    {
        internal struct State
        {
            public double Timestamp;
            public Vector3 Position;
            public Quaternion Rotation;
        }

        private State[] bufferedState = new State[20];

        private int timestampCount;

        public double InterpolationDelay = 0.15;

        private Transform transform;

        public TransformInterStrategy(Transform transform)
        {
            this.transform = transform;
        }

        public void Initialize()
        {            
        }

        public void ReceiveData(PhotonStream stream, PhotonMessageInfo info)
        {
            var position = Vector3.zero;
            var rotation = Quaternion.identity;
            stream.Serialize(ref position);
            stream.Serialize(ref rotation);

            for (int i = bufferedState.Length - 1; i >= 1; i--)
            {
                bufferedState[i] = bufferedState[i - 1];
            }

            var state = new State
            {
                Timestamp = info.timestamp,
                Position = position,
                Rotation = rotation
            };
            
            bufferedState[0] = state;

            timestampCount = Mathf.Min(timestampCount + 1, bufferedState.Length);

            for (int i = 0; i < timestampCount - 1; i++)
            {
                if (bufferedState[i].Timestamp < bufferedState[i + 1].Timestamp)
                {
                    Log.Info("State inconsistent");
                }
            }
        }

        public void SyncData()
        {
            double currentTime = PhotonNetwork.time;
            double interpolationTime = currentTime - InterpolationDelay;

            if (bufferedState[0].Timestamp > interpolationTime)
            {
                for (int i = 0; i < timestampCount; i++)
                {
                    if (bufferedState[i].Timestamp <= interpolationTime || i == timestampCount - 1)
                    {
                        // The state one slot newer (<100ms) than the best playback state
                        State rhs = bufferedState[Mathf.Max(i - 1, 0)];
                        // The best playback state (closest to 100 ms old (default time))
                        State lhs = bufferedState[i];

                        double diffBetweenUpdates = rhs.Timestamp - lhs.Timestamp;
                        var t = 0.0f;

                        // As the time difference gets closer to 100 ms t gets closer to 1 in 
                        // which case rhs is only used
                        if (diffBetweenUpdates > 0.0001)
                        {
                            t = (float)((interpolationTime - lhs.Timestamp) / diffBetweenUpdates);
                        }

                        // if t=0 => lhs is used directly

                        transform.position = Vector3.Lerp(lhs.Position, rhs.Position, t);
                        transform.rotation = Quaternion.Slerp(lhs.Rotation, rhs.Rotation, t);
                        return;
                    }
                }
            }
            else
            {
                // If our interpolation time catched up with the time of the latest update:
                // Simply move to the latest known position.

                //Log.Info("Lerping!");
                State latest = bufferedState[0];

                transform.position = Vector3.Lerp(transform.position, latest.Position, Time.deltaTime * 20);
                transform.rotation = Quaternion.Slerp(transform.rotation, latest.Rotation, Time.deltaTime * 20);
            }
        }
    }
}
