using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICraftTypeChanger : MonoBehaviour
{
    [SerializeField] private Button btn;

    [Space]
    [SerializeField] private CraftType craftType;

    public Action<CraftType> onClickInfo;

    private void Awake()
    {
        btn.onClick.AddListener(() =>
        {
            onClickInfo?.Invoke(craftType);
        });
    }
}
