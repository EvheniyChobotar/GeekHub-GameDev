using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour {

	public static CollisionController instance;
	private Collision coll;
	public bool inFlight;

	void Awake() {
		if (instance == null)
			instance = this;
		else
			Destroy (this);
		inFlight = false;
		coll = null;
	}

	public Collision collision {
		get {
			return this.coll;
		}
	}

	void OnCollisionEnter(Collision collision) {
		coll = collision;
		inFlight = false;
	}

	void OnCollisionExit(Collision collisionInfo) {
		if (collisionInfo.gameObject.tag == "ground"
		|| collisionInfo.gameObject.tag == "bridge") {
			inFlight = true;
		} 
	}
}
