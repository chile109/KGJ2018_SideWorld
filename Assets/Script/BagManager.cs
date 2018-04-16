using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BagManager : MonoBehaviour
{
    public GameObject Diolog;
    public Image _icon;
    public Text _log;
    public static BagManager _instant;
    public BagItem[] BagContents;
    // Use this for initialization
    void Start()
    {
        if (_instant == null)
        {
            BagManager._instant = this;
        }

        BagContents = this.gameObject.GetComponentsInChildren<BagItem>();

        ClearImg();

    }

    //顯示背包物件
    public void ShowBag() {
        foreach(var i in BagContents) {
            if(i.name != "empty") {
                print(i.name);
            }
        }
    } 

    IEnumerator showLog(Sprite myImg, string s)
    {
        _icon.sprite = myImg;
        _log.text = s;
        Diolog.SetActive(true);
        yield return new WaitForSeconds(2f);
        Diolog.SetActive(false);
    }

    public void GotItem(ItemData _data)
    {
        foreach (var i in BagContents)
        {
            if (i.name == "empty")
            {
                i.gameObject.name = _data.name;
                Sprite myImg = Resources.Load<Sprite>("item/" + _data.sn.ToString());
                //Debug.Log("item/" + _data.sn);
                i._Img.sprite = myImg;
                i.gameObject.SetActive(true);

                _icon.sprite = myImg;
                string tmp = "你獲得了" + _data.name + "!";
                StartCoroutine(showLog(myImg, tmp));
                break;
            }
        }
    }
    public bool LostItem(ItemData _data)
    {
        foreach (var i in BagContents)
        {
            if (i.name == _data.name.ToString())
            {
                i.gameObject.name = "empty";
                i._Img.sprite = null;
                i.gameObject.SetActive(false);

                Sprite myImg = Resources.Load<Sprite>("item/" + _data.sn.ToString());
                _icon.sprite = myImg;
                string tmp = "你失去了" + _data.name + "!";
                StartCoroutine(showLog(myImg, tmp));
                return true;
            }

        }

        return false;
    }

    public void ClearImg()
    {
        foreach (var i in BagContents)
        {
            i.gameObject.name = "empty";
            i._Img.sprite = null;
            i.gameObject.SetActive(false);
        }
    }
}
