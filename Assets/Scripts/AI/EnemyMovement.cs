using System.Collections;
using UnityEngine;


public class EnemyMovement : MonoBehaviour
{
	float speed;
	[SerializeField]
	float stopDistance = 3f;

	Rigidbody myBody;
	EnemyTarget target;

	const float ROTATION_SPEED = 5f;


	private void Awake()
	{
		myBody = GetComponent<Rigidbody>();
		speed = GetComponent<Creature>().Stats.MoveSpeed;
		target = GetComponent<EnemyTarget>();
	}

	private void FixedUpdate()
	{
		var dirVector = (target.TargetObj.position - transform.position);
		if (dirVector.sqrMagnitude > stopDistance * stopDistance)
		{
			myBody.linearVelocity = dirVector.normalized * speed;
		} else
		{
			myBody.linearVelocity = Vector3.zero;
		}
		myBody.MoveRotation(Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dirVector), ROTATION_SPEED));


	}
}