using System.Collections;
using UnityEngine;


public class Creature : MonoBehaviour
{
	public Rigidbody Body { get => myBody; }
	Rigidbody myBody;

	public Stats Stats { get => stats; }

	[SerializeField]
	Stats stats;

	private void Awake()
	{
		myBody = GetComponent<Rigidbody>();	
	}

}
