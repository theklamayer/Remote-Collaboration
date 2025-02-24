using Fusion;
using Unity.XR.CoreUtils;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    private Transform xrRig; // Dein XR Origin (Kamera-Rig)

    [Networked] private Vector3 NetworkedPosition { get; set; }
    [Networked] private Quaternion NetworkedRotation { get; set; }

    void Start()
    {
        if (HasStateAuthority) // Nur der lokale Spieler setzt sein XR Rig
        {
            Debug.Log("XR Rig wird mit dem PlayerPrefab verknüpft...");

            XROrigin xrOrigin = FindObjectOfType<XROrigin>();
            if (xrOrigin != null)
            {
                xrRig = xrOrigin.transform;
            }
            else
            {
                Debug.LogError("Kein XR Origin gefunden! Stelle sicher, dass ein XR Rig in der Szene existiert.");
            }
        }
    }

    void Update()
    {
        if (HasStateAuthority) // Nur der eigene Spieler sendet Positionsupdates
        {
            if (xrRig != null)
            {
                NetworkedPosition = xrRig.position;
                NetworkedRotation = xrRig.rotation;
            }
        }
        else // Andere Clients empfangen die synchronisierte Position
        {
            transform.position = NetworkedPosition;
            transform.rotation = NetworkedRotation;
        }
    }
}
