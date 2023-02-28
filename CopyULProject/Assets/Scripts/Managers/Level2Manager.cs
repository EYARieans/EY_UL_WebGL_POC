using EY.Managers.Notification;
using EY.Managers.Sound;
using EY.Model.Enums;
using EY.Utility.Fader;
using System;
using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.XR.Interaction.Toolkit;

namespace EY.Managers.Levels
{
    public class Level2Manager : MonoBehaviour
    {
        private const string PromptClip = "BoardRoomMeeting";
        private const string WelcomeClip = "Welcome";
        [SerializeField] private PlayableAsset BoardMeetingTimeline;
        [SerializeField] private PlayableAsset ProcurementTimeLine;
        [SerializeField] private PlayableAsset PilotICAATimeLine;
        [SerializeField] private PlayableAsset BoardMeeting_EndTimeline;
        [SerializeField] private PlayableDirector PlayableDirector;
        [SerializeField] private GameObject TimeTraveller;
        [SerializeField] private GameObject GrappelGun;
        [SerializeField] private ActionBasedController LeftController;
        [SerializeField] private ActionBasedController RightController;
        [SerializeField] private GameObject XRCamera;
        [SerializeField] private GameObject TimeMenu;
        [SerializeField] private GameObject TimeMenu1;
        [SerializeField] private GameObject TimeMenu2;
        [SerializeField] private GameObject Characters;
        [SerializeField] private GameObject Characters1;
        [SerializeField] private GameObject SpyMode_Prompt;
        [SerializeField] private Transform SpyPos1;
        [SerializeField] private Transform SpyPos2;
        [SerializeField] private Transform SpyPos3;
        [SerializeField] private Transform Observer1;
        [SerializeField] private Transform Observer2;
        [SerializeField] private Transform Observer3;

        [SerializeField] private Transform Level1_MarchCal;
        [SerializeField] private Transform Level1_NavCal;

        [SerializeField] private Transform Level2_MarchCal;
        [SerializeField] private Transform Level2_JuneCal;

        [SerializeField] private Transform Level3_JuneCal;
        [SerializeField] private Transform Level3_AugCal;
        [SerializeField] private FadeScreen fadeScreen;
        [SerializeField] private VideoPlayer VideoPlayer_Scene3;
        [SerializeField] private GameObject notebookArrows;
        [SerializeField] private GameObject notebookArrowsLevel2;
        [SerializeField] private XRSimpleInteractable notebookInteractable;
        [SerializeField] private GameObject ITAnalystBook;
        [SerializeField] private GameObject ITInstructions;
        [SerializeField] private AudioSource audioSource;
        private Action AudioSourceStops { get; set; }


        // Start is called before the first frame update
        void Start()
        {
            //VideoPlayer1.url = $"{Application.streamingAssetsPath}/Teams_Call_01_v01.mp4";
            //VideoPlayer2.url = $"{ Application.streamingAssetsPath}/Teams_Call_02_v01.mp4";
            //VideoPlayer_Scene3.url = $"{ Application.streamingAssetsPath}/Teams_Call_01_v01.mp4";
            // NotificationManager.Instance.ShowPromptMessage("UseSpyMode");
            // Invoke(nameof(LevelInitialization), 10f);
            // LevelInitialization();
        }

        public void LevelInitialization()
        {
            StartCoroutine(PlayAsset(BoardMeetingTimeline, callBack: () =>
            {
                NotificationManager.Instance.ShowPromptMessage("TimeToUse");
                StartCoroutine(SoundManager.Instance.PlayClip("TimeToUse"));
                if(!TimeMenu.activeInHierarchy) TimeMenu.SetActive(true);
            }));
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


        public IEnumerator PlayAsset(PlayableAsset asset, Action callBack = null)
        {
            PlayableDirector.playableAsset = asset;
            PlayableDirector.Play();
            yield return new WaitWhile(() => PlayableDirector.state == PlayState.Playing);
            callBack?.Invoke();
        }

        public void SpyModeOn1(GameObject spybtn)
        {
            var xrpos = XRCamera.transform.position;
            xrpos.z = -4.77f;
            XRCamera.transform.position = xrpos;
            spybtn.SetActive(false);
            LevelInitialization();
        }

        public void SpyModeOn2(GameObject spybtn)
        {
            GrappelGun.SetActive(false);
            XRCamera.transform.position = Observer2.position;
            spybtn.SetActive(false);
            StartCoroutine(PlayAsset(ProcurementTimeLine, callBack: () =>
            {
                NotificationManager.Instance.ShowPromptMessage("WhatAreYou");
                StartCoroutine(SoundManager.Instance.PlayClip("WhatAreYou"));
                if (!TimeMenu1.activeInHierarchy) TimeMenu1.SetActive(true);
            }));
        }

        public void SpyModeOn3(GameObject spybtn)
        {
            GrappelGun.SetActive(false);
            XRCamera.transform.position = Observer3.position;
            spybtn.SetActive(false);
            StartCoroutine(PlayAsset(PilotICAATimeLine, callBack: () =>
            {
                GrabNoteBook();
            }));
        }

        public void EmptyRoom()
        {
            fadeScreen.FadeOut();
            fadeScreen.FadeIn();
            TimeMenu.SetActive(false);
            Characters.SetActive(false);
            Level1_NavCal.gameObject.SetActive(false);
            Level1_MarchCal.gameObject.SetActive(true);
            NotificationManager.Instance.ShowPromptMessage("LooksLike", 7f);
            StartCoroutine(SoundManager.Instance.PlayClip("LooksLike", callBack: ()=>
            {
                SpyMode_Prompt.SetActive(false);
                var xrpos = XRCamera.transform.position;
                xrpos.z = -6.1f;
                XRCamera.transform.position = xrpos;
                GrappelGun.SetActive(true);
            }));
        }

        public void EmptyRoom1()
        {
            fadeScreen.FadeOut();
            fadeScreen.FadeIn();
            TimeMenu1.SetActive(false);
            Characters1.SetActive(false);
            Level2_MarchCal.gameObject.SetActive(false);
            Level2_JuneCal.gameObject.SetActive(true);
            NotificationManager.Instance.ShowPromptMessage("LooksLike", 7f);

            StartCoroutine(SoundManager.Instance.PlayClip("LooksLike", callBack: () =>
            {
                SpyMode_Prompt.SetActive(false);
                var xrpos = XRCamera.transform.position;
                xrpos.z = -6.1f;
                XRCamera.transform.position = xrpos;
                GrappelGun.SetActive(true);
            }));
        }

        public void Scene_BackToNov()
        {
            fadeScreen.FadeOut();
            Observer1Position();
            fadeScreen.FadeIn();
            TimeMenu2.SetActive(false);
            ITInstructions.SetActive(true);
            Level1_NavCal.gameObject.SetActive(true);
            Characters.SetActive(true);
            notebookArrowsLevel2.SetActive(true);
            ITAnalystBook.GetComponent<XRSimpleInteractable>().enabled = true;
        }

        public void BoardMeetingEnd()
        {
            ITInstructions.SetActive(false);
            StartCoroutine(PlayAsset(BoardMeeting_EndTimeline, callBack: () =>
            {
                NotificationManager.Instance.ShowPromptMessage("WellDone", 15f);
                StartCoroutine(SoundManager.Instance.PlayClip("WellDone", callBack: ()=>
                {
                    fadeScreen.FadeOut();
                    StartCoroutine(GoToSceneAsyncRoutine(Level.Level3));
                }));
            }));
        }

        public void Observer1Position()
        {
            XRCamera.transform.position = Observer1.position;
        }

        public void Hotspot2()
        {
            XRCamera.transform.position = SpyPos2.position;
        }

        public void Hotspot3()
        {
            XRCamera.transform.position = SpyPos3.position;
        }

        public void PlayVideo(string path)
        {
            VideoClip clip = Resources.Load(path, typeof(VideoClip)) as VideoClip;
            VideoPlayer_Scene3.clip = clip;
            VideoPlayer_Scene3.Play();
        }

        public void GrabNoteBook()
        {
            NotificationManager.Instance.ShowPromptMessage("Quick", 4f);
            StartCoroutine(SoundManager.Instance.PlayClip("Quick", callBack: () =>
            {
                if (!notebookArrows.activeInHierarchy) notebookArrows.SetActive(true);
                notebookInteractable.enabled = true;
            }));
           
        }

        public void BackToNov()
        {
            if (notebookArrows.activeInHierarchy) notebookArrows.SetActive(false);
            if (notebookInteractable.gameObject.activeInHierarchy) notebookInteractable.gameObject.SetActive(false);
            NotificationManager.Instance.ShowPromptMessage("BackToNov", 4f);
            StartCoroutine(SoundManager.Instance.PlayClip("BackToNov", callBack: () =>
            {
                TimeMenu2.SetActive(true);
            }));
        }
    }

}
