using SharedLibrary;
using UnityEngine;

public class GameFlowManager : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {
        //var player = new Player();
        var player = await HttpClient.Get<Player>("https://localhost:7288/player/200");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
