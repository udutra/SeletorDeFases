using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {

    private int currentPage = 0;                                // guardar a página atual que deve ser exibida.
    private Level selectedLevel;                                // guardar level selecionado
    [SerializeField] private Level[] levels;                    // array com todas as fases que queremos que sejam exibidas na interface.

    [Header("Level cards")]
    [SerializeField] private GameObject levelCardPrefab;        // referência ao Prefab do cartão da fase
    [SerializeField] private RectTransform levelCardsContainer; // referência ao RectTransform usado como pai dos cartões quando criarmos, assim podemos posicionar os cartões onde queremos
    [SerializeField] private ToggleGroup levelCardToggleGroup;  // referência ao componente Toggle Group para agrupar cartões

    [Header("Pagination")]
    [SerializeField] private int levelsPerPage;                 // responsável por guardar a quantidade de fases que temos por página
    [SerializeField] private Button nextPageButton;             // referência ao botão para avançar a paginação
    [SerializeField] private Button previousPageButton;         // referência ao botão para voltar a paginação
    


    // chama os métodos CreateLevelCards() e RefreshPageContent() quando o objeto é criado
    private void Awake() {
        CreateLevelCards();
        RefreshPageContent();
    }

    // responsável por criar o cartão e passar as informações da fase que ele deve exibir
    private void CreateLevelCards() {
        foreach (Level level in levels) {
            GameObject levelCardGO = Instantiate(levelCardPrefab, levelCardsContainer);
            LevelCard levelCard = levelCardGO.GetComponent<LevelCard>();
            levelCard.SetCardContent(level);
            levelCard.SetToggleGroup(levelCardToggleGroup);
            levelCard.Toggle.onValueChanged.AddListener(isOn => LevelSelected(isOn, levelCard));
        }
    }

    // responsável por esconder ou exibir o cartão de uma fase dependendo da página atual
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

    // responsável por bloquear os botões da paginação para que o jogador não possa clicar neles e quebrar a paginação
    private void CheckPaginationButtons() {
        int totalPages = levels.Length / levelsPerPage;
        nextPageButton.interactable = currentPage != totalPages;
        previousPageButton.interactable = currentPage != 0;
    }

    // responsável por incrementar o currentPage e chamar o método RefreshPageContent para atualizar os cartões das fases
    public void NextPage() {
        currentPage++;
        RefreshPageContent();
    }

    // responsável por decrementar o currentPage e chamar o método RefreshPageContent para atualizar os cartões das fases
    public void PreviousPage() {
        currentPage--;
        RefreshPageContent();
    }

    // checar o valor de isOn, caso seja false iremos ignorar e pararemos o método aqui mesmo, pois isso significa que a fase foi desselecionada.
    // Caso seja true iremos salvar a informação da fase selecionada no campo selectedLevel.
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