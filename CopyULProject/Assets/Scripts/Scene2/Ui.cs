using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Video;


public class Ui : MonoBehaviour
{
    public AudioSource aud;
   // public GameObject arrow;
    public GameObject Q_prompt;
    bool m_ToggleChange = true;
    [SerializeField] private bool openTrigger = false;
    [SerializeField] private Animator future_btn = null;
    public GameObject panel;
    public VideoPlayer video;
    public GameObject replay_btn;
    public GameObject tt_object;
    public GameObject panel_prompt;// after tt effect q's prompt for gg use
    public GameObject gg_btn;// after tt effect q's prompt for gg use
    [SerializeField] private UnityEvent Event;


    void Start()
    {
        video.url = System.IO.Path.Combine(Application.streamingAssetsPath, "360_video_Teleport_effect.mp4");
        //VideoPlayer.loopPointReached += Reload;
        
    }
    void Update()
    {
        //obj_gg1.transform.rotation.y= FPS.transform.rotation.y;

        //audi1.PlayDelayed(10f);
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())//Using Raycast to click on the object i.e teleportation lift
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.collider.tag == "future_btn")
                {
                    future_btn.Play("ButtonFuture", 0, 0.0f);
                    Level();
                   // panel.SetActive(true);
                    //video.Play();

                }

            }

        }
        if ((video.frame) > 0 && (video.isPlaying == false))
        {
            panel.SetActive(false);
            panel_prompt.SetActive(true);
            gg_btn.SetActive(true);
            Event.Invoke();
        }



    }
    IEnumerator Video_Play()
    {
        yield return new WaitForSeconds(1f);
        panel.SetActive(true);
        video.Play();
        tt_object.SetActive(false);
        //StartCoroutine(Reload());
        
        //Invoke("Reload", (float)video.length);

        /* while(video.isPlaying)
         {
             yield return new WaitForEndOfFrame();
         }
         Reload();*/
        //StartCoroutine(Reload());
    }
    IEnumerator Reload()
    {
        while (video.isPlaying)
        {
            yield return null;
        }
        
       


    }
    public void Level()
    {
        StartCoroutine(Video_Play());
        //SceneManager.LoadScene(1);
    }
    public void Audio_appear()
    { 
        if (m_ToggleChange == true)//to play the audio only once
        {
            Q_prompt.SetActive(true);
            // myAudioEvent.Invoke();
            m_ToggleChange = false;
            Invoke("arrow_appear", aud.clip.length); //Invoke is called to display the text msg after the audio clip in length i.e in sec     

        }
    }
    public void arrow_appear()
    {
        
      //  arrow.SetActive(true);
       
    }
    IEnumerator Replay_btn_activate()
    {
        yield return new WaitForSeconds(30f);
        replay_btn.SetActive(true);
    }
    public void REPLAY()
    {
        StartCoroutine(Replay_btn_activate());
        //SceneManager.LoadScene(1);
    }

}
