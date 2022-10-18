using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cable : MonoBehaviour
{
    [SerializeField, Range(0, 100)] private float m_ImpactChance;
    [SerializeField, Range(0, 10)] private float m_ImpactCheckageTimeInSeconds;
    [SerializeField] private float m_ImpactForce;
    private PlayerController player;
    private float counter;
    private bool isCounting;

    private void Update()
    {
        if(player != null)
        {
            if (!isCounting)
            {
                isCounting = true;
                counter = m_ImpactCheckageTimeInSeconds;
            }

            counter = counter >= 0 ? counter - Time.deltaTime : 0;

            if(counter == 0)
            {
                float randomChance = Random.Range(0f, 100f);

                if(randomChance <= m_ImpactChance)
                {
                    Vector3 force = player.transform.right;
                    int random = Random.Range(0, 10);

                    player.AddForce(random <= 5 ? force : -force, m_ImpactForce);

                    counter = m_ImpactCheckageTimeInSeconds;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();

        if(playerController != null)
        {
            player = playerController;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();

        if(playerController != null)
        {
            player = null;
            isCounting = false;
        }
    }
}