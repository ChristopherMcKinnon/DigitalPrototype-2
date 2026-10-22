using System;
using UnityEngine;

public class BackgroundTracking : MonoBehaviour
{

    [SerializeField] private Transform Camera;
    [SerializeField] private float bgSpeed;
    [SerializeField] private bool followY;
    [SerializeField] private bool followX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    void FixedUpdate()
    {
        if (Camera == null) return;

        Vector2 target = Camera.position;
        Vector3 pos = transform.position;
        if (followX) pos.x = Mathf.Lerp(pos.x, target.x, bgSpeed * Time.deltaTime);
        if (followY) pos.y = Mathf.Lerp(pos.y, target.y, bgSpeed * Time.deltaTime);
        if (target.x < -4 && target.x > -6) pos = target;
        transform.position = pos;
    }
}
