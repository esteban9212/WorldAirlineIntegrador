namespace mockUps
{
    partial class VisualizacionCompleta
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labNombre = new System.Windows.Forms.Label();
            this.labId = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.listCiudades = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listSolucion = new System.Windows.Forms.ListBox();
            this.checkBoxVista = new System.Windows.Forms.CheckBox();
            this.mapaSolucion = new GMap.NET.WindowsForms.GMapControl();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.SuspendLayout();
            // 
            // labNombre
            // 
            this.labNombre.AutoSize = true;
            this.labNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labNombre.Location = new System.Drawing.Point(15, 68);
            this.labNombre.Name = "labNombre";
            this.labNombre.Size = new System.Drawing.Size(71, 16);
            this.labNombre.TabIndex = 0;
            this.labNombre.Text = "Nombre :";
            // 
            // labId
            // 
            this.labId.AutoSize = true;
            this.labId.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labId.Location = new System.Drawing.Point(15, 26);
            this.labId.Name = "labId";
            this.labId.Size = new System.Drawing.Size(33, 16);
            this.labId.TabIndex = 1;
            this.labId.Text = "Id : ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.checkBoxVista);
            this.groupBox1.Controls.Add(this.labNombre);
            this.groupBox1.Controls.Add(this.labId);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(593, 243);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Viajero :";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.listCiudades);
            this.groupBox3.Location = new System.Drawing.Point(18, 110);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(192, 125);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Ciudades a visitar";
            // 
            // listCiudades
            // 
            this.listCiudades.FormattingEnabled = true;
            this.listCiudades.Location = new System.Drawing.Point(16, 19);
            this.listCiudades.Name = "listCiudades";
            this.listCiudades.Size = new System.Drawing.Size(170, 95);
            this.listCiudades.TabIndex = 6;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listSolucion);
            this.groupBox2.Location = new System.Drawing.Point(216, 110);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 125);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Itinerario Propuesto";
            // 
            // listSolucion
            // 
            this.listSolucion.FormattingEnabled = true;
            this.listSolucion.Location = new System.Drawing.Point(6, 19);
            this.listSolucion.Name = "listSolucion";
            this.listSolucion.Size = new System.Drawing.Size(188, 95);
            this.listSolucion.TabIndex = 7;
            this.listSolucion.SelectedIndexChanged += new System.EventHandler(this.listSolucion_SelectedIndexChanged);
            // 
            // checkBoxVista
            // 
            this.checkBoxVista.AutoSize = true;
            this.checkBoxVista.Location = new System.Drawing.Point(462, 156);
            this.checkBoxVista.Name = "checkBoxVista";
            this.checkBoxVista.Size = new System.Drawing.Size(80, 17);
            this.checkBoxVista.TabIndex = 5;
            this.checkBoxVista.Text = "checkBox1";
            this.checkBoxVista.UseVisualStyleBackColor = true;
            this.checkBoxVista.CheckedChanged += new System.EventHandler(this.checkBoxVista_CheckedChanged);
            // 
            // mapaSolucion
            // 
            this.mapaSolucion.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.mapaSolucion.Bearing = 0F;
            this.mapaSolucion.CanDragMap = true;
            this.mapaSolucion.CausesValidation = false;
            this.mapaSolucion.EmptyTileColor = System.Drawing.Color.Navy;
            this.mapaSolucion.GrayScaleMode = false;
            this.mapaSolucion.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.mapaSolucion.LevelsKeepInMemmory = 5;
            this.mapaSolucion.Location = new System.Drawing.Point(13, 261);
            this.mapaSolucion.MarkersEnabled = true;
            this.mapaSolucion.MaxZoom = 30;
            this.mapaSolucion.MinZoom = 2;
            this.mapaSolucion.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.mapaSolucion.Name = "mapaSolucion";
            this.mapaSolucion.NegativeMode = false;
            this.mapaSolucion.PolygonsEnabled = true;
            this.mapaSolucion.RetryLoadTile = 0;
            this.mapaSolucion.RoutesEnabled = true;
            this.mapaSolucion.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.mapaSolucion.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.mapaSolucion.ShowTileGridLines = false;
            this.mapaSolucion.Size = new System.Drawing.Size(592, 316);
            this.mapaSolucion.TabIndex = 8;
            this.mapaSolucion.Zoom = 2D;
            this.mapaSolucion.Load += new System.EventHandler(this.mapaSolucion_Load);
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // VisualizacionCompleta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(608, 577);
            this.Controls.Add(this.mapaSolucion);
            this.Controls.Add(this.groupBox1);
            this.Name = "VisualizacionCompleta";
            this.Text = "Visualizacion Completa";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.VisualizacionCompleta_FormClosed);
            this.Load += new System.EventHandler(this.VisualizacionCompleta_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labNombre;
        private System.Windows.Forms.Label labId;
        private System.Windows.Forms.GroupBox groupBox1;
        private GMap.NET.WindowsForms.GMapControl mapaSolucion;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.CheckBox checkBoxVista;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox listCiudades;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox listSolucion;
    }
}