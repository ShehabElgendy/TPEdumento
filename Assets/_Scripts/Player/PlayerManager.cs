using UnityEngine;

public class PlayerManager : MonoBehaviour, ISaveManager
{
    public static PlayerManager instance;

    public int currency;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public bool HaveEnoughCurrency(int _price)
    {
        if (_price > currency)
        {
            Debug.Log("Not Enough Currency");
            return false;
        }

        currency -= _price;

        return true;
    }

    public int GetCurrency()
    {
        return currency;
    }
    public void LoadData(GameData _data)
    {
        this.currency = _data.currency;
    }

    public void SaveData(ref GameData _data)
    {
        _data.currency = this.currency;
    }


}

