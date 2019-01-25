using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallControlScript : MonoBehaviour {

	// Reference to Rigidbody2D component of the ball game object
	Rigidbody2D rb;

	// Range option so moveSpeedModifier can be modified in Inspector
	// this variable helps to simulate objects acceleration
	[Range(0.2f, 2f)]
	public float moveSpeedModifier = 0.5f;

	// Direction variables that read acceleration input to be added
	// as velocity to Rigidbody2d component
	float dirX, dirY;

	// Reference to Balls Animator component to control animaations transition
	Animator anim;

	// Setting bool variable that ball is alive at the beginning
	static bool isDead;

	// Variable to allow or disallow movement when ball is alive or dead
	static bool moveAllowed;

	// Variable to be set to true if you win
	static bool youWin;

    static bool isWinnerUIShown;
    static bool isLooserUIShown;

    // Reference to WinText game object to control its appearance
    // Text game object can be added in inspector because of [SerializeField] line
    [SerializeField]
	GameObject winText;

    [SerializeField]
    GameObject looseText;

    [SerializeField]
    GameObject nextLevelButton;

    [SerializeField]
    GameObject mainMenuButton;

    [SerializeField]
    GameObject restartLevelButton;
    

    // Use this for initialization
    void Start () {

		// Turn WinText off at the start
		winText.gameObject.SetActive(false);
        looseText.gameObject.SetActive(false);

		// You don't win at the start
		youWin = false;

		// Movement is allowed at the start
		moveAllowed = true;

		// Ball is alive at the start
		isDead = false;

		// Getting Rigidbody2D component of the ball game object
		rb = GetComponent<Rigidbody2D> ();

		// Getting Animator component of the ball game object
		anim = GetComponent<Animator> ();

		// Set BallAlive animation
		anim.SetBool ("BallDead", isDead);

        nextLevelButton.gameObject.SetActive(false);
        restartLevelButton.gameObject.SetActive(false);
        mainMenuButton.gameObject.SetActive(false);

        isWinnerUIShown = false;
        isLooserUIShown = false;

    }
	
	// Update is called once per frame
	void Update () {

		// Getting devices accelerometer data in X and Y direction
		// multiplied by move speed modifier
		dirX = Input.acceleration.x * moveSpeedModifier;
		dirY = Input.acceleration.y * moveSpeedModifier;

		// if isDead is true
		if (isDead) {

			// then ball movement is stopped
			rb.velocity = new Vector2 (0, 0);

			// Set Animators BallDead variable to true to switch to 
			anim.SetBool ("BallDead", isDead);

        }

		// If you win
		if (youWin) {

			if (SceneManager.GetActiveScene().name.Equals("Level5"))
            {
                Invoke("ChangeSceneToEnd", 2f);
            }
			

			// ball movement is not allowed anymore
			moveAllowed = false;

			// switch to Ball Dead Animation so ball falls into exit hole
			anim.SetBool("BallDead", true);
            
        }
        
        if (isWinnerUIShown && !SceneManager.GetActiveScene().name.Equals("Level5"))
        {
            winText.gameObject.SetActive(true);
            nextLevelButton.gameObject.SetActive(true);
            mainMenuButton.gameObject.SetActive(true);
        }

        if (isLooserUIShown)
        {
            looseText.gameObject.SetActive(true);
            restartLevelButton.gameObject.SetActive(true);
            mainMenuButton.gameObject.SetActive(true);
        }

	}

    void ChangeSceneToEnd()
    {
        Application.LoadLevel("End");
    }

	void FixedUpdate()
	{
		// Setting a velocity to Rigidbody2D component according to accelerometer data
		if (moveAllowed)
		rb.velocity = new Vector2 (rb.velocity.x + dirX, rb.velocity.y + dirY);
	}

	// Method is invoked by DeathHoleScript when ball touches deathHole collider
	public static void setIsDeadTrue()
	{
		// Setting isDead to true
		isDead = true;
	}

	// Method is inviked by exit hole game object when ball thouches its collider
	public static void setYouWinToTrue()
	{
		youWin = true;
	}

    public static void showWinnerUI()
    {
        isWinnerUIShown = true;
    }

    public static void hideUI()
    {
        isWinnerUIShown = false;
        isLooserUIShown = false;
    }

    public static void showLooserUI()
    {
        isLooserUIShown = true;
    }

	// Method to restart current scene
	void RestartScene()
	{
		SceneManager.LoadScene ("Level1");
	}

  
}
