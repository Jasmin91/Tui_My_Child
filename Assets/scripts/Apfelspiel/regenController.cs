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

Code angepasst von: Jasmin Profus
*/

using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Kontrolliert den Regen
/// </summary>
public class regenController : MonoBehaviour
{
    public int MarkerID = 0;

    /// <summary>
    /// Countdown-Dauer
    /// </summary>
    public float Duration = 0f;

    /// <summary>
    /// Dauer, die es regnet
    /// </summary>
    private int rainDuration = 0;

    /// <summary>
    /// Hilfsbool gibt an, dass Fiducial nicht sofort erkannt wird
    /// </summary>
    private bool ShouldWait = true;
    

    /// <summary>
    /// Dauer, die es regnen soll
    /// </summary>
    public float countdownDuration = 5; 

    /// <summary>
    /// Bool, ob es genug geregnet hat
    /// </summary>
    private bool RainReady = false;

    /// <summary>
    /// Sound bei Anzeigen der Wolke
    /// </summary>
    public AudioSource RainSound;
    //http://www.salamisound.de/1871420-einsetzender-regen-auf-einer (Abrufdatum: 29.01.18)

    /// <summary>
    /// Hilfsbool, damit Sound nur 1xgespielt wird
    /// </summary>
    private bool PlayingSound = false;

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

    /// <summary>
    ///Erstellt eine Instanz der Manager-Klasse 
    /// </summary>
	private ApfelManager ms_Instance; 
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
        HideGameObject();
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

    
        
        
            if (this.m_TuioManager.IsConnected
                && this.m_TuioManager.IsMarkerAlive(this.MarkerID))
            {
                if (!ShouldWait) { 
                    TUIO.TuioObject marker = this.m_TuioManager.GetMarker(this.MarkerID);

                this.m_IsVisible = true;

                //set game object to visible, if it was hidden before
                ShowGameObject();

            }
           }
            else
            {
                //automatically hide game object when marker is not visible
                if (this.AutoHideGO)
                {
                    HideGameObject();
                    ShouldWait = false;
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


    /// <summary>
    /// Zeigt die Regenwolke
    /// </summary>
    private void ShowGameObject()
    {
       
            if (Duration < 5)
                Duration += Time.deltaTime;
            rainDuration = (int)Duration;
        if(rainDuration == 5)
        {
            this.RainReady = true;
        }

        if (!PlayingSound) {
            RainSound.Play();
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




    /// <summary>
    /// Blendet Regenwolke aus
    /// </summary>
    private void HideGameObject()
    {
        if (rainDuration < 5)
        {
            rainDuration = 0;
            Duration = 0;
        }

        if (PlayingSound)
        {
            RainSound.Stop();
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


    #region Getter&Setter

    /// <summary>
    /// Setzt bool, ob es genug geregnet hat
    /// </summary>
    /// <param name="rain">bool, ob es genug geregnet hat</param>
    public void SetRainReady(bool rain)
    {
        this.RainReady = rain;

    }

    /// <summary>
    /// Getter, ob es genug geregnet hat
    /// </summary>
    /// <returns>bool, ob es genug geregnet hat</returns>
    public bool GetRainReady()
    {
        return this.RainReady;
    }

    /// <summary>
    /// Getter, wie lang es bisher geregnet hat
    /// </summary>
    /// <returns>Bisherige Regendauer</returns>
    public int GetRainDuration()
    {
        return this.rainDuration;
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

