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
using UnityEngine;
[System.Serializable]

/// <summary>  
///Klasse um die Tiere zu steuern
/// </summary>  
public class AnimalController : MonoBehaviour
{
    public int MarkerID = 0;
    private ManagerKlasse Manager; //Erstellt eine Instanz der Manager-Klasse
    Talk Sprechblase;

    public enum RotationAxis { Forward, Back, Up, Down, Left, Right };
    public float Geschwindigkeit = 0.01f;

    //translation
    public bool IsPositionMapped = false;
    public bool InvertX = false;
    public bool InvertY = false;

    //rotation
    public bool AutoHideGO = false;
    private bool m_ControlsGUIElement = false;


    public float CameraOffset = 10;
    public RotationAxis RotateAround = RotationAxis.Back;
    private UniducialLibrary.TuioManager m_TuioManager;
	private ApfelManager ms_Instance;
    private Camera m_MainCamera;

    //members
    private float m_Angle;
    private float m_AngleDegrees;

    public float RotationMultiplier = 1;

    void Awake()
    {

        this.Manager = ManagerKlasse.Instance;
        Manager.AddAnimal(this);
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
        
        this.m_Angle = 0f;
        this.m_AngleDegrees = 0;
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
		}


        if (this.m_TuioManager.IsConnected
            && this.m_TuioManager.IsMarkerAlive(this.MarkerID))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0.5f, 0);//Setze Geschwindigkeit, sobald Fiducial sichtbar
            TUIO.TuioObject marker = this.m_TuioManager.GetMarker(this.MarkerID);

            //update parameters
            this.m_Angle = marker.getAngle() * RotationMultiplier;
            this.m_AngleDegrees = marker.getAngleDegrees() * RotationMultiplier;


            //update transform component
            UpdateTransform();
        }
        else
        {
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);//Setze Geschwindigkeit auf 0, sobald Fiducial unsichtbar
               
            }
            
        }
    }


    void OnApplicationQuit()
    {
        if (this.m_TuioManager.IsConnected)
        {
            this.m_TuioManager.Disconnect();
        }
    }

    private void UpdateTransform()
    {
     
      
        {
            Quaternion rotation = Quaternion.identity;
            

            switch (this.RotateAround)
            {
                case RotationAxis.Forward:
                    rotation = Quaternion.AngleAxis(this.m_AngleDegrees, Vector3.forward);
                  //  GetComponent<Rigidbody2D>().velocity = Vector3.forward;
                    break;
                case RotationAxis.Back:
                    rotation = Quaternion.AngleAxis(this.m_AngleDegrees, Vector3.back);
                  //  GetComponent<Rigidbody2D>().velocity = Vector3.back;
                    break;
                case RotationAxis.Up:
                    rotation = Quaternion.AngleAxis(this.m_AngleDegrees, Vector3.up);
                   // GetComponent<Rigidbody2D>().velocity = Vector3.up;
                    break;
                case RotationAxis.Down:
                    rotation = Quaternion.AngleAxis(this.m_AngleDegrees, Vector3.down);
                   // GetComponent<Rigidbody2D>().velocity = Vector3.down;
                    break;
                case RotationAxis.Left:
                    rotation = Quaternion.AngleAxis(this.m_AngleDegrees, Vector3.left);
                   // GetComponent<Rigidbody2D>().velocity = Vector3.left;
                    break;
                case RotationAxis.Right:
                    rotation = Quaternion.AngleAxis(this.m_AngleDegrees, Vector3.right);
                    
                    break;
            }

            transform.localRotation = rotation; //Rotiert das Tier
            float Geschwindigkeit = this.Geschwindigkeit; //Setzt die Geschwindigkeit
          
            if (transform.right.x < -0.5) {
                Geschwindigkeit = Geschwindigkeit * 2; //Erhöht Geschwindigkeit, wenn sich Tier nach links bewegt (Ausgleich)
            }
            transform.position += transform.right * Geschwindigkeit; //Bewegt das Tier
            
        }
    }


    public void Speak(String s)
    {
        this.Sprechblase.DisplayText(s);

    }

    public void Speak(String s, float x)
    {
        this.Sprechblase.DisplayText(s,x);

    }

    public void BeSilent()
    {
        this.Sprechblase.Hide();
    }

    public void setSpeaker(Talk speaker)
    {
        this.Sprechblase = speaker;
    }

    

    #region Getter

    public bool isAttachedToGUIComponent()
    {
        return (gameObject.GetComponent<GUIText>() != null || gameObject.GetComponent<GUITexture>() != null);
    }
 
    public float Angle
    {
        get { return this.m_Angle; }
    }
    public float AngleDegrees
    {
        get { return this.m_AngleDegrees; }
    }

    #endregion
}

