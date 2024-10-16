using CuGraph.Edge;
using CuGraph.Node;

namespace CuGraph.Graph;

public class CustomGraph
{
    public HashSet<CustomNode> Nodes { get; set; }=new HashSet<CustomNode>();

    public void AddNode(CustomNode node){
        Nodes.Add(node);
    }
    public void RemoveNode(string node_name){
        var node = Nodes.Where(x=> x.Name==node_name).FirstOrDefault();

        
        if(node==null){
            throw new Exception("Вершина не найдена");
        }

        node.Neighbours.Clear();

        foreach(CustomNode child in Nodes){
            child.RemoveNeighbour(node);
        }
        Nodes.Remove(node);
    }

    public void AddEdge(string begin, string end,int length){
        var start=Nodes.Where(x=>x.Name==begin).FirstOrDefault();
        var finish=Nodes.Where(x=>x.Name==end).FirstOrDefault();

        if(start==null||finish==null){
            throw new Exception("Вершины не найдены");
        }


        start.AddNeighbour(finish,length);
    }
    
    public void RemoveEdge(string begin, string end)
    {
        var start=Nodes.Where(x=>x.Name==begin).FirstOrDefault();
        var finish=Nodes.Where(x=>x.Name==end).FirstOrDefault();

        if(start==null||finish==null){
            throw new Exception("Вершины не найдены");
        }


        start.RemoveNeighbour(finish);
    }
    
}