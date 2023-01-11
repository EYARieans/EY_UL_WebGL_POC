using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class LevelUpdate : MonoBehaviour
{
    //public Animator transition;
    public VideoPlayer video;
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
        SceneManager.LoadScene(4);
    }
    IEnumerator  LoadLevel(int levelIndex)
    {
        yield return new WaitForSeconds((float)video.length);
        SceneManager.LoadScene(levelIndex);
    }
    public void Level()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        //SceneManager.LoadScene(1);
    }

}
