using UnityEngine;
using LitJson;
using System.Collections.Generic;

public class ItemData
{
    public string sn;
    public string name;
}

public class EventData
{
    public string Sn;
    public string Name;
    public string DescriptionGood;
    public string DescriptionBad;
    public int[] got = new int[4];
    public int[] Lost = new int[4];
    public int GetMoney;
    public int LostMoney;
    public int MinAge;
    public int MaxAge;
    public int GoodPic;
    public int BadPic;
    public int Repeat;
    public int End;
}

public class PlayerData
{
    public string Sn;
    public string Name;
    public string Description;
    public int Money;
    public int[] Item = new int[4];
}

public class JsonLoader : MonoBehaviour {

    public static List<ItemData> ItemPool = new List<ItemData>();
    public static List<EventData> EventPool = new List<EventData>();
    public static List<PlayerData> refPool = new List<PlayerData>();

	void Start ()
    {      
        //Debug.LogFormat("txt is {0}", txt.text);
        //Debug.LogFormat("Count = {0}", jsonData.Count);

        loadItem();
        loadEvent();
        loadRef();

        foreach (var i in refPool)
        {
            Debug.Log(i.Name);
        }
    }

    private static void loadItem()
    {
        if (Resources.Load("item") != null)
        {
            TextAsset txt = (Resources.Load("item")) as TextAsset;
            JsonReader jsonary = new JsonReader(txt.text);
            var jsonData = JsonMapper.ToObject(jsonary);

            for (int i = 0; i < jsonData.Count; ++i)
            {
                ItemData _item = new ItemData();

                _item.sn = jsonData[i].ToString();
                _item.name = jsonData[i]["Name"].ToString();

                ItemPool.Add(_item);

            }
        }
    }

    private static void loadEvent()
    {
        if (Resources.Load("event") != null)
        {
            TextAsset txt = (Resources.Load("event")) as TextAsset;
            JsonReader jsonary = new JsonReader(txt.text);
            var jsonData = JsonMapper.ToObject(jsonary);

            Debug.LogFormat("txt is {0}", txt.text);

            for (int i = 0; i < jsonData.Count; ++i)
            {
                if (jsonData[i].ToString() != "")
                {
                    EventData _event = new EventData();

                    _event.Sn = jsonData[i]["Sn"].ToString();
                    _event.Name = jsonData[i]["Name"].ToString();
                    _event.DescriptionGood = jsonData[i]["DescriptionGood"].ToString();
                    _event.DescriptionBad = jsonData[i]["DescriptionGood"].ToString();

                    _event.got[1] = (int)jsonData[i]["Get1"];
                    _event.got[2] = (int)jsonData[i]["Get2"];
                    _event.got[3] = (int)jsonData[i]["Get3"];

                    _event.Lost[0] = (int)jsonData[i]["Lost0"];
                    _event.Lost[1] = (int)jsonData[i]["Lost1"];
                    _event.Lost[2] = (int)jsonData[i]["Lost2"];
                    _event.Lost[3] = (int)jsonData[i]["Lost3"];

                    _event.GetMoney = (int)jsonData[i]["GetMoney"];
                    _event.LostMoney = (int)jsonData[i]["LostMoney"];
                    _event.MinAge = (int)jsonData[i]["MinAge"];
                    _event.MaxAge = (int)jsonData[i]["MaxAge"];
                    _event.GoodPic = (int)jsonData[i]["GoodPic"];
                    _event.BadPic = (int)jsonData[i]["BadPic"];
                    _event.Repeat = (int)jsonData[i]["Repeat"];
                    _event.End = (int)jsonData[i]["End"];

                    EventPool.Add(_event);
                }
            }
        }
    }

    private static void loadRef()
    {
        if (Resources.Load("Born") != null)
        {
            TextAsset txt = (Resources.Load("Born")) as TextAsset;
            JsonReader jsonary = new JsonReader(txt.text);
            var jsonData = JsonMapper.ToObject(jsonary);

            for (int i = 0; i < jsonData.Count; ++i)
            {
                PlayerData _ref = new PlayerData();

                _ref.Sn = jsonData[i].ToString();
                _ref.Name = jsonData[i]["Name"].ToString();
                _ref.Description = jsonData[i]["Description"].ToString();
                _ref.Money = (int) jsonData[i]["Money"];

                _ref.Item[0] = (int)jsonData[i]["Item0"];
                _ref.Item[1] = (int)jsonData[i]["Item1"];
                _ref.Item[2] = (int)jsonData[i]["Item2"];
                _ref.Item[3] = (int)jsonData[i]["Item3"];


                refPool.Add(_ref);

            }
        }
    }
}
