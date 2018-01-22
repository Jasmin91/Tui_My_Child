using UnityEngine;

/// <summary>  
///  Diese Klasse steuert die  Funktion eines  Apfels
/// </summary> 

public class appleScript : MonoBehaviour
{

    /// <summary>
    ///Erstellt eine Instanz der Manager-Klasse 
    /// </summary>
    private ApfelManager ms_Instance;

    /// <summary>
    ///Geschwindigkeit, mit der der Apfel fällt
    /// </summary>
    public float fallingSpeed = 0.23f; 

    /// <summary>
    ///Boolscher Wert, ob Apfel bereits gefallen ist 
    /// </summary>
    private bool fallen = false; 

    /// <summary>
    ///Boolscher Wert, ob Apfel bereits reif ist 
    /// </summary>
    private bool red = false; 

    /// <summary>
    ///Boolscher Wert, ob Apfel bereits gewachsen ist 
    /// </summary>
    private bool grown = false; 

    /// <summary>
    ///Nummer des zu ladenden Apfel-Bildes (ändert sich je nach Reifegrad) 
    /// </summary>
    private float appleNumber = 0;

    


    void Update()
    {
        Debug.Log(this.name + ": I'm alive!");
    }


    void Start()
    {
        this.ms_Instance = ApfelManager.Instance; 
        this.ms_Instance.AddApple(this); //Füge Apfel dem Apfel-Array im Manager hinzu
        
    }
    

    /// <summary>
    /// Zeigt Apfel
    /// </summary>
    void ShowApple()
    {
        gameObject.GetComponent<Renderer>().enabled = true; //Apfel-Objekt anzeigen
       
    }
    
    /// <summary>
    /// Markiert einen Apfel als bereit zur Ernte, wenn er reif ist
    /// </summary>
    public void FallableApple()
    {
        if (red&&grown) {
            gameObject.GetComponent<Rigidbody2D>().gravityScale = this.fallingSpeed; //Schwerkraft auf Fallgeschwindigkeit setzen
            this.SetFallen(true);
        }
      }

    /// <summary>
    ///Lässt den Apfel wachsen 
    /// </summary>
    /// <param name="rainDuration">Dauer, die es bereits regnet</param>

    public void GrowingApple(float rainDuration)
    {
        float oldSize = gameObject.transform.localScale.x; //Alte Größe des Apfels
       
        if (rainDuration <= 5 && rainDuration > 0)
        {
            float size = 0.1f + (0.02f * (rainDuration)); //Berechnen der neuen Größe
            if (size > oldSize && size <= 0.2f) //checkt, ob neue Größe wirklich größer, als die alte
            {
                if (size == 0.09f)
                {
                    size = 0.2f;
                }
                gameObject.transform.localScale = new Vector3(size, size, 1f); //Setzt die neue Größe/

            }
            if (size >= 0.2f)
            {
                grown = true;
            }
        }
    }


    /// <summary>
    ///Lässt den Apfel reifen 
    /// </summary>
    /// <param name="sunDuration">Dauer, die die Sonne bereits scheint</param>
    public void RipingApple(float sunDuration)
    {
        if (appleNumber + 1 == sunDuration && sunDuration <= 5) //checkt, ob das nächste Bild auch wirklich einen Apfel zeigt, der reifer ist, als der vorhergehende
        {
            appleNumber = sunDuration;
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("apfel_" + appleNumber, typeof(Sprite)) as Sprite; //Setzt neues (reiferes) Bild
            
            if(appleNumber == 5)
            {
                red = true;
            }
        }
    }

#region Getter&Setter

/// <summary>
/// Setzt den Wert, ob Apfel bereits gefallen 
/// </summary>
/// <param name="fallen">Wert, ob Apfel bereits gefallen</param>
public void SetFallen(bool fallen)
    {
        this.fallen = fallen;
    }

    /// <summary>
    /// Gibt an, ob Apfel bereits gefallen ist
    /// </summary>
    /// <returns>Bool, ob Apfel bereits gefallen</returns>
    public bool GetFallen()
    {
        return this.fallen;
    }

    #endregion
}
