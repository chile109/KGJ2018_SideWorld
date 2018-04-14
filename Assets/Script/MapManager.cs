using System.Collections;
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

    //地圖動畫
    public Animator ani;

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
        ani.Play("None");
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

        while(t < 1) {
            t += Time.deltaTime;
            //計算比例
            float per = Mathf.InverseLerp(0,1,t);
            
            hero.localPosition = Vector3.Lerp(oriPos,targetPos,per);
            yield return 0;
        }

        //播放動畫
        hero.GetComponent<Animator>().Play("Stop");

        yield return new WaitForSeconds(1);

        //移動方塊
        yield return new WaitForSeconds(0.3f);
        //隱藏圖示
        t = 0.3f;
        while(t > 0) {
            t -= Time.deltaTime;
            Color c = Color.white;
            c.a = t * 3;
            mapData[(int)MapPos.Right].sp.color = c;
            yield return 0;
        }

        mapData[(int)MapPos.Right].transform.Translate(0, 0, dis * 2);

        yield return new WaitForSeconds(0.3f);
        //隱藏圖示
        t = 0.3f;
        while(t > 0) {
            t -= Time.deltaTime;
            Color c = Color.white;
            c.a = t * 3;
            mapData[(int)MapPos.Start].sp.color = c;
            yield return 0;
        }

        mapData[(int)MapPos.Start].transform.Translate(0, 0, dis * 2);

        //設定新材質
        mapData[(int)MapPos.Start].sp.sprite = mapData[(int)MapPos.Up].sp.sprite;
        mapData[(int)MapPos.Right].sp.sprite = mapData[(int)MapPos.End].sp.sprite;

        //顯示新圖
        yield return new WaitForSeconds(0.3f);
        t = 0f;
        while(t < 0.35) {
            t += Time.deltaTime;
            Color c = Color.white;
            c.a = t * 3;
            mapData[(int)MapPos.Right].sp.color = c;
            yield return 0;
        }

        yield return new WaitForSeconds(0.3f);
        t = 0f;
        while(t < 0.35) {
            t += Time.deltaTime;
            Color c = Color.white;
            c.a = t * 3;
            mapData[(int)MapPos.Start].sp.color = c;
            yield return 0;
        }

        MapInfo[] temp = new MapInfo[4];
        temp[(int)MapPos.Start] = mapData[(int)MapPos.Up];
        temp[(int)MapPos.Up] = mapData[(int)MapPos.Start];
        temp[(int)MapPos.Right] = mapData[(int)MapPos.End];
        temp[(int)MapPos.End] = mapData[(int)MapPos.Right];

        mapData = temp;

        //地圖向下
        ani.Play("MapMove_Up");

        yield return new WaitForSeconds(2);

        yield return 0;
    }

    public IEnumerator TranslateRight() {
        //回復位置
        ani.Play("None");
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
            t += Time.deltaTime;
            //計算比例
            float per = Mathf.InverseLerp(0, 1, t);

            hero.localPosition = Vector3.Lerp(oriPos, targetPos, per);
            yield return 0;
        }

        //播放動畫
        hero.GetComponent<Animator>().Play("Stop");

        yield return new WaitForSeconds(1);

        //移動方塊
        yield return new WaitForSeconds(0.3f);
        //隱藏圖示
        t = 0.3f;
        while(t > 0) {
            t -= Time.deltaTime;
            Color c = Color.white;
            c.a = t * 3;
            mapData[(int)MapPos.Up].sp.color = c;
            yield return 0;
        }

        mapData[(int)MapPos.Up].transform.Translate(dis * 2, 0, 0);

        yield return new WaitForSeconds(0.3f);
        //隱藏圖示
        t = 0.3f;
        while(t > 0) {
            t -= Time.deltaTime;
            Color c = Color.white;
            c.a = t * 3;
            mapData[(int)MapPos.Start].sp.color = c;
            yield return 0;
        }

        mapData[(int)MapPos.Start].transform.Translate(dis * 2, 0, 0);

        //設定新材質
        mapData[(int)MapPos.Start].sp.sprite = mapData[(int)MapPos.Right].sp.sprite;
        mapData[(int)MapPos.Up].sp.sprite = mapData[(int)MapPos.End].sp.sprite;

        //顯示新圖
        yield return new WaitForSeconds(0.3f);
        t = 0f;
        while(t < 0.35) {
            t += Time.deltaTime;
            Color c = Color.white;
            c.a = t * 3;
            mapData[(int)MapPos.Up].sp.color = c;
            yield return 0;
        }

        yield return new WaitForSeconds(0.3f);
        t = 0f;
        while(t < 0.35) {
            t += Time.deltaTime;
            Color c = Color.white;
            c.a = t * 3;
            mapData[(int)MapPos.Start].sp.color = c;
            yield return 0;
        }

        MapInfo[] temp = new MapInfo[4];
        temp[(int)MapPos.Start] = mapData[(int)MapPos.Right];
        temp[(int)MapPos.Up] = mapData[(int)MapPos.End];
        temp[(int)MapPos.Right] = mapData[(int)MapPos.Start];
        temp[(int)MapPos.End] = mapData[(int)MapPos.Up];

        mapData = temp;

        //地圖向右
        ani.Play("MapMove_Right");

        yield return new WaitForSeconds(2);


        yield return 0;
    }
}
