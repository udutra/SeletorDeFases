using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class LevelCard : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI levelName;         // guardar uma referência ao GameObject com o componente de texto em que será usado para exibir o nome
    [SerializeField] private TextMeshProUGUI levelDescription;  // guardar uma referência ao GameObject com o componente de texto em que será usado para exibir a descrição da fase
    [SerializeField] private Image levelImage;                  // guardar uma referência ao componente Image, em que será exibido a imagem de capa da fase
    public Level LevelInfo { get; private set; }
    public Toggle Toggle { get; private set; }

    private void Awake() {
        Toggle = GetComponent<Toggle>();
    }

    // método para agrupar os cartões
    public void SetToggleGroup(ToggleGroup toggleGroup) {
        Toggle.group = toggleGroup;
    }

    //  recebe as informações de uma fase para atualizar as informações exibidas no cartão
    public void SetCardContent(Level level) {
        LevelInfo = level;
        levelName.SetText(level.LevelName);
        levelDescription.SetText(level.LevelDescription);
        if (level.IsUnlocked) {
            levelImage.sprite = level.LevelUnlockedImage;
        }
        else {
            levelImage.sprite = level.LevelLockedImage;
            Toggle.interactable = false;
        }
    }
}