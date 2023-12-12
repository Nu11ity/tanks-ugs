using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnDestroy : MonoBehaviour
{
    [SerializeField] GameObject prefab;

    private void OnDestroy()
    {
        if(!gameObject.scene.isLoaded) { return; }   

        Instantiate(prefab, transform.position, Quaternion.identity);
    }
}
