using UnityEngine;
using UnityEngine.UI;
using TMPro; // Importamos TextMeshPro

public class TicTacToeGame : MonoBehaviour
{
    public Button[] buttons; // Array de botones (9 botones)
    public TMP_Text statusText; // Texto para mostrar mensajes
    private string[] board = new string[9]; // Estado del tablero
    private string currentPlayer = "X"; // Jugador actual ("X" o "O")

    void Start()
    {
        ResetGame(); // Inicia el juego en cada ejecución
    }

    public void OnButtonClick(int index)
    {
        if (board[index] == "") // Verifica si la casilla está vacía
        {
            board[index] = currentPlayer;

            // Busca el componente TMP_Text dentro del botón
            TMP_Text buttonText = buttons[index].GetComponentInChildren<TMP_Text>();
            if (buttonText != null)
            {
                buttonText.text = currentPlayer; // Coloca "X" o "O"
            }
            else
            {
                Debug.LogError("Error: El botón no tiene un componente TMP_Text.");
            }

            // Verificar si hay ganador
            if (CheckWin())
            {
                statusText.text = "Ganador: " + currentPlayer;
                DisableButtons();
                return;
            }

            // Verificar si hay empate
            if (CheckTie())
            {
                statusText.text = "¡Empate!";
                DisableButtons();
                return;
            }

            // Cambiar de jugador
            SwitchPlayer();
        }
    }

    void SwitchPlayer()
    {
        currentPlayer = (currentPlayer == "X") ? "O" : "X";
        statusText.text = "Turno: " + currentPlayer;
    }

    bool CheckWin()
    {
        int[,] winPatterns = new int[,]
        {
            {0, 1, 2}, {3, 4, 5}, {6, 7, 8}, // Filas
            {0, 3, 6}, {1, 4, 7}, {2, 5, 8}, // Columnas
            {0, 4, 8}, {2, 4, 6}  // Diagonales
        };

        for (int i = 0; i < winPatterns.GetLength(0); i++)
        {
            if (board[winPatterns[i, 0]] != "" &&
                board[winPatterns[i, 0]] == board[winPatterns[i, 1]] &&
                board[winPatterns[i, 1]] == board[winPatterns[i, 2]])
            {
                return true;
            }
        }
        return false;
    }

    bool CheckTie()
    {
        foreach (string cell in board)
        {
            if (cell == "") return false;
        }
        return true;
    }

    void DisableButtons()
    {
        foreach (Button button in buttons)
        {
            button.interactable = false;
        }
    }

    public void ResetGame()
    {
        currentPlayer = "X";
        statusText.text = "Turno: X";

        for (int i = 0; i < board.Length; i++)
        {
            board[i] = "";
            TMP_Text buttonText = buttons[i].GetComponentInChildren<TMP_Text>();
            if (buttonText != null) buttonText.text = "";
            buttons[i].interactable = true;
        }
    }
}