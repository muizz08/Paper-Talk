using UnityEngine;

[System.Serializable]
public class DialogueChoice
{
    public string choiceText;       // Teks yang muncul di tombol
    public int ratingImpact;        // Pengaruh ke rating (+10 atau -10)
    public DialogueData nextDialogue; // Dialog selanjutnya jika pilihan ini diklik
}

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue/DialogueData")]
public class DialogueData : ScriptableObject
{
    [TextArea(3, 10)]
    public string npcMessage;       // Apa yang NPC katakan
    public DialogueChoice[] choices; // Daftar pilihan untuk player
}