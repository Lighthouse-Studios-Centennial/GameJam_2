using System;
using System.Collections;
using UnityEngine;

public class AudioZone : MonoBehaviour
{
    [SerializeField] AudioZoneManager audioZoneManager;
    [SerializeField] AudioClip songToBePlayed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] float startingTime = 0f;
    float endingTime = -1f;

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Aie!! {other.tag}");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Aie!");
            audioZoneManager.PlaySong(songToBePlayed, startingTime);
        }
    }
}
