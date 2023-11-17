using TMPro;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Unit _unit;
    [SerializeField] private TextMeshProUGUI _hpText;

    private void Update()
    {
        _hpText.text = _unit.CurrentHealthPublic.ToString();
    }
}
