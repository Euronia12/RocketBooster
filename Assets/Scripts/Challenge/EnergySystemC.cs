﻿using System;
using UnityEngine;

public class EnergySystemC : MonoBehaviour
{
    public event Action OnEnergyChanged;

    public float MaxFuel { get; private set; } = 10f;
    public float Fuel { get; private set; } = 10f;
    
    public bool UseEnergy(float amount)
    {
        if (Fuel < amount) return false;
        Fuel -= amount;
        return true;
    }

    private void Update()
    {
        if (Fuel < MaxFuel)
            Fuel += Time.deltaTime;
        else
            Fuel = MaxFuel;
        OnEnergyChanged?.Invoke();
    }
}