using System;
using System.Collections.Generic;

namespace GridLib{

    //Grid of objects of type T
    public class Grid<T>{
        
        public Dictionary<int, GridObject<T>> GridObjects;
        public List<int> FreeIDs;
        public int[,] GridArray{get;}
        public GCoord GridSize;

        public Grid(int xsize, int ysize)
        {
            //initializing Grid itself
            this.GridSize = new GCoord(xsize,ysize);
            this.GridArray = new int[xsize, ysize];

            //list of gObjects
            this.GridObjects = new Dictionary<int, GridObject<T>>();
            //Initializing "EMPTY CELL gObject"
            this.GridObjects.Add(0,new GridObject<T>(0));

            FreeIDs = new List<int>();
        }
        
        //Place method that works with rectangle shapes
        public int Place(T gObject, GCoord coordinate, GCoord size){
            if(FitsPattern(coordinate,size)){

                //finds Id for the place method
                int assignedID;
                if (FreeIDs.Count > 0){
                    assignedID = FreeIDs[0];
                    FreeIDs.Remove(assignedID);
                }else{
                    assignedID = GridObjects.Count + 1;
                }

                //sets up Grid cells
                List<GCoord> assignedCells = new List<GCoord>();
                for (int y = coordinate.y; y < coordinate.y + size.y; y++)
                {
                    for (int x = coordinate.x; x < coordinate.x + size.x; x++){
                        
                        GridArray[x,y] = assignedID;
                        assignedCells.Add(new GCoord(x,y));
                        
                    }   
                }

                //Creates gObject and adds it to gridObjects dictionary with corresponding id
                GridObjects.Add(assignedID,new GridObject<T>(assignedCells,assignedID,gObject));

                return assignedID;
            }else{
                return 0;
            }
                
        }

        //Place method that works with custom shapes
        public int Place(T gObject, GCoord origin, List<GCoord> pattern){
            if(FitsPattern(origin, pattern)){

                //finds Id for the place method
                int assignedID;
                if (FreeIDs.Count > 0){
                    assignedID = FreeIDs[0];
                    FreeIDs.Remove(assignedID);
                }else{
                    assignedID = GridObjects.Count + 1;
                }

                //sets up Grid cells
                List<GCoord> assignedCells = new List<GCoord>();

                foreach (GCoord localc in pattern){
                    int cx = origin.x + localc.x, cy = origin.y + localc.y;
                    GridArray[cx,cy] = assignedID;
                    assignedCells.Add(new GCoord(cx,cy));
                }

                //Creates gObject and adds it to gridObjects dictionary with corresponding id
                GridObjects.Add(assignedID,new GridObject<T>(assignedCells,assignedID,gObject));

                return assignedID;
            }else{
                return 0;
            }
                
        }

        //Removes existing Grid object by id
        public void Remove(int id){
            GridObject<T> gridObject = GridObjects[id];
            foreach (var cell in gridObject.relatedCells)
            {
                GridArray[cell.x, cell.y] = 0;
            }
            
            FreeIDs.Add(id);
            GridObjects.Remove(id);
        }

        //Checks if rectangle shape is in boundaries of the grid
        public bool IsInMapRange(GCoord origin, GCoord size){
            return origin.x + size.x <= GridSize.x && origin.y + size.y <= GridSize.y;
        }

        //Checks if cystom shape is in boundaries of the grid
        public bool IsInMapRange(GCoord origin, List<GCoord> pattern){
            bool gate = true;
            foreach (GCoord localc in pattern)
            {
                gate = (origin.x + localc.x <= GridSize.x && origin.y + localc.y <= GridSize.y);
            }
            return gate;
        }

        //Checks if rectangle (origin : origin + size) is all empty 
        public bool FitsPattern(GCoord origin, GCoord size){
            bool gate = IsInMapRange(origin, size);
            if (!gate) return gate;
            for (int x = origin.x; x < origin.x + size.x; x++)
            {
                for (int y = origin.y; y < origin.y + size.y; y++){
                    gate = (GridArray[x,y] == 0);
                }   
            }
            return gate;
        }

        //Detects any Grid collision provided with origin coordinate in global Grid and list of local coordinates relative to origin
        public bool FitsPattern(GCoord origin, List<GCoord> pattern){
            bool gate = IsInMapRange(origin, pattern);
            if (!gate) return gate;
            foreach (GCoord localc in pattern)
            {
                gate = (GridArray[origin.x + localc.x, origin.y + localc.y] == 0);
            }
            return gate;
        }

        //Represents Grid as objects instead ids
        public T[,] GridAsObjects(){
            T[,] objectgrid = new T[GridSize.x,GridSize.y];

            for (int y = 0; y < GridSize.y; y++)
            {
              for (int x = 0; x < GridSize.x; x++)
              {
                  objectgrid[x,y] = GridObjects[GridArray[x,y]].gObject;
              }  
            }

            return objectgrid;
        }

        //Returns object at coordinates
        public T ObjectAt(GCoord coord){
            return GridObjects[GridArray[coord.x,coord.y]].gObject;
        }

        //Returns GridObject at coordinates
        public GridObject<T> GridObjectAt(GCoord coord){
            return GridObjects[GridArray[coord.x,coord.y]];
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