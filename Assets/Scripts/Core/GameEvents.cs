using System;

public static class GameEvents
{
    public static event Action OnInteractionStarted;
    public static event Action OnInteractionEnded;
    public static event Action<string> OnAnnouncmentTriggered;

    public static void TriggerInteractionStarted()
    {
        OnInteractionStarted?.Invoke();
    }

    public static void TriggerInteractionEnded()
    {
        OnInteractionEnded?.Invoke();
    }

    public static void TriggerAnnouncmentTriggered(string _content)
    {
        OnAnnouncmentTriggered?.Invoke(_content);
    }
}
