using System.Numerics;
using System.IO;

namespace CuGraph.Tools.AntAlgoritmSolver;

using CuGraph.Tools.Ants;
using CuGraph.Tools.Ant.Blueprint;
using CuGraph.Graph;
using CuGraph.Edge;
using CuGraph.Node;


public class AntColonySolver{
    public CustomGraph Graph{get;set;}=new CustomGraph();
    public double P{get;set;}=0.5;

    private List<Ant> _Colony{get;set;}=new List<Ant>();
    private List<AntBlueprint> _ColonyData=new List<AntBlueprint>();
    private Dictionary<CustomEdge,double> _Pheromones {get;set;}=new Dictionary<CustomEdge, double>();  

    private void _CreateColony(){
        foreach(var antType in _ColonyData){
            for(int c=0; c<antType.population; c++){
                var ant=new Ant(Graph.Nodes.FirstOrDefault());
                ant.alpha=antType.alpha;
                ant.beta=antType.beta;
                _Colony.Add(ant);
            }
        }
    }

    public AntColonySolver(CustomGraph graph, List<AntBlueprint> ants,double p=0.5){
        Graph=graph;
        _ColonyData=ants;
        P=P;
    }

    public AntPath Solve(){

        //создаём ограничения
        int moves_without_change=100;
        int nodes_count=Graph.Nodes.Count()+1;
        int cur_move=0;
        AntPath ret=new AntPath(long.MaxValue,new List<Node.CustomNode>(),new List<CustomEdge>());


        long cur_it=0;
        
        while(cur_move!=moves_without_change){
            List<Ant> passedAnts=new List<Ant>();
            cur_it++;
            
            Console.WriteLine(string.Format("{0} {1}",cur_it,ret.Length));
            //sw.WriteLine(string.Format("{0} {1}",cur_it,ret.Length));
            //создаём муравьев
            _CreateColony();
            cur_move++;
            int antName=-1;
            foreach(var ant in _Colony){
                
                antName++;
                //бегаем пока муравей может двигаться
                while(ant.IsMoving){
                    ant.ChooseNode(Graph,_Pheromones);
                }
                
                //получаем путь
                var path=ant.GetPath();
                //если путь включает в себя полную проходку по всему графу - работаем
                if (path.Nodes.Count()==nodes_count){
                    passedAnts.Add(ant);
                    //логи
                    StreamWriter sw = new StreamWriter("./logs.txt",append:true);
                    sw.WriteLine(string.Format("{0} {1} {2}",antName,cur_it,path.Length));
                    sw.Close();
                    
                    if(path.Length<ret.Length){
                        ret=path;
                        cur_move=0;
                        Console.WriteLine("path changed");
                        Console.WriteLine(ret.Length);


                    }



                }
                else{
                    StreamWriter sw = new StreamWriter("./logs.txt",append:true);
                    sw.WriteLine(string.Format("{0} {1} {2}",antName,cur_it,long.MaxValue));
                    sw.Close();
                }

            }
            _Colony=[];
            //испарение феромонов
            foreach(var pherLine in _Pheromones){
                _Pheromones[pherLine.Key]=(1-P)*pherLine.Value;
            }



            //обновление феромонов
            foreach(var cur_ant in passedAnts){

                foreach(var edge in cur_ant.GetPath().Edges){
                    if (!_Pheromones.ContainsKey(edge)){
                        _Pheromones.Add(edge,0.0001);
                    }
                    _Pheromones[edge]+=1/cur_ant.GetPath().Length;
                }
            }
            passedAnts=new List<Ant>();
        }

        return ret;
    }

}
