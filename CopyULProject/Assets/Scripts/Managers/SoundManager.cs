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
        [SerializeField] private AudioSource audio_MainSource;
        [SerializeField] private List<AudioSample> audioSamples;
        public Action audioSourceIsPlaying { get; set; }

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
            yield return new WaitWhile(() => audio_MainSource.isPlaying);
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

