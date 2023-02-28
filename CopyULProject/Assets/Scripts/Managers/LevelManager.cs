using EY.Managers.Collider;
using EY.Managers.Notification;
using EY.Managers.Sound;
using EY.Model.Enums;
using EY.Utility.Fader;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

namespace EY.Managers.Levels
{
    public class LevelManager : MonoBehaviour, IDisposable
    {
        public Level ActiveLevel { get; set; }
        [SerializeField] private ColliderManager colliderManager;
        [SerializeField] private GameObject Prompt_User;
        [SerializeField] private GameObject Prompt_GrabGun;
        [SerializeField] private GameObject Prompt_Move;
        [SerializeField] private SoundManager soundManager;
        [SerializeField] private PlayableAsset ScientisTimeline;
        [SerializeField] private PlayableDirector PlayableDirector;
        [SerializeField] private TeleportationArea terraceArea;
        [SerializeField] private XRSimpleInteractable grababaleGunInteractable;
        [SerializeField] private XRSimpleInteractable timeTravellerInteractable;
        [SerializeField] private XRSimpleInteractable flagTargetInteractable;
        [SerializeField] private GameObject entry_Btn;
        [SerializeField] private GameObject arrowTowardsTerrace;
        [SerializeField] private GameObject ControllerGrabbleGun;
        [SerializeField] private Animator entryDoorAnim;
        [SerializeField] private FadeScreen fadeScreen;
        private const string PromptClip = "PromptNotification";
        private const string WelcomeClip = "Welcome";
        private bool IsLabSessionDone = false;
        private bool IsGunGrabbedDone = false;
        public bool IsGrappleGunCollected { get; set; } = false;
        public bool IsTimeTravellerGunCollected { get; set; } = false;

        // Start is called before the first frame update
        void Start()
        {
            //    colliderManager.OnMainGateEntry += OnMainGateEnter;
            //    colliderManager.OnLaboratoryEntry += OnLaboratoryEnter;
            //    colliderManager.OnTerraceEntry += OnTerraceEnter;
            entry_Btn.GetComponent<Button>().onClick.AddListener(() => {
                entryDoorAnim.Play("door_open");
                StartCoroutine(soundManager.PlayClip("Door"));
                entry_Btn.transform.parent.gameObject.SetActive(false);
            });
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
                NotificationManager.Instance.ShowPromptMessage("Move");
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
            StartCoroutine(GoToSceneAsyncRoutine(changeTo));
            
        }

        private IEnumerator GoToSceneAsyncRoutine(Level level)
        {
            fadeScreen.FadeOut();
            var operation = SceneManager.LoadSceneAsync(level.ToString());

            float timer = 0;
            while(timer <= fadeScreen.fadeTime && !operation.isDone)
            {
                timer +=Time.deltaTime;
                yield return null;
            }

            operation.allowSceneActivation = true;
        }

        public void Dispose()
        {
            //colliderManager.OnMainGateEntry -= OnMainGateEnter;
            //colliderManager.OnLaboratoryEntry -= OnLaboratoryEnter;
            //colliderManager.OnTerraceEntry -= OnTerraceEnter;

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

        public void PlaySound(string clip)
        {
            StartCoroutine(soundManager.PlayClip(clip));
        }

        private void Update()
        {
            if (IsGrappleGunCollected && IsTimeTravellerGunCollected)
            {
                if (IsGunGrabbedDone) return;

                IsGunGrabbedDone = true;
                arrowTowardsTerrace.SetActive(true);
                terraceArea.enabled = true;
                NotificationManager.Instance.ShowPromptMessage("HighlightedArea", 8f);
            }
        }
    }

}
