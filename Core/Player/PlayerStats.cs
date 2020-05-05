using Godot;
using System;
using System.Collections.Generic;
//using Newtonsoft.Json;


public class PlayerStats : Node2D
{
    //Durability
        public double Health;
[Export]public double HealthMax = 100;
[Export]public double BaseHealthRegen = 10;
        public double HealthRegen = 0;

        public double Shields = 0;
        public double ShieldsMax = 0;

    
[Export]public double Armor = 0;

    //Env/Survival
        public double Energy = 100;
[Export]public double EnergyMax = 1000;
[Export]public double BaseEnergyRegen = 10;
        public double EnergyRegen = 0;

        public double Heat = 0;
[Export]public double HeatMax = 100;
[Export]public double BaseHeatVent = 10;

        public double Temperature = 0;
[Export]public double MaxTemp = 50;
[Export]public double ComfortTemp = 31;
[Export]public double MinTemp = -5;
[Export]public double TempChangeModifier = 1;
[Export]public double TempChangeEnergyCost = 1;

    //Progression
        public double Experience = 0;
[Export]public double ExpToNext = 1000;
        public int Level = 0;

    //Other
[Export]public double MovementSpeed = 200;

    //Dictionary
    public Dictionary<string, double> DStats;

    public override void _Ready()
    {

        //JSON
        
    }

    public override void _Process(float delta){
        
        //REGENS
        Heal((HealthRegen + BaseHealthRegen) * delta);
        AddEnergy((EnergyRegen + BaseEnergyRegen) * delta);
        VentilateHeat(BaseHeatVent * delta);

    }

    //HEALTH RELATED
    public void TakeHealthDamage(double value){
        if(Health - value <= 0){
            Health = 0;    
            Die();    
        }else Health -= value;
    }

    
    public void Heal(double amount){
        if(Health + amount > HealthMax){
            Health = HealthMax;        
        }else Health += amount;  
    }

    public void Die(){
          GD.Print("Dead");
    }

    //ENERGY RELATED
    public void AddEnergy(double amount){
        if(Energy + amount > EnergyMax){
            Energy = EnergyMax;        
        }else Energy += amount; 
    }

    public bool DecreaseEnergy(double amount){
        if (Energy - amount >= 0)
        {
            Energy -= amount;
            return true;
        }else return false;
        
    }

    //HEAT RELATED
        //add something for overheating?
    public double AddHeat(double amount){
       
        if (Heat + amount > HeatMax)
        {
            double heatleft = Heat + amount - HeatMax;
            Heat = HeatMax;
            return heatleft;
        }else{
             Clamp(Heat, amount, 0, HeatMax);
             return 0;
        }
    }

    public void VentilateHeat(double amount){
        Clamp(Heat, amount, 0, HeatMax);
    }


    public bool RequestHeat(double amount){
        if (Heat - amount >= 0)
        {
            Heat -= amount;
            return true;
        }else return false;
        
    }

    //SHIELDS
    public void TakeShieldDamage(double value){
        if(Shields - value <= 0){
            Shields = 0;    
            //Шилды все  
        }else Shields -= value;
    }

    //MISC
    public double Clamp(double value, double amount, double min, double max){
        if (value + amount > max) return max;
        if (value + amount < min) return min;
        return value + amount;
        
    }



}

