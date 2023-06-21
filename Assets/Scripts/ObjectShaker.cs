using UnityEngine;

public class ObjectShaker : MonoBehaviour
{
    public float shakeDuration = 0.5f; // Duration of the shake animation
    public float shakeIntensity = 0.1f; // Intensity of the shake animation

    private Vector3 originalPosition;
    private Transform objectTransform;

    public void ShakeObject(GameObject objectToShake)
    {
        originalPosition = objectToShake.transform.position;
        objectTransform = objectToShake.transform;

        StartCoroutine(ShakeCoroutine());
    }

    private System.Collections.IEnumerator ShakeCoroutine()
    {
        float timer = 0f;

        while (timer < shakeDuration)
        {
            timer += Time.deltaTime;

            Vector3 randomOffset = Random.insideUnitSphere * shakeIntensity;
            randomOffset.y = 0f; // Prevent vertical movement
            objectTransform.position = originalPosition + randomOffset;

            yield return null;
        }

        objectTransform.position = originalPosition;
    }
}
