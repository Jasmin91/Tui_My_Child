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
using System.Collections;
[System.Serializable]

/// <summary>  
///Klasse um die Tiere zu steuern
/// </summary>  
public class AnimalController : MonoBehaviour
{
    float WaitingToExit = 3;

    public int MarkerID = 0;

    /// <summary>
    ///Erstellt eine Instanz der Manager-Klasse
    /// </summary>
    private ManagerKlasse Manager;

    /// <summary>
    /// Name, der in Sprechblase angezeigt werden soll
    /// </summary>
    public String Nickname = "kein Name vergeben";

    /// <summary>
    /// Sprechblase des Tieres
    /// </summary>
    Talk Sprechblase;

    /// <summary>
    /// Bool, ob Tier bereits sein Essen hat
    /// </summary>
    private bool hasFood = false;

    /// <summary>
    /// Aktuelle Rotation des Tieres
    /// </summary>
    private Quaternion actRotation;
    
    private bool waiting = false;

    public float WaitingToMove = 1;

    public AudioSource audioo;

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
        
        this.m_TuioManager = UniducialLibrary.TuioManager.Instance;
        this.ms_Instance = ApfelManager.Instance;
        //uncomment next line to set port explicitly (default is 3333)
        //m_TuioManager.TuioPort = 7777;

        this.m_TuioManager.Connect();



        //check if the game object needs to be transformed in normalized 2d space
        if (IsAttachedToGUIComponent())
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
        this.actRotation = GetComponent<Rigidbody2D>().transform.rotation;
        Manager.AddAnimal(this);
        //check if the main camera exists
        if (this.m_MainCamera == null)
        {
            Debug.LogError("There is no main camera defined in your scene.");
        }

        waiting = true;
    }
   
    void Update()
    {

        
        if (waiting)
        {
            WaitingToMove -= Time.deltaTime;
            if (WaitingToMove <= 0.0f)
            {
                waiting = false;
            }
        }
     


        if (this.m_TuioManager.IsConnected
            && this.m_TuioManager.IsMarkerAlive(this.MarkerID)&&!waiting)
        {
           
            GetComponent<Rigidbody2D>().velocity = new Vector2(0.5f, 0);//Setze Geschwindigkeit, sobald Fiducial sichtbar
            
             TUIO.TuioObject marker = this.m_TuioManager.GetMarker(this.MarkerID);

            //update parameters
            this.m_Angle = marker.getAngle() * RotationMultiplier;
            this.m_AngleDegrees = marker.getAngleDegrees() * RotationMultiplier;


            //update transform component
            UpdateTransform();
            this.actRotation = GetComponent<Rigidbody2D>().transform.rotation;
        }
        else
        {
          
            
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);//Setze Geschwindigkeit auf 0, sobald Fiducial unsichtbar
            GetComponent<Rigidbody2D>().transform.rotation=this.actRotation;

             
		
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
                    break;
                case RotationAxis.Back:
                    rotation = Quaternion.AngleAxis(this.m_AngleDegrees, Vector3.back);
                    break;
                case RotationAxis.Up:
                    rotation = Quaternion.AngleAxis(this.m_AngleDegrees, Vector3.up);
                    break;
                case RotationAxis.Down:
                    rotation = Quaternion.AngleAxis(this.m_AngleDegrees, Vector3.down);
                    break;
                case RotationAxis.Left:
                    rotation = Quaternion.AngleAxis(this.m_AngleDegrees, Vector3.left);
                    break;
                case RotationAxis.Right:
                    rotation = Quaternion.AngleAxis(this.m_AngleDegrees, Vector3.right);
                    
                    break;
            }
            transform.localRotation = rotation; //Rotiert das Tier
            float Geschwindigkeit = this.Geschwindigkeit; //Setzt die Geschwindigkeit

           
            GetComponent<Rigidbody2D>().velocity = transform.right * Geschwindigkeit; //Bewegt das Tier
             
        }
    }

   /// <summary>
   ///
   ///</summary>

   public IEnumerator StartEndTimerFirst(float countdownValue = 0)
    {
        {
            while (countdownValue <= WaitingToExit)
            {
                countdownValue++;

                
                yield return new WaitForSeconds(1.0f);

            }
            if(countdownValue==WaitingToExit){
            Debug.Log("Spiel wird beendet...");
            }
        }
    }

   

    /// <summary>
    /// Lässt das Tier in Sprechblase "sprechen"
    /// </summary>
    /// <param name="s">Auszugebender Text</param>
    public void Speak(String s)
    {
        this.Sprechblase.DisplayText(s);

    }

    /// <summary>
    /// Lässt das Tier in Sprechblase für bestimmte Dauer "sprechen"
    /// </summary>
    /// <param name="s">Auszugebender Text</param>
    /// <param name="x">Anzuzeigende Dauer</param>
    public void Speak(String s, float x)
    {
        this.Sprechblase.DisplayText(s,x);

    }

    /// <summary>
    /// Lässt Sprechblase verschwinden 
    /// </summary>
    public void BeSilent()
    {
        this.Sprechblase.Hide();
    }



    #region Getter&Setter


    /// <summary>
    /// Setzt die Sprechblase
    /// </summary>
    /// <param name="speaker">Zuzuweisende Sprechblase</param>
    public void SetSpeaker(Talk speaker)
    {
        this.Sprechblase = speaker;
    }

    /// <summary>
    /// Getter für Nickname
    /// </summary>
    /// <returns>Nickname</returns>
    public String GetNickname()
    {
        return Nickname;
    }

  

    /// <summary>
    /// Getter, ob Tier bereits Essen hat
    /// </summary>
    /// <returns>Bool, ob Tier bereits Essen hat</returns>
    public bool GetHasFood()
    {
        return hasFood;
    }

    /// <summary>
    /// Setter, ob Tier bereits Essen hat
    /// </summary>
    /// <param name="var">Bool, ob Tier bereits Essen hat</param>
    public void SetHasFood(bool var)
    {
        this.hasFood = var;
    }

    public bool IsAttachedToGUIComponent()
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

