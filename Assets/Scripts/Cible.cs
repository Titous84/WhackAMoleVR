using UnityEngine;

public class Cible : MonoBehaviour
{
    [Header("Haptique - Impact")]
    [SerializeField] private float amplitudeImpact = 1.0f;
    [SerializeField] private float dureeImpact = 0.2f;

    [Header("Délai avant disparition automatique")]
    [SerializeField] private float delaiDisparition = 3f;

    [Header("Audio - Impact")]
    [SerializeField] private AudioClip sonImpact;

    // Référence au GameManager, assignée par SpawnManager après l'Instantiate.
    private GameManager gameManager;
    private bool dejaTouchee;

    void Start()
    {
        // Disparaît automatiquement si non frappée.
        Invoke(nameof(DisparaitreSansPts), delaiDisparition);
    }

    // Appelée par SpawnManager juste après Instantiate.
    public void Initialiser(GameManager gm)
    {
        gameManager = gm;
    }

    // Détection du coup par contact physique avec le marteau.
    void OnCollisionEnter(Collision collision)
    {
        TraiterImpact(collision.collider);
    }

    // Variante trigger : utile si la cible ou le marteau utilise un collider "Is Trigger".
    void OnTriggerEnter(Collider other)
    {
        TraiterImpact(other);
    }

    private void TraiterImpact(Collider colliderTouche)
    {
        if (dejaTouchee) return;

        // Récupère le script du marteau même si le collider touché est un enfant.
        FeedbackMarteau marteau = colliderTouche.GetComponentInParent<FeedbackMarteau>();
        if (marteau == null) return;

        dejaTouchee = true;

        // Envoie la vibration d'impact (distincte de la vibration de grab).
        marteau.EnvoyerVibrationImpact(amplitudeImpact, dureeImpact);

        // Joue le son d'impact à la position de la cible.
        if (sonImpact != null)
        {
            AudioSource.PlayClipAtPoint(sonImpact, transform.position);
        }

        // Notifie le GameManager pour ajouter les points.
        if (gameManager != null)
        {
            gameManager.AjouterPoint();
        }

        // Annule la disparition automatique puis détruit la cible.
        CancelInvoke(nameof(DisparaitreSansPts));
        Destroy(gameObject);
    }

    private void DisparaitreSansPts()
    {
        Destroy(gameObject);
    }
}