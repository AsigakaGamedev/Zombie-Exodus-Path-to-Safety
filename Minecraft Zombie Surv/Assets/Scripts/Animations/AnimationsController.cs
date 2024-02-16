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
    [AnimatorParam(nameof(animator)), SerializeField] private string moveZKey;
    [AnimatorParam(nameof(animator)), SerializeField] private string moveXKey;

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
        //switch (moveAnimType)
        //{
        //    case MoveAnimType.Bool:
        //        animator.SetBool(moveZKey, lastPos != transform.position);
        //        break;
        //}

        //lastPos = transform.position;
    }

    public void MoveToWithDots(Vector3 dir)
    {
        switch (moveAnimType)
        {
            case MoveAnimType.Velocity:
                float velocityZ = Vector3.Dot(dir, transform.forward);
                float velocityX = Vector3.Dot(dir, transform.right);
                animator.SetFloat(moveZKey, velocityZ, 0.1f, Time.deltaTime);
                animator.SetFloat(moveXKey, velocityX, 0.1f, Time.deltaTime);

                break;
        }
    }

    public void MoveTo(Vector3 dir)
    {
        animator.SetFloat(moveZKey, dir.z, 0.1f, Time.deltaTime);
        animator.SetFloat(moveXKey, dir.x, 0.1f, Time.deltaTime);
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
