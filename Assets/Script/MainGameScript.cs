using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//主要遊戲流程
//利用一個主協程GameThread進行

public class MainGameScript : MonoBehaviour {

    //標題畫面
    public RawImage titleImage;

    //背景黑幕
    public RawImage blackBack;

    //英雄描述
    public Text heroInfo;

    //驗證用參數
    public bool pass = false;

	// Use this for initialization
	void Start () {
        //開啟協程
        StartCoroutine("GameThread");
	}

    public IEnumerator GameThread() {
        //等待滑鼠點擊
        while(!pass) {
            if(Input.GetMouseButtonUp(0)) {
                pass = true;
            }
            yield return 0;
        }
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
            if(Input.GetMouseButtonUp(0)) {
                pass = true;
            }
            yield return 0;
        }
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

        yield return new WaitForSeconds(1);
        //背景淡出
        t = 1;
        while(t > 0) {
            t -= Time.deltaTime;
            Color c = Color.black;
            c.a = t;
            blackBack.color = c;
            yield return 0;
        }

        yield return new WaitForSeconds(2);

        bool gameOver = false;

        //等待遊戲結束(GameOver)
        while(!gameOver) {
            //等待玩家按按鈕
            while(!pass) {
                if(Input.GetKeyDown(KeyCode.RightArrow)) {
                    MapManager.Instance.MapRight();
                    pass = true;
                }
                if(Input.GetKeyDown(KeyCode.UpArrow)) {
                    MapManager.Instance.MapUp();
                    pass = true;
                }
                yield return 0;
            }
            pass = false;
            //等待事件結束
            while(!pass) {
                yield return 0;
            }
            yield return 0;
        }

        //顯示遊戲結束畫面
        

    }

    
	
	
}
