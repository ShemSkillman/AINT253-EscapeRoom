using EscapeRoom.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingWater : MonoBehaviour
{
    [SerializeField] Transform targetHeight;
    [SerializeField] float maxGameDuration = 20f;

    float speed;
    bool reachedTarget = false;

    WaterHazard waterHazard;

    private void Awake()
    {
        waterHazard = FindObjectOfType<WaterHazard>();
    }

    private void Start()
    {
        float distanceToTravel = targetHeight.position.y - transform.position.y;

        speed = distanceToTravel / maxGameDuration;
    }
    
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        if (transform.position.y > targetHeight.position.y && reachedTarget == false)
        {
            reachedTarget = true;
            waterHazard.SeaLevelRiseComplete();
        }
    }
}
