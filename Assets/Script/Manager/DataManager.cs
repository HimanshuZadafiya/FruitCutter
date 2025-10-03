using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
   

    private void Awake()
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

 public void SetSound(int no)
    {
        PlayerPrefs.SetInt("SoundValue", no);

    }
    public int GetSound()
    {
        return PlayerPrefs.GetInt("SoundValue", 0);
    }

    public void SetVibration(int no)
    {
        PlayerPrefs.SetInt("VibrationValue", no);
    }
    public int GetVibration()
    {
        return PlayerPrefs.GetInt("VibrationValue", 0);
    }


   

}
