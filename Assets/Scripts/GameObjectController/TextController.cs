﻿using UnityEngine;
using UnityEngine.UI;

namespace GameObjectController
{
    public class TextController : MonoBehaviour
    {
        [SerializeField] private Text text;

        public void SetText(string text)
        {
            this.text.text = text;
        }

        public void SetColor(Color color)
        {
            text.color = color;
        }

        public void SetTextWithColor(string text, Color color)
        {
            SetText(text);
            SetColor(color);
        }
    }
}