using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GunScript : MonoBehaviour
{
    [Header("Grab Interactable")]
    [SerializeField] private XRGrabInteractable grabInteractable;

    [Header("Raycasting Settings")]
    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private LayerMask targetLayer;

    [Header("Visual Effects")]
    [SerializeField] private GameObject hitGraphic;

    private void Awake()
    {
        if (grabInteractable == null)
        {
            grabInteractable = GetComponent<XRGrabInteractable>();
        }
    }

    private void OnEnable()
    {
        if (grabInteractable != null)
        {
            grabInteractable.activated.AddListener(TriggerPulled);
        }
    }

    private void OnDisable()
    {
        if (grabInteractable != null)
        {
            grabInteractable.activated.RemoveListener(TriggerPulled);
        }
    }

    private void TriggerPulled(ActivateEventArgs args)
    {
        HandleHapticFeedback(args);
        FireRaycastIntoScene();
    }

    private void HandleHapticFeedback(ActivateEventArgs args)
    {
        if (args.interactorObject == null) return;

        // Безопасное получение компонента контроллера
        if (args.interactorObject.transform.TryGetComponent(out XRBaseController controller))
        {
            controller.SendHapticImpulse(0.5f, 0.25f);
        }
    }

    private void FireRaycastIntoScene()
    {
        if (raycastOrigin == null)
        {
            Debug.LogError("Raycast Origin not assigned!");
            return;
        }

        Ray ray = new Ray(raycastOrigin.position, raycastOrigin.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, targetLayer))
        {
            HandleTargetHit(hit);
        }
    }

    private void HandleTargetHit(RaycastHit hit)
    {
        if (hit.transform.TryGetComponent(out ITargetInterface target))
        {
            target.TargetShot(hit.point);
            //CreateHitGraphicOnTarget(hit.point);
            return;
        }

        if (hit.transform.TryGetComponent(out GameStartUIController uiController))
        {
            uiController.TargetHit();
        }
    }

    private void CreateHitGraphicOnTarget(Vector3 position)
    {
        if (hitGraphic != null)
        {
            Instantiate(hitGraphic, position, Quaternion.identity);
        }
    }
}