using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScreen : MonoBehaviour
{
    [SerializeField] private TutorialScreen nextTutor;
    [SerializeField] private Button btn;
    [SerializeField] private GameObject[] linkeds;

    private void Awake()
    {
        btn.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);

            if (nextTutor != null )
            {
                nextTutor.gameObject.SetActive(true);
            }
            else
            {
                TutorialManager.Instance.EndTutorial();
            }
        });
    }

    private void OnEnable()
    {
        foreach (GameObject objs in linkeds)
        {
            objs.SetActive(true);
        }
    }

    private void OnDisable()
    {
        foreach (GameObject objs in linkeds)
        {
            objs.SetActive(false);
        }
    }
}
