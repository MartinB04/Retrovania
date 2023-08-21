using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Hace referencia a los estados posibels del juego
public enum GameState
{
    menu, inGame, gameOver, pause, win,
};

public class GameManager : MonoBehaviour
{
    //Variable que hace referencia al mismo game manager
    public static GameManager sharedInstance;
    //Variable para saber estado del juego, al inicio esta en menu principal
    public GameState currentGameState = GameState.menu;

    public bool gameIsPaused = false;

    public Canvas canvasMenu, canvasGameOver, canvasInGame, canvasPause, canvasWin;
    private void Awake()
    {
        sharedInstance = this;
    }

    public void Start()
    {
            if (ChangeScene.sharedInstance.GetValue() == 0)
        BackToMenu();
            else
        StartGame();
        
    }

    private void Update()
    {
        //if (Input.GetButtonDown("Start") && this.currentGameState != GameState.inGame)
        //StartGame();
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }
    public void StartGame()
    {
        this.SetGameState(GameState.inGame);
        //resetea posicion de camara evitando barrido
        //CameraFollow.sharedInstance.ResetCameraPosition();
        //resetea posicion player
        PlayerController.sharedInstance.StartGame();

    }



    public void TogglePause()
    {
        gameIsPaused = !gameIsPaused;

        if (gameIsPaused)
        {
            Pause();
        }
        else
        {
            Resume();
        }
    }

    public void Pause()
    {
        gameIsPaused = true;
        Time.timeScale = 0f;
        this.SetGameState(GameState.pause);
    }

    public void Resume()
    {
        gameIsPaused = false;
        Time.timeScale = 1f;
        this.SetGameState(GameState.inGame);
    }

    public void GameOver()
    {
        this.SetGameState(GameState.gameOver);
    }

    public void Win()
    {
        this.SetGameState(GameState.win);
    }

    public void BackToMenu()
    {
        ChangeScene.sharedInstance.SaveData();
        //Debug.Log("Click backTomenu");
        if(PlayerController.sharedInstance.IsAlive() == false ||
            this.currentGameState == GameState.win)
        {
            //Debug.Log("Jugador muerto");
            //Debug.Log(ChangeScene.sharedInstance.GetValue());
            ChangeScene.sharedInstance.RefreshScene();
            //Debug.Log(ChangeScene.sharedInstance.GetValue());
        }
            
        this.SetGameState(GameState.menu);
    }

    //Funcion que cambia el estado del juego
    void SetGameState(GameState newGameState)
    {
        // sera necesario que solo sea en el lvl1
        if(newGameState == GameState.menu)
        {
            //Preparar codigo para volver al menu
            canvasMenu.enabled = true;
            canvasGameOver.enabled = false;
            canvasInGame.enabled = false;
            canvasPause.enabled = false;
            canvasWin.enabled = false;

        } else if(newGameState == GameState.gameOver)
        {
            //Preparar codigo para pantalla de gameover
            canvasMenu.enabled = false;
            canvasGameOver.enabled = true;
            canvasInGame.enabled = false;
            canvasPause.enabled = false;
            canvasWin.enabled = false;
        } else if(newGameState == GameState.inGame)
        {
            //Preparar codigo para estar en juego
            canvasMenu.enabled = false;
            canvasGameOver.enabled = false;
            canvasInGame.enabled = true;
            canvasPause.enabled = false;
            canvasWin.enabled = false;
        }
        else if (newGameState == GameState.pause)
        {
            //Preparar codigo para estar en juego
            canvasMenu.enabled = false;
            canvasGameOver.enabled = false;
            canvasInGame.enabled = false;
            canvasPause.enabled = true;
            canvasWin.enabled = false;
        }
        else if (newGameState == GameState.win)
        {
            //Preparar codigo para estar en juego
            canvasMenu.enabled = false;
            canvasGameOver.enabled = false;
            canvasInGame.enabled = false;
            canvasPause.enabled = false;
            canvasWin.enabled = true;
        }
        this.currentGameState = newGameState;
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
             Application.Quit();
        #endif
    }
}
