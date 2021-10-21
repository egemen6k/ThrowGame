using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBall : MonoBehaviour
{
    [SerializeField] private Rigidbody Hook;
    private Rigidbody _rbPlayer;
    private bool _isPressed = false;
    private float maxDragDistance = 2f;
    private float _velocityMultiplier = 13f;
    private Vector3 mousePos, firstpos;
    public Vector3 liveVelVector;
    public GameObject nextBall;
    public AudioClip _clip;
    public bool isShot = false;
    public ParticleSystem playerParticle;

    private void Start()
    {
        _rbPlayer = GetComponent<Rigidbody>();
        _rbPlayer.isKinematic = true;

        firstpos = _rbPlayer.position;

    }
    private void Update()
    {
        if (_isPressed == true)
        {
            mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,10));
            if (Vector3.Distance(mousePos, Hook.position) > maxDragDistance)
            {
                _rbPlayer.position = Hook.position + (mousePos - Hook.position).normalized * maxDragDistance;
            }
            else
            {
                _rbPlayer.position = mousePos;
            }
        }

        liveVelVector = (firstpos - _rbPlayer.position) * _velocityMultiplier;
    }

    private void OnMouseDown()
    {
        _isPressed = true;
    }

    private void OnMouseUp()
    {
        _isPressed = false;
        _rbPlayer.isKinematic = false;
        AudioSource.PlayClipAtPoint(_clip, _rbPlayer.position);
        StartCoroutine(Release(liveVelVector));
        this.enabled = false;
    }

    IEnumerator Release(Vector3 velocityVector)
    {
        _rbPlayer.velocity = velocityVector;
        isShot = true;
        yield return new WaitForSeconds(4f);

       if (nextBall != null)
        {
            nextBall.SetActive(true);
        }
        else
        {
            yield return new WaitForSeconds(5);
            SceneManager.LoadScene(0);
        }
        yield return new WaitForSeconds(3);
        Instantiate(playerParticle, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
