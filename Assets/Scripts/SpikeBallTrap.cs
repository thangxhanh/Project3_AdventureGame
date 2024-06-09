using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBallTrap : MonoBehaviour
{
    public Transform chainLink;
    public float swingSpeed = 1f;
    public float swingAngle = 45f;

    private Quaternion startRotation;
    private float t;

    private void Start()
    {
        startRotation = chainLink.localRotation;
    }

    private void Update()
    {
        t += Time.deltaTime * swingSpeed;
        float angle = Mathf.Sin(t) * swingAngle;
        Quaternion targetRotation = startRotation * Quaternion.Euler(0, 0, angle);
        chainLink.localRotation = targetRotation;
    }
}
