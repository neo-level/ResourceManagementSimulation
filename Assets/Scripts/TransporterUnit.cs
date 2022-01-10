using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Subclass of Unit that will transport resource from a Resource Pile back to Base.
/// </summary>
public class TransporterUnit : Unit
{
    public int maxAmountTransported = 1;

    private Building _mCurrentTransportTarget;
    private Building.InventoryEntry _mTransporting = new Building.InventoryEntry();

    // We override the GoTo function to remove the current transport target, as any go to order will cancel the transport
    public override void GoTo(Vector3 position)
    {
        base.GoTo(position);
        _mCurrentTransportTarget = null;
    }

    protected override void BuildingInRange()
    {
        if (MTarget == Base.Instance)
        {
            //we arrive at the base, unload!
            if (_mTransporting.count > 0)
                MTarget.AddItem(_mTransporting.resourceId, _mTransporting.count);

            //we go back to the building we came from
            GoTo(_mCurrentTransportTarget);
            _mTransporting.count = 0;
            _mTransporting.resourceId = "";
        }
        else
        {
            if (MTarget.Inventory.Count > 0)
            {
                _mTransporting.resourceId = MTarget.Inventory[0].resourceId;
                _mTransporting.count = MTarget.GetItem(_mTransporting.resourceId, maxAmountTransported);
                _mCurrentTransportTarget = MTarget;
                GoTo(Base.Instance);
            }
        }
    }

    //Override all the UI function to give a new name and display what it is currently transporting
    public override string GetName()
    {
        return "Transporter";
    }

    public override string GetData()
    {
        return $"Can transport up to {maxAmountTransported}";
    }

    public override void GetContent(ref List<Building.InventoryEntry> content)
    {
        if (_mTransporting.count > 0)
            content.Add(_mTransporting);
    }
}