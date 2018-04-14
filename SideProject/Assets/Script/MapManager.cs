using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//地圖控制程式

public class MapManager : MonoBehaviour {

    //單例模式
    private static MapManager instance = null;
    public static MapManager Instance {
        get {
            // 還沒指定時就先尋找遊戲中有沒有⼀樣的
            if(instance == null) {
                instance = FindObjectOfType<MapManager>();
            }
            if(instance == null) {
                print("找不到MapManager");
            }
            return instance;
        }
    }

    //地圖資訊

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
