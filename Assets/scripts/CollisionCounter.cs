using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCounter : MonoBehaviour
{
    public GameObject doska;
    public AudioSource zvon;
    private int questObjectCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("quest"))
        {
            questObjectCount++;
            Destroy(other.gameObject); // ���������� ������ � ����� quest

            if (questObjectCount == 5)
            {
                zvon.Play();
                doska.SetActive(true); // ���������� ������ � ����� doska
                StartCoroutine(HideDoskaAfterDelay(3f));
            }

   
        }
    }

    IEnumerator HideDoskaAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        doska.SetActive(false); // ������ ������ � ����� doska
    }
}