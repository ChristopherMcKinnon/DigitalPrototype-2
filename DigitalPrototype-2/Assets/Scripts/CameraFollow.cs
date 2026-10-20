using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] private Transform Player;
    [SerializeField] private float camSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    void FixedUpdate()
    {
        if (Player == null) return;

        Vector2 target = Player.position;
        Vector3 pos = transform.position;
        pos.x = Mathf.Lerp(pos.x, target.x, camSpeed * Time.deltaTime);
        pos.y = Mathf.Lerp(pos.y, target.y, camSpeed * Time.deltaTime);
        transform.position = pos;
    }
}
