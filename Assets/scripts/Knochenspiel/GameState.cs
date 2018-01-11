using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>  
///  Diese Klasse lädt das Startspiel nach beenden des Spiels
/// </summary> 


public class GameState : MonoBehaviour
{
    
    /// <summary>
    /// Beendet das Spiel
    /// </summary>
    private void FinishGame()
    {
        SceneManager.LoadScene("Startspiel");
    }
    
}
