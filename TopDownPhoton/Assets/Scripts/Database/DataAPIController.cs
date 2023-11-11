using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DataAPIController : Singleton<DataAPIController>
{
    [SerializeField]
    private DatabaseLocal dataModel;

    public void OnInit(Action callback)
    {
        if(dataModel.LoadData())
        {
            callback?.Invoke();
        }
        else
        {
            PlayerData playerData = new PlayerData();

            PlayerInfo playerInfo = new PlayerInfo();
            playerInfo.username = "Hero";
            playerInfo.exp = 0;
            playerInfo.level = 1;
            playerInfo.idGun = 1;
            playerInfo.idSkin = 1;

            PlayerInventory playerInventory = new PlayerInventory();
            playerInventory.cash = 500;
            playerInventory.gold = 50;

            WeaponData weaponData = new WeaponData();
            weaponData.id = 1;
            weaponData.level = 1;            

            playerData.playerInfo = playerInfo;
            playerData.playerInventory = playerInventory;
            dataModel.CreateNewData(playerData);
            dataModel.UpdateData<WeaponData>(DataPath.PLAYER_WEAPON, 1, weaponData, callback);
        }
    }

    public PlayerInfo GetPlayerInfo()
    {
        return dataModel.Read<PlayerInfo>(DataPath.PLAYER_INFO);
    }

    public PlayerInventory GetPlayerInventory()
    {
        return dataModel.Read<PlayerInventory>(DataPath.PLAYER_INVENTORY);
    }

    public void ChangeGun(int id, Action callback)
    {
        dataModel.UpdateData(DataPath.PLAYER_INFO_WEAPON, id, callback);
    }

    public void ChangeSkin(int id, Action callback)
    {
        dataModel.UpdateData(DataPath.PLAYER_INFO_SKIN, id, callback);
    }

    public WeaponData GetWeaponData(int id)
    {
        return dataModel.Read<WeaponData>(DataPath.PLAYER_WEAPON, id);
    }
}
