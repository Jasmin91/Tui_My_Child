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
    public float rainDuration = 0;

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
	private Management ms_Instance; //Erstellt eine Instanz der Manager-Klasse
    private Camera m_MainCamera;

    //members
    private Vector2 m_ScreenPosition;
    private Vector3 m_WorldPosition;
    private Vector2 m_Direction;
    private float m_Angle;
    private float m_AngleDegrees;
    private float m_Speed;
    private float m_Acceleration;
    private float m_RotationSpeed;
    private float m_RotationAcceleration;
    private bool m_IsVisible = false;

    public float RotationMultiplier = 1;

    void Awake()
    {
        this.m_TuioManager = UniducialLibrary.TuioManager.Instance;
		this.ms_Instance = Management.Instance;
        ms_Instance.Regen = this; //Speichert sich selbst im Manager
        //uncomment next line to set port explicitly (default is 3333)
        //m_TuioManager.TuioPort = 7777;

        this.m_TuioManager.Connect();



        //check if the game object needs to be transformed in normalized 2d space
        if (isAttachedToGUIComponent())
        {
            Debug.LogWarning("Rotation of GUIText or GUITexture is not supported. Use a plane with a texture instead.");
            this.m_ControlsGUIElement = true;
        }

        this.m_ScreenPosition = Vector2.zero;
        this.m_WorldPosition = Vector3.zero;
        this.m_Direction = Vector2.zero;
        this.m_Angle = 0f;
        this.m_AngleDegrees = 0;
        this.m_Speed = 0f;
        this.m_Acceleration = 0f;
        this.m_RotationSpeed = 0f;
        this.m_RotationAcceleration = 0f;
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
            this.m_ScreenPosition.x = marker.getX();
            this.m_ScreenPosition.y = marker.getY();
            this.m_Angle = marker.getAngle() * RotationMultiplier;
            this.m_AngleDegrees = marker.getAngleDegrees() * RotationMultiplier;
            this.m_Speed = marker.getMotionSpeed();
            this.m_Acceleration = marker.getMotionAccel();
            this.m_RotationSpeed = marker.getRotationSpeed() * RotationMultiplier;
            this.m_RotationAcceleration = marker.getRotationAccel();
            this.m_Direction.x = marker.getXSpeed();
            this.m_Direction.y = marker.getYSpeed();
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


    //Methode mit Countdown, sorgt dafür, dass das Regen-Fiducial mindestens 4 Sekunden in die Kamera gehalten werden muss
    public IEnumerator StartCountdownToGrow(float countdownValue = 4)
    {
        currCountdownValue = countdownValue;
        while (currCountdownValue > 0)
        {
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
            ms_Instance.setRainDuration(currCountdownValue);
            if (currCountdownValue == 0)
            {
                ms_Instance.setRainReady(true);  //Es hat genug geregnet
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



    #region Getter

    

    public bool isAttachedToGUIComponent()
    {
        return (gameObject.GetComponent<GUIText>() != null || gameObject.GetComponent<GUITexture>() != null);
    }
    public Vector2 ScreenPosition
    {
        get { return this.m_ScreenPosition; }
    }
    public Vector3 WorldPosition
    {
        get { return this.m_WorldPosition; }
    }
    public Vector2 MovementDirection
    {
        get { return this.m_Direction; }
    }
    public float Angle
    {
        get { return this.m_Angle; }
    }
    public float AngleDegrees
    {
        get { return this.m_AngleDegrees; }
    }
    public float Speed
    {
        get { return this.m_Speed; }
    }
    public float Acceleration
    {
        get { return this.m_Acceleration; }
    }
    public float RotationSpeed
    {
        get { return this.m_RotationSpeed; }
    }
    public float RotationAcceleration
    {
        get { return this.m_RotationAcceleration; }
    }
    public bool IsVisible
    {
        get { return this.m_IsVisible; }
    }
    #endregion
}

