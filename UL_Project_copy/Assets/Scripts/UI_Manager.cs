using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.Video;

public class UI_Manager : MonoBehaviour
{
     
     public GameObject obj_tt;
     public GameObject btn_tt;
    public GameObject obj_gg;
    public GameObject btn_gg;
    public VideoPlayer Video;
    public GameObject panel;
    public AudioSource audi;//audio for welcome text as soon as it over btn appear
    public GameObject btn_enterance;//audio for welcome text as soon as it over btn appear
    public GameObject takeover_txt;
    [SerializeField] private UnityEvent Event;
    [SerializeField]
    private UnityEvent Anim_events;



    //public Transform liftpos;



    // Start is called before the first frame update
    void Start()
    {
        //aud = GetComponent<AudioSource>();
        //gg_target = GetComponent<Animation>();
        Invoke("Button", audi.clip.length);


    }

    // Update is called once per frame
    void Update()
    {
        
        //obj_gg1.transform.rotation.y= FPS.transform.rotation.y;

        //audi1.PlayDelayed(10f);
        if (Input.GetMouseButtonDown(0)&& !EventSystem.current.IsPointerOverGameObject())//Using Raycast to click on the object i.e teleportation lift
        {
            RaycastHit hit;
           
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
           
            if(Physics.Raycast(ray, out hit,100.0f))
            {
                if (hit.collider.tag == "tt")
                {
                obj_tt.SetActive(false);
                btn_tt.SetActive(true);
                obj_gg.SetActive(true);
                    Anim_events.Invoke();
                }
                else if(hit.collider.tag == "gg")
                {
                    //  Event.Invoke();
                    obj_gg.SetActive(false);
                    btn_gg.SetActive(true);
                    takeover_txt.SetActive(false);
                    Event.Invoke();
                }
            }
            
        }
        
      
    }
    IEnumerator Video_Play()
    {
        yield return new WaitForSeconds(2f);
        panel.SetActive(true);
        Video.Play();
    }
    public void Level()
    {
        StartCoroutine(Video_Play());
        //SceneManager.LoadScene(1);
    }
    void OnEnable()
    {
        //EventManager.Ontrigger += play_clip; subscribe to particular event
        
    }
    void OnDisable()
    {
       // EventManager.Ontrigger -= play_clip;unsubscribe to particular event for that we specifically use Enable and OnDisable
       
    }
    /*public void Transform_position()
    {
        FPS.transform.position = liftpos.transform.position;
     //  FPS.transform.rotation = Quaternion.Euler(0, 180f, 0);
    }*/

    public void Button()
    {
        btn_enterance.SetActive(true);
    }
   

}
