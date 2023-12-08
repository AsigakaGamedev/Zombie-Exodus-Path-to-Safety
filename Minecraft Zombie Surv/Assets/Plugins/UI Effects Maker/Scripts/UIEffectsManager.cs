using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI/UI Effects Manager")]
[System.Serializable]
public class UIEffectsManager : MonoBehaviour
{
    //Effects Settings
    public List<UIEffect> Settings = new List<UIEffect>();

    private void OnEnable()
    {
        for (int i = 0; i < Settings.Count; i++)
        {
            if (Settings[i].RunOnEnable)
                StartCoroutine(performEffect(i));
        }
    }

    void Start()
    {
        //Run initial effects
        for (int i = 0; i < Settings.Count; i++)
        {
            if (Settings[i].RunAtStart)
                StartCoroutine(performEffect(i));
        }
    }
	
    //Find the index of the given effect
    int indexOfEffect (string effectName)
    {
        for (int i = 0; i < Settings.Count; i++)
        {
            if (Settings[i].Name == effectName)
                return i;
        }
        return -1;
    }

    public void Run(string effectName)
    {
        StartCoroutine(performEffect(indexOfEffect(effectName)));
    }

    IEnumerator performEffect(int index)
    {
        if (Settings[index].OnStart != null)
            Settings[index].OnStart.Invoke();

        UIEffect effect = Settings[index];

        if (effect.killed && effect.running)
            yield return new WaitForSeconds(0.093f);

        effect.killed = false;
        effect.running = true;
        if (effect.TargetType == UIEffect.targetTypes.This)
            effect.targetObj = gameObject;
        Settings[index] = effect;

        float t = 0.0f;
        //Delay
        while (t < Settings[index].Delay)
        {
            t += Time.deltaTime;
            if (Settings[index].killed)
                yield break;
            yield return null;
        }

        t = 0.0f;
        //Checking the type of the given effect
        if (Settings[index].EffectType != UIEffect.effectTypes.Fade && Settings[index].EffectType != UIEffect.effectTypes.Color)
        {
            switch (Settings[index].EffectType)
            {
                case UIEffect.effectTypes.Move:
                    //Custom Start Position
                    if (Settings[index].initialState == UIEffect.initialStates.Custom)
                        Settings[index].targetObj.transform.localPosition = Settings[index].startVector;
                    //Smooth Movement
                    while (!Approximately(Settings[index].targetObj.transform.localPosition.x, Settings[index].targetVector.x, 0.02f) || !Approximately(Settings[index].targetObj.transform.localPosition.y, Settings[index].targetVector.y, 0.02f))
                    {
                        if (Settings[index].killed)
                            yield break;
                        t += Time.deltaTime * Settings[index].Speed * 0.2f;
                        Settings[index].targetObj.transform.localPosition = Vector3.Lerp(Settings[index].targetObj.transform.localPosition, Settings[index].targetVector, Mathf.SmoothStep(0.0f, 1.0f, t));
                        yield return null;
                    }
                    Settings[index].targetObj.transform.localPosition = Settings[index].targetVector;
                    break;
                case UIEffect.effectTypes.Rotate:
                    Quaternion targetRotation = Quaternion.Euler(Settings[index].targetVector);
                    //Checking the rotation type
                    if (Settings[index].RotationType == UIEffect.rotationTypes.Direct)
                    {
                        //Custom Start Rotation
                        if (Settings[index].initialState == UIEffect.initialStates.Custom)
                            Settings[index].targetObj.transform.localRotation = Quaternion.Euler(Settings[index].startVector);
                        //Smooth Rotation
                        while (!Approximately(Settings[index].targetObj.transform.localEulerAngles.x, targetRotation.eulerAngles.x, 0.02f) || !Approximately(Settings[index].targetObj.transform.localEulerAngles.y, targetRotation.eulerAngles.y, 0.02f) || !Approximately(transform.localEulerAngles.z, targetRotation.eulerAngles.z, 0.02f))
                        {
                            if (Settings[index].killed)
                                yield break;
                            t += Time.deltaTime * Settings[index].Speed * 0.06f;
                            Settings[index].targetObj.transform.localRotation = Quaternion.Lerp(Settings[index].targetObj.transform.localRotation, targetRotation, Mathf.SmoothStep(0.0f, 1.0f, t));
                            yield return null;
                        }
                    }
                    else
                    {
                        //Setting up the direction of the rotation
                        Vector3 rotationVector;
                        if ((int)Settings[index].RotationDirection % 3 == 0)
                            rotationVector = Vector3.right;
                        else if ((int)Settings[index].RotationDirection % 3 == 1)
                            rotationVector = Vector3.up;
                        else
                            rotationVector = Vector3.forward;
                        if ((int)Settings[index].RotationDirection >= 3)
                        {
                            rotationVector *= -1;
                        }
                        rotationVector *= Settings[index].Speed * 2.0f;
                        //Constant rotation till the duration
                        while (t < Settings[index].Duration)
                        {
                            if (Settings[index].killed)
                                yield break;
                            Settings[index].targetObj.transform.Rotate(rotationVector);
                            t += Time.deltaTime;
                            yield return null;
                        }
                    }

                    Settings[index].targetObj.transform.localRotation = targetRotation;
                    break;
                case UIEffect.effectTypes.Scale:
                    //Custom Start Scale
                    if (Settings[index].initialState == UIEffect.initialStates.Custom)
                        Settings[index].targetObj.transform.localScale = new Vector3(Settings[index].startVector.x, Settings[index].startVector.y, Settings[index].targetObj.transform.localScale.z);
                    //Smooth Scale
                    while (!Approximately(Settings[index].targetObj.transform.localScale.x, Settings[index].targetVector.x, 0.01f) || !Approximately(Settings[index].targetObj.transform.localScale.y, Settings[index].targetVector.y, 0.01f))
                    {
                        if (Settings[index].killed)
                            yield break;
                        t += Time.deltaTime * Settings[index].Speed * 0.2f;
                        Settings[index].targetObj.transform.localScale = Vector3.Lerp(Settings[index].targetObj.transform.localScale, new Vector3(Settings[index].targetVector.x, Settings[index].targetVector.y, Settings[index].targetObj.transform.localScale.z), Mathf.SmoothStep(0.0f, 1.0f, t));
                        yield return null;
                    }
                    Settings[index].targetObj.transform.localScale = new Vector3(Settings[index].targetVector.x, Settings[index].targetVector.y, Settings[index].targetObj.transform.localScale.z);
                    break;
                case UIEffect.effectTypes.Shine:
                    yield return new WaitForEndOfFrame();
                    if (Settings[index].killed)
                        yield break;
                    //Setting up the brightness game object
                    RectTransform brightness = new GameObject("brightness").AddComponent<RectTransform>();
                    RectTransform thisRT = Settings[index].targetObj.GetComponent<RectTransform>();
                    brightness.anchorMin = thisRT.anchorMin;
                    brightness.anchorMax = thisRT.anchorMax;
                    brightness.anchoredPosition = thisRT.anchoredPosition;
                    brightness.sizeDelta = thisRT.sizeDelta;
                    brightness.pivot = thisRT.pivot;
                    brightness.SetParent(thisRT);
                    bool hasMask = Settings[index].targetObj.GetComponent<Mask>() != null;
                    if (!hasMask)
                        Settings[index].targetObj.AddComponent<Mask>();
                    brightness.localPosition = Vector3.zero;
                    brightness.localRotation = Quaternion.Euler(Vector3.zero);
                    brightness.localScale = Vector3.one;
                    Image targetObj = brightness.gameObject.AddComponent<Image>();
                    Color initColor = Settings[index].color;
                    targetObj.color = initColor;
                    //Wait till the brightness duration
                    while (t < Settings[index].BrightnessDuration)
                    {
                        t += Time.deltaTime;
                        if (Settings[index].killed)
                        {
                            if (!hasMask)
                                Destroy(thisRT.GetComponent<Mask>());
                            Destroy(brightness.gameObject);
                            yield break;
                        }
                        yield return null;
                    }

                    initColor.a = 0.0f;
                    t = 0.0f;
                    //Fade the brightness
                    while (targetObj.color != initColor)
                    {
                        if (Settings[index].killed)
                        {
                            if (!hasMask)
                                Destroy(thisRT.GetComponent<Mask>());
                            Destroy(brightness.gameObject);
                            yield break;
                        }
                        t += Time.deltaTime * Settings[index].Speed * 0.25f;
                        targetObj.color = Color.Lerp(targetObj.color, initColor, Mathf.SmoothStep(0.0f, 1.0f, t));
                        yield return null;
                    }

                    if (!hasMask)
                        Destroy(thisRT.GetComponent<Mask>());
                    Destroy(brightness.gameObject);
                    break;
                case UIEffect.effectTypes.Shake:
                    Vector3 startPos = Settings[index].targetObj.transform.position;
                    //Shake till the duration of the effect
                    while (t < Settings[index].Duration)
                    {
                        Vector3 distance;
                        //Setting the target distance
                        if (Settings[index].ShakeOrJellyDirection == UIEffect.shakeDirections.Mixed)
                        {
                            System.Random rnd = new System.Random();
                            distance.x = rnd.Next(2, 10) / 10f * Mathf.Pow(-1, rnd.Next(10));
                            distance.y = rnd.Next(2, 10) / 10f * Mathf.Pow(-1, rnd.Next(10));
                            distance.z = 0f;
                        }
                        else if (Settings[index].ShakeOrJellyDirection == UIEffect.shakeDirections.Vertical)
                            distance = Vector3.right;
                        else
                            distance = Vector3.up;

                        distance *= Settings[index].Amplitude * 5;
                        //Processing the frames
                        for (int i = 0; ; i++)
                        {
                            if (i == 1)
                            {
                                if (Settings[index].ShakeOrJellyDirection == UIEffect.shakeDirections.Mixed)
                                {
                                    i++;
                                    continue;
                                }
                                else
                                {
                                    //Invert the distance
                                    distance *= -1;
                                }
                            }
                            //Delay between frames
                            yield return new WaitForSeconds(-0.005466f * Settings[index].Speed + 0.055466f);
                            t += Time.deltaTime + -0.005466f * Settings[index].Speed + 0.055466f;
                            if (Settings[index].killed)
                            {
                                Settings[index].targetObj.transform.position = startPos;
                                yield break;
                            }
                            if (i == 3)
                            {
                                //Last frame
                                Settings[index].targetObj.transform.position = startPos;
                                break;
                            }
                            //Move to the target distance
                            Settings[index].targetObj.transform.position += distance;
                        }
                    }
                    break;
                case UIEffect.effectTypes.Jelly:
                    Vector3 startScale = Settings[index].targetObj.transform.localScale;
                    //Each delta size for the corresponding frame
                    Vector3[] deltaSizes = {
                        new Vector3(0.8f, 0.8f, 0.0f) * Settings[index].Amplitude * 0.07f,
                        new Vector3(-1.1f, -1.1f, 0.0f) * Settings[index].Amplitude * 0.07f,
                        new Vector3(0.8f, 0.8f, 0.0f) * Settings[index].Amplitude * 0.07f,
                        new Vector3(-0.7f, -0.7f, 0.0f) * Settings[index].Amplitude * 0.07f,
                        new Vector3(0.4f, 0.4f, 0.0f) * Settings[index].Amplitude * 0.07f
                    };

                    int counter = 0;
                    while (counter < deltaSizes.Length)
                    {
                        deltaSizes[counter].x *= startScale.x;
                        deltaSizes[counter].y *= startScale.y;
                        if (Settings[index].ShakeOrJellyDirection == UIEffect.shakeDirections.Horizontal)
                        {
                            //No size change throughout X-axis
                            deltaSizes[counter].x = 0.0f;
                        }
                        else if (Settings[index].ShakeOrJellyDirection == UIEffect.shakeDirections.Vertical)
                        {
                            //No size change throughout Y-axis
                            deltaSizes[counter].y = 0.0f;
                        }
                        t = Settings[index].Speed * 3 / (counter + 2);
                        //Delay between frames
                        yield return new WaitForSeconds(0.00203042f * t * t - 0.0287651f * t + 0.0996658f);
                        if (Settings[index].killed)
                        {
                            Settings[index].targetObj.transform.localScale = startScale;
                            yield break;
                        }
                        Settings[index].targetObj.transform.localScale += deltaSizes[counter];
                        counter++;
                        yield return null;
                    }
                    //Delay for the last frame
                    yield return new WaitForSeconds(0.00203042f * t * t - 0.0287651f * t + 0.0996658f);
                    Settings[index].targetObj.transform.localScale = startScale;
                    if (Settings[index].killed)
                        yield break;
                    break;
            }
            RunNext(index);
        }
        else
        {
            // Fade/Color
            if (!Settings[index].ApplyToChildren)
                StartCoroutine(ColorLerp(Settings[index].targetObj.GetComponent<Graphic>(), index));
            else
            {
                Transform[] children = Settings[index].targetObj.GetComponentsInChildren<Transform>();
                foreach (Transform child in children)
                {
                    if (child.GetComponent<Graphic>() != null)
                        StartCoroutine(ColorLerp(child.GetComponent<Graphic>(), index));
                }
            }
        }
    }
	
	bool Approximately(float valueA, float valueB, float epsilon)
    {
        return Mathf.Abs(valueA - valueB) < epsilon;
    }

    IEnumerator ColorLerp(Graphic targetObj, int index)
    {
        float t = 0.0f;
        Color targetColor = Settings[index].color;
        if (Settings[index].EffectType == UIEffect.effectTypes.Fade)
        {
            targetColor = targetObj.color;
            //Custom Start Alpha
            if (Settings[index].initialState == UIEffect.initialStates.Custom)
            {
                targetColor.a = Settings[index].startAlpha;
                targetObj.color = targetColor;
            }
            //Setting up target color according to Fade
            if (Settings[index].FadeType == UIEffect.fadeTypes.In)
                targetColor.a = 1.0f;
            else
                targetColor.a = 0.0f;
        }
        else if (Settings[index].initialState == UIEffect.initialStates.Custom)
        {
            //Custom Start Color
            targetObj.color = Settings[index].startColor;
        }
        //Lerp color till the target
        while (targetObj.color != targetColor)
        {
			if (Settings[index].killed)
                yield break;
            t += Time.deltaTime * Settings[index].Speed * 0.25f;
            targetObj.color = Color.Lerp(targetObj.color, targetColor, Mathf.SmoothStep(0.0f, 1.0f, t));
            yield return null;
        }

        if (targetObj.gameObject == Settings[index].targetObj)
            RunNext(index);
    }

    void RunNext(int index)
    {
        UIEffect effect = Settings[index];
        effect.running = false;
        Settings[index] = effect;

        if (Settings[index].OnFinished != null)
            Settings[index].OnFinished.Invoke();

        if (Settings[index].Loop)
            StartCoroutine(performEffect(index));

        //Run all the next effects
        foreach (string next in Settings[index].Outputs)
        {
            Run(next);
        }
    }

    public List<string> runningEffects()
    {
        List<string> runningEffectsList = new List<string>();
        foreach (UIEffect effect in Settings)
        {
            if (effect.running)
                runningEffectsList.Add(effect.Name);
        }
        return runningEffectsList;
    }

    public void Kill(string effectName)
    {
        int index = indexOfEffect(effectName);
        UIEffect effect = Settings[index];
        effect.killed = true;
        Settings[index] = effect;
        if (effect.running)
            StartCoroutine(setRunning(index));
    }

    IEnumerator setRunning (int effectIndex)
    {
        yield return new WaitForSeconds(0.093f);
        UIEffect effect = Settings[effectIndex];
        effect.running = false;
        Settings[effectIndex] = effect;
    }

    public void SetProperty(string effectName, string propertyName, object value)
    {
        int index = indexOfEffect(effectName);
        System.Reflection.FieldInfo property = typeof(UIEffect).GetField(propertyName);
        if (property == null)
            throw new System.NullReferenceException("There is no property called " + propertyName + " on UIEffect structure! Find the correct name through the source code.");
        object boxed = Settings[index];
        property.SetValue(boxed, value);
        Settings[index] = (UIEffect)boxed;
    }
}
