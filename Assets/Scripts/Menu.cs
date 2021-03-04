using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

//  вся логика UI 
public class Menu : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI pointsCountText;
    [SerializeField]
    TextMeshProUGUI GameOverScore;
    [SerializeField]
    List<GameObject> lifesIMG;
    [SerializeField]
    GameObject GameOverPanel;


    public void StartGame()
    {
        GameManager.Instance.StartGame();
    }

    public void PointUpdate(string points)
    {
        pointsCountText.text = points;
    }
    public void LifesUpdate(int lifes)
    {
        for (int i = 0; i < lifesIMG.Count; i++)
        {
            lifesIMG[i].SetActive(i < lifes - 1 ? true : false);
        }
    }
    public void GameOver(int score)
    {
        GameOverScore.text = score.ToString();
        GameOverPanel.SetActive(true);
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }



}
