using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	Rigidbody rb;
	GameObject playerCam;
	EnemyHealth enemyHealth;
	Vector3 playerDirection;
	bool playerInRange;
	NavMeshAgent nav;
	Animation anim;

	public float dudeSpeed;


	void Awake ()
	{
		playerCam = GameObject.FindGameObjectWithTag ("MainCamera");
		enemyHealth = GetComponent<EnemyHealth> ();
		nav = GetComponent <NavMeshAgent> ();
		rb = GetComponent<Rigidbody> ();
		anim = GetComponent<Animation> ();
		transform.LookAt (playerCam.transform.position);
	}


	void Update()
	{
		if (nav)
		{
			nav.SetDestination (playerCam.transform.position);
			if (!enemyHealth.isDead)
			{
				if (playerInRange)
				{
					anim.Play ("punch");
				} 
				else
				{
					anim.Play ("run");
				}
			}
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if(other.gameObject == playerCam)
		{
			playerInRange = true;
		}
	}


	void OnTriggerExit (Collider other)
	{
		if(other.gameObject == playerCam)
		{
			playerInRange = false;
		}
	}
}
