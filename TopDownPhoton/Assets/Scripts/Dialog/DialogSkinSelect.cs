using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSkinSelect : BaseDialog
{
    public Transform parentLayout;
    public ItemSkinSelect itemSkinSelectPrefab;

    private List<ConfigSkinRecord> lsConfigSkin;
    private List<ItemSkinSelect> lsItem = new List<ItemSkinSelect>();

    public override void OnSetup(DialogParam param)
    {
        if(this.lsConfigSkin == null)
        {
            lsConfigSkin = ConfigManager.instance.configSkin.GetAll();
            foreach(ConfigSkinRecord e in lsConfigSkin)
            {
                ItemSkinSelect item = Instantiate(itemSkinSelectPrefab);
                item.transform.SetParent(parentLayout);
                lsItem.Add(item);
            }
        }

        for(int i=0; i<lsItem.Count; i++)
        {
            lsItem[i].Setup(lsConfigSkin[i]);
        }
    }

    public void OnClose()
    {
        DialogManager.instance.HideDialog(this.index);
    }
}
