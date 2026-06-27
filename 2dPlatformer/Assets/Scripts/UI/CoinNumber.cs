using TMPro;
using UnityEngine;

public class CoinNumber : MonoBehaviour
{
    [SerializeField] private TMP_Text coinNumberTxt;
    [SerializeField] private AudioClip coinCollectClip;
    [SerializeField] private AudioSource audioSource;

    private float minPitch = 0.9f;
    private float maxPitch = 1.1f;

    public int coinNumber = 0;

    public static CoinNumber instance;
    private void Awake() { instance = this; }

    private void Start()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        coinNumberTxt.text = coinNumber + " x";
    }

    public void AddCoin(int AddAmount = 1)
    {
        coinNumber += AddAmount;
        UpdateUI();
        if (coinNumber == 1) RocketSpawnController.instance.gotFirstCoin = true;

        audioSource.pitch = Random.Range(minPitch, maxPitch);
        audioSource.PlayOneShot(coinCollectClip);
    }
}
