using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//用來管理英雄身上的數值

public enum HeroCareer {

}

public class HeroManager : MonoBehaviour {

    //單例模式
    private static HeroManager instance = null;
    public static HeroManager Instance {
        get {
            // 還沒指定時就先尋找遊戲中有沒有⼀樣的
            if(instance == null) {
                instance = FindObjectOfType<HeroManager>();
            }
            if(instance == null) {
                print("找不到HeroManager");
            }
            return instance;
        }
    }

    //英雄的年齡(走了多少步)
    public int age = 0;

    //英雄的金錢
    public int money = 0;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        if(Input.GetKeyDown(KeyCode.UpArrow)) {
            MapManager.Instance.MapUp();
        }
        if(Input.GetKeyDown(KeyCode.RightArrow)) {
            MapManager.Instance.MapRight();
        }
        
	}

    //英雄初始化
    void SetHeroCareer(HeroCareer job) {

    }
}
