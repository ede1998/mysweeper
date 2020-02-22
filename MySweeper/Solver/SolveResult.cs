namespace MySweeper.Solver
{
    public enum SolveResult
    {
        /// <summary>
        /// The solver could not do anything.
        /// </summary>
        NothingToDo,
        /// <summary>
        /// The solver finished its work and rerunning it will not do anything.
        /// </summary>
        Done,
        /// <summary>
        /// The solver finished its work and rerunning it might solve the game further.
        /// </summary>
        Rerun
    }
}
