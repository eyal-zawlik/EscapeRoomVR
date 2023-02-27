using UnityEngine;

public class NumericWheel : MonoBehaviour
{
   public int currentValue = 0;
   [SerializeField] private Animator animator;
   [SerializeField] private AudioClip soundFx;
   private void OnValidate()
   {
      if (animator == null) animator = GetComponent<Animator>();
   }

   public void SetValue(int index)
   {
      var codeStr = $"{GameManager.instance.clockCode}".PadLeft(3, '0');
      currentValue = int.Parse(codeStr[index]+"");
      animator.SetInteger("Value", currentValue);
   }

   private void OnTriggerEnter(Collider other)
   {
      if (!other.CompareTag("HandController")) return;
      currentValue++;
      if (currentValue > 9) currentValue = 0;
      animator.SetInteger("Value", currentValue);
      GameManager.instance.puzzleDoor.VerifyCode();
      AudioSource.PlayClipAtPoint(soundFx, transform.position);
   }
}
