using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IngredientType
{
    Volunteer,
    Barbarian,
    Corpse,
    Zombie,
    SuperZombie,
    Poison,
    PotionOfElectricity,
    PotionOfLife,
    Water,
    Dirt,
    WetClay,
    Golem,
    FrankensteinsMonster,
    Rogue
}

[CreateAssetMenu(fileName ="Create New Spritemanager", menuName ="Create New")]
public class SpriteManager : ScriptableObject
{
    /// <summary>
    /// 1 = Volunteer, 2 = Babarian, 3 = Corpse, 4 = Zombie
    /// 5 = SuperZombie, 6 = Poison, 7 = PotionOfElectricity
    /// 8 = PotionOfLife, 9 = Water, 10 = Dirt, 11 = WetClay
    /// 12 = Golem, 13 = FrankensteinsMonster, 14 = Rogue 
    /// </summary>
    [SerializeField] private Sprite[] _IngredientSprites;

    public Sprite GetSprite(IngredientType ingredientType)
    {
        return _IngredientSprites[(int)ingredientType];
    }

}
