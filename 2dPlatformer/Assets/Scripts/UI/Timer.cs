using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text timerTxt;
    private float sec = 0;
    private int min = 0;
    private int intSec;
    public string timeStr;
    private string secStr;
    private string minStr;

    private void FixedUpdate()
    {
        sec += Time.deltaTime;

        if (sec >= 60)
        {
            sec = 0;
            min += 1;
        }

        intSec = (int) sec;

        secStr = (intSec + "").Length == 1 ? "0" + intSec : intSec + "";
        minStr = (min + "").Length == 1 ? "0" + min : min + "";

        timeStr =  minStr + ":" + secStr;

        timerTxt.text = timeStr;
    }
}
