using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
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
    public GameObject highlight_prompt;
    public GameObject guided_arrow;
    public VideoPlayer Video1;//splash screen video
    public AudioSource audi1;
    public GameObject panel1;
    public GameObject pane2;
    public AudioSource aud_obj;//sfx for gg and tt
    public GameObject guided_arrow_gg;
    public GameObject guided_arrow_tt;

    // Start is called before the first frame update
    void Start()
    {
        //aud = GetComponent<AudioSource>();
        //gg_target = GetComponent<Animation>();
        //videoUrl = "C:/Users/KV763EQ/unity porjects/Copy_UL_Project/UL_Project_copy/Assets/StreamingAssets/teleportation lift up.mp4";
        // Video.url = "file:///" + videoUrl;
        //Video.url = $"{Application.streamingAssetsPath}/Grapple_gun_teleport_effect_scene_01_to_02_v07.mp4";
        Video1.url = $"{Application.streamingAssetsPath}/Splash_screen.mp4";
    }

    // Update is called once per frame
    void Update()
    {
        if ((Video1.frame) > 0 && (Video1.isPlaying == false)) //splash screee video
        {
            pane2.SetActive(true);
            panel1.SetActive(false);
            audi1.Play();
        }
        //obj_gg1.transform.rotation.y= FPS.transform.rotation.y;

        //audi1.PlayDelayed(10f);
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())//Using Raycast to click on the object i.e teleportation lift
        {
            RaycastHit hit;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.collider.tag == "tt")// moment when we take tt from mad scientist
                {
                    guided_arrow_tt.SetActive(false);
                    obj_tt.SetActive(false);
                    btn_tt_lock.SetActive(false);
                    btn_tt.SetActive(true);
                    aud_obj.Play();
                }
                else if (hit.collider.tag == "gg")//moment when we take gg from Mad scientist
                {
                    guided_arrow_gg.SetActive(false);
                    //  Event.Invoke();
                    obj_gg.SetActive(false);
                    btn_gg_lock.SetActive(false);
                    btn_gg.SetActive(true);
                    takeover_txt.SetActive(false);
                    aud_obj.Play();

                }
            }

        }
        if (obj_tt.active == false && obj_gg.active == false && guided_arrow.active == true)
        {
            StartCoroutine(prompt_prompt());

        }


    }
    IEnumerator prompt_prompt() //waiting for grapple gun to end its animation
    {
        yield return new WaitForSeconds(1f);
        highlight_prompt.SetActive(true);

    }

    IEnumerator Video_Play() //waiting for grapple gun to end its animation
    {
        yield return new WaitForSeconds(0.45f);
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

    public void Spalsh_video()
    {
        Video1.Play();
    }


}
