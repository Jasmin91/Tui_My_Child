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
    public float Countdown = 60.0f;
    public float width = 1;
    public float height = 1;
    public bool ShowCloud = true;
    public float AxeZ = -3f;
    private bool paused = true;
    Text ausgaben;
    public Text Ausgabe;
    UniducialLibrary.TuioManager m_TuioManager;
    private float TimeToWarn;
    public float WaitingTime = 10;
    GameObject go;
    public bool Opening = false;


    void Awake()
    {
        this.m_TuioManager = UniducialLibrary.TuioManager.Instance;
        TimeToWarn = Countdown - WaitingTime;
        ResetTime = Countdown;
        this.m_TuioManager.Connect();
        this.Manager = ManagerKlasse.Instance;
        Manager.SetTimer(this);
        Ausgabe.text = "";


        go = new GameObject("Cloud");
        SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
        GUITexture te = go.AddComponent<GUITexture>();
        go.GetComponent<SpriteRenderer>().sprite = Resources.Load("Cloud", typeof(Sprite)) as Sprite;
        go.transform.position = new Vector3(Ausgabe.rectTransform.position.x, Ausgabe.rectTransform.position.y, AxeZ);
        //Ausgabe.rectTransform.position = new Vector3(0, 0, 0);
        go.transform.localScale = new Vector3(width, height, 0);
        go.SetActive(false);
    }

        void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (!paused)
        {
            Countdown -= Time.deltaTime;
           
            if (Countdown <= TimeToWarn)
            {
                this.printTimer(Countdown);

                if (ShowCloud)
                {
                    go.SetActive(true);
                }
            }
            if (Countdown <= 0.0f || Input.GetKeyDown(KeyCode.A))
            {

                timerEnded();
                Manager.GetTimer().ResetTimer();
            }
        }

        int[] IDs = this.Manager.GetAllIDs();
        if (!IsRunning() && !this.m_TuioManager.IsMarkerAlive(0) && !this.m_TuioManager.IsMarkerAlive(1) && !this.m_TuioManager.IsMarkerAlive(2) && !this.m_TuioManager.IsMarkerAlive(3))
        {
            Manager.GetTimer().StartTimer(Countdown);
        }
        else if (this.m_TuioManager.IsMarkerAlive(0) || this.m_TuioManager.IsMarkerAlive(1) || this.m_TuioManager.IsMarkerAlive(2) || this.m_TuioManager.IsMarkerAlive(3))
        {
            Manager.GetTimer().ResetTimer();
        }

    }

    public void StartTimer(float duration)
    {
        ResetTime = duration;
        Countdown = duration;
        TimeToWarn = duration - WaitingTime;
        paused = false;
    }
    public void ResetTimer()
    {
        paused = true;
        Ausgabe.text = "";
        Countdown = ResetTime;
        go.SetActive(false);

    }

    public bool IsRunning()
    {
        return !paused;
    }

    void timerEnded()
    {
        paused = true;
        Manager.Reset();
        if (Opening)
        {
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene("Opening");
        }
    }

    private void printTimer(float time)
    {
        int IntTime = (int) time;
        Ausgabe.text = "Das Spiel endet in " + (int)time + " Sekunden, wenn kein Fiducial auf dem Tisch steht!";
        go.GetComponent<GUITexture>().enabled = true;
    }
}