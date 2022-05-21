using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioClip kbc, win, correct, error;
    public AudioSource audioSource;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        audioSource.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void KBC()
    {
        audioSource.clip = kbc;
        audioSource.Play();
    }
    public void WinFx()
    {
        audioSource.clip = win;
        audioSource.Play();
    }
    public void Correct()
    {
        audioSource.clip = correct;
        audioSource.Play();
    }
    public void Error()
    {
        audioSource.clip = error;
        audioSource.Play();
    }
    public void Stop()
    {
        audioSource.Stop();
    }

}
