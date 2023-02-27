using UnityEngine;

public class GramophonePlayer : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    private bool _firstInteraction;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (_firstInteraction) return;
        audioSource.Play();
        _firstInteraction = true;
    }

    private void OnValidate()
    {
        audioSource = GetComponent<AudioSource>();
    }
}
