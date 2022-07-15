using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {

    private int currentPage = 0;                                // guardar a p�gina atual que deve ser exibida.
    private Level selectedLevel;                                // guardar level selecionado
    [SerializeField] private Level[] levels;                    // array com todas as fases que queremos que sejam exibidas na interface.

    [Header("Level cards")]
    [SerializeField] private GameObject levelCardPrefab;        // refer�ncia ao Prefab do cart�o da fase
    [SerializeField] private RectTransform levelCardsContainer; // refer�ncia ao RectTransform usado como pai dos cart�es quando criarmos, assim podemos posicionar os cart�es onde queremos
    [SerializeField] private ToggleGroup levelCardToggleGroup;  // refer�ncia ao componente Toggle Group para agrupar cart�es

    [Header("Pagination")]
    [SerializeField] private int levelsPerPage;                 // respons�vel por guardar a quantidade de fases que temos por p�gina
    [SerializeField] private Button nextPageButton;             // refer�ncia ao bot�o para avan�ar a pagina��o
    [SerializeField] private Button previousPageButton;         // refer�ncia ao bot�o para voltar a pagina��o
    


    // chama os m�todos CreateLevelCards() e RefreshPageContent() quando o objeto � criado
    private void Awake() {
        CreateLevelCards();
        RefreshPageContent();
    }

    // respons�vel por criar o cart�o e passar as informa��es da fase que ele deve exibir
    private void CreateLevelCards() {
        foreach (Level level in levels) {
            GameObject levelCardGO = Instantiate(levelCardPrefab, levelCardsContainer);
            LevelCard levelCard = levelCardGO.GetComponent<LevelCard>();
            levelCard.SetCardContent(level);
            levelCard.SetToggleGroup(levelCardToggleGroup);
            levelCard.Toggle.onValueChanged.AddListener(isOn => LevelSelected(isOn, levelCard));
        }
    }

    // respons�vel por esconder ou exibir o cart�o de uma fase dependendo da p�gina atual
    private void RefreshPageContent() {
        int startIndex = currentPage * levelsPerPage;
        int endIndex = startIndex + levelsPerPage;
        for (int i = 0; i < levels.Length; i++) {
            if (i >= startIndex && i < endIndex) {
                levelCardsContainer.GetChild(i).gameObject.SetActive(true);
            }
            else {
                levelCardsContainer.GetChild(i).gameObject.SetActive(false);
            }
        }
        CheckPaginationButtons();
    }

    // respons�vel por bloquear os bot�es da pagina��o para que o jogador n�o possa clicar neles e quebrar a pagina��o
    private void CheckPaginationButtons() {
        int totalPages = levels.Length / levelsPerPage;
        nextPageButton.interactable = currentPage != totalPages;
        previousPageButton.interactable = currentPage != 0;
    }

    // respons�vel por incrementar o currentPage e chamar o m�todo RefreshPageContent para atualizar os cart�es das fases
    public void NextPage() {
        currentPage++;
        RefreshPageContent();
    }

    // respons�vel por decrementar o currentPage e chamar o m�todo RefreshPageContent para atualizar os cart�es das fases
    public void PreviousPage() {
        currentPage--;
        RefreshPageContent();
    }

    // checar o valor de isOn, caso seja false iremos ignorar e pararemos o m�todo aqui mesmo, pois isso significa que a fase foi desselecionada.
    // Caso seja true iremos salvar a informa��o da fase selecionada no campo selectedLevel.
    private void LevelSelected(bool isOn, LevelCard levelCard) {
        if (isOn == false) {
            return;
        }
        selectedLevel = levelCard.LevelInfo;
    }

    public void StartSelectedLevel() {
        SceneManager.LoadScene(selectedLevel.SceneName);
    }
}