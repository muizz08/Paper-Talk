using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace PaperTalk.CanvasManager
{
    using PaperTalk.Player;
    public class CanvasManager : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _interactText;

        public void SetInteractText(string text)
        {
            _interactText.text = text;
        }
    }
}