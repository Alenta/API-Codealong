
internal class Program
{
    private static void Main(string[] args)
    {
        
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddSingleton<List<Movie>>(); //Register the list with the dependency injection from the Movie Class below
        var app = builder.Build();


        //READ:Get all Movies
        app.MapGet("/Movies", (List<Movie> movies) => movies);

        //CREATE: Create a Movie
        app.MapPost("/Movies", (Movie? movie, List<Movie> movies) => 
        { 
            if(movie == null){
                return Results.BadRequest();
            }
            
            movies.Add(movie);

            return Results.Created();
        });


        //DELETE: Deletea movie by ID
        app.MapDelete("/Movies/{id}", (int id) => $"Delete movie with id: {id}");

        //UPDATE:Update a Movie by ID
        app.MapPut("/Movies/{id}", (int id) => $"Update Movie with ID: {id}");
        
        app.MapGet("/health", () => "STATUS: OK");

        app.Run();
    }
}

class Movie
{
    public string Title {get; set;}
    public Movie(string newTitle)
    {
        Title = newTitle;
    }
    
}