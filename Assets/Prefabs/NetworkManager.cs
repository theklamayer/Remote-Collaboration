using Fusion;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public NetworkRunner runnerPrefab;
    public GameObject playerPrefab;

    private void Start()
    {
        StartGame();
    }

    async void StartGame()
    {
        var runner = Instantiate(runnerPrefab);
        await runner.StartGame(new StartGameArgs()
        {
            GameMode = GameMode.AutoHostOrClient,
            SessionName = "TestRoom",
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });

        // Spieler-Avatar spawnen
        runner.Spawn(playerPrefab, Vector3.zero, Quaternion.identity, runner.LocalPlayer);
    }
}


