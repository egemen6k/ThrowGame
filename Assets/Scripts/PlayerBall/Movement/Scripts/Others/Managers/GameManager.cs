using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    GameObject _player;
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        if (_player == null)
        {
            Debug.LogError("Player is null");
        }

    }

    public void NewGame()
    {
        SceneManager.LoadScene(0);
    }

    public void NewBall()
    {
        _player.SetActive(false);
        _player.SetActive(true);
    }
}
