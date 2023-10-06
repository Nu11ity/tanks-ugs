using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Lifetime : MonoBehaviour
{
    [SerializeField] float lifetime = 1f;

    private void Start()
    {
        Destroy(gameObject, lifetime);    
    }
}
