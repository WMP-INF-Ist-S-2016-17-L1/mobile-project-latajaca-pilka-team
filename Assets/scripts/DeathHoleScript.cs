using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Klasa skryptu podpiętego do fioletowych dziur
public class DeathHoleScript : MonoBehaviour {

	// Metoda wywoływana podczas najechania piłki na fioletową dziurę
	void OnTriggerEnter2D (Collider2D col)
	{
		BallControlScript.setIsDeadTrue ();
        BallControlScript.showLooserUI();
	}
}
