using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [AnimatorParam(nameof(animator)), SerializeField] private string attackKey;

    [Space]
    [AnimatorParam(nameof(animator)), SerializeField] private string moveBoolKey;

    [Space]
    [SerializeField] private bool changeAnimSpeed = true;
    [ShowIf(nameof(changeAnimSpeed)), AnimatorParam(nameof(animator)), SerializeField] private string animSpeedKey;
    [ShowIf(nameof(changeAnimSpeed)), SerializeField] private Vector2 animSpeed;

    [Space]
    [AnimatorParam(nameof(animator)), SerializeField] private string animTypeKey;
    [SerializeField] private bool changeAnimTypeAtStart = true;
    [ShowIf(nameof(changeAnimTypeAtStart)), SerializeField] private Vector2Int animTypes;

    private Vector3 lastPos;

    private void Awake()
    {
        if (changeAnimSpeed) animator.SetFloat(animSpeedKey, Random.Range(animSpeed.x, animSpeed.y));

        if (changeAnimTypeAtStart) SetAnimType(Random.Range(animTypes.x, animTypes.y + 1));

        lastPos = transform.position;
    }

    public void SetBool(string key, bool value)
    {
        animator.SetBool(key, value);
    }

    public void SetMove(bool value)
    {
        animator.SetBool(moveBoolKey, value);
    }

    public void SetAnimType(int index)
    {
        animator.SetInteger(animTypeKey, index);
    }

    public void SetTrigger(string triggerKey)
    {
        animator.SetTrigger(triggerKey);
    }

    public void SetAttackTrigger()
    {
        SetTrigger(attackKey);
    }
}
