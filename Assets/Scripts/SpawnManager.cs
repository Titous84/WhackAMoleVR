// SpawnManager.cs
// Attaché au GameObject SpawnManager.
// Crée dynamiquement des cibles à des positions aléatoires parmi les points de spawn.

using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Prefab à instancier")]
    [SerializeField] private GameObject prefabCible;

    [Header("Points de spawn (GameObjects vides sur la table)")]
    [SerializeField] private Transform[] pointsDeSpawn;

    [Header("Fréquence de spawn")]
    [SerializeField] private float intervalle = 1.5f;

    [Header("Références")]
    [SerializeField] private GameManager gameManager;

    private bool actif = false;

    // Appelée par le GameManager quand la partie commence.
    public void DemarrerSpawn()
    {
        actif = true;
        InvokeRepeating(nameof(SpawnerUneCible), 0f, intervalle);
    }

    // Appelée par le GameManager quand la partie se termine.
    public void ArreterSpawn()
    {
        actif = false;
        CancelInvoke(nameof(SpawnerUneCible));

        // Détruit toutes les cibles encore présentes.
        foreach (Cible c in FindObjectsByType<Cible>(FindObjectsSortMode.None))
        {
            Destroy(c.gameObject);
        }
    }

    private void SpawnerUneCible()
    {
        if (!actif || pointsDeSpawn.Length == 0) return;

        // Choisit un point de spawn aléatoire.
        int index = Random.Range(0, pointsDeSpawn.Length);
        Transform pointChoisi = pointsDeSpawn[index];

        // Instancie la cible.
        GameObject nouvelleCible = Instantiate(prefabCible, pointChoisi.position, Quaternion.identity);

        // Donne la référence au GameManager à la cible.
        Cible scriptCible = nouvelleCible.GetComponent<Cible>();
        if (scriptCible != null)
        {
            scriptCible.Initialiser(gameManager);
        }
    }
}
