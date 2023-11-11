using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Reflection;
using Newtonsoft.Json;

public class DataEventTrigger : UnityEvent<object>
{

}

public static class DataTrigger
{
    public static Dictionary<string, DataEventTrigger> dicOnValueChange = new Dictionary<string, DataEventTrigger>();
    public static void RegisterValueChange(string s, UnityAction<object> delegateDataChange)
    {
        if(dicOnValueChange.ContainsKey(s))
        {
            dicOnValueChange[s].AddListener(delegateDataChange);
        }
        else
        {
            dicOnValueChange.Add(s, new DataEventTrigger());
            dicOnValueChange[s].AddListener(delegateDataChange);
        }
    }

    public static void TriggerEventData(this object data, string path)
    {
        if (dicOnValueChange.ContainsKey(path))
            dicOnValueChange[path].Invoke(data);
    }
}

public class DatabaseLocal : MonoBehaviour
{
    private PlayerData dataPlayer;

    private void GetData()
    {
        string s = PlayerPrefs.GetString("DATA");
        dataPlayer = JsonConvert.DeserializeObject<PlayerData>(s);
    }

    private void SaveData()
    {
        string s = JsonConvert.SerializeObject(dataPlayer, Formatting.None);
        PlayerPrefs.SetString("DATA", s);
    }

    public void CreateNewData(PlayerData data)
    {
        dataPlayer = data;
        SaveData();
    }

    public bool LoadData()
    {
        if(PlayerPrefs.HasKey("DATA"))
        {
            GetData();
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public T Read<T>(string path)
    {
        object data = null;
        string[] s = path.Split('/');
        List<string> paths = new List<string>();
        paths.AddRange(s);
        ReadDataByPath(paths, dataPlayer, out data);

        return (T)data;
    }

    public T Read<T>(string path, object key)
    {
        object data = null;
        string[] s = path.Split('/');
        List<string> paths = new List<string>();
        paths.AddRange(s);
        ReadDataByPath(paths, dataPlayer, out data);
        Dictionary<string, T> newDic = (Dictionary<string, T>)data;

        return newDic[key.ToKey()];
    }

    private void ReadDataByPath(List<string> paths, object data, out object dataOut)
    {
        string p = paths[0];
        Type t = data.GetType();
        FieldInfo field = t.GetField(p);
        if(paths.Count == 1)
        {
            dataOut = field.GetValue(data);
        }
        else
        {
            paths.RemoveAt(0);
            ReadDataByPath(paths, field.GetValue(data), out dataOut);
        }
    }

    public void UpdateData(string path, object dataNew, Action callback)
    {
        string[] s = path.Split('/');
        List<string> paths = new List<string>();
        paths.AddRange(s);
        UpdateDataByPath(paths, dataPlayer, dataNew, callback);
        SaveData();
        dataNew.TriggerEventData(path);
    }

    private void UpdateDataByPath(List<string> paths, object data, object dataNew, Action callback)
    {
        string p = paths[0];
        Type t = data.GetType();
        FieldInfo field = t.GetField(p);
        if(paths.Count == 1)
        {
            field.SetValue(data, dataNew);
            if (callback != null)
                callback();
        }
        else
        {
            paths.RemoveAt(0);
            UpdateDataByPath(paths, field.GetValue(data), dataNew, callback);
        }
    }

    public void UpdateData<TValue>(string path, object key, TValue dataNew, Action callback)
    {
        string[] s = path.Split('/');
        List<string> paths = new List<string>();
        paths.AddRange(s);
        UpdateDataDicByPath(paths, dataPlayer, key, dataNew, callback);
        SaveData();
        dataNew.TriggerEventData(path);
    }

    private void UpdateDataDicByPath<TValue>(List<string> paths, object data, object key, TValue dataNew, Action callback)
    {
        string p = paths[0];
        Type t = data.GetType();
        FieldInfo field = t.GetField(p);
        if(paths.Count == 1)
        {
            object dic = field.GetValue(data);
            Dictionary<string, TValue> newDic = (Dictionary<string, TValue>)dic;
            newDic[key.ToKey()] = dataNew;
            field.SetValue(data, newDic);
            if (callback != null)
                callback();
        }
        else
        {
            paths.RemoveAt(0);
            UpdateDataDicByPath(paths, field.GetValue(data), key, dataNew, callback);
        }
    }
}
