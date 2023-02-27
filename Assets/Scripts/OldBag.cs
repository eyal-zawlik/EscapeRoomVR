using System.Collections;
using UnityEngine;

public class OldBag : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer meshRenderer;
    
    [SerializeField] private float openTime = .8f;
    private float _timer = 0f;
    private bool _isOpening = false;

    public void OpenBag(bool open = true)
    {
        if (open && !_isOpening)
        {
            _isOpening = true;
            StartCoroutine(OpenBagCoroutine());
        }
        else if (!open && _isOpening)
        {
            _isOpening = false;
            StopCoroutine(OpenBagCoroutine(100, 0));
        }
    }

    IEnumerator OpenBagCoroutine(float start = 0, float end = 100)
    {
        _timer = 0f;
        while (_timer < openTime)
        {
            var blendShapeWeight = Mathf.Lerp(start, end, _timer / openTime);
            meshRenderer.SetBlendShapeWeight(0, blendShapeWeight);
            yield return null;
            _timer += Time.deltaTime;
        }
        meshRenderer.SetBlendShapeWeight(0, end);
        _isOpening = false;
    }
}





