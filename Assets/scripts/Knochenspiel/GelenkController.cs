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
/// Kontrolliert die Drehung der Gelenke
/// </summary>
public class GelenkController : MonoBehaviour
{
    public int MarkerID = 0;
    bool RightRotation = false;

    /// <summary>
    /// Ziel-Drehung des Gelenkes
    /// </summary>
    private Quaternion reference;

    /// <summary>
    /// Instanz des Knochen-Managers
    /// </summary>
    private KnochenManager Manager;

    /// <summary>
    /// Bool, ob rotiert werden soll
    /// </summary>
    private bool rotate = true;

    /// <summary>
    /// Wert, um welchen Drehung abweichen darf
    /// </summary>
    private float deviation = 0.05f;

    public enum RotationAxis { Forward, Back, Up, Down, Left, Right };

    //translation

    //rotation
    private bool m_ControlsGUIElement = false;


    public float CameraOffset = 10;
    public RotationAxis RotateAround = RotationAxis.Back;
    private UniducialLibrary.TuioManager m_TuioManager;
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
    private bool m_IsVisible;

    public float RotationMultiplier = 1;

    void Awake()
    {
        this.m_TuioManager = UniducialLibrary.TuioManager.Instance;
        //uncomment next line to set port explicitly (default is 3333)
        //m_TuioManager.TuioPort = 7777;

        this.m_TuioManager.Connect();
        this.Manager = KnochenManager.Instance;
        this.Manager.AddGelenk(this);


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


        //Setzt je nach Gelenk den richtigen Zielwert
        switch (this.name)
        {
            case "red":
                reference = new Quaternion(0.0f, 0.0f, 0.0f, -1.0f);
                break;
            case "blue":
                reference = new Quaternion(0.0f, 0.0f, -1.0f, 0.0f);
                break;
            case "green":
                reference = new Quaternion(0.0f, 0.0f, 0.0f, -1.0f);
                break;
            case "yellow":
                reference = new Quaternion(0.0f, 0.0f, 0.0f, -1.0f);
                break;

        }
    }

    void Update()
    {

		if (this.m_TuioManager.IsMarkerAlive(this.MarkerID)) {
		}
        

        if (this.m_TuioManager.IsConnected
            && this.m_TuioManager.IsMarkerAlive(this.MarkerID))
        {
            TUIO.TuioObject marker = this.m_TuioManager.GetMarker(this.MarkerID);

            //update parameters
            this.m_Angle = marker.getAngle() * RotationMultiplier;
            this.m_AngleDegrees = marker.getAngleDegrees() * RotationMultiplier;
            this.m_Acceleration = marker.getMotionAccel();
            this.m_RotationSpeed = marker.getRotationSpeed() * RotationMultiplier;
            this.m_RotationAcceleration = marker.getRotationAccel();
            this.m_Direction.x = marker.getXSpeed();
            this.m_Direction.y = marker.getYSpeed();


            //update transform component
            if (rotate)
            {
                UpdateTransform();
            }
        }
        else
        {

            this.m_IsVisible = false;
        }

        if (this.Manager.GetRightWayFound())
        {
            this.rotate = false; //Lässt Gelenk nur drehen, solang der richtige Weg noch nicht gefunden wurde
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

        //rotation mapping
        
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
            transform.localRotation = rotation;
        

        //Überprüft, ob die Rotation mit Zielrotation übereinstimmt
        if (Equal(rotation))
        {
            this.RightRotation = true;
        }
        else
        {
            this.RightRotation = false;
        }
        
    }

    /// <summary>
    /// Vergleich aktuelle Rotation mit Zielrotation (hier nur z-Wert relevant)
    /// </summary>
    /// <param name="q">Abzugleichender, aktuellert Wert</param>
    /// <returns>Bool, ob übereinstimmend</returns>
    private bool Equal(Quaternion q)
    {
        bool result = false;
        if (Check(q.z, reference.z))
        {
            result = true;
        }
        
        return result;
    }

    /// <summary>
    /// Prüft, ob Wert ungefähr dem Zielwert entspricht
    /// </summary>
    /// <param name="value">Zu überprüfender Wert</param>
    /// <param name="reference">Zielwert</param>
    /// <returns></returns>
    private bool Check(float value, float reference)
    {
        bool result = false;
        

        if (IsBetween(value, CalcUpperBound(reference), CalcLowerBound(reference))){
            result = true;
        }
        return result;
    }

    /// <summary>
    /// Prüft ob Wert zwischen zwei Werten lieg
    /// </summary>
    /// <param name="value">Zu überprüfender Wert</param>
    /// <param name="upper">Oberer Grenzwert</param>
    /// <param name="lower">Unterer Grenzwert</param>
    /// <returns></returns>
    private bool IsBetween(float value, float upper, float lower)
    {
        bool result = false;

        if(value <= upper && value >= lower)
        {
            result = true;
        }
        
        return result;
    }

    /// <summary>
    /// Berechnet oberen Grenzwert
    /// </summary>
    /// <param name="value">Ursprungswert</param>
    /// <returns>Berechnete Obergrenze</returns>
    private float CalcUpperBound(float value)
    {
        float add = deviation;
        float result = value + add; ;
        return result;
    }

    /// <summary>
    /// Berechnet unteren Grenzwert
    /// </summary>
    /// <param name="value">Ursprungswert</param>
    /// <returns>Berechnete Untergrenze</returns>
    private float CalcLowerBound(float value)
    {
        float sub = deviation;
        float result = value - sub; ;
        return result;
    }



    #region Getter

    /// <summary>
    /// Getter, ob richtige Drehung gefunden wurde
    /// </summary>
    /// <returns>Bool, ob richtige Drehung gefunden wurde</returns>
    public bool GetRightRotation()
    {
        return RightRotation;
    }
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

