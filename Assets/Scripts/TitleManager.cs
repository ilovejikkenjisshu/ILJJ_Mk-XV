using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public Button m_StartButton;

    void Start()
    {
        Button startButton = m_StartButton.GetComponent<Button>();

        startButton.onClick.AddListener(StartButtonOnClick);
    }

    void StartButtonOnClick()
    {
        SceneManager.LoadScene("SimpleStageScene");
    }
}
