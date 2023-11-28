using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogGunSelect : BaseDialog
{
    public Transform parentLayout;
    public ItemGunSelect itemGunPrefab;

    private List<ConfigWeaponRecord> lsConfigWeaponRecord;
    private List<ItemGunSelect> lsItem = new List<ItemGunSelect>();

    public override void OnSetup(DialogParam param)
    {
        if(lsConfigWeaponRecord == null)
        {
            lsConfigWeaponRecord = ConfigManager.instance.configWeapon.GetAll();
            foreach(ConfigWeaponRecord e in lsConfigWeaponRecord)
            {
                ItemGunSelect item = Instantiate(itemGunPrefab);
                item.transform.SetParent(parentLayout);
                lsItem.Add(item);
            }
        }

        for(int i=0; i<lsConfigWeaponRecord.Count; i++)
        {
            lsItem[i].Setup(lsConfigWeaponRecord[i]);
        }
    }

    public void OnClose()
    {
        DialogManager.instance.HideAllDialog(this.index);
    }
}
