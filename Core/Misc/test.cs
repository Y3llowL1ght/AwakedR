using Godot;
using System;
using GridLib;

public class test : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
        Grid<int> testgrid = new Grid<int>(10,10);
        testgrid.Place(5, new GCoord(0,0), new GCoord(5,1));
        //testgrid.Place(4, new GCoord(0,0), new GCoord(5,2));
        //testgrid.Place(3, new GCoord(7,6), new GCoord(2,2));

        int[,] grid = testgrid.GridAsObjects();
        for (int y = 0; y < 10; y++)
        {
            string buff = "";
            for (int x = 0; x < 10; x++)
            {
                buff += $"{grid[x,y]} ";
            }
            GD.Print(buff);
        }
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//          
//  }
}
