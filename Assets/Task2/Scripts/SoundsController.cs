using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsController : MonoBehaviour {
	public 	AudioClip 			walkOnGround;
	public 	AudioClip 			walkOnWood;
	public 	AudioClip 	 		heartEffect;
	private AudioClip 			currentClip;
	private AudioSource[] 		audioSource;
	private CollisionController collInfo;
	public 	Transform	 		scaryObject;

	void Start () {
		currentClip = walkOnGround;
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
			audioSource [3].Play();
		}

		if(!audioSource[0].isPlaying) {
			audioSource[0].PlayOneShot (heartEffect);
		}
		if (Vector3.Distance (scaryObject.position, CharacterController.instance.playerTr.position) < 40f) {
			audioSource[0].pitch = Mathf.Lerp(audioSource[0].pitch, 1f + (40f - Vector3.Distance (scaryObject.position, CharacterController.instance.playerTr.position)) * 0.04f, Time.deltaTime);
		}
		
	}
}
