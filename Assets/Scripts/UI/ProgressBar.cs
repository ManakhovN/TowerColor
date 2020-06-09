using System;
using TMPro;
using UnityEngine;

namespace Src.UI.Scripts
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] 
        protected RectTransform bar;

        [SerializeField] 
        protected TextMeshProUGUI textMeshProUGUI;

        public virtual void Refresh()
        {
        }

        public virtual void RefreshView(float percentage, string text)
        {
            RefreshRectOnBar(bar, percentage);
            if (textMeshProUGUI != null)
            {
                textMeshProUGUI.text = text;
            }
        }

        protected virtual void RefreshRectOnBar(RectTransform rect, float percentage)
        {
            rect.localScale = Vector3.one;
            rect.sizeDelta = Vector2.zero;
            rect.anchoredPosition = Vector2.zero;
            rect.anchorMax = new Vector2(percentage, 1f);
            rect.sizeDelta = Vector2.zero;
        }
    }
}