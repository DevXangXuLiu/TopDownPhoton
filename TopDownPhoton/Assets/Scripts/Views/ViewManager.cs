using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ViewManager : Singleton<ViewManager>
{
    public event Action<BaseView> OnSwitchNewView;

    public ViewIndex currentViewIndex;
    public BaseView previousView;
    public BaseView currentView;
    public RectTransform anchorParent;

    private Dictionary<ViewIndex, BaseView> dicView = new Dictionary<ViewIndex, BaseView>();

    private void Start()
    {
        foreach(ViewIndex e in ViewConfig.viewIndices)
        {
            string nameView = e.ToString();
            GameObject goView = Instantiate(Resources.Load("View/" + nameView, typeof(GameObject))) as GameObject;
            goView.transform.SetParent(anchorParent, false);
            BaseView view = goView.GetComponent<BaseView>();
            dicView[e] = view;
            view.gameObject.SetActive(false);
        }

        OnSwitchView(currentViewIndex);
    }

    public void OnSwitchView(ViewIndex index, ViewParam param = null, Action callback = null)
    {
        if(currentView != null)
        {
            if (currentView.index == index)
                return;

            previousView = currentView;
            previousView.OnHide(() =>
            {
                previousView.gameObject.SetActive(false);
                currentView = dicView[index];
                OnSwitchNewView?.Invoke(currentView);
                currentView.gameObject.SetActive(true);
                currentView.OnSetup(param);
                currentView.OnShow(callback);
            });
        }
        else
        {
            currentView = dicView[index];
            OnSwitchNewView?.Invoke(currentView);
            currentView.gameObject.SetActive(true);
            currentView.OnSetup(param);
            currentView.OnShow(callback);
        }
    }

    public void OnPreviousView(ViewParam param = null, Action callback = null)
    {
        BaseView temp = previousView;
        previousView = currentView;
        previousView.OnHide(() =>
        {
            previousView.gameObject.SetActive(false);
            currentView = temp;
            OnSwitchNewView?.Invoke(currentView);
            currentView.gameObject.SetActive(true);
            currentView.OnSetup(param);
            currentView.OnShow(callback);
        });
    }
}
