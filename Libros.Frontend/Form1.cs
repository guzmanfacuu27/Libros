using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Libros.Backend;   // para poder usar las clases del Backend

namespace Libros.Frontend
{
    public partial class Form1 : Form
    {
        // Acá guardo los libros mientras el programa está abierto
        private List<Libro> listaLibros = new List<Libro>();

        // Con este objeto guardo y cargo del archivo
        private LibroRepositorio repositorio = new LibroRepositorio();

        public Form1()
        {
            InitializeComponent();

            // Conecto los botones con sus métodos desde el código
            btnAgregar.Click += btnAgregar_Click;
            btnEliminar.Click += btnEliminar_Click;
            btnModificar.Click += btnModificar_Click;

            // Apenas abre el programa cargo los libros que estaban guardados
            listaLibros = repositorio.Cargar();
            ActualizarLista();
        }

        // Borra lo que hay en la lista visual y la vuelve a llenar con los libros actuales
        private void ActualizarLista()
        {
            lstLibros.Items.Clear();

            foreach (Libro l in listaLibros)
            {
                lstLibros.Items.Add(l.ToString());
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Si falta algún dato aviso y corto acá con return
            if (txtTitulo.Text == "" || txtAutor.Text == "" || txtAnio.Text == "")
            {
                MessageBox.Show("Por favor completá todos los campos.");
                return;
            }

            // Compruebo que el año sea un número. TryParse devuelve false si escribieron letras
            int anio;
            if (!int.TryParse(txtAnio.Text, out anio))
            {
                MessageBox.Show("El año debe ser un número.");
                return;
            }

            // Armo el libro con lo que se escribió en las cajas
            Libro nuevoLibro = new Libro(txtTitulo.Text, txtAutor.Text, txtAnio.Text);

            listaLibros.Add(nuevoLibro);
            repositorio.Guardar(listaLibros);   // lo guardo enseguida para no perderlo si se cierra el programa
            ActualizarLista();

            // Dejo las cajas vacías para cargar otro
            txtTitulo.Text = "";
            txtAutor.Text = "";
            txtAnio.Text = "";
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Si no hay nada seleccionado, SelectedIndex vale -1
            if (lstLibros.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor seleccioná un libro de la lista.");
                return;
            }

            // El orden de la lista visual es el mismo que el de listaLibros,
            // así que con la posición puedo borrar el libro correcto
            int indice = lstLibros.SelectedIndex;
            listaLibros.RemoveAt(indice);
            repositorio.Guardar(listaLibros);
            ActualizarLista();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (lstLibros.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor seleccioná un libro de la lista.");
                return;
            }

            int indice = lstLibros.SelectedIndex;

            // Paso los datos del libro a las cajas para poder editarlos
            txtTitulo.Text = listaLibros[indice].Titulo;
            txtAutor.Text = listaLibros[indice].Autor;
            txtAnio.Text = listaLibros[indice].Anio;

            // Saco el libro viejo, cuando el usuario toque Agregar se guarda el nuevo con los cambios
            listaLibros.RemoveAt(indice);
            repositorio.Guardar(listaLibros);
            ActualizarLista();

            MessageBox.Show("Modificá los datos y presioná Agregar para guardar.");
        }
    }
}