using System.Collections;
using System.Text;
using UnityEngine;
using TMPro;

/// <summary>
/// Manages activation, duration coroutines, and timer UI formatting for various player power-ups.
/// </summary>
public class PlayerPowerUps : MonoBehaviour
{
    [Header("Power-Up States")]
    public bool IsTripleShotActive { get; private set; }
    public bool IsShieldActive { get; private set; }
    public bool IsMagnetActive { get; private set; }

    [Header("Shield UI/Visual")]
    [SerializeField] private GameObject shieldVisual;

    [Header("Rocket Settings")]
    [SerializeField] private GameObject rocketPrefab;
    [SerializeField] private Transform firePoint;

    [Header("Cooldown UI Text")]
    [SerializeField] private TMP_Text timerText;

    private Coroutine tripleShotCoroutine;
    private Coroutine shieldCoroutine;
    private Coroutine magnetCoroutine;
    private Coroutine timerCoroutine;

    private float tripleShotTimeRemaining;
    private float shieldTimeRemaining;
    private float magnetTimeRemaining;

    private void Start()
    {
        if (timerText != null)
        {
            timerText.gameObject.SetActive(false);
        }
    }

    public void ActivatePowerUp(ItemType type, float duration)
    {
        switch (type)
        {
            case ItemType.TripleShot:
                if (IsTripleShotActive)
                {
                    tripleShotTimeRemaining += duration; // Extend time if already active
                }
                else
                {
                    if (tripleShotCoroutine != null) StopCoroutine(tripleShotCoroutine);
                    tripleShotCoroutine = StartCoroutine(TripleShotRoutine(duration));
                }
                break;

            case ItemType.Shield:
                if (IsShieldActive)
                {
                    shieldTimeRemaining += duration;
                }
                else
                {
                    if (shieldCoroutine != null) StopCoroutine(shieldCoroutine);
                    shieldCoroutine = StartCoroutine(ShieldRoutine(duration));
                }
                break;

            case ItemType.Rocket:
                LaunchRocket();
                break;

            case ItemType.Magnet:
                if (IsMagnetActive)
                {
                    magnetTimeRemaining += duration;
                }
                else
                {
                    if (magnetCoroutine != null) StopCoroutine(magnetCoroutine);
                    magnetCoroutine = StartCoroutine(MagnetRoutine(duration));
                }
                break;
        }

        if (type != ItemType.Rocket)
        {
            if (timerCoroutine == null)
            {
                timerCoroutine = StartCoroutine(UpdateTimerUIRoutine());
            }
        }
    }

    private IEnumerator TripleShotRoutine(float duration)
    {
        IsTripleShotActive = true;
        tripleShotTimeRemaining = duration;

        while (tripleShotTimeRemaining > 0f)
        {
            if (!PauseManager.IsPaused && !Mathf.Approximately(Time.timeScale, 0f))
            {
                tripleShotTimeRemaining -= Time.deltaTime;
            }
            yield return null;
        }

        IsTripleShotActive = false;
        tripleShotTimeRemaining = 0f;
    }

    private IEnumerator ShieldRoutine(float duration)
    {
        IsShieldActive = true;
        shieldTimeRemaining = duration;
        if (shieldVisual != null) shieldVisual.SetActive(true);

        while (shieldTimeRemaining > 0f)
        {
            if (!PauseManager.IsPaused && !Mathf.Approximately(Time.timeScale, 0f))
            {
                shieldTimeRemaining -= Time.deltaTime;
            }
            yield return null;
        }

        IsShieldActive = false;
        shieldTimeRemaining = 0f;
        if (shieldVisual != null) shieldVisual.SetActive(false);
    }

    private IEnumerator MagnetRoutine(float duration)
    {
        IsMagnetActive = true;
        magnetTimeRemaining = duration;

        while (magnetTimeRemaining > 0f)
        {
            if (!PauseManager.IsPaused && !Mathf.Approximately(Time.timeScale, 0f))
            {
                magnetTimeRemaining -= Time.deltaTime;
            }
            yield return null;
        }

        IsMagnetActive = false;
        magnetTimeRemaining = 0f;
    }

    private IEnumerator UpdateTimerUIRoutine()
    {
        if (timerText != null) timerText.gameObject.SetActive(true);

        StringBuilder stringBuilder = new StringBuilder();

        while (IsTripleShotActive || IsShieldActive || IsMagnetActive)
        {
            stringBuilder.Clear();

            if (IsTripleShotActive)
            {
                int seconds = Mathf.CeilToInt(tripleShotTimeRemaining);
                stringBuilder.AppendLine($"Triple: {seconds}s");
            }

            if (IsMagnetActive)
            {
                int seconds = Mathf.CeilToInt(magnetTimeRemaining);
                stringBuilder.AppendLine($"Magnet: {seconds}s");
            }

            if (IsShieldActive)
            {
                int seconds = Mathf.CeilToInt(shieldTimeRemaining);
                stringBuilder.AppendLine($"Shield: {seconds}s");
            }

            if (timerText != null)
            {
                timerText.text = stringBuilder.ToString().TrimEnd();
            }

            yield return null;
        }

        if (timerText != null)
        {
            timerText.gameObject.SetActive(false);
        }
    }

    private void LaunchRocket()
    {
        if (rocketPrefab != null)
        {
            Vector3 spawnPos = firePoint != null ? firePoint.position : transform.position;
            Instantiate(rocketPrefab, spawnPos, Quaternion.identity);
        }
    }
}