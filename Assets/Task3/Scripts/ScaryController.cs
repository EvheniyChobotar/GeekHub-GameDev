using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaryController : MonoBehaviour {

	private AudioSource[] audioSource;
	public AudioClip 	 heartEffect;
	public Transform	 scaryObject;
	Vector3 scaryObjPos;

	void Start () {
		audioSource = GetComponents<AudioSource> ();
		scaryObjPos = scaryObject.position;
		audioSource[0].loop = true;
		audioSource[0].volume = 0.3f;
	}
	
	void Update () {
		if(!audioSource[0].isPlaying) {
			audioSource[0].PlayOneShot (heartEffect);
		}
		if (Vector3.Distance (scaryObjPos, CharacterController.instance.playerTr.position) < 40f) {
			audioSource[0].pitch = Mathf.Lerp(audioSource[0].pitch, 1f + (40f - Vector3.Distance (scaryObjPos, CharacterController.instance.playerTr.position)) * 0.04f, Time.deltaTime);
		}
	}
}
