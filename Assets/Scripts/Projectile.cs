using System.Collections;
using UnityEngine;


public class Projectile : MonoBehaviour
{
	Rigidbody myBody;
	Creature source;
	DamageModifier[] abilityModifiers;
	Vector3 moveVector;
	int baseDamage;

	bool isAOE;
	float aoeRange;
	bool isStopped = false;

	const float EXPLOSION_TIME = 0.5f;

	private void Awake()
	{
		myBody = GetComponent<Rigidbody>();
	}

	public void Initialize(
		int baseDamage, 
		Vector3 direction, 
		float speed, 
		Creature source, 
		bool isAOE, 
		float aoeRange, 
		DamageModifier[] abilityModifiers = null)
	{
		this.source = source;
		this.abilityModifiers = abilityModifiers;
		this.isAOE = isAOE;
		this.aoeRange = aoeRange;
		this.baseDamage = baseDamage;
		moveVector = direction.normalized * speed;
	}

	private void FixedUpdate()
	{
		if (!isStopped)
		{
			myBody.MovePosition(transform.position + moveVector);
		}
	}

	private void OnTriggerEnter(Collider collider)
	{
		if (isStopped)
		{
			return;
		}

		if (isAOE)
		{
			DoAOE();
		}
		else
		{
			DoSingleTarget(collider);
		}
	}

	private void DoSingleTarget(Collider collider)
	{
		var damageTaker = collider.GetComponent<Creature>();
		if (damageTaker != null)
		{
			CallDamageEvent(damageTaker, true);
		}
		Destroy(gameObject);
	}

	private void DoAOE()
	{
		isStopped = true;
		var overlap = Physics.OverlapSphere(transform.position, aoeRange, ~myBody.excludeLayers.value);
		bool hitAlreadyRegistered = false;
		
		foreach (var overlapCollider in overlap)
		{
			var damageTaker = overlapCollider.GetComponent<Creature>();
			if (damageTaker != null)
			{	
				CallDamageEvent(damageTaker, !hitAlreadyRegistered);
				hitAlreadyRegistered = true;
			}
		}

		StartCoroutine(Explode());
	}

	void CallDamageEvent(Creature target, bool countAsSeparateHit)
	{
		EventBus<TryDealDamageEventArgs>.Invoke(new TryDealDamageEventArgs()
		{
			BaseDamage = baseDamage,
			DamageDealerStats = source.Stats,
			DamageTakerStats = target.Stats,
			Target = target.gameObject,
			AbilityModifiers = abilityModifiers,
			CountAsSeparateHit = countAsSeparateHit
		});
	}

	IEnumerator Explode()
	{
		var startingScale = transform.localScale;
		var endScale = Vector3.one * aoeRange;

		float elapsedTime = 0;

		while (elapsedTime < EXPLOSION_TIME)
		{
			transform.localScale = Vector3.Lerp(startingScale, endScale, elapsedTime / EXPLOSION_TIME);
			yield return null;
			elapsedTime += Time.deltaTime;
		}
		Destroy(gameObject);
	}
}