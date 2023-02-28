using EY.Managers.Notification;
using EY.Managers.Sound;
using EY.Utility.Fader;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace EY.Managers.Levels
{
    public class Level3Manager : MonoBehaviour
    {
        [SerializeField] private FadeScreen fadeScreen;
        [SerializeField] private PlayableAsset ScientisTimeline;
        [SerializeField] private PlayableDirector PlayableDirector;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(PlayAsset(ScientisTimeline, callBack: () =>
            {
                NotificationManager.Instance.ShowPromptMessage("ThankYou");
            }));
        }

        public IEnumerator PlayAsset(PlayableAsset asset, Action callBack = null)
        {
            PlayableDirector.playableAsset = asset;
            PlayableDirector.Play();
            yield return new WaitWhile(() => PlayableDirector.state == PlayState.Playing);
            callBack?.Invoke();
        }
    }

}
