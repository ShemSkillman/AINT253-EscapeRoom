using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameCollider : MonoBehaviour
{
    public delegate void OnDetectPlayer();
    public event OnDetectPlayer onDetectPlayer;

    private void OnTriggerEnter(Collider other)
    {
        onDetectPlayer();
    }
}
