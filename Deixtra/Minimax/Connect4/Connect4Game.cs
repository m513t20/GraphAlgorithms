
namespace MiniMax.Conncet4.Game;



public class Conncet4Game{
    private const int WIDTH=7;
    private const int HEIGHT=6;

    public char[,] Board=new char[HEIGHT,WIDTH];

    public Conncet4Game(){
        for (int row=0; row<HEIGHT; row++){
            for(int col=0; col<WIDTH; col++){
                Board[row,col]=' ';
            }
        }


    }

    public Conncet4Game(Conncet4Game orig){
        for (int row=0; row<HEIGHT; row++){
            for(int col=0; col<WIDTH; col++){
                Board[row,col]=orig.Board[row,col];
            }
        }


    }

    private bool _ColumnIsFree(int col){
        if (col>=0 && col<WIDTH){
            return Board[0,col]==' ';
        }
        return false;

    }

    public List<int> GetMoves(){
        List<int> ans=new List<int>();
        for (int cur_col=0; cur_col<WIDTH; cur_col++){
            if(_ColumnIsFree(cur_col))
                ans.Add(cur_col);
        }
        return ans;
    }

    public bool MakeMove(int col, char token){
        if (_ColumnIsFree(col)){
            for (int row=HEIGHT-1; row>=0; row--){
                if (Board[row,col]==' '){
                    Board[row,col]=token;
                    return true;
                }
            }
        }
        return false;
    }


    public override string ToString()
    {
        string ans="";
        for (int row=0; row<HEIGHT; row++){
            ans+="|";
            for(int col=0; col<WIDTH; col++){
                ans+=Board[row,col].ToString()+"|";
            }
            ans+="\n";
        }


        ans+="|";
        for(int col=0; col<WIDTH; col++){
            ans+=col.ToString()+"|";
        }

        return ans;
    }
}