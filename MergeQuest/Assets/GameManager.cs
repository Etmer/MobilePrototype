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
    [SerializeField] private Texture _text;
    [SerializeField] private Field _lastField;
    [SerializeField] private LevelGenerator _levelGenerator;
    [SerializeField] private MapData _mapdata;
    [SerializeField] private Transform _startPoint;

    private WorldRepresentation _currentWorldRepresentation;

    public static GameManager instance;

    private Backend _backEnd;

    void Start()
    {
        _backEnd = new Backend();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
        GameMetrics.Init(_defaultSprite,4);
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
        _currentIndex = GameMetrics.VectorToIndex(_worldPosition - _startPoint.position);
        if (_playArea.Contains(new Vector2(_worldPosition.x, _worldPosition.y)))
        {
            if (Input.GetMouseButtonDown(0) && GetCurrentField.HasContent)
            {
                PickUpIngredient(GetCurrentField.Ingredient);
                _currentWorldRepresentation.Lift(true);
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
            if (GetCurrentField.HasContent)
            {
                Ingredient temp;
                if (Combine(_currentIngredient, GetCurrentField.Ingredient, out temp))
                {
                    GetCurrentField.SetIngredient(temp);
                    GetCurrentWorldRepresentation().ChangeSprite(IngredientSprite(temp.ingredientType));

                    if (_currentWorldRepresentation != GetCurrentWorldRepresentation())
                    {
                        _currentWorldRepresentation.DeleteSprite();
                    }
                }
                else
                {
                    _lastField.SetIngredient(_currentIngredient);
                }
            }
            else
            {
                ChangeField();
            }
            _currentWorldRepresentation.ReturnToStart();
            _currentWorldRepresentation.Lift(false);
            _currentIngredient = null;
        }
    }

    private void ChangeField()
    {
        Debug.Log("Field changed");
        GetCurrentField.SetIngredient(_currentIngredient);
        GetCurrentWorldRepresentation().ChangeSprite(IngredientSprite(_currentIngredient.ingredientType));
        if (_currentWorldRepresentation != GetCurrentWorldRepresentation())
        {
            _currentWorldRepresentation.DeleteSprite();
        }
    }

    private void PickUpIngredient(Ingredient fieldIngredient)
    {
        Debug.Log(fieldIngredient.ingredientType);
        _currentWorldRepresentation = GetCurrentWorldRepresentation();
        _currentIngredient = fieldIngredient;
        _currentWorldRepresentation.ChangeSprite(IngredientSprite(fieldIngredient.ingredientType));
        GetCurrentField.SetIngredient(null);
        _lastField = GetCurrentField;
    }

    public bool Combine(Ingredient current, Ingredient target, out Ingredient output)
    {
        int newType = (int)current.ingredientType + ((int)target.ingredientType * CSVParser.lineLength);
        if (CSVParser.instance.combinations.ContainsKey(newType))
        {
            output = new Ingredient(_backEnd.GetData(newType));
            return true;
        }
        output = null;
        return false;
    }
    
    private WorldRepresentation GetCurrentWorldRepresentation()
    {
        if (_currentIndex >= 0 && _currentIndex < _worldSlots.Length)
        {
            return _worldSlots[_currentIndex];
        }
        return null;
    }

    private Field GetCurrentField { get { return _model.GetField(_currentIndex); } }

    private Sprite IngredientSprite(IngredientType ingredientType)
    {
        int index = (int)ingredientType;
        string Id = "ID-" + index.ToString("D2");
        return _backEnd.GetSprite(Id);
    }


    public Backend GameBackEnd { get { return _backEnd; } }
}
