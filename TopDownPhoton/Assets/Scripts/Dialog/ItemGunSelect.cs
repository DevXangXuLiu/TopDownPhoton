using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemGunSelect : MonoBehaviour
{
    public Image icon;
    public Text gunName;
    public Text power;

    private ConfigWeaponRecord configWeaponRecord;

    public void Setup(ConfigWeaponRecord cfGun)
    {
        if(this.configWeaponRecord != cfGun)
        {
            this.configWeaponRecord = cfGun;
            gunName.text = configWeaponRecord.name;
            icon.overrideSprite = Resources.Load("Icon/" + configWeaponRecord.prefab, typeof(Sprite)) as Sprite;
        }
    }

    public void OnSelectGun()
    {
        HomeView homeView = (HomeView)ViewManager.instance.currentView;
        homeView.SetGunInfo(configWeaponRecord.id);
    }
}
