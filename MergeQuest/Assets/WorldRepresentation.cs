using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldRepresentation 
{
    private Transform _worldTransform;
    private SpriteRenderer _renderer;
    private Vector3 _startPosition;

    public WorldRepresentation(Transform worldTransform, SpriteRenderer renderer)
    {
        _worldTransform = worldTransform;
        _startPosition = _worldTransform.position;
        _renderer = renderer;
    }

    public void PickUp()
    {
    }

    public void Move(Vector2 Position)
    {
        _worldTransform.position = Position;
    }

    public void ReturnToStart()
    {
        _worldTransform.position = _startPosition;
    }

    public void ChangeSprite(Sprite targetSprite)
    {
        _renderer.sprite = targetSprite;
    }

    public void DeleteSprite()
    {
        _renderer.sprite = null;
        ReturnToStart();
    }

    public Sprite GetSprite()
    {
        return _renderer.sprite;
    }
}
