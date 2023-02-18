using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class EventManager : MonoBehaviour
{
    
    [SerializeField] private UnityEvent Eventontrigger1;
    [SerializeField] private UnityEvent Eventontrigger3;
    [SerializeField] private Animator tt= null;
    [SerializeField] private Animator gg = null;
   

    public AudioSource aud;//q's audio
    public GameObject takeover_txt;
    public GameObject direction_arrow;
    public PlayableDirector director;
    public GameObject click_gg_prompt;
    [SerializeField] private Animator gg_btn = null;
    public GameObject guided_arrow_gg;
    public GameObject guided_arrow_tt;
    public GameObject trigger4;//after timeline finished it should not play again thats why we will disable the trigger
    public AudioSource aud1;//audio when user enter the collider of trigger near q
    public AudioSource aud2;//audio for machine and console
    public GameObject stand_here_prompt;

    public GameObject tt_tag;//for changing tag in a runtime 
    public GameObject gg_tag;//for changing tag in a runtime 
    public string newtag;//for changing tag in a runtime 
    public string newtag1;//for changing tag in a runtime 
   




    //for Events like after animation of door the audio will get played//event is one of a kind of delegate
    //public delegate void TriggerAction();
    //public static event TriggerAction Ontrigger;
    void Start()
    {
        director.played += Director_Played;
        director.stopped += Director_Stopped;

    }
    private void Director_Stopped(PlayableDirector obj)
    {

        guided_arrow_gg.SetActive(true);
        guided_arrow_tt.SetActive(true);
        //arrow.SetActive(true);
        takeover_txt.SetActive(true);
        trigger4.SetActive(false);
        Edit_tt();
        Edit_gg();
    }
    private void Director_Played(PlayableDirector obj)
    {
        //  btn.SetActive(false);
    }
    

    private void OnTriggerEnter(Collider other)

    {
            if (other.CompareTag("trigger1"))
            {
                Eventontrigger1.Invoke();
            }
            else if (other.CompareTag("trigger2"))
            {
                direction_arrow.SetActive(true);
                   // glass_door1.Play("Door2", 0, 0.0f);

            }
            else if (other.CompareTag("trigger3"))
            {
                Eventontrigger3.Invoke();
           

                
               
               
            }
            else if (other.CompareTag("trigger4"))
            {

             
                    tt.Play("tt", 0, 0.0f);
                    gg.Play("gg", 0, 0.0f);
                    aud1.Play();
                    director.Play();
                 stand_here_prompt.SetActive(false);

                    
                
            }

            /*if (Ontrigger != null) //Creation of event,always check if the event is null or not
            {
                Ontrigger();
            }*/

        

        if (other.CompareTag("triggerM"))
        {
            aud2.Play();
        }


    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("trigger4")) 
        {
            director.Pause();
        }
        else if (other.CompareTag("triggerM"))
        {
            aud2.Pause();
        }
    }
   
    IEnumerator click_appear() //waiting for grapple gun to end its animation
    {
        yield return new WaitForSeconds(3f);
        click_gg_prompt.SetActive(true);
        if(click_gg_prompt.active ==true)
        {
            gg_btn.Play("Button_gg_Icon", 0, 0.0f);
        }
    }
    public void click()
    {
        StartCoroutine(click_appear());
        //SceneManager.LoadScene(1);
    }
    public void Edit_tt()//for changing tag in a script
    {
        tt_tag.tag = newtag;
       
    }
    public void Edit_gg()//for changing tag in a script
    {
        
        gg_tag.tag = newtag1;
    }


}
