public interface IScreenListener
{
    void OnScreenInit();
}

public interface IScreenShowListener : IScreenListener
{
    void OnScreenShow();
}

public interface IScreenHideListener : IScreenListener
{
    void OnScreenHide();
}

public interface IScreenUpdateListener : IScreenListener
{
    void OnScreenUpdate();
}