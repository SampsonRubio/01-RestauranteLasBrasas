﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace _01_RestauranteLasBrasas
{
    /// <summary>
    /// Lógica de interacción para WindowGestionarClientes.xaml
    /// </summary>
    public partial class WindowGestionarClientes : Window
    {
        private DataClasses1DataContext data;
        public WindowGestionarClientes()
        {
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["_01_RestauranteLasBrasas.Properties.Settings.BD_RestauranteLasBrasasConnectionString"].ConnectionString;

            data = new DataClasses1DataContext(connectionString);
            var cliente = from u in data.GetTable<Cliente>()
                          select new { u.IdCliente, u.Identidad, u.Nombre, u.Apellido, u.Direccion, u.Sexo, u.Telefono, };
           
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Cliente cli = new Cliente();
                cli.Identidad = txtIdentidadCliente.Text;
                cli.Nombre = txtNombreCliente.Text;
                cli.Apellido = txtApellidoCliente.Text;
                cli.Direccion = txtDireccionCliente.Text;
                cli.Telefono = txtTelefonoCliente.Text;
                cli.Sexo = cbSexoCliente.Text;
                

                data.Cliente.InsertOnSubmit(cli);
                data.SubmitChanges();
                //d.ItemsSource = data.Empleado;
                MessageBox.Show("REGISTRO GUARDADO CORRECTAMENTE");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (txtIdentidadCliente.Text != "")
            {
                var cliente = (from emp in data.Cliente
                                where emp.Identidad == txtIdentidadCliente.Text
                                select emp).First();
                //var empleado = data.Empleado.First(emp => emp.nombre.Equals(txtNombre.Text));
                if (cliente != null)
                {
                    var eliminar = from elim in data.Cliente
                                   where elim.Identidad.Equals(txtIdentidadCliente.Text)
                                   select elim;
                    foreach (var cliente1 in eliminar)
                    {
                        data.Cliente.DeleteOnSubmit(cliente1);
                    }
                    try
                    {
                        data.SubmitChanges();
                        MessageBox.Show("Registro eliminado con exito");
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.ToString());
                    }
                }
                else
                    MessageBox.Show("Para eliminar escriba un numero de identidad"); txtIdentidadCliente.Focus();
            }
            else
                MessageBox.Show("No existe registo con ese nombre");

        }
    }
}
