using System;
using UnityEngine;

public abstract class DialogSystem : MonoBehaviour
{
    public Action OnDialogShow;
    public Action OnDialogHide;

    public virtual void DialogShow()
    {
        this.gameObject.SetActive(true);
        OnDialogShow?.Invoke();
    }
    public virtual void DialogHide()
    {
        this.gameObject.SetActive(false);
        OnDialogHide?.Invoke();
    }
}
