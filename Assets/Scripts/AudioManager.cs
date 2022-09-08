using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System.Linq;


public class AudioManager : MonoBehaviour {
    public AudioClip[] audioClips;
    private AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public void Stop() {
        if (audioSource.loop == true) {
            // can stop right now looping newer ends
            audioSource.Stop();
        } 
    }

        public void Play(string name, bool loop = false) {

        AudioClip clip = audioClips.Where(item => item.name == name).FirstOrDefault();
        if (clip) {
            audioSource.clip = clip;
            audioSource.loop = loop;
            audioSource.Play();
        } else {
            Debug.LogWarning("No audio clip named: " + name);
        }

    }

}
