using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class StartExperience : MonoBehaviour
{
    [SerializeField] private GameObject cube;

    public void OnStartExperience(ARPlane plane)
    {
        var scene = Instantiate(cube, plane.transform.position, Quaternion.identity);

        for (int i = 0; i < scene.transform.childCount; i++)
        {
            var child = scene.transform.GetChild(i).gameObject;
            StartCoroutine(ScaleUp(child, 0.5f));
        }
    }

    IEnumerator ScaleUp(GameObject obj, float duration)
    {
        Vector3 initialScale = Vector3.zero;
        Vector3 targetScale = obj.transform.localScale;

        float elapsedTime = 0f;

        obj.transform.localScale = initialScale;

        while (elapsedTime < duration)
        {
            obj.transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null; //a unity espera um frame pra continuar pode ser waitforseconds tambem
        }

    }
}
