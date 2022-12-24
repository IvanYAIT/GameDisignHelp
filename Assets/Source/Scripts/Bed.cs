using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Bed : MonoBehaviour
{
    [SerializeField] private GameObject darkPanel;

    public static Action<bool> OnDayStart;

    private bool isDayEnd;
    private int alpha;
    private Color color;

    void Start()
    {
        color = darkPanel.GetComponent<Image>().color;
        Dialog.OnDayEnd += DayEnd;
    }

    public void Sleep()
    {
        if (isDayEnd)
        {
            Time.timeScale = 0;
            StartCoroutine(IncreaseDecreaseAlpha());
            if (alpha == 2)
                isDayEnd = false;
        }
        else
        {
            Time.timeScale = 1;
            OnDayStart?.Invoke(isDayEnd);
        }
    }

    public void DayEnd(bool isDayEnd) =>
        this.isDayEnd = isDayEnd;

    IEnumerator IncreaseDecreaseAlpha()
    {
        while (alpha == 0)
        {
            color.a += 0.2f;
            darkPanel.GetComponent<Image>().color = color;
            if (color.a >= 1)
                alpha = 1;

            yield return new WaitForSeconds(0.5f);
        }
        while (alpha == 1)
        {
            color.a -= 0.2f;
            darkPanel.GetComponent<Image>().color = color;
            if (color.a <= 0)
                alpha = 2;
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void View(TextMeshProUGUI text)
    {
        if(isDayEnd)
            text.text = "Нажмите E чтобы поспать";
    }
}
