
namespace MiniMax.Conncet4.Process;

using MiniMax.Conncet4.Game;
using MiniMax.Opponnents.Conncet4;



public class GameProcess{
    private const string TOKENS = "ox";

    private int _movePlayer(){
        int col = Convert.ToInt32(Console.ReadLine());
        return col;
    }

    public GameProcess(bool PlayersTurn=true){
        var board=new Conncet4Game();
        Console.WriteLine(board.ToString());
        bool moved=false;
        while(board.GetMoves().Count>0 ){
            if (PlayersTurn){
                moved=board.MakeMove(_movePlayer(),TOKENS[1]);
                Console.WriteLine(board.ToString());
            }
            else{
                ComputerOpponent ai=new ComputerOpponent(board,8);
                int move=ai.AlphaBeta();
                //Console.WriteLine(ai.Score(board,true));

                // moved=board.MakeMove(_movePlayer(),TOKENS[0]);
                // Console.WriteLine(board.ToString());

                Console.WriteLine($"move: {move}");
                moved=board.MakeMove(move,TOKENS[0]);
                Console.WriteLine(board.ToString());

                if(ai.game_over(board)){
                    Console.WriteLine("===Game ended===");
                    break;
                }
            }

            if(moved){
                moved=false;
                PlayersTurn=!PlayersTurn;
            }


        }

        if( board.GetMoves().Count>0 && !PlayersTurn){
            Console.WriteLine("AI has won!!!");
        }
        else if ( board.GetMoves().Count>0){
            Console.WriteLine("YOU won!!");
        }
        else{
            Console.WriteLine("tie");
        }

    }
}