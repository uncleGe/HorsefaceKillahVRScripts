using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	
	public int spawnCount;
	public int spawnMax;
	public PlayerHealth playerHealth;
	public GameObject enemy;
	public float spawnTime = 8f;
	public Transform[] spawnPoints;

	GameObject playerCam;


	void Start ()
	{
		spawnCount = 0;
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
		playerCam = GameObject.FindGameObjectWithTag ("MainCamera");
		playerHealth = playerCam.GetComponentInChildren <PlayerHealth> ();
	}


	void Spawn ()
	{
		// Don't spawn new enemies if the game is over
		if(playerHealth.currentHealth <= 0f)
		{
			return;
		}
		// Generate random place to spawn enemy
		Vector3 spawnSpot = new Vector3(Random.Range (-45f, 45f), 0f, Random.Range (-45f,45f));

		if (spawnCount < spawnMax)
		{
			Instantiate (enemy, spawnSpot, Quaternion.identity);
		}
		spawnCount++;
	}
}
