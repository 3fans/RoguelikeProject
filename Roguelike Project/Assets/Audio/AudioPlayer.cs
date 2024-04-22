using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = GameInstance.Instance.gameVolume;
        audioSource.Play();

    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            GameObject.Destroy(gameObject);
        }
    }
}
