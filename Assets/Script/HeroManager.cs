using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//用來管理英雄身上的數值

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
    public int age = 15;

    //勇者的金錢
    public int money = 0;

    //勇者的職業
    public string job;

    //職業的描述
    public string dis;

    // Use this for initialization
    IEnumerator Start () {
        yield return new WaitForSeconds(0.1f);
        SetHeroCareer();

    }
	
    //英雄初始化
    public void SetHeroCareer() {
        //隨機職業
        int r = Random.Range(0,JsonLoader.refPool.Count);
        job = JsonLoader.refPool[r].Name;
        money = JsonLoader.refPool[r].Money;
        PanelManager._inst._Job.text = JsonLoader.refPool[r].Name;
        PanelManager._inst._Age.text = "15";
        PanelManager._inst._Money.text = money.ToString();
        
        MainGameScript.Instance.heroInfo.text = JsonLoader.refPool[r].Description;
        //print("Item C = " + JsonLoader.refPool[r].Item.Length);
        foreach(int obj in JsonLoader.refPool[r].Item) {
            if(obj != -1) {
                print(JsonLoader.ItemPool[obj].name);
                BagManager._instant.GotItem(JsonLoader.ItemPool[obj]);
            }
            
        }
    }

}
