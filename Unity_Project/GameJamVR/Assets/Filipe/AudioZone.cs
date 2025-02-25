using System;
using System.Collections;
using UnityEngine;

public class AudioZone : MonoBehaviour
{
    [SerializeField] AudioSource source;

    void OnTriggerEnter(Collider other)
    {
      Debug.Log("OnTriggerEnter");
        if (other.CompareTag("Player"))
        {
            source.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            source.Stop();
        }
    }
}
