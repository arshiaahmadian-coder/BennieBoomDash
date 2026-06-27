using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Canvas gameUI;
    [SerializeField] private Canvas winDialog;
    [SerializeField] private Canvas loseDialog;

    [SerializeField] private TMP_Text[] winTexts;
    [SerializeField] private TMP_Text[] loseTexts;

    [Header("sound")]
    [SerializeField] private AudioSource gameMusicSource;
    [SerializeField] private AudioClip winClip;
    [SerializeField] private AudioClip loseClip;

    public static GameManager instance;

    private bool gameEnded;

    private Timer timer;
    private CoinNumber coinNumber;
    private PlayerMovement player;
    private RocketSpawnController rocketSpawner;
    private Joystick joystick;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        timer = FindFirstObjectByType<Timer>();
        coinNumber = FindFirstObjectByType<CoinNumber>();
        player = FindFirstObjectByType<PlayerMovement>();
        rocketSpawner = FindFirstObjectByType<RocketSpawnController>();
        joystick = FindFirstObjectByType<Joystick>();

        winDialog.gameObject.SetActive(false);
        loseDialog.gameObject.SetActive(false);
    }

    public void Gameover(bool win)
    {
        if (gameEnded) return;
        gameEnded = true;

        gameMusicSource.Stop();

        DisableGameplay();

        gameUI.gameObject.SetActive(false);

        if (win)
        {
            winDialog.gameObject.SetActive(true);
            winTexts[0].text = timer.timeStr;
            winTexts[1].text = coinNumber.coinNumber.ToString();
            gameMusicSource.PlayOneShot(winClip);
        }
        else
        {
            loseDialog.gameObject.SetActive(true);
            loseTexts[0].text = timer.timeStr;
            loseTexts[1].text = coinNumber.coinNumber.ToString();
            gameMusicSource.PlayOneShot(loseClip);
        }
    }

    private void DisableGameplay()
    {
        if (joystick != null)
            joystick.gameObject.SetActive(false);

        if (rocketSpawner != null)
            rocketSpawner.gameObject.SetActive(false);

        if (player != null)
        {
            if (player.animator != null)
            {
                player.animator.SetBool("isSitting", false);
                player.animator.SetBool("isRunning", false);
                player.animator.SetBool("isWalking", false);
            }
        }
        Destroy(player);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
