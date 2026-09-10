using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
	public class EnemyMovement : MonoBehaviour
	{

		[SerializeField]
		Transform targetObj;

		float speed;
		[SerializeField]
		float stopDistance = 3f;

		Rigidbody myBody;

		private void Awake()
		{
			myBody = GetComponent<Rigidbody>();
			speed = GetComponent<Creature>().Stats.MoveSpeed;
		}

		void FixedUpdate()
		{
			var dirVector = (targetObj.position - transform.position);
			if (dirVector.sqrMagnitude > stopDistance * stopDistance)
			{
				myBody.linearVelocity = dirVector.normalized * speed;
			} else
			{
				myBody.linearVelocity = Vector3.zero;
			}
			
		}
	}
}