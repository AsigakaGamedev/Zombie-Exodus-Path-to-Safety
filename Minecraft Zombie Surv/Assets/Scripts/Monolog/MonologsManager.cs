using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonologsManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI monologText;
    [SerializeField] private float monologCharDelay = 0.1f;

    [Space]
    [SerializeField] private List<MonologData> curMonologList;

    public void SetMonolog(List<MonologData> monologList)
    {
        curMonologList = monologList;
    }

    private void Awake()
    {
        monologText.text = "";
        StartCoroutine(EShowMonolog());
    }

    private IEnumerator EShowMonolog()
    {
        while (true) 
        {
            if (curMonologList.Count == 0)
            {
                yield return new WaitForEndOfFrame();
                continue;
            }

            foreach (MonologData monologData in curMonologList)
            {
                string monologResultText = "";

                foreach (char monologChar in monologData.Text)
                {
                    monologResultText += monologChar;
                    monologText.text = monologResultText;
                    yield return new WaitForSeconds(monologCharDelay);
                }

                yield return new WaitForSeconds(monologData.Time);
                monologText.text = "";
            }

            curMonologList.Clear();
        }
    }

}
