using UnityEngine;

public class HealButtonBehavior : MonoBehaviour
{
    [SerializeField] private Unit _unit;
    private int _healingAmount = 5;

    public void ReceiveHealing()
    {
        _unit.ReceiveHealing(_healingAmount);
    }
}
