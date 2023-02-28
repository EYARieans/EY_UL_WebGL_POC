using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Collections;

namespace EY.Managers.Sound
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] public AudioSource audio_MainSource;
        [SerializeField] private List<AudioSample> audioSamples;
        private static SoundManager _instance;
        public static SoundManager Instance { get { return _instance; } }
        public Action audioSourceIsPlaying { get; set; }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        public void Start()
        {
            //var audioSourceStream = Observable.EveryUpdate().Where(_ => !audio_MainSource.isPlaying).Subscribe(x=> audioSourceIsPlaying?.Invoke());
        }

        public void PlayOneShot(string clipName, float volScale = 0.5f, Action callBack = null)
        {
            var clip = audioSamples.Where(s=>s.name == clipName).FirstOrDefault();
            audio_MainSource.PlayOneShot(clip.audioClip, volScale);
            var sub = Observable.Interval(TimeSpan.FromSeconds((int)clip.audioClip.length)).Subscribe(x => callBack?.Invoke(), () => Debug.Log("Audio completed!"));
            sub.Dispose();
        }

        public IEnumerator PlayClip(string clipName, float volScale = 0.5f, Action callBack = null)
        {
            var clip = audioSamples.Where(s => s.name == clipName).FirstOrDefault();
            audio_MainSource.PlayOneShot(clip.audioClip, volScale);
            yield return new WaitForSeconds(clip.audioClip.length);
            callBack?.Invoke();
        }

    }

    [Serializable]
    public class AudioSample
    {
        public string name;
        public AudioClip audioClip;
    }
}

