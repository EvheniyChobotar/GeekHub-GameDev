using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

	public Transform taskPosition;
	public Rigidbody player;
	public MeshCollider bridgeColl;
	public MeshCollider levelColl;
	public AudioSource woodBreak;

	Vector3 endPos;
	void Start() {
		endPos = taskPosition.position;
	}

	void Update () {
		if (Vector3.Distance (endPos, CharacterController.instance.playerTr.position) < 2f) {
			if (!woodBreak.isPlaying) {
				woodBreak.Play ();
			}
			bridgeColl.enabled = false;
			levelColl.enabled = false;

			player.drag = 0;
			player.isKinematic = false;
			StartCoroutine (preloadAction ());
		}


	}

	IEnumerator preloadAction() {
		yield return new WaitForSeconds (1.5f);
		SceneManager.LoadScene ("Task3Scene");
	}
}
