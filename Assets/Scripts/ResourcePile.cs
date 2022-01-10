using Helpers;
using UnityEngine;

/// <summary>
/// A subclass of Building that produce resource at a constant rate.
/// </summary>
public class ResourcePile : Building
{
    public ResourceItem item;

    public float productionSpeed = 0.5f;

    private float _mCurrentProduction;

    private void Update()
    {
        if (_mCurrentProduction > 1.0f)
        {
            int amountToAdd = Mathf.FloorToInt(_mCurrentProduction);
            int leftOver = AddItem(item.id, amountToAdd);

            _mCurrentProduction -= amountToAdd + leftOver;
        }

        if (_mCurrentProduction < 1.0f)
        {
            _mCurrentProduction += productionSpeed * Time.deltaTime;
        }
    }

    public override string GetData()
    {
        return $"Producing at the speed of {productionSpeed}/s";
    }
}