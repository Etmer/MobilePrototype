using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IngredientType
{
    Head,
    Torso,
    Arm,
    Foot
}

[CreateAssetMenu(fileName ="Create New Spritemanager", menuName ="Create New")]
public class SpriteManager : ScriptableObject
{
    /// <summary>
    /// 1 = Head, 2 = Torso, 3 = Arm, 4 = Foot
    /// </summary>
    [SerializeField] private Sprite[] _IngredientSprites;

    public Sprite GetSprite(IngredientType ingredientType)
    {
        return _IngredientSprites[(int)ingredientType];
    }

}
