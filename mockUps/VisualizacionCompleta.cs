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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mockUps
{
    public partial class VisualizacionCompleta : Form
    {

        private List<Ciudad> grafoViajero;
        private Ventana ventana;
        public VisualizacionCompleta(Ventana x)
        {
            ventana = x;
            InitializeComponent();
          
        }

        public void asignarSolucion(List<Ciudad> solucion)
        {
            grafoViajero = solucion;
        }


        private void VisualizacionCompleta_Load(object sender, EventArgs e)
        {
            mapaSolucion.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            mapaSolucion.DragButton = MouseButtons.Left;
            checkBoxVista.Text = "Ver Destinos" + "\n" +
                                  "con rutas" + "\n" +
                                  "y distancias";
        }

        public void informacionViajero(string i,string n,string ciu,string solucion)
        {
            labId.Text += " "+i;
            labNombre.Text += " " + n;

            foreach (string ciudad in ciu.Split('#'))
            {
                listCiudades.Items.Add(ciudad);
            }
            foreach (string ciudad in solucion.Split('#'))
            {
                listSolucion.Items.Add(ciudad);
            }

        }

        public void dibujarSolucion(bool ponderado)
        {

            mapaSolucion.Overlays.Clear();

            GMapOverlay marcadores = new GMapOverlay("ciudades");
           
            GMapOverlay lineas = new GMapOverlay("rutas");

            for (int i =0;i<grafoViajero.Count;i++)
            {

                Ciudad ciudad = grafoViajero.ElementAt(i);
                GMarkerGoogle iniciom = null;
                if (i==0)
                {
                   iniciom = new GMarkerGoogle(new PointLatLng(ciudad.Latitud, ciudad.Longitud), GMarkerGoogleType.red_dot);
                }
                else
                {
                   iniciom = new GMarkerGoogle(new PointLatLng(ciudad.Latitud, ciudad.Longitud), GMarkerGoogleType.blue_dot);
                }
               
                iniciom.ToolTipText = "Nombre: " + ciudad.Nombre + "\n" +
                                      "Latitud : " + ciudad.Latitud + "\n" +
                                      "Longitud : " + ciudad.Longitud + "\n" +
                                      "Poblacion : " + ciudad.TotalPoblacion;
                marcadores.Markers.Add(iniciom);

                if (ponderado)
                {
                    Ciudad otraciudad = null;
                    if (i==grafoViajero.Count-1)
                    {
                        otraciudad = grafoViajero.ElementAt(0);

                    }
                    else
                    {
                        otraciudad = grafoViajero.ElementAt(i + 1);
                    }

                    List<PointLatLng> points = new List<PointLatLng>();
                    points.Add(new PointLatLng(ciudad.Latitud, ciudad.Longitud));
                    points.Add(new PointLatLng(otraciudad.Latitud, otraciudad.Longitud));
                    GMapPolygon polygon = new GMapPolygon(points, "mypolygon");
                    polygon.Fill = new SolidBrush(Color.FromArgb(50, Color.Red));
                    polygon.Stroke = new Pen(Color.Red, 5);
                    lineas.Polygons.Add(polygon);
                    double lat = (ciudad.Latitud + otraciudad.Latitud) / 2;
                    double lon = (ciudad.Longitud + otraciudad.Longitud) / 2;
                    PointLatLng puntoDistancia = new PointLatLng(lat, lon);
                    GMarkerCross marcadorDistancia = new GMarkerCross(puntoDistancia);

                    double distancia = Math.Round(ciudad.distancia(otraciudad, 'K'),2);

                    marcadorDistancia.ToolTipText = distancia + " Km";
                    marcadorDistancia.ToolTipMode = MarkerTooltipMode.Always;

                    marcadores.Markers.Add(marcadorDistancia);
                    
                    mapaSolucion.Overlays.Add(lineas);

                }

                mapaSolucion.Overlays.Add(marcadores);

            }      
               
            mapaSolucion.Zoom = 1;
            mapaSolucion.Zoom = 0;
        }


        private void mapaSolucion_Load(object sender, EventArgs e)
        {

        }

        private void checkBoxVista_CheckedChanged(object sender, EventArgs e)
        {
            mapaSolucion.Overlays.Clear();
            dibujarSolucion(checkBoxVista.Checked);
        }

        private void listSolucion_SelectedIndexChanged(object sender, EventArgs e)
        {
            string seleccion = listSolucion.SelectedItem.ToString();
           

            Ciudad ciudadInicio = null;
            foreach (Ciudad city in grafoViajero)
            {
                if (city.Id.Equals(seleccion))
                {
                    ciudadInicio = city;
                    break;
                }
            }


            grafoViajero = ventana.reordenar(grafoViajero, ciudadInicio);
            dibujarSolucion(checkBoxVista.Checked);

        }

      
       

        private void VisualizacionCompleta_FormClosed(object sender, FormClosedEventArgs e)
        {
            ventana.Mapa.Enabled = true;
        }
    }
}
