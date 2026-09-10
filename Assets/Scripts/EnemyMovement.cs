using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
	public class EnemyMovement : MonoBehaviour
	{

		[SerializeField]
		Transform targetObj;

		const float SPEED = 2f;
		const float MIN_DISTANCE = 3f;

		Rigidbody myBody;

		private void Awake()
		{
			myBody = GetComponent<Rigidbody>();
		}

		void FixedUpdate()
		{
			var dirVector = (targetObj.position - transform.position);
			if (dirVector.sqrMagnitude > MIN_DISTANCE * MIN_DISTANCE)
			{
				myBody.linearVelocity = dirVector.normalized * SPEED;
			} else
			{
				myBody.linearVelocity = Vector3.zero;
			}
			
		}
	}
}