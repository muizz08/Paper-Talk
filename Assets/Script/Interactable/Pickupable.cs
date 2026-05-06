using UnityEngine;

namespace PaperTalk.Interactable
{
    // Class ini mewarisi Interactable
    public class Pickupable : Interactable
    {
        protected override void Awake()
        {
            base.Awake(); // WAJIB ada agar rb di class induk terisi
                          // Kode lainnya...
        }
        protected override void Interact()
        {
            Debug.Log("Mengambil: " + gameObject.name);
        }
    }
}