/*
Copyright (c) 2012 André Gröschel

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/

using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>  
///  Diese Klasse steuert die  Funktion des Windes
/// </summary> 
public class windController : MonoBehaviour
{
    public int MarkerID = 0;

    public AudioSource WindSound;

    private bool PlayingSound = false;

    public enum RotationAxis { Forward, Back, Up, Down, Left, Right };

    //translation
    public bool IsPositionMapped = false;
    public bool InvertX = false;
    public bool InvertY = false;
    public float yAxe = -2;

    //rotation
    public bool IsRotationMapped = false;
    public bool AutoHideGO = false;
    private bool m_ControlsGUIElement = false;


    public float CameraOffset = 10;
    public RotationAxis RotateAround = RotationAxis.Back;
    private UniducialLibrary.TuioManager m_TuioManager;
	private ApfelManager ms_Instance; //Erstellt eine Instanz der Manager-Klasse
    private Camera m_MainCamera;

    //members
    private bool m_IsVisible;

    public float RotationMultiplier = 1;
    private bool warWeg = true; //Boolsche Variable, ob der Wind ausgeblendet war, bevor er gezeigt wurde

    void Awake()
    {
        this.m_TuioManager = UniducialLibrary.TuioManager.Instance;
		this.ms_Instance = ApfelManager.Instance;

        //uncomment next line to set port explicitly (default is 3333)
        //m_TuioManager.TuioPort = 7777;

        this.m_TuioManager.Connect();



        //check if the game object needs to be transformed in normalized 2d space
        if (isAttachedToGUIComponent())
        {
            Debug.LogWarning("Rotation of GUIText or GUITexture is not supported. Use a plane with a texture instead.");
            this.m_ControlsGUIElement = true;
        }
        
        this.m_IsVisible = true;
    }

    void Start()
    {
        //get reference to main camera
        this.m_MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        //check if the main camera exists
        if (this.m_MainCamera == null)
        {
            Debug.LogError("There is no main camera defined in your scene.");
        }
    }

    void Update()
    {

		if (this.m_TuioManager.IsMarkerAlive(this.MarkerID)) {
			//Debug.Log("FidcialController Zeile 110:this.m_TuioManager.IsMarkerAlive(this.MarkerID)");
		}


        if (this.m_TuioManager.IsConnected
            && this.m_TuioManager.IsMarkerAlive(this.MarkerID))
        {
            TUIO.TuioObject marker = this.m_TuioManager.GetMarker(this.MarkerID);

            //update parameters
            this.m_IsVisible = true;

            //set game object to visible, if it was hidden before
            ShowGameObject();
            if (warWeg) {
                ms_Instance.EinApfelFaellt();
                this.warWeg = false;
            }
            

        }
        else
        {
            //automatically hide game object when marker is not visible
            if (this.AutoHideGO)
            {
                HideGameObject();
            }            
            this.m_IsVisible = false;
            this.warWeg = true; //Stellt sicher, dass Fiducial für jeden Apfel neu gezeigt werden muss
        }
    }


    void OnApplicationQuit()
    {
        if (this.m_TuioManager.IsConnected)
        {
            this.m_TuioManager.Disconnect();
        }
    }

    
    private void ShowGameObject()
    {
        if (!PlayingSound)
        {
            WindSound.Play();
            PlayingSound = true;
        }

        if (this.m_ControlsGUIElement)
        {
            //show GUI components
            if (gameObject.GetComponent<GUIText>() != null && !gameObject.GetComponent<GUIText>().enabled)
            {
                gameObject.GetComponent<GUIText>().enabled = true;
            }
            if (gameObject.GetComponent<GUITexture>() != null && !gameObject.GetComponent<GUITexture>().enabled)
            {
                gameObject.GetComponent<GUITexture>().enabled = true;
            }
        }
        else
        {
            if (gameObject.GetComponent<Renderer>() != null && !gameObject.GetComponent<Renderer>().enabled)
            {
                gameObject.GetComponent<Renderer>().enabled = true;
            }
        }
    }

    private void HideGameObject()

    {
        if (PlayingSound)
        {
            WindSound.Stop();
            PlayingSound = false;
        }
        if (this.m_ControlsGUIElement)
        { 
            
        
            //hide GUI components
            if (gameObject.GetComponent<GUIText>() != null && gameObject.GetComponent<GUIText>().enabled)
            {
                gameObject.GetComponent<GUIText>().enabled = false;
            }
            if (gameObject.GetComponent<GUITexture>() != null && gameObject.GetComponent<GUITexture>().enabled)
            {
                gameObject.GetComponent<GUITexture>().enabled = false;
            }
        }
        else
        {
            //set 3d game object to visible, if it was hidden before
            if (gameObject.GetComponent<Renderer>() != null && gameObject.GetComponent<Renderer>().enabled)
            {
                gameObject.GetComponent<Renderer>().enabled = false;
            }
        }
    }

    #region Getter

    public bool getWarWeg()
    {
        return this.warWeg;
    }

    public bool isAttachedToGUIComponent()
    {
        return (gameObject.GetComponent<GUIText>() != null || gameObject.GetComponent<GUITexture>() != null);
    }
   
    public bool IsVisible
    {
        get { return this.m_IsVisible; }
    }
    #endregion
}

