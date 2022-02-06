using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Placement
{
    public static List<Direction> neighbours(Vector3Int position, ICollection <Vector3Int> collection)
    {
        List<Direction> neighbour = new List<Direction>();
        if (collection.Contains(position + Vector3Int.right))
        {
            neighbour.Add(Direction.Left);
        }
        if (collection.Contains(position - Vector3Int.right))
        {
            neighbour.Add(Direction.Right);
        }
        if (collection.Contains(position + new Vector3Int(0,0,1)))
        {
            neighbour.Add(Direction.Up);
        }
        if (collection.Contains(position - new Vector3Int(0, 0, 1)))
        {
            neighbour.Add(Direction.Down);
        }
        return neighbour;
    }

    internal static Vector3Int getOffset(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return new Vector3Int(0, 0, 1);
            case Direction.Down:
                return new Vector3Int(0, 0, -1);
            case Direction.Left:
                return new Vector3Int(1, 0, 0);
            case Direction.Right:
                return new Vector3Int(-1, 0, 0);
            default:
                break;
        }
        throw new Exception("no direction");
    }

    public static Direction GetDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return Direction.Down;
            case Direction.Down:
                return Direction.Up;
            case Direction.Left:
                return Direction.Right;
            case Direction.Right:
                return Direction.Left;
            default:
                break;
        }
        throw new Exception("no direction");
    }
}
