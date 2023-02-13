using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class della : MonoBehaviour
{
    private PlayableDirector director;
    public GameObject btn;
    // Start is called before the first frame update
    void Start()
    {
        director = GetComponent<PlayableDirector>();
        director.played += Director_Played;
        director.stopped += Director_Stopped;
    }
    private void Director_Stopped(PlayableDirector obj)
    {
        btn.SetActive(true);
    }
    private void Director_Played(PlayableDirector obj)
    {
        btn.SetActive(false);
    }
    public void StartTimeline()
    {
        director.Play();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
