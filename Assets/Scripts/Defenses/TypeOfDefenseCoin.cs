using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TypeOfDefenseCoin : MonoBehaviour
{
    public GameObject DefenseID;
    public DefenseType defenseType;

    [SerializeField] GameObject offTower;
    [SerializeField] private GameObject _offTower;
    [SerializeField] MeshRenderer _offTowerSR;
    public Material skyBlue;
    public Material Red;

    [SerializeField] private Collider _col;
    [SerializeField] private Rigidbody _rigidbody;
    public bool OnHand;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        _offTower = Instantiate(offTower, transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
        _col = _offTower.GetComponent<Collider>();
        _offTower.SetActive(false);
    }
    private void Update()
    {
        _offTower.transform.position = new Vector3(transform.position.x, 0 ,transform.position.z);
    }
    public void CoinOnHand()
    {
        OnHand = true;
    }

    public void CoinOffHand()
    {
        OnHand = false;
    }

    private void ActivateGhostTower()
    {
        _offTower.SetActive(true);
    }
    private void DeactivateGhostTower()
    {
        _offTower.SetActive(false);
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            _offTowerSR = _offTower.GetComponent<MeshRenderer>();
            if (!OnHand && MyGrid.singleton.TryPlaceTower(_col))
            {
                var defense = Instantiate(DefenseID, transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
                defense.transform.position = gameObject.transform.position;
                Destroy(gameObject);
            }
            else if (OnHand && MyGrid.singleton.TryPlaceTower(_col))
            {
                ActivateGhostTower();
                _offTowerSR.material = skyBlue;
            }
            else if (OnHand && !MyGrid.singleton.TryPlaceTower(_col))
            {
                ActivateGhostTower();
                _offTowerSR.material = Red;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        DeactivateGhostTower();
    }
}
