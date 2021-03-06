﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//地圖控制程式
//用MoveUp()   MoveRight()啟動地圖移動機制
//

public enum MapPos {
    Start = 0,
    Right,
    Up,
    End,        //右下
}

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

    //腳色捷徑
    public Transform hero;

    //地圖塊淡出時間
    public float mapOut = 0.1f;

    //地圖目前方塊
    public MapInfo[] mapData = new MapInfo[4];

    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    //地圖移動指令
    public void MapUp() {
        StartCoroutine("TranslateUp");
    }

    //地圖移動指令
    public void MapRight() {
        StartCoroutine("TranslateRight");
    }

    public IEnumerator TranslateUp() {
        //重置位置
        mapData[(int)MapPos.Start].transform.localPosition = new Vector3(-1.1f,0,-1.1f);
        mapData[(int)MapPos.Right].transform.localPosition = new Vector3(1.1f, 0, -1.1f);
        mapData[(int)MapPos.Up].transform.localPosition = new Vector3(-1.1f, 0, 1.1f);
        mapData[(int)MapPos.End].transform.localPosition = new Vector3(1.1f, 0, 1.1f);
        hero.transform.localPosition = new Vector3(-1.1f, 0.5f, -1.1f);

        //需要的秒數
        float t = 0;

        //移動的距離(2.2)
        float dis = 2.2f;

        //移動腳色
        Vector3 oriPos = hero.localPosition;
        Vector3 targetPos = oriPos;
        targetPos.z = oriPos.z + dis;

        //播放動畫
        hero.GetComponent<Animator>().Play("HeroWalk");
        MusicManager.order.PlaySound(8);

        while(t < 1) {
            t += Time.deltaTime*2;
            //計算比例
            float per = Mathf.InverseLerp(0,1,t);
            
            hero.localPosition = Vector3.Lerp(oriPos,targetPos,per);
            yield return 0;
        }

        //播放動畫
        hero.GetComponent<Animator>().Play("Stop");

        //隱藏圖示
        t = mapOut;
        while(t > 0) {
            t -= Time.deltaTime;
            Color c = Color.white;
            c.a = t * 10;
            mapData[(int)MapPos.Right].sp.color = c;
            yield return 0;
        }

        mapData[(int)MapPos.Right].transform.Translate(0, 0, dis * 2 * transform.localScale.x);
        //要求新事件
        mapData[(int)MapPos.Right].eve = EventManager.Instance.GetEventData();
        //依事件擺圖
        Sprite myImg = Resources.Load<Sprite>("Event/" + mapData[(int)MapPos.Right].eve.Sn.ToString());
        mapData[(int)MapPos.Right].item.sprite = myImg;
        mapData[(int)MapPos.Right].item.gameObject.SetActive(true);

        //隱藏圖示
        t = mapOut;
        while(t > 0) {
            t -= Time.deltaTime;
            Color c = Color.white;
            c.a = t * 10;
            mapData[(int)MapPos.Start].sp.color = c;
            yield return 0;
        }

        mapData[(int)MapPos.Start].transform.Translate(0, 0, dis * 2 * transform.localScale.x);
        //要求新事件
        mapData[(int)MapPos.Start].eve = EventManager.Instance.GetEventData();

        //依事件擺圖
        myImg = Resources.Load<Sprite>("Event/" + mapData[(int)MapPos.Start].eve.Sn.ToString());
        mapData[(int)MapPos.Start].item.sprite = myImg;
        mapData[(int)MapPos.Start].item.gameObject.SetActive(true);

        //設定新材質
        mapData[(int)MapPos.Start].sp.sprite = mapData[(int)MapPos.Up].sp.sprite;
        mapData[(int)MapPos.Right].sp.sprite = mapData[(int)MapPos.End].sp.sprite;

        //顯示新圖
        t = 0f;
        while(t < mapOut) {
            t += Time.deltaTime;
            Color c = Color.white;
            c.a = t * 10;
            mapData[(int)MapPos.Right].sp.color = c;
            yield return 0;
        }

        yield return new WaitForSeconds(0.1f);
        t = 0f;
        while(t < mapOut) {
            t += Time.deltaTime;
            Color c = Color.white;
            c.a = t * 10;
            mapData[(int)MapPos.Start].sp.color = c;
            yield return 0;
        }

        //重整座標
        MapInfo[] temp = new MapInfo[4];
        temp[(int)MapPos.Start] = mapData[(int)MapPos.Up];
        temp[(int)MapPos.Up] = mapData[(int)MapPos.Start];
        temp[(int)MapPos.Right] = mapData[(int)MapPos.End];
        temp[(int)MapPos.End] = mapData[(int)MapPos.Right];

        mapData = temp;

        for(int i = 0; i < mapData.Length; i++) {
            mapData[i].name = ((MapPos)i).ToString();
        }

        //地圖向下
        t = 0f;
        Vector3[] oriP = new Vector3[4];
        Vector3[] tarP = new Vector3[4];
        for(int i = 0; i < mapData.Length; i++) {
            oriP[i] = mapData[i].transform.localPosition;
            tarP[i] = mapData[i].transform.localPosition;
            tarP[i].z -= dis; 
        }
        while(t < 1) {
            t += Time.deltaTime*2;
            float per = Mathf.InverseLerp(0, 1, t);
            Vector3 p = Vector3.Lerp(oriP[(int)MapPos.Start], tarP[(int)MapPos.Start], per);
            p.y = 0.5f;
            hero.transform.localPosition = p;
            for(int i = 0; i < mapData.Length; i++) {
                mapData[i].transform.localPosition = Vector3.Lerp(oriP[i],tarP[i],per);
            }
            yield return 0;
        }

        //執行事件
        EventManager.Instance.PlayEvent(mapData[(int)MapPos.Start].eve);
        //隱藏小圖示
        mapData[(int)MapPos.Start].item.gameObject.SetActive(false);

        yield return 0;
    }

    public IEnumerator TranslateRight() {
        //回復位置
        mapData[(int)MapPos.Start].transform.localPosition = new Vector3(-1.1f, 0, -1.1f);
        mapData[(int)MapPos.Right].transform.localPosition = new Vector3(1.1f, 0, -1.1f);
        mapData[(int)MapPos.Up].transform.localPosition = new Vector3(-1.1f, 0, 1.1f);
        mapData[(int)MapPos.End].transform.localPosition = new Vector3(1.1f, 0, 1.1f);
        hero.transform.localPosition = new Vector3(-1.1f, 0.5f, -1.1f);

        //需要的秒數
        float t = 0;

        //移動的距離(2.2)
        float dis = 2.2f;

        //移動腳色
        Vector3 oriPos = hero.localPosition;
        Vector3 targetPos = oriPos;
        targetPos.x = oriPos.x + dis;

        //播放動畫
        hero.GetComponent<Animator>().Play("HeroWalk");

        while(t < 1) {
            t += Time.deltaTime*2;
            //計算比例
            float per = Mathf.InverseLerp(0, 1, t);

            hero.localPosition = Vector3.Lerp(oriPos, targetPos, per);
            yield return 0;
        }

        //播放動畫
        hero.GetComponent<Animator>().Play("Stop");

        //隱藏圖示
        t = mapOut;
        while(t > 0) {
            t -= Time.deltaTime;
            Color c = Color.white;
            c.a = t * 10;
            mapData[(int)MapPos.Up].sp.color = c;
            yield return 0;
        }

        mapData[(int)MapPos.Up].transform.Translate(dis * 2 * transform.localScale.x, 0, 0);
        //要求新事件
        mapData[(int)MapPos.Up].eve = EventManager.Instance.GetEventData();
        //依事件擺圖
        
        Sprite myImg = Resources.Load<Sprite>("Event/" + mapData[(int)MapPos.Up].eve.Sn.ToString());
        mapData[(int)MapPos.Up].item.sprite = myImg;
        mapData[(int)MapPos.Up].item.gameObject.SetActive(true);

        //隱藏圖示
        t = mapOut;
        while(t > 0) {
            t -= Time.deltaTime;
            Color c = Color.white;
            c.a = t * 10;
            mapData[(int)MapPos.Start].sp.color = c;
            yield return 0;
        }

        mapData[(int)MapPos.Start].transform.Translate(dis * 2 * transform.localScale.x, 0, 0);
        //要求新事件
        mapData[(int)MapPos.Start].eve = EventManager.Instance.GetEventData();

        //依事件擺圖
        myImg = Resources.Load<Sprite>("Event/" + mapData[(int)MapPos.Start].eve.Sn.ToString());
        mapData[(int)MapPos.Start].item.sprite = myImg;
        mapData[(int)MapPos.Start].item.gameObject.SetActive(true);

        //設定新材質
        mapData[(int)MapPos.Start].sp.sprite = mapData[(int)MapPos.Right].sp.sprite;
        mapData[(int)MapPos.Up].sp.sprite = mapData[(int)MapPos.End].sp.sprite;

        //顯示新圖
        yield return new WaitForSeconds(0.3f);
        t = 0f;
        while(t < mapOut) {
            t += Time.deltaTime;
            Color c = Color.white;
            c.a = t * 10;
            mapData[(int)MapPos.Up].sp.color = c;
            yield return 0;
        }

        t = 0f;
        while(t < mapOut) {
            t += Time.deltaTime;
            Color c = Color.white;
            c.a = t * 10;
            mapData[(int)MapPos.Start].sp.color = c;
            yield return 0;
        }

        //重整座標
        MapInfo[] temp = new MapInfo[4];
        temp[(int)MapPos.Start] = mapData[(int)MapPos.Right];
        temp[(int)MapPos.Up] = mapData[(int)MapPos.End];
        temp[(int)MapPos.Right] = mapData[(int)MapPos.Start];
        temp[(int)MapPos.End] = mapData[(int)MapPos.Up];

        mapData = temp;

        for(int i = 0; i < mapData.Length; i++) {
            mapData[i].name = ((MapPos)i).ToString();
        }

        //地圖向右
        t = 0f;
        Vector3[] oriP = new Vector3[4];
        Vector3[] tarP = new Vector3[4];
        
        for(int i = 0; i < mapData.Length; i++) {
            oriP[i] = mapData[i].transform.localPosition;
            tarP[i] = mapData[i].transform.localPosition;
            tarP[i].x -= dis;
        }
        while(t < 1) {
            t += Time.deltaTime*2;
            float per = Mathf.InverseLerp(0, 1, t);
            Vector3 p = Vector3.Lerp(oriP[(int)MapPos.Start], tarP[(int)MapPos.Start], per);
            p.y = 0.5f;
            hero.transform.localPosition = p;
            for(int i = 0; i < mapData.Length; i++) {
                mapData[i].transform.localPosition = Vector3.Lerp(oriP[i], tarP[i], per);
            }
            yield return 0;
        }

        //執行事件
        EventManager.Instance.PlayEvent(mapData[(int)MapPos.Start].eve);
        //隱藏小圖示
        mapData[(int)MapPos.Start].item.gameObject.SetActive(false);   

        yield return 0;
    }

    
}
