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
    public GameObject Q_prompt;
    public GameObject replay_btn;
    public AudioSource aud_q;
    public GameObject arrow;
    public GameObject notebook;
    public GameObject notebook_duplicate;
    public GameObject notebook_btn;
    public GameObject notebook_btn_lock;
    public GameObject spy_Btn;
    public GameObject smoke;
    [SerializeField] private Animator spy_mode = null;
    [SerializeField] private Animator past_btn=null;//to press the animation
    public GameObject panel_for_tt;//effect to show for tt in spheere
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
    public GameObject click_here_prompt;


    // Start is called before the first frame update
    void Start()
    {
        video_2.url = System.IO.Path.Combine(Application.streamingAssetsPath, "Time_Travel_03.mp4");
        video.url= System.IO.Path.Combine(Application.streamingAssetsPath, "Teams_calls_videos_merged.mp4");
     
        //director = GetComponent<PlayableDirector>();
        director.played += Director_Played;
            director.stopped += Director_Stopped;
        video_2.loopPointReached += OnMovieFinished;
    }

    // Update is called once per frame
    void Update()
    {
       
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
                    click_here_prompt.SetActive(false);
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
                    panel_for_tt.SetActive(true);
                    video_2.gameObject.SetActive(true);
                    video_2.Play();
                    timetravaller.SetActive(false);
                    canvas_4.SetActive(false);
                  //  Level();
                    // panel.SetActive(true);
                    //video.Play();

                }

            }

        }
      /* if ((video_2.frame) > 0 && (video_2.isPlaying == false))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            panel_for_tt.SetActive(false);
            video_2.gameObject.SetActive(false);
        }*/

    }
    public void OnMovieFinished(VideoPlayer player)
    {
        player.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        panel_for_tt.SetActive(false);
        


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
        notebook_duplicate.SetActive(false);
        notebook.SetActive(true);
        click_here_prompt.SetActive(true);

    }
    private void Director_Played(PlayableDirector obj)
    {
      //  btn.SetActive(false);
    }

}
