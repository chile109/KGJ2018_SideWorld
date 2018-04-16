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

    //狀態版
    public GameObject board;

	// Use this for initialization
	void Start () {
		
	}


    //向資料庫要資料
    public EventData GetEventData() {
        EventData data = new EventData();
        bool canUse = false;
        int age = HeroManager.Instance.age;
        int count = 0;

        //強制為使萊姆
        //data = JsonLoader.EventPool[5];
        
        while(!canUse) {
            int r = Random.Range(0, JsonLoader.EventPool.Count);
            //print(JsonLoader.EventPool[r].Name + " got[0]= " + JsonLoader.EventPool[r].got[0] ); 
            if(JsonLoader.EventPool[r].MinAge < age || age <= JsonLoader.EventPool[r].MaxAge) {
                data = JsonLoader.EventPool[r];
                canUse = true;
            }
            count += 1;
            if(count >30) {
                canUse = true;
            }

        }

        return data;
    }

    //事件反應(False表示死亡，True表示可以繼續)
    public bool PlayEvent(EventData data) {
        //顯示壞結局用
        bool goodDes = true;

        //有大圖的話就秀出大圖
        if(data.GoodPic ==1 || data.BadPic == 1) {
            Sprite myImg = Resources.Load<Sprite>("EventPic/" + data.Sn.ToString());
            MainGameScript.Instance.eventPic.gameObject.SetActive(true);
            MainGameScript.Instance.eventPic.sprite = myImg;
            
        } else {
            MainGameScript.Instance.eventPic.gameObject.SetActive(false);
        }
        

        //print("Event" + data.Name);
        //print("0 = " + data.got[0]);
        //增加道具
        foreach(int obj in data.got) {
           // print(obj);
            if(obj != -1) {
                BagManager._instant.GotItem(JsonLoader.ItemPool[obj]);
                MusicManager.order.PlaySound(4);
            }
        }

        //檢查事件是否需要物件
        foreach(int obj in data.Lost) {
            if(obj != -1) {  //一定沒有999，必死
                if(BagManager._instant.LostItem(JsonLoader.ItemPool[obj]) == false) {
                    MainGameScript.Instance.gameOver = true;
                    goodDes = false;
                    MusicManager.order.PlaySound(5);
                }
            }
        }

        //增加金幣
        if(data.GetMoney > 0) {
            MusicManager.order.PlaySound(6);
            HeroManager.Instance.money = HeroManager.Instance.money + data.GetMoney;
        }
        
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

        //顯示整個狀態版
        board.SetActive(true);

        //顯示文本
        if(goodDes) {
            eventBoard.text = data.DescriptionGood;
            //更新人生足跡
            MainGameScript.Instance.lifeStep += HeroManager.Instance.age + "歲" + data.Name + "\n";
        } else {
            eventBoard.text = data.DescriptionBad;
            //死了
            //更新人生足跡
            MainGameScript.Instance.lifeStep += "然後" + HeroManager.Instance.age+ "歲就死了ㄏㄏ";
        }

        //狀態繼續推行
        //MainGameScript.Instance.pass = true;
        return true;
    }
}
