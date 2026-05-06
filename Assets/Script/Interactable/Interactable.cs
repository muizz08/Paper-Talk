using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PaperTalk.Interactable
{
    public abstract class Interactable : MonoBehaviour
    {
        public string _promptMessage;
        public Rigidbody rb { get; private set; }

        protected virtual void Awake()
        {
            rb = GetComponent<Rigidbody>();
            if (rb == null) Debug.LogError("Rigidbody tidak ditemukan di " + gameObject.name);
        }
        public void baseInteract()
        {
            Interact();
        }

        // Update is called once per frame
        protected virtual void Interact()
        {
            Debug.Log("Interacted with " + gameObject.name);
        }
    }
}

