using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//用來管理地圖事件的觸發,每次更新地圖時會向資料庫要資料

/*
 * 事件參數說明
   //事件編號
   public string Sn;   
   //事件名稱
   public string Name;
   //事件成功
   public string DescriptionGood;
   //事件失敗
   public string DescriptionBad;
   //獲得道具
   public int[] got = new int[4];
   //失去道具
   public int[] Lost = new int[4];
   //獲得金錢
   public int GetMoney;
   //失去今錢
   public int LostMoney;
   //最小年齡限制
   public int MinAge;
   //最大年齡限制
   public int MaxAge;
   //好結局事件圖片
   public int GoodPic;
   //壞結局事件圖片
   public int BadPic;
   //可重複次數
   public int Repeat;
   //是否為結局
   public int End;
 * */

public class EventManager : MonoBehaviour {

    //單例模式
    private static EventManager instance = null;
    public static EventManager Instance {
        get {
            // 還沒指定時就先尋找遊戲中有沒有⼀樣的
            if(instance == null) {
                instance = FindObjectOfType<EventManager>();
            }
            if(instance == null) {
                print("找不到EventManager");
            }
            return instance;
        }
    }
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //向資料庫要資料

}
