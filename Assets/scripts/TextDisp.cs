using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDisp : MonoBehaviour

    {


public GameObject textObject;
public float delayTime = 2f;
public float displayTime =2f;

void Start()
{
    StartCoroutine(DisplayText());
}

IEnumerator DisplayText()
{
    yield return new WaitForSeconds(delayTime);

    textObject.SetActive(true);

    yield return new WaitForSeconds(displayTime);

    textObject.SetActive(false);
}
}
