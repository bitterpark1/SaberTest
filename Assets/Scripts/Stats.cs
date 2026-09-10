using System.Collections;
using UnityEngine;

[System.Serializable]
public class Stats
{
	public int BaseHealth { get => baseHealth; }
	public float MoveSpeed { get => moveSpeed; }

	[SerializeField]
	int baseHealth;
	[SerializeField]
	float moveSpeed;

	public DamageModifier[] DealerModifiers => damageDealerModifiers;
	public DamageModifier[] TakerModifiers => damageTakerModifiers;

	[SerializeField]
	DamageModifier[] damageDealerModifiers;
	[SerializeField]
	DamageModifier[] damageTakerModifiers;
}