using UnityEngine;

/// <summary>  
///  Diese Klasse steuert die  Funktion eines  Apfels
/// </summary> 

public class AppleScript : MonoBehaviour
{

    /// <summary>
    ///Erstellt eine Instanz der Manager-Klasse 
    /// </summary>
    private ApfelManager ms_Instance;

    /// <summary>
    ///Geschwindigkeit, mit der der Apfel fällt
    /// </summary>
    public float FallingSpeed = 0.23f; 

    /// <summary>
    ///Boolscher Wert, ob Apfel bereits gefallen ist 
    /// </summary>
    private bool Fallen = false; 

    /// <summary>
    ///Boolscher Wert, ob Apfel bereits reif ist 
    /// </summary>
    private bool Red = false; 

    /// <summary>
    ///Boolscher Wert, ob Apfel bereits gewachsen ist 
    /// </summary>
    private bool Grown = false; 

    /// <summary>
    ///Nummer des zu ladenden Apfel-Bildes (ändert sich je nach Reifegrad) 
    /// </summary>
    private float AppleNumber = 0;

    


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
        if (Red&&Grown) {
            gameObject.GetComponent<Rigidbody2D>().gravityScale = this.FallingSpeed; //Schwerkraft auf Fallgeschwindigkeit setzen
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
                Grown = true;
            }
        }
    }


    /// <summary>
    ///Lässt den Apfel reifen 
    /// </summary>
    /// <param name="sunDuration">Dauer, die die Sonne bereits scheint</param>
    public void RipingApple(float sunDuration)
    {
        if (AppleNumber + 1 == sunDuration && sunDuration <= 5) //checkt, ob das nächste Bild auch wirklich einen Apfel zeigt, der reifer ist, als der vorhergehende
        {
            AppleNumber = sunDuration;
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("apfel_" + AppleNumber, typeof(Sprite)) as Sprite; //Setzt neues (reiferes) Bild
            
            if(AppleNumber == 5)
            {
                Red = true;
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
        this.Fallen = fallen;
    }

    /// <summary>
    /// Gibt an, ob Apfel bereits gefallen ist
    /// </summary>
    /// <returns>Bool, ob Apfel bereits gefallen</returns>
    public bool GetFallen()
    {
        return this.Fallen;
    }

    #endregion
}
