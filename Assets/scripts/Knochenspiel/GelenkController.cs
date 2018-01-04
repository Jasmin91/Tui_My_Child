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
using System.Collections.Generic;
using UnityEngine;


//wird nicht genutzt! Dient als Kopiervorlage
public class GelenkController : MonoBehaviour
{
    public int MarkerID = 0;
    bool RightRotation = false;
    Quaternion reference;
    private KnochenManager Manager;

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
        this.Manager.addGelenk(this);


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
       // Debug.Log(this.name + " After switch:" + reference);
    }

    void Update()
    {

		if (this.m_TuioManager.IsMarkerAlive(this.MarkerID)) {
			//Debug.Log("FidcialController Zeile 110:this.m_TuioManager.IsMarkerAlive(this.MarkerID)");
		}

       // Debug.Log(this.name+": rotation: " + this.transform.rotation + " -- refereenz: " + reference);

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
            UpdateTransform();
        }
        else
        {

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
        

        if (Equal(rotation))
        {
            this.RightRotation = true;
        }
        else
        {
            this.RightRotation = false;
        }
        Debug.Log(this.name + ": " + this.RightRotation);
        
    }

    private bool Equal(Quaternion q)
    {
        bool result = false;
        if (check(q.z, reference.z))
        {
            // if (check(q.z, reference.z) && check(q.w, reference.w)){
            result = true;
        }

      //  Debug.Log(this.name + ".Equal: " + result);
        return result;
    }

    private bool check(float value, float reference)
    {
        bool result = false;

       // Debug.Log(value+" - "+ calcUpperBound(reference) + " - "+  calcLowerBound(reference));

        if (IsBetween(value, calcUpperBound(reference), calcLowerBound(reference))){
            result = true;
        }
      //  Debug.Log(this.name + ".check : " + result+ " ### " + value + " - " + calcUpperBound(reference) + " - " + calcLowerBound(reference));
       // Debug.Log(this.name + ".check : " + result);
        return result;
    }

    private bool IsBetween(float value, float upper, float lower)
    {
        bool result = false;

        if(value <= upper && value >= lower)
        {
            result = true;
        }

       // Debug.Log(this.name + ".IsBetween: " + result+ " +++ " + value+  "<="+ upper + "&&" + value +">="+ lower);
        return result;
    }

    private float calcUpperBound(float value)
    {
        float add = 0.05f;
        float result = value + add; ;

       // Debug.Log(this.name + ".calcUpperBound("+value+"): " + result);
        return result;
    }

    private float calcLowerBound(float value)
    {
        float sub = 0.05f;
        float result = value - sub; ;

       // Debug.Log(this.name + ".calcLowerBound(" + value + "): " + result);
        return result;
    }

    public bool getRightRotation()
    {
        return RightRotation;
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

