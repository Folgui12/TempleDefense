using System;

public class ArcherEventManager
{
    public static event Action ShootEvent;

    public static void Execute()
    {
        ShootEvent?.Invoke();
    }
}
