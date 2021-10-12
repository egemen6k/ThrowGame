using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionBehav : MonoBehaviour, ICollision
{
    [SerializeField]
    private Material _mat;
    [SerializeField]
    private AudioClip _collisionSound;
    [SerializeField]
    GameObject _explosionPrefab;
    [SerializeField]
    AudioClip _explosionSound;

    private Rigidbody _rb;
    private MeshRenderer _mr;
    private UIManager _uiManager;

    private void Start()
    {
        _rb = transform.GetComponent<Rigidbody>();
        if (_rb == null)
        {
            Debug.Log("Rigidbody is null");
        }

        _mr = GetComponent<MeshRenderer>();
        if (_mr == null)
        {
            Debug.Log("MeshRenderer is null");
        }

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null)
        {
            Debug.Log("UIManager is null");
        }
    }
    public bool onBall()
    {

        StartCoroutine(Sequence());
        return false;
    }

    public bool onPlayer()
    {
        StartCoroutine(Sequence());
        return false;
    }

    IEnumerator Sequence()
    {
        _rb.useGravity = true;
        _mr.material = _mat;
        AudioSource.PlayClipAtPoint(_collisionSound, transform.position);
        _uiManager.UpdateScore();

        yield return new WaitForSeconds(5f);

        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(_explosionSound, transform.position);
        Destroy(this.gameObject, 0.2f);
    }
}
