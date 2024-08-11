using System;
using System.Collections;
using Random = UnityEngine.Random;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]

public class Cube : MonoBehaviour
{
    private Material _material;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _material = GetComponent<Material>();
        _rigidbody = GetComponent<Rigidbody>();
    }
}