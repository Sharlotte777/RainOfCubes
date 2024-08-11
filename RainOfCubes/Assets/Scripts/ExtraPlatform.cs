using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]

public class ExtraPlatform : MonoBehaviour
{
    public event Action WasContact;
    private int _amountOfContact = 0;
    [SerializeField] private Spawner _spawner;

    private void OnCollisionEnter(Collision other)
    {
        if (_amountOfContact == 0)
        {
            _amountOfContact++;
            WasContact?.Invoke();
            _spawner.ChangeColor(other.gameObject);
            _spawner.StartTime(other.gameObject, ref _amountOfContact);
        }
    }
}
