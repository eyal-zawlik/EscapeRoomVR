using UnityEngine;

public class FixCamera : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    
            private void Start()
            {
                FixCanvasCam();
            }
    
            private void OnValidate()
            {
                FixCanvasCam();
            }
    
            private void FixCanvasCam()
            {
                if (canvas == null)
                    canvas = GetComponent<Canvas>();
                if (canvas.worldCamera != null) return;
                var cam = Camera.main;
                if (cam == null)
                {
                    var c = GameObject.FindGameObjectWithTag("MainCamera");
                    if (c == null) return;
                    cam = c.GetComponent<Camera>();
                }
    
                canvas.worldCamera = cam;
            }
}