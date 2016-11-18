using UnityEngine;
using System.Collections.Generic;
using System;

public class GameObjActionController : MonoBehaviour {
	private	Transform door;
	private RaycastHit hit;

	private Dictionary<string, Action> objBehavior;

	void Awake() {
		door = GameObject.FindWithTag ("StartDoor").GetComponent<Transform>();
		objBehavior = new Dictionary<string, Action> {
			{door.gameObject.tag, () => door.localRotation = Quaternion.Euler (door.localEulerAngles.x, Mathf.Lerp (door.localEulerAngles.y, 70f, Time.deltaTime * 0.5f), door.localEulerAngles.z)},
			{"", () => Debug.Log("I cant perform this action")},
		};
	}

	void FixedUpdate() {
		if (Input.GetKey ("e")) {
			Physics.Raycast (CharacterController.instance.cameraTr.position, CharacterController.instance.cameraTr.forward, out hit, 1f);
			performObjBehavior (((Func<RaycastHit, string>)((hitResult) => { return (hitResult.collider == null) ? "" : hit.transform.gameObject.name; }))(hit));
		}
	}

	private void performObjBehavior(string action) {
		if (objBehavior.ContainsKey (action)) {
			objBehavior [action] ();
		} 
	}
}
