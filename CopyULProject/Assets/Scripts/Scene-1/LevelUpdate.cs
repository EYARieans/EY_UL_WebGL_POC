using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class LevelUpdate : MonoBehaviour
{
    //public Animator transition;
    public VideoPlayer video;
    public Transform pos;
    public Camera cam;
    float xRotation;
    //public float transitiontime=1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    
    public void level_Change()
    {
        SceneManager.LoadScene(0);
    }
    /*IEnumerator  LoadLevel(int levelIndex)
    {
       // yield return new WaitForSeconds((float)video.length);
        //SceneManager.LoadScene(levelIndex);
    }*/
    public void Level()
    {
        if ((video.frame) > 0 && (video.isPlaying == false))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            video.Stop();
        }
        
        //SceneManager.LoadScene(1);
    }
    public void next_btn()
    {
        cam.transform.position = pos.transform.position;
        transform.rotation = Quaternion.Euler(xRotation, -90, 0);


    }

        
 }


