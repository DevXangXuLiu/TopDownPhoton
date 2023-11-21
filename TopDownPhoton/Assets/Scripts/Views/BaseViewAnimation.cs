using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BaseViewAnimation : MonoBehaviour
{
    public virtual void OnShowAnim(Action callback)
    {
        callback?.Invoke();
    }

    public virtual void OnHideAnim(Action callback)
    {
        callback?.Invoke();
    }
}
