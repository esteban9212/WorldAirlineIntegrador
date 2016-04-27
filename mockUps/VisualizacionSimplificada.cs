using Mundo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mockUps
{
    public partial class VisualizacionSimplificada : Form
    {
        private Ventana ventana;
        private VisualizacionCompleta visualizacionViajero;
        private long paginas;
        private long totalPaginas;
        private long totalViajeros;
        private long paginaActual;
       
        private Thread hiloSiguienteSolucion;
        private Thread hiloAnteriorSolucion;
        private Thread hiloIrPaginaSolucion;
       
        private bool eficiente;
        private bool libre;
        private bool exploracionCompleta;

        public bool Eficiente
        {
            get
            {
                return eficiente;
            }

            set
            {
                eficiente = value;
            }
        }

        public bool Libre
        {
            get
            {
                return libre;
            }

            set
            {
                libre = value;
            }
        }

        public bool ExploracionCompleta
        {
            get
            {
                return exploracionCompleta;
            }

            set
            {
                exploracionCompleta = value;
            }
        }

        public VisualizacionSimplificada(Ventana x)
        {
            ventana = x;
            totalViajeros = ventana.TotalViajeros;
            paginaActual = 1;
            totalPaginas = ventana.totalPaginas();
            paginas = totalPaginas;


            labPaginasSolucion.Text = "Página  1";
            labTotalpaginaSolucion.Text = "De " + totalPaginas;


            InitializeComponent();
        }

        private void VisualizacionSimplificada_Load(object sender, EventArgs e)
        {
            
        
        }

        public void agregarFila(string id, string nombre, string apellido)
        {
            tablaSoluciones.Rows.Add(id,nombre,apellido);
            tablaSoluciones.Rows[tablaSoluciones.Rows.Count - 1].Selected = true;
            tablaSoluciones.CurrentCell = tablaSoluciones.Rows[tablaSoluciones.Rows.Count - 1].Cells[0];
            
        }

        public void buscarPagina()
        {
            if (InvokeRequired)
            {
                int pag = 0;
                try
                {
                    Invoke(new Action(() => pag = int.Parse(textPaginaSolucion.Text)));
                }
                catch
                {
                    MessageBox.Show("El formato de página es incorrecto,\n verifique que sea un número valido!");
                }
                int inicio = 0;
                int fin = 0;
                if (pag > 0 && pag <= paginas)
                {
                    Invoke(new Action(() => tablaSoluciones.Rows.Clear()));
                    paginaActual = pag;
                    inicio = (pag - 1) * ventana.Mundo.CANTIDAD_LOTE;
                    fin = inicio + ventana.Mundo.CANTIDAD_LOTE - 1;

                    Invoke(new Action(() => labPorcentajeSolucion.Visible = true)
                        );
                    Invoke(new Action(() => progressBarSolucion.Visible = true)
                        );
                    Invoke(new Action(() => butPlaySolucion.Visible = true)
                        );
                    Invoke(new Action(() => butPauseSolucion.Visible = true)
                        );
                    Invoke(new Action(() => butStopSolucion.Visible = true)
                         );
                    Invoke(new Action(()
                      => progressBarSolucion.Maximum = ventana.Mundo.CANTIDAD_LOTE
                       ));
                    Invoke(new Action(()
                       => progressBarSolucion.Value = 50
                        ));
                    Invoke(new Action(()
                       => labPorcentajeSolucion.Text = "5%"
                        ));
                    ventana.Mundo.cargarViajeros(ventana.Mundo.RutaViajeros, inicio, fin);

                    Invoke(new Action(() => tablaSoluciones.Rows.Clear()));

                    var viajeros = ventana.Mundo.Viajeros.Values;

                    foreach (Viajero viajero in viajeros)
                    {

                        string id = viajero.Id;
                        string nombre = viajero.Nombre;
                        string apellido = viajero.Apellido;


                        List<Arista<Ciudad>> lista = null;
                        if (eficiente)
                        {
                            lista = viajero.itinerarioEficiente();
                            eficiente = true;
                            exploracionCompleta = false;
                            libre = false;
                        }
                        if (exploracionCompleta)
                        {
                            lista = viajero.itinerarioExploracionCompleta();
                            eficiente = false;
                            exploracionCompleta = true;
                            libre = false;
                        }
                        if (libre)
                        {
                            lista = viajero.itinerarioLibre();
                            eficiente = false;
                            exploracionCompleta = false;
                            libre = true;
                        }




                        Invoke(new Action(()
                                    => tablaSoluciones.Rows.Add(id, nombre, apellido)
                                     ));

                     
                        long porcen = 0;

                        Invoke(new Action(()
                        => progressBarSolucion.Increment(1)
                         ));

                        Invoke(new Action(()
                       => porcen = (progressBarSolucion.Value * 100) / ventana.Mundo.CANTIDAD_LOTE
                       ));

                        Invoke(new Action(()
                        => labPorcentajeSolucion.Text = porcen + " %"
                       ));
                    }

                    Invoke(new Action(()
                       => progressBarSolucion.Value = ventana.Mundo.CANTIDAD_LOTE
                        ));
                    Invoke(new Action(()
                                => labPorcentajeSolucion.Text = 100 + "%"
                                 ));
                    Invoke(new Action(()
                     => labPaginasSolucion.Text = "Pagina " + paginaActual
                     ));

                    //MessageBox.Show("Completado!!");
                    Invoke(new Action(()
                               => progressBarSolucion.Value = 0
                                ));
                    Invoke(new Action(()
                                => labPorcentajeSolucion.Text = 0 + "%"
                                 ));
                    Invoke(new Action(()
                                => butAnteriorSolucion.Enabled = true
                                 ));
                    Invoke(new Action(()
                                => butSiguienteSolucion.Enabled = true
                                 ));
                    Invoke(new Action(()
                               => butPaginaSolucion.Enabled = true
                                ));
                    Invoke(new Action(() => labPorcentajeSolucion.Visible = false)
                        );
                    Invoke(new Action(() => progressBarSolucion.Visible = false)
                        );
                    Invoke(new Action(() => butPlaySolucion.Visible = false)
                        );
                    Invoke(new Action(() => butPauseSolucion.Visible = false)
                        );
                    Invoke(new Action(() => butStopSolucion.Visible = false)
                         );
                    Invoke(new Action(()
                  => labPaginasSolucion.Visible = true
                 ));
                    Invoke(new Action(()
                    => labTotalpaginaSolucion.Visible = true
                   ));
                }
                else
                {
                    MessageBox.Show("Por favor digite una pagina correcta");
                    Invoke(new Action(()
                               => butAnteriorSolucion.Enabled = true
                                ));
                    Invoke(new Action(()
                                => butSiguienteSolucion.Enabled = true
                                 ));
                    Invoke(new Action(()
                               => butPaginaSolucion.Enabled = true
                                ));
                }
            }

        
    }


        private void butPaginaSolucion_Click(object sender, EventArgs e)
        {
            try
            {
                int x = Int32.Parse(textPaginaSolucion.Text);
                if (x != paginaActual)
                {
                    hiloIrPaginaSolucion = new Thread(buscarPagina);
                    butAnteriorSolucion.Enabled = false;
                    butSiguienteSolucion.Enabled = false;
                    butPaginaSolucion.Enabled = false;
                    hiloIrPaginaSolucion.Start();
                }
            }
            catch
            {
                MessageBox.Show("digite una pagina correcta");
            }
        }

        private void siguiente()
        {
            if (InvokeRequired)
            {
                int pag = Convert.ToInt32(paginaActual + 1);

                int inicio = 0;
                int fin = 0;
                if (pag > 0 && pag <= paginas)
                {
                    Invoke(new Action(() => tablaSoluciones.Rows.Clear()));
                    paginaActual = pag;
                    inicio = (pag - 1) * ventana.Mundo.CANTIDAD_LOTE;
                    fin = inicio + ventana.Mundo.CANTIDAD_LOTE - 1;

                    Invoke(new Action(() => labPorcentajeSolucion.Visible = true)
                        );
                    Invoke(new Action(() => progressBarSolucion.Visible = true)
                        );
                    Invoke(new Action(() => butPlaySolucion.Visible = true)
                        );
                    Invoke(new Action(() => butPauseSolucion.Visible = true)
                        );
                    Invoke(new Action(() => butStopSolucion.Visible = true)
                         );
                    Invoke(new Action(()
                      => progressBarSolucion.Maximum = ventana.Mundo.CANTIDAD_LOTE
                       ));
                    Invoke(new Action(()
                       => progressBarSolucion.Value = 50
                        ));
                    Invoke(new Action(()
                       => labPorcentajeSolucion.Text = "5%"
                        ));
                    ventana.Mundo.cargarViajeros(ventana.Mundo.RutaViajeros, inicio, fin);



                    var viajeros = ventana.Mundo.Viajeros.Values;


                    foreach (Viajero viajero in viajeros)
                    {

                        string id = viajero.Id;
                        string nombre = viajero.Nombre;
                        string apellido = viajero.Apellido;

                        List<Arista<Ciudad>> lista = null;
                        if (eficiente)
                        {
                            lista = viajero.itinerarioEficiente();
                            eficiente = true;
                            exploracionCompleta = false;
                            libre = false;

                        }
                        if (exploracionCompleta)
                        {
                            lista = viajero.itinerarioExploracionCompleta();
                            eficiente = false;
                            exploracionCompleta = true;
                            libre = false;

                        }
                        if (libre)
                        {
                            lista = viajero.itinerarioLibre();
                            eficiente = false;
                            exploracionCompleta = false;
                            libre = true;

                        }


                        Invoke(new Action(()
                                    => tablaSoluciones.Rows.Add(id, nombre, apellido)
                                     ));

                        long porcen = 0;

                        Invoke(new Action(()
                        => progressBarSolucion.Increment(1)
                         ));

                        Invoke(new Action(()
                       => porcen = (progressBarSolucion.Value * 100) / ventana.Mundo.CANTIDAD_LOTE
                       ));

                        Invoke(new Action(()
                        => labPorcentajeSolucion.Text = porcen + " %"
                       ));
                        Invoke(new Action(()
                      => labPaginasSolucion.Text = "Pagina " + paginaActual
                      ));
                    }

                    Invoke(new Action(()
                       => progressBarSolucion.Value = ventana.Mundo.CANTIDAD_LOTE
                        ));
                    Invoke(new Action(()
                                => labPorcentajeSolucion.Text = 100 + "%"
                                 ));

                    //MessageBox.Show("Completado!!");
                    Invoke(new Action(()
                               => progressBarSolucion.Value = 0
                                ));
                    Invoke(new Action(()
                                => labPorcentajeSolucion.Text = 0 + "%"
                                 ));
                    Invoke(new Action(()
                                => butAnteriorSolucion.Enabled = true
                                 ));
                    Invoke(new Action(()
                                => butSiguienteSolucion.Enabled = true
                                 ));
                    Invoke(new Action(()
                               => butPaginaSolucion.Enabled = true
                                ));
                    Invoke(new Action(() => labPorcentajeSolucion.Visible = false)
                        );
                    Invoke(new Action(() => progressBarSolucion.Visible = false)
                        );
                    Invoke(new Action(() => butPlaySolucion.Visible = false)
                        );
                    Invoke(new Action(() => butPauseSolucion.Visible = false)
                        );
                    Invoke(new Action(() => butStopSolucion.Visible = false)
                         );
                    Invoke(new Action(()
                  => labPaginasSolucion.Visible = true
                 ));
                    Invoke(new Action(()
                    => labTotalpaginaSolucion.Visible = true
                   ));
                }
                else
                {
                    MessageBox.Show("Ha llegado a la ultima pagina");
                    Invoke(new Action(()
                                => butAnteriorSolucion.Enabled = true
                                 ));

                    Invoke(new Action(()
                               => butPaginaSolucion.Enabled = true
                                ));

                }
            }


        }
        private void butSiguienteSolucion_Click(object sender, EventArgs e)
        {
            hiloSiguienteSolucion = new Thread(siguiente);
            textPaginaSolucion.Text = "";
            butAnteriorSolucion.Enabled = false;
            butSiguienteSolucion.Enabled = false;
            hiloSiguienteSolucion.Start();
        }

        public void anterior()
        {
            if (InvokeRequired)
            {
                int pag = Convert.ToInt32(paginaActual - 1);

                int inicio = 0;
                int fin = 0;
                if (pag > 0 && pag <= paginas)
                {
                    Invoke(new Action(() => tablaSoluciones.Rows.Clear()));
                    paginaActual = pag;
                    inicio = (pag - 1) * ventana.Mundo.CANTIDAD_LOTE;
                    fin = inicio + ventana.Mundo.CANTIDAD_LOTE - 1;

                    Invoke(new Action(() => labPorcentajeSolucion.Visible = true)
                        );
                    Invoke(new Action(() => progressBarSolucion.Visible = true)
                        );
                    Invoke(new Action(() => butPlaySolucion.Visible = true)
                        );
                    Invoke(new Action(() => butPauseSolucion.Visible = true)
                        );
                    Invoke(new Action(() => butStopSolucion.Visible = true)
                         );
                    Invoke(new Action(()
                      => progressBarSolucion.Maximum = ventana.Mundo.CANTIDAD_LOTE
                       ));
                    Invoke(new Action(()
                       => progressBarSolucion.Value = 50
                        ));
                    Invoke(new Action(()
                       => labPorcentajeSolucion.Text = "5%"
                        ));
                    ventana.Mundo.cargarViajeros(ventana.Mundo.RutaViajeros, inicio, fin);



                    var viajeros = ventana.Mundo.Viajeros.Values;


                    foreach (Viajero viajero in viajeros)
                    {

                        string id = viajero.Id;
                        string nombre = viajero.Nombre;
                        string apellido = viajero.Apellido;

                        List<Arista<Ciudad>> lista = null;
                        if (eficiente)
                        {
                            lista = viajero.itinerarioEficiente();
                            eficiente = true;
                            exploracionCompleta = false;
                            libre = false;

                        }
                        if (exploracionCompleta)
                        {
                            lista = viajero.itinerarioExploracionCompleta();
                            eficiente = false;
                            exploracionCompleta = true;
                            libre = false;

                        }
                        if (libre)
                        {
                            lista = viajero.itinerarioLibre();
                            eficiente = false;
                            exploracionCompleta = false;
                            libre = true;

                        }

                        Invoke(new Action(()
                                    => tablaSoluciones.Rows.Add(id, nombre, apellido)
                                     ));

                        long porcen = 0;

                        Invoke(new Action(()
                        => progressBarSolucion.Increment(1)
                         ));

                        Invoke(new Action(()
                       => porcen = (progressBarSolucion.Value * 100) / ventana.Mundo.CANTIDAD_LOTE
                       ));

                        Invoke(new Action(()
                        => labPorcentajeSolucion.Text = porcen + " %"
                       ));
                        Invoke(new Action(()
                      => labPaginasSolucion.Text = "Pagina " + paginaActual
                      ));
                    }

                    Invoke(new Action(()
                       => progressBarSolucion.Value = ventana.Mundo.CANTIDAD_LOTE
                        ));
                    Invoke(new Action(()
                                => labPorcentajeSolucion.Text = 100 + "%"
                                 ));

                    //MessageBox.Show("Completado!!");
                    Invoke(new Action(()
                               => progressBarSolucion.Value = 0
                                ));
                    Invoke(new Action(()
                                => labPorcentajeSolucion.Text = 0 + "%"
                                 ));
                    Invoke(new Action(()
                                => butAnteriorSolucion.Enabled = true
                                 ));
                    Invoke(new Action(()
                                => butSiguienteSolucion.Enabled = true
                                 ));
                    Invoke(new Action(()
                               => butPaginaSolucion.Enabled = true
                                ));
                    Invoke(new Action(() => labPorcentajeSolucion.Visible = false)
                        );
                    Invoke(new Action(() => progressBarSolucion.Visible = false)
                        );
                    Invoke(new Action(() => butPlaySolucion.Visible = false)
                        );
                    Invoke(new Action(() => butPauseSolucion.Visible = false)
                        );
                    Invoke(new Action(() => butStopSolucion.Visible = false)
                         );
                    Invoke(new Action(()
                  => labPaginasSolucion.Visible = true
                 ));
                    Invoke(new Action(()
                    => labTotalpaginaSolucion.Visible = true
                   ));
                }
                else
                {
                    MessageBox.Show("Ha llegado a la primera pagina");

                    Invoke(new Action(()
                                => butSiguienteSolucion.Enabled = true
                                 ));
                    Invoke(new Action(()
                               => butPaginaSolucion.Enabled = true
                                ));

                }
            }
        }

        private void butAnteriorSolucion_Click(object sender, EventArgs e)
        {
            hiloAnteriorSolucion = new Thread(anterior);
            textPaginaSolucion.Text = "";
            butSiguienteSolucion.Enabled = false;
            butAnteriorSolucion.Enabled = false;
            hiloAnteriorSolucion.Start();
        }

        private void tablaSoluciones_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int indice = e.RowIndex;
            if (indice >= 0)
            {
                DataGridViewRow fila = tablaSoluciones.Rows[indice];
                //string seleccion = tablaCiudades.SelectedRows.ToString();
                if (fila.Cells[0] != null)
                {
                    string id = fila.Cells[0].Value.ToString();
                    Viajero viajero = (Viajero)ventana.Mundo.Viajeros[id];

                    listBoxCiudades.Items.Clear();
                    listBoxSoluciones.Items.Clear();
                 
                    int i = 1;
                    
                    int j = 0;
                    List<Arista<Ciudad>> lista = null;
                    if (eficiente)
                    {
                        lista = viajero.itinerarioEficiente();
                        eficiente = true;
                        exploracionCompleta = false;
                        libre = false;

                    }
                    if (exploracionCompleta)
                    {
                        lista = viajero.itinerarioExploracionCompleta();
                        eficiente = false;
                        exploracionCompleta = true;
                        libre = false;

                    }
                    if (libre)
                    {
                        lista = viajero.itinerarioLibre();
                        eficiente = false;
                        exploracionCompleta = false;
                        libre = true;

                    }
                    foreach (Arista<Ciudad> arista in lista)
                    {
                        listBoxSoluciones.Items.Add(arista.Destino1.Nombre); 
                        j++;
                    }
                    foreach (Ciudad ciudad in viajero.Itinerario.Ciuadades.Values)
                    {
                        listBoxCiudades.Items.Add(i + ") " + ciudad.Nombre);
                        i++;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection filas = tablaSoluciones.SelectedRows;
            foreach (DataGridViewRow fila in filas)
            {
                DataGridViewCell x = fila.Cells[0];
              
                if (x != null)
                {
                    string id = fila.Cells[0].Value.ToString();
                    string nombreApellido = "";
                    string rutica = "";
                    string inicial = "";
                
                    Viajero viajero = (Viajero)ventana.Mundo.Viajeros[id];


                    nombreApellido = viajero.Nombre + " " + viajero.Apellido;

                    var ciudades = viajero.Itinerario.Ciuadades.Values;

                    int i = 0;
                    foreach (Ciudad esta in ciudades)
                    {
                        if (i == 0)
                        {
                            inicial = esta.Nombre;
                        }
                        rutica += esta.Nombre + "#";
                        i++;
                    }

                    List<Arista<Ciudad>> lista = null;
                   
                    if (eficiente)
                    {
                        lista = viajero.itinerarioEficiente();
                        eficiente = true;
                        exploracionCompleta = false;
                        libre = false;

                    }
                    if (exploracionCompleta)
                    {
                        lista = viajero.itinerarioExploracionCompleta();
                        eficiente = false;
                        exploracionCompleta = true;
                        libre = false;

                    }
                    if (libre)
                    {
                        lista = viajero.itinerarioLibre();
                        eficiente = false;
                        exploracionCompleta = false;
                        libre = true;

                    }
             
                    string solucionCadena = "";
                    foreach (Arista<Ciudad> arista in lista)
                    {
                        solucionCadena += arista.Destino1.Nombre + "#";
                    }
                    visualizacionViajero = new VisualizacionCompleta();
                    visualizacionViajero.asignarSolucion(lista);
                    visualizacionViajero.informacionViajero(id,nombreApellido, rutica,solucionCadena);
                    visualizacionViajero.dibujarSolucion(false);
                    visualizacionViajero.Show();

                }
                else
                {
                    MessageBox.Show("nullpointer");
                }
            }
        }
    }
}
