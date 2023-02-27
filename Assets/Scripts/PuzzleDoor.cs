using System.Linq;
using TMPro;
using UnityEngine;

public class PuzzleDoor : MonoBehaviour
{
    [SerializeField] private NumericWheel[] wheels;
    [SerializeField] private AudioClip soundFx;
    [SerializeField] private TMP_Text codetxt;

    public void VerifyCode()
    {
        var code = wheels.Aggregate("", (current, wheel) => current + $"{wheel.currentValue}");
        // var code = int.Parse(wheels.Aggregate("", (current, wheel) => current + (GetNumberFromRotation(wheel) + "")));
        codetxt.text = $"(a+b) * c = {code}";
        if (GameManager.instance.clockCode == int.Parse(code))
        {
            GameManager.instance.clockCodeSolved = true;
            AudioSource.PlayClipAtPoint(soundFx, wheels[0].transform.position);
            // do some magic.
            foreach (var wheel in wheels)
            {
                wheel.enabled = false;
            }
            if (GameManager.instance.VerifyPuzzles())
            {
                GameManager.instance.GameOver();
            }
        }
    }
}
