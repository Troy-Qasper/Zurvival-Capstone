using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    public class BlackOut : MonoBehaviour
    {
        public Image fadeOut;
        public float fadeDur = 7.0f;

        public void StartFade()
        {
            StartCoroutine(FadeOut());
        }

        private IEnumerator FadeOut()
        {
            float timer = 0f;
            Color startColor = fadeOut.color;
            Color endColor = new Color(0f, 0f, 0f, 1f);

            while (timer < fadeDur)
            {
                fadeOut.color = Color.Lerp(startColor, endColor, timer / fadeDur);
                timer += Time.deltaTime;
                yield return null;
            }

            fadeOut.color = endColor;
        }
    }
