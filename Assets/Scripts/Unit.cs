using System.Collections;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private int _baseHealth = 100;
    private int _currentHealth;
    private bool _healingIsActive;
    
    public int CurrentHealthPublic
    {
        get { return _currentHealth; }
        set { _currentHealth = value; }
    }

    void Start()
    {
        _currentHealth = _baseHealth;
        _healingIsActive = false;
    }
    public void TakeDamage(int _damageAmount)
    {
        if (_currentHealth > 1)
        {
            _currentHealth -= _damageAmount;
        }
        
        else if (_currentHealth <= 1)
        {
            _currentHealth = 1;
        }
    }
    
    public void ReceiveHealing(int _healingAmount)
    {
        StartCoroutine(HealingCoroutine(_healingAmount));
        _healingIsActive = true;
    }

    IEnumerator HealingCoroutine(int _healingAmount)
    {
        if (!_healingIsActive)
        {
            for (int i = 0; i < 6; i++)
            {
                yield return new WaitForSeconds(0.5f);
                _currentHealth += _healingAmount;
            
                if (_currentHealth > 100)
                {
                    _currentHealth = 100;
                    _healingIsActive = false;
                    yield break;
                }
            }
            _healingIsActive = false;
        }
    }
}
