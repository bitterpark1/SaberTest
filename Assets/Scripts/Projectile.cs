using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
	public class Projectile : MonoBehaviour
	{
		Rigidbody myBody;
		IDamageDealer source;
		Vector3 moveVector;

		private void Awake()
		{
			myBody = GetComponent<Rigidbody>();
		}

		public void Initialize(Vector3 direction, float speed, IDamageDealer source)
		{
			this.source = source;
			moveVector = direction.normalized * speed;
		}

		private void FixedUpdate()
		{
			myBody.MovePosition(transform.position + moveVector);
		}

		private void OnTriggerEnter(Collider collider)
		{
			var damageTakerComp = collider.GetComponent<IDamageTaker>();
			if (damageTakerComp != null)
			{
				EventBus<DamageDealtEventArgs>.Invoke(new DamageDealtEventArgs() { DamageDealer = source, DamageTaker = damageTakerComp, Target = collider.gameObject });
			}
			Destroy(gameObject);
		}
	}
}