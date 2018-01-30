using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Klasse steuert den Coundown zum automatischen Abschalten des Spiels
/// </summary>

public class Timer : MonoBehaviour
{


    /// <summary>
    ///Erstellt eine Instanz der Manager-Klasse
    /// </summary>
    private ManagerKlasse Manager;

    /// <summary>
    /// Speichert die Länge des Countdowns
    /// </summary>
    private float ResetTime;

    /// <summary>
    /// Countdown-Dauer
    /// </summary>
    public float Countdown = 60.0f;

    /// <summary>
    /// Breite der Wolke
    /// </summary>
    public float width = 1;

    /// <summary>
    /// Höhe der Wolke
    /// </summary>
    public float height = 1;

    /// <summary>
    /// bool, ob Wolke gezeigt werden soll
    /// </summary>
    public bool ShowCloud = true;

    /// <summary>
    /// Z-Achse der Position der Wolke
    /// </summary>
    public float AxeZ = -3f;

    /// <summary>
    /// Bool, ob Countdown unterbrochen wurde
    /// </summary>
    private bool paused = true;

    /// <summary>
    /// 
    /// </summary>
    Text ausgaben;

    /// <summary>
    /// Auszugebender Text
    /// </summary>
    public Text Ausgabe;

    /// <summary>
    /// TuioManager
    /// </summary>
    UniducialLibrary.TuioManager m_TuioManager;

    /// <summary>
    /// Zeit, die Warnung eingeblendet wird
    /// </summary>
    private float TimeToWarn;

    /// <summary>
    /// Wartezeit, bis Warnung eingeblendet wird
    /// </summary>
    public float WaitingTime = 10;

    /// <summary>
    /// Spielobjekt Wolke
    /// </summary>
    GameObject Cloud;

    /// <summary>
    /// Bool, ob es sich bei aktiver Szene um Opening Szene handelt
    /// </summary>
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


        Cloud = new GameObject("Cloud");
        SpriteRenderer renderer = Cloud.AddComponent<SpriteRenderer>();
        GUITexture te = Cloud.AddComponent<GUITexture>();
        Cloud.GetComponent<SpriteRenderer>().sprite = Resources.Load("Cloud", typeof(Sprite)) as Sprite;
        Cloud.transform.position = new Vector3(Ausgabe.rectTransform.position.x, Ausgabe.rectTransform.position.y, AxeZ);
        //Ausgabe.rectTransform.position = new Vector3(0, 0, 0);
        Cloud.transform.localScale = new Vector3(width, height, 0);
        Cloud.SetActive(false);
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
                this.PrintTimer(Countdown);

                if (ShowCloud)
                {
                    Cloud.SetActive(true);
                }
            }
            if (Countdown <= 0.0f || Input.GetKeyDown(KeyCode.A))
            {

                TimerEnded();
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

    /// <summary>
    /// Startet den Timer
    /// </summary>
    /// <param name="duration">Dauer des Timers</param>
    public void StartTimer(float duration)
    {
        ResetTime = duration;
        //Countdown = duration;
        TimeToWarn = duration - WaitingTime;
        paused = false;
    }

    /// <summary>
    /// Setzt den Timer zurück
    /// </summary>
    public void ResetTimer()
    {
        paused = true;
        Ausgabe.text = "";
        Countdown = ResetTime;
        Cloud.SetActive(false);

    }

    /// <summary>
    /// Gibt Bool zurück, ob Warnung gerade läuft
    /// </summary>
    /// <returns>Bool, ob Warnung gerade läuft</returns>
    public bool IsRunning()
    {
        return !paused;
    }

    /// <summary>
    /// Methode reguliert, was bei Ablaufen des Timers passiert
    /// </summary>
    void TimerEnded()
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


    /// <summary>
    /// Gibt den Warnungs-Text aus
    /// </summary>
    /// <param name="time">Aktuelle Countdown Zeit</param>
    private void PrintTimer(float time)
    {
        int IntTime = (int)time;
        Ausgabe.text = "Das Spiel endet in " + (int)time + " Sekunden, wenn kein Fiducial auf dem Tisch steht!";
        Cloud.GetComponent<GUITexture>().enabled = true;
    }
}