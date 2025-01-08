using System;
using UnityEngine;

public static class DirectionEnumExtensions
{
    /// <summary>
    /// Converts the vector into a normalized vector in a cardinal direction.
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    public static Vector2 ToDirectionVector(this Vector2 vector)
    {
        vector.Normalize();

        // Determine the angle.
        float angle = Vector2.Angle(Vector2.right, vector);
        if (vector.y.IsNegative())
        {
            angle += 180;
        }

        // Round to the nearest 90 degrees.
        angle += 45;
        angle -= angle % 90;
        float rads = angle * Mathf.Deg2Rad;

        return new Vector2(Mathf.Cos(rads), Mathf.Sin(rads));
    }

    /// <summary>
    /// Converts the enum into a normalized vector in a cardinal direction.
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static Vector2 ToDirectionVector(this DirectionEnum direction)
    {
        switch (direction)
        {
            case DirectionEnum.None:
                return new Vector2(0, 0);
            case DirectionEnum.Right:
                return new Vector2(1, 0);
            case DirectionEnum.Up:
                return new Vector2(0, 1);
            case DirectionEnum.Left:
                return new Vector2(-1, 0);
            case DirectionEnum.Down:
                return new Vector2(0, -1);
            default:
                throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Converts the vector into the nearest direction enum.
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    public static DirectionEnum ToDirectionEnum(this Vector2 vector)
    {
        vector = vector.ToDirectionVector();
        if (vector.x.IsZero() && vector.y.IsZero())
        {
            return DirectionEnum.None;
        }

        if (vector.x.IsZero())
        {
            return vector.y.IsPositive() ? DirectionEnum.Up : DirectionEnum.Down;
        }
        else
        {
            return vector.x.IsPositive() ? DirectionEnum.Right : DirectionEnum.Left;
        }
    }

    /// <summary>
    /// Returns a signed integer representing the x portion of the direction.
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    public static int GetXSign(this DirectionEnum direction)
        => (int)Mathf.Sign(direction.ToDirectionVector().x);

    /// <summary>
    /// Returns a signed integer representing the y portion of the direction.
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    public static int GetYSign(this DirectionEnum direction)
        => (int)Mathf.Sign(direction.ToDirectionVector().y);
}