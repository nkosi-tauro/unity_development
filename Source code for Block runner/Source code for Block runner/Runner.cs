using UnityEngine;

public class Runner : MonoBehaviour {

	public static float distanceTraveled;

	public float acceleration;

	private bool touchingPlatform;

	public Vector3 boostVelocity, jumpVelocity;

	public float gameOverY;

	private Vector3 startPosition;

	void Start () {
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		startPosition = transform.localPosition;
		GetComponent<Renderer>().enabled = false;
		GetComponent<Rigidbody>().isKinematic = true;
		enabled = false;
	}

	private static int boosts;

	private void GameStart () {
		boosts = 0;
		GUIManager.SetBoosts(boosts);
		distanceTraveled = 0f;
		GUIManager.SetDistance(distanceTraveled);
		transform.localPosition = startPosition;
		GetComponent<Renderer>().enabled = true;
		GetComponent<Rigidbody>().isKinematic = false;
		enabled = true;
	}

	public static void AddBoost () {
		boosts += 1;
		GUIManager.SetBoosts(boosts);
	}

	private void GameOver () {
		GetComponent<Renderer>().enabled = false;
		GetComponent<Rigidbody>().isKinematic = true;
		enabled = false;
	}

	void Update () {
		    if(touchingPlatform && Input.GetButtonDown("Jump")){
			if(touchingPlatform){
				touchingPlatform = false;
			}
			else if(boosts > 0){
				GetComponent<Rigidbody>().AddForce(boostVelocity, ForceMode.VelocityChange);
				boosts -= 1;
				GUIManager.SetBoosts(boosts);
			}
			    GetComponent<Rigidbody>().AddForce(jumpVelocity, ForceMode.VelocityChange);
			                 touchingPlatform = false;
		    }
		    distanceTraveled = transform.localPosition.x;
		    GUIManager.SetDistance(distanceTraveled);

		    if(transform.localPosition.y < gameOverY){
			GameEventManager.TriggerGameOver();
		}
	}

	void FixedUpdate () {
		    if(touchingPlatform){
			GetComponent<Rigidbody>().AddForce(acceleration, 0f, 0f, ForceMode.Acceleration);
		}
	}

	void OnCollisionEnter () {
		touchingPlatform = true;
	}

	void OnCollisionExit () {
		touchingPlatform = false;
	}
}


