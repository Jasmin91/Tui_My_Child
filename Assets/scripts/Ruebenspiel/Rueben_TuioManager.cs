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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TUIO;
namespace UniducialLibrary
{
    public class Rueben_TuioManager : TuioListener
    {

        // Singleton Instance
        private static Rueben_TuioManager rm_Instance;
        private TuioClient rm_Client = null;
        private List<TuioObject> rm_TUIOObjects;

        public static Rueben_TuioManager Instance
        {
            get
            {
                if (rm_Instance == null)
                {
                    rm_Instance = new Rueben_TuioManager();
                }

                return rm_Instance;
            }
        }




        public Rueben_TuioManager()
        {
            if (rm_Instance != null)
            {
                Debug.LogError("Trying to create two instances of singleton.");
                return;
            }

            rm_Instance = this;

            rm_Client = new TuioClient();
            rm_Client.addTuioListener(this);

            //init members
            this.rm_TUIOObjects = new List<TuioObject>();


        }

        ~Rueben_TuioManager()
        {
            Disconnect();
        } 

        #region TUIOListener methods

        void TuioListener.addTuioObject(TuioObject in_TUIOObject)
        {
            this.rm_TUIOObjects.Add(in_TUIOObject);
        }

        void TuioListener.updateTuioObject(TuioObject tobj)
        {

        }

        void TuioListener.removeTuioObject(TuioObject in_TUIOObject)
        {
            this.rm_TUIOObjects.Remove(in_TUIOObject);
        }

        void TuioListener.addTuioCursor(TuioCursor tcur)
        {
            // throw new System.NotImplementedException();
        }

        void TuioListener.updateTuioCursor(TuioCursor tcur)
        {
            //throw new System.NotImplementedException();
        }

        void TuioListener.removeTuioCursor(TuioCursor tcur)
        {
            //throw new System.NotImplementedException();
        }

        void TuioListener.refresh(TuioTime ftime)
        {
            //throw new System.NotImplementedException();
        }
        #endregion

        public void Connect()
        {
            //setup TUIO client connection
            rm_Client.connect();

            if (this.rm_Client.isConnected())
            {

                Debug.Log("Listening to TUIO port " + rm_Client.getPort() + ".");
            }
            else
            {
                Debug.LogError("Failed to connect to TUIO port " + rm_Client.getPort() + ".");
            }
        }

        public bool IsMarkerAlive(int in_MarkerID)
        {

            foreach (TuioObject tuioObject in this.rm_TUIOObjects)
            {
                if (tuioObject.getSymbolID() == in_MarkerID)
                {
                    return true;
                }
            }

            return false;
        }


        public TuioObject GetMarker(int in_MarkerID)
        {
            foreach (TuioObject tuioObject in this.rm_TUIOObjects)
            {
                if (tuioObject.getSymbolID() == in_MarkerID)
                {
                    return tuioObject;
                }

            }

            return null;
        }

        public int GetObjectCount()
        {
            return this.rm_TUIOObjects.Count;
        }

        public void Disconnect()
        {
            if (this.rm_Client.isConnected())
            {
                int port = rm_Client.getPort();
                rm_Client.removeTuioListener(this);
                rm_Client.disconnect();
                Debug.Log("Stopped listening to TUIO port " + port + ".");
            }
        }

        public bool IsConnected
        {
            get { return this.rm_Client.isConnected(); }
        }

        public int TuioPort
        {
            get { return rm_Client.getPort(); }
            set { rm_Client.setPort(value); }
        }
    }
}