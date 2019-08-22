using Godot;
using System;
using System.Collections.Generic;

public class Map : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {   
                int[] e = {1,2,3,4};
        List<int> t = new List<int>(e);
        GD.Print(t[1]);
        
        
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
