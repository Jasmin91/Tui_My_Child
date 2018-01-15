using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{


    /// <summary>
    ///Erstellt eine Instanz der Manager-Klasse
    /// </summary>
    private ManagerKlasse Manager;
    private float ResetTime;
    public float targetTime = 60.0f;
    public float width = 1;
    public float height = 1;
    public bool ShowCloud = true;
    private bool paused = true;
    Text ausgaben;
    public Text Ausgabe;
    UniducialLibrary.TuioManager m_TuioManager;
    private float WaitingTime;
    GameObject go;


    void Awake()
    {
        this.m_TuioManager = UniducialLibrary.TuioManager.Instance;
        WaitingTime = targetTime - 5;
        ResetTime = targetTime;
        this.m_TuioManager.Connect();
        this.Manager = ManagerKlasse.Instance;
        Manager.SetTimer(this);
        Ausgabe.text = "";


        go = new GameObject("Neu");
        SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
        GUITexture te = go.AddComponent<GUITexture>();
        go.GetComponent<SpriteRenderer>().sprite = Resources.Load("Cloud", typeof(Sprite)) as Sprite;
        go.transform.position = new Vector3(Ausgabe.rectTransform.position.x, Ausgabe.rectTransform.position.y, 0);
        //Ausgabe.rectTransform.position = new Vector3(0, 0, 0);
        go.transform.localScale = new Vector3(width, height, 0);
        go.SetActive(false);
    }

        void Update()
    {
        Debug.Log("update timer");
        if (!paused)
        {
            targetTime -= Time.deltaTime;
           
            if (targetTime <= WaitingTime)
            {
                this.printTimer(targetTime);

                if (ShowCloud)
                {
                    go.SetActive(true);
                }
            }
            if (targetTime <= 0.0f)
            {
                timerEnded();
            }
        }

        int[] IDs = this.Manager.GetAllIDs();
        if (!IsRunning() && !this.m_TuioManager.IsMarkerAlive(0) && !this.m_TuioManager.IsMarkerAlive(1) && !this.m_TuioManager.IsMarkerAlive(2) && !this.m_TuioManager.IsMarkerAlive(3))
        {
            Manager.GetTimer().StartTimer(targetTime);
        }
        else if (this.m_TuioManager.IsMarkerAlive(0) || this.m_TuioManager.IsMarkerAlive(1) || this.m_TuioManager.IsMarkerAlive(2) || this.m_TuioManager.IsMarkerAlive(3))
        {
            Manager.GetTimer().ResetTimer();
        }

    }

    public void StartTimer(float duration)
    {
        Debug.Log("Start Timer");
        ResetTime = duration;
        targetTime = duration;
        WaitingTime = duration - 5;
        paused = false;
        
        //IsRunning = true;
    }
    public void ResetTimer()
    {
        Debug.Log("Pausiere Timer");
        paused = true;
        Ausgabe.text = "";
        targetTime = ResetTime;
        go.SetActive(false);

    }

    public bool IsRunning()
    {
        return !paused;
    }

    void timerEnded()
    {
        paused = true;
        SceneManager.LoadScene("Opening");
        Debug.Log("Spiel wird beendet...");
    }

    private void printTimer(float time)
    {
        int IntTime = (int) time;
        Ausgabe.text = "Das Spiel endet in " + (int)time + " Sekunden, wenn kein Fiducial auf dem Tisch steht!";
        go.GetComponent<GUITexture>().enabled = true;
    }
}