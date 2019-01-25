using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Klasa skryptu podpiętego do błękitnej dziury
public class ExitHoleScript : MonoBehaviour {

	// Metoda wywoływana podczas najechania piłki na błękitną dziurę
	void OnTriggerEnter2D (Collider2D col)
	{
		BallControlScript.setYouWinToTrue ();
        BallControlScript.showWinnerUI();
	}
}
