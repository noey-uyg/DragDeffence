using System.Collections.Generic;
using System.Numerics;
using System.Text;

public static class CurrencyFomatter
{
    private static Dictionary<int, string> _unitCache = new Dictionary<int, string>();

    public static string FormatBigInt(BigInteger gold)
    {
        if (gold < 1000) return gold.ToString();

        int unitIndex = 0;
        BigInteger remainder = 0;
        BigInteger tempGold = gold;

        while (tempGold >= 1000)
        {
            remainder = tempGold % 1000;
            tempGold /= 1000;
            unitIndex++;
        }

        string unitString = GetUnitString(unitIndex);

        float decimalPart = (float)remainder / 1000f;
        float finalValue = (float)tempGold + decimalPart;

        return $"{finalValue:F2}{unitString}";
    }

    private static string GetUnitString(int index)
    {
        if (index == 0) return "";
        if (_unitCache.TryGetValue(index, out var unit)) return unit;

        StringBuilder sb = new StringBuilder();
        int tempIndex = index;

        while (tempIndex > 0)
        {
            int modulo = (tempIndex - 1) % 26;
            sb.Insert(0, (char)('A' + modulo));
            tempIndex = (tempIndex - modulo) / 26;
        }

        string result = sb.ToString();
        _unitCache[index] = result;

        return result;
    }
}
