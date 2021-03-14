using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerController : MonoBehaviour
{
    public string tagToDetect;
    public UnityEvent OnEnter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagToDetect))
        {
            if (OnEnter != null)
                OnEnter.Invoke();
        }
    }
}