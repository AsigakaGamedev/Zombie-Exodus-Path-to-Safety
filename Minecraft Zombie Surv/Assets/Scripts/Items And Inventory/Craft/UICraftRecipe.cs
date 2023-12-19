using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICraftRecipe : PoolableObject
{
    [SerializeField] private Button btn;

    private CraftInfo craftInfo;

    public Action<CraftInfo> onClickInfo;

    private void Awake()
    {
        btn.onClick.AddListener(() =>
        {
            onClickInfo?.Invoke(craftInfo);
        });
    }

    public void Init(CraftInfo craftInfo)
    {
        this.craftInfo = craftInfo;
    }
}
