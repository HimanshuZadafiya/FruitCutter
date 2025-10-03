using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance;

    [Header("Background Music")]
    public AudioSource bgAudio;
    public AudioClip bgClip;


    [Header("Button Audio")]
    public AudioSource btnAudio;
    public AudioClip btnClip;

    
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            DestroyImmediate(this.gameObject);
        }

    }

    private void Start()
    {
        // if (DataManager.Instance.GetSound() == 0)
        // {
        //     StartBackgroundMusic();
        // }
        // else if (DataManager.Instance.GetSound() == 1)
        // {
        //     StopBackgroundMusic();
        // }
    }


    public void StartBackgroundMusic()
    {
        if (DataManager.Instance.GetSound() == 0)
        {
            bgAudio.clip = bgClip;
            bgAudio.Play();
        }
        else
        {
            bgAudio.Stop();
        }
    }
    public void StopBackgroundMusic()
    {
        bgAudio.Stop();
    }

    public void ButtonClick()
    {
        if (DataManager.Instance.GetSound() == 0)
        {
            btnAudio.clip = btnClip;
            btnAudio.Play();
        }
    }

  


}
