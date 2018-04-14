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
    public string sn;
    public string name;
}

public class PlayerData
{
    public string sn;
    public string name;
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

        foreach (var i in ItemPool)
        {
            Debug.Log(i.name);
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

            for (int i = 0; i < jsonData.Count; ++i)
            {
                EventData _event = new EventData();

                _event.sn = jsonData[i].ToString();
                _event.name = jsonData[i]["Event"].ToString();

                EventPool.Add(_event);

            }
        }
    }

    private static void loadRef()
    {
        if (Resources.Load("PlerRef") != null)
        {
            TextAsset txt = (Resources.Load("PlerRef")) as TextAsset;
            JsonReader jsonary = new JsonReader(txt.text);
            var jsonData = JsonMapper.ToObject(jsonary);

            for (int i = 0; i < jsonData.Count; ++i)
            {
                PlayerData _ref = new PlayerData();

                _ref.sn = jsonData[i].ToString();
                _ref.name = jsonData[i]["Event"].ToString();

                refPool.Add(_ref);

            }
        }
    }
}
