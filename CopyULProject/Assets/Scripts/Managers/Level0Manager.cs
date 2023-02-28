using EY.Model.Enums;
using EY.Utility.Fader;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

namespace EY.Managers.Levels
{
    public class Level0Manager : MonoBehaviour
    {
        public Level ActiveLevel { get; set; }
        [SerializeField] private string VideoFileName = "Splash_screen";
        [SerializeField] private FadeScreen fadeScreen;
        [SerializeField] private VideoPlayer videoPlayer;

        private void Awake()
        {
            PlayVideo(VideoFileName);
            videoPlayer.loopPointReached += OnMovieFinished;

        }

        private void OnMovieFinished(VideoPlayer source)
        {
            ChangeLevel(Level.Level1);
        }

        public void ChangeLevel(Level changeTo)
        {
            if (ActiveLevel == changeTo) return;

            ActiveLevel = changeTo;
            StartCoroutine(GoToSceneAsyncRoutine(changeTo));

        }

        private void PlayVideo(string path)
        {
            VideoClip clip = Resources.Load(path, typeof(VideoClip)) as VideoClip;
            videoPlayer.clip = clip;
            videoPlayer.Play();
        }

        private IEnumerator GoToSceneAsyncRoutine(Level level)
        {
            fadeScreen.FadeOut();
            var operation = SceneManager.LoadSceneAsync(level.ToString());

            float timer = 0;
            while (timer <= fadeScreen.fadeTime && !operation.isDone)
            {
                timer += Time.deltaTime;
                yield return null;
            }

            operation.allowSceneActivation = true;
        }
    }
}

