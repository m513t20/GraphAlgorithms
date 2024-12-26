namespace CuGraph.Tools.Ants;

using System.Numerics;
using CuGraph.Graph;
using CuGraph.Edge;
using CuGraph.Node;
public class Ant{
    public CustomNode Start{get;set;}=new CustomNode();

    public List<CustomNode> Visited{get;set;}=new List<CustomNode>();

    private List<CustomEdge> Edges{get;set;}=new List<CustomEdge>();

    public int WayLength{get;set;}=0;

    public bool IsMoving{get;set;}=true;

    public double alpha{get;set;}=1.0;

    public double beta{get;set;}=1.0;


    public Ant(CustomNode start){
        Start=start;
    }

    public void ChooseNode(CustomGraph graph, Dictionary<CustomEdge,double> pheromones){
        //проверка на начало алгоритма
        if (!Visited.Any()){
            Visited.Add(Start);
        }


        //взятие соседей
        var all_edges=Visited.Last().Neighbours.ToList();
        var edges=all_edges;

        //фильтрация соседей от посещённыых
        foreach(var Node in Visited){
            edges=edges.Where(x=> Node.Name!=x.Finish.Name).ToList();
        }
    
        if(edges.Count()==0){
            IsMoving=false;
            //проверим может ли прийти в начало из текущего нода
            var way_to_start=all_edges.Where(x=>x.Finish.Name==Start.Name);
            if (way_to_start.Count()!=0){
                WayLength+=way_to_start.First().Length;
                Visited.Add(way_to_start.First().Finish);
            }
            return;
        }



        List<double> possibility=new List<double>();

        double all_possibility=0;
        //пересчет привлекательности рёбер
        foreach (var edge in edges){
            double pher=0.0001;
            // пахнет ли феромоном
            if (pheromones.ContainsKey(edge)){
                pher=pheromones[edge];
            }
            var p=Math.Pow(pher,alpha)*Math.Pow(1.0/edge.Length,beta);
            possibility.Add(p);
            all_possibility+=p;
        }

        double toLow=0.0;
        //считаем привлекательность для каждого ребра
        for(int index=0; index< possibility.Count;index++){

            possibility[index]=possibility[index]/all_possibility;
            // if(possibility[index]<=0.1){
            //     toLow+=0.1-possibility[index];
            //     possibility[index]=0.1;
            // }
        }

        for(int index=1; index< possibility.Count;index++){
            
            possibility[index]+=possibility[index-1];

        }
        possibility[possibility.Count-1]-=toLow;


        //случайно выбираем ребро
        var randomnes=new Random();
        double rand_value=randomnes.NextDouble();
        //ищем нужный индекс
        int selected_index=possibility.Count-1;
        while (rand_value<possibility[selected_index] && selected_index!=0){
            selected_index--;
        }

        //выбираем нужный путь и добавляем его в маршрут
        Edges.Add(edges[selected_index]);
        WayLength+=edges[selected_index].Length;
        Visited.Add(edges[selected_index].Finish);

        
    }

    public AntPath GetPath(){
        
        return new AntPath(WayLength,Visited,Edges);
    }

}


public record AntPath(long Length, List<CustomNode> Nodes,List<CustomEdge>Edges);