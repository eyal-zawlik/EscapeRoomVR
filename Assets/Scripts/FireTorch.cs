using UnityEngine;

public class FireTorch : MonoBehaviour
{
    public static float StartFireDelay = 1;
    
    [Header("Reference")] [SerializeField] private GameObject fireParticle;

    [Header("Audio Clip")] [SerializeField]
    private AudioClip fireStartEffect;

    [SerializeField] private AudioSource fireAudio;
    [SerializeField] private bool isHandTorch;
    
    private bool _hasLookedAt;

    private void Awake()
    {
        fireParticle.SetActive(false);
        fireAudio.gameObject.SetActive(false);
    }

    public void Fire(bool isFire)
    {
        fireParticle.SetActive(isFire);
    }

    private void OnBecameVisible()
    {
        if (_hasLookedAt)
        {
            if(fireAudio.gameObject.activeSelf)
                fireAudio.Play();
            return;
        }
        if (isHandTorch) return;
        _hasLookedAt = true;
        StartFireDelay += .5f;
        Invoke(nameof(StartFire), StartFireDelay);
    }

    private void OnBecameInvisible()
    {
        fireAudio.Pause();
    }

    private void StartFire()
    {
        fireParticle.SetActive(true);
        fireAudio.gameObject.SetActive(true);
        // give sound effect
        StartFireDelay -= .5f;
        AudioSource.PlayClipAtPoint(fireStartEffect, transform.position, .8f);
        fireAudio.PlayDelayed(StartFireDelay);
    }
}