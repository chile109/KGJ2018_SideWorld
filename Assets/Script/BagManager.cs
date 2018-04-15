using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class BagManager : MonoBehaviour
{

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

	public void GotItem(ItemData _data)
    {
        foreach(var i in BagContents)
        {
            if (i.name == "empty")
            {
                i.gameObject.name = _data.name;
                Sprite myImg = Resources.Load<Sprite>("item/" + _data.sn);
                i._Img.sprite = myImg;
                i.gameObject.SetActive(true);
                break;
            }
        }
    }
    public bool LostItem(ItemData _data)
    {
        foreach (var i in BagContents)
        {
            if (i.name == _data.sn)
            {
                i.gameObject.name = "empty";
                i._Img.sprite = null;
                i.gameObject.SetActive(false);
                return true;
                break;
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
