using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BootLoaderControl : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        ConfigManager.instance.InitConfig(() =>
        {
            DataAPIController.instance.OnInit(() =>
            {
                ViewManager.instance.OnSwitchView(ViewIndex.HomeView);
                SceneManager.LoadSceneAsync(1);
            });
        });
    }
}
