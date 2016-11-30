using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BoidController : MonoBehaviour
{
	public float minVelocity = 5;
	public float maxVelocity = 20;
	public float randomOffset = 1;
	public int flockSize = 20;

	public BoidFlocking boidprefab;
	public Transform target;

	internal Vector3 midPoint;
	internal Vector3 flockVelocity;

	List<BoidFlocking> boids = new List<BoidFlocking>();

	void Start()
	{
		for (int i = 0; i < flockSize; i++)
		{
			BoidFlocking boid = Instantiate(boidprefab, transform.position, transform.rotation) as BoidFlocking;
			boid.transform.parent = transform;
			boid.transform.localPosition = new Vector3(
							Random.value * GetComponent<Collider>().bounds.size.x,Random.value * GetComponent<Collider>().bounds.size.y,
							Random.value * GetComponent<Collider>().bounds.size.z) - GetComponent<Collider>().bounds.extents;

			boid.controller = this;
			boids.Add(boid);
		}
	}

	void Update()
	{
		Vector3 center = Vector3.zero;
		Vector3 velocity = Vector3.zero;

		foreach (BoidFlocking boid in boids)
		{
			center += boid.transform.localPosition;
			velocity += boid.GetComponent<Rigidbody>().velocity;
		}

		midPoint = center / flockSize;
		flockVelocity = velocity / flockSize;
	}
}