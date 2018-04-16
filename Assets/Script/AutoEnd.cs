using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//自動關閉

public class AutoEnd : MonoBehaviour {

    public float time = 5;

	// Use this for initialization
	void Start () {
		
	}

    void OnEnable() {
        time = 5;
    }
	
	// Update is called once per frame
	void Update () {
        time -= Time.deltaTime;
        if(time < 0) {
            gameObject.SetActive(false);
        }
	}
}
