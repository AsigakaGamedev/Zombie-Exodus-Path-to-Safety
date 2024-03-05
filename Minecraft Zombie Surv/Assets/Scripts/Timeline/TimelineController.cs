using UnityEngine;
using UnityEngine.Playables;
using NaughtyAttributes;

public class TimelineController : AInitializable
{
    [SerializeField] private PlayableDirector startTimeline;

    [Space]
    [ReadOnly, SerializeField] private PlayableDirector currentTimeline;

    private UIManager uiManager;

    private void OnEnable()
    {
        ServiceLocator.AddService(this);
    }

    private void OnDisable()
    {
        ServiceLocator.RemoveService(this);
    }

    public override void OnInit()
    {
        uiManager = ServiceLocator.GetService<UIManager>();

        if (startTimeline)
        {
            SetTimeline(startTimeline);
            Play();
        }
    }

    public void SetTimeline(PlayableDirector timeline)
    {
        if (currentTimeline)
        {
            currentTimeline.played -= OnTimelinePlay;
            currentTimeline.stopped -= OnTimelineStop;
        }

        currentTimeline = timeline;

        currentTimeline.played += OnTimelinePlay;
        currentTimeline.stopped += OnTimelineStop;
    }

    private void OnTimelinePlay(PlayableDirector timeline)
    {
        uiManager.ChangeScreen("timeline");
    }

    private void OnTimelineStop(PlayableDirector timeline)
    {
        uiManager.ChangeScreen("hud");
    }

    [Button]
    public void Play()
    {
        currentTimeline.Play();
    }

    [Button]
    public void Pause()
    {
        currentTimeline.Pause();
    }

    [Button]
    public void Resume()
    {
        currentTimeline.Resume();
    }
}
