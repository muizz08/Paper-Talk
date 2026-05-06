using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PaperTalk.Player
{
    using PaperTalk.Interactable;
    using PaperTalk.CanvasManager;
    public class PlayerInteract : MonoBehaviour
    {
        [SerializeField] private Camera _cam;
        [SerializeField] private float _distance = 3f;
        [SerializeField] private LayerMask _mask;
        [SerializeField] private Transform _holdPosition;
        [SerializeField] private CanvasManager _canvasManager;
        private Interactable _currentTarget; // Untuk deteksi umum
        private Pickupable _pickupable;     // Spesifik untuk benda yang sedang dibawa
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            _canvasManager.SetInteractText(string.Empty);

            if (_pickupable != null) return;
            //create a ray from the center of the camera, and check if it hits an interactable object
            Ray ray = new Ray(_cam.transform.position, _cam.transform.forward);
            Debug.DrawRay(ray.origin, ray.direction * _distance, Color.red);
            if (Physics.Raycast(ray, out RaycastHit hit, _distance, _mask))
            {
                if (hit.collider.GetComponent<Interactable>() != null)
                {
                    _canvasManager.SetInteractText(hit.collider.GetComponent<Interactable>()._promptMessage);
                }
            }
        }
         private void FixedUpdate()
        {
            if (_pickupable != null && _holdPosition != null)
            {
                // Gunakan MovePosition untuk stabilitas fisika
                _pickupable.rb.MovePosition(_holdPosition.position);
                _pickupable.rb.MoveRotation(_holdPosition.rotation);
            }
        }

        public void OnPickup()
        {
            // Jika sedang membawa benda, maka jatuhkan
            if (_pickupable != null)
            {
                Debug.Log("Sedang menarik " + _pickupable.name + " ke " + _holdPosition.position);
                Drop();
                return;
            }

            // Jika tangan kosong, cari objek dengan Raycast
            Ray ray = new Ray(_cam.transform.position, _cam.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, _distance, _mask))
            {
                if (hitInfo.collider.TryGetComponent<Interactable>(out Interactable interactable))
                {
                    interactable.baseInteract();

                    Pickupable p = hitInfo.collider.GetComponent<Pickupable>();
                    if (p != null)
                    {
                        Grab(p);
                    }
                    else
                    {
                        Debug.Log("Objek memiliki Interactable, tapi BUKAN Pickupable. Script yang terdeteksi: " + interactable.GetType());
                    }
                }
            }
        }

        private void Grab(Pickupable obj)
        {
            _pickupable = obj;

            // 1. Matikan fisika agar tidak goyang atau menabrak player
            _pickupable.rb.isKinematic = true;

            // 2. Jadikan benda sebagai 'anak' dari holdPosition
            _pickupable.transform.SetParent(_holdPosition);

            // 3. Reset posisi dan rotasi agar tepat di tengah holdPosition
            _pickupable.transform.localPosition = Vector3.zero;
            _pickupable.transform.localRotation = Quaternion.identity;

            // 4. Ubah layer agar tidak terkena Raycast sendiri
            _pickupable.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        }

        private void Drop()
        {
            // 1. Lepaskan dari parent
            _pickupable.transform.SetParent(null);

            // 2. Aktifkan kembali fisikanya
            _pickupable.rb.isKinematic = false;

            // 3. Beri sedikit dorongan agar tidak langsung jatuh ke kaki (opsional)
            _pickupable.rb.AddForce(_cam.transform.forward * 2f, ForceMode.Impulse);

            // 4. Kembalikan layer
            _pickupable.gameObject.layer = LayerMask.NameToLayer("Interactable");

            _pickupable = null;
        }

       
    }
}
