using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct UIEffect
{
    #region Editor Variables
    public bool showSettings;
    public string Name;
    public Vector2 nodePos;
    public List<string> Outputs;
    public float spacing;
    #endregion
    public enum targetTypes { This, Other };
    public targetTypes TargetType;
    public GameObject targetObj;
    public bool RunAtStart;
    public bool RunOnEnable;
    public bool Loop;
    public enum effectTypes { Move, Rotate, Scale, Fade, Color, Shine, Shake, Jelly};
    public effectTypes EffectType;
    public float Delay;
    public float Speed;
    public enum initialStates { Current, Custom };
    public initialStates initialState;
    public Vector3 startVector; //Start Position/Rotation/Scale
    public Vector3 targetVector; //Target Position/Rotation/Scale
    public float Duration;
    public Color color;
    #region Rotate Variables
    public enum rotationTypes { Direct, Constant };
    public rotationTypes RotationType;
    public enum rotDirections { X, Y, Z, __X, __Y, __Z };
    public rotDirections RotationDirection;
    #endregion
    #region Fade/Color Variables
    public enum fadeTypes { In, Out };
    public fadeTypes FadeType;
    public bool ApplyToChildren;
    public float startAlpha;
    public Color startColor;
    #endregion
    public float BrightnessDuration;
    #region Shake/Jelly Variables
    public enum shakeDirections { Mixed, Vertical, Horizontal };
    public shakeDirections ShakeOrJellyDirection;
    public float Amplitude;
    #endregion
    public bool running;
    public bool killed;
    public UnityEvent OnStart;
    public UnityEvent OnFinished;
	
    public UIEffect (string name, Vector2 pos)
    {
        showSettings = false;
        Name = name;
        nodePos = pos;
        Outputs = new List<string>();
        spacing = 0.0f;
        TargetType = targetTypes.This;
        targetObj = null;
        RunAtStart = false;
        RunOnEnable = false;
        Loop = false;
        EffectType = effectTypes.Move;
        Delay = 0.0f;
        Speed = 5.0f;
        initialState = initialStates.Current;
        startVector = Vector3.zero;
        targetVector = Vector3.zero;
        Duration = 1.0f;
        color = Color.white;
        RotationType = rotationTypes.Direct;
        RotationDirection = rotDirections.Z;
        FadeType = fadeTypes.In;
        ApplyToChildren = false;
        startAlpha = 0.0f;
        startColor = Color.white;
        BrightnessDuration = 0.0f;
        ShakeOrJellyDirection = shakeDirections.Mixed;
        Amplitude = 1.0f;
        running = false;
        killed = false;
        OnStart = null;
        OnFinished = null;
    }
}
