using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoUp : MonoBehaviour {

    public Vector3 speed;

	// Update is called once per frame
	void Update () {
        transform.position = transform.position + speed * Time.deltaTime;
	}
}
