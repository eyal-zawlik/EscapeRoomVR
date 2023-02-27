using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PuzzleLock : MonoBehaviour
{
    [SerializeField] private XRSocketInteractor socketInteractor;
    [SerializeField] private bool isCoinSocket;
    [SerializeField] private AudioClip soundClip;
    public void OnSelectCoin()
    {
        AudioSource.PlayClipAtPoint(soundClip, transform.position);
        Invoke(nameof(RemoveGrab), 0.5f);
    }

    private void RemoveGrab()
    {
        var coin = socketInteractor.GetOldestInteractableSelected().transform;
        Destroy(coin.GetComponent<XRGrabInteractable>());
        Destroy(coin.GetComponent<Rigidbody>());
        if(isCoinSocket)
            GameManager.instance.solvedCoins++;
        else
            GameManager.instance.foundMissingPuzzle = true;

        if (GameManager.instance.VerifyPuzzles())
        {
            GameManager.instance.GameOver();
        }
    }
}
