using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Video;
using UnityEngine.Playables;


public class Ui : MonoBehaviour
{
  
    public AudioSource aud;
    public Animator q_anim=null;
    public GameObject audio_2; //for prompt after tt use
    // public GameObject arrow;
    public GameObject Q_prompt;
    public GameObject Q_prompt_1;
    bool m_ToggleChange = true;
    [SerializeField] private Animator future_btn = null;
    //public GameObject panel;
    public VideoPlayer video;//tt video
    public GameObject panel_for_tt;
    public GameObject replay_btn;
    public GameObject tt_object;
    public GameObject tt_object_1;
    public GameObject panel_prompt;// after tt effect q's prompt for gg use
    public GameObject sphere;
    public GameObject characters;
    public GameObject spy_off_prompt;
    public GameObject tt_btn;//after the audio stop then Q's prompt ,tt btn,tt prompt should appear in scene 2
    public Animator tt_btn_anim=null;//after the audio stop then Q's prompt ,tt btn,tt prompt should appear in scene 2
    public GameObject tt_selected;
    public GameObject Canvas_1;
    public GameObject click_btn_click_tt_prompt; //after pressing tt it should be unable
    public Animator Spy_toggle_on_anim=null;
    public GameObject panel;
    public VideoPlayer video_1;//video from 2 to 3
    public GameObject arrow;
    public PlayableDirector director;
    public GameObject Director;
    public GameObject screen_prompt;
    public Animator Spy_toggle_off_anim = null;
    public AudioSource audio;//audio of Q's look like they are congregatting in different room
    public GameObject calendar_nov;
    public GameObject calendar_march;
    public GameObject spy_normal;
   




    [SerializeField] private UnityEvent Event;

    void Start()
    {
        video.url = System.IO.Path.Combine(Application.streamingAssetsPath, "New_Time_Travel_Effect.mp4");
        video_1.url= System.IO.Path.Combine(Application.streamingAssetsPath, "Grapple_gun_teleport_effect_scene_02_to_03_v07.mp4");
        //VideoPlayer.loopPointReached += Reload;
        director.played += Director_Played;
        director.stopped += Director_Stopped;
        StartCoroutine(Screen_prompt());
       


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
                if (hit.collider.tag == "future_btn")//when future btn is clicked
                {
                    future_btn.Play("ButtonFuture", 0, 0.0f);
                    panel_for_tt.SetActive(true);
                    video.Play();
                    // sphere.SetActive(true);
                    Canvas_1.SetActive(false);
                    arrow.SetActive(false);
                    tt_object.SetActive(false);
                    tt_object_1.SetActive(false);
                    characters.SetActive(false);
                    Director.SetActive(false);
                    
                    calendar_nov.SetActive(false);
                   // Level();
                   // panel.SetActive(true);
                    //video.Play();

                }

            }

        }

        
        if ((video.frame) > 0 && (video.isPlaying == false)) //time traveller video when get over
        {
            //panel.SetActive(false);
            Canvas_1.SetActive(true);
            //panel_prompt.SetActive(true);
            //video.gameObject.SetActive(false);
            panel_for_tt.SetActive(false);
            
            //sphere.SetActive(false);
            tt_btn.SetActive(true);
            tt_selected.SetActive(false);
            click_btn_click_tt_prompt.SetActive(false);
            Q_prompt_1.SetActive(true);
            audio_2.SetActive(true);
            q_anim.Play("Q", 0, 0.0f);
            calendar_march.SetActive(true);
            Invoke("qprompt_appear_1", audio.clip.length);

            Event.Invoke();
        }
        /*if(audio.time != 0 && !audio.isPlaying)
        {
            
        }*/
       /* if (aud.time != 0 && !aud.isPlaying)
        {
            // q_anim.enabled = false;
            //replay_btn.SetActive(false);
           // Q_prompt.SetActive(false);
           // q_anim.Play("New State", 0, 0.0f);
        }*/

       // Invoke("arrow_appear", aud.clip.length);

    }
    public void qprompt_appear_1()
    {
        Q_prompt_1.SetActive(false);
        q_anim.Play("New State", 0, 0.0f);
        spy_off_prompt.SetActive(true);
        
        if(spy_off_prompt.activeSelf== true)
        {
            spy_normal.SetActive(false);
            Spy_toggle_on_anim.gameObject.SetActive(true);
            Spy_toggle_on_anim.Play("spy_toogle", 0, 0.0f);

        }
    }
    public void qprompt_appear()
    {
        Q_prompt.SetActive(false);
        q_anim.Play("New State", 0, 0.0f);
    }
    IEnumerator Screen_prompt() //waiting for grapple gun to end its animation
    {
        yield return new WaitForSeconds(1.5f);
        screen_prompt.SetActive(true);
        if(screen_prompt.active== true)
        {
            Spy_toggle_off_anim.gameObject.SetActive(true);
            Spy_toggle_off_anim.Play("Spyoff_effect", 0, 0.0f);
        }
        
    }
    private void Director_Stopped(PlayableDirector obj)
    {
        Q_prompt.SetActive(true);
        q_anim.Play("Q", 0, 0.0f);
        tt_btn_anim.gameObject.SetActive(true);
        tt_btn_anim.Play("Time_travel_controller", 0, 0.0f);
        tt_btn.SetActive(false);
        replay_btn.SetActive(true);
        Invoke("qprompt_appear", aud.clip.length);

      
    }
    private void Director_Played(PlayableDirector obj)
    {
       
    }
    
   
   
   /* public void Audio_appear()
    { 
        if (m_ToggleChange == true)//to play the audio only once
        {
            // myAudioEvent.Invoke();
            m_ToggleChange = false;
            Invoke("arrow_appear", aud.clip.length); //Invoke is called to display the text msg after the audio clip in length i.e in sec     

        }
        
    }*/
   /* public void arrow_appear()
    {
        
      //  arrow.SetActive(true);
       
    }*/
   /* IEnumerator Replay_btn_activate()
    {
        yield return new WaitForSeconds(31f);
        replay_btn.SetActive(true);
    }
    public void REPLAY()
    {
        StartCoroutine(Replay_btn_activate());
        //SceneManager.LoadScene(1);
    }*/
    IEnumerator Video_Play_1() //waiting for grapple gun to end its animation from 2 to 3
    {
        yield return new WaitForSeconds(0.6f);
        panel.SetActive(true);
        video_1.Play();
    }
    public void Level_1()//it is used for going playing video for 2 to 3rd scene
    {
        StartCoroutine(Video_Play_1());
        //SceneManager.LoadScene(1);
    }

}
