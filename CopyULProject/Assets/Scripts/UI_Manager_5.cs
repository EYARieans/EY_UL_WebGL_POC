using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;

public class UI_Manager_5 : MonoBehaviour
{
    [SerializeField] private Animator smoke=null;
    public GameObject notebook;
    public GameObject guided_arrow;
    public GameObject click_promt;//tthe click on It lady notebook prompt
    [SerializeField] private Animator dairy_mode = null;
    [SerializeField] private Animator madq = null;
    public GameObject dairy_mode_normal;
    public GameObject Q_prompt;
    public PlayableDirector director;
    public PlayableDirector director1;
    public GameObject next_btn;
    public AudioSource aud_q;
    public GameObject screen_prompt;
    [SerializeField] private Animator Spy_toggle_off_anim = null;
    public GameObject prompt;
    public GameObject close_btn;
    public AudioSource aud_obj;
    public GameObject click_here_prompt;

    // Start is called before the first frame update
    void Start()
    {
        director.played += Director_Played;
        director.stopped += Director_Stopped;
        director1.played += Director_Played1;
        director1.stopped += Director_Stopped1;
        StartCoroutine(Screen_prompt());
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
                    guided_arrow.SetActive(false);
                    click_promt.SetActive(false);
                    dairy_mode_normal.SetActive(false);
                    dairy_mode.gameObject.SetActive(true);
                    dairy_mode.Play("Diary", 0, 0.0f);
                    smoke.Play("Smokewave", 0, 0.0f);
                    aud_obj.Play();
                    click_here_prompt.SetActive(false);

                }
                

            }

        }
        /*if (aud_q.time != 0 && !aud_q.isPlaying)
        {
            
        }*/
    }
    public void q_prompt_audi()
    {
        madq.Play("New State", 0, 0.0f);
        next_btn.SetActive(true);
    }
    IEnumerator Screen_prompt() //waiting for grapple gun to end its animation
    {
        yield return new WaitForSeconds(2f);
        screen_prompt.SetActive(true);
        if (screen_prompt.active == true)
        {
            Spy_toggle_off_anim.Play("Spyoff_effect", 0, 0.0f);
        }

    }
    private void Director_Stopped(PlayableDirector obj)
    {
        madq.gameObject.SetActive(true);
        madq.Play("Q", 0, 0.0f);
        Q_prompt.SetActive(true);
       
        Invoke("q_prompt_audi", aud_q.clip.length);
        //replay_btn.SetActive(true);
    }
    private void Director_Played(PlayableDirector obj)
    {
        
        //  btn.SetActive(false);
    }
    private void Director_Stopped1(PlayableDirector obj)
    {
        prompt.SetActive(true);
       // close_btn.SetActive(true);

        //replay_btn.SetActive(true);
    }
    private void Director_Played1(PlayableDirector obj)
    {
        //  btn.SetActive(false);
    }
    public void close_app()
    {
        Application.Quit();
    }
}
