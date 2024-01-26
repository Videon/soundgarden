using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColliderTrigger : MonoBehaviour
{

    [SerializeField] private Collider collider;
    [SerializeField] private string tag = "Player";
    [SerializeField] private UnityEvent onTriggerEnterEvents;

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag(tag)) return;
        
        onTriggerEnterEvents?.Invoke();
    }
}
