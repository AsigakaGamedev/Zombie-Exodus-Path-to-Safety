using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveAnimType { Bool, Velocity}

public class AnimationsController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [AnimatorParam(nameof(animator)), SerializeField] private string attackKey;

    [Space]
    [SerializeField] private MoveAnimType moveAnimType;
    [AnimatorParam(nameof(animator)), SerializeField] private string moveKey;

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

    private void LateUpdate()
    {
        switch (moveAnimType)
        {
            case MoveAnimType.Bool:
                animator.SetBool(moveKey, lastPos != transform.position);
                break;
        }

        lastPos = transform.position;
    }

    public void MoveTo(Vector3 dir)
    {
        switch (moveAnimType)
        {
            case MoveAnimType.Velocity:
                float velocityZ = Vector3.Dot(dir, transform.forward);
                animator.SetFloat(moveKey, velocityZ, 0.1f, Time.deltaTime);

                break;
        }
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
