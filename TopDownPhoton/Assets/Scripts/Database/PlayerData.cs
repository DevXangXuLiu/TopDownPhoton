using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerData
{
    [SerializeField]
    public PlayerInfo playerInfo = new PlayerInfo();
    [SerializeField]
    public PlayerInventory playerInventory = new PlayerInventory();
    [SerializeField]
    public Dictionary<string, WeaponData> weapons = new Dictionary<string, WeaponData>();
}

[Serializable]
public class PlayerInfo
{
    public string username;
    public int exp;
    public int level;
    public int idGun;
    public int idSkin;
}

[Serializable]
public class PlayerInventory
{    
    public int cash;
    public int gold;
}

[Serializable]
public class WeaponData
{
    public int id;
    public int level;
}
