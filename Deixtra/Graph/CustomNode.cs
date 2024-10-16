using CuGraph.Edge;

namespace CuGraph.Node;

public class CustomNode{
    
    public HashSet<CustomEdge> Neighbours{get; set;}=new HashSet<CustomEdge>();
    public string Name {get; set;}=string.Empty;

    public CustomNode (string aname){
        Name = aname;
    }

    public CustomNode(){}


    public void AddNeighbour(CustomNode neighbour,int length=1){
        Neighbours.Add(new CustomEdge(neighbour,length));
    }   
    public void RemoveNeighbour(CustomNode neighbour){

        Neighbours.RemoveWhere(x => x.Finish==neighbour);
    }



}