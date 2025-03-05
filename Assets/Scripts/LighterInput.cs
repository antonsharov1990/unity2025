using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LighterInput : MonoBehaviour
{
    private Light flashlight;

    private void Start()
    {
        flashlight = GetComponent<Light>();
    }
    private void OnSwitchLighter()
    {
        flashlight.enabled = ! flashlight.enabled;
    }
}
