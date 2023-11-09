using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ConfigManager : Singleton<ConfigManager>
{
    private ConfigSkin configSkin_;
    public ConfigSkin configSkin
    {
        get
        {
            return configSkin_;
        }
    }

    private ConfigWeapon configWeapon_;
    public ConfigWeapon configWeapon
    {
        get
        {
            return configWeapon_;
        }
    }

    private IEnumerator Init(Action callback)
    {
        configSkin_ = Resources.Load("DataTable/ConfigSkin", typeof(ScriptableObject)) as ConfigSkin;
        yield return new WaitUntil(() => configSkin_ != null);
        configWeapon_ = Resources.Load("DataTable/ConfigWeapon", typeof(ScriptableObject)) as ConfigWeapon;
        yield return new WaitUntil(() => configWeapon_ != null);

        callback?.Invoke();
    }

    public void InitConfig(Action callback)
    {
        StartCoroutine(Init(callback));
    }
}
