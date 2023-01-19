using System.Collections;
using System.IO;
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
    public GameObject btn_tt_lock;
    public GameObject btn_gg_lock;
    public GameObject obj_gg;
    public GameObject btn_gg;
    public VideoPlayer Video;//this video is component is for showing teleportation effect from one scene to anothe scene
    private string videoUrl;
    public GameObject panel;
    public AudioSource audi;//audio for welcome text as soon as it over btn appear
    public GameObject btn_enterance;//audio for welcome text as soon as it over btn appear
    public GameObject takeover_txt;
    public GameObject Close_btn;//this btn is the starting btn where the btn will close then audio will play
    [SerializeField] private UnityEvent Event;
    [SerializeField]
    private UnityEvent Anim_events;



    //public Transform liftpos;



    // Start is called before the first frame update
    void Start()
    {
        //aud = GetComponent<AudioSource>();
        //gg_target = GetComponent<Animation>();
        //videoUrl = "C:/Users/KV763EQ/unity porjects/Copy_UL_Project/UL_Project_copy/Assets/StreamingAssets/teleportation lift up.mp4";
       // Video.url = "file:///" + videoUrl;
       Video.url = System.IO.Path.Combine(Application.streamingAssetsPath, "teleportation.mp4");


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
                if (hit.collider.tag == "tt")// moment when we take tt from mad scientist
                {
                obj_tt.SetActive(false);
                btn_tt_lock.SetActive(false);
                btn_tt.SetActive(true);
                obj_gg.SetActive(true);
                    Anim_events.Invoke();
                }
                else if(hit.collider.tag == "gg")//moment when we take gg from Mad scientist
                {
                    //  Event.Invoke();
                    obj_gg.SetActive(false);
                    btn_gg_lock.SetActive(false);
                    btn_gg.SetActive(true);
                    takeover_txt.SetActive(false);
                    Event.Invoke();
                }
            }
            
        }
        
      
    }
    IEnumerator Video_Play() //waiting for grapple gun to end its animation
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
   public void close()
    {
        Close_btn.SetActive(false);
        Invoke("Button", audi.clip.length);
    }

}
