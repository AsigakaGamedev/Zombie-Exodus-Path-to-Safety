using UnityEngine;
using UnityEngine.UI;

public class UIEM_Demos : MonoBehaviour
{
    //Methods here reset the UI elements of the demo scene to have proper loops

    public Transform ScorePanel;
    public GameObject[] StampObjects;
    private Transform StampParent;
    public Text LevelText;

    private void Start()
    {
        StampParent = StampObjects[0].transform.parent;
    }

    public void LevelUp ()
    {
        LevelText.text = (int.Parse(LevelText.text) + 1).ToString();
    }

    public void SetStampParent (bool state)
    {
        if (state)
            StampObjects[0].transform.SetParent(ScorePanel);
        else
        {
            Color[] resetColors = { StampObjects[0].GetComponent<Image>().color, StampObjects[1].GetComponent<Text>().color };
            resetColors[0].a = resetColors[1].a = 0.0f;
            StampObjects[0].GetComponent<Image>().color = resetColors[0];
            StampObjects[1].GetComponent<Text>().color = resetColors[1];
            StampObjects[0].transform.SetParent(StampParent);
            StampObjects[0].transform.localPosition = new Vector3(StampObjects[0].transform.localPosition.x, 135f, StampObjects[0].transform.localPosition.z);
        }
    }
}
