using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CsCameraShake : MonoBehaviour {


    public struct ShakeCamera : IEvent { }


    public Properties testProperties;

    private Vector3 startPosition;
    private Vector3 startEularAngles;

    const float maxAngle = 10f;
	IEnumerator currentShakeCoroutine;
    Coroutine coroutineShake;


    /*
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EventManager.TriggerEvent(new ShakeCamera());
        }
    }
    */

    private void OnEnable()
    {
        EventManager.StartListening(typeof(ShakeCamera), RunShakeCamera);
    }

    private void OnDisable()
    {
        EventManager.StopListening(typeof(ShakeCamera), RunShakeCamera);
    }


    void RunShakeCamera(IEvent param) {
        StartShake(testProperties);
    }

    public void StartShake(Properties properties) {
		if (currentShakeCoroutine != null) {
			StopCoroutine (currentShakeCoroutine);
		}

		currentShakeCoroutine = Shake (properties);

        if (coroutineShake != null)
        {
            StopCoroutine(coroutineShake);
            transform.localPosition = startPosition;
            transform.localEulerAngles = startEularAngles;
        }

        coroutineShake = StartCoroutine(currentShakeCoroutine);
	}

	IEnumerator Shake(Properties properties) {
		float completionPercent = 0;
		float movePercent = 0;

		float angle_radians = properties.angle * Mathf.Deg2Rad - Mathf.PI;
		Vector3 previousWaypoint = Vector3.zero;
		Vector3 currentWaypoint = Vector3.zero;
		float moveDistance = 0;
		float speed = 0;

		Quaternion targetRotation = Quaternion.identity;
		Quaternion previousRotation = Quaternion.identity;

        startPosition = transform.localPosition;
        startEularAngles = transform.localEulerAngles;


        do {
			if (movePercent >= 1 || completionPercent == 0) {
				float dampingFactor = DampingCurve (completionPercent, properties.dampingPercent);
				float noiseAngle = (Random.value - .5f) * Mathf.PI;
				angle_radians += Mathf.PI + noiseAngle * properties.noisePercent;
				currentWaypoint = new Vector3 (Mathf.Cos (angle_radians), Mathf.Sin (angle_radians)) * properties.strength * dampingFactor;
				previousWaypoint = transform.localPosition - startPosition;
				moveDistance = Vector3.Distance (currentWaypoint, previousWaypoint);

				targetRotation = Quaternion.Euler (new Vector3 (currentWaypoint.y, currentWaypoint.x).normalized * properties.rotationPercent * dampingFactor * maxAngle);
				previousRotation = transform.localRotation;

				speed = Mathf.Lerp(properties.minSpeed,properties.maxSpeed,dampingFactor);

			}

			completionPercent += Time.deltaTime / properties.duration;
			movePercent += Time.deltaTime / moveDistance * speed;
			transform.localPosition = startPosition + Vector3.Lerp (previousWaypoint, currentWaypoint, movePercent);
			transform.localEulerAngles = startEularAngles + Quaternion.Slerp (previousRotation, targetRotation, movePercent).eulerAngles;

			yield return null;
		} while (moveDistance > 0 && completionPercent < 1.0f);

        coroutineShake = null;
    }

	float DampingCurve(float x, float dampingPercent) {
		x = Mathf.Clamp01 (x);
		float a = Mathf.Lerp (2, .25f, dampingPercent);
		float b = 1 - Mathf.Pow (x, a);
		return b * b * b;
	}


	[System.Serializable]
	public class Properties {
		public float angle;
		public float strength;
		public float maxSpeed;
		public float minSpeed;
		public float duration;
		[Range(0,1)]
		public float noisePercent;
		[Range(0,1)]
		public float dampingPercent;
		[Range(0,1)]
		public float rotationPercent;

		public Properties (float angle, float strength, float speed, float duration, float noisePercent, float dampingPercent, float rotationPercent)
		{
			this.angle = angle;
			this.strength = strength;
			this.maxSpeed = speed;
			this.duration = duration;
			this.noisePercent = Mathf.Clamp01(noisePercent);
			this.dampingPercent = Mathf.Clamp01(dampingPercent);
			this.rotationPercent = Mathf.Clamp01(rotationPercent);
		}
		

	}
}