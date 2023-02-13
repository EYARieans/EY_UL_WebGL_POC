using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;


public class UI_Manager_4 : MonoBehaviour
{
    public VideoPlayer video;//intial video
    public VideoPlayer video_teams;//intial video

    public GameObject panel;//first video on plane
    public GameObject panel1;//2nd video on  this plane
    public GameObject Q_prompt;
    public GameObject replay_btn;
    public AudioSource aud_q;
    public GameObject arrow;
    public GameObject notebook;
    public GameObject notebook_btn;
    public GameObject notebook_btn_lock;
    public GameObject spy_Btn;
    public GameObject smoke;
    [SerializeField] private Animator spy_mode = null;
    [SerializeField] private Animator past_btn=null;//to press the animation
    public GameObject sphere;//effect to show for tt in spheere
    public GameObject timetravaller;
    public GameObject canvas_4;
    public VideoPlayer video_2;
    public PlayableDirector director;
    public GameObject screen_prompt_1;
    public Animator Spy_toggle_off_anim = null;
    public GameObject spyoff_prompt;
    public AudioSource aud_q1;
    public GameObject Q_prompt1;
    [SerializeField] private Animator btn_qanim = null;
    public GameObject panel_effect;



    // Start is called before the first frame update
    void Start()
    {
        video_2.url = System.IO.Path.Combine(Application.streamingAssetsPath, "360_video_Teleport_effect.mp4");
        video.url= System.IO.Path.Combine(Application.streamingAssetsPath, "Teams_Call_01_v01.mp4");
        video_teams.url= System.IO.Path.Combine(Application.streamingAssetsPath, "Teams_Call_02_v01.mp4");
        //director = GetComponent<PlayableDirector>();
        director.played += Director_Played;
            director.stopped += Director_Stopped;
    }

    // Update is called once per frame
    void Update()
    {
        if ((video.frame) > 0 && (video.isPlaying == false))
        {
            //panel.SetActive(false);
            // panel1.SetActive(true);
            //video1.Play();
           
            
        }
       
       /* if ((video_2.frame) > 0 && (video_2.isPlaying == false))
        {
            video_2.gameObject.SetActive(false);
            panel_effect.SetActive(true);
        }*/
      /*  if (aud_q.time != 0 && !aud_q.isPlaying)//for 1st prompt of q
        {
            
           

        }*/
       /* if (aud_q1.time != 0 && !aud_q1.isPlaying)//for 2nd promt of q
        {
            
            

        }*/
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())//Using Raycast to click on the object i.e teleportation lift
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.collider.tag == "Notebook")
                {

                    notebook.SetActive(false);
                    notebook_btn_lock.SetActive(false);
                    notebook_btn.SetActive(true);
                    arrow.SetActive(false);
                    spy_Btn.SetActive(false);
                    spyoff_prompt.SetActive(true);
                    spy_mode.gameObject.SetActive(true);
                    spy_mode.Play("spy_toogle", 0, 0.0f);
                    smoke.SetActive(true);
                   // replay_btn.SetActive(false);

                }
                else if (hit.collider.tag == "past_btn")
                {
                    past_btn.Play("ButtonPast", 0, 0.0f);
                    sphere.SetActive(true);
                    timetravaller.SetActive(false);
                    canvas_4.SetActive(false);
                  //  Level();
                    // panel.SetActive(true);
                    //video.Play();

                }

            }

        }
       if ((video_2.frame) > 0 && (video_2.isPlaying == false))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            video_2.gameObject.SetActive(false);
        }

    }
    public void Screen_display_timer()
    {
        StartCoroutine(Screen_prompt());
    }
    IEnumerator Screen_prompt() //waiting for grapple gun to end its animation
    {
        yield return new WaitForSeconds(5f);
        screen_prompt_1.SetActive(true);
        if (screen_prompt_1.active == true)
        {
            Spy_toggle_off_anim.Play("Spyoff_effect", 0, 0.0f);
        }
        
    }
    public void q_prompt_1()
    {
        Q_prompt1.SetActive(false);
        btn_qanim.Play("New State", 0, 0.0f);
        
    }
    public void sound_appear()
    {
        Invoke("q_prompt_1", aud_q1.clip.length);

    }
    public void q_prompt()
    {
        Q_prompt.SetActive(false);
        btn_qanim.Play("New State", 0, 0.0f);
    }
    private void Director_Stopped(PlayableDirector obj)
    {
      Q_prompt.SetActive(true);
       // replay_btn.SetActive(true);
        arrow.SetActive(true);
        btn_qanim.Play("Q", 0, 0.0f);
        Invoke("q_prompt", aud_q.clip.length);

    }
    private void Director_Played(PlayableDirector obj)
    {
      //  btn.SetActive(false);
    }

}
