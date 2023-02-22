using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class EventManager : MonoBehaviour
{
    [SerializeField] private UnityEvent myAudioEvent;
    [SerializeField] private UnityEvent Eventontrigger1;
    [SerializeField] private UnityEvent Eventontrigger3;
    [SerializeField] private Animator tt= null;
    [SerializeField] private Animator gg = null;

    [SerializeField] private bool openTrigger = false;
    bool m_ToggleChange = true;
    public GameObject arrow;
    public AudioSource aud;
    public GameObject takeover_txt;
    public GameObject highlight_txt;
    public GameObject direction_arrow;
    public PlayableDirector director;
    public GameObject click_gg_prompt;
    [SerializeField] private Animator gg_btn = null;
    public GameObject guided_arrow_gg;
    public GameObject guided_arrow_tt;
<<<<<<< Updated upstream
=======
    public GameObject trigger4;//after timeline finished it should not play again thats why we will disable the trigger
    public AudioSource aud1;//audio when user enter the collider of trigger near q
    public AudioSource aud2;//audio for machine and console
    public GameObject stand_here_prompt;

    public GameObject tt_tag;//for changing tag in a runtime 
    public GameObject gg_tag;//for changing tag in a runtime 
    public string newtag;//for changing tag in a runtime 
    public string newtag1;//for changing tag in a runtime 
>>>>>>> Stashed changes

    public GameObject door_collider;



    //for Events like after animation of door the audio will get played//event is one of a kind of delegate
    //public delegate void TriggerAction();
    //public static event TriggerAction Ontrigger;

<<<<<<< Updated upstream
=======
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
        door_collider.SetActive(false);
    }
    private void Director_Played(PlayableDirector obj)
    {
        //  btn.SetActive(false);
    }
    
>>>>>>> Stashed changes

    private void OnTriggerEnter(Collider other)

    {
        if (openTrigger)
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
                highlight_txt.SetActive(true);
               
            }
            else if (other.CompareTag("trigger4"))
            {

             if (m_ToggleChange == true)//to play the audio only once
               {
                    tt.Play("tt", 0, 0.0f);
                    gg.Play("gg", 0, 0.0f);
                    m_ToggleChange = false;
                    director.Play();
                    Invoke("Arrow_display", aud.clip.length); //Invoke is called to display the text msg after the audio clip in length i.e in sec     

                }
            }

            /*if (Ontrigger != null) //Creation of event,always check if the event is null or not
            {
                Ontrigger();
            }*/

        }

        if (other.CompareTag("triggerM"))
        {
            myAudioEvent.Invoke();
        }


    }
    public void Arrow_display()
    {
        guided_arrow_gg.SetActive(true);
        guided_arrow_tt.SetActive(true);
        arrow.SetActive(true);
        takeover_txt.SetActive(true);

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
}
