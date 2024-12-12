using System.Numerics;
using System.IO;

namespace CuGraph.Tools.AntAlgoritmSolver;

using CuGraph.Tools.Ants;
using CuGraph.Graph;
using CuGraph.Edge;
using CuGraph.Node;

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
        int moves_without_change=1000;
        int nodes_count=Graph.Nodes.Count()+1;
        int cur_move=0;
        AntPath ret=new AntPath(long.MaxValue,new List<Node.CustomNode>(),new List<CustomEdge>());

        StreamWriter sw = new StreamWriter("./logs.txt");
        long cur_it=0;

        while(cur_move!=moves_without_change){

            cur_it++;
            
            Console.WriteLine(string.Format("{0} {1}",cur_it,ret.Length));
            //sw.WriteLine(string.Format("{0} {1}",cur_it,ret.Length));
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
                sw.WriteLine(string.Format("{0} {1} {2}",ant.Start.Name,cur_it,ret.Length));
                if (path.Nodes.Count()==nodes_count){
                    
                    //Console.WriteLine("yey");
                    //если путь короче кратчайшего обновляем кратчайший и зануляем счетчик для работы без изменений
                    if(path.Length<ret.Length){
                        ret=path;
                        cur_move=0;
                        Console.WriteLine("path changed");
                        Console.WriteLine(ret.Length);
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
        sw.Close();
        return ret;
    }

}
