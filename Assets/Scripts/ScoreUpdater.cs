using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreUpdater : MonoBehaviour
{
    [SerializeField] private Text _text;

    void Start()
    {
        _text.text = GameManager.Score.ToString(); 
    }
}
