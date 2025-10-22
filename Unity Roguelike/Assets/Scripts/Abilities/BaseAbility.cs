using UnityEngine;

public enum AbilityType { Q, W, E, R }
public enum ClassType { Tank, Mage, Marksman, Fighter }

[CreateAssetMenu(fileName = "New Ability", menuName = "Ability")]
public class BaseAbility : ScriptableObject
{
    public string abilityName;
    public ClassType classType;
    public AbilityType abilityType;
    public Sprite icon;
    public float cooldown;
    public int level;
    public GameObject abilityPrefab;

    public virtual void Activate(GameObject caster)
    {
        // Override in derived classes
    }
}
