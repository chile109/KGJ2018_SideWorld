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

    //勇者的年齡(走了多少步)
    public int age = 0;

    //勇者的金錢
    public int money = 0;

    //勇者身上攜帶的道具
    public List<int> item = new List<int>();


    // Use this for initialization
    void Start () {
		
	}
	

    //英雄初始化
    public void SetHeroCareer(HeroCareer job) {

    }

    //勇者獲得道具
    public void HeroGetItem(int i) {
        if(item.Contains(i)) {
            print("已擁有道具");
            return;
        }
        item.Add(i);
        //刷新UI狀態

    }

    //勇者失去得道具(return false表示沒有該項道具)
    public bool HeroLostItem(int i) {
        if(item.Contains(i)) {
            item.Remove(i);
            //刷新UI狀態

        } else {
            return false;
        }

        return true;
    }

}
