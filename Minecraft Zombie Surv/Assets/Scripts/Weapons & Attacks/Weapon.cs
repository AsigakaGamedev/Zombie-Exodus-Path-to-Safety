using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Weapon : MonoBehaviour
{
    [SerializeField] private AttacksHandler attacksHandler;

    [Space]
    [Tooltip("1 - non weapon\n2 - rifle\n3 - pistol")]
    [SerializeField] private int animTypeIndex;

    public int AnimTypeIndex { get => animTypeIndex; }

    public void Init()
    {
        attacksHandler.Init();
    }

    public bool TryAttack()
    {
        return attacksHandler.TryAttack();
    }

    public void OnEquip()
    {
        gameObject.SetActive(true);
    }

    public void OnDequip()
    {
        gameObject.SetActive(false);
    }
}
