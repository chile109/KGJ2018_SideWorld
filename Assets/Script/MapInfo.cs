using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//單塊地圖的資訊
[System.Serializable]
public class MapInfo : MonoBehaviour {

    //地圖使用的Sprite
    public SpriteRenderer sp;

    //地圖上的掉落物
    public SpriteRenderer item;

    //內藏事件
    public EventData eve;

    //設定事件
    public void SetEvent() {
        eve = EventManager.Instance.GetEventData();
    }

    //播放事件
	public void PlayEvent() {
        EventManager.Instance.PlayEvent(eve);
    }
}
