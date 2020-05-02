using Godot;
using System.Collections.Generic;
using System;
using GridLib;

public class GridDrawer : Node2D
{
    
    Grid<int> GridToDraw;
    float size = 10f;
    float range = 2f;
    Vector2 offset = new Vector2(100f,0f);
    

    public override void _Ready()
    {
        range += size;

        GridToDraw = new Grid<int>(40,30);
        GridToDraw.Place(10, new GCoord(1,0),new GCoord(10,5));
        GridToDraw.Place(10, new GCoord(21,0),new GCoord(10,5));
        GridToDraw.Place(10, new GCoord(0,20),new GCoord(10,5));
        GridToDraw.Place(10, new GCoord(0,27),new GCoord(10,5));
        GridToDraw.Place(10, new GCoord(10,28),new GCoord(10,5));
        
        List<GCoord> custom = new List<GCoord>();
        custom.Add(new GCoord(0,0));
        custom.Add(new GCoord(0,-1));
        custom.Add(new GCoord(0,-2));
        custom.Add(new GCoord(1,-2));
        GridToDraw.Place(22, new GCoord(20,20), custom);

    }

    public override void _Draw()
    {
        Color greeng = new Color("#00A300");
        greeng.a = 0.8f;
        Color redg = new Color("#683333");
        redg.a = 0.8f;
        for (int y = 0; y < GridToDraw.GridSize.y; y++)
        {
            for (int x = 0; x < GridToDraw.GridSize.x; x++)
            {
                List<Vector2> points = new List<Vector2>();
                points.Add(new Vector2(0+(x*range)+offset.x,0+(y*range)+offset.y));
                points.Add(new Vector2(size+(x*range)+offset.x,0+(y*range)+offset.y));
                points.Add(new Vector2(size+(x*range)+offset.x,size+(y*range)+offset.y));
                points.Add(new Vector2(0+(x*range)+offset.x,size+(y*range)+offset.y));
                //points.Add(new Vector2(0+(x*range)+offset.x,0+(y*range)+offset.y));
                
                if(GridToDraw.GridArray[x,y] == 0){
                    DrawColoredPolygon(points.ToArray(), greeng);
                }else
                {
                    DrawColoredPolygon(points.ToArray(), redg);
                }
                //DrawPolyline(points.ToArray(), redg);
                
            }
        }
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
