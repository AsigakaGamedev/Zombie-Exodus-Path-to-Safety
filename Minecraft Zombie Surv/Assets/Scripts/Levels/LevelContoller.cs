using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class LevelContoller : MonoBehaviour
{
    [SerializeField] private PlayableDirector startTimeline;

    [Space]
    [ReadOnly, SerializeField] private PlayerController playerInstance;

    private TimelineController timelineController;

    public PlayerController PlayerInstance { get => playerInstance; }

    private void OnEnable()
    {
        ServiceLocator.AddService(this);
    }

    private void OnDisable()
    {
        ServiceLocator.RemoveService(this);
    }

    private void Start()
    {
        timelineController = ServiceLocator.GetService<TimelineController>();

        if (startTimeline)
        {
            timelineController.SetTimeline(startTimeline);
            timelineController.Play();
        }
    }
}
