using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Klasse steuert den Coundown um Hinweise zum Ausschalten des Spiels zu geben
/// </summary>

public class Hint : MonoBehaviour
{
    

    /// <summary>
    /// Speichert die Länge des Countdowns
    /// </summary>
    private float ResetTime;

    /// <summary>
    /// Countdown-Dauer
    /// </summary>
    public float Countdown = 60.0f;
  
    /// <summary>
    /// Bool, ob Countdown unterbrochen wurde
    /// </summary>
    private bool paused = true;

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
    public float TimeToWarn;

    /// <summary>
    /// Wartezeit, bis Warnung eingeblendet wird
    /// </summary>
    private float WaitingTime = 10;

    /// <summary>
    /// Anzuzeigender Text
    /// </summary>
    string text = "";


    void Awake()
    {
        this.m_TuioManager = UniducialLibrary.TuioManager.Instance;
        TimeToWarn = Countdown - WaitingTime;
        ResetTime = Countdown;
        this.m_TuioManager.Connect();
        text = Ausgabe.text; //Holt Text aus Textelement
        Ausgabe.text = "";
       
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
                this.PrintHint();
                
            }
            if (Countdown <= 0.0f || Input.GetKeyDown(KeyCode.A))
            {
                ResetTimer();
            }
        }
        
        if  (!this.m_TuioManager.IsMarkerAlive(0) && !this.m_TuioManager.IsMarkerAlive(1) && !this.m_TuioManager.IsMarkerAlive(2) && !this.m_TuioManager.IsMarkerAlive(3))
        {
            Debug.Log("reset");
            this.ResetTimer();
        }
        else if (!IsRunning() && this.m_TuioManager.IsMarkerAlive(0) || this.m_TuioManager.IsMarkerAlive(1) || this.m_TuioManager.IsMarkerAlive(2) || this.m_TuioManager.IsMarkerAlive(3))
        {
            this.StartTimer(Countdown);
        }
    
    }

    /// <summary>
    /// Startet den Timer
    /// </summary>
    /// <param name="duration">Dauer des Timers</param>
    public void StartTimer(float duration)
    {
        Countdown = duration;
        paused = false;
    }

    /// <summary>
    /// Setzt den Timer zurück
    /// </summary>
    public void ResetTimer()
    {
        paused = true;
        Debug.Log("reset");
        Ausgabe.text = "";
        Countdown = ResetTime;

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
        Countdown = ResetTime;
    }


    /// <summary>
    /// Gibt den Warnungs-Text aus
    /// </summary>
    /// <param name="time">Aktuelle Countdown Zeit</param>
    private void PrintHint()
    {

        Ausgabe.text = text;

    }
}