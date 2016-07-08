using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
	public int startingHealth = 100;
	public int currentHealth;
	public float sinkSpeed = 2.5f;
	public int scoreValue = 10;
	public AudioClip gruntClip;
	public bool isDead;

	bool isSinking;

	NavMeshAgent nav;
	Animation anim;
	AudioSource enemyAudio;
	ParticleSystem hitParticles;
	CapsuleCollider capsuleCollider;
	GameObject gameController;
	GameObject canvas;
	EnemyManager enemyManager;


	void Awake ()
	{
		canvas = GameObject.FindGameObjectWithTag ("Canvas");
		gameController = GameObject.FindGameObjectWithTag ("GameController");
		enemyManager = gameController.GetComponent<EnemyManager> ();
		nav = GetComponent<NavMeshAgent> ();
		anim = GetComponent <Animation> ();
		enemyAudio = GetComponent <AudioSource> ();
		hitParticles = GetComponentInChildren <ParticleSystem> ();
		capsuleCollider = GetComponent <CapsuleCollider> ();

		currentHealth = startingHealth;
	}


	void Update ()
	{
		if(isDead && !anim.IsPlaying ("death"))
		{
			// Dead enemy begins to sink through ground
			transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
			StartSinking ();
		}
	}


	public void TakeDamage (int amount, Vector3 hitPoint)
	{
		if(isDead)
			return;

		enemyAudio.clip = gruntClip;
		enemyAudio.Play ();

		currentHealth -= amount;

		hitParticles.transform.position = hitPoint;
		hitParticles.Play();

		if(currentHealth <= 0)
		{
			Death ();
		}
	}


	void Death ()
	{
		isDead = true;

		nav.enabled = false;
		anim.Play ("death");
	}


	public void StartSinking ()
	{
		// Removes rigid body physics from dead enemy
		GetComponent <NavMeshAgent> ().enabled = false;
		GetComponent <Rigidbody> ().isKinematic = true;
		capsuleCollider.isTrigger = true;

		// Player gets points for killing enemy
		ScoreManager.score += scoreValue;

		Destroy (gameObject, 0.5f);

		// Limit on total enemies may now allow a new spawn
		enemyManager.spawnCount--;
	}



}
