using UnityEngine;
using System;
using System.Collections.Generic;

public sealed class CharacterController : MonoBehaviour {


	public static CharacterController instance;
	private Dictionary<string, Action> playerBehavior;
	public  GameObject 	plCamera;
	public  GameObject 	player;
	public	Light 		fleshLight;
	private Transform 	_playerTr = null;
	private Rigidbody 	_playerRb = null;
	private Transform 	_cameraTr = null;
	public float 		_speed = 4f;
	private float 		_boost = 1.2f;
	private float 		_sensivity = 1f;
	private float 		_side = 0, upDown = 0;
	private bool 		_isMoving;


	void Awake() {
		if (instance == null)
			instance = this;
		else
			Destroy (this);
		Application.targetFrameRate = -1;
		cacheComponents ();
		playerRigidBody.isKinematic = true;
		Cursor.lockState = CursorLockMode.Locked;
		_isMoving = false;

	}


	void Start () {
		playerBehavior = new Dictionary<string, Action> {
			{"walkForward", 	() => walkForward()},
			{"walkBackward", 	() => walkBackward()},
			{"walkLeft", 		() => walkLeft()},
			{"walkRight", 		() => walkRight()},
			{"jump", 			() => playerRigidBody.AddRelativeForce(Vector3.up 	* 30f, ForceMode.Impulse)},
			{"boost", 			() => playerTr.Translate(Vector3.forward * (speed + boost) * Time.deltaTime)}, 
			{"lighOnOff", 		() => flashLightControl()},
		};
	}

	void Update () {
		checkPlayerBehavior ();
	}

	void LateUpdate(){
		cameraControl ();
	}

	Vector3 head = new Vector3 (0, 0.5f, 0);
	private void cameraControl() {
		_side +=  _sensivity * Input.GetAxis("Mouse X");
		upDown -= _sensivity * Input.GetAxis("Mouse Y");
		upDown = Mathf.Clamp (upDown, -70f, 70f);
		cameraRotation = Quaternion.Euler(upDown, _side, 0.0f);
		playerRotation = Quaternion.Euler(0.0f, _side, 0.0f);
		cameraTr.position = playerTr.position + head;
	}

	private void performPlayerBehavior(string action) {
		if (playerBehavior.ContainsKey (action)) {
			playerBehavior [action] ();
		}
	}

	private void checkPlayerBehavior() {
		if (Input.GetKey ("w")) { performPlayerBehavior ("walkForward"); } 
		if (Input.GetKey ("s")) { performPlayerBehavior("walkBackward"); }
		if (Input.GetKey ("a")) { performPlayerBehavior("walkLeft"); }
		if (Input.GetKey ("d")) { performPlayerBehavior("walkRight"); }
		if (Input.GetKeyDown ("f")) { performPlayerBehavior("lighOnOff"); }

		if (CollisionController.instance.inFlight) { playerRigidBody.drag = 0f; } 
		if (!CollisionController.instance.inFlight) { playerRigidBody.drag = 15f; }

		if (_isMoving) { 
			playerRigidBody.isKinematic = false; 
			_isMoving = false; 
		} 
		else { playerRigidBody.isKinematic = true; }
	}

	void walkForward() {
		_isMoving = true;
		playerTr.Translate(Vector3.forward 	* speed * Time.deltaTime);
	}

	void walkBackward() {
		_isMoving = true;
		playerTr.Translate (Vector3.back * speed * Time.deltaTime);
	}
	void walkLeft() {
		_isMoving = true;
		playerTr.Translate (Vector3.left * speed * Time.deltaTime);
	}
	void walkRight() {
		_isMoving = true;
		playerTr.Translate (Vector3.right * speed * Time.deltaTime);
	}

	void flashLightControl() {
		fleshLight.enabled = !fleshLight.enabled;
	}

	private void cacheComponents() {
		_playerTr = player.transform.GetComponent<Transform> ();
		_playerRb = player.transform.GetComponent<Rigidbody> ();
		_cameraTr = plCamera.transform.GetComponent<Transform> ();
	}


	public float speed {
		get { return this._speed; }
		private set { this._speed = value; }
	}
	public float boost {
		get { return this._boost; }
		private set { this._boost = value; }
	}
	public float sensivity {
		get { return this._sensivity; }
		private set { this._sensivity = value; }
	}

	public Quaternion playerRotation {
		get { return this._playerTr.rotation; }
		private	set { this._playerTr.rotation = value; }
	}

	public Transform playerTr {
		get { return this._playerTr; }
		set { this._playerTr = value;}
	}

	public Rigidbody playerRigidBody {
		get { return this._playerRb; }
	}

	public Quaternion cameraRotation {
		get { return this._cameraTr.rotation; }
		private set { this._cameraTr.rotation = value; }
	}

	public Transform cameraTr {
		get { return this._cameraTr; }
		private set { this._cameraTr = value; }
	}

}
