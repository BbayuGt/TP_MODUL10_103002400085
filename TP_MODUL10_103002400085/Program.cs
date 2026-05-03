using TP_MODUL10_103002400085;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

List<Film> films = new List<Film>
{
    new Film
    {
        Judul = "Inception",
        Sutradara = "Christopher Nolan",
        Tahun = "2010",
        Genre = "Sci-Fi",
        Rating = "9.0"
    },
    new Film
    {
        Judul = "Interstellar",
        Sutradara = "Christopher Nolan",
        Tahun = "2014",
        Genre = "Sci-Fi",
        Rating = "8.7"
    },
    new Film
    {
        Judul = "Parasite",
        Sutradara = "Bong Joon-ho",
        Tahun = "2019",
        Genre = "Thriller",
        Rating = "8.6"
    }
};

app.MapGet("/api/Film", () =>
    {
        return films;
    })
    .WithName("/api/Film");

app.MapPost("/api/Film", (Film newFilm) =>
{
    // validasi user
    if (newFilm.Genre == null) return -1;
    if (newFilm.Judul == null)  return -1;
    if (newFilm.Rating == null) return -1;
    if (newFilm.Sutradara == null) return -1;
    if (newFilm.Tahun == null) return -1;
    
    films.Add(newFilm);
    return films.Count; // return id film yang baru di post
}).WithName("Add /api/Film");

app.MapGet("/api/Film/{id}", (int id) =>
{
    if (id < 0 || id >= films.Count)
    {
        return null;
    } // Kalau id tidak valid, return null

    return films[id];
}).WithName("/api/Film/{id}");

app.MapDelete("/api/Film/{id}", (int id) =>
{
    if (id < 0 || id >= films.Count)
    {
        return -1;
    } // check, kalau id tidak valid, return -1 (invalid)
    films.RemoveAt(id);
    return films.Count; // kembalikan jumlah film setelah penghapusan
}).WithName("Delete /api/Film/{id}");

app.Run();