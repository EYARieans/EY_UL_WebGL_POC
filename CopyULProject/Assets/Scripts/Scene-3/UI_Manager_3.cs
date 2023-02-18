using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.EventSystems;
using UnityEngine.Video;

public class UI_Manager_3 : MonoBehaviour
{
    public PlayableDirector director;
    public GameObject replay_btn;
    public GameObject screen_prompt;//it is when it will say turn off the spy mode after the conversation is over
    public GameObject spy_btn_normal;
    public Animator spy_anim = null;//animation of spy on btn
    [SerializeField] private Animator future_btn = null;
    public GameObject Panel_for_tt;//
    public VideoPlayer video;//When Video gets over of 360 degree
    public GameObject Canvas;
    public GameObject tt_object;//when future btn is clicked that should disappear
    public GameObject q_prompt_1;
    public GameObject q_prompt;
    public Animator q_anim = null;
    public Animator gg_anim=null ;
    public GameObject obj;
    public GameObject tt_btn_selected;
    public GameObject tt_btn;
    public GameObject panel;
    public VideoPlayer video_1;
    public Animator Spy_toggle_off_anim = null;
    public GameObject screen_prompt_1; 
     public Animator tt_anim_btn = null;
    public AudioSource audio_source;
    public GameObject calendarjun;
    public GameObject calendarmarch;
    public GameObject characters;
    public GameObject prompt;
    public AudioSource audio1;
    public GameObject gg_btn_normal;
    


    // Start is called before the first frame update
    void Start()
    {
        video.url = System.IO.Path.Combine(Application.streamingAssetsPath, "Time_Travel_02.mp4");
        video_1.url= System.IO.Path.Combine(Application.streamingAssetsPath, "Grapple_gun_teleport_effect_scene_03_to_04_v07.mp4");
        director.played += Director_Played;
        director.stopped += Director_Stopped;
      video.loopPointReached += OnMovieFinished;

    }

    // Update is called once per frame
    void Update()
    {
       /* if (audio_source.time != 0 && !audio_source.isPlaying)//when qprompt what are waiting for get over
        {
            
            

        }*/
        if (audio1.time != 0 && !audio1.isPlaying)
        {
            
        }

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())//Using Raycast to click on the object i.e teleportation lift
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.collider.tag == "future_btn")//when future btn is clicked
                {
                    future_btn.Play("ButtonFuture", 0, 0.0f);
                    //sphere.SetActive(true);
                    Panel_for_tt.SetActive(true);
                    video.Play();
                    Canvas.SetActive(false);
                   tt_object.SetActive(false);
                    q_prompt.SetActive(false);
                    characters.SetActive(false);
                    calendarjun.SetActive(true);
                    calendarmarch.SetActive(false);
                    replay_btn.SetActive(false);
                    prompt.SetActive(false);
                    StartCoroutine("WaitForVideoEnd");
                    // obj.SetActive(false);
                    //Canvas_1.SetActive(false);
                    //arrow.SetActive(false);
                    //Level();
                    // panel.SetActive(true);
                    //video.Play();

                }

            }
          /*if ((video.frame) > 0 && (video.isPlaying == false)) //time traveller video when get over
            {
                Canvas.SetActive(true);
                Panel_for_tt.SetActive(false);
                tt_anim_btn.gameObject.SetActive(false);
                tt_btn_selected.SetActive(false);
                tt_btn.SetActive(true);
               
                // video.gameObject.SetActive(false);
               
                screen_prompt.SetActive(true);
                spy_anim.gameObject.SetActive(true);
                spy_anim.Play("spy_toogle", 0, 0.0f);
                spy_btn_normal.SetActive(false);

                // q_prompt_1.SetActive(true);
                // q_anim.Play("Q", 0, 0.0f);
                // gg_anim.gameObject.SetActive(true);
                // gg_anim.Play("Button_gg_Icon", 0, 0.0f);
                // sphere.SetActive(false);
                // tt_btn.SetActive(true);
                // video.gameObject.SetActive(false);


            }*/
            

        }
        
    }
    public void OnMovieFinished(VideoPlayer player)
    {
        
        Canvas.SetActive(true);
        player.gameObject.SetActive(false);
        Panel_for_tt.SetActive(false);
        tt_anim_btn.gameObject.SetActive(false);
        tt_btn_selected.SetActive(false);
        tt_btn.SetActive(true);

        // video.gameObject.SetActive(false);

        screen_prompt.SetActive(true);
        spy_anim.gameObject.SetActive(true);
        spy_anim.Play("spy_toogle", 0, 0.0f);
        spy_btn_normal.SetActive(false);

        // q_prompt_1.SetActive(true);
        // q_anim.Play("Q", 0, 0.0f);
        // gg_anim.gameObject.SetActive(true);
        // gg_anim.Play("Button_gg_Icon", 0, 0.0f);
        // sphere.SetActive(false);
        // tt_btn.SetActive(true);
        // video.gameObject.SetActive(false);

    }





    public void qprompt_appear()//q's prompt for using tt
    {
        q_prompt.SetActive(false);
        q_anim.Play("New State", 0, 0.0f);
    }
    public void qprompt_appear_1()//for qs prompt grapple gun then they should be on one of the floor
    {
        q_anim.Play("New State", 0, 0.0f);
        gg_btn_normal.SetActive(false);
        gg_anim.gameObject.SetActive(true);
        gg_anim.Play("Button_gg_Icon", 0, 0.0f);
        q_prompt_1.SetActive(false);
    }
    public void start_audio()
    {
        Invoke("qprompt_appear_1", audio1.clip.length);
    }

    public void Screen_display_timer()
    {
        StartCoroutine(Screen_prompt());
    }
    IEnumerator Screen_prompt() //waiting for grapple gun to end its animation
    {
        yield return new WaitForSeconds(3f);
        screen_prompt_1.SetActive(true);
        if(screen_prompt_1.active== true)
        {
            Spy_toggle_off_anim.Play("Spyoff_effect", 0, 0.0f);
        }
        
    }
    
    private void Director_Stopped(PlayableDirector obj)
    {
        
        replay_btn.SetActive(true);
        q_prompt.SetActive(true);
        q_anim.Play("Q", 0, 0.0f);
        tt_btn.SetActive(false);
        tt_anim_btn.gameObject.SetActive(true);
        tt_anim_btn.Play("Time_travel_controller", 0, 0.0f);
        Invoke("qprompt_appear", audio_source.clip.length);

    }
    private void Director_Played(PlayableDirector obj)
    {
        //  btn.SetActive(false);
    }
    IEnumerator Video_Play_1() //waiting for grapple gun to end its animation forscene transition
    {
        yield return new WaitForSeconds(0.4f);
        panel.SetActive(true);
        video_1.Play();
    }
    public void Level_1()//it is used for going playing video for 3rd to 4th
    {
        StartCoroutine(Video_Play_1());
        //SceneManager.LoadScene(1);
    }
}
