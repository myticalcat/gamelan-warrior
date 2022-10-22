using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game.Cutscene.UI {
    public class DialogBox : MonoBehaviour {
        [SerializeField] private float _charWriteDuration = 0.1f;
        [SerializeField] private TMP_Text _text;
        private Image _image;
        private bool _waitingForInput = false;
        
        public delegate void InputHandler();
        public event InputHandler OnInput;
        
        private void Awake() {
            _image = transform.GetComponent<Image>();
        }
        
        private void Update() {
            if (_waitingForInput) {
                if (Input.GetMouseButtonUp(0)) {
                    if (OnInput != null)
                        OnInput();
                }
            }
        }
        
        public void SetText(string text) {
            _text.gameObject.SetActive(true);
            
            Color newColor = _image.color;
            newColor.a = 1f;
            _image.color = newColor;
            
            StartCoroutine(WriteText(text));
        }
        
        private IEnumerator WriteText(string text) {
            _waitingForInput = false;
            string currentText = "";
            for (int i = 0; i < text.Length; i++) {
                currentText += text[i];
                _text.text = currentText;
                yield return new WaitForSeconds(_charWriteDuration);
            }
            _waitingForInput = true;
        }
        
        public void Close() {
            _text.text = "";
            _text.gameObject.SetActive(false);
            
            Color newColor = _image.color;
            newColor.a = 0f;
            _image.color = newColor;
        }
    }
}


