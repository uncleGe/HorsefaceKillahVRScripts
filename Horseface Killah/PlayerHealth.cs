using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
	public int startingHealth = 100;
	public int currentHealth;
	public Slider healthSlider;
	public Image damageImage;
	public AudioClip deathClip;
	public float flashSpeed = 5f;
	public float deathSpeed = 2f;
	public Color flashColor = new Color(1f, 0f, 0f, 0.1f);
	public Color deathColor = new Color (0f, 0f, 0f, 1f);
	public AudioSource playerAudio;
	public bool isDead;

	GameObject cube;
	GameObject screen;
	GameObject gameOverText;
	GameObject tapText;
	Animator anim0;
	Animator anim1;
	Animator anim2;
	PlayerCamMovement playerCamMovement;
	PlayerShooting playerShooting;
	bool damaged;


	void Awake ()
	{
		isDead = false;	
		playerAudio = GetComponent <AudioSource> ();
		screen = GameObject.FindGameObjectWithTag ("Screen");
		damageImage = screen.GetComponentInChildren<Image> ();

		cube = GameObject.FindGameObjectWithTag ("CubeL");
		anim0 = cube.GetComponent <Animator> ();

		gameOverText = GameObject.FindGameObjectWithTag ("GameOverText");
		anim1 = gameOverText.GetComponent <Animator> ();

		tapText = GameObject.FindGameObjectWithTag ("TapText");
		anim2 = tapText.GetComponent <Animator> ();

		playerCamMovement = GetComponent <PlayerCamMovement> ();
		playerShooting = GetComponentInChildren <PlayerShooting> ();
		currentHealth = startingHealth;

		playerShooting = GetComponentInChildren <PlayerShooting> ();
	}


	void Update ()
	{
		if(damaged)
		{
			Invoke ("punchBloodInitiate", 0.5f);
		}
		else
		{
			Invoke ("punchBloodLerp", 0.5f);
		}
		damaged = false;
	}


	// Screen turns translucent red and health display lowers
	void punchBloodInitiate()
	{
		damageImage.color = flashColor;
		healthSlider.value = currentHealth;
	}


	// Each frame lowers alpha value from red damage image
	void punchBloodLerp()
	{
		damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
	}


	public void TakeDamage (int amount)
	{
		currentHealth -= amount;
		damaged = true;

		if(currentHealth <= 0 && !isDead)
		{
			Invoke ("Death", 0.1f);
		}
	}


	void Death ()
	{
		isDead = true;
	
		playerAudio.clip = deathClip;
		playerAudio.Play ();

		// disable player controls
		playerCamMovement.enabled = false;
		playerShooting.enabled = false;
		playerShooting.DisableEffects ();

		//play Game Over animations
		anim0.SetTrigger ("isDead");
		anim1.SetTrigger ("gameOver");
		anim2.SetTrigger ("playAgain");

	
	}
		

	public void RestartLevel ()
	{
		SceneManager.LoadScene (0);
	}
}
