namespace Libros.Backend
{
    // Esta clase es el molde de un libro
    public class Libro
    {
        // Los datos que tiene cada libro
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Anio { get; set; }   // le puse Anio sin ñ para no tener problemas con los caracteres

        // El constructor me obliga a pasar los 3 datos cuando creo un libro
        public Libro(string titulo, string autor, string anio)
        {
            Titulo = titulo;
            Autor = autor;
            Anio = anio;
        }

        // Esto define cómo se ve el libro cuando lo muestro en la lista
        public override string ToString()
        {
            return $"{Titulo} | {Autor} | {Anio}";
        }
    }
}