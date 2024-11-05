using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Banana : MonoBehaviour
    {
        public GameObject textObject;
        public float displayTime = 3f;

        private bool displayingText = false;
        private float displayTimer = 0f;

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !displayingText) // ���������, ��� �������������� � ������� � ����� �� ������������
            {
                textObject.SetActive(true); // �������� ������ � �������
                displayingText = true; // ������������� ���� ����������� ������
                displayTimer = Time.time + displayTime; // ������������� �����, ����� ����� ������ ���������
            }
        }

        void Update()
        {
            if (displayingText && Time.time >= displayTimer) // ���������, ���� ����� ������������ � ����� ����������� ������
            {
                textObject.SetActive(false); // ��������� ������ � �������
                displayingText = false; // ���������� ���� ����������� ������
            }
        }
    }