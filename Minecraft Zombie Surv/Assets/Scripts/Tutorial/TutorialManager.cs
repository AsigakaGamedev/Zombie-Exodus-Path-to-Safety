using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private TutorialScreen[] allScreens;
    [SerializeField] private GameObject[] linkeds;

    public static TutorialManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        foreach (var screen in allScreens)
        {
            screen.gameObject.SetActive(false);
        }

        allScreens[0].gameObject.SetActive(true);
        StartTutorial();
    }

    public void StartTutorial()
    {
        foreach (GameObject objs in linkeds)
        {
            objs.SetActive(false);
        }

        //Time.timeScale = 0;
        gameObject.SetActive(true);
    }

    public void EndTutorial()
    {
        foreach (GameObject objs in linkeds)
        {
            objs.SetActive(true);
        }

        //Time.timeScale = 1;
    }
}
