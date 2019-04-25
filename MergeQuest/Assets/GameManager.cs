using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Sprite _defaultSprite;
    private DataModel _model;
    [SerializeField] private Transform[] _fieldPool;
    private Vector3 _mousePosition;
    private Vector3 _worldPosition;
    private int _currentIndex;
    private Ingredient _currentIngredient;
    [SerializeField] private WorldRepresentation[] _worldSlots;
    [SerializeField] private Transform _prefab;
    [SerializeField] private Rect _playArea;
    [SerializeField] private SpriteManager _spriteManager;
    [SerializeField] private Texture _text;
    
    [SerializeField] private LevelGenerator _levelGenerator;
    [SerializeField] private MapData _mapdata;

    private WorldRepresentation _currentWorldRepresentation;
    void Start()
    {
        GameMetrics.Init(_defaultSprite);
        _mapdata = new MapData();
        _model = new DataModel(_levelGenerator.CreateField(ref _mapdata));
        ReadMapData();
    }
    private void ReadMapData()
    {
        for (int i = 0; i < _mapdata.FieldCount; i++)
        {
            _fieldPool[i].position = _mapdata.GetPosition(i);
        }
        _playArea = _mapdata.PlayeRect;
        _worldSlots = _mapdata.WorldRepresentationsToArray;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMousePosition();
        _worldPosition = Camera.main.ScreenToWorldPoint(_mousePosition);
        _currentIndex = GameMetrics.VectorToIndex(_worldPosition - Vector3.zero);
        if (_playArea.Contains(new Vector2(_worldPosition.x, _worldPosition.y)))
        {
            if (Input.GetMouseButtonDown(0) && CurrentField.HasContent)
            {
                PickUpIngredient(CurrentField.Ingredient);
            }
            if (Input.GetMouseButton(0) && _currentWorldRepresentation != null)
            {
                _currentWorldRepresentation.Move(_worldPosition);
            }
            else if (_currentIngredient != null)
            {
                ReleaseIngredient();
            }
        }
    }

    private Vector2 CalculateMousePosition()
    {
        _mousePosition.x = Input.mousePosition.x;
        _mousePosition.y = Input.mousePosition.y;
        _mousePosition.z = 5;
        return _mousePosition;
    }
    
    private int CalculateIndex(int x, int y)
    {
        return x + (y * 3);
    }

    private bool FieldChanged()
    {
        return _currentIndex != GetCurrentWorldRepresentation().WorldIndex;
    }

    private void ReleaseIngredient()
    {
        if (_currentIngredient != null)
        {
            if (_currentWorldRepresentation != GetCurrentWorldRepresentation())
            {
                _currentWorldRepresentation.DeleteSprite();
            }
            if (CurrentField.HasContent)
            {
                Ingredient temp = Combine(_currentIngredient, CurrentField.Ingredient);
                CurrentField.SetIngredient(temp);
                GetCurrentWorldRepresentation().ChangeSprite(IngredientSprite(temp.SpriteIndex));
            }
            else
            {
                CurrentField.SetIngredient(_currentIngredient);
                GetCurrentWorldRepresentation().ChangeSprite(IngredientSprite(_currentIngredient.SpriteIndex));
            }
            _currentWorldRepresentation.ReturnToStart();
            _currentIngredient = null;
        }
    }

    private void PickUpIngredient(Ingredient fieldIngredient)
    {
        _currentWorldRepresentation = GetCurrentWorldRepresentation();
        _currentIngredient = fieldIngredient;
        _currentWorldRepresentation.ChangeSprite(IngredientSprite(fieldIngredient.SpriteIndex));
        CurrentField.SetIngredient(null);
    }

    public Ingredient Combine(Ingredient current, Ingredient target)
    {
        IngredientType newType = CombinedIngredientType(current.ingredientType, target.ingredientType);
        return new Ingredient(newType);
    }


    private IngredientType CombinedIngredientType(IngredientType lhs, IngredientType rhs)
    {
        switch (rhs)
        {
            case IngredientType.Volunteer:
                if (lhs == IngredientType.Poison)
                {
                    return IngredientType.Zombie;
                }
                break;
            case IngredientType.Poison:
                if (lhs == IngredientType.Volunteer)
                {
                    return IngredientType.Zombie;
                }
                break;
            case IngredientType.Zombie:
                return IngredientType.Zombie;
            default:
                throw new System.Exception();
        }
        return lhs;
    }

    private WorldRepresentation GetCurrentWorldRepresentation()
    {
        if (_currentIndex >= 0 && _currentIndex < _worldSlots.Length)
        {
            return _worldSlots[_currentIndex];
        }
        return null;
    }

    private Field CurrentField { get { return _model.GetField(_currentIndex); } }

    private Sprite IngredientSprite(int index)
    {
       return _spriteManager.GetSprite((IngredientType)index);
    }

}
