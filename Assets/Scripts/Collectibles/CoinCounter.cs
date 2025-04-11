using TMPro;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    private bool _firstLoad = true;
    private uint _coinCount = 0;
    private TMP_Text _tmpText = default;

    private void Awake()
    {
        if(!gameObject.TryGetComponent(out _tmpText))
        {
            _tmpText = GetComponentInChildren<TMP_Text>();

            if(_tmpText == null)
            {
                _tmpText = GetComponent<TMP_Text>();
            }
        }

        if (_firstLoad)
        {
            _coinCount = 0;
            _tmpText.text = "0";
            _firstLoad = false;
        }
    }

    private void OnEnable()
    {
        Coin.CoinCollected += OnCoinCollected;
    }

    private void OnDisable()
    {
        Coin.CoinCollected -= OnCoinCollected;
    }

    private void OnCoinCollected(uint points)
    {
        _coinCount += points;
        _tmpText.text = _coinCount.ToString();
    }
}
