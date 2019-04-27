using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IngredientType
{
    Volunteer,
    Zombie,
    Poison,
    Foot
}

[CreateAssetMenu(fileName ="Create New Spritemanager", menuName ="Create New")]
public class SpriteManager : ScriptableObject
{
    /// <summary>
    /// 1 = Volunteer, 2 = Undead, 3 = Poison, 4 = Foot
    /// </summary>
    [SerializeField] private Sprite[] _IngredientSprites;

    public Sprite GetSprite(IngredientType ingredientType)
    {
        return _IngredientSprites[(int)ingredientType];
    }

}
