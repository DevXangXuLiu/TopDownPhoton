using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BaseDialog : MonoBehaviour
{
    public DialogIndex index;

    private BaseDialogAnimation baseDialogAnimation;

    private void Awake()
    {
        baseDialogAnimation = gameObject.GetComponentInChildren<BaseDialogAnimation>();
    }

    public virtual void OnSetup(DialogParam param)
    {

    }

    public void OnShow(Action callback)
    {
        baseDialogAnimation.OnShowAnim(() =>
        {
            OnShowDialog();
            callback?.Invoke();
        });
    }

    public void OnHide(Action callback)
    {
        baseDialogAnimation.OnHideAnim(() =>
        {
            OnHideDialog();
            callback?.Invoke();
        });
    }

    public virtual void OnShowDialog()
    {

    }

    public virtual void OnHideDialog()
    {

    }
}
