using MySweeper.Basic;
using MySweeper.InputOutput.Commands;
using System.Net;
using System;
using Xunit;
using System.Linq;

namespace MySweeper.Tests
{
    public class CommandTests
    {
        #region Properties

        private Coordinate Middle = new Coordinate(1,1);
        private Minefield Minefield;

        private const string HiddenMine = @"111
                                            1X1
                                            111";

        private const string MarkedMine = @"111
                                            1M1
                                            111";

        #endregion Properties
        
        #region Tests

        [Fact]
        public void FieldNotMarkedAsMine_ExecuteMarkAsMineCommand_FieldIsMarkedAsMine()
        {
            this.Minefield = TestMinefieldFactory.CreateMinefield(HiddenMine);
            var command = new MarkAsMineCommand(this.Minefield, this.Middle);

            command.Execute();

            Assert.True(Minefield.GetValue(Middle).BombMarked); // Field not marked as mine.
            Assert.Equal(1, Minefield.Count(x => x.BombMarked)); // Too many fields marked as mine.
        }

        [Fact]
        public void FieldMarkedAsMine_UndoExecuteMarkAsMineCommand_FieldIsNotMarkedAsMine()
        {
            this.Minefield = TestMinefieldFactory.CreateMinefield(MarkedMine);
            var command = new MarkAsMineCommand(this.Minefield, this.Middle);

            command.UndoExecution();

            Assert.Equal(0, Minefield.Count(x => x.BombMarked)); // Too many fields marked as mine.
        }

        #endregion Tests
    }
}