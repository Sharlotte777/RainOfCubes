using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]

public class ExtraPlatform : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    public event Action WasContact;
    private int _amountOfContact = 0;
    private ColorChanger _colorChanger;

    private void Awake()
    {
        _colorChanger = gameObject.AddComponent<ColorChanger>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (_amountOfContact == 0)
        {
            _amountOfContact++;
            WasContact?.Invoke();
            _colorChanger.ChangeColor(other.gameObject);
            _spawner.StartTime(other.gameObject, ref _amountOfContact);
        }
    }
}
