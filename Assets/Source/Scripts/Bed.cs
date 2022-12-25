using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Bed : MonoBehaviour
{
    [SerializeField] private GameObject darkPanel;
    [SerializeField] private PlayerInput playerInput;

    public static Action<bool> OnDayStart;

    private bool isDayEnd;
    private bool isSleep;
    private int alpha;
    private Color color;

    void Start()
    {
        color = darkPanel.GetComponent<Image>().color;
        Player.OnZeroEndurance += DayEnd;
    }

    private void Update()
    {
        if (alpha == 2)
            isDayEnd = false;
        if (!isDayEnd && isSleep)
        {
            playerInput.ActivateInput();
            OnDayStart?.Invoke(isDayEnd);
            isSleep = false;
            alpha = 0;
        }
    }

    public void Sleep()
    {
        playerInput.DeactivateInput();
        StartCoroutine(IncreaseDecreaseAlpha());
        isSleep = true;
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
