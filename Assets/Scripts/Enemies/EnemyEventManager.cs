using System;

public class EnemyEventManager
{
    public static event Action ShootEvent;

    public static void Execute()
    {
        ShootEvent?.Invoke();
    }
}
