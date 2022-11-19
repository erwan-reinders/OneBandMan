using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    public static void GetFloorFrac(float x, out float floor, out float frac)
    {
        floor = Mathf.Floor(x);
        frac = x - floor;
    }

    public static float EaseInCubic(float x)
    {
        return x * x * x;
    }

    public static float EaseOutBounce(float x)
    {
        float n1 = 7.5625f;
        float d1 = 2.75f;

        if (x < 1 / d1)
        {
            return n1 * x * x;
        }
        else if (x < 2 / d1)
        {
            return n1 * (x -= 1.5f / d1) * x + 0.75f;
        }
        else if (x < 2.5 / d1)
        {
            return n1 * (x -= 2.25f / d1) * x + 0.9375f;
        }
        else
        {
            return n1 * (x -= 2.625f / d1) * x + 0.984375f;
        }
    }
}
