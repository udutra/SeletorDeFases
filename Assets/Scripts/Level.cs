using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Level")]
public class Level : ScriptableObject {

    [SerializeField] private string sceneName;                              // nome da cena
    [SerializeField] private string levelName;                              // nome da fase
    [SerializeField] [TextArea(10, 15)] private string levelDescription;    // descrição da fase
    [SerializeField] private bool startUnlocked;                            // saber se a fase deve iniciar liberada ou não
    [SerializeField] private Sprite levelLockedImage;                       // eferências das imagens não liberada de destaque da fase
    [SerializeField] private Sprite levelUnlockedImage;                     // eferências das imagens liberada de destaque da fase

    public string SceneName { get { return sceneName; } }
    public string LevelName { get { return levelName; } }
    public string LevelDescription { get { return levelDescription; } }
    public Sprite LevelLockedImage { get { return levelLockedImage; } }
    public Sprite LevelUnlockedImage { get { return levelUnlockedImage; } }
    public bool IsUnlocked { get; set; }

    // chamado automaticamente pela engine quando esse objeto é criado durante o jogo
    private void OnEnable() {
        IsUnlocked = startUnlocked;
    }
}