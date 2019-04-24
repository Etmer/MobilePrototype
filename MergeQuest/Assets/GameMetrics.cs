using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMetrics : MonoBehaviour
{
    private static int _currentFieldWidth;
    private static float width;
    private static float height;

    public static void Init (Sprite defaultSprite, int inititalWidth = 3)
    {
        width = defaultSprite.bounds.size.x;
        height = defaultSprite.bounds.size.y;
        ChangeMapWidth(inititalWidth);
    }

    public static float SpriteWidth { get { return width; } }
    public static float SpriteHeight { get { return height; } }

    public static int MapWidth { get { return _currentFieldWidth; } }
    public static void ChangeMapWidth(int newWidth)
    {
        _currentFieldWidth = newWidth;
    }
    public static int VectorToIndex(Vector3 input)
    {
        int x = Mathf.RoundToInt(input.x / SpriteWidth);
        int y = Mathf.RoundToInt(input.y / SpriteWidth);
        return x + (y * _currentFieldWidth);
    }

}
