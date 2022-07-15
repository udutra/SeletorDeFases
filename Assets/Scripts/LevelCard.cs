using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class LevelCard : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI levelName;         // guardar uma refer�ncia ao GameObject com o componente de texto em que ser� usado para exibir o nome
    [SerializeField] private TextMeshProUGUI levelDescription;  // guardar uma refer�ncia ao GameObject com o componente de texto em que ser� usado para exibir a descri��o da fase
    [SerializeField] private Image levelImage;                  // guardar uma refer�ncia ao componente Image, em que ser� exibido a imagem de capa da fase
    public Level LevelInfo { get; private set; }
    public Toggle Toggle { get; private set; }

    private void Awake() {
        Toggle = GetComponent<Toggle>();
    }

    // m�todo para agrupar os cart�es
    public void SetToggleGroup(ToggleGroup toggleGroup) {
        Toggle.group = toggleGroup;
    }

    //  recebe as informa��es de uma fase para atualizar as informa��es exibidas no cart�o
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