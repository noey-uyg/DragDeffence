using System.Collections.Generic;
using UnityEngine;

public static class PlayerStat
{
    // [초기 수치]
    // [Game]
    private const float BasePlayTime = 10f;
    private const float BaseGoldGainPercent = 1f;
    // [Center]
    private const float BaseHP = 20f;
    private const float BaseDamageReduction = 0f;
    // [Circle]
    private const float BaseAtk = 1f;
    private const float BaseAtkDelay = 1.5f;
    private const float BaseRadius = 0.25f;

    // [현재 수치]
    // [Game]
    public static float CurPlayTime;
    public static float CurGoldGainPercent;
    public static int CurGold;
    // [Center]
    public static float CurMaxHP;
    public static float CurDamageReduction;
    // [Circle]
    public static float CurAtk;
    public static float CurAtkDelay;
    public static float CurRadius;

    // [누적 수치]
    // [Game]
    private static Dictionary<int, float> UpgradedPlayTime = new Dictionary<int, float>();
    private static Dictionary<int, float> UpgradedGoldGainPercent = new Dictionary<int, float>();
    // [Center]
    private static Dictionary<int, float> UpgradedHP = new Dictionary<int, float>();
    private static Dictionary<int, float> UpgradedDefense = new Dictionary<int, float>();
    // [Circle]
    private static Dictionary<int, float> UpgradedAtk = new Dictionary<int, float>();
    private static Dictionary<int, float> UpgradedAtkDelay = new Dictionary<int, float>();
    private static Dictionary<int, float> UpgradedRadius = new Dictionary<int, float>();

    public static void UpdateContiribution(int id, float value)
    {
        int SID = id / 10;

        switch (SID)
        {
            case 301:
                {
                    UpgradedAtk[id] = value;
                    Debug.Log("업그레이드!");
                    break;
                }
            default:
                {
                    Debug.Log("!!!!!");
                    break;
                }
        }
    }
}
