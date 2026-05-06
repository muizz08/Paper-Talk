using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PaperTalk.DialogueManager
{
    public class DialogueManager : MonoBehaviour
    {
        public TextMeshProUGUI npcTextDisplay;
        public Button[] choiceButtons;
        public int shopRating = 0;

        public void StartDialogue(DialogueData data)
        {
            npcTextDisplay.text = data.npcMessage;

            // Sembunyikan semua tombol dulu
            foreach (Button b in choiceButtons) b.gameObject.SetActive(false);

            // Tampilkan tombol sesuai jumlah pilihan di ScriptableObject
            for (int i = 0; i < data.choices.Length; i++)
            {
                if (i >= choiceButtons.Length) break;

                choiceButtons[i].gameObject.SetActive(true);
                choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = data.choices[i].choiceText;

                // Setup fungsi tombol
                int index = i; // Penting untuk closure
                DialogueData next = data.choices[i].nextDialogue;
                int impact = data.choices[i].ratingImpact;

                choiceButtons[i].onClick.RemoveAllListeners();
                choiceButtons[i].onClick.AddListener(() => SelectChoice(next, impact));
            }
        }

        void SelectChoice(DialogueData nextData, int ratingChange)
        {
            shopRating += ratingChange;
            Debug.Log("Rating Toko: " + shopRating);

            if (nextData != null)
            {
                StartDialogue(nextData);
            }
            else
            {
                EndDialogue();
            }
        }

        void EndDialogue()
        {
            Debug.Log("Percakapan Selesai.");
            // Tutup UI Panel Dialog kamu di sini
        }
    }
}
