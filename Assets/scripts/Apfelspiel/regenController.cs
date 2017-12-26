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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class regenController : MonoBehaviour
{
    public int MarkerID = 0;
    private float rainDuration = 0; //Dauer, die die Sonne scheint
    public float countdownDuration = 5; //Dauer, die die Sonne scheinen soll
    private bool RainReady = false; //es hat genug geregnet

    public enum RotationAxis { Forward, Back, Up, Down, Left, Right };
    float currCountdownValue;

    //translation
    public bool IsPositionMapped = false;

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
    private bool m_IsVisible = false;

    public float RotationMultiplier = 1;

    void Awake()
    {
        this.m_TuioManager = UniducialLibrary.TuioManager.Instance;
		this.ms_Instance = ApfelManager.Instance;
        ms_Instance.Rain = this; //Speichert sich selbst im Manager
        //uncomment next line to set port explicitly (default is 3333)
        //m_TuioManager.TuioPort = 7777;

        this.m_TuioManager.Connect();



        //check if the game object needs to be transformed in normalized 2d space
        if (isAttachedToGUIComponent())
        {
            Debug.LogWarning("Rotation of GUIText or GUITexture is not supported. Use a plane with a texture instead.");
            this.m_ControlsGUIElement = true;
        }
        this.m_IsVisible = false;
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
            
            this.m_IsVisible = true;

            //set game object to visible, if it was hidden before
            ShowGameObject();
            

        }
        else
        {
            //automatically hide game object when marker is not visible
            if (this.AutoHideGO)
            {
                HideGameObject();
            }
            this.m_IsVisible = false;
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
        
        StartCoroutine(StartCountdownToGrow());
       
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

 
    //Methode mit Zähler, sorgt dafür, dass das Regen-Fiducial mindestens countdownDuration Sekunden in die Kamera gehalten werden muss
    public IEnumerator StartCountdownToGrow(float countdownValue = 0)
    {
        if (!RainReady)
        {
            rainDuration = countdownValue;
            while (rainDuration <= countdownDuration)
            {

                rainDuration++;
                ms_Instance.ApfelWachsenLassen(this.rainDuration);
                yield return new WaitForSeconds(1.0f);
                if (rainDuration >= countdownDuration)
                {
                    this.setRainReady(true); //Es hat genug geregnet
                 }

            }
        }
    }


    private void HideGameObject()
    {
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

    public void setRainReady(bool rain)
    {
        this.RainReady = rain;

    }

    public bool getRainReady()
    {
        return this.RainReady;
    }

    public float getRainDuration()
    {
        return this.rainDuration;
    }

    public void setRainDuration(float duration)
    {
        this.rainDuration = duration;
    }
    #region Getter



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

