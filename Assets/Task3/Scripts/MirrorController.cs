using UnityEngine;

public class MirrorController : MonoBehaviour {
	public Transform 	playerHead;
	public Transform 	mirror;
	public Transform	camTr;
	public Camera		cam;
	public Light		reflectionLight;
	public Light		plFleshLIght;
	private float 		timeForTransformations;
	public float asp;
	void Start () {
		timeForTransformations = 30f;
		asp = 1;
		cam.aspect = asp;
	}

	void LateUpdate () {
		reactOnDistance ();
		reflectionCameraBehavior ();
	} 

	float distance;
	void reactOnDistance() {
		distance = Vector3.Distance (mirror.position, playerHead.position);
		cam.fieldOfView = Mathf.Lerp (cam.fieldOfView, 100 - (distance * 10f),  Time.deltaTime * timeForTransformations);
		cam.aspect = Mathf.Lerp (cam.aspect, asp,  Time.deltaTime * timeForTransformations);
		camTr.position = new Vector3 (camTr.position.x, playerHead.position.y, camTr.position.z);
		reflectionLight.intensity = 1.5f - (distance * 0.1f);
		reflectionLight.spotAngle = 10 + (distance * 10);
		reflectionLight.enabled = plFleshLIght.enabled;
		if (distance < 7f && !cam.enabled ) {
			cam.enabled = true;
		} if (distance > 7f && cam.enabled ) {
			cam.enabled = false;

		}

	}

	void reflectionCameraBehavior() {
		camTr.rotation = Quaternion.LookRotation (Vector3.Reflect (Vector3.Normalize (mirror.position - playerHead.position), mirror.right), Vector3.up);
	}

}
