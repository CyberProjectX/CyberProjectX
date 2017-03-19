using UnityEngine;

namespace Scripts.Client.UI
{
    public enum PresetType
    {
        None,
        ShotgunPreset,
        CrysisPreset
    }

    public class CrosshairController : MonoBehaviour
    {
        public PresetType CrosshairPreset = PresetType.None;

        public bool ShowCrosshair = true;
        public Texture2D VerticalTexture;
        public Texture2D HorizontalTexture;

        //Size of boxes
        public float Length = 5f;
        public float Width = 2f;

        //Spreed setup
        public float MinSpread = 6.0f;
        public float MaxSpread = 50.0f;
        public float IcreaseSpreadPerSecond = 150.0f;
        public float DecreaseSpreadPerSecond = 300.0f;

        //Rotation
        public float RotAngle = 45.0f;
        public float RotSpeed = 0.0f;

        public Vector3 Position;
        
        private float spread;

        public void IncreaseSpread()
        {
            spread += IcreaseSpreadPerSecond * Time.deltaTime;
            spread = Mathf.Clamp(spread, MinSpread, MaxSpread);
        }

        public void DecreaseSpread()
        {
            spread -= DecreaseSpreadPerSecond * Time.deltaTime;
            spread = Mathf.Clamp(spread, MinSpread, MaxSpread);
        }

        public void Start()
        {
            CrosshairPreset = PresetType.None;
            Position = new Vector3(Screen.width / 2f, Screen.height / 2f);
        }

        public void Update()
        {

            //Rotation
            RotAngle += RotSpeed * Time.deltaTime;

        }

        public void OnGUI()
        {
            if (ShowCrosshair && VerticalTexture && HorizontalTexture)
            {
                GUIStyle verticalT = new GUIStyle();
                GUIStyle horizontalT = new GUIStyle();
                verticalT.normal.background = VerticalTexture;
                horizontalT.normal.background = HorizontalTexture;
                spread = Mathf.Clamp(spread, MinSpread, MaxSpread);
                Vector2 pivot = new Vector2(Position.x, Position.y);

                if (CrosshairPreset == PresetType.CrysisPreset)
                {
                    GUI.Box(new Rect(Position.x - 1f, Position.y - spread / 2 - 14, 2, 14), (Texture2D)null, horizontalT);
                    GUIUtility.RotateAroundPivot(45, pivot);
                    GUI.Box(new Rect(Position.x + spread / 2, Position.y - 1f, 14, 2), (Texture2D)null, verticalT);
                    GUIUtility.RotateAroundPivot(0, pivot);
                    GUI.Box(new Rect(Position.x - 1f, Position.y + spread / 2, 2, 14), (Texture2D)null, horizontalT);
                }
                if (CrosshairPreset == PresetType.ShotgunPreset)
                {
                    GUIUtility.RotateAroundPivot(45, pivot);
                    //Horizontal
                    GUI.Box(new Rect(Position.x - 7f, Position.y - spread / 2 - 3, 14, 3), (Texture2D)null, horizontalT);
                    GUI.Box(new Rect(Position.x - 7f, Position.y + spread / 2, 14, 3), (Texture2D)null, horizontalT);
                    //Vertical
                    GUI.Box(new Rect(Position.x - spread / 2 - 3, Position.y - 7f, 3, 14), (Texture2D)null, verticalT);
                    GUI.Box(new Rect(Position.x + spread / 2, Position.y - 7f, 3, 14), (Texture2D)null, verticalT);
                }

                if (CrosshairPreset == PresetType.None)
                {
                    GUIUtility.RotateAroundPivot(RotAngle % 360, pivot);
                    //Horizontals
                    GUI.Box(new Rect(Position.x - Width / 2, Position.y - spread / 2 - Length, Width, Length), (Texture2D)null, horizontalT);
                    GUI.Box(new Rect(Position.x - Width / 2, Position.y + spread / 2, Width, Length), (Texture2D)null, horizontalT);
                    //Vertical
                    GUI.Box(new Rect(Position.x - spread / 2 - Length, Position.y - Width / 2, Length, Width), (Texture2D)null, verticalT);
                    GUI.Box(new Rect(Position.x + spread / 2, Position.y - Width / 2, Length, Width), (Texture2D)null, verticalT);
                }
            }
        }

    }
}
