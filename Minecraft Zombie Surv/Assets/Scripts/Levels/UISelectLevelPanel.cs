using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UISelectLevelPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelLabelTxt;
    [SerializeField] private TextMeshProUGUI levelDescriptionTxt;

    [Space]
    [SerializeField] private Image levelPreviewImg;

    [Space]
    [SerializeField] private Button startLevelBtn;
    [SerializeField] private Button nextLevelBtn;
    [SerializeField] private Button prevLevelBtn;

    private LevelsManager levelsManager;

    private int selectedLevelIndex;

    [Inject]
    private void Construct(LevelsManager levelsManager)
    {
        this.levelsManager = levelsManager;
    }

    private void Start()
    {
        SelectLevel(0);

        startLevelBtn.onClick.AddListener(() =>
        {
            levelsManager.LoadSelectedLevel();
        });

        prevLevelBtn.onClick.AddListener(() =>
        {
            SelectLevel(selectedLevelIndex - 1);
        });

        nextLevelBtn.onClick.AddListener(() =>
        {
            SelectLevel(selectedLevelIndex + 1);
        });
    }

    private void SelectLevel(int index)
    {
        if (index > levelsManager.AllLevels.Length - 1) index = 0;
        else if (index < 0) index = levelsManager.AllLevels.Length - 1;

        selectedLevelIndex = index;
        LevelInfo level = levelsManager.AllLevels[selectedLevelIndex];

        levelsManager.SelectLevel(level);

        levelLabelTxt.text = level.LevelName;
        levelDescriptionTxt.text = level.LevelDescription;

        levelPreviewImg.sprite = level.LevelPreview;
    }
}
