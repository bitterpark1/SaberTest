using System.Collections;
using UnityEngine;


public abstract class Ability: ScriptableObject
{
	public Sprite Icon { get => icon; }
	public float Cooldown { get => cooldown; }

	[SerializeField]
	Sprite icon;
	[SerializeField]
	float cooldown;

	public abstract void UseAbility(Creature owner, Vector3 ownerCoords, Vector3 targetCoords);

}


