using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    [SerializeField] private UnityEvent myAudioEvent;
    [SerializeField] private Animator glass_door = null;
    [SerializeField] private Animator glass_door1 = null;
    [SerializeField] private Animator tt= null;
   
    [SerializeField] private bool openTrigger = false;
    bool m_ToggleChange = true;
    public GameObject arrow;
    public AudioSource aud;
    public GameObject takeover_txt;
    public GameObject gg_txt;
    public GameObject image_click;//to guide the user
    public GameObject image_click2;





    //for Events like after animation of door the audio will get played//event is one of a kind of delegate
    //public delegate void TriggerAction();
    //public static event TriggerAction Ontrigger;


    private void OnTriggerEnter(Collider other)

    {
        if (openTrigger)
        {
            if (other.CompareTag("trigger1"))
            {
                glass_door.Play("Door1", 0, 0.0f);
            }
            else if (other.CompareTag("trigger2"))
            {
               
                    glass_door1.Play("Door2", 0, 0.0f);
                    
            }
            else if (other.CompareTag("trigger3"))
            {
                gg_txt.SetActive(true);
                image_click2.SetActive(true);
                

            }
            else if (other.CompareTag("trigger4"))
            {

             if (m_ToggleChange == true)//to play the audio only once
               {
                    tt.Play("tt", 0, 0.0f);
                    myAudioEvent.Invoke();
                    m_ToggleChange = false;
                    Invoke("Arrow_display", aud.clip.length); //Invoke is called to display the text msg after the audio clip in length i.e in sec     

                }
            }

            /*if (Ontrigger != null) //Creation of event,always check if the event is null or not
            {
                Ontrigger();
            }*/

        }
        

    }
    public void Arrow_display()
    {
        arrow.SetActive(true);
        takeover_txt.SetActive(true);
        image_click.SetActive(true);

    }
}
