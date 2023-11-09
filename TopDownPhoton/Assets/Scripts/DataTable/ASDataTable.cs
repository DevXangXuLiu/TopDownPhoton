using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.Linq;
using System.Text.RegularExpressions;

public class ASDataTableOrigin : ScriptableObject
{
    public virtual void ImportData(string textData)
    {

    }

    public virtual string GetCSVData()
    {
        return string.Empty;
    }
}

public abstract class ConfigCompare<T> : IComparer<T>
{
    public int Compare(T x, T y)
    {
        return IcompareHandle(x, y);
    }

    public abstract int IcompareHandle(T x, T y);
    public abstract T SetKeySearch(object key);
}

public abstract class ASDataTable<T> : ASDataTableOrigin where T : class
{
    public ConfigCompare<T> recordCompare;
    [SerializeField]
    private List<T> records;

    private void OnEnable()
    {
        InitComparison();
    }

    public abstract void InitComparison();
    public override void ImportData(string textData)
    {
        if(records != null)
        {
            records.Clear();
        }
        else
        {
            records = new List<T>();
        }

        List<List<string>> grid = SplitCSVFile(textData);
        Type type = typeof(T);
        FieldInfo[] members = type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        for(int i=1;i<grid.Count;i++)
        {
            string jsonText = string.Empty;
            jsonText = "{";
            for(int j=0; j<members.Length; j++)
            {
                FieldInfo fieldInfo = members[j];
                if(j>0)
                {
                    jsonText += ",";
                }
                jsonText += "\"" + fieldInfo.Name + "\":" + "\"" + grid[i][j] + "\"";
            }
            jsonText += "}";
            T dataRecord = JsonUtility.FromJson<T>(jsonText);
            records.Add(dataRecord);
        }
        records.Sort(recordCompare);
    }

    public override string GetCSVData()
    {
        string s = string.Empty;
        Type mType = typeof(T);
        FieldInfo[] fieldInfos = mType.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        for(int x=0; x<fieldInfos.Length; x++)
        {
            if (x > 0)
                s += ",";
            s += fieldInfos[x].Name;
        }

        foreach(T e in records)
        {
            s += "\n";
            for(int x=0; x<fieldInfos.Length; x++)
            {
                if (x > 0)
                    s += ",";
                s += fieldInfos[x].GetValue(e);
            }
        }

        return s;
    }

    private List<List<string>> SplitCSVFile(string textInput)
    {
        List<List<string>> grid = new List<List<string>>();
        char lineSeparator = '\n';
        string[] lines = textInput.Split(lineSeparator);
        foreach(string e in lines)
        {
            string[] row = SplitCSVLine(e);
            List<string> r = new List<string>();
            foreach(string er in row)
            {
                r.Add(er);
            }

            grid.Add(r);
        }

        return grid;
    }

    private string[] SplitCSVLine(string line)
    {
        List<string> results = new List<string>();
        string pattern = @"(((?<x>(?=[,\r\n]+))|""(?<x>([^""]|"""")+)""|(?<x>[^,\r\n]+)),?)";
        MatchCollection matchCollection = Regex.Matches(line, pattern, RegexOptions.ExplicitCapture);
        foreach(Match e in matchCollection)
        {
            if(e.Groups["x"].Length > 0)
            {
                results.Add(e.Groups["x"].Value);
            }
        }

        return results.ToArray();
    }

    public T GetRecordByKeySearch(object key)
    {
        T item = recordCompare.SetKeySearch(key);
        int index = records.BinarySearch(item, recordCompare);

        return CopyData(records[index]);
    }

    private T CopyData(object data)
    {
        string s = JsonUtility.ToJson(data);
        return JsonUtility.FromJson<T>(s);
    }

    public List<T> GetAll()
    {
        List<T> ls = new List<T>();
        for(int i=0; i<records.Count; i++)
        {
            string s = JsonUtility.ToJson(records[i]);
            ls.Add(JsonUtility.FromJson<T>(s));
        }

        return ls;
    }
}
