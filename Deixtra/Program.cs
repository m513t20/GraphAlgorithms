// See https://aka.ms/new-console-template for more information
using CuGraph.Graph;
using CuGraph.Node;
using CuGraph.Tools.AntAlgoritmSolver;
using CuGraph.Tools.DijcstraAlg;
using MiniMax.Conncet4.Process;


void TestDem(){

    CustomGraph gr_tst=new CustomGraph();
    var a=new CustomNode("A");
    var b=new CustomNode("B");
    var c=new CustomNode("C");
    var d=new CustomNode("D");
    var f=new CustomNode("F");
    var g=new CustomNode("G");

    gr_tst.AddNode(a);
    gr_tst.AddNode(b);
    gr_tst.AddNode(c);
    gr_tst.AddNode(d);
    gr_tst.AddNode(f);
    gr_tst.AddNode(g);

    gr_tst.AddEdge("A","B",3);
    gr_tst.AddEdge("A","F",1);
    gr_tst.AddEdge("B","A",3);
    gr_tst.AddEdge("B","G",3);
    gr_tst.AddEdge("B","C",8);
    gr_tst.AddEdge("C","B",3);
    gr_tst.AddEdge("C","G",1);
    gr_tst.AddEdge("C","D",1);
    gr_tst.AddEdge("D","C",8);
    gr_tst.AddEdge("D","F",1);
    gr_tst.AddEdge("F","A",3);
    gr_tst.AddEdge("F","D",3);

    gr_tst.AddEdge("G","A",3);
    gr_tst.AddEdge("G","B",3);
    gr_tst.AddEdge("G","C",3);
    gr_tst.AddEdge("G","D",5);
    gr_tst.AddEdge("G","F",4);


    var solver=new AntColonySolver(gr_tst);
    var path=solver.Solve();
    Console.WriteLine("answer:");
    Console.WriteLine(path.Length);
    foreach(var node in path.Nodes){
        Console.WriteLine(node.Name);
    }
}

void TestTxt(){
    var gr=new CustomGraph();
    gr.ReadFromFile("1000.txt");
    Console.WriteLine("Finished");

    var solver=new AntColonySolver(gr);
    var path=solver.Solve();
    Console.WriteLine("answer:");
    Console.WriteLine(path.Length);
    foreach(var node in path.Nodes){
        Console.WriteLine(node.Name);
    }
}

void TestDij(){
    CustomGraph gr_tst=new CustomGraph();
    var a=new CustomNode("A");
    var b=new CustomNode("B");
    var c=new CustomNode("C");
    var d=new CustomNode("D");
    var f=new CustomNode("F");
    var g=new CustomNode("G");

    gr_tst.AddNode(a);
    gr_tst.AddNode(b);
    gr_tst.AddNode(c);
    gr_tst.AddNode(d);
    gr_tst.AddNode(f);
    gr_tst.AddNode(g);


    gr_tst.AddEdge("A","G",50);
    gr_tst.AddEdge("A","B",10);
    gr_tst.AddEdge("A","C",20);
    gr_tst.AddEdge("B","C",10);
    gr_tst.AddEdge("C","B",10);
    gr_tst.AddEdge("B","D",15);
    gr_tst.AddEdge("C","F",25);
    gr_tst.AddEdge("D","F",5);
    gr_tst.AddEdge("F","D",5);
    gr_tst.AddEdge("D","G",30);
    gr_tst.AddEdge("F","G",15);

    var dij=new Dijcstra(gr_tst);
    var path=dij.FindShortestWay(a,g);
    Console.WriteLine(path.Length);

}

//дейкстра
//TestDij();

//Муравьи
// TestDem();
//TestTxt();
Console.WriteLine("First or second player 1/2");
int player = Convert.ToInt32(Console.ReadLine());

var tst=new GameProcess(player==1);