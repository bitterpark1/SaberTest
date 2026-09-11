using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
	public class PlayerController : MonoBehaviour
	{
		[SerializeField]
		Ability[] abilities;
		[SerializeField]
		Ability mainAttackAbility;

		Rigidbody myBody;
		float[] abilityCooldowns;

		float speed = 5f;
		Vector2 inputVector;

		bool isDashing = false;
		float currentDashSpeed;
		Vector3 dashDirection;

		private void Awake()
		{
			myBody = GetComponent<Rigidbody>();
			speed = GetComponent<Creature>().Stats.MoveSpeed;
			abilityCooldowns = new float[abilities.Length];	
			EventBus<GameStateChangedEventArgs>.Subscribe(OnGameStateChanged);
		}

		private void OnDestroy()
		{
			EventBus<GameStateChangedEventArgs>.Unsubscribe(OnGameStateChanged);
		}

		private void OnGameStateChanged(GameStateChangedEventArgs obj)
		{
			if (obj.NewState == GameStateManager.State.Gameplay)
			{
				gameObject.SetActive(true);
				EventBus<PlayerAbilitiesSetEventArgs>.Invoke(new PlayerAbilitiesSetEventArgs() { Abilities = abilities });
			} else
			{
				gameObject.SetActive(false);
			}
		}

		private void Update()
		{
			GetMovementInputs();

			UpdateAbilityCooldowns();	
			if (isDashing)
			{
				UpdateDashSpeed();
			} else
			{
				UseAbilities();
			}
		}

		private void UseAbilities()
		{
			if (Input.GetMouseButton(0))
			{
				TryUseAbility(0);
			}

			if (Input.GetMouseButtonDown(1))
			{
				TryUseAbility(1);
			}

			if (Input.GetKeyDown(KeyCode.LeftShift))
			{
				TryUseAbility(2);
			}
		}

		private void UpdateAbilityCooldowns()
		{
			for (int i = 0; i < abilityCooldowns.Length; i++)
			{
				var remainingTime = abilityCooldowns[i];
				if (remainingTime > 0)
				{
					var cooldownTimeMax = abilities[i].Cooldown;
					if (cooldownTimeMax <= 0)
					{
						continue;
					}

					remainingTime -= Time.deltaTime;
					abilityCooldowns[i] = remainingTime;
					EventBus<AbilityCooldownEventArgs>.Invoke(new AbilityCooldownEventArgs() { AbilityIndex = i, CooldownPercentage = remainingTime / cooldownTimeMax });
				}
			}
		}

		public Vector3 GetWorldMousePos()
		{
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Plane plane = new Plane(Vector3.up, Vector3.zero);
			plane.Raycast(ray, out float dist);
			return ray.GetPoint(dist);
		}

		private void TryUseAbility(int index)
		{
			if (index < abilities.Length && abilityCooldowns[index] <= 0)
			{
				var ability = abilities[index];
				ability.UseAbility(GetComponent<Creature>(), transform.position, GetWorldMousePos());
				abilityCooldowns[index] = ability.Cooldown;
			}
		}

		private void GetMovementInputs()
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

			if (!isDashing && Input.GetKeyDown(KeyCode.Space))
			{
				dashDirection = new Vector3(inputVector.x, 0, inputVector.y);
				currentDashSpeed = 30f;
				isDashing = true;
				
			}
		}

		private void FixedUpdate()
		{
			UpdateMovement();
		}

		private void UpdateDashSpeed()
		{
			float speedReduction = 5f;
			currentDashSpeed -= currentDashSpeed * speedReduction * Time.deltaTime;
			if (currentDashSpeed <= 5f)
			{
				isDashing = false;
			}
		}

		private void UpdateMovement()
		{
			if (isDashing)
			{
				myBody.linearVelocity = dashDirection * currentDashSpeed;
			} else
			{
				var moveVector = new Vector3(inputVector.x, 0, inputVector.y) * speed;
				myBody.linearVelocity = moveVector;
			}
		}
	}
}