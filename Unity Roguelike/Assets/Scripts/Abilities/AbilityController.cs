using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class AbilityController : MonoBehaviour
{
    public BaseAbility Q;
    public BaseAbility W;
    public BaseAbility E;
    public BaseAbility R;

    private Dictionary<AbilityType, float> cooldownTimers = new();

    void Update()
    {
        HandleAbility(Q, AbilityType.Q, Keyboard.current.qKey);
        HandleAbility(W, AbilityType.W, Keyboard.current.wKey);
        HandleAbility(E, AbilityType.E, Keyboard.current.eKey);
        HandleAbility(R, AbilityType.R, Keyboard.current.rKey);
    }

    void HandleAbility(BaseAbility ability, AbilityType type, KeyControl key)
    {
        if (ability == null || !key.wasReleasedThisFrame) return;

        if (!cooldownTimers.ContainsKey(type) || Time.time >= cooldownTimers[type])
        {
            ability.Activate(gameObject);
            cooldownTimers[type] = Time.time + ability.cooldown;
        }
    }

    public void EquipAbility(BaseAbility newAbility)
    {
        switch (newAbility.abilityType)
        {
            case AbilityType.Q: Q = newAbility; break;
            case AbilityType.W: W = newAbility; break;
            case AbilityType.E: E = newAbility; break;
            case AbilityType.R: R = newAbility; break;
        }
    }
}

