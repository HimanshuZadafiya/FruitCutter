using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour
{

    void Start()
    {
        Invoke(nameof(CompleteSplash),2f);
    }
    public void CompleteSplash()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    private void Update()
    {

    }


}
