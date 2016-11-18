using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsController : MonoBehaviour {
	
	public 	AudioClip walkOnGround;
	public 	AudioClip walkOnWood;
	private AudioClip currentClip;
	private AudioSource[] audioSource;
	CollisionController collInfo;

	void Start () {
		currentClip = walkOnWood;
		audioSource = GetComponents<AudioSource> ();	
		audioSource [1].volume = 0.6f;
		collInfo = CollisionController.instance;
	}

	float stepTimer = 1f;
	void Update () {
		if(collInfo.collision != null)
			switch (collInfo.collision.gameObject.tag) {
			case "ground": 
				currentClip = walkOnGround;
				break;
			case "bridge":
				currentClip = walkOnWood;
				break;
			default:
				break;
			}

		stepTimer -= Time.deltaTime;
		if (Input.GetKey ("w") || Input.GetKey ("a") || Input.GetKey ("s") || Input.GetKey ("d")) {
			stepTimer = 1f;
			if (!audioSource [1].isPlaying ) {
				audioSource [1].PlayOneShot (currentClip);
			}
		} else 
			if(stepTimer < 0.7f) {
			audioSource [1].Stop ();
		}
		if (Input.GetKeyDown ("f")) {
			audioSource [3].Play ();
		}
	}
}
