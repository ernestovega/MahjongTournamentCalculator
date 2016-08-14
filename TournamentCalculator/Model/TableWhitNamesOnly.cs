namespace TournamentCalculator
{
    public class TableWithNamesOnly
    {
        public int roundId;
    public int tableId;
    public string player1Name;
    public string player2Name;
    public string player3Name;
    public string player4Name;

    public TableWithNamesOnly(int roundId, int tableId,
        string player1Name, string player2Name, string player3Name,
        string player4Name)
    {
        this.roundId = roundId;
        this.tableId = tableId;
        this.player1Name = player1Name;
        this.player2Name = player2Name;
        this.player3Name = player3Name;
        this.player4Name = player4Name;
    }

    public TableWithNamesOnly()
    {
    }
}
}