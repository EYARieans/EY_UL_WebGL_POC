using EY.Managers.Collider;
using EY.Managers.Sound;
using EY.Model.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

namespace EY.Managers.Game
{
    public class LevelManager : MonoBehaviour, IDisposable
    {
        public Level ActiveLevel { get; set; }
        [SerializeField] private ColliderManager colliderManager;
        [SerializeField] private GameObject Prompt_GrabGun;
        [SerializeField] private GameObject Prompt_Move;
        [SerializeField] private SoundManager soundManager;
        [SerializeField] private PlayableAsset ScientisTimeline;
        [SerializeField] private PlayableDirector PlayableDirector;
        [SerializeField] private XRSimpleInteractable grababaleGunInteractable;
        [SerializeField] private XRSimpleInteractable timeTravellerInteractable;
        [SerializeField] private XRSimpleInteractable flagTargetInteractable;
        [SerializeField] private GameObject entry_Btn;
        [SerializeField] private GameObject arrowTowardsTerrace;
        [SerializeField] private GameObject ControllerGrabbleGun;
        private const string PromptClip = "PromptNotification";
        private const string WelcomeClip = "Welcome";
        private bool IsLabSessionDone = false;
        public bool IsGrappleGunCollected { get; set; } = false;
        public bool IsTimeTravellerGunCollected { get; set; } = false;

        // Start is called before the first frame update
        void Start()
        {
            colliderManager.OnMainGateEntry += OnMainGateEnter;
            colliderManager.OnLaboratoryEntry += OnLaboratoryEnter;
            colliderManager.OnTerraceEntry += OnTerraceEnter;

            LevelInitialization();
        }

        public void LevelInitialization()
        {
            StartCoroutine(soundManager.PlayClip(PromptClip, callBack: () =>
            {
                StartCoroutine(soundManager.PlayClip(WelcomeClip, callBack: () =>
                {
                    if (!entry_Btn.activeInHierarchy) entry_Btn.SetActive(true);
                }));
            }));
        }

        public void OnTerraceEnter()
        {
            Debug.Log("Terrace entry");
        }

        public void OnLaboratoryEnter()
        {
            if (IsLabSessionDone) return;

            IsLabSessionDone= true;
            StartCoroutine(PlayAsset(ScientisTimeline, callBack: () =>
            {
                if(!Prompt_GrabGun.activeInHierarchy) Prompt_GrabGun.SetActive(true);
                grababaleGunInteractable.firstSelectEntered.AddListener(OnGrabableGunGrabbed);
                timeTravellerInteractable.firstSelectEntered.AddListener(OnTimeTravellerGrabbed);
                flagTargetInteractable.firstSelectEntered.AddListener(OnFlagGrabbed);
            }));
        }

        /// <summary>
        /// When flag grabbed
        /// </summary>
        /// <param name="arg0"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void OnFlagGrabbed(SelectEnterEventArgs arg0)
        {
            ChangeLevel(Level.Level2);
        }

        /// <summary>
        /// When grabbable gun grabbed
        /// </summary>
        /// <param name="arg0"></param>
        private void OnGrabableGunGrabbed(SelectEnterEventArgs arg0)
        {
            grababaleGunInteractable.transform.parent.gameObject.SetActive(false);
            if (!ControllerGrabbleGun.activeInHierarchy) ControllerGrabbleGun.SetActive(true);
            IsGrappleGunCollected = true;
        }

        /// <summary>
        /// When time traveller grabbed
        /// </summary>
        /// <param name="arg0"></param>
        private void OnTimeTravellerGrabbed(SelectEnterEventArgs arg0)
        {
            timeTravellerInteractable.transform.parent.gameObject.SetActive(false);
            IsTimeTravellerGunCollected = true;
        }

        public void OnMainGateEnter()
        {
            Debug.Log("Main Gate entry");
        }

        public void ChangeLevel(Level changeTo)
        {
            if (ActiveLevel == changeTo) return;

            ActiveLevel = changeTo;

            SceneManager.LoadScene(changeTo.ToString(), LoadSceneMode.Single);
        }

        public void Dispose()
        {
            colliderManager.OnMainGateEntry -= OnMainGateEnter;
            colliderManager.OnLaboratoryEntry -= OnLaboratoryEnter;
            colliderManager.OnTerraceEntry -= OnTerraceEnter;

            grababaleGunInteractable.firstSelectEntered.RemoveAllListeners();
            timeTravellerInteractable.firstSelectEntered.RemoveAllListeners();
            flagTargetInteractable.firstSelectEntered.RemoveAllListeners();
        }

        public IEnumerator PlayAsset(PlayableAsset asset, Action callBack = null)
        {
            PlayableDirector.playableAsset = asset;
            PlayableDirector.Play();
            yield return new WaitWhile(() => PlayableDirector.state == PlayState.Playing);
            callBack?.Invoke();
        }

        private void Update()
        {
            if (IsGrappleGunCollected && IsTimeTravellerGunCollected)
            {
                if (!arrowTowardsTerrace.activeInHierarchy) arrowTowardsTerrace.SetActive(true);
                if (Prompt_GrabGun.activeInHierarchy) Prompt_GrabGun.SetActive(false);
                if (!Prompt_Move.activeInHierarchy) Prompt_Move.SetActive(true);
            }
        }
    }

}
