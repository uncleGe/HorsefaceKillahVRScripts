using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {


	PlayerHealth playerHealth;
	GameObject cameraObject;
	float timer;


	void Awake()
	{
		timer = 0f;
		cameraObject = GameObject.FindGameObjectWithTag ("MainCamera");
		playerHealth = cameraObject.GetComponent<PlayerHealth> ();
	}


	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Escape))
		{
			Application.Quit ();
		}

		if (playerHealth.isDead)
		{
			timer += Time.deltaTime;

			// Input from Google Cardboard trigger or computer keyboard can restart the game
			if((Cardboard.SDK.Triggered || Input.GetButton ("Fire1")) && timer > 1.5 && Time.timeScale != 0)
			{
				playerHealth.RestartLevel ();
			}
		}
	}
}
