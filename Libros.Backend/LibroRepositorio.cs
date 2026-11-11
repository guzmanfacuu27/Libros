using System.Collections.Generic;
using System.IO;

namespace Libros.Backend
{
    // Esta clase se encarga solamente de guardar y leer los libros del archivo
    public class LibroRepositorio
    {
        // El archivo se crea solo en la carpeta del programa la primera vez que guardo
        private string rutaArchivo = "libros.txt";

        public void Guardar(List<Libro> libros)
        {
            List<string> lineas = new List<string>();

            // Paso cada libro a una línea de texto con los datos separados por ;
            // Usé ; en vez de coma por si algún título tiene comas
            foreach (Libro l in libros)
            {
                lineas.Add($"{l.Titulo};{l.Autor};{l.Anio}");
            }

            // Escribe todo el archivo de nuevo con la lista actual
            File.WriteAllLines(rutaArchivo, lineas);
        }

        public List<Libro> Cargar()
        {
            List<Libro> libros = new List<Libro>();

            // La primera vez el archivo todavía no existe, así que devuelvo la lista vacía
            if (!File.Exists(rutaArchivo)) return libros;

            string[] lineas = File.ReadAllLines(rutaArchivo);

            foreach (string linea in lineas)
            {
                // Corto la línea en pedazos usando el ;
                string[] partes = linea.Split(';');

                // Me fijo que la línea tenga los 3 datos, si no, la ignoro
                if (partes.Length == 3)
                {
                    libros.Add(new Libro(partes[0], partes[1], partes[2]));
                }
            }

            return libros;
        }
    }
}