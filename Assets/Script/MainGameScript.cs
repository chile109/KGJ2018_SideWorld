using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

//主要遊戲流程
//利用一個主協程GameThread進行

public class MainGameScript : MonoBehaviour {

    //單例模式
    private static MainGameScript instance = null;
    public static MainGameScript Instance {
        get {
            // 還沒指定時就先尋找遊戲中有沒有⼀樣的
            if(instance == null) {
                instance = FindObjectOfType<MainGameScript>();
            }
            if(instance == null) {
                print("找不到MainGameScript");
            }
            return instance;
        }
    }

    //標題畫面
    public RawImage titleImage;

    //背景黑幕
    public RawImage blackBack;

    //製作組
    public GameObject worker;

    //英雄描述
    public Text heroInfo;

    //重新遊戲提示
    public GameObject replayHint;

    //事件狀態圖
    public Image eventPic;

    //人生足跡
    public string lifeStep= "";

    //人生版
    public Text lifeBoard;

    //墓碑
    public GameObject tomb;

    //驗證用參數
    [NonSerialized]
    public bool pass = false;
    public bool gameOver = false;

    // Use this for initialization
    void Start () {
        //開啟協程
        StartCoroutine("GameThread");
	}

    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }

    public IEnumerator GameThread() {
        //等待滑鼠點擊
        while(!pass) {
            if(Input.GetMouseButtonUp(0) || Input.GetKeyDown(KeyCode.Space)) {
                pass = true;
            }
            yield return 0;
        }

        MusicManager.order.PlaySound(2);

        pass = false;

        //標題淡出
        float t = 1;
        while(t > 0) {
            t -= Time.deltaTime;
            Color c = Color.white;
            c.a = t;
            titleImage.color = c;
            yield return 0;
        }

        worker.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        //顯示英雄的描述
        t = 0;
        while(t < 1) {
            t += Time.deltaTime;
            Color c = Color.white;
            c.a = t;
            heroInfo.color = c;
            yield return 0;
        }

        //等待滑鼠點擊
        while(!pass) {
            if(Input.GetMouseButtonUp(0) || Input.GetKeyDown(KeyCode.Space)) {
                pass = true;
            }
            yield return 0;
        }
        MusicManager.order.PlaySound(2);
        pass = false;

        //描述淡出
        t = 1;
        while(t > 0) {
            t -= Time.deltaTime;
            Color c = Color.white;
            c.a = t;
            heroInfo.color = c;
            yield return 0;
        }

       // yield return new WaitForSeconds(1);

        //背景淡出
        t = 1;
        while(t > 0) {
            t -= Time.deltaTime;
            Color c = Color.black;
            c.a = t;
            blackBack.color = c;
            yield return 0;
        }
        MusicManager.order.Change(0);
        MusicManager.order.Play();

        //設定初始格子的事件
        for(int i = 1; i < MapManager.Instance.mapData.Length; i++) {
            MapManager.Instance.mapData[i].eve = EventManager.Instance.GetEventData();
            //依事件擺圖
            Sprite myImg = Resources.Load<Sprite>("Event/" + MapManager.Instance.mapData[i].eve.Sn.ToString());
            MapManager.Instance.mapData[i].item.sprite = myImg;
            MapManager.Instance.mapData[i].item.gameObject.SetActive(true);
        }
        
        yield return new WaitForSeconds(0.5f);

        //等待遊戲結束(GameOver)
        while(!gameOver) {
            //等待玩家按按鈕(按下按鈕後會由MapManager執行Start上的MapInfo事件)
            while(!pass) {
                if(Input.GetKeyDown(KeyCode.RightArrow) ||Input.GetKeyDown(KeyCode.D)) {
                    MapManager.Instance.MapRight();
                    pass = true;
                }
                if(Input.GetKeyDown(KeyCode.UpArrow ) || Input.GetKeyDown(KeyCode.W)) {
                    MapManager.Instance.MapUp();
                    pass = true;
                }
                yield return 0;
            }
            pass = false;

            yield return new WaitForSeconds(1);

            t = 20;

            //等待事件結束或自己按下空白鍵
            while(!pass) {

                t -= Time.deltaTime;
                if(Input.GetKeyDown(KeyCode.Space) || t < 0) {
                    pass = true;
                    EventManager.Instance.board.SetActive(false);
                    MusicManager.order.PlaySound(2);
                }
                yield return 0;
            }
            pass = false;

            //勇者歲數+1
            HeroManager.Instance.age += 1;
            yield return 0;

            //更新狀態版
            PanelManager._inst._Age.text = HeroManager.Instance.age.ToString();
            PanelManager._inst._Money.text = HeroManager.Instance.money.ToString();

            /*BagManager._instant.ShowBag();
            print("--------");*/

        }

        //顯示遊戲結束畫面
        blackBack.color = Color.black;
        replayHint.SetActive(true);
        lifeBoard.text = lifeStep;
        lifeBoard.gameObject.SetActive(true);
        tomb.SetActive(true);
        MusicManager.order.Stop();
        MusicManager.order.PlaySound(3);

        yield return new WaitForSeconds(2);

        //等玩家按下空白鍵重來
        while(true) {
            if(Input.GetKeyDown(KeyCode.Space)) {
                StopAllCoroutines();
                Application.LoadLevel(0);
            }
            yield return 0;
        }

    }

    
	
	
}
