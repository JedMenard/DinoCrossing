using UnityEngine;

public static class FloatExtensions
{
    public static bool IsZero(this float val, float eps = 1e-5f) => Mathf.Abs(val) <= eps;

    public static bool IsNegative(this float val, float eps = 1e-5f) => val < -eps;

    public static bool IsPositive(this float val, float eps = 1e-5f) => val > eps;
}