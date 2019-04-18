using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager instance;
    public ParticleSystem poofPrefab;

    private void Awake()
    {
        instance = this;
    }
}
