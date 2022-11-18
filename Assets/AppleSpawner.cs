using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AppleSpawner : MonoBehaviour
{
    public GameObject applePrefab;

    void Awake()
    {
        GameObject apple = Instantiate(applePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        apple.name = "Apple";
        apple.SetActive(true);
    }
}
