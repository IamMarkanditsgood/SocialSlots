using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSizeManager : MonoBehaviour
{
    [SerializeField] private Transform sceneRoot; // головний контейнер сцени
    [SerializeField] private float baseAspect = 9f / 16f;

    private void Start()
    {
        float currentAspect = (float)Screen.width / Screen.height;
        float scale = currentAspect / baseAspect;

        sceneRoot.localScale = new Vector3(scale, sceneRoot.localScale.y, 1); // або X/Y разом
    }
}
