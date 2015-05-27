using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(Mathf.Clamp(Input.GetAxis("Mouse Y"),-1.0f,1.0f), 0, 0) * 50 * Time.deltaTime);
        Debug.Log(Input.GetAxis("Mouse Y"));
	}
}
