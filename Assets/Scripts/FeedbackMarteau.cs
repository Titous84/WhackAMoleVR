using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

[RequireComponent(typeof(XRGrabInteractable))]
public class FeedbackMarteau : MonoBehaviour
{
    [Header("Haptique - Grab")]
    [SerializeField] private float amplitudeGrab = 0.4f;
    [SerializeField] private float dureeGrab = 0.1f;

    // Référence au contrôleur qui tient actuellement le marteau.
    // Elle est réutilisée lors de l'impact pour déclencher une vibration.
    public XRBaseController controleurActif { get; private set; }

    private XRGrabInteractable grabInteractable;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(OnGrabEntered);
        grabInteractable.selectExited.AddListener(OnGrabExited);
    }

    void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrabEntered);
        grabInteractable.selectExited.RemoveListener(OnGrabExited);
    }

    private void OnGrabEntered(SelectEnterEventArgs args)
    {
        // Récupère et stocke le contrôleur actif (pour le réutiliser à l'impact).
        controleurActif = args.interactorObject.transform.GetComponent<XRBaseController>();

        if (controleurActif != null)
        {
            controleurActif.SendHapticImpulse(amplitudeGrab, dureeGrab);
        }
    }

    private void OnGrabExited(SelectExitEventArgs args)
    {
        controleurActif = null;
    }

    // Appelée depuis Cible.cs pour envoyer la vibration d'impact.
    public void EnvoyerVibrationImpact(float amplitude, float duree)
    {
        if (controleurActif != null)
        {
            controleurActif.SendHapticImpulse(amplitude, duree);
        }
    }
}
