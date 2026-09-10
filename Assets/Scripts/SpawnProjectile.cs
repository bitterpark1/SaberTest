using UnityEngine;

[CreateAssetMenu(fileName = "SpawnProjectile", menuName = "ScriptableObjects/Abilities/SpawnProjectile")]
public class SpawnProjectile : Ability
{
	[SerializeField]
	Projectile projectile;
	[SerializeField]
	int damageBase;
	[SerializeField]
	float projectileSpeed;
	[SerializeField]
	DamageModifier[] abilityModifiers;
	[SerializeField]
	bool isAOE;
	[SerializeField]
	float aoeRange;

	public override void UseAbility(Creature owner, Vector3 ownerPosition, Vector3 targetCoords)
	{
		var newProjectile = Instantiate(projectile, ownerPosition, Quaternion.identity);
		newProjectile.Initialize(damageBase, (targetCoords - ownerPosition).normalized, projectileSpeed, owner, isAOE, aoeRange, abilityModifiers);
	}
}