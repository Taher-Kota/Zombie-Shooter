using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateFX : MonoBehaviour
{
    void OnEnable()
    {
        Invoke("Deactivate", 2f);
    }
    void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
