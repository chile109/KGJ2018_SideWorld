using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//用來設定初始顏色

public class StartColor : MonoBehaviour {

    public Color c = Color.white;

	// Use this for initialization
	void Start () {
        RawImage rw = GetComponent<RawImage>();
        if(rw) {
            rw.color = c;
        }
        Image i = GetComponent<Image>();
        if(i) {
            i.color = c;
        }
        Text t = GetComponent<Text>();
        if(t) {
            t.color = c;
        }

	}
	

}
