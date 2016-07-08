using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
	public float timeBetweenAttacks = 1f;
	public int attackDamage = 10;


	Animation anim;
	GameObject player;
	PlayerHealth playerHealth;
	AudioSource playerAudio, enemyAudio;
	public AudioClip punchClip;
	EnemyHealth enemyHealth;
	bool playerInRange;
	float timer = 1f;


	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("MainCamera");
		playerHealth = player.GetComponent <PlayerHealth> ();
		enemyAudio = GetComponent <AudioSource> ();
		playerAudio = player.GetComponent <AudioSource> ();
		enemyHealth = GetComponent<EnemyHealth>();
		anim = GetComponent <Animation> ();
	}


	void OnTriggerEnter (Collider other)
	{
		if(other.gameObject == player)
		{
			playerInRange = true;
		}
	}


	void OnTriggerExit (Collider other)
	{
		if(other.gameObject == player)
		{
			playerInRange = false;
		}
	}


	void Update ()
	{
		timer += Time.deltaTime;

		if(timer >= timeBetweenAttacks && playerInRange && playerHealth.currentHealth > 0 && enemyHealth.currentHealth > 0)
		{
			Attack ();
		}
	}


	void Attack ()
	{
		timer = 0f;

		// Syncs punch sound with correct time in punch animation
		enemyAudio.clip = punchClip;
		enemyAudio.PlayDelayed (0.5f);

		if (playerHealth.currentHealth > 0)
		{
			playerHealth.TakeDamage (attackDamage);
		} 
		else
		{
			anim.Play ("idol");
		}
	}
}
