using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public enum EtatJeu { Menu, EnJeu, GameOver }

    [Header("Canvas")]
    [SerializeField] private GameObject canvasMenu;
    [SerializeField] private GameObject canvasHUD;
    [SerializeField] private GameObject canvasGameOver;

    [Header("Textes UI")]
    [SerializeField] private TextMeshProUGUI texteScore;
    [SerializeField] private TextMeshProUGUI texteTimer;
    [SerializeField] private TextMeshProUGUI texteScoreFinal;

    [Header("Références")]
    [SerializeField] private SpawnManager spawnManager;

    [Header("Paramètres de jeu")]
    [SerializeField] private float dureeTotale = 60f;
    [SerializeField] private int pointsParCible = 10;

    private EtatJeu etatActuel;
    private float tempsRestant;
    private int score;
    private bool timerActif;

    void Start()
    {
        ChangerEtat(EtatJeu.Menu);
    }

    // Gère le compte à rebours du timer et termine le jeu lorsque le temps est écoulé.
    void Update()
    {
        if (!timerActif) return;

        tempsRestant -= Time.deltaTime;
        AfficherTimer();

        if (tempsRestant <= 0f)
        {
            tempsRestant = 0f;
            TerminerJeu();
        }
    }

    // Gère la transition entre les états du jeu et l'affichage des canvas correspondants.
    private void ChangerEtat(EtatJeu nouvelEtat)
    {
        etatActuel = nouvelEtat;
        canvasMenu.SetActive(etatActuel == EtatJeu.Menu);
        canvasHUD.SetActive(etatActuel == EtatJeu.EnJeu);
        canvasGameOver.SetActive(etatActuel == EtatJeu.GameOver);
    }

    // Appelée par le bouton "Jouer".
    public void CommencerJeu()
    {
        score = 0;
        tempsRestant = dureeTotale;
        timerActif = true;
        AfficherScore();
        AfficherTimer();
        ChangerEtat(EtatJeu.EnJeu);
        spawnManager.DemarrerSpawn();
    }

    // Appelée depuis Cible.cs quand une cible est frappée.
    public void AjouterPoint()
    {
        if (etatActuel != EtatJeu.EnJeu) return;
        score += pointsParCible;
        AfficherScore();
    }

    // Appelée quand le timer atteint zéro.
    private void TerminerJeu()
    {
        timerActif = false;
        spawnManager.ArreterSpawn();
        texteScoreFinal.text = "Score final : " + score;
        ChangerEtat(EtatJeu.GameOver);
    }

    // Appelée par le bouton "Rejouer".
    public void Rejouer()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }

    // Affiche le score actuel.
    private void AfficherScore()
    {
        texteScore.text = "Score : " + score;
    }

    // Affiche le temps restant arrondi à l'entier supérieur.
    private void AfficherTimer()
    {
        texteTimer.text = "Temps restant : " + Mathf.CeilToInt(tempsRestant).ToString() + "s";
    }
}
