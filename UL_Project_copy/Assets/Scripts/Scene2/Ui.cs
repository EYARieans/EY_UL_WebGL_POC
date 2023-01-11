using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Video;


public class Ui : MonoBehaviour
{
    public AudioSource aud;
    public GameObject arrow;
    public GameObject Q_prompt;
    bool m_ToggleChange = true;
    [SerializeField] private bool openTrigger = false;
    [SerializeField] private Animator future_btn = null;
    public GameObject panel;
    public VideoPlayer video;

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
    }
    IEnumerator Video_Play()
    {
        yield return new WaitForSeconds(1f);
        panel.SetActive(true);
        video.Play();
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
        
        arrow.SetActive(true);
       
    }

}
