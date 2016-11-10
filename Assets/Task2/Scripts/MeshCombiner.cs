using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MeshCombiner : MonoBehaviour {
	public bool requireCollider;
	void Start() {
		performCombine ();
	}

	void performCombine() {
		Quaternion oldRot = transform.rotation;
		Vector3 oldPos = transform.position;
		transform.rotation = Quaternion.identity;
		transform.position = Vector3.zero;

		MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
		CombineInstance[] combine = new CombineInstance[meshFilters.Length];

		Mesh finalMesh = new Mesh ();

		for (int i = 0; i < meshFilters.Length; i++) {
			if (meshFilters [i].transform == transform)
				continue;

			combine [i].subMeshIndex = 0;
			combine [i].mesh = meshFilters [i].sharedMesh;
			combine [i].transform = meshFilters [i].transform.localToWorldMatrix;

		}

		finalMesh.CombineMeshes (combine);
		GetComponent<MeshFilter> ().sharedMesh = finalMesh;
		if(requireCollider)
			gameObject.AddComponent<MeshCollider> ();
		transform.rotation = oldRot;
		transform.position = oldPos;

		for (int i = 0; i < transform.childCount; i++) {
			transform.GetChild (i).gameObject.SetActive (false);
		}
	}

}