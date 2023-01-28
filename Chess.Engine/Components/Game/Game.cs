using Chess.Engine.Builders;
using Chess.Engine.Enums;
using Chess.Engine.Extensions;
using Chess.Engine.Resolvers;

namespace Chess.Engine.Components
{
    public class Game
    {
        private readonly MoveResolver _moveResolver;
        private readonly MoveCandidateResolver _moveCandidateResolver;
        private readonly ThreatResolver _threatResolver;
        private readonly GameEndingResolver _gameEndingResolver;
        private readonly GameStateResolver _gameStateResolver;

        public GameSetup GameSetup { get; set; }
        public IList<Player> Players { get; set; }
        public Player ActivePlayer => Players.FirstOrDefault(x => x.Color == ActiveTurn.Color);
        public Player PassivePlayer => Players.FirstOrDefault(x => x.Color != ActiveTurn.Color);
        public IList<Turn> Turns { get; set; }
        public IEnumerable<Turn> ExecutedTurns => Turns?.Where(x => x.IsExcecuted) ?? Enumerable.Empty<Turn>();
        public Turn LastExecutedTurn => ExecutedTurns.LastOrDefault();
        public Turn ActiveTurn => Turns.LastOrDefault();
        public ChessBoard ChessBoard { get; protected set; }
        public GameEnding GameEnding { get; set; }
        public bool HasEnded => GameEnding is not null;

        public Game()
        {
            _moveResolver = new MoveResolver();
            _moveCandidateResolver = new MoveCandidateResolver();
            _threatResolver = new ThreatResolver();
            _gameEndingResolver = new GameEndingResolver();
            _gameStateResolver = new GameStateResolver();
            Players = new List<Player>();
            Turns = new List<Turn>();
        }

        public void Initialize()
        {
            var chessBoardState = 
                LastExecutedTurn?.GameState.ChessBoardState ?? 
                GameSetup.InitialChessBoardState;
            ChessBoard = chessBoardState.ToChessBoard();
            ChessBoard.Initialize(this);

            foreach (var turn in Turns)
            {
                turn.Initialize(this);
            }

            foreach (var player in Players)
            {
                player.Initialize(this);
            }
        }

        public void Solve()
        {
            _moveCandidateResolver.Solve(ActivePlayer);
            _threatResolver.Solve(PassivePlayer);
            _moveResolver.Solve(ActivePlayer);
            _gameEndingResolver.Solve(this);
        }

        public void ExecuteMove(Move move)
        {
            var timeTo = DateTime.Now;

            if (move.Capture is not null)
                move.Piece.Capture(move.Capture.CaptureSquare.Piece);

            move.Piece.Move(move.ArrivalSquare);

            if (move.Promotion is not null)
                (move.Piece as Pawn).Promote(move.Promotion.Value);

            if (move.Castle is not null)
                move.Castle.Rook.Move(move.Castle.ArrivalSquare);

            move.Description = move.ToAlgebraicNotation(this);

            ActiveTurn.Move = move;
            _gameStateResolver.Solve(this);
            ActiveTurn.DateTimeTo = timeTo;
            InitializeNextTurn();
        }

        private void InitializeNextTurn()
        {
            var nextTurn = new Turn()
            {
                TurnClock = ActivePlayer.Color == Color.White ?
                    ActiveTurn.TurnClock : ActiveTurn.TurnClock + 1,
                Color = PassivePlayer.Color,
                DateTimeFrom = DateTime.Now
            };
            Turns.Add(nextTurn);
        }

        public void Resign(Color color)
        {
            var loser = Players.FirstOrDefault(x => x.Color == color);
            var winner = Players.FirstOrDefault(x => x.Color != color);

            GameEnding = new GameEndingBuilder()
                .New()
                .LossFor(loser)
                .WinFor(winner)
                .WithDetails("{Loser} have resigned")
                .Build();
        }

        public void DrawByAgreement()
        {
            GameEnding = new GameEndingBuilder()
                .New()
                .Draw()
                .WithDetails("Draw by agreement")
                .Build();
        }
    }
}
