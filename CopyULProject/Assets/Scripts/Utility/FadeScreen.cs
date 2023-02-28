using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EY.Utility.Fader
{
    public class FadeScreen : MonoBehaviour
    {
        public bool fadeOnStart = true;
        public float fadeTime = 3f;
        public Color fadeColor = Color.white;
        private Renderer rend;

        // Start is called before the first frame update
        void Start()
        {
            rend = GetComponent<Renderer>();
            if(fadeOnStart) FadeIn();
        }

        public void FadeIn()
        {
            Fade(1, 0);
        }

        public void FadeOut()
        {
            Fade(0, 1);
        }

        public void Fade(float alphaIn, float alphaOut)
        {
            StartCoroutine(FadeRoutine(alphaIn, alphaOut));
        }

        public IEnumerator FadeRoutine(float alphaIn, float alphaOut)
        {
            float timer = 0;
            while (timer < fadeTime)
            {
                var color = fadeColor;
                color.a = Mathf.Lerp(alphaIn, alphaOut, timer/fadeTime);
                rend.material.SetColor("_BaseColor", color);

                timer +=Time.deltaTime;
                yield return null;
            }

            var color2 = fadeColor;
            color2.a = alphaOut;
            rend.material.SetColor("_Color", color2);
            yield return new WaitForSeconds(fadeTime);
        }
    }

}
