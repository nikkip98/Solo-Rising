using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTransition : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
