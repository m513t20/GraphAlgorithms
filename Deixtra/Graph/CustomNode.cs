using System.Reflection.Metadata;

namespace CustomGraph.Node;

class CustomNode{
    
    public HashSet<CustomNode> neighbours{get; set;}=new HashSet<CustomNode>();
    public string Name {get; set;}=string.Empty;

    public CustomNode (string aname){
        Name = aname;
    }



    public void AddNeighbour(CustomNode neighbour){
        neighbours.Add(neighbour);
    }   
    public void RemoveNeighbour(CustomNode neighbour){
        neighbours.Remove(neighbour);
    }



}