using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeView : BaseView
{
    [Header("----------------SKIN----------------")]
    public Text nameSkin;
    public Text hp_SkinInfo;

    [Header("----------------GUN----------------")]
    public Text gunName;
    public Text damage;
    public Image damageAmount;
    public Text clipSize;
    public Image clipSizeAmount;
    public Text rof;
    public Image rofAmount;

    private ConfigWeaponRecord configGun;
    private ConfigSkinRecord configSkin;

    public override void OnSetup(ViewParam param)
    {
        PlayerInfo playerInfo = DataAPIController.instance.GetPlayerInfo();

        configGun = ConfigManager.instance.configWeapon.GetRecordByKeySearch(playerInfo.idGun);
        ChangeGunInfoUI(configGun);
        DataTrigger.RegisterValueChange(DataPath.PLAYER_INFO_WEAPON, (idGun) =>
        {
            configGun = ConfigManager.instance.configWeapon.GetRecordByKeySearch((int)idGun);
            ChangeGunInfoUI(configGun);
        });

        configSkin = ConfigManager.instance.configSkin.GetRecordByKeySearch(playerInfo.idSkin);
        ChangeSkinInfoUI(configSkin);
        DataTrigger.RegisterValueChange(DataPath.PLAYER_INFO_SKIN, (idSkin) =>
        {
            configSkin = ConfigManager.instance.configSkin.GetRecordByKeySearch((int)idSkin);
            ChangeSkinInfoUI(configSkin);
        });
    }

    private void ChangeGunInfoUI(ConfigWeaponRecord cfWeaponRecord)
    {
        configGun = cfWeaponRecord;
        gunName.text = cfWeaponRecord.name;

        damage.text = cfWeaponRecord.damage.ToString();
        damageAmount.fillAmount = (float)cfWeaponRecord.damage / 30f;

        clipSize.text = cfWeaponRecord.clipsize.ToString();
        clipSizeAmount.fillAmount = (float)cfWeaponRecord.clipsize / 100f;

        rof.text = cfWeaponRecord.rof.ToString();
        rofAmount.fillAmount = 0.1f / cfWeaponRecord.rof;
    }

    private void ChangeSkinInfoUI(ConfigSkinRecord cfSkinRecord)
    {
        configSkin = cfSkinRecord;
        nameSkin.text = cfSkinRecord.name;
        hp_SkinInfo.text = cfSkinRecord.hp.ToString();
    }

    public void OnGunSelect()
    {

    }

    public void OnSkinSelect()
    {

    }

    public void SetGunInfo(int id)
    {
        DataAPIController.instance.ChangeGun(id, null);
    }

    public void SetSkinInfo(int id)
    {
        DataAPIController.instance.ChangeSkin(id, null);
    }

    public void OnFight()
    {
        MatchViewParam param = new MatchViewParam();
        param.configGun = this.configGun;
        ViewManager.instance.OnSwitchView(ViewIndex.MatchView, param);
    }
}
