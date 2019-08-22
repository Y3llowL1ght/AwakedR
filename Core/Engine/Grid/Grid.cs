using System;
using System.Collections.Generic;

namespace GridLib{

    //grid of objects of type T
    public class Grid<T>{
        
        public Dictionary<int, GridObject<T>> gridObjects;
        public List<int> freeIds;
        public int[,] grid{get;}
        public GCoord size;

        //Constructor
        public Grid(int xsize, int ysize)
        {
            //initializing Grid itself
            this.size = new GCoord(xsize,ysize);
            this.grid = new int[xsize, ysize];

            //list of gObjects
            this.gridObjects = new Dictionary<int, GridObject<T>>();
            //Initializing "EMPTY CELL gObject"
            this.gridObjects.Add(0,new GridObject<T>(0));

            freeIds = new List<int>();
        }
        
        //Place method. that works with rectangle objects
        public bool Place(T gObject, GCoord coordinate, GCoord size){
            if(FitsRect(coordinate,size)){

                //finds Id for the place method
                int assignedID = -1;
                if (freeIds.Count > 0){
                    assignedID = freeIds[0];
                    freeIds.Remove(assignedID);
                }else{
                    assignedID = gridObjects.Count + 1;
                }

                //sets up grid cells
                List<GCoord> assignedCells = new List<GCoord>();
                for (int y = coordinate.y; y < coordinate.y + size.y; y++)
                {
                    for (int x = coordinate.x; x < coordinate.x + size.x; x++){
                        if (grid[x,y] == 0 )
                        {
                            grid[x,y] = assignedID;
                            assignedCells.Add(new GCoord(x,y));
                        }
                    }   
                }

                //Creates gObject and adds it to gridObjects dictionary with corresponding id
                gridObjects.Add(assignedID,new GridObject<T>(assignedCells,assignedID,gObject));

                return true;
            }else{
                return false;
            }
                
        }

        //Removes existing grid object by id
        public void Remove(int id){
            GridObject<T> gridObject = gridObjects[id];
            foreach (var cell in gridObject.relatedCells)
            {
                grid[cell.x, cell.y] = 0;
            }
            //freeing id
            freeIds.Add(id);
            //deleting from dictionary
            gridObjects.Remove(id);
        }

        //Checks if rectangle (coordinate : coordinate + size) is all empty
        public bool FitsRect(GCoord coordinate, GCoord size){
            bool gate = true;
            for (int x = coordinate.x; x < coordinate.x + size.x; x++)
            {
                for (int y = coordinate.y; y < coordinate.y + size.y; y++){
                    if (grid[x,y] != 0 )
                    {
                        gate = false;
                    }
                }   
            }
            return gate;
        }

        public T[,] GridAsObjects(){
            T[,] objectgrid = new T[size.x,size.y];

            for (int y = 0; y < size.y; y++)
            {
              for (int x = 0; x < size.x; x++)
              {
                  objectgrid[x,y] = gridObjects[grid[x,y]].gObject;
              }  
            }

            return objectgrid;
        }

        public T ObjectAt(GCoord coord){
            return gridObjects[grid[coord.x,coord.y]].gObject;
        }
        public GridObject<T> GridObjectAt(GCoord coord){
            return gridObjects[grid[coord.x,coord.y]];
        }
        
        
    }


    public class GridObject<T>{

        public List<GCoord> relatedCells;
        public int id{get;}
        public T gObject{get;set;}

        public GridObject(List<GCoord> relatedCells, int id, T gObject)
        {
            this.relatedCells = relatedCells;
            this.id = id;
            this.gObject = gObject;
        }

        public GridObject(int id)
        {
            this.id = id;
        }
        

    }


    public struct GCoord{
        public int x, y;
        public GCoord(int x, int y){
            this.x = x;
            this.y = y;
        }
    }



}