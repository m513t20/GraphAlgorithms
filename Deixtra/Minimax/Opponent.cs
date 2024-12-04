namespace MiniMax.Opponnents.Conncet4;

using MiniMax.Conncet4.Game;

public class ComputerOpponent{

    private Conncet4Game _board=new Conncet4Game();
    private int _depth=0;

    public ComputerOpponent(Conncet4Game i_board, int depth=7){
        _board=new Conncet4Game(i_board);
        _depth=depth;
    }

    private bool _has_row(Conncet4Game i_board,int row, int col,int length){
        int count=0;
        int space=0;
        for (int cur_col=col; cur_col<i_board.Board.GetLength(1); cur_col++){
            if (i_board.Board[row,col]==i_board.Board[row,cur_col]){
                count++;
            }
            else{
                // if(i_board.Board[row,cur_col]==' '){
                // space++;
                // }
                // else{
                //     break;
                // }
                break;

            }
        }
        return count>=length;
    }


    private bool _has_col(Conncet4Game i_board,int row, int col,int length){
        int count=0;
        int space=0;
        for (int cur_row=row; cur_row<6; cur_row++){
            if (i_board.Board[row,col]==i_board.Board[cur_row,col]){
                count++;
            }
            else{
                // if(i_board.Board[cur_row,col]==' '){
                //     space++;
                // }
                // else{
                //     break;
                // }
                break;
            }
        }
        return count>=length;
    }

    private bool _has_diag_right_up(Conncet4Game i_board,int row, int col,int length){
        int count=0;
        int space=0;
        int cur_col=col;
        for (int cur_row=row; cur_row<i_board.Board.GetLength(0); cur_row++){

            if (i_board.Board[row,col]==i_board.Board[cur_row,cur_col]){
                count++;
            }
            else{
                // if(i_board.Board[cur_row,cur_col]==' '){
                //     space++;
                // }
                // else{
                //     break;
                // }
                break;
            }
            cur_col++;
            if(cur_col>=i_board.Board.GetLength(1))
                break;
        }
        return count>=length;
    }

    private bool _has_diag_left_down(Conncet4Game i_board,int row, int col,int length){
        int count=0;
        int space=0;
        int cur_col=col;
        for (int cur_row=row; cur_row>=0; cur_row--){

            if (i_board.Board[row,col]==i_board.Board[cur_row,cur_col]){
                count++;
            }
            else{
                // if(i_board.Board[cur_row,cur_col]==' '){
                //     space++;
                // }
                // else{
                //     break;
                // }
                break;
            }
            cur_col++;
            if(cur_col>=i_board.Board.GetLength(1))
                break;
            }
        return count>=length;
    }



    private bool _has_row_trap(Conncet4Game i_board,int row, int col,int length){
        int count=0;
        bool br=false;
        int space=0;
        for (int cur_col=col; cur_col<i_board.Board.GetLength(1); cur_col++){
            if (i_board.Board[row,col]==i_board.Board[row,cur_col]){
                count++;
                br=false;
            }
            else{
                if(br || i_board.Board[row,cur_col]!=' '){
                    break;
                }
                else{
                    space++;
                    br=true;
                }

            }

        }
        return count>2 && space>=2;
    }


    private bool _game_over(Conncet4Game i_board){
        return _CountSequences(i_board,4,'o')>0 ||  _CountSequences(i_board,4,'x')>0;
    }

    private int _CountSequences(Conncet4Game i_board,int length,char token){
        int count=0;

        for (int row=0; row<_board.Board.GetLength(0); row++){
            for(int col=0; col<_board.Board.GetLength(1); col++){
                if( _board.Board[row,col]==token){
                    count+=Convert.ToInt32(_has_col(i_board,row,col,length));
                    count+=Convert.ToInt32(_has_row(i_board,row,col,length));
                    count+=Convert.ToInt32(_has_diag_left_down(i_board,row,col,length));
                    count+=Convert.ToInt32(_has_diag_right_up(i_board,row,col,length));
                    count+=Convert.ToInt32(_has_row_trap(i_board,row,col,length))*10;
                }
            }
        }



        return count;
    }




    public long Score(Conncet4Game i_board,bool aiTurn){
        long weWon=(_CountSequences(i_board,4,'o')%10);
        long  AiScore=weWon*10000+(_CountSequences(i_board,3,'o')%10)*100+(_CountSequences(i_board,2,'o')%10)*10;
        long weLost=(_CountSequences(i_board,4,'x')%10);
        long almost=_CountSequences(i_board,3,'x');
        //Console.WriteLine(weLost);
        long  PlayerScore=weLost*10000+(almost%10)*100+(_CountSequences(i_board,2,'x')%10)*10;


        if(weLost>0 || almost%10>0){
            return long.MinValue;
        }
        if (weWon>0){
            return long.MaxValue;
        }


        return  aiTurn? AiScore-PlayerScore : PlayerScore-AiScore;
    }

    public int AlphaBeta(){
        var moves=_board.GetMoves();

        long bestScore=long.MinValue;


        Random rand=new Random();
        int bestMove=rand.Next(0,moves.ToArray().Length);

        long alpha=long.MinValue;
        long beta=long.MaxValue;

        foreach(var move in moves){
            var boardTmp=new Conncet4Game(_board);
            boardTmp.MakeMove(move,'o');
            var score=_Beta(boardTmp,alpha,beta,_depth-1);
            if (bestScore<score){

                bestScore=score;
                bestMove=move;
            }

        }
        Console.WriteLine($"score: {bestScore} move:{bestMove} random:{rand.Next(0,moves.ToArray().Length)} {Convert.ToInt32(_has_col(_board,6,5,4))}");

        return bestMove;
    }

    private long _Beta(Conncet4Game board,long a,long b,int depth){
        var new_moves=board.GetMoves();

        if (depth==0 || new_moves.ToArray().Length==0 || _game_over(board))
            return Score(board,false);

        var beta=b;

        foreach(var move in new_moves){
            long minScore=long.MaxValue;

            if(a<beta){
                var new_board=new Conncet4Game(board);
                new_board.MakeMove(move,'x');
                minScore=_Alpha(new_board,a,beta,depth-1);

            }

            if(minScore<beta){
                beta=minScore;
            }
        }
        return beta;


    }

    private long _Alpha(Conncet4Game board,long a,long b,int depth){
        var new_moves=board.GetMoves();

        if (depth==0 || new_moves.ToArray().Length==0 || _game_over(board))
            return Score(board,true);

        long alpha=a;

        foreach(var move in new_moves){
            long maxScore=long.MinValue;
            if (alpha<b){
                var new_board=new Conncet4Game(board);
                new_board.MakeMove(move,'o');
                maxScore=_Beta(new_board,alpha,b,depth-1);
            }
            if (maxScore>alpha){
                alpha=maxScore;
            }
        }
        return alpha;
    }

}