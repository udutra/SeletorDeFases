using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Level")]
public class Level : ScriptableObject {

    [SerializeField] private string sceneName;                              // nome da cena
    [SerializeField] private string levelName;                              // nome da fase
    [SerializeField] [TextArea(10, 15)] private string levelDescription;    // descri��o da fase
    [SerializeField] private bool startUnlocked;                            // saber se a fase deve iniciar liberada ou n�o
    [SerializeField] private Sprite levelLockedImage;                       // efer�ncias das imagens n�o liberada de destaque da fase
    [SerializeField] private Sprite levelUnlockedImage;                     // efer�ncias das imagens liberada de destaque da fase

    public string SceneName { get { return sceneName; } }
    public string LevelName { get { return levelName; } }
    public string LevelDescription { get { return levelDescription; } }
    public Sprite LevelLockedImage { get { return levelLockedImage; } }
    public Sprite LevelUnlockedImage { get { return levelUnlockedImage; } }
    public bool IsUnlocked { get; set; }

    // chamado automaticamente pela engine quando esse objeto � criado durante o jogo
    private void OnEnable() {
        IsUnlocked = startUnlocked;
    }
}