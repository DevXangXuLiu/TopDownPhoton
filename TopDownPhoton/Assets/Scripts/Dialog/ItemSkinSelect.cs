using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSkinSelect : MonoBehaviour
{
    public Image icon;
    public Text skinName;
    public Text HP;

    private ConfigSkinRecord configSkin;

    public void Setup(ConfigSkinRecord cfSkin)
    {
        this.configSkin = cfSkin;
        skinName.text = configSkin.name;
        icon.overrideSprite = Resources.Load("Icon/" + configSkin.prefab, typeof(Sprite)) as Sprite;
    }

    public void OnSelectSkin()
    {
        HomeView homeView = (HomeView)ViewManager.instance.currentView;
        homeView.SetSkinInfo(configSkin.id);
    }
}
