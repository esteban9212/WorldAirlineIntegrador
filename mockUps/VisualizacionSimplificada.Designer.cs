namespace mockUps
{
    partial class VisualizacionSimplificada
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VisualizacionSimplificada));
            this.tablaSoluciones = new System.Windows.Forms.DataGridView();
            this.ColId2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColNomb2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColApellido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.butPauseSolucion = new System.Windows.Forms.Button();
            this.butSiguienteSolucion = new System.Windows.Forms.Button();
            this.butStopSolucion = new System.Windows.Forms.Button();
            this.butAnteriorSolucion = new System.Windows.Forms.Button();
            this.butPlaySolucion = new System.Windows.Forms.Button();
            this.labPorcentajeSolucion = new System.Windows.Forms.Label();
            this.progressBarSolucion = new System.Windows.Forms.ProgressBar();
            this.labTotalpaginaSolucion = new System.Windows.Forms.Label();
            this.butPaginaSolucion = new System.Windows.Forms.Button();
            this.textPaginaSolucion = new System.Windows.Forms.TextBox();
            this.labPaginasSolucion = new System.Windows.Forms.Label();
            this.listBoxCiudades = new System.Windows.Forms.ListBox();
            this.groupBoxCiudades = new System.Windows.Forms.GroupBox();
            this.groupBoxSoluciones = new System.Windows.Forms.GroupBox();
            this.listBoxSoluciones = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.tablaSoluciones)).BeginInit();
            this.groupBoxCiudades.SuspendLayout();
            this.groupBoxSoluciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // tablaSoluciones
            // 
            this.tablaSoluciones.AllowUserToAddRows = false;
            this.tablaSoluciones.AllowUserToDeleteRows = false;
            this.tablaSoluciones.AllowUserToResizeColumns = false;
            this.tablaSoluciones.AllowUserToResizeRows = false;
            this.tablaSoluciones.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tablaSoluciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tablaSoluciones.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColId2,
            this.ColNomb2,
            this.ColApellido});
            this.tablaSoluciones.Location = new System.Drawing.Point(12, 22);
            this.tablaSoluciones.MultiSelect = false;
            this.tablaSoluciones.Name = "tablaSoluciones";
            this.tablaSoluciones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tablaSoluciones.Size = new System.Drawing.Size(391, 276);
            this.tablaSoluciones.TabIndex = 5;
            this.tablaSoluciones.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tablaSoluciones_CellClick);
            // 
            // ColId2
            // 
            this.ColId2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.ColId2.HeaderText = "ID";
            this.ColId2.Name = "ColId2";
            this.ColId2.Width = 43;
            // 
            // ColNomb2
            // 
            this.ColNomb2.HeaderText = "NOMBRE";
            this.ColNomb2.Name = "ColNomb2";
            // 
            // ColApellido
            // 
            this.ColApellido.HeaderText = "APELLIDO";
            this.ColApellido.Name = "ColApellido";
            // 
            // butPauseSolucion
            // 
            this.butPauseSolucion.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("butPauseSolucion.BackgroundImage")));
            this.butPauseSolucion.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butPauseSolucion.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ButtonFace;
            this.butPauseSolucion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butPauseSolucion.Location = new System.Drawing.Point(335, 369);
            this.butPauseSolucion.Name = "butPauseSolucion";
            this.butPauseSolucion.Size = new System.Drawing.Size(21, 23);
            this.butPauseSolucion.TabIndex = 30017;
            this.butPauseSolucion.UseVisualStyleBackColor = true;
            // 
            // butSiguienteSolucion
            // 
            this.butSiguienteSolucion.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("butSiguienteSolucion.BackgroundImage")));
            this.butSiguienteSolucion.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butSiguienteSolucion.FlatAppearance.BorderSize = 0;
            this.butSiguienteSolucion.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ButtonFace;
            this.butSiguienteSolucion.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.butSiguienteSolucion.Location = new System.Drawing.Point(348, 319);
            this.butSiguienteSolucion.Name = "butSiguienteSolucion";
            this.butSiguienteSolucion.Size = new System.Drawing.Size(35, 23);
            this.butSiguienteSolucion.TabIndex = 30012;
            this.butSiguienteSolucion.UseVisualStyleBackColor = true;
            this.butSiguienteSolucion.Click += new System.EventHandler(this.butSiguienteSolucion_Click);
            // 
            // butStopSolucion
            // 
            this.butStopSolucion.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("butStopSolucion.BackgroundImage")));
            this.butStopSolucion.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butStopSolucion.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ButtonFace;
            this.butStopSolucion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butStopSolucion.Location = new System.Drawing.Point(362, 369);
            this.butStopSolucion.Name = "butStopSolucion";
            this.butStopSolucion.Size = new System.Drawing.Size(21, 23);
            this.butStopSolucion.TabIndex = 30016;
            this.butStopSolucion.UseVisualStyleBackColor = true;
            // 
            // butAnteriorSolucion
            // 
            this.butAnteriorSolucion.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("butAnteriorSolucion.BackgroundImage")));
            this.butAnteriorSolucion.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butAnteriorSolucion.FlatAppearance.BorderSize = 0;
            this.butAnteriorSolucion.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ButtonFace;
            this.butAnteriorSolucion.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.butAnteriorSolucion.Location = new System.Drawing.Point(169, 319);
            this.butAnteriorSolucion.Name = "butAnteriorSolucion";
            this.butAnteriorSolucion.Size = new System.Drawing.Size(35, 23);
            this.butAnteriorSolucion.TabIndex = 30011;
            this.butAnteriorSolucion.UseVisualStyleBackColor = true;
            this.butAnteriorSolucion.Click += new System.EventHandler(this.butAnteriorSolucion_Click);
            // 
            // butPlaySolucion
            // 
            this.butPlaySolucion.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("butPlaySolucion.BackgroundImage")));
            this.butPlaySolucion.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butPlaySolucion.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ButtonFace;
            this.butPlaySolucion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butPlaySolucion.Location = new System.Drawing.Point(308, 369);
            this.butPlaySolucion.Name = "butPlaySolucion";
            this.butPlaySolucion.Size = new System.Drawing.Size(21, 23);
            this.butPlaySolucion.TabIndex = 30013;
            this.butPlaySolucion.UseVisualStyleBackColor = true;
            // 
            // labPorcentajeSolucion
            // 
            this.labPorcentajeSolucion.AutoSize = true;
            this.labPorcentajeSolucion.Location = new System.Drawing.Point(166, 373);
            this.labPorcentajeSolucion.Name = "labPorcentajeSolucion";
            this.labPorcentajeSolucion.Size = new System.Drawing.Size(21, 13);
            this.labPorcentajeSolucion.TabIndex = 30015;
            this.labPorcentajeSolucion.Text = "0%";
            // 
            // progressBarSolucion
            // 
            this.progressBarSolucion.Location = new System.Drawing.Point(197, 368);
            this.progressBarSolucion.Name = "progressBarSolucion";
            this.progressBarSolucion.Size = new System.Drawing.Size(109, 23);
            this.progressBarSolucion.TabIndex = 30014;
            // 
            // labTotalpaginaSolucion
            // 
            this.labTotalpaginaSolucion.AutoSize = true;
            this.labTotalpaginaSolucion.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labTotalpaginaSolucion.Location = new System.Drawing.Point(287, 346);
            this.labTotalpaginaSolucion.Name = "labTotalpaginaSolucion";
            this.labTotalpaginaSolucion.Size = new System.Drawing.Size(24, 15);
            this.labTotalpaginaSolucion.TabIndex = 30023;
            this.labTotalpaginaSolucion.Text = "De ";
            // 
            // butPaginaSolucion
            // 
            this.butPaginaSolucion.FlatAppearance.BorderSize = 0;
            this.butPaginaSolucion.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ButtonFace;
            this.butPaginaSolucion.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.butPaginaSolucion.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butPaginaSolucion.Location = new System.Drawing.Point(283, 319);
            this.butPaginaSolucion.Name = "butPaginaSolucion";
            this.butPaginaSolucion.Size = new System.Drawing.Size(59, 22);
            this.butPaginaSolucion.TabIndex = 30022;
            this.butPaginaSolucion.Text = "Ir a";
            this.butPaginaSolucion.UseVisualStyleBackColor = true;
            this.butPaginaSolucion.Click += new System.EventHandler(this.butPaginaSolucion_Click);
            // 
            // textPaginaSolucion
            // 
            this.textPaginaSolucion.Location = new System.Drawing.Point(221, 319);
            this.textPaginaSolucion.Name = "textPaginaSolucion";
            this.textPaginaSolucion.Size = new System.Drawing.Size(47, 20);
            this.textPaginaSolucion.TabIndex = 30020;
            // 
            // labPaginasSolucion
            // 
            this.labPaginasSolucion.AutoSize = true;
            this.labPaginasSolucion.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labPaginasSolucion.Location = new System.Drawing.Point(218, 346);
            this.labPaginasSolucion.Name = "labPaginasSolucion";
            this.labPaginasSolucion.Size = new System.Drawing.Size(38, 15);
            this.labPaginasSolucion.TabIndex = 30021;
            this.labPaginasSolucion.Text = "Página";
            // 
            // listBoxCiudades
            // 
            this.listBoxCiudades.FormattingEnabled = true;
            this.listBoxCiudades.ItemHeight = 15;
            this.listBoxCiudades.Location = new System.Drawing.Point(6, 13);
            this.listBoxCiudades.Name = "listBoxCiudades";
            this.listBoxCiudades.Size = new System.Drawing.Size(199, 94);
            this.listBoxCiudades.TabIndex = 30024;
            // 
            // groupBoxCiudades
            // 
            this.groupBoxCiudades.Controls.Add(this.listBoxCiudades);
            this.groupBoxCiudades.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxCiudades.Location = new System.Drawing.Point(409, 22);
            this.groupBoxCiudades.Name = "groupBoxCiudades";
            this.groupBoxCiudades.Size = new System.Drawing.Size(216, 112);
            this.groupBoxCiudades.TabIndex = 30025;
            this.groupBoxCiudades.TabStop = false;
            this.groupBoxCiudades.Text = "Ciudades a visitar";
            // 
            // groupBoxSoluciones
            // 
            this.groupBoxSoluciones.Controls.Add(this.listBoxSoluciones);
            this.groupBoxSoluciones.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxSoluciones.Location = new System.Drawing.Point(409, 138);
            this.groupBoxSoluciones.Name = "groupBoxSoluciones";
            this.groupBoxSoluciones.Size = new System.Drawing.Size(216, 118);
            this.groupBoxSoluciones.TabIndex = 30026;
            this.groupBoxSoluciones.TabStop = false;
            this.groupBoxSoluciones.Text = "Orden de recorrido recomendado";
            // 
            // listBoxSoluciones
            // 
            this.listBoxSoluciones.FormattingEnabled = true;
            this.listBoxSoluciones.ItemHeight = 15;
            this.listBoxSoluciones.Location = new System.Drawing.Point(6, 14);
            this.listBoxSoluciones.Name = "listBoxSoluciones";
            this.listBoxSoluciones.Size = new System.Drawing.Size(199, 94);
            this.listBoxSoluciones.TabIndex = 30024;
            // 
            // button1
            // 
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ButtonFace;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(457, 262);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(132, 36);
            this.button1.TabIndex = 30027;
            this.button1.Text = "Ver Solucion";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // VisualizacionSimplificada
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(626, 403);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBoxSoluciones);
            this.Controls.Add(this.groupBoxCiudades);
            this.Controls.Add(this.labTotalpaginaSolucion);
            this.Controls.Add(this.butPaginaSolucion);
            this.Controls.Add(this.textPaginaSolucion);
            this.Controls.Add(this.labPaginasSolucion);
            this.Controls.Add(this.butPauseSolucion);
            this.Controls.Add(this.butSiguienteSolucion);
            this.Controls.Add(this.butStopSolucion);
            this.Controls.Add(this.butAnteriorSolucion);
            this.Controls.Add(this.butPlaySolucion);
            this.Controls.Add(this.labPorcentajeSolucion);
            this.Controls.Add(this.progressBarSolucion);
            this.Controls.Add(this.tablaSoluciones);
            this.Name = "VisualizacionSimplificada";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Visualizacion Simplificada";
            this.Load += new System.EventHandler(this.VisualizacionSimplificada_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tablaSoluciones)).EndInit();
            this.groupBoxCiudades.ResumeLayout(false);
            this.groupBoxSoluciones.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView tablaSoluciones;
        private System.Windows.Forms.Button butPauseSolucion;
        private System.Windows.Forms.Button butSiguienteSolucion;
        private System.Windows.Forms.Button butStopSolucion;
        private System.Windows.Forms.Button butAnteriorSolucion;
        private System.Windows.Forms.Button butPlaySolucion;
        private System.Windows.Forms.Label labPorcentajeSolucion;
        private System.Windows.Forms.ProgressBar progressBarSolucion;
        private System.Windows.Forms.Label labTotalpaginaSolucion;
        private System.Windows.Forms.Button butPaginaSolucion;
        private System.Windows.Forms.TextBox textPaginaSolucion;
        private System.Windows.Forms.Label labPaginasSolucion;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColId2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColNomb2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColApellido;
        private System.Windows.Forms.ListBox listBoxCiudades;
        private System.Windows.Forms.GroupBox groupBoxCiudades;
        private System.Windows.Forms.GroupBox groupBoxSoluciones;
        private System.Windows.Forms.ListBox listBoxSoluciones;
        private System.Windows.Forms.Button button1;
    }
}