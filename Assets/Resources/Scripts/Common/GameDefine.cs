public enum GameState 
{
    None,
    Lobby,
    Playing,
    Upgrade,
    Paused,
    GameOver,
}

public enum UpgradeType
{
    GamePlayTime = 101,
    GameGoldGainPercent = 102,
    GameSpawnTime = 103,
    CenterHP = 201,
    CenterDefense = 202,
    CircleAtk = 301,
    CircleAtkDelay = 302,
    CircleRadius = 303,
    CircleCiritical = 304,
}

public enum EffectType
{
    CircleHit,
    CenterDam,
    MonsterDam,
}