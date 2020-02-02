namespace MySweeper.InputOutput.Terminal
{
    public class Terminal
    {
        public TerminalPrinter TerminalPrinter { get; set; }
        public TerminalReader TerminalReader { get; set; }

        public Terminal()
        {
            this.TerminalPrinter = new TerminalPrinter();
            this.TerminalReader = new TerminalReader();
        }
    }
}