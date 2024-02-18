using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class UIGraphicsSettings : MonoBehaviour
{
    [SerializeField] private UIGraphicsPart[] graphicsParts;

    private void Start()
    {
        foreach (var part in graphicsParts)
        {
            part.Btn.interactable = QualitySettings.renderPipeline != part.PipelineAsset;

            part.Btn.onClick.AddListener(() =>
            {
                ChangePipline(part.PipelineAsset);
            });
        }
    }

    private void ChangePipline(UniversalRenderPipelineAsset pipelineAsset)
    {
        QualitySettings.renderPipeline = pipelineAsset;

        foreach (var part in graphicsParts)
        {
            part.Btn.interactable = pipelineAsset != part.PipelineAsset;
        }
    }
}

[System.Serializable]
public class UIGraphicsPart
{
    [SerializeField] private Button btn;
    [SerializeField] private UniversalRenderPipelineAsset pipelineAsset;

    public Button Btn { get => btn; }
    public UniversalRenderPipelineAsset PipelineAsset { get => pipelineAsset; }
}
