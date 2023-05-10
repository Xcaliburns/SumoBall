using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public bool haspowerup = false;
    public GameObject powerupIndicator;
    private Rigidbody playerRb;
    private GameObject focalPoint;
    private float powerupStrength = 15.0f;
    private float powerupDuration = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
    }

    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        Vector3 indicatorOffset = new Vector3(0, -0.5f, 0);
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
        powerupIndicator.transform.position = transform.position + indicatorOffset;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            haspowerup = true;
            StartCoroutine(PowerupCoroutine());
            powerupIndicator.gameObject.SetActive(true);
            Debug.Log(haspowerup);
        }
    }

    IEnumerator PowerupCoroutine()
    {
        yield return new WaitForSeconds(powerupDuration);
        haspowerup = false;
        powerupIndicator.gameObject.SetActive(false);
        Debug.Log(haspowerup);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && haspowerup)
        {
            Rigidbody enemyRigidbody = (collision.gameObject).GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
            Debug.Log("collision with" + collision.gameObject.name + "with powerup set to " + haspowerup);
            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }
    }

    // Update is called once per frame

}
