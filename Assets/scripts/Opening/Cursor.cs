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
using UnityEngine.SceneManagement;

/// <summary>
/// Steuert den Cursor des Startbildschirms
/// </summary>
public class Cursor : MonoBehaviour
{
    public int MarkerID0=0;
    public int MarkerID1=1;
    public int MarkerID2=2;
    public int MarkerID3=3;

    /// <summary>
    /// Sound bei Auswählen eines Buttons
    /// </summary>
    public AudioSource PlingSound;

    
    public enum RotationAxis { Forward, Back, Up, Down, Left, Right };

    //translation
   
    public bool InvertX = false;

    //rotation
    private bool m_ControlsGUIElement = false;


    public float CameraOffset = 10;
    public RotationAxis RotateAround = RotationAxis.Back;
    private UniducialLibrary.TuioManager m_TuioManager;
	private ApfelManager ms_Instance; //Erstellt eine Instanz der Manager-Klasse
    private Camera m_MainCamera;

    //members
    private Vector2 m_ScreenPosition;
    private Vector3 m_WorldPosition;
    private bool m_IsVisible;




    public float PosY = 0;
    

    void Awake()
    {
        this.m_TuioManager = UniducialLibrary.TuioManager.Instance;
		this.ms_Instance = ApfelManager.Instance;

        //uncomment next line to set port explicitly (default is 3333)
        //m_TuioManager.TuioPort = 7777;

        this.m_TuioManager.Connect();
        PosY = this.transform.position.y;


        //check if the game object needs to be transformed in normalized 2d space
        if (isAttachedToGUIComponent())
        {
            Debug.LogWarning("Rotation of GUIText or GUITexture is not supported. Use a plane with a texture instead.");
            this.m_ControlsGUIElement = true;
        }

        this.m_ScreenPosition = Vector2.zero;
       this.m_WorldPosition = Vector3.zero;
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

        //  this.transform.position = new Vector3 (this.transform.position.x, PosY, this.transform.position.z);

        //if (this.m_TuioManager.IsConnected)
        if (this.m_TuioManager.IsConnected&& this.m_TuioManager.IsMarkerAlive(this.MarkerID0) && this.m_TuioManager.IsMarkerAlive(this.MarkerID1) && this.m_TuioManager.IsMarkerAlive(this.MarkerID2) && this.m_TuioManager.IsMarkerAlive(this.MarkerID3))
        {

            string s = "aktiv:";
            int counter = 0;
            float posX = 0;
            float x = this.transform.position.x;

            if (this.m_TuioManager.IsMarkerAlive(this.MarkerID0))
            {
                TUIO.TuioObject marker0 = this.m_TuioManager.GetMarker(0);
                posX += marker0.getX();
                s += this.MarkerID0 + " posX:" + marker0.getX() + "+";

                counter++;
            }
            if (this.m_TuioManager.IsMarkerAlive(this.MarkerID1))
            {
                TUIO.TuioObject marker1 = this.m_TuioManager.GetMarker(1);
                posX += marker1.getX();
                s += this.MarkerID1 + " posX:" + marker1.getX() + "+";
                counter++;
            }
            if (this.m_TuioManager.IsMarkerAlive(this.MarkerID2))
            {
                TUIO.TuioObject marker2 = this.m_TuioManager.GetMarker(2);
                posX += marker2.getX();
                s += this.MarkerID2 + " posX:" + marker2.getX() + "+";
                counter++;
            }
            if (this.m_TuioManager.IsMarkerAlive(this.MarkerID3))
            {
                TUIO.TuioObject marker3 = this.m_TuioManager.GetMarker(3);
                posX += marker3.getX();
                s += this.MarkerID3 + " posX:" + marker3.getX() + "+";
                counter++;
            }
            if (counter > 0)
            {
                x = posX/counter ;
            }
                s += "=" + x;
            
            Debug.Log(s);

            if (counter > 0)
            {
                Vector3 position = new Vector3(x * Screen.width,
                    (1 - this.m_ScreenPosition.y) * Screen.height, this.CameraOffset);
                this.m_WorldPosition = this.m_MainCamera.ScreenToWorldPoint(position);
                //worldPosition += cameraOffset * mainCamera.transform.forward;
                transform.position = new Vector3(this.m_WorldPosition.x, PosY, -1);
            }
            

           // this.transform.position = new Vector3(x, this.transform.position.y, 0);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

    }

   
    /// <summary>
    ///Collider, der erkennt ob Cursor und Button
    /// </summary>
    /// <param name="col">Collider</param>
    void OnTriggerEnter2D(Collider2D col)
    {
        PlingSound.Play();
        if (col.gameObject.name == "Start")
        {
            SceneManager.LoadScene("Startspiel");
        }
        else if (col.gameObject.name == "Ende")
        {
            Application.Quit();
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
    ///Bewegung des Cursors, allerdings nur auf der x-Achse
    /// </summary>
    private void UpdateTransform()
    {
        //position mapping
        
            //calculate world position with respect to camera view direction
            float xPos = this.m_ScreenPosition.x;
            if (this.InvertX) xPos = 1 - xPos;

            if (this.m_ControlsGUIElement)
            {
                transform.position = new Vector3(xPos, 1 - PosY, 0);
            }
            else
            {
                Vector3 position = new Vector3(xPos * Screen.width,
                    this.transform.position.y, this.CameraOffset);
                this.m_WorldPosition = this.m_MainCamera.ScreenToWorldPoint(position);
                transform.position = new Vector3(this.m_WorldPosition.x, PosY, this.m_WorldPosition.z);
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
   
    #endregion
}

