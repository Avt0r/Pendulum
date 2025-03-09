using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField]
    private PendulumEnd _pendulum;

    void Update()
    {
        if(Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            _pendulum.LetGo();
        }

    }
}
