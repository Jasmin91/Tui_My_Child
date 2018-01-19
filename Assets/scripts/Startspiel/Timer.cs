using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


/// <summary>
/// Steuert die Funktionen des Countdowbs, bevor das Spiel automatisch abschält
/// </summary>
public class Timer : MonoBehaviour
{


    /// <summary>
    ///Erstellt eine Instanz der Manager-Klasse
    /// </summary>
    private ManagerKlasse Manager;
    /// <summary>
    /// Speichert die Zeit, um den Timer resetten zu können
    /// </summary>
    private float ResetTime;
    /// <summary>
    /// Zeit, die Countdown dauern soll
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
    /// Bool, ob Wolke gezeigt werden soll
    /// </summary>
    public bool ShowCloud = true;

    /// <summary>
    /// Bestimmt die xAchse der Wolke
    /// </summary>
    public float AxeZ = -3f;

    /// <summary>
    /// Bool, ob Timer resetted wurde
    /// </summary>
    private bool resetted = true;


    Text ausgaben;
    public Text Ausgabe;
    UniducialLibrary.TuioManager m_TuioManager;
    private float TimeToWarn;
    public float WaitingTime = 10;
    GameObject Cloud;
    public bool Opening = false;


    void Awake()
    {
        this.m_TuioManager = UniducialLibrary.TuioManager.Instance;
        this.m_TuioManager.Connect();
        this.Manager = ManagerKlasse.Instance;
        Manager.SetTimer(this);
        Ausgabe.text = "";


        Cloud = new GameObject("Cloud");
        SpriteRenderer renderer = Cloud.AddComponent<SpriteRenderer>();
        GUITexture te = Cloud.AddComponent<GUITexture>();
        Cloud.GetComponent<SpriteRenderer>().sprite = Resources.Load("Cloud", typeof(Sprite)) as Sprite;
        Cloud.transform.position = new Vector3(Ausgabe.rectTransform.position.x, Ausgabe.rectTransform.position.y, AxeZ);
        Cloud.transform.localScale = new Vector3(width, height, 0);
        Cloud.SetActive(false);
    }

        void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (!resetted)
        {
            Countdown -= Time.deltaTime;

            Debug.Log((int)Countdown + "<=" + (int)TimeToWarn + "=" + true);
            if (Countdown <= TimeToWarn)
            {
                this.PrintTimer(Countdown);

                if (ShowCloud)
                {
                    Cloud.SetActive(true);
                }
            }
            if (Countdown <= 0.0f)
            {
                TimerEnded();
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
    /// Startet den Countdown
    /// </summary>
    /// <param name="duration">Dauer, die Countdown runterzählen soll</param>
    public void StartTimer(float duration)
    {
        ResetTime = duration;
        Countdown = duration;
        TimeToWarn = duration - WaitingTime;
        resetted = false;
    }

    /// <summary>
    /// Setzt den Countdown zurück
    /// </summary>
    public void ResetTimer()
    {
        resetted = true;
        Ausgabe.text = "";
        Countdown = ResetTime;
        Cloud.SetActive(false);

    }

    /// <summary>
    /// Bool, ob Countdown gerade abläuft
    /// </summary>
    /// <returns>Bool, ob Countdown gerade abläuft</returns>
    public bool IsRunning()
    {
        return !resetted;
    }

    /// <summary>
    /// Methode definiert, was passiert, wenn der Timer abgelaufen ist, Spiel beenden oder zu Startbildschirm zurück 
    /// </summary>
    void TimerEnded()
    {
        resetted = true;
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
    /// Gibt die Countdown-Zeit zusammen mit einem Beschreibungstext aus
    /// </summary>
    /// <param name="time">Auszugebende Zeit</param>
    private void PrintTimer(float time)
    {
        int IntTime = (int) time;
        Ausgabe.text = "Das Spiel endet in " + (int)time + " Sekunden, wenn kein Fiducial auf dem Tisch steht!";
        Cloud.GetComponent<GUITexture>().enabled = true;
    }
}