namespace Scripts.Common
{
    public static class Consts
    {
        public static class Network
        {
            public const string ApplicationVersion = "0.1";

            public const string DefaultRoom = "DefaultRoom";

            public const string PersonController = "PersonController";
        }

        public static class DefaultSceneObjects
        {
            public const string Time = "Time";

            public const string Audio = "Audio";

            public const string Camera = "3rdPersonCamera";
        }

        public static class Prefab
        {
            public static class Common
            {
                public const string Time = "Time";

                public const string Audio = "Audio";

                public const string AudioSource = "AudioSource";
            }

            public static class Weapons
            {
                public static class Vulcan
                {
                    public const string Projectile = "vulcan_projectile_example";

                    public const string Muzzle = "vulcan_muzzle_example";

                    public const string Impact = "vulcan_impact_example";
                }

                public static class SoloGun
                {
                    public const string Projectile = "solo_gun_flare_trail_example";

                    public const string Muzzle = "solo_gun_muzzle_example";

                    public const string Impact = "solo_gun_flames_01_example";
                }
            }
        }
    }
}
