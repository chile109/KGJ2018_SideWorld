using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    //事件顯示狀態版
    public Text eventBoard;

	// Use this for initialization
	void Start () {
		
	}


    //向資料庫要資料
    public EventData GetEventData() {
        EventData data = new EventData();
        data.Sn = Random.Range((int)50,100);
        data.Name = "Item Shop";
        data.DescriptionGood = "哈哈哈";
        data.GetMoney = 20;
        data.got = new int[4] { -1, -1, -1, -1 };
        data.Lost = new int[4] { -1, -1, -1, -1 };
        return data;
    }

    //事件反應(False表示死亡，True表示可以繼續)
    public bool PlayEvent(EventData data) {
        //顯示壞結局用
        bool goodDes = true;

        //增加道具
        foreach(int obj in data.got) {
            if(obj != -1) {  
                HeroManager.Instance.HeroGetItem(obj);
            }
        }

        //檢查事件是否需要物件
        foreach(int obj in data.Lost) {
            if(obj != -1) {  //一定沒有999，必死
                if(HeroManager.Instance.HeroLostItem(obj) == false) {
                    MainGameScript.Instance.gameOver = true;
                    goodDes = false;
                }
            }
        }

        //增加金幣
        HeroManager.Instance.money = HeroManager.Instance.money + data.GetMoney;

        //檢查金幣是否足夠
        if(HeroManager.Instance.money - data.LostMoney < 0) {
            MainGameScript.Instance.gameOver = true;
            goodDes = false;
        } else {
            HeroManager.Instance.money = HeroManager.Instance.money - data.LostMoney;
        }

        //強制死亡事件
        if(data.DescriptionGood == "-1") {
            goodDes = false;
            MainGameScript.Instance.gameOver = true;
        }

        //顯示文本
        if(goodDes) {
            eventBoard.text = data.DescriptionGood;
        } else {
            eventBoard.text = data.DescriptionBad;
        }

        //狀態繼續推行
        MainGameScript.Instance.pass = true;
        return true;
    }
}
