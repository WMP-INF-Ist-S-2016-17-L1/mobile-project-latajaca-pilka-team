using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Główna klasa zawierająca logikę gry. Cały skrypt podpięty jest pod obiekt piłki w unity
public class BallControlScript : MonoBehaviour {

	// Referencja do obiektu odpowiadający za fizykę piłki
	Rigidbody2D rb;

	// Zmienna odpowiadająca za prędkość piłki (od 0.2 do 2)
	[Range(0.2f, 2f)]
	public float moveSpeedModifier = 0.5f;

	// Zmienne kierunkowe
	float dirX, dirY;

	// Referencja do animatora, aby móc zarządzać przejściami animacji piłki
	Animator anim;

	// Wartość logiczna okreslająca czy piłka "wpadła" do fioletowej dziury
	static bool isDead;

	// Wartość logiczna okreslająca czy piłka może się poruszać
	static bool moveAllowed;

	// Wartość logiczna określająca czy piłka trafiła do błękitnego otworu czy tez nie
	static bool youWin;

    // Wartości logiczne decydujące o wyświetleniu UI wygranej i przegranej
    static bool isWinnerUIShown;
    static bool isLooserUIShown;

    // Referencja do obiektu napisu "You Win!"
    [SerializeField]
	GameObject winText;

    // Referencja do obiektu napisu "You Loose!"
    [SerializeField]
    GameObject looseText;

    // Referencja do obiektu przycisku następnego poziomu
    [SerializeField]
    GameObject nextLevelButton;

    // Referencja do przycisku przejścia do głównego menu
    [SerializeField]
    GameObject mainMenuButton;

    // Referencja do przycisku powtarzania poziomu
    [SerializeField]
    GameObject restartLevelButton;
    

    // Metoda wywołana przy ładowaniu poziomów. Inicjalizuje pola
    void Start () {

		// Wyłączenie napisów na starcie
		winText.gameObject.SetActive(false);
        looseText.gameObject.SetActive(false);

		// Nie wygraliśmy na początku gry
		youWin = false;

		// Nadajemy możliwość ruchu piłce
		moveAllowed = true;

		// Nie przegraliśmy na początku gry
		isDead = false;

        // Inicjalizacja komponentów fizyki i animacji
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();

		// Wyłączamy animacje, uruchamianą przy przegranej grze
		anim.SetBool ("BallDead", isDead);
        
        // Chowamy przyciski UI
        nextLevelButton.gameObject.SetActive(false);
        restartLevelButton.gameObject.SetActive(false);
        mainMenuButton.gameObject.SetActive(false);

        // Ustawiamy wartości logiczne UI na false
        isWinnerUIShown = false;
        isLooserUIShown = false;

    }
	
	// Metoda wywoływana raz na klatkę
	void Update () {

		// Pobieramy wartość z akcelerometru
		dirX = Input.acceleration.x * moveSpeedModifier;
		dirY = Input.acceleration.y * moveSpeedModifier;

		// Jeśli przegraliśmy
		if (isDead) {

			// zatrzymujemy piłkę
			rb.velocity = new Vector2 (0, 0);

			// uruchamiamy animacje spadania 
			anim.SetBool ("BallDead", isDead);

        }

		// Jeśli wygraliśmy
		if (youWin) {

            // decyduje o załadowaniu sceny końcowej, jeśli przeszliśmy ostatni poziom
			if (SceneManager.GetActiveScene().name.Equals("Level5"))
            {
                Invoke("ChangeSceneToEnd", 2f);
            }
			

			// zatrzymujemy piłkę
			moveAllowed = false;

			// uruchamiamy animacje spadania
			anim.SetBool("BallDead", true);
            
        }
        
        // Wyświetlenie UI wygranej jeśli wartość logiczna została zmieniona i nie jest to ostatni poziom
        if (isWinnerUIShown && !SceneManager.GetActiveScene().name.Equals("Level5"))
        {
            winText.gameObject.SetActive(true);
            nextLevelButton.gameObject.SetActive(true);
            mainMenuButton.gameObject.SetActive(true);
        }

        // Wyświetlenie UI przegranej jeśli wartość logiczna zostala zmieniona
        if (isLooserUIShown)
        {
            looseText.gameObject.SetActive(true);
            restartLevelButton.gameObject.SetActive(true);
            mainMenuButton.gameObject.SetActive(true);
        }

	}

    // Metoda ładująca scene końcową (po przejsciu ostatniego poziomu)
    void ChangeSceneToEnd()
    {
        Application.LoadLevel("End");
    }

    // Metoda nadająca prędkość kulce na podstawie danych z akcelerometru
	void FixedUpdate()
	{
		if (moveAllowed)
		rb.velocity = new Vector2 (rb.velocity.x + dirX, rb.velocity.y + dirY);
	}

	// Metoda wywoływana przez DeathHoleScript.cs, po tym jak zostanie wykryte natrafienie piłki na fioletową dziurę
	public static void setIsDeadTrue()
	{
		isDead = true;
	}

	// Metoda wywoływana przez ExitHoleScript.cs, po tym jak zostanie wykryte natrafienie piłki na błękitną dziurę
	public static void setYouWinToTrue()
	{
		youWin = true;
	}

    // Metoda wywoływana przez ExitHoleScript.cs, po tym jak zostanie wykryte natrafienie piłki na błękitną dziurę
    public static void showWinnerUI()
    {
        isWinnerUIShown = true;
    }

    // Metoda do chowania interfejsu podczas gry
    public static void hideUI()
    {
        isWinnerUIShown = false;
        isLooserUIShown = false;
    }

    // Metoda wywoływana przez DeathHoleScript.cs, po tym jak zostanie wykryte natrafienie piłki na fioletową dziurę
    public static void showLooserUI()
    {
        isLooserUIShown = true;
    }

	// Metoda do restartu pierwszego poziomu
	void RestartScene()
	{
		SceneManager.LoadScene ("Level1");
	}

  
}
