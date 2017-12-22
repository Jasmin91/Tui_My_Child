﻿/*
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

/// <summary>  
///  Diese Klasse steuert die  Funktion der Sonne
/// </summary> 

public class sonneController : MonoBehaviour
{
    public int MarkerID = 0;

    public enum RotationAxis { Forward, Back, Up, Down, Left, Right };
    public float sunDuration = 0;
    public float countdownDuration = 5; //Dauer, die die Sonne scheint
    private bool sunReady = false; //die Sonne hat genug geschienen
    //translation
    public bool IsPositionMapped = false;
    public bool InvertX = false;
    public bool InvertY = false;

    //rotation
    public bool IsRotationMapped = false;
    public bool AutoHideGO = false;
    private bool m_ControlsGUIElement = false;


    public float CameraOffset = 10;
    public RotationAxis RotateAround = RotationAxis.Back;
    private UniducialLibrary.TuioManager m_TuioManager;
	private Management ms_Instance; //Erstellt eine Instanz der Manager-Klasse
    private Camera m_MainCamera;

    //members
    private bool m_IsVisible;

    public float RotationMultiplier = 1;

    void Awake()
    {
        this.m_TuioManager = UniducialLibrary.TuioManager.Instance;
		this.ms_Instance = Management.Instance;
        ms_Instance.Sun = this; //Speichert sich selbst im Management
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

            //update parameters
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
        if (ms_Instance.Rain.getRainReady()) {
            StartCoroutine(StartCountdownToRed());
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

    //Methode mit Zähler, sorgt dafür, dass das Sonnen-Fiducial mindestens countdownDuration Sekunden in die Kamera gehalten werden muss
    public IEnumerator StartCountdownToRed(float countdownValue = 0)
    {
        sunDuration = countdownValue;
        while (sunDuration < countdownDuration)
        {
            yield return new WaitForSeconds(1.0f);
            sunDuration++;
            ms_Instance.ApfelReifenLassen(this.sunDuration);
            if (sunDuration == 5)
            {
                this.setSunReady(true); //Sonne hat genug geschienen
            }
        }


    }

    public void setSunReady(bool sun)
    {
        this.sunReady = sun;

    }

    public bool getSunReady()
    {
        return this.sunReady;
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

