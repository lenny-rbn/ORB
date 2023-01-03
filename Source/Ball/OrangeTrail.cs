using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeTrail : MonoBehaviour
{
    [SerializeField] private TrailRenderer trail1;
    [SerializeField] private TrailRenderer trail2;
    [SerializeField] private TrailRenderer trail3;
    [SerializeField] private TrailRenderer trail4;

    public void DisableTrails()
    {
        trail1.enabled = false;
        trail2.enabled = false;
        trail3.enabled = false;
        trail4.enabled = false;
    }

    public void ClearTrails()
    {
        trail1.Clear();
        trail2.Clear();
        trail3.Clear();
        trail4.Clear();
    }

    public void TrailGestion(float ballForce)
    {
        if (ballForce < 4)
        {
            trail1.enabled = true;
            trail2.enabled = false;
            trail3.enabled = false;
            trail4.enabled = false;
        }
        else if (ballForce < 8)
        {
            trail1.enabled = false;
            trail2.enabled = true;
            trail3.enabled = false;
            trail3.enabled = false;
        }
        else if (ballForce < 12)
        {
            trail1.enabled = false;
            trail2.enabled = false;
            trail3.enabled = true;
            trail4.enabled = false;
        }
        else
        {
            trail1.enabled = false;
            trail2.enabled = false;
            trail3.enabled = false;
            trail4.enabled = true;
        }
    }
}
