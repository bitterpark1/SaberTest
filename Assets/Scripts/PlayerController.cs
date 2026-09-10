using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
	public class PlayerController : MonoBehaviour
	{
		Rigidbody myBody;

		const float SPEED = 5f;

		private void Awake()
		{
			myBody = GetComponent<Rigidbody>();
		}

		Vector2 inputVector;

		private void Update()
		{
			GetInputs();
		}

		private void GetInputs()
		{
			int inputX = 0;
			int inputY = 0;

			if (Input.GetKey(KeyCode.W))
			{
				inputY += 1;
			}
			if (Input.GetKey(KeyCode.A))
			{
				inputX -= 1;
			}
			if (Input.GetKey(KeyCode.S))
			{
				inputY -= 1;
			}
			if (Input.GetKey(KeyCode.D))
			{
				inputX += 1;
			}

			inputVector = new Vector2(inputX, inputY).normalized;
		}

		private void FixedUpdate()
		{
			var moveVector = new Vector3(inputVector.x, 0, inputVector.y) * SPEED;
			myBody.linearVelocity = moveVector;
		}

	}
}