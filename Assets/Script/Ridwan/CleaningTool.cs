using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PaperTalk.Interactable;

public class CleaningTool : MonoBehaviour
{
    [Header("Settings")]
    public string dirtTag = "Kotoran";
    public GameObject cleaningUI;

    [SerializeField] private GameObject _currentDirt; // Saya buat serialize agar bisa kamu pantau di Inspector
    private Pickupable _pickupable;

    void Start()
    {
        _pickupable = GetComponent<Pickupable>();
        if (cleaningUI == null) Debug.LogError("Cleaning UI belum dimasukkan ke slot Inspector!");
    }

    void Update()
    {
        // CEK 1: Apakah sapu sedang dipegang?
        // Kita pakai pengecekan 'transform.parent' karena script temanmu melakukan SetParent
        bool isHeld = transform.parent != null;

        if (isHeld && _currentDirt != null)
        {
            // Pastikan UI aktif
            if (!cleaningUI.activeSelf) cleaningUI.SetActive(true);

            // Input untuk bersihkan
            if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
            {
                Clean();
            }
        }
        else
        {
            // Jika tidak dekat kotoran atau tidak dipegang, matikan UI
            if (cleaningUI != null && cleaningUI.activeSelf)
            {
                cleaningUI.SetActive(false);
            }
        }
    }

    private void Clean()
    {
        Debug.Log("Sedang membersihkan: " + _currentDirt.name);
        Destroy(_currentDirt);
        _currentDirt = null; // Penting agar UI langsung hilang
    }

    // Gunakan Stay agar lebih aman jika Enter terlewat
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(dirtTag))
        {
            _currentDirt = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(dirtTag))
        {
            _currentDirt = null;
        }
    }
}