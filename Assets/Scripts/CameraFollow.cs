using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class CameraFollow : MonoBehaviour {

	[SerializeField] 
	Transform target;
	[SerializeField]
	float smoothing = 5f;
	Vector3 offset;

	void Awake(){
		Assert.IsNotNull(target);
	}
	// for calculation of moving of camera
	void Start () {
		offset = transform.position - target.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		Vector3 targetCamPos = target.position + offset;
		transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
	}
}
