using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public void ChangeColor(GameObject cubePrefab)
    {
        cubePrefab.gameObject.GetComponent<Renderer>().material.color = Random.ColorHSV();
    }
}
