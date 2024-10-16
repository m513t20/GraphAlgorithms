using CuGraph.Node;

namespace CuGraph.Edge;


public class CustomEdge{

    public CustomNode Finish {get;set;}=new CustomNode();
    public int Length {get;set;}=new int();



    public CustomEdge (CustomNode finish,int length){

        Finish=finish;
        Length=length;

    }


}