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
        if (length<=0){
            Console.WriteLine("Недопустимая длинна пути");
            return;
        }
        var start=Nodes.Where(x=>x.Name==begin).FirstOrDefault();
        var finish=Nodes.Where(x=>x.Name==end).FirstOrDefault();

        if(start==null){
            start=new CustomNode(begin);
            AddNode(start);
        }
        if(finish==null){
            finish=new CustomNode(end);
            AddNode(finish);
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
    
    public void ReadFromFile(string file_name){
        StreamReader reader = File.OpenText(file_name);
        var line=reader.ReadLine();
        line=reader.ReadLine();
        while(line!=null){
            var data=line.Split('\t');
            AddEdge(int.Parse(data[0]).ToString(),int.Parse(data[1]).ToString(),int.Parse(data[2]));
            AddEdge(int.Parse(data[1]).ToString(),int.Parse(data[0]).ToString(),int.Parse(data[2]));

            line=reader.ReadLine();
        }
        Console.WriteLine(this.Nodes.ToArray().Length);

    }
}