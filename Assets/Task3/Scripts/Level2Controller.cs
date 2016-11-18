using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Level2Controller : MonoBehaviour {

	public GameObject 	frHospital;
	public GameObject 	horrorHospital;

	void Start () {
		
	}
	
	void Update () {
		if (Input.GetKeyDown ("q")) {
			frHospital.SetActive (!frHospital.activeInHierarchy);
			horrorHospital.SetActive (!horrorHospital.activeInHierarchy);
		}
	}

}
