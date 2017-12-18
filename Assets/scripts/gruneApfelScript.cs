using System;
using System.Collections.Generic;
using UnityEngine;


public class gruneApfelScript : MonoBehaviour
{
    private Management ms_Instance;
    private bool appleVisible = true;
    private float appleNumber = 0;



    void Start()
    {
        // tuioManager = UniducialLibrary.TuioManager.Instance;
        this.ms_Instance = Management.Instance;
        gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 1f);
        Debug.Log("Initiale Apfel-größe:" + gameObject.transform.localScale.x);
    }

    void Update()
    {
        
        if (this.ms_Instance.Regen.IsVisible)
        {
            Debug.Log("Apfel sieht Regen.");
            this.GrowingApple();
        }

        if (this.ms_Instance.Sonne.IsVisible)
        {
            Debug.Log("Apfel sieht Sonne.");
            this.RipingApple();
        }

        if (this.ms_Instance.getHarvestingReady()&&appleVisible)
        {
            this.HideApple();
        }
    }

    void ShowgApple()
    {


        //if (gameObject.GetComponent<Renderer>() != null && !gameObject.GetComponent<Renderer>().enabled)
        
            gameObject.GetComponent<Renderer>().enabled = true;
        appleVisible = true;

            
        

    }

    void HideApple()
    {


        //set 3d game object to visible, if it was hidden before
        //if (gameObject.GetComponent<Renderer>() != null && gameObject.GetComponent<Renderer>().enabled)
        {
            gameObject.GetComponent<Renderer>().enabled = false;
            appleVisible = false;
            Debug.Log("Der Apfel verschwindet");
        }

    }

    void RipingApple()
    {
        if (appleNumber+1 == ms_Instance.Sonne.sunDuration)
        {
            appleNumber = ms_Instance.Sonne.sunDuration;
        }
        Debug.Log("Suche nach Apfel_" + appleNumber);
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("apfel_" + appleNumber, typeof(Sprite)) as Sprite;
    }
    void GrowingApple()
    {
        
        float oldSize = gameObject.transform.localScale.x;
        Debug.Log("oldSize:"+oldSize);

        if (ms_Instance.getRainDuration() < 5 && ms_Instance.getRainDuration() > 0)
        {
            float size = 0.1f + (0.1f - (0.02f * (ms_Instance.getRainDuration())));
            Debug.Log("Formel: 0.1f + (0.1f - (0.02f * ("+ms_Instance.getRainDuration()+")))");
            if (size > oldSize&&size<=0.2f)
            {
                gameObject.transform.localScale = new Vector3(size, size, 1f);
                Debug.Log("Der Apfel wächst." + size);
            }
        }
    }
}
