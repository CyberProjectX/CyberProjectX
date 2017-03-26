using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Client.UI
{
    public class TextPopUpController : MonoBehaviour
    {
        private Text popText;

        public  float popTextSpeed = 50f;
        public bool fadeOut = true;
        public float fadeSpeed = 3.3f;
        public float offWaitTime = 1f;

        public void Start()
        {
            popText = GetComponent<Text>();
        }

        public void Update()
        {
            transform.position += new Vector3(0f, popTextSpeed * 0.01f);

            if (offWaitTime > 0)
            {
                offWaitTime -= Time.deltaTime;
            }
            else
            {
                if (fadeOut)
                {
                    popText.color -= new Color(0f, 0f, 0f, fadeSpeed * 0.01f);
                    if (popText.color.a <= 0)
                    {
                        Destroy(gameObject);
                    }
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
