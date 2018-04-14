using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//單塊地圖的資訊
[System.Serializable]
public class MapInfo : MonoBehaviour {

    //地圖使用的Sprite
    public SpriteRenderer sp;

    //地圖上的掉落物
    public SpriteRenderer item;

    //內藏事件
    public EventData eve;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
