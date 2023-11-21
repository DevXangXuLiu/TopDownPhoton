using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BaseView : MonoBehaviour
{
    public ViewIndex index;
    public string nameView;

    private BaseViewAnimation baseViewAnimation;

    private void Awake()
    {
        baseViewAnimation = gameObject.GetComponentInChildren<BaseViewAnimation>();
    }

    public virtual void OnSetup(ViewParam param)
    {

    }

    public void OnShow(Action callback)
    {
        baseViewAnimation.OnShowAnim(() =>
        {
            OnShowView();
            callback?.Invoke();
        });
    }

    public void OnHide(Action callback)
    {
        baseViewAnimation.OnHideAnim(() =>
        {
            OnHideView();
            callback?.Invoke();
        });
    }

    public virtual void OnShowView()
    {

    }

    public virtual void OnHideView()
    {

    }
}
