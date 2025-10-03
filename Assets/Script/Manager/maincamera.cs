using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maincamera : MonoBehaviour {

    public Camera maincam;
    public Blade blade;
	// Use this for initialization
	void Start () {
       // blade = FindObjectOfType<Blade>();
        ScaleBackgroundImageFitScreenSize();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void ScaleBackgroundImageFitScreenSize()
    {
        Vector2 deviceScreenResolution = new Vector2(Screen.width, Screen.height);

        float srcHeight = Screen.height;
        float srcWidth = Screen.width;

        float Device_Scree_Aspect = srcWidth / srcHeight;
       // blade.moveX = Device_Scree_Aspect;
        //set main Camera aspect
        maincam.aspect = Device_Scree_Aspect;

        float Cameraheight = 100.0f * maincam.orthographicSize * 2.0f;
        float camWidth = Cameraheight * Device_Scree_Aspect;
        //print(Cameraheight);
        //backgroundimage image size
       
        
     
    }
















}
