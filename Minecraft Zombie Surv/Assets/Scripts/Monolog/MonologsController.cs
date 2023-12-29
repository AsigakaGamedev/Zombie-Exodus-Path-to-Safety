using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonologsController : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI monologText;
    [SerializeField] private List<MonologData> curMonologList;
    [SerializeField] private MonologData curMonolog;

    private void OnEnable()
    {
        ServiceLocator.AddService(this);
    }

    private void OnDisable()
    {
        ServiceLocator.RemoveService(this);
    }

    public void SetMonolog(List<MonologData> monologList)
    {
        curMonologList = monologList;
    }

    private void Awake()
    {
        StartCoroutine(EShowMonolog());
    }

    private IEnumerator EShowMonolog()
    {
        while (true) 
        {
            if (curMonologList.Count == 0) yield return new WaitForEndOfFrame();
            foreach (MonologData monologData in curMonologList)
            {
                string text = monologData.Text;
                float time = monologData.Time;
                monologText.text = text;
                yield return new WaitForSeconds(time);
                monologText.text = "";
            }
            curMonologList.Clear();
            yield return null;
        }
    }

}
