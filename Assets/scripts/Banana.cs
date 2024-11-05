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
            if (other.CompareTag("Player") && !displayingText) // Проверяем, что соприкоснулись с игроком и текст не отображается
            {
                textObject.SetActive(true); // Включаем объект с текстом
                displayingText = true; // Устанавливаем флаг отображения текста
                displayTimer = Time.time + displayTime; // Устанавливаем время, когда текст должен исчезнуть
            }
        }

        void Update()
        {
            if (displayingText && Time.time >= displayTimer) // Проверяем, если текст отображается и время отображения прошло
            {
                textObject.SetActive(false); // Выключаем объект с текстом
                displayingText = false; // Сбрасываем флаг отображения текста
            }
        }
    }