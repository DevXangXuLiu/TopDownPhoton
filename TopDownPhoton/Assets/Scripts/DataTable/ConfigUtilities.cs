using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public static class ConfigUtilities
{
    public static List<int> GetListDataByString(this string data, char key)
    {
        List<int> ls = new List<int>();
        string[] s = data.Split(key);
        foreach(string e in s)
        {
            ls.Add(int.Parse(e));
        }

        return ls;
    }
}

public class ConfigPrimaryKeyCompare<T2> : ConfigCompare<T2> where T2 : class, new()
{
    private FieldInfo keyField;

    public ConfigPrimaryKeyCompare(string keyFieldName)
    {
        keyField = typeof(T2).GetField(keyFieldName);
    }

    public override int IcompareHandle(T2 x, T2 y)
    {
        var val_x = keyField.GetValue(x);
        var val_y = keyField.GetValue(y);

        if(val_x == null && val_y == null)
        {
            return 0;
        }
        else if(val_x != null && val_y == null)
        {
            return 1;
        }
        else if(val_x == null && val_y != null)
        {
            return -1;
        }
        else
        {
            return ((IComparable)val_x).CompareTo(val_y);
        }
    }

    public override T2 SetKeySearch(object key)
    {
        T2 data = new T2();
        keyField.SetValue(data, key);
        return data;
    }
}
