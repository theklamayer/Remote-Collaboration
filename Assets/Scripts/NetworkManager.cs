using Fusion;
using Fusion.Sockets;
using System;
using UnityEngine;

public class NetworkManager : MonoBehaviour, INetworkRunnerCallbacks
{
    public NetworkRunner runnerPrefab;
    public GameObject playerPrefab;

    private NetworkRunner runner;

    private void Start()
    {
        Debug.Log("NetworkManager gestartet");
        StartGame();
    }

    async void StartGame()
    {
        Debug.Log("Erstelle NetworkRunner...");
        runner = Instantiate(runnerPrefab);
        runner.AddCallbacks(this); // Registriere die Callbacks für Spieler-Beitritt usw.

        Debug.Log("Starte Session 'TestRoom'...");
        var result = await runner.StartGame(new StartGameArgs()
        {
            GameMode = GameMode.AutoHostOrClient,
            SessionName = "TestRoom",
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });

        if (result.Ok)
        {
            Debug.Log("Erfolgreich der Session beigetreten: TestRoom");
        }
        else
        {
            Debug.LogError($"Fehler beim Beitreten der Session: {result.ShutdownReason}");
        }
    }

    // Wird aufgerufen, wenn ein Spieler beitritt
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log("1");
        Debug.Log($"Spieler {player.PlayerId} ist beigetreten");
        Debug.Log("2");

        if(runner == null){ Debug.Log("Runner ist null"); }
        Debug.Log("3");
        Debug.Log($"Runner Mode: {runner.GameMode}");
        Debug.Log("4");

        // Spawne den Spieler für alle Clients
        if (runner.IsServer)
        {
            if (playerPrefab == null)
            {
                Debug.LogError("Fehler: playerPrefab ist null!");
                return;
            }
            
            Vector3 spawnPosition = new Vector3(UnityEngine.Random.Range(-2, 2), 1, UnityEngine.Random.Range(-2, 2));
            Debug.Log($"Versuche, Spieler {player.PlayerId} zu spawnen mit Prefab: {playerPrefab.name}");
            var spawnedPlayer = runner.Spawn(playerPrefab, spawnPosition, Quaternion.identity, player);

            if (spawnedPlayer != null)
            {
                Debug.Log($"Spieler {player.PlayerId} erfolgreich gespawnt!");
            }
            else
            {
                Debug.LogError($"Spieler {player.PlayerId} konnte nicht gespawnt werden!");
            }
        }

    }

    // Fusion Callbacks (können ignoriert werden, falls nicht benötigt)
    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data) { }
    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { }
    public void OnConnectedToServer(NetworkRunner runner) { Debug.Log("Verbindung zum Server hergestellt"); }
    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason) { Debug.Log($"Verbindung zum Server verloren: {reason}"); }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { Debug.Log($"Fusion beendet: {shutdownReason}"); }
    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { Debug.Log($"Spieler {player.PlayerId} hat das Spiel verlassen"); }
    public void OnInput(NetworkRunner runner, NetworkInput input) { }
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    public void OnSessionListUpdated(NetworkRunner runner, System.Collections.Generic.List<SessionInfo> sessionList) { }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, System.Collections.Generic.Dictionary<string, object> data) { }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, System.ArraySegment<byte> data) { }
    public void OnSceneLoadDone(NetworkRunner runner) { }
    public void OnSceneLoadStart(NetworkRunner runner) { }


    
}
