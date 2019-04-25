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
    private WorldRepresentation _currentWorldRepresentation;
    private int _currentIndex;
    private Ingredient _currentIngredient;
    [SerializeField] private WorldRepresentation[] _worldSlots;
    [SerializeField] private Transform _prefab;
    [SerializeField] private Rect _playArea;
    [SerializeField] private SpriteManager _spriteManager;
    [SerializeField] private Texture _text;

    [Header("Fields")]
    private Field _currentField;

    [SerializeField] private LevelGenerator _levelGenerator;
    [SerializeField] private MapData _mapdata;


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
        Debug.Log(_currentIndex);
        if (_playArea.Contains(new Vector2(_worldPosition.x, _worldPosition.y)))
        {
            if (Input.GetMouseButtonDown(0) && _model.GetField(_currentIndex).HasContent)
            {
                _currentIngredient = _model.GetField(_currentIndex).GetIngredient.PickUp();
                _currentWorldRepresentation = _worldSlots[_currentIndex];
                _currentField = _model.GetField(_currentIndex);
            }
            if (_currentIngredient != null)
            {
                if (Input.GetMouseButton(0))
                {
                    _currentWorldRepresentation.Move(_worldPosition);
                }
                else
                {
                    if (_model.GetField(_currentIndex).HasContent)
                    {
                        if (_currentIngredient.index == _model.GetField(_currentIndex).GetIngredient.index)
                        {
                            Ingredient newIngredient = _currentIngredient.Combine(_model.GetField(_currentIndex).GetIngredient);
                            _model.GetField(_currentIndex).SetIngredient(newIngredient);
                            _worldSlots[_currentIndex].ChangeSprite(_spriteManager.GetSprite(newIngredient.ingredientType));
                            _currentWorldRepresentation.DeleteSprite();
                            _currentField.SetIngredient(null);
                        }
                        _currentWorldRepresentation.ReturnToStart();
                    }
                    else
                    {
                        if (FieldChanged())
                        {
                            _currentField.DecoupleIngredient();
                            _worldSlots[_currentIndex].ChangeSprite(_currentWorldRepresentation.GetSprite());
                            _currentWorldRepresentation.DeleteSprite();
                            _currentField = null;
                        }
                        else
                        {
                            _currentWorldRepresentation.ReturnToStart();
                        }
                        _model.GetField(_currentIndex).SetIngredient(_currentIngredient);
                    }
                    _currentIngredient = null;
                }
            }
        }
        else
        {
            if (_currentWorldRepresentation != null)
            {
                _currentWorldRepresentation.ReturnToStart();
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
        return _currentIndex != _currentWorldRepresentation.WorldIndex;
    }

    private void ReleaseIngredient()
    {

    } 
}
