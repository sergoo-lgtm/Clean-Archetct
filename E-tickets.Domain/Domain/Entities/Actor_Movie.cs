namespace eTickets.Models;

public class Actor_Movie
{
    private Actor_Movie() { }

    public Actor_Movie(int movieId, int actorId)
    {
        MovieId = movieId;
        ActorId = actorId;
    }

    public int MovieId { get; private set; }
    public Movie Movie { get; private set; }

    public int ActorId { get; private set; }
    public Actor Actor { get; private set; }
}