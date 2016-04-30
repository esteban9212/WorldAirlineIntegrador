using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using Mundo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Windows.Forms;


namespace mockUps
{
    public partial class Ventana : Form
    {

        private long paginas;
        private VisualizacionCompleta solucionParticular;
        private VisualizacionSimplificada solucionGeneral;
        private WorldAirline mundo;
        private bool tabla;      
        private long totalViajeros;
        private long totalCiudades;
        private Thread hiloCargaCiudades;
        private Thread hiloCargaViajeros;
        private Thread hilofiltroCiudades;
        private Thread hiloBuscarCiudades;
        private Thread hiloBuscarViajeros;
        private Thread hiloSiguiente;
        private Thread hiloAnterior;
        private Thread hiloIrPagina;  
        private Thread hiloVolverPaginaActual;
        private Thread hiloKruskal;
        private Thread hiloLibre;
        private Thread hiloExploracionCompleta;
        private long paginaActual;
        private bool cargoCiudades;
        private bool cargoViajeros;
        private int excedentePagina = 0;



        public Ventana()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            tabla = false;

            cargoCiudades = false;
            cargoViajeros = false;

            checkBoxVista.Text = "Ver Destinos" + "\n" +
                                  "con rutas" + "\n" +
                                  "y distancias";


            mapa.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            mapa.DragButton = MouseButtons.Left;

            mundo = new WorldAirline();

        }


        public long Paginas
        {
            get
            {
                return paginas;
            }

            set
            {
                paginas = value;
            }
        }
        public GMapControl Mapa
        {
            get
            {
                return mapa;
            }

            set
            {
                mapa = value;
            }
        }
        public long TotalViajeros
        {
            get
            {
                return totalViajeros;
            }

            set
            {
                totalViajeros = value;
            }
        }

        public long PaginaActual
        {
            get
            {
                return paginaActual;
            }

            set
            {
                paginaActual = value;
            }
        }

        public int ExcedentePagina
        {
            get
            {
                return excedentePagina;
            }

            set
            {
                excedentePagina = value;
            }
        }

        public WorldAirline Mundo
        {
            get
            {
                return mundo;
            }

            set
            {
                mundo = value;
            }
        }

        private void butBuscarCiudad_MouseEnter(object sender, EventArgs e)
        {
            ToolTip ToolTip = new ToolTip();
            ToolTip.SetToolTip(this.butBuscarCiudad, "Buscar ciudad");
          
        }

        private void butBuscarViajero_MouseEnter(object sender, EventArgs e)
        {
            ToolTip ToolTip = new ToolTip();
            ToolTip.SetToolTip(this.butBuscarViajero, "Buscar viajero");
        }


        private void butLimpiarCiudades_MouseEnter(object sender, EventArgs e)
        {
            ToolTip ToolTip = new ToolTip();
            ToolTip.SetToolTip(this.butLimpiarCiudades, "Limpiar campo de busqueda");
        }

        private void butLimpiarUsuario_MouseEnter(object sender, EventArgs e)
        {
            ToolTip ToolTip = new ToolTip();
            ToolTip.SetToolTip(this.butLimpiarUsuario, "Limpiar campo de busqueda");
        }


        private void cargarCiudad()
        {
            try
            {
                if (InvokeRequired)
                {

                    OpenFileDialog archivo = new OpenFileDialog();
                    archivo.Title = "seleccione archivo";

                    archivo.Filter = "TXT files|*.txt";

                    bool si = false;

                    Invoke(new Action(()
                               => si = archivo.ShowDialog() == DialogResult.OK
                               ));

                    if (si)
                    {
                        string ruta = archivo.FileName;
                        totalCiudades = mundo.CantidadLineas(ruta);
                        //MessageBox.Show("total cuidades : "+totalCiudades);
                        //10%
                        Invoke(new Action(() => labCargando.Visible = true)
                        );
                        Invoke(new Action(() => labPorcentajeCarga.Visible = true)
                        );
                        Invoke(new Action(() => progressBarCarga.Visible = true)
                            );
                        Invoke(new Action(() => butPlayCarga.Visible = true)
                            );
                        Invoke(new Action(() => butPausaCarga.Visible = true)
                            );
                        Invoke(new Action(() => butStopCarga.Visible = true)
                             );

                        mundo.cargarCiudades(ruta);

                        var paises = mundo.Paises.Values;

                        foreach (Pais pais in paises)
                        {
                            string codi = pais.Codigo;
                            var ciudades = pais.Ciudades.Values;
                            foreach (Ciudad ciudad in ciudades)
                            {
                                string nombre = ciudad.Nombre;
                                string latitud = ciudad.Latitud.ToString();
                                string longitud = ciudad.Longitud.ToString();


                                Invoke(new Action(()
                                => tablaCiudades.Rows.Add(codi, nombre, latitud, longitud)
                                 ));

                             




                                long porcen = 0;
                                Invoke(new Action(()
                               => progressBarCarga.Maximum = Convert.ToInt32(totalCiudades)
                                ));
                                Invoke(new Action(()
                                => progressBarCarga.Increment(1)
                                 ));

                                Invoke(new Action(()
                               => porcen = (progressBarCarga.Value * 100) / totalCiudades
                               ));

                                Invoke(new Action(()
                                => labPorcentajeCarga.Text = porcen + " %"
                               ));
                            }
                        }

                        Invoke(new Action(()
                                   => progressBarCarga.Value = Convert.ToInt32(totalCiudades)
                                    ));
                        Invoke(new Action(()
                                    => labPorcentajeCarga.Text = 100 + "%"
                                     ));

                        Invoke(new Action(()
                             => tablaCiudades.Rows[tablaCiudades.Rows.Count - 1].Selected = true
                              ));

                        Invoke(new Action(()
                        => tablaCiudades.CurrentCell = tablaCiudades.Rows[tablaCiudades.Rows.Count - 1].Cells[0]
                         ));
                        MessageBox.Show("Carga Exitosa!!");
                        Invoke(new Action(()
                                   => progressBarCarga.Value = 0
                                    ));
                        Invoke(new Action(()
                                    => labPorcentajeCarga.Text = 0 + "%"
                                     ));
                        Invoke(new Action(()
                                => groupDestinos.Enabled = true
                                 ));

                        Invoke(new Action(()
                                => cargarViajerosTool.Enabled = true
                                 ));
                        Invoke(new Action(()
                                => filtroTool.Enabled = true
                                 ));

                                               
                    }
                    else
                    {
                        MessageBox.Show("seleccione el archivo");
                        cargoCiudades = false;
                    }
                    Invoke(new Action(() => labCargando.Visible = false)
                                            );
                    Invoke(new Action(() => labPorcentajeCarga.Visible = false)
                                            );
                    Invoke(new Action(() => progressBarCarga.Visible = false)
                        );
                    Invoke(new Action(() => butPlayCarga.Visible = false)
                        );
                    Invoke(new Action(() => butPausaCarga.Visible = false)
                        );
                    Invoke(new Action(() => butStopCarga.Visible = false)
                         );
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                cargoCiudades = false;
            }
            


        }

        private void butCargarCiudades_Click(object sender, EventArgs e)
        {
            if (cargoCiudades)
            {
                MessageBox.Show("Las ciudades ya se encuentran cargadas");

            }else
            {
                hiloCargaCiudades = new Thread(cargarCiudad);
                hiloCargaCiudades.Start();
                cargoCiudades = true;
            }

        }
        private void contar()
        {
            
            if (InvokeRequired)
            {

                Invoke(new Action(()
                             => labPaginas.Text = "Calculando paginas "
                             ));
                Invoke(new Action(()
                      => labTotalpagina.Visible = false
                      ));

                Invoke(new Action(()
                             => textPagina.Enabled = false
                             ));
                Invoke(new Action(()
                      => butPagina.Enabled = false
                      ));


                totalViajeros = mundo.CantidadLineas(mundo.RutaViajeros);

                Invoke(new Action(()
                     => labTotalpagina.Visible = true
                     ));
                Invoke(new Action(()
                              => labPaginas.Text = "Pagina " + paginaActual
                              ));
                Invoke(new Action(()
                      => labTotalpagina.Text = "De " + totalPaginas()
                      ));
                Invoke(new Action(()
                           => textPagina.Enabled = true
                           ));
                Invoke(new Action(()
                      => butPagina.Enabled = true
                      ));
            }
        }
        private void cargarViajeros()
        {
            try
            {
                if (InvokeRequired)
                {
                    OpenFileDialog archivo = new OpenFileDialog();
                    archivo.Title = "seleccione archivo";

                    archivo.Filter = "TXT files|*.txt";

                    bool si = false;

                    Invoke(new Action(()
                               => si = archivo.ShowDialog() == DialogResult.OK
                               ));

                    if (si)
                    {
                        string ruta = archivo.FileName;
                        

                        mundo.cargarViajeros(ruta, 0, mundo.CANTIDAD_LOTE - 1);

                        Invoke(new Action(()
                              => cargarViajerosTool.Enabled=false
                              ));
                        Invoke(new Action(()
                               => tablaViajeros.Rows.Clear()
                               ));
                        Invoke(new Action(() => labCargando.Visible = true)
                                            );
                        Invoke(new Action(() => labCargando.Text = "Calculando...")
                                            );
                        Invoke(new Action(() => labPorcentajeCarga.Visible = true)
                                            );
                        Invoke(new Action(() => progressBarCarga.Visible = true)
                            );

                        Invoke(new Action(() => butPlayCarga.Visible = true)
                            );
                        Invoke(new Action(() => butPausaCarga.Visible = true)
                            );
                        Invoke(new Action(() => butStopCarga.Visible = true)
                             );

                        Invoke(new Action(()
                           => progressBarCarga.Maximum = mundo.CANTIDAD_LOTE
                            ));
                        Invoke(new Action(()
                        => progressBarCarga.Increment(1)
                         ));

                        Invoke(new Action(()
                       => progressBarCarga.Value=50
                       ));
                        Invoke(new Action(()
                       => labPorcentajeCarga.Text = "5 %"
                       ));


                        Thread contar1 = new Thread(contar);
                        contar1.Start();

   ;
                        Invoke(new Action(() => labCargando.Text = "Cargando...")
                                            );
                        
                        var viajeros = mundo.Viajeros.Values;

                        foreach (Viajero viajero in viajeros)
                        {

                            string id = viajero.Id;
                            string nombre = viajero.Nombre;
                            string apellido = viajero.Apellido;
                            var itinerario = viajero.Itinerario.Ciuadades.Values;

                            //DataGridViewComboBoxCell comboBox = new DataGridViewComboBoxCell();
                            //string[] arreglo = { "a", "b", "c" };
                            
                         

                            Invoke(new Action(()
                               => tablaViajeros.Rows.Add(id, nombre, apellido)
                               ));

                            Invoke(new Action(()
                             => tablaViajeros.Rows[tablaViajeros.Rows.Count - 1].Selected = true
                              ));

                            Invoke(new Action(()
                            => tablaViajeros.CurrentCell = tablaViajeros.Rows[tablaViajeros.Rows.Count - 1].Cells[0]
                             ));
                            long porcen = 0;
                            Invoke(new Action(()
                           => progressBarCarga.Maximum = mundo.CANTIDAD_LOTE
                            ));
                            Invoke(new Action(()
                            => progressBarCarga.Increment(1)
                             ));

                            Invoke(new Action(()
                           => porcen = (progressBarCarga.Value * 100) / mundo.CANTIDAD_LOTE
                           ));
                            //MessageBox.Show("progesbar value : " + progressBarCarga.Value);
                            Invoke(new Action(()
                            => labPorcentajeCarga.Text = porcen + " %"
                           ));

                        }
                        Invoke(new Action(()
                                  => progressBarCarga.Value = Convert.ToInt32(mundo.CANTIDAD_LOTE)
                                   ));
                        Invoke(new Action(()
                                    => labPorcentajeCarga.Text = 100 + "%"
                                     ));
                        Invoke(new Action(()
                                   => cargarViajerosTool.Enabled = false
                                    ));
                        
                        MessageBox.Show("Carga Exitosa!!");
                        Invoke(new Action(()
                                   => progressBarCarga.Value = 0
                                    ));
                        Invoke(new Action(()
                                    => labPorcentajeCarga.Text = 0 + "%"
                                     ));
                        Invoke(new Action(()
                              => groupViajeros.Enabled = true
                              ));


                    }
                    else
                    {
                        MessageBox.Show("seleccione el archivo");
                        cargoViajeros = false;
                    }
                }
                Invoke(new Action(() => labCargando.Visible = false)
                                            );
                Invoke(new Action(() => labPorcentajeCarga.Visible = false)
                                            );
                Invoke(new Action(() => progressBarCarga.Visible = false)
                    );
                Invoke(new Action(() => butPlayCarga.Visible = false)
                    );
                Invoke(new Action(() => butPausaCarga.Visible = false)
                    );
                Invoke(new Action(() => butStopCarga.Visible = false)
                     );
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                cargoViajeros = false;
            }
            
        }


        private void butCargarViajeros_Click(object sender, EventArgs e)
        {

            if (cargoViajeros)
            {
                MessageBox.Show("Los viajeros ya se encuentran cargados");
            }
            else
            {
                paginaActual = 1;
                hiloCargaViajeros = new Thread(cargarViajeros);
                progressBarCarga.Maximum = mundo.CANTIDAD_LOTE;                                    
                hiloCargaViajeros.Start();
                cargoViajeros = true;
            }

            
        }

        private void butFiltro_Click(object sender, EventArgs e)
        {
            mapa.Overlays.Clear();
            hilofiltroCiudades = new Thread(DibujarFiltro);
            if (hilofiltroCiudades!=null&&!hilofiltroCiudades.IsAlive)
            {
                
                hilofiltroCiudades.Start();                
            }
            else
            {
                MessageBox.Show("estamos cumpliendo su peticion, por favor espere un momento");
            }
            
        }

        private void butBuscarCiudad_Click(object sender, EventArgs e)
        {

            mapa.Overlays.Clear();
            hiloBuscarCiudades = new Thread(mostrarCiudadesBusqueda);
            if (hiloBuscarCiudades != null && !hiloBuscarCiudades.IsAlive)
            {

                hiloBuscarCiudades.Start();
            }
            else
            {
                MessageBox.Show("estamos cumpliendo su peticion, por favor espere un momento");
            }
            



        }

        private void butBuscarViajero_Click(object sender, EventArgs e)
        {
            
            hiloBuscarViajeros = new Thread(buscarViajero);
            if (hiloBuscarViajeros != null && !hiloBuscarViajeros.IsAlive)
            {

                hiloBuscarViajeros.Start();
            }
            else
            {
                MessageBox.Show("estamos cumpliendo su peticion, por favor espere un momento");
            }
           
            
        }

       


        private void textCiudades_TextChanged(object sender, EventArgs e)
        {
            tablaCiudades.Rows.Clear();

            CultureInfo ci = null;

            if (!textCiudades.Text.Equals(""))
            {
                var paises = mundo.Paises.Values;

                foreach (Pais p in paises)
                {
                    var ciudades = p.Ciudades.Values;
                    foreach (Ciudad c in ciudades)
                    {
                        string nombre = c.Nombre;
                        string pais = p.Codigo;
                        if (nombre.StartsWith(textCiudades.Text, true, ci))
                        {
                            Invoke(new Action(()
                        => tablaCiudades.Rows.Add(pais, nombre, c.Latitud, c.Longitud)
                         ));
                        }
                    }
                }
            }

        }
        private void VolverPaginaActual()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => tablaViajeros.Rows.Clear()));

                long inicio = (paginaActual-1) * mundo.CANTIDAD_LOTE;
                long fin = inicio + mundo.CANTIDAD_LOTE - 1;

                Invoke(new Action(() => labPorcentajeLotes.Visible = true)
                    );
                Invoke(new Action(() => progressBarLotes.Visible = true)
                    );
                Invoke(new Action(() => butPlayLotes.Visible = true)
                    );
                Invoke(new Action(() => butPauseLotes.Visible = true)
                    );
                Invoke(new Action(() => butStopLotes.Visible = true)
                     );
                Invoke(new Action(()
                  => progressBarLotes.Maximum = mundo.CANTIDAD_LOTE
                   ));
                Invoke(new Action(()
                   => progressBarLotes.Value = 50
                    ));
                Invoke(new Action(()
                   => labPorcentajeLotes.Text = "5%"
                    ));
                mundo.cargarViajeros(mundo.RutaViajeros, Convert.ToInt32(inicio), Convert.ToInt32(fin));



                var viajeros = mundo.Viajeros.Values;


                foreach (Viajero viajero in viajeros)
                {

                    string id = viajero.Id;
                    string nombre = viajero.Nombre;
                    string apellido = viajero.Apellido;
                    var itinerario = viajero.Itinerario.Ciuadades.Values;
                    string rutica = "";
                    foreach (Ciudad ciudad in itinerario)
                    {
                        rutica += ciudad.Nombre + " / ";
                    }

                    Invoke(new Action(()
                                => tablaViajeros.Rows.Add(id, nombre, apellido, rutica)
                                 ));

                    long porcen = 0;

                    Invoke(new Action(()
                    => progressBarLotes.Increment(1)
                     ));

                    Invoke(new Action(()
                   => porcen = (progressBarLotes.Value * 100) / mundo.CANTIDAD_LOTE
                   ));

                    Invoke(new Action(()
                    => labPorcentajeLotes.Text = porcen + " %"
                   ));
                }

                Invoke(new Action(()
                   => progressBarLotes.Value = mundo.CANTIDAD_LOTE
                    ));
                Invoke(new Action(()
                            => labPorcentajeLotes.Text = 100 + "%"
                             ));
                Invoke(new Action(()
                 => labPaginas.Text = "Pagina " + paginaActual
                 ));

                //MessageBox.Show("Completado!!");
                Invoke(new Action(()
                           => progressBarLotes.Value = 0
                            ));
                Invoke(new Action(()
                            => labPorcentajeLotes.Text = 0 + "%"
                             ));
                Invoke(new Action(()
                            => butAnterior.Enabled = true
                             ));
                Invoke(new Action(()
                            => butSiguiente.Enabled = true
                             ));
                Invoke(new Action(()
                           => butPagina.Enabled = true
                            ));
                Invoke(new Action(() => labPorcentajeLotes.Visible = false)
                    );
                Invoke(new Action(() => progressBarLotes.Visible = false)
                    );
                Invoke(new Action(() => butPlayLotes.Visible = false)
                    );
                Invoke(new Action(() => butPauseLotes.Visible = false)
                    );
                Invoke(new Action(() => butStopLotes.Visible = false)
                     );
                Invoke(new Action(()
              => labPaginas.Visible = true
             ));
                Invoke(new Action(()
                => labTotalpagina.Visible = true
               ));
            }
    }
        private void textViajeros_TextChanged(object sender, EventArgs e)
        {
            
            if (!textViajeros.Text.Equals(""))
            {
                tablaViajeros.Rows.Clear();
                CultureInfo ci = null;
                var viajeros = mundo.Viajeros.Values;
                foreach (Viajero v in viajeros)
                {
                    string nombre = v.Nombre;
                    string apellido = v.Apellido;

                    if ((nombre.StartsWith(textViajeros.Text, true, ci)) || (apellido.StartsWith(textViajeros.Text, true, ci)))
                    {
                        string id = v.Id;
                        string rutica = "";
                        var ciudades = v.Itinerario.Ciuadades.Values;
                                               
                        foreach (Ciudad esta in ciudades)
                        {
                            rutica += esta.Nombre + " / ";
                        }
                        tablaViajeros.Rows.Add(id, nombre, apellido, rutica);

                    }
                }
            }
            else
            {
                hiloVolverPaginaActual = new Thread(VolverPaginaActual);
                hiloVolverPaginaActual.Start();
            }
        }

        private void textCiudades_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar== Convert.ToChar(Keys.Enter))
            {
                mapa.Overlays.Clear();
                hiloBuscarCiudades = new Thread(mostrarCiudadesBusqueda);
                if (hiloBuscarCiudades != null && !hiloBuscarCiudades.IsAlive)
                {

                    hiloBuscarCiudades.Start();
                }
                else
                {
                    MessageBox.Show("estamos cumpliendo su peticion, por favor espere un momento");
                }
            }
        }

        private void textViajeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                
                hiloBuscarViajeros = new Thread(buscarViajero);
                if (!hiloBuscarViajeros.IsAlive)
                {
                    //MessageBox.Show("entro enter");
                    hiloBuscarViajeros.Start();
                }
                else
                {
                    MessageBox.Show("estamos cumpliendo su peticion, por favor espere un momento");
                }
            }

        }

        private void textFiltro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                hilofiltroCiudades = new Thread(DibujarFiltro);
                mapa.Overlays.Clear();
                if (hilofiltroCiudades!=null&&!hilofiltroCiudades.IsAlive)
                {
                    
                    hilofiltroCiudades.Start();
                }
                else
                {
                    MessageBox.Show("estamos cumpliendo su peticion, por favor espere un momento");
                }
            }
        }

        private void tablaViajeros_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tabla = true;
           
            int indice = e.RowIndex;
            if (indice >= 0)
            {
                DataGridViewRow fila = tablaViajeros.Rows[indice];
                //string seleccion = tablaCiudades.SelectedRows.ToString();
                if (fila.Cells[0] != null)
                {
                    string id = fila.Cells[0].Value.ToString();
                    Viajero viajero = (Viajero)mundo.Viajeros[id];

                    viajero.Itinerario.voraz((Ciudad)viajero.Itinerario.CiuadadesGrafo[0]);


                    listBoxCiudades.Items.Clear();
                    groupBox4.Text = "Ciudades a visitar de : " + viajero.Nombre;
                    int i = 1;
                    foreach (Ciudad ciudad in viajero.Itinerario.Ciuadades.Values)
                    {
                        listBoxCiudades.Items.Add(i+") "+ciudad.Nombre);
                        i++;
                    }

                    dibujarItinerarioViajero(viajero, checkBoxVista.Checked);
                   
                }
            }
        }
        private void tablaCiudades_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int indice = e.RowIndex;
            if (indice >= 0)
            {
                DataGridViewRow fila = tablaCiudades.Rows[indice];
               
                if (fila.Cells[0] != null)
                {
                    string ciudad = fila.Cells[1].Value.ToString();
                    string pais = fila.Cells[0].Value.ToString();
                    Pais country = (Pais)mundo.Paises[pais];
                    Ciudad city = (Ciudad)country.Ciudades[ciudad];
                    dibujarMarcador(country, city);
                }
            }

        }
        
        private void butLimpiarCiudades_Click(object sender, EventArgs e)
        {
            textCiudades.Text = "";
        }

        private void butLimpiarUsuario_Click(object sender, EventArgs e)
        {
            textViajeros.Text = "";
        } 

        private void dibujarMarcador(Pais pais, Ciudad ciudad)
        {
       
            mapa.Overlays.Clear();

            GMapOverlay marcadores = new GMapOverlay("ciudades");
            PointLatLng coordenada = new PointLatLng(ciudad.Latitud, ciudad.Longitud);
            GMarkerGoogle punto = new GMarkerGoogle(coordenada, GMarkerGoogleType.blue_dot);
            punto.ToolTipText = "Codigo del pais : "+ pais.Codigo + "\n" + 
                                "Nombre: " + ciudad.Nombre + "\n" + 
                                "Latitud : " + ciudad.Latitud + "\n" + 
                                "Longitu : " + ciudad.Longitud + "\n" +
                                "Poblacion : " + ciudad.TotalPoblacion ;
            marcadores.Markers.Add(punto);
            mapa.Overlays.Add(marcadores);
            
            mapa.Position = coordenada;
            mapa.Zoom = 5;
        }

        private void dibujarMarcador(Ciudad ciudad)
        {
            GMapOverlay marcadores = new GMapOverlay("ciudades");
            PointLatLng coordenada = new PointLatLng(ciudad.Latitud, ciudad.Longitud);
            GMarkerGoogle punto = new GMarkerGoogle(coordenada, GMarkerGoogleType.blue_dot);
            punto.ToolTipText = "Nombre: " + ciudad.Nombre + "\n" +
                                "Latitud : " + ciudad.Latitud + "\n" +
                                "Longitud : " + ciudad.Longitud + "\n" +
                                "Poblacion : " + ciudad.TotalPoblacion;
            marcadores.Markers.Add(punto);
            mapa.Overlays.Add(marcadores);
            
            mapa.Zoom = 0;

        }

        private void dibujarItinerarioViajero(Viajero viajero, bool ponderado)
        {
            mapa.Overlays.Clear();
            GMapOverlay marcadores = new GMapOverlay("ciudades");
            GMapOverlay lineas = new GMapOverlay("rutas");
            if (!ponderado)
            {
                var ciudades = viajero.Itinerario.Ciuadades.Values;
                foreach (Ciudad ciudad in ciudades)
                {

                    PointLatLng coordenada = new PointLatLng(ciudad.Latitud, ciudad.Longitud);
                    GMarkerGoogle punto = new GMarkerGoogle(coordenada, GMarkerGoogleType.blue_dot);
                    punto.ToolTipText = "Nombre: " + ciudad.Nombre + "\n" +
                                        "Latitud : " + ciudad.Latitud + "\n" +
                                        "Longitud : " + ciudad.Longitud + "\n" +
                                        "Poblacion : " + ciudad.TotalPoblacion;
                    marcadores.Markers.Add(punto);
                    mapa.Overlays.Add(marcadores);

                }
                
            }
            else
            {
                var ciudades = viajero.Itinerario.Ciuadades.Values;
                foreach (Ciudad city in ciudades)
                {
                    Ciudad estaCiudad = city;
                    GMarkerGoogle punto = new GMarkerGoogle(new PointLatLng(estaCiudad.Latitud, estaCiudad.Longitud), GMarkerGoogleType.blue_dot);
                    punto.ToolTipText = "Nombre: " + estaCiudad.Nombre + "\n" +
                                        "Latitud : " + estaCiudad.Latitud + "\n" +
                                        "Longitud : " + estaCiudad.Longitud + "\n" +
                                        "Poblacion : " + estaCiudad.TotalPoblacion;
                    marcadores.Markers.Add(punto);

                    foreach (Ciudad city2 in ciudades)
                    {
                        Ciudad otraCiudad = city2;
                        double distancia = viajero.Itinerario.Grafo[estaCiudad.PosEnGrafo, otraCiudad.PosEnGrafo];
                        distancia = Math.Round(distancia, 2);

                        if (distancia!=0)
                        {                            
                            List<PointLatLng> points = new List<PointLatLng>();
                            points.Add(new PointLatLng(estaCiudad.Latitud, estaCiudad.Longitud));
                            points.Add(new PointLatLng(otraCiudad.Latitud, otraCiudad.Longitud));
                            GMapPolygon polygon = new GMapPolygon(points, "mypolygon");
                            polygon.Fill = new SolidBrush(Color.FromArgb(50, Color.Gray));
                            polygon.Stroke = new Pen(Color.Gray, 1);
                            lineas.Polygons.Add(polygon);
                          
                            double lat = (estaCiudad.Latitud + otraCiudad.Latitud) / 2;
                            double lon = (estaCiudad.Longitud + otraCiudad.Longitud) / 2;
                            PointLatLng puntoDistancia = new PointLatLng(lat, lon);
                            GMarkerCross marcadorDistancia = new GMarkerCross(puntoDistancia);
                            marcadorDistancia.ToolTipText = distancia + " Km";
                            marcadorDistancia.ToolTipMode = MarkerTooltipMode.Always;

                            marcadores.Markers.Add(marcadorDistancia);
                        }
                    }
                }

                mapa.Overlays.Add(marcadores);
                mapa.Overlays.Add(lineas);
                //MessageBox.Show("en proceso de desarrollo, gracias por su paciencia");
            }
            mapa.Zoom = 0;
            
        }

        private void DibujarFiltro()
        {           
            if (InvokeRequired)
            {

                Invoke(new Action(()
                            => textFiltro.Enabled = false
                             ));
                Invoke(new Action(()
                            => filtroTool.Enabled = false
                             ));
                try
                {
                    int poblacion = 0;
                    string escrito = "";
                    Invoke(new Action(()
                            => escrito = textFiltro.Text
                             ));
                    if (escrito.Equals(""))
                    {
                        MessageBox.Show("Por favor digite un valor correcto");
                    }
                    else {
                        poblacion = int.Parse(escrito);


                        if (poblacion == 0)
                        {
                            MessageBox.Show("Digite un valor mayor a 0");
                        }
                        if (poblacion > 0)
                        {
                            Invoke(new Action(() => labFiltrando.Visible = true)
                             );
                            Invoke(new Action(() => labPorcentajeFiltro.Visible = true)
                             );
                            Invoke(new Action(() => progressBarFiltro.Visible = true)
                                );
                            Invoke(new Action(() => butPlayFiltro.Visible = true)
                                );
                            Invoke(new Action(() => butPausaFiltro.Visible = true)
                                );
                            Invoke(new Action(() => butStopFiltro.Visible = true)
                                 );

                            var lista = mundo.filtrarCiudades(poblacion).Values;
                            int cantidad = lista.Count;
                            string pob = "";
                            Invoke(new Action(()
                                => pob = textFiltro.Text
                                 ));
                            var opcion = MessageBox.Show("Se han encontrado " + cantidad + " ciudades con poblacion mayor a " + pob + "\n" +
                                                          "¿Desea verlas en el mapa? (Puede verse algo saturado el mapa)", "Ciudades con poblacion mayor", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            bool si = false;

                            Invoke(new Action(()
                                => si = opcion.Equals(DialogResult.Yes)
                                 ));
                            if (si)
                            {
                                Invoke(new Action(()
                                => mapa.Overlays.Clear()
                                 ));

                                foreach (Ciudad ciudad in lista)
                                {
                                    Invoke(new Action(()
                                => dibujarMarcador(ciudad)
                                 ));
                                    long porcen = 0;
                                    Invoke(new Action(()
                                   => progressBarFiltro.Maximum = cantidad
                                    ));
                                    Invoke(new Action(()
                                    => progressBarFiltro.Increment(1)
                                     ));

                                    Invoke(new Action(()
                                   => porcen = (progressBarFiltro.Value * 100) / cantidad
                                   ));

                                    Invoke(new Action(()
                                    => labPorcentajeFiltro.Text = porcen + " %"
                                   ));
                                }

                                Invoke(new Action(()
                                   => progressBarFiltro.Value = cantidad
                                    ));
                                Invoke(new Action(()
                                            => labPorcentajeFiltro.Text = 100 + "%"
                                             ));

                                MessageBox.Show("ya Puede ver las ciudades filtradas en el mapa!!");
                                Invoke(new Action(()
                                           => progressBarFiltro.Value = 0
                                            ));
                                Invoke(new Action(()
                                            => labPorcentajeFiltro.Text = 0 + "%"
                                             ));
                                Invoke(new Action(()
                                            => groupBox3.Enabled = true
                                             ));
                             
                            }
                            Invoke(new Action(() => labFiltrando.Visible = false)
                                                         );
                            Invoke(new Action(() => labPorcentajeFiltro.Visible = false)
                                                         );
                            Invoke(new Action(() => progressBarFiltro.Visible = false)
                                );
                            Invoke(new Action(() => butPlayFiltro.Visible = false)
                                );
                            Invoke(new Action(() => butPausaFiltro.Visible = false)
                                );
                            Invoke(new Action(() => butStopFiltro.Visible = false)
                                 );
                        }

                    }
                }
                catch
                {
                    MessageBox.Show("Por favor digite un valor correcto");
                }
                Invoke(new Action(()
                  => textFiltro.Text = ""
               ));
                Invoke(new Action(()
                  => textFiltro.Enabled = true
               ));
                Invoke(new Action(()
                  => filtroTool.Enabled = true
               ));


            }
        }
        

        public void mostrarCiudadesBusqueda()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(()
                 => textCiudades.Enabled = false
              ));
                Invoke(new Action(()
                 => butBuscarCiudad.Enabled = false
              ));
               
                int cantidadlistica = 0;
             
                Invoke(new Action(()
                 => cantidadlistica = tablaCiudades.Rows.Count
              ));
                
                var opcion = MessageBox.Show("Se han encontrado " + cantidadlistica +" ciudades "+"\n" +
                                                      "Desea verlas en el mapa?(puede verse algo saturado el mapa)" + "\n" +
                                                      "En la lista a su derecha puede obsevar el resultado de su busqueda e ir a una ciudad especifica", "Busqueda Ciudades", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                bool si = false;

                Invoke(new Action(()
                    => si = opcion.Equals(DialogResult.Yes)
                     ));
                if (si)
                {
                    Invoke(new Action(() => labPorcentajeCiudad.Visible = true)
                             );
                    Invoke(new Action(() => progressBarCiudad.Visible = true)
                        );
                    Invoke(new Action(() => butPlayCiudad.Visible = true)
                        );
                    Invoke(new Action(() => butPauseCiudad.Visible = true)
                        );
                    Invoke(new Action(() => butStopCiudad.Visible = true)
                         );
                    //var paises=
                    //foreach ()
                    //{

                    //}

                    for (int i = 0; i < tablaCiudades.RowCount-1; i++)
                    {
                        string pais = tablaCiudades[0, i].Value.ToString();
                        string ciudad = tablaCiudades[1, i].Value.ToString();
                        Pais country = (Pais)mundo.Paises[pais];
                        Ciudad city = (Ciudad)country.Ciudades[ciudad];
                        Invoke(new Action(()
                        => dibujarMarcador(city)
                          ));
                        long porcen = 0;
                        Invoke(new Action(()
                       => progressBarCiudad.Maximum = cantidadlistica
                        ));
                        Invoke(new Action(()
                        => progressBarCiudad.Increment(1)
                         ));

                        Invoke(new Action(()
                       => porcen = (progressBarCiudad.Value * 100) / cantidadlistica
                       ));

                        Invoke(new Action(()
                        => labPorcentajeCiudad.Text = porcen + " %"
                       ));
                    }                

                    Invoke(new Action(()
                       => progressBarCiudad.Value = cantidadlistica
                        ));
                    Invoke(new Action(()
                                => labPorcentajeCiudad.Text = 100 + "%"
                                 ));

                    MessageBox.Show("Observe el resultado de su busqueda!!");
                   
                }
                else
                {
                    Invoke(new Action(()
                                => mapa.Overlays.Clear()
                                 ));
                }
                Invoke(new Action(()
                              => progressBarCiudad.Value = 0
                               ));
                Invoke(new Action(()
                            => labPorcentajeCiudad.Text = 0 + "%"
                             ));
                Invoke(new Action(() => labPorcentajeCiudad.Visible = false)
                         );
                Invoke(new Action(() => progressBarCiudad.Visible = false)
                    );
                Invoke(new Action(() => butPlayCiudad.Visible = false)
                    );
                Invoke(new Action(() => butPauseCiudad.Visible = false)
                    );
                Invoke(new Action(() => butStopCiudad.Visible = false)
                     );

                Invoke(new Action(()
                => textCiudades.Enabled = true
             ));
                Invoke(new Action(()
                 => butBuscarCiudad.Enabled = true
              ));
            }
        }

        private void siguiente()
        {
            if (InvokeRequired)
            {
                int pag = Convert.ToInt32(paginaActual+1);
               
                int inicio = 0;
                int fin = 0;
                if (pag > 0 && pag <= paginas)
                {
                    Invoke(new Action(() => tablaViajeros.Rows.Clear()));
                    paginaActual = pag;
                    inicio = (pag - 1) * mundo.CANTIDAD_LOTE;
                    fin = inicio + mundo.CANTIDAD_LOTE - 1;

                    Invoke(new Action(() => labPorcentajeLotes.Visible = true)
                        );
                    Invoke(new Action(() => progressBarLotes.Visible = true)
                        );
                    Invoke(new Action(() => butPlayLotes.Visible = true)
                        );
                    Invoke(new Action(() => butPauseLotes.Visible = true)
                        );
                    Invoke(new Action(() => butStopLotes.Visible = true)
                         );
                    Invoke(new Action(()
                      => progressBarLotes.Maximum = mundo.CANTIDAD_LOTE
                       ));
                    Invoke(new Action(()
                       => progressBarLotes.Value = 50
                        ));
                    Invoke(new Action(()
                       => labPorcentajeLotes.Text = "5%"
                        ));
                    mundo.cargarViajeros(mundo.RutaViajeros, inicio, fin);

                    

                    var viajeros = mundo.Viajeros.Values;

                    
                    foreach (Viajero viajero in viajeros)
                    {

                        string id = viajero.Id;
                        string nombre = viajero.Nombre;
                        string apellido = viajero.Apellido;
 
                        Invoke(new Action(()
                                    => tablaViajeros.Rows.Add(id, nombre, apellido)
                                     ));

                        long porcen = 0;

                        Invoke(new Action(()
                        => progressBarLotes.Increment(1)
                         ));

                        Invoke(new Action(()
                       => porcen = (progressBarLotes.Value * 100) / mundo.CANTIDAD_LOTE
                       ));

                        Invoke(new Action(()
                        => labPorcentajeLotes.Text = porcen + " %"
                       ));
                        Invoke(new Action(()
                      => labPaginas.Text = "Pagina "+paginaActual
                      ));
                    }

                    Invoke(new Action(()
                       => progressBarLotes.Value = mundo.CANTIDAD_LOTE
                        ));
                    Invoke(new Action(()
                                => labPorcentajeLotes.Text = 100 + "%"
                                 ));

                    //MessageBox.Show("Completado!!");
                    Invoke(new Action(()
                               => progressBarLotes.Value = 0
                                ));
                    Invoke(new Action(()
                                => labPorcentajeLotes.Text = 0 + "%"
                                 ));
                    Invoke(new Action(()
                                => butAnterior.Enabled = true
                                 ));
                    Invoke(new Action(()
                                => butSiguiente.Enabled = true
                                 ));
                    Invoke(new Action(()
                               => butPagina.Enabled = true
                                ));
                    Invoke(new Action(() => labPorcentajeLotes.Visible = false)
                        );
                    Invoke(new Action(() => progressBarLotes.Visible = false)
                        );
                    Invoke(new Action(() => butPlayLotes.Visible = false)
                        );
                    Invoke(new Action(() => butPauseLotes.Visible = false)
                        );
                    Invoke(new Action(() => butStopLotes.Visible = false)
                         );
                    Invoke(new Action(()
                  => labPaginas.Visible = true
                 ));
                    Invoke(new Action(()
                    => labTotalpagina.Visible = true
                   ));
                }
                else
                {
                    MessageBox.Show("Ha llegado a la ultima pagina");
                    Invoke(new Action(()
                                => butAnterior.Enabled = true
                                 ));
                   
                    Invoke(new Action(()
                               => butPagina.Enabled = true
                                ));
                   
                }            
            }
            
            
        }
        private void butSiguiente_Click(object sender, EventArgs e)
        {
           
            hiloSiguiente = new Thread(siguiente);
            textPagina.Text = "";
            butAnterior.Enabled = false;
            butSiguiente.Enabled = false;
            hiloSiguiente.Start();

            
        }


        private void anterior()
        {

            if (InvokeRequired)
            {
                int pag = Convert.ToInt32(paginaActual - 1);

                int inicio = 0;
                int fin = 0;
                if (pag > 0 && pag <= paginas)
                {
                    paginaActual = pag;
                    inicio = (pag - 1) * mundo.CANTIDAD_LOTE;
                    fin = inicio + mundo.CANTIDAD_LOTE - 1;

                    Invoke(new Action(() => labPorcentajeLotes.Visible = true)
                        );
                    Invoke(new Action(() => progressBarLotes.Visible = true)
                        );
                    Invoke(new Action(() => butPlayLotes.Visible = true)
                        );
                    Invoke(new Action(() => butPauseLotes.Visible = true)
                        );
                    Invoke(new Action(() => butStopLotes.Visible = true)
                         );

                    Invoke(new Action(()
                      => progressBarLotes.Maximum = mundo.CANTIDAD_LOTE
                       ));
                    Invoke(new Action(()
                       => progressBarLotes.Value = 50
                        ));
                    Invoke(new Action(()
                       => labPorcentajeLotes.Text = "5%"
                        ));
                    mundo.cargarViajeros(mundo.RutaViajeros, inicio, fin);

                    Invoke(new Action(() => tablaViajeros.Rows.Clear()));

                    var viajeros = mundo.Viajeros.Values;

                    
  
                    foreach (Viajero viajero in viajeros)
                    {

                        string id = viajero.Id;
                        string nombre = viajero.Nombre;
                        string apellido = viajero.Apellido;
                     
                        Invoke(new Action(()
                                    => tablaViajeros.Rows.Add(id, nombre, apellido)
                                     ));

                        long porcen = 0;

                        Invoke(new Action(()
                        => progressBarLotes.Increment(1)
                         ));

                        Invoke(new Action(()
                       => porcen = (progressBarLotes.Value * 100) / mundo.CANTIDAD_LOTE
                       ));

                        Invoke(new Action(()
                        => labPorcentajeLotes.Text = porcen + " %"
                       ));
                        Invoke(new Action(()
                      => labPaginas.Text = "Pagina " + paginaActual
                      ));
                    }

                    Invoke(new Action(()
                       => progressBarLotes.Value = mundo.CANTIDAD_LOTE
                        ));
                    Invoke(new Action(()
                                => labPorcentajeLotes.Text = 100 + "%"
                                 ));

                    //MessageBox.Show("Completado!!");
                    Invoke(new Action(()
                               => progressBarLotes.Value = 0
                                ));
                    Invoke(new Action(()
                                => labPorcentajeLotes.Text = 0 + "%"
                                 ));
                    Invoke(new Action(()
                                => butAnterior.Enabled = true
                                 ));
                    Invoke(new Action(()
                                => butSiguiente.Enabled = true
                                 ));
                    Invoke(new Action(()
                               => butPagina.Enabled = true
                                ));
                    Invoke(new Action(() => labPorcentajeLotes.Visible = false)
                       );
                    Invoke(new Action(() => progressBarLotes.Visible = false)
                        );
                    Invoke(new Action(() => butPlayLotes.Visible = false)
                        );
                    Invoke(new Action(() => butPauseLotes.Visible = false)
                        );
                    Invoke(new Action(() => butStopLotes.Visible = false)
                         );
                    Invoke(new Action(()
                  => labPaginas.Visible = true
                 ));
                    Invoke(new Action(()
                    => labTotalpagina.Visible = true
                   ));
                }
                else
                {
                    MessageBox.Show("Ha llegado a la primera pagina");
                   
                    Invoke(new Action(()
                                => butSiguiente.Enabled = true
                                 ));
                    Invoke(new Action(()
                               => butPagina.Enabled = true
                                ));
                   
                }
            }
            
        }


        private void butAnterior_Click(object sender, EventArgs e)
        {
            
            hiloAnterior = new Thread(anterior);
            textPagina.Text = "";
            butSiguiente.Enabled = false;
            butAnterior.Enabled = false;
            hiloAnterior.Start();
            
        }

        public void buscarViajero()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(()
                => textViajeros.Enabled = false
               ));
                Invoke(new Action(()
                => butBuscarViajero.Enabled = false
               ));
                Invoke(new Action(()
                => textPagina.Enabled = false
               ));
                Invoke(new Action(()
                => butAnterior.Enabled = false
               ));
                Invoke(new Action(()
                => butSiguiente.Enabled = false
               ));
                Invoke(new Action(()
                => butPagina.Enabled = false
               ));
                string escrito = "";
                Invoke(new Action(()
                => escrito= textViajeros.Text
               ));

                if (!escrito.Equals("")) {
                    Invoke(new Action(() => labPorcentajeViajero.Visible = true)
                             );
                    Invoke(new Action(() => progressBarViajero.Visible = true)
                        );
                    Invoke(new Action(() => butPlayViajero.Visible = true)
                        );
                    Invoke(new Action(() => butPauseViajero.Visible = true)
                        );
                    Invoke(new Action(() => butStopViajero.Visible = true)
                         );
                    Invoke(new Action(()
                => progressBarViajero.Increment(1)
                 ));
                
                    Invoke(new Action(()
                       => progressBarViajero.Value = 5
                        ));
                    Invoke(new Action(()
                                => labPorcentajeViajero.Text = "5 %"
                                 ));


                    List<Viajero> viajeros = mundo.buscarViajeros(escrito);

                    Invoke(new Action(()
                                => tablaViajeros.Rows.Clear()
                                 ));
                    

                    int cantidadViajeros = viajeros.Count();
                    long porcen = 0;
                    foreach (Viajero viajero in viajeros)
                    {
                        string nombre = viajero.Nombre;
                        string apellido = viajero.Apellido;        
                        string id = viajero.Id;
                        string rutica = "";
                        var ciudades = viajero.Itinerario.Ciuadades.Values;

                        
                       foreach (Ciudad esta in ciudades)
                       {
                        rutica += esta.Nombre + " / ";
                       }
                        Invoke(new Action(()
                        => tablaViajeros.Rows.Add(id, nombre, apellido, rutica)
                        ));
                    
                       
                        Invoke(new Action(()
                       => progressBarViajero.Maximum = cantidadViajeros
                        ));
                        Invoke(new Action(()
                        => progressBarViajero.Increment(1)
                         ));

                        Invoke(new Action(()
                       => porcen = (progressBarViajero.Value * 100) / cantidadViajeros
                       ));

                        Invoke(new Action(()
                        => labPorcentajeViajero.Text = porcen + " %"
                       ));
                    }

                    Invoke(new Action(()
                       => progressBarViajero.Value = cantidadViajeros
                        ));
                    Invoke(new Action(()
                                => labPorcentajeViajero.Text = 100 + "%"
                                 ));

                    MessageBox.Show("Se han encontrado " + cantidadViajeros + " viajeros como resultado de su busqueda" + "\n" +
                                        "Puede ver uno por uno en la tabla a continuacion");
                    Invoke(new Action(()
                               => progressBarViajero.Value = 0
                                ));
                    Invoke(new Action(()
                                => labPorcentajeViajero.Text = 0 + "%"
                                ));
                    Invoke(new Action(()
                    => textViajeros.Enabled = true
                     ));
                    Invoke(new Action(()
                    => butBuscarViajero.Enabled = true
                   ));
                    Invoke(new Action(()
                     => textPagina.Enabled = true
                   ));
                    Invoke(new Action(()
                    => butAnterior.Enabled = true
                   ));
                    Invoke(new Action(()
                    => butSiguiente.Enabled = true
                   ));
                    Invoke(new Action(()
                    => butPagina.Enabled = true
                   ));
                    Invoke(new Action(()
                   => labPaginas.Visible=false
                  ));
                    Invoke(new Action(()
                    => labTotalpagina.Visible=false
                   ));

                }
                else
                {
                    MessageBox.Show("Primero digite un nombre o un apellido para realizar la busqueda");
                    Invoke(new Action(()
               => textViajeros.Enabled = true
              ));
                    Invoke(new Action(()
                    => butBuscarViajero.Enabled = true
                   ));
                    Invoke(new Action(()
                    => textPagina.Enabled = true
                   ));
                    Invoke(new Action(()
                    => butAnterior.Enabled = true
                   ));
                    Invoke(new Action(()
                    => butSiguiente.Enabled = true
                   ));
                    Invoke(new Action(()
                    => butPagina.Enabled = true
                   ));
                }
                Invoke(new Action(() => labPorcentajeViajero.Visible = false)
                            );
                Invoke(new Action(() => progressBarViajero.Visible = false)
                    );
                Invoke(new Action(() => butPlayViajero.Visible = false)
                    );
                Invoke(new Action(() => butPauseViajero.Visible = false)
                    );
                Invoke(new Action(() => butStopViajero.Visible = false)
                     );

            }
        }

        private void checkBoxVista_CheckedChanged(object sender, EventArgs e)
        {        
            if (tabla)
            {
                DataGridViewSelectedRowCollection filas = tablaViajeros.SelectedRows;
               
                foreach (DataGridViewRow fila in filas)
                {
                    if (fila.Cells[0] != null)
                    {
                        string id = fila.Cells[0].Value.ToString();
                        Viajero viajero = (Viajero)mundo.Viajeros[id];
                        
                        dibujarItinerarioViajero(viajero, checkBoxVista.Checked);

                    }
                }
            }
          
        }

        private void butPlayCarga_Click(object sender, EventArgs e)
        {
            if (hiloCargaCiudades != null&& hiloCargaCiudades.ThreadState == ThreadState.Suspended&& hiloCargaCiudades.IsAlive)
            {
                hiloCargaCiudades.Resume();
            }
            if (hiloCargaViajeros != null && hiloCargaViajeros.ThreadState == ThreadState.Suspended && hiloCargaViajeros.IsAlive)
            {
                hiloCargaViajeros.Resume();
            }
        }

        private void butPausaCarga_Click(object sender, EventArgs e)
        {
            if (hiloCargaCiudades != null && hiloCargaCiudades.IsAlive)
            {
                hiloCargaCiudades.Suspend();
            }
            if (hiloCargaViajeros != null && hiloCargaViajeros.IsAlive)
            {
                hiloCargaViajeros.Suspend();
            }
        }

        private void butStopCarga_Click(object sender, EventArgs e)
        {
            if (hiloCargaCiudades != null && hiloCargaCiudades.IsAlive)
            {
                hiloCargaCiudades.Abort();
                cargoCiudades = false;
                tablaCiudades.Rows.Clear();
            }
            if (hiloCargaViajeros != null && hiloCargaViajeros.IsAlive)
            {
                hiloCargaViajeros.Abort();
                cargoViajeros = false;
                tablaViajeros.Rows.Clear();
            }
            
            progressBarCarga.Value = 0;
            labPorcentajeCarga.Text = 0 + "%";
        }

        private void butPlayFiltro_Click(object sender, EventArgs e)
        {
            if (hilofiltroCiudades != null && hilofiltroCiudades.ThreadState == ThreadState.Suspended && hilofiltroCiudades.IsAlive)
            {
                hilofiltroCiudades.Resume();
            }
            
        }

        private void butPausaFiltro_Click(object sender, EventArgs e)
        {
            if (hilofiltroCiudades != null && hilofiltroCiudades.IsAlive)
            {
                hilofiltroCiudades.Suspend();
            }
        }

        private void butStopFiltro_Click(object sender, EventArgs e)
        {
            if (hilofiltroCiudades != null && hilofiltroCiudades.IsAlive)
            {
                hilofiltroCiudades.Abort();
            }
            textFiltro.Enabled = true;
            filtroTool.Enabled = true;
            progressBarFiltro.Value = 0;
            labPorcentajeFiltro.Text = 0 + "%";
        }

        private void butPlayCiudad_Click(object sender, EventArgs e)
        {
            if (hiloBuscarCiudades != null && hiloBuscarCiudades.ThreadState == ThreadState.Suspended && hiloBuscarCiudades.IsAlive)
            {
                hiloBuscarCiudades.Resume();
            }
        }

        private void butPauseCiudad_Click(object sender, EventArgs e)
        {
            if (hiloBuscarCiudades != null && hiloBuscarCiudades.IsAlive)
            {
                hiloBuscarCiudades.Suspend();
            }
        }

        private void butStopCiudad_Click(object sender, EventArgs e)
        {
            if (hiloBuscarCiudades != null && hiloBuscarCiudades.IsAlive)
            {
                hiloBuscarCiudades.Abort();
            }
            progressBarCiudad.Value = 0;
            labPorcentajeCiudad.Text = 0 + "%";
            textCiudades.Enabled = true;
            butBuscarCiudad.Enabled = true;
        }

        private void butPlayLotes_Click(object sender, EventArgs e)
        {
            if (hiloSiguiente != null && hiloSiguiente.ThreadState == ThreadState.Suspended && hiloSiguiente.IsAlive)
            {
                hiloSiguiente.Resume();
            }
            if (hiloAnterior != null && hiloAnterior.ThreadState == ThreadState.Suspended && hiloAnterior.IsAlive)
            {
                hiloAnterior.Resume();
            }
        }

        private void butPauseLotes_Click(object sender, EventArgs e)
        {
            if (hiloSiguiente != null && hiloSiguiente.IsAlive)
            {
                hiloSiguiente.Suspend();
            }
            if (hiloAnterior != null && hiloAnterior.IsAlive)
            {
                hiloAnterior.Suspend();
            }
        }

        private void butStopLotes_Click(object sender, EventArgs e)
        {
            if (hiloSiguiente != null && hiloSiguiente.IsAlive)
            {
                hiloSiguiente.Abort();
            }
            if (hiloAnterior != null && hiloAnterior.IsAlive)
            {
                hiloAnterior.Abort();
            }

            progressBarLotes.Value = 0;
            labPorcentajeLotes.Text = 0+"%";
            butAnterior.Enabled = true;
            butSiguiente.Enabled = true;
        }

        private void butStopViajero_Click(object sender, EventArgs e)
        {
            if (hiloBuscarViajeros != null && hiloBuscarViajeros.IsAlive)
            {
                hiloBuscarViajeros.Abort();
            }
            textViajeros.Enabled = true;
            butBuscarViajero.Enabled = true;
            progressBarViajero.Value = 0;
            labPorcentajeViajero.Text = 0 + "%";
        }

        private void butPauseViajero_Click(object sender, EventArgs e)
        {
            if (hiloBuscarViajeros != null && hiloBuscarViajeros.IsAlive)
            {
                hiloBuscarViajeros.Suspend();
            }
        }

        private void butPlayViajero_Click(object sender, EventArgs e)
        {
            if (hiloBuscarViajeros != null && hiloBuscarViajeros.ThreadState == ThreadState.Suspended && hiloBuscarViajeros.IsAlive)
            {
                hiloBuscarViajeros.Resume();
            }
        }


        public void solucionTotal()
        {
            if (InvokeRequired)
            {
       
                Invoke(new Action(() => solucionGeneral = new VisualizacionSimplificada(this)));

                Invoke(new Action(() => labResolverTotal.Visible = true)
                                        );
                Invoke(new Action(() => progressBarResolverTotal.Visible = true)
                    );
                Invoke(new Action(() => butPlayResolverTotal.Visible = true)
                    );
                Invoke(new Action(() => butPausaResolverTotal.Visible = true)
                    );
                Invoke(new Action(() => butStopResolverTotal.Visible = true)
                     );
                Invoke(new Action(()
                  => progressBarResolverTotal.Maximum = mundo.CANTIDAD_LOTE
                   ));
                Invoke(new Action(()
                   => progressBarResolverTotal.Value = 50
                    ));
                Invoke(new Action(()
                   => labResolverTotal.Text = "5%"
                    ));

                mundo.cargarViajeros(mundo.RutaViajeros,0,mundo.CANTIDAD_LOTE-1);
                var viajeros = mundo.Viajeros.Values;

                Invoke(new Action(() => solucionGeneral.Show()));

                foreach (Viajero viajero in viajeros)
            {
                    string nombre = viajero.Nombre;
                    string apellido = viajero.Apellido;
                    string rutica = "";
                    var ciudades = viajero.Itinerario.Ciuadades.Values;

                    if (radEficiente.Checked)
                    {
                        solucionGeneral.Eficiente = true;
                        solucionGeneral.ExploracionCompleta = false;
                        solucionGeneral.Libre = false;
                        List<Arista<Ciudad>> kruskal = viajero.itinerarioEficiente();                                                
                    }
                    if (radExacto.Checked)
                    {                            
                                solucionGeneral.Eficiente = false;
                                solucionGeneral.ExploracionCompleta = true;
                                solucionGeneral.Libre = false;
                                List<Arista<Ciudad>> fuerzabruta = viajero.itinerarioExploracionCompleta();       
                        }
                    if (radLibre.Checked)
                    {
                        //solucionGeneral.Eficiente = false;
                        //solucionGeneral.ExploracionCompleta = false;
                        //solucionGeneral.Libre = true;
                        //List<Arista<Ciudad>> libre = viajero.itinerarioLibre();
                      
                    }
                    Invoke(new Action(() => solucionGeneral.agregarFila(viajero.Id, nombre, apellido)));





                    long porcen = 0;
                    Invoke(new Action(()
                    => progressBarResolverTotal.Increment(1)
                     ));

                    Invoke(new Action(()
                   => porcen = (progressBarResolverTotal.Value * 100) / mundo.CANTIDAD_LOTE
                   ));

                    Invoke(new Action(()
                    => labResolverTotal.Text = porcen + " %"
                   ));

                }                    
                                     
                Invoke(new Action(()
                       => progressBarResolverTotal.Value = mundo.CANTIDAD_LOTE
                        ));
                Invoke(new Action(()
                            => labResolverTotal.Text = 100 + "%"
                             ));


                //MessageBox.Show("Completado!!");
                Invoke(new Action(()
                           => progressBarResolverTotal.Value = 0
                            ));
                Invoke(new Action(()
                            => labResolverTotal.Text = 0 + "%"
                             ));
                Invoke(new Action(() => labResolverTotal.Visible = false)
                    );
                Invoke(new Action(() => progressBarResolverTotal.Visible = false)
                    );
                Invoke(new Action(() => butPlayResolverTotal.Visible = false)
                    );
                Invoke(new Action(() => butPausaResolverTotal.Visible = false)
                    );
                Invoke(new Action(() => butStopResolverTotal.Visible = false)
                     );
            }
            }
        

        private void butSolucionTotal_Click(object sender, EventArgs e)
        {

            if (!radEficiente.Checked&&!radExacto.Checked&&!radLibre.Checked)
            {
                MessageBox.Show("seleccione una opcion para resolver");
            }
            else
            {
                if (radExacto.Checked)
                {
                    var opcion = MessageBox.Show("Se calcularan todas las posibilidades y retornara la mejor entre todas" + "\n" +
                                                         "Puede tardar mucho tiempo o nunca responder ¿ desea usar esta opcion ?", "Exploracion Completa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    bool si = false;


                    si = opcion.Equals(DialogResult.Yes);
                         
                    if (si)
                    {
                        Thread hilo = new Thread(solucionTotal);
                        hilo.Start();
                    }

                    }
                if (radEficiente.Checked||radLibre.Checked)
                {
                    Thread hilo = new Thread(solucionTotal);
                    hilo.Start();
                }
               
            }
            

           
       
        }
        private void FuerzaBruta()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => butResolver.Enabled = false));
                DataGridViewSelectedRowCollection filas = null;
                Invoke(new Action(() => filas = tablaViajeros.SelectedRows));
                
                Invoke(new Action(() => filas = tablaViajeros.SelectedRows));
                foreach (DataGridViewRow fila in filas)
                {
                    DataGridViewCell x = null;
                    Invoke(new Action(() => x = fila.Cells[0]));
                    if ( x!= null)
                    {
                        string id = "";
                        string nombreApellido = "";
                        string rutica = "";
                        string inicial = "";
                        Invoke(new Action(() => id = fila.Cells[0].Value.ToString()));
                        Viajero viajero = (Viajero)mundo.Viajeros[id];
                     
                       
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

                        List<Arista<Ciudad>> fuezabruta = viajero.itinerarioExploracionCompleta();
                        string solucionCadena = "";
                        foreach (Arista<Ciudad> arista in fuezabruta)
                        {
                            solucionCadena += arista.Destino1.Nombre + "#";
                        }
                        Invoke(new Action(() => solucionParticular.informacionViajero(id, nombreApellido, rutica,  solucionCadena)));
                        Invoke(new Action(() => labResolverEleccion.Text = "Resolviendo para " + viajero.Nombre));
                        //Invoke(new Action(() => solucionParticular.asignarSolucion(fuezabruta)));
                        Invoke(new Action(() => solucionParticular.dibujarSolucion2(false)));
                        Invoke(new Action(() => solucionParticular.Show()));
                    }
                }
                Invoke(new Action(() => butResolver.Enabled = true));
                Invoke(new Action(() => labResolverEleccion.Visible= false));
            }
        }

        private void kruskal()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => butResolver.Enabled=false));
                DataGridViewSelectedRowCollection filas = null;

                Invoke(new Action(() => filas = tablaViajeros.SelectedRows));
                foreach (DataGridViewRow fila in filas)
                {
                    DataGridViewCell x = null;
                    Invoke(new Action(() => x = fila.Cells[0]));
                    if (x != null)
                    {
                        string id = "";
                        string nombreApellido = "";
                        string rutica = "";
                        string inicial = "";
                        Invoke(new Action(() => id = fila.Cells[0].Value.ToString()));
                        Viajero viajero = (Viajero)mundo.Viajeros[id];
                       

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

                        List<Arista<Ciudad>> kruskal = viajero.itinerarioEficiente();
                        string solucionCadena = "";
                        foreach (Arista<Ciudad> arista in kruskal)
                        {
                            solucionCadena += arista.Destino1.Nombre + "#";
                        }
                        Invoke(new Action(() => labResolverEleccion.Text="Resolviendo para "+viajero.Nombre));
                        Invoke(new Action(() => solucionParticular.informacionViajero(id, nombreApellido, rutica,solucionCadena)));

                        //Invoke(new Action(() => solucionParticular.asignarSolucion(kruskal)));
                        Invoke(new Action(() => solucionParticular.dibujarSolucion2(false)));
                        Invoke(new Action(() => solucionParticular.Show()));
                    }
                }
                Invoke(new Action(() => butResolver.Enabled = true));
                Invoke(new Action(() => labResolverEleccion.Visible = false));
            }
        }
        private void libre()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => butResolver.Enabled = false));
                DataGridViewSelectedRowCollection filas = null;

                Invoke(new Action(() => filas = tablaViajeros.SelectedRows));
                foreach (DataGridViewRow fila in filas)
                {
                    DataGridViewCell x = null;
                    Invoke(new Action(() => x = fila.Cells[0]));
                    if (x != null)
                    {
                        string id = "";
                        string nombreApellido = "";
                        string rutica = "";
                        string inicial = "";
                        Invoke(new Action(() => id = fila.Cells[0].Value.ToString()));
                        Viajero viajero = (Viajero)mundo.Viajeros[id];


                        nombreApellido = viajero.Nombre + " " + viajero.Apellido;

                        var ciudades = viajero.Itinerario.Ciuadades.Values;

                        int i = 0;
                        foreach (Ciudad esta in ciudades)
                        {
                            if (i == 0)
                            {
                                inicial = esta.Nombre;
                            }
                            rutica += esta.Id+"-"+esta.Nombre + "#";
                            i++;
                        }

                        List < Ciudad > libre = viajero.Itinerario.voraz((Ciudad)viajero.Itinerario.CiuadadesGrafo[0]); ;
                        string solucionCadena = "";
                        foreach (Ciudad ciudad in libre)
                        {
                            solucionCadena += ciudad.Id+"-"+ciudad.Nombre + "#";
                        }
                        Invoke(new Action(() => labResolverEleccion.Text = "Resolviendo para " + viajero.Nombre));
                        Invoke(new Action(() => solucionParticular.informacionViajero(id, nombreApellido, rutica, solucionCadena)));

                        Invoke(new Action(() => solucionParticular.asignarSolucion(libre)));
                        Invoke(new Action(() => solucionParticular.dibujarSolucion2(false)));
                        Invoke(new Action(() => solucionParticular.Show()));
                    }
                }
                Invoke(new Action(() => butResolver.Enabled = true));
                Invoke(new Action(() => labResolverEleccion.Visible = false));
            }
        }

        private void butResolver_Click(object sender, EventArgs e)
        {
            

            solucionParticular = new VisualizacionCompleta(this);
            if (!radEficiente.Checked && !radExacto.Checked && !radLibre.Checked)
            {
                MessageBox.Show("seleccione una opcion para resolver");
            }
            else
            {          
            if (tabla)
            {
                if (radEficiente.Checked)
                {
                    butResolver.Enabled = false;
                    labResolverEleccion.Visible = true;
                    hiloKruskal = new Thread(kruskal);
                    hiloKruskal.Start();

                        mapa.Enabled = false;
                        //Thread hilo = new Thread(procesoCargaKruskal);
                        //hilo.Start();                                
                    }
                else if (radExacto.Checked)
                {
                    var opcion = MessageBox.Show("Se calcularan todas las posibilidades y retornara la mejor ente todas" + "\n" +
                                                          "Puede tardar mucho tiempo ¿ desea usar esta opcion ?", "Exploracion Completa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    bool si = false;

                    Invoke(new Action(()
                        => si = opcion.Equals(DialogResult.Yes)
                         ));
                    if (si)
                    {

                        butResolver.Enabled = false;
                        labResolverEleccion.Visible = true;
                        hiloExploracionCompleta = new Thread(FuerzaBruta);
                        hiloExploracionCompleta.Start();
                            mapa.Enabled = false;
                        }

                    //Thread hilo = new Thread(procesoCargaExploracion);
                    //hilo.Start();

                }
                else if (radLibre.Checked)
                {
                    MessageBox.Show("en proceso de desarrollo");
                        hiloLibre = new Thread(libre);
                        butResolver.Enabled = false;
                        hiloLibre.Start();
                       
                        labResolverEleccion.Visible = true;
                      
                        mapa.Enabled = false;
                    }
                else
                {
                    MessageBox.Show("seleccione una opcion para resolver");
                }
            }
        }
        }
   


        //Este método genera las paginas totales de navegacion
        //totalLineas: es la cantidad de lineas que tiene el archivo
        //lote: es la cantidad de registros que se visualizaran la tabla. 
        public long totalPaginas()
        {
            long pag = 0;

            if ((totalViajeros % mundo.CANTIDAD_LOTE) == 0)
            {
                excedentePagina = mundo.CANTIDAD_LOTE;
                pag = totalViajeros / mundo.CANTIDAD_LOTE;
            }
            else
            {
                excedentePagina =Convert.ToInt32( totalViajeros % mundo.CANTIDAD_LOTE);
                pag = (totalViajeros / mundo.CANTIDAD_LOTE) + 1;
            }
            paginas = pag;
            return pag;
        }

        //Buscar pagina
        public void buscarPagina()
        {
            if (InvokeRequired)
            {
                int pag = 0;
                try
                {
                    Invoke(new Action(() => pag = int.Parse(textPagina.Text)));
                }
                catch
                {
                    MessageBox.Show("El formato de página es incorrecto,\n verifique que sea un número valido!");
                }
                int inicio = 0;
                int fin = 0;
                if (pag > 0 && pag <= paginas) 
                {
                    Invoke(new Action(() => tablaViajeros.Rows.Clear()));
                    paginaActual = pag;
                    inicio = (pag - 1) * mundo.CANTIDAD_LOTE;
                    fin = inicio + mundo.CANTIDAD_LOTE - 1;

                    Invoke(new Action(() => labPorcentajeLotes.Visible = true)
                        );
                    Invoke(new Action(() => progressBarLotes.Visible = true)
                        );
                    Invoke(new Action(() => butPlayLotes.Visible = true)
                        );
                    Invoke(new Action(() => butPauseLotes.Visible = true)
                        );
                    Invoke(new Action(() => butStopLotes.Visible = true)
                         );
                    Invoke(new Action(()
                      => progressBarLotes.Maximum = mundo.CANTIDAD_LOTE
                       ));
                    Invoke(new Action(()
                       => progressBarLotes.Value = 50
                        ));
                    Invoke(new Action(()
                       => labPorcentajeLotes.Text="5%"
                        ));
                    mundo.cargarViajeros(mundo.RutaViajeros, inicio, fin);

                    Invoke(new Action(() => tablaViajeros.Rows.Clear()));

                    var viajeros = mundo.Viajeros.Values;

                    
                    foreach (Viajero viajero in viajeros)
                    {

                        string id = viajero.Id;
                        string nombre = viajero.Nombre;
                        string apellido = viajero.Apellido;
                    

                        Invoke(new Action(()
                                    => tablaViajeros.Rows.Add(id, nombre, apellido)
                                     ));

                        long porcen = 0;
                       
                        Invoke(new Action(()
                        => progressBarLotes.Increment(1)
                         ));

                        Invoke(new Action(()
                       => porcen = (progressBarLotes.Value * 100) / mundo.CANTIDAD_LOTE
                       ));

                        Invoke(new Action(()
                        => labPorcentajeLotes.Text = porcen + " %"
                       ));
                    }
                    
                    Invoke(new Action(()
                       => progressBarLotes.Value = mundo.CANTIDAD_LOTE
                        ));
                    Invoke(new Action(()
                                => labPorcentajeLotes.Text = 100 + "%"
                                 ));
                    Invoke(new Action(()
                     => labPaginas.Text = "Pagina " + paginaActual
                     ));

                    //MessageBox.Show("Completado!!");
                    Invoke(new Action(()
                               => progressBarLotes.Value = 0
                                ));
                    Invoke(new Action(()
                                => labPorcentajeLotes.Text = 0 + "%"
                                 ));
                    Invoke(new Action(()
                                => butAnterior.Enabled = true
                                 ));
                    Invoke(new Action(()
                                => butSiguiente.Enabled = true
                                 ));
                    Invoke(new Action(()
                               => butPagina.Enabled = true
                                ));
                    Invoke(new Action(() => labPorcentajeLotes.Visible = false)
                        );
                    Invoke(new Action(() => progressBarLotes.Visible = false)
                        );
                    Invoke(new Action(() => butPlayLotes.Visible = false)
                        );
                    Invoke(new Action(() => butPauseLotes.Visible = false)
                        );
                    Invoke(new Action(() => butStopLotes.Visible = false)
                         );
                    Invoke(new Action(()
                  => labPaginas.Visible = true
                 ));
                    Invoke(new Action(()
                    => labTotalpagina.Visible = true
                   ));
                }
                else
                {
                    MessageBox.Show("Por favor digite una pagina correcta");
                    Invoke(new Action(()
                               => butAnterior.Enabled = true
                                ));
                    Invoke(new Action(()
                                => butSiguiente.Enabled = true
                                 ));
                    Invoke(new Action(()
                               => butPagina.Enabled = true
                                ));                   
                }               
            }

        }

        private void butPagina_Click(object sender, EventArgs e)
        {
            try
            {
                int x = Int32.Parse(textPagina.Text);
                if (x!=paginaActual)
                {
                    hiloIrPagina = new Thread(buscarPagina);
                    butAnterior.Enabled = false;
                    butSiguiente.Enabled = false;
                    butPagina.Enabled = false;
                    hiloIrPagina.Start();
                }                
            }
            catch
            {
                MessageBox.Show("digite una pagina correcta");
            }
      
        }

        private void cargarCiudadesTool_Click(object sender, EventArgs e)
        {
            if (cargoCiudades)
            {
                MessageBox.Show("Las ciudades ya se encuentran cargadas");

            }
            else
            {
                hiloCargaCiudades = new Thread(cargarCiudad);
                hiloCargaCiudades.Start();
                       
                cargoCiudades = true;
            }
        }

        private void cargarViajerosTool_Click(object sender, EventArgs e)
        {
            
        }

        private void filtroTool_Click(object sender, EventArgs e)
        {
           
        }

        private void textFiltro_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                mapa.Overlays.Clear();
                hilofiltroCiudades = new Thread(DibujarFiltro);
                if (hilofiltroCiudades != null && !hilofiltroCiudades.IsAlive)
                {
                    hilofiltroCiudades.Start();
                }
                else
                {
                    MessageBox.Show("estamos cumpliendo su peticion, por favor espere un momento");
                }

            }
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int eleccion = int.Parse(toolStripComboBox1.SelectedItem.ToString());
            //toolStripComboBox1.DropDownStyle
            mundo.CANTIDAD_LOTE = eleccion;

            if (cargoViajeros)
            {
                MessageBox.Show("Los viajeros ya se encuentran cargados");
            }
            else
            {
                paginaActual = 1;
                hiloCargaViajeros = new Thread(cargarViajeros);

                hiloCargaViajeros.Start();
                cargoViajeros = true;
            }



        }

        private void textPagina_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                try
                {
                    int x = Int32.Parse(textPagina.Text);
                    if (x != paginaActual)
                    {
                        hiloIrPagina = new Thread(buscarPagina);
                        butAnterior.Enabled = false;
                        butSiguiente.Enabled = false;
                        butPagina.Enabled = false;
                        hiloIrPagina.Start();
                    }
                }
                catch
                {
                    MessageBox.Show("digite una pagina correcta");
                }

            }
        }

        private void misionAplicacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("World Airlines ofrece a sus usuarios la posiblidad de planear" + "\n" +
                                  "sus viajes con tiempo, aconsejando la manera mas rapida" + "\n" +
                                    "de recorrer sus sitios de interes");
        }

        private void desarrolladoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Equipo de desarrollo :" + "\n" +
                            "Esteban Aguirre" + "\n" +
                            "Jefry Cardona" + "\n" +
                            "Esteban Camacho" + "\n" +
                            "Ray Torres" + "\n" +
                            "Jorge Hernandez");
        }

        private void Ventana_FormClosed(object sender, FormClosedEventArgs e)
        {
            
            Dispose();
        
            //MessageBox.Show("cerrando");
        }

        private void mapa_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Ruta grafito = mundo.Grafo;
            GMapOverlay marcadores = new GMapOverlay("ciudades2");
            GMapOverlay lineas = new GMapOverlay("rutas");
            if (radEfi.Checked)
            {
                grafito.kruskalOpcion1();
              var x=  grafito.rutaViajero(0);
                foreach (Arista<Ciudad> arista in x)
                {

                    Ciudad inicio = arista.Destino1;
                    Ciudad fin = arista.Destino2;
                    double distancia = Math.Round(arista.Distancia, 2);
                    GMarkerGoogle iniciom = null;
                    GMarkerGoogle finm = null;

                    iniciom = new GMarkerGoogle(new PointLatLng(inicio.Latitud, inicio.Longitud), GMarkerGoogleType.blue_dot);

                    finm = new GMarkerGoogle(new PointLatLng(fin.Latitud, fin.Longitud), GMarkerGoogleType.blue_dot);
                    iniciom.ToolTipText = "Nombre: " + inicio.Nombre + "\n" +
                                        "Latitud : " + inicio.Latitud + "\n" +
                                        "Longitud : " + inicio.Longitud + "\n" +
                                        "Poblacion : " + inicio.TotalPoblacion;
                    finm.ToolTipText = "Nombre: " + fin.Nombre + "\n" +
                                        "Latitud : " + fin.Latitud + "\n" +
                                        "Longitud : " + fin.Longitud + "\n" +
                                        "Poblacion : " + fin.TotalPoblacion;
                    marcadores.Markers.Add(iniciom);
                    marcadores.Markers.Add(finm);
                    List<PointLatLng> points = new List<PointLatLng>();
                    points.Add(new PointLatLng(inicio.Latitud, inicio.Longitud));
                    points.Add(new PointLatLng(fin.Latitud, fin.Longitud));
                    GMapPolygon polygon = new GMapPolygon(points, "mypolygon");
                    polygon.Fill = new SolidBrush(Color.FromArgb(50, Color.Red));
                    polygon.Stroke = new Pen(Color.Red, 5);
                    lineas.Polygons.Add(polygon);
                    double lat = (inicio.Latitud + fin.Latitud) / 2;
                    double lon = (inicio.Longitud + fin.Longitud) / 2;
                    PointLatLng puntoDistancia = new PointLatLng(lat, lon);
                    GMarkerCross marcadorDistancia = new GMarkerCross(puntoDistancia);
                    marcadorDistancia.ToolTipText = distancia + " Km";
                    marcadorDistancia.ToolTipMode = MarkerTooltipMode.Always;

                    marcadores.Markers.Add(marcadorDistancia);
                    mapa.Overlays.Add(marcadores);
                    mapa.Overlays.Add(lineas);
                   
                }
            }
            else if (radExac.Checked)
            {
                MessageBox.Show("En proceso de desarrollo");
            }
            else if (radFree.Checked)
            {
                MessageBox.Show("En proceso de desarrollo");
            }
            else 
            {
                MessageBox.Show("Seleccione una opcion por favor");
            }
        }

        private void radFree_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void tablaViajeros_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //mapa.Overlays.First(x => x.Id == "ciudades2").Clear();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //private void playSimpleSound()
        //{
        //    SoundPlayer simpleSound = new SoundPlayer(@"c:\Windows\Media\chimes.wav");
        //    simpleSound.Play();
        //}
    }
}