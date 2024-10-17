namespace CuGraph.Tools.DijcstraAlg;
using CuGraph.Graph;
using CuGraph.Node;

public class Dijcstra{

    private CustomGraph Graph=new CustomGraph();

    public Dijcstra(CustomGraph graph){
        Graph=graph;
    }

    public Path FindShortestWay(CustomNode start, CustomNode finish){
        // создаём очередь с приоритетом и список посещённых
        PriorityQueue<CustomNode,int> to_visit=new PriorityQueue<CustomNode, int>();
        List<CustomNode> nodes=new List<CustomNode>();


        if (start==null || finish==null){
            return new Path(-1,new List<CustomNode>());
        }

        to_visit.Enqueue(start,0);

        while(to_visit.Count>0){
            //берем из очереди вершину
            to_visit.TryDequeue(out CustomNode node,out int length);
            nodes.Add(node);
            if (node.Name==finish.Name){
                return new Path(length,nodes);
            }
            //проходимся пососедям
            foreach(var edge in node.Neighbours){
                if (nodes.Where(x=> x.Name==edge.Finish.Name).ToList().Count==0){
                    to_visit.Enqueue(edge.Finish,edge.Length+length);
                }
            }

        }

        return new Path(-1,new List<CustomNode>());
    }


}

public record Path(int Length, List<CustomNode> Nodes);