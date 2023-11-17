using UnityEngine;
using Random = UnityEngine.Random;

public class DamageButtonBehavior : MonoBehaviour
{
   [SerializeField] private Unit _unit;
   private int _damageAmount;

   public void MakeSomeDamage()
   {
      _damageAmount = Random.Range(7, 13);
      _unit.TakeDamage(_damageAmount);
   }
}
