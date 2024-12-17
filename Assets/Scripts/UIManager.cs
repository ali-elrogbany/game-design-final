using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject invisiblePowerupIcon;
    [SerializeField] private GameObject doubleScorePowerupIcon;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
    }

    public void UpdateScoreText(float score)
    {
        scoreText.text = ((int) score).ToString();
    }

    public void SetInvisiblePowerupIcon(bool active)
    {
        invisiblePowerupIcon.SetActive(active);
    }

    public void SetDoubleScorePowerupIcon(bool active)
    {
        doubleScorePowerupIcon.SetActive(active);
    }
}
