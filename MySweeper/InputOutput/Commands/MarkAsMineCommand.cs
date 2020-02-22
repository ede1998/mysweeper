using MySweeper.Basic;
using log4net;

namespace MySweeper.InputOutput.Commands
{
    public class MarkAsMineCommand : AbstractCommand
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(MarkAsMineCommand));
        public MarkAsMineCommand(Minefield minefield, Coordinate coordinate)
        : base(minefield, coordinate)
        {
        }

        public override void Execute()
        {
            Logger.DebugFormat("Marking field [{0}] as mine.", this.Coordinate);
            var field = this.Minefield.GetValue(this.Coordinate);
            field.BombMarked = true;
        }

        public override void UndoExecution()
        {
            Logger.DebugFormat("Undo marking field [{0}] as mine.", this.Coordinate);
            var field = this.Minefield.GetValue(this.Coordinate);
            field.BombMarked = false;
        }
    }
}