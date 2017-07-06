using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {

	public Text timeText;
	public Text winText;

	public float speed;
	public float maxScoreTime = 5f;

	private Rigidbody2D rg2d;
	private int count;
	private bool isGameCompleted;

	// Use this for initialization
	void Start () {
		rg2d = GetComponent<Rigidbody2D> ();
		count = 0;
		isGameCompleted = false;
		SetTimeText ();
		winText.text = "";
	}

	void FixedUpdate() {
		float horizontalMovement = Input.GetAxis ("Horizontal");
		float verticalMovement = Input.GetAxis ("Vertical");
		Vector2 movement = new Vector2 (horizontalMovement, verticalMovement);
		rg2d.AddForce (movement * speed, ForceMode2D.Force);
	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log ("onTriggerEnter2D: " + other.tag);
		if (other.gameObject.CompareTag ("PickUp")) {
			other.gameObject.SetActive (false);
			count++;
			if (count >= 12) {
				isGameCompleted = true;
				OnGameComplete ();
			}
		}
	}

	void Update() {
		if (!isGameCompleted) {
			SetTimeText ();
		}
	}

	void SetTimeText() {
		timeText.text = "Time Elapsed:" + Time.realtimeSinceStartup.ToString ("F2");
	}

	void OnGameComplete() {
		float timeSpent = Time.realtimeSinceStartup;
		int score = 100;
		if (timeSpent > maxScoreTime) {
			score = score - (int)((Time.realtimeSinceStartup - maxScoreTime) / maxScoreTime * 100);
		}
		if (score < 0) {
			score = 0;
		}
		winText.text = "You Win!\n" + "Score: " + score;
	}
}
