using System.Numerics;

namespace CuGraph.Tools.AntAlgoritmSolver;

using CuGraph.Tools.Ants;
using CuGraph.Graph;
using CuGraph.Edge;

public class AntColonySolver{
    public CustomGraph Graph{get;set;}=new CustomGraph();
    public double Alpha{get;set;}=0.0;
    public double Beta{get;set;}=0.0;
    public double P{get;set;}=0.0;
    private List<Ant> _Colony{get;set;}=new List<Ant>();
    private Dictionary<CustomEdge,double> _Pheromones {get;set;}=new Dictionary<CustomEdge, double>();  

    private void _CreateColony(){
        foreach(var node in Graph.Nodes){
            var ant=new Ant(node);
            _Colony.Add(ant);
        }
    }

    public AntColonySolver(CustomGraph graph, double alpha=1.0, double beta=1.0,double p=0.5){
        Graph=graph;
        Alpha=alpha;
        Beta=beta;
        P=p;
    }

    public AntPath Solve(){

        //создаём ограничения
        int moves_without_change=100;
        int nodes_count=Graph.Nodes.Count()+1;
        int cur_move=0;
        AntPath ret=new AntPath(int.MaxValue,new List<Node.CustomNode>(),new List<CustomEdge>());

        while(cur_move!=moves_without_change){
            //создаём муравьев
            _CreateColony();
            cur_move++;
            foreach(var ant in _Colony){
                //бегаем пока муравей может двигаться
                while(ant.IsMoving){
                    ant.ChooseNode(Graph,_Pheromones,Alpha,Beta);
                }

                //получаем путь
                var path=ant.GetPath();
                //если путь включает в себя полную проходку по всему графу - работаем
                if (path.Nodes.Count()==nodes_count){
                    //если путь короче кратчайшего обновляем кратчайший и зануляем счетчик для работы без изменений
                    if(path.Length<ret.Length){
                        ret=path;
                        cur_move=0;
                        // Console.WriteLine("path changed");
                        // foreach(var node in path.Nodes){
                        //     Console.WriteLine(node.Name);
                        // }

                    }

                    foreach(var edge in path.Edges){
                        if (!_Pheromones.ContainsKey(edge)){
                            _Pheromones.Add(edge,0.00001);
                        }
                        _Pheromones[edge]=(1-P)*_Pheromones[edge]+1/edge.Length;
                    }

                }

            }
        }

        return ret;
    }

}
