namespace SistemaMAPAN
{
    partial class frmPrincipal
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
            this.pnlEsquerda = new System.Windows.Forms.Panel();
            this.dgvEstoque = new System.Windows.Forms.DataGridView();
            this.dgcNome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcEstoque = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcQuantidade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblEstoque = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.pnlDireita = new System.Windows.Forms.Panel();
            this.lblInfo = new System.Windows.Forms.Label();
            this.rdbVistoria = new System.Windows.Forms.RadioButton();
            this.rdbEvento = new System.Windows.Forms.RadioButton();
            this.dgvEvento = new System.Windows.Forms.DataGridView();
            this.dgcData = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcTitulo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.sistemaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loginToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.novoToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.sairToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.animaisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.novoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.consultaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transferenciaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adoçãoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.novaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.consultaToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.protetorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.novoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.consultaToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.agendaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.novoToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.eventoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vistoriaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.consultaToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.doaçõesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colaboradoresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.consultaToolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.denunciaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.novaToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.consultaToolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.estoqueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.produtosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.entregasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configuraçãoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.estatísticasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.financeiraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adoçõesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlEsquerda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEstoque)).BeginInit();
            this.pnlDireita.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEvento)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlEsquerda
            // 
            this.pnlEsquerda.BackColor = System.Drawing.Color.Transparent;
            this.pnlEsquerda.Controls.Add(this.dgvEstoque);
            this.pnlEsquerda.Controls.Add(this.lblEstoque);
            this.pnlEsquerda.Controls.Add(this.lblUsuario);
            this.pnlEsquerda.Location = new System.Drawing.Point(0, 29);
            this.pnlEsquerda.Name = "pnlEsquerda";
            this.pnlEsquerda.Size = new System.Drawing.Size(321, 722);
            this.pnlEsquerda.TabIndex = 3;
            // 
            // dgvEstoque
            // 
            this.dgvEstoque.AllowUserToAddRows = false;
            this.dgvEstoque.AllowUserToDeleteRows = false;
            this.dgvEstoque.AllowUserToResizeColumns = false;
            this.dgvEstoque.AllowUserToResizeRows = false;
            this.dgvEstoque.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcNome,
            this.dgcEstoque,
            this.dgcQuantidade});
            this.dgvEstoque.Location = new System.Drawing.Point(6, 97);
            this.dgvEstoque.MultiSelect = false;
            this.dgvEstoque.Name = "dgvEstoque";
            this.dgvEstoque.ReadOnly = true;
            this.dgvEstoque.RowHeadersVisible = false;
            this.dgvEstoque.Size = new System.Drawing.Size(304, 357);
            this.dgvEstoque.TabIndex = 2;
            this.dgvEstoque.Paint += new System.Windows.Forms.PaintEventHandler(this.dataGridView1_Paint);
            // 
            // dgcNome
            // 
            this.dgcNome.HeaderText = "Nome";
            this.dgcNome.Name = "dgcNome";
            this.dgcNome.ReadOnly = true;
            this.dgcNome.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgcNome.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgcEstoque
            // 
            this.dgcEstoque.HeaderText = "Estoque";
            this.dgcEstoque.Name = "dgcEstoque";
            this.dgcEstoque.ReadOnly = true;
            this.dgcEstoque.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgcEstoque.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgcQuantidade
            // 
            this.dgcQuantidade.HeaderText = "Quantidade";
            this.dgcQuantidade.Name = "dgcQuantidade";
            this.dgcQuantidade.ReadOnly = true;
            this.dgcQuantidade.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgcQuantidade.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // lblEstoque
            // 
            this.lblEstoque.AutoSize = true;
            this.lblEstoque.Location = new System.Drawing.Point(3, 81);
            this.lblEstoque.Name = "lblEstoque";
            this.lblEstoque.Size = new System.Drawing.Size(160, 13);
            this.lblEstoque.TabIndex = 1;
            this.lblEstoque.Text = "Produtos em quantidade minima:";
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(3, 7);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(0, 13);
            this.lblUsuario.TabIndex = 0;
            // 
            // pnlDireita
            // 
            this.pnlDireita.BackColor = System.Drawing.Color.Transparent;
            this.pnlDireita.Controls.Add(this.lblInfo);
            this.pnlDireita.Controls.Add(this.rdbVistoria);
            this.pnlDireita.Controls.Add(this.rdbEvento);
            this.pnlDireita.Controls.Add(this.dgvEvento);
            this.pnlDireita.Controls.Add(this.monthCalendar1);
            this.pnlDireita.Location = new System.Drawing.Point(477, 32);
            this.pnlDireita.Name = "pnlDireita";
            this.pnlDireita.Size = new System.Drawing.Size(246, 722);
            this.pnlDireita.TabIndex = 4;
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(6, 276);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(35, 13);
            this.lblInfo.TabIndex = 4;
            this.lblInfo.Text = "label1";
            // 
            // rdbVistoria
            // 
            this.rdbVistoria.AutoSize = true;
            this.rdbVistoria.Location = new System.Drawing.Point(177, 246);
            this.rdbVistoria.Name = "rdbVistoria";
            this.rdbVistoria.Size = new System.Drawing.Size(59, 17);
            this.rdbVistoria.TabIndex = 3;
            this.rdbVistoria.TabStop = true;
            this.rdbVistoria.Text = "Vistoria";
            this.rdbVistoria.UseVisualStyleBackColor = true;
            this.rdbVistoria.CheckedChanged += new System.EventHandler(this.rdbVistoria_CheckedChanged);
            // 
            // rdbEvento
            // 
            this.rdbEvento.AutoSize = true;
            this.rdbEvento.Location = new System.Drawing.Point(9, 246);
            this.rdbEvento.Name = "rdbEvento";
            this.rdbEvento.Size = new System.Drawing.Size(59, 17);
            this.rdbEvento.TabIndex = 2;
            this.rdbEvento.TabStop = true;
            this.rdbEvento.Text = "Evento";
            this.rdbEvento.UseVisualStyleBackColor = true;
            this.rdbEvento.CheckedChanged += new System.EventHandler(this.rdbEvento_CheckedChanged);
            // 
            // dgvEvento
            // 
            this.dgvEvento.AllowUserToAddRows = false;
            this.dgvEvento.AllowUserToDeleteRows = false;
            this.dgvEvento.AllowUserToResizeColumns = false;
            this.dgvEvento.AllowUserToResizeRows = false;
            this.dgvEvento.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcData,
            this.dgcTitulo});
            this.dgvEvento.Location = new System.Drawing.Point(9, 292);
            this.dgvEvento.Name = "dgvEvento";
            this.dgvEvento.ReadOnly = true;
            this.dgvEvento.RowHeadersVisible = false;
            this.dgvEvento.Size = new System.Drawing.Size(227, 357);
            this.dgvEvento.TabIndex = 1;
            this.dgvEvento.Paint += new System.Windows.Forms.PaintEventHandler(this.dgvEvento_Paint);
            // 
            // dgcData
            // 
            this.dgcData.HeaderText = "Data";
            this.dgcData.Name = "dgcData";
            this.dgcData.ReadOnly = true;
            this.dgcData.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgcData.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgcTitulo
            // 
            this.dgcTitulo.HeaderText = "Nome";
            this.dgcTitulo.Name = "dgcTitulo";
            this.dgcTitulo.ReadOnly = true;
            this.dgcTitulo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgcTitulo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgcTitulo.Width = 123;
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Location = new System.Drawing.Point(9, 59);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sistemaToolStripMenuItem,
            this.animaisToolStripMenuItem,
            this.adoçãoToolStripMenuItem,
            this.protetorToolStripMenuItem,
            this.agendaToolStripMenuItem,
            this.doaçõesToolStripMenuItem,
            this.denunciaToolStripMenuItem,
            this.estoqueToolStripMenuItem,
            this.estatísticasToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1072, 29);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // sistemaToolStripMenuItem
            // 
            this.sistemaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loginToolStripMenuItem,
            this.toolStripMenuItem1,
            this.sairToolStripMenuItem});
            this.sistemaToolStripMenuItem.Image = global::SistemaMAPAN.Properties.Resources.QuartoLogo_MAPAN_128x102_;
            this.sistemaToolStripMenuItem.Name = "sistemaToolStripMenuItem";
            this.sistemaToolStripMenuItem.Size = new System.Drawing.Size(76, 25);
            this.sistemaToolStripMenuItem.Text = "&Sistema";
            // 
            // loginToolStripMenuItem
            // 
            this.loginToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loginToolStripMenuItem1,
            this.novoToolStripMenuItem3});
            this.loginToolStripMenuItem.Name = "loginToolStripMenuItem";
            this.loginToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.loginToolStripMenuItem.Text = "&Usuáro";
            // 
            // loginToolStripMenuItem1
            // 
            this.loginToolStripMenuItem1.Name = "loginToolStripMenuItem1";
            this.loginToolStripMenuItem1.Size = new System.Drawing.Size(104, 22);
            this.loginToolStripMenuItem1.Text = "Login";
            this.loginToolStripMenuItem1.Click += new System.EventHandler(this.loginToolStripMenuItem1_Click);
            // 
            // novoToolStripMenuItem3
            // 
            this.novoToolStripMenuItem3.Name = "novoToolStripMenuItem3";
            this.novoToolStripMenuItem3.Size = new System.Drawing.Size(104, 22);
            this.novoToolStripMenuItem3.Text = "Novo";
            this.novoToolStripMenuItem3.Click += new System.EventHandler(this.novoToolStripMenuItem3_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(108, 6);
            // 
            // sairToolStripMenuItem
            // 
            this.sairToolStripMenuItem.Name = "sairToolStripMenuItem";
            this.sairToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.sairToolStripMenuItem.Text = "Sai&r";
            this.sairToolStripMenuItem.Click += new System.EventHandler(this.sairToolStripMenuItem_Click_1);
            // 
            // animaisToolStripMenuItem
            // 
            this.animaisToolStripMenuItem.AutoSize = false;
            this.animaisToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.novoToolStripMenuItem,
            this.consultaToolStripMenuItem,
            this.transferenciaToolStripMenuItem});
            this.animaisToolStripMenuItem.Image = global::SistemaMAPAN.Properties.Resources.t9dog2_trans;
            this.animaisToolStripMenuItem.Name = "animaisToolStripMenuItem";
            this.animaisToolStripMenuItem.Size = new System.Drawing.Size(78, 25);
            this.animaisToolStripMenuItem.Text = "&Animais";
            // 
            // novoToolStripMenuItem
            // 
            this.novoToolStripMenuItem.Image = global::SistemaMAPAN.Properties.Resources.add_16x16;
            this.novoToolStripMenuItem.Name = "novoToolStripMenuItem";
            this.novoToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.novoToolStripMenuItem.Text = "&Novo";
            this.novoToolStripMenuItem.Click += new System.EventHandler(this.novoToolStripMenuItem_Click);
            // 
            // consultaToolStripMenuItem
            // 
            this.consultaToolStripMenuItem.Image = global::SistemaMAPAN.Properties.Resources.search_16x16;
            this.consultaToolStripMenuItem.Name = "consultaToolStripMenuItem";
            this.consultaToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.consultaToolStripMenuItem.Text = "&Consulta";
            this.consultaToolStripMenuItem.Click += new System.EventHandler(this.consultaToolStripMenuItem_Click);
            // 
            // transferenciaToolStripMenuItem
            // 
            this.transferenciaToolStripMenuItem.Image = global::SistemaMAPAN.Properties.Resources.system_log_out_003;
            this.transferenciaToolStripMenuItem.Name = "transferenciaToolStripMenuItem";
            this.transferenciaToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.transferenciaToolStripMenuItem.Text = "Transferencia";
            this.transferenciaToolStripMenuItem.Click += new System.EventHandler(this.transferenciaToolStripMenuItem_Click);
            // 
            // adoçãoToolStripMenuItem
            // 
            this.adoçãoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.novaToolStripMenuItem,
            this.consultaToolStripMenuItem2});
            this.adoçãoToolStripMenuItem.Image = global::SistemaMAPAN.Properties.Resources.user_female;
            this.adoçãoToolStripMenuItem.Name = "adoçãoToolStripMenuItem";
            this.adoçãoToolStripMenuItem.Size = new System.Drawing.Size(76, 25);
            this.adoçãoToolStripMenuItem.Text = "A&doção";
            // 
            // novaToolStripMenuItem
            // 
            this.novaToolStripMenuItem.Image = global::SistemaMAPAN.Properties.Resources.add_16x16;
            this.novaToolStripMenuItem.Name = "novaToolStripMenuItem";
            this.novaToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.novaToolStripMenuItem.Text = "&Nova";
            this.novaToolStripMenuItem.Click += new System.EventHandler(this.novaToolStripMenuItem_Click);
            // 
            // consultaToolStripMenuItem2
            // 
            this.consultaToolStripMenuItem2.Image = global::SistemaMAPAN.Properties.Resources.search_16x16;
            this.consultaToolStripMenuItem2.Name = "consultaToolStripMenuItem2";
            this.consultaToolStripMenuItem2.Size = new System.Drawing.Size(121, 22);
            this.consultaToolStripMenuItem2.Text = "&Consulta";
            this.consultaToolStripMenuItem2.Click += new System.EventHandler(this.consultaToolStripMenuItem2_Click);
            // 
            // protetorToolStripMenuItem
            // 
            this.protetorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.novoToolStripMenuItem1,
            this.consultaToolStripMenuItem1});
            this.protetorToolStripMenuItem.Image = global::SistemaMAPAN.Properties.Resources.user_gray;
            this.protetorToolStripMenuItem.Name = "protetorToolStripMenuItem";
            this.protetorToolStripMenuItem.Size = new System.Drawing.Size(89, 25);
            this.protetorToolStripMenuItem.Text = "&Protetores";
            // 
            // novoToolStripMenuItem1
            // 
            this.novoToolStripMenuItem1.Image = global::SistemaMAPAN.Properties.Resources.add_16x16;
            this.novoToolStripMenuItem1.Name = "novoToolStripMenuItem1";
            this.novoToolStripMenuItem1.Size = new System.Drawing.Size(121, 22);
            this.novoToolStripMenuItem1.Text = "&Novo";
            this.novoToolStripMenuItem1.Click += new System.EventHandler(this.novoToolStripMenuItem1_Click);
            // 
            // consultaToolStripMenuItem1
            // 
            this.consultaToolStripMenuItem1.Image = global::SistemaMAPAN.Properties.Resources.search_16x16;
            this.consultaToolStripMenuItem1.Name = "consultaToolStripMenuItem1";
            this.consultaToolStripMenuItem1.Size = new System.Drawing.Size(121, 22);
            this.consultaToolStripMenuItem1.Text = "&Consulta";
            this.consultaToolStripMenuItem1.Click += new System.EventHandler(this.consultaToolStripMenuItem1_Click);
            // 
            // agendaToolStripMenuItem
            // 
            this.agendaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.novoToolStripMenuItem2,
            this.consultaToolStripMenuItem3});
            this.agendaToolStripMenuItem.Image = global::SistemaMAPAN.Properties.Resources.calendar_select;
            this.agendaToolStripMenuItem.Name = "agendaToolStripMenuItem";
            this.agendaToolStripMenuItem.Size = new System.Drawing.Size(76, 25);
            this.agendaToolStripMenuItem.Text = "A&genda";
            // 
            // novoToolStripMenuItem2
            // 
            this.novoToolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eventoToolStripMenuItem,
            this.vistoriaToolStripMenuItem});
            this.novoToolStripMenuItem2.Image = global::SistemaMAPAN.Properties.Resources.add_16x16;
            this.novoToolStripMenuItem2.Name = "novoToolStripMenuItem2";
            this.novoToolStripMenuItem2.Size = new System.Drawing.Size(121, 22);
            this.novoToolStripMenuItem2.Text = "&Novo";
            // 
            // eventoToolStripMenuItem
            // 
            this.eventoToolStripMenuItem.Image = global::SistemaMAPAN.Properties.Resources.calendar__plus;
            this.eventoToolStripMenuItem.Name = "eventoToolStripMenuItem";
            this.eventoToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.eventoToolStripMenuItem.Text = "&Evento";
            this.eventoToolStripMenuItem.Click += new System.EventHandler(this.eventoToolStripMenuItem_Click);
            // 
            // vistoriaToolStripMenuItem
            // 
            this.vistoriaToolStripMenuItem.Image = global::SistemaMAPAN.Properties.Resources.mypc_search_16;
            this.vistoriaToolStripMenuItem.Name = "vistoriaToolStripMenuItem";
            this.vistoriaToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.vistoriaToolStripMenuItem.Text = "&Vistoria";
            this.vistoriaToolStripMenuItem.Click += new System.EventHandler(this.vistoriaToolStripMenuItem_Click);
            // 
            // consultaToolStripMenuItem3
            // 
            this.consultaToolStripMenuItem3.Image = global::SistemaMAPAN.Properties.Resources.search_16x16;
            this.consultaToolStripMenuItem3.Name = "consultaToolStripMenuItem3";
            this.consultaToolStripMenuItem3.Size = new System.Drawing.Size(121, 22);
            this.consultaToolStripMenuItem3.Text = "&Consulta";
            this.consultaToolStripMenuItem3.Click += new System.EventHandler(this.consultaToolStripMenuItem3_Click);
            // 
            // doaçõesToolStripMenuItem
            // 
            this.doaçõesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.colaboradoresToolStripMenuItem,
            this.consultaToolStripMenuItem5});
            this.doaçõesToolStripMenuItem.Image = global::SistemaMAPAN.Properties.Resources.zone_money;
            this.doaçõesToolStripMenuItem.Name = "doaçõesToolStripMenuItem";
            this.doaçõesToolStripMenuItem.Size = new System.Drawing.Size(80, 25);
            this.doaçõesToolStripMenuItem.Text = "D&oações";
            // 
            // colaboradoresToolStripMenuItem
            // 
            this.colaboradoresToolStripMenuItem.Image = global::SistemaMAPAN.Properties.Resources.add_16x16;
            this.colaboradoresToolStripMenuItem.Name = "colaboradoresToolStripMenuItem";
            this.colaboradoresToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.colaboradoresToolStripMenuItem.Text = "&Novo";
            this.colaboradoresToolStripMenuItem.Click += new System.EventHandler(this.colaboradoresToolStripMenuItem_Click);
            // 
            // consultaToolStripMenuItem5
            // 
            this.consultaToolStripMenuItem5.Image = global::SistemaMAPAN.Properties.Resources.search_16x16;
            this.consultaToolStripMenuItem5.Name = "consultaToolStripMenuItem5";
            this.consultaToolStripMenuItem5.Size = new System.Drawing.Size(121, 22);
            this.consultaToolStripMenuItem5.Text = "&Consulta";
            this.consultaToolStripMenuItem5.Click += new System.EventHandler(this.consultaToolStripMenuItem5_Click);
            // 
            // denunciaToolStripMenuItem
            // 
            this.denunciaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.novaToolStripMenuItem1,
            this.consultaToolStripMenuItem4});
            this.denunciaToolStripMenuItem.Image = global::SistemaMAPAN.Properties.Resources.user_silhouette;
            this.denunciaToolStripMenuItem.Name = "denunciaToolStripMenuItem";
            this.denunciaToolStripMenuItem.Size = new System.Drawing.Size(90, 25);
            this.denunciaToolStripMenuItem.Text = "&Denuncias";
            // 
            // novaToolStripMenuItem1
            // 
            this.novaToolStripMenuItem1.Image = global::SistemaMAPAN.Properties.Resources.add_16x16;
            this.novaToolStripMenuItem1.Name = "novaToolStripMenuItem1";
            this.novaToolStripMenuItem1.Size = new System.Drawing.Size(121, 22);
            this.novaToolStripMenuItem1.Text = "&Nova";
            this.novaToolStripMenuItem1.Click += new System.EventHandler(this.novaToolStripMenuItem1_Click);
            // 
            // consultaToolStripMenuItem4
            // 
            this.consultaToolStripMenuItem4.Image = global::SistemaMAPAN.Properties.Resources.search_16x16;
            this.consultaToolStripMenuItem4.Name = "consultaToolStripMenuItem4";
            this.consultaToolStripMenuItem4.Size = new System.Drawing.Size(121, 22);
            this.consultaToolStripMenuItem4.Text = "&Consulta";
            this.consultaToolStripMenuItem4.Click += new System.EventHandler(this.consultaToolStripMenuItem4_Click);
            // 
            // estoqueToolStripMenuItem
            // 
            this.estoqueToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.produtosToolStripMenuItem,
            this.entregasToolStripMenuItem,
            this.configuraçãoToolStripMenuItem});
            this.estoqueToolStripMenuItem.Image = global::SistemaMAPAN.Properties.Resources.box1;
            this.estoqueToolStripMenuItem.Name = "estoqueToolStripMenuItem";
            this.estoqueToolStripMenuItem.Size = new System.Drawing.Size(77, 25);
            this.estoqueToolStripMenuItem.Text = "&Estoque";
            // 
            // produtosToolStripMenuItem
            // 
            this.produtosToolStripMenuItem.Image = global::SistemaMAPAN.Properties.Resources.box1;
            this.produtosToolStripMenuItem.Name = "produtosToolStripMenuItem";
            this.produtosToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.produtosToolStripMenuItem.Text = "&Produtos";
            this.produtosToolStripMenuItem.Click += new System.EventHandler(this.produtosToolStripMenuItem_Click);
            // 
            // entregasToolStripMenuItem
            // 
            this.entregasToolStripMenuItem.Image = global::SistemaMAPAN.Properties.Resources.cargo1;
            this.entregasToolStripMenuItem.Name = "entregasToolStripMenuItem";
            this.entregasToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.entregasToolStripMenuItem.Text = "E&ntregas";
            this.entregasToolStripMenuItem.Click += new System.EventHandler(this.entregasToolStripMenuItem_Click);
            // 
            // configuraçãoToolStripMenuItem
            // 
            this.configuraçãoToolStripMenuItem.Image = global::SistemaMAPAN.Properties.Resources.config_16x16;
            this.configuraçãoToolStripMenuItem.Name = "configuraçãoToolStripMenuItem";
            this.configuraçãoToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.configuraçãoToolStripMenuItem.Text = "Configuração";
            // 
            // estatísticasToolStripMenuItem
            // 
            this.estatísticasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.financeiraToolStripMenuItem,
            this.adoçõesToolStripMenuItem});
            this.estatísticasToolStripMenuItem.Image = global::SistemaMAPAN.Properties.Resources._2006_16x16;
            this.estatísticasToolStripMenuItem.Name = "estatísticasToolStripMenuItem";
            this.estatísticasToolStripMenuItem.Size = new System.Drawing.Size(92, 25);
            this.estatísticasToolStripMenuItem.Text = "E&statísticas";
            // 
            // financeiraToolStripMenuItem
            // 
            this.financeiraToolStripMenuItem.Image = global::SistemaMAPAN.Properties.Resources.money_coin;
            this.financeiraToolStripMenuItem.Name = "financeiraToolStripMenuItem";
            this.financeiraToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.financeiraToolStripMenuItem.Text = "&Financeira";
            this.financeiraToolStripMenuItem.Click += new System.EventHandler(this.financeiraToolStripMenuItem_Click);
            // 
            // adoçõesToolStripMenuItem
            // 
            this.adoçõesToolStripMenuItem.Image = global::SistemaMAPAN.Properties.Resources.user_female;
            this.adoçõesToolStripMenuItem.Name = "adoçõesToolStripMenuItem";
            this.adoçõesToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.adoçõesToolStripMenuItem.Text = "&Adoções";
            this.adoçõesToolStripMenuItem.Click += new System.EventHandler(this.adoçõesToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel1.Controls.Add(this.menuStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1072, 33);
            this.panel1.TabIndex = 2;
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.BackgroundImage = global::SistemaMAPAN.Properties.Resources.Untitled_1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1072, 751);
            this.ControlBox = false;
            this.Controls.Add(this.pnlDireita);
            this.Controls.Add(this.pnlEsquerda);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1024, 726);
            this.Name = "frmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistema MAPAN";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPrincipal_FormClosing);
            this.Load += new System.EventHandler(this.frmPrincipal_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmPrincipal_Paint);
            this.pnlEsquerda.ResumeLayout(false);
            this.pnlEsquerda.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEstoque)).EndInit();
            this.pnlDireita.ResumeLayout(false);
            this.pnlDireita.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEvento)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlDireita;
        private System.Windows.Forms.Panel pnlEsquerda;
        private System.Windows.Forms.ToolStripMenuItem sistemaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loginToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem sairToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem animaisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem novoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem consultaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem adoçãoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem novaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem consultaToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem protetorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem novoToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem consultaToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem agendaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem novoToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem eventoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vistoriaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem consultaToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem doaçõesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colaboradoresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem denunciaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem novaToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem consultaToolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem estoqueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem produtosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem entregasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem estatísticasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem financeiraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem adoçõesToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcData;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcTitulo;
        private System.Windows.Forms.ToolStripMenuItem transferenciaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem consultaToolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem loginToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem novoToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem configuraçãoToolStripMenuItem;
        public System.Windows.Forms.MonthCalendar monthCalendar1;
        public System.Windows.Forms.DataGridView dgvEvento;
        public System.Windows.Forms.RadioButton rdbVistoria;
        public System.Windows.Forms.RadioButton rdbEvento;
        public System.Windows.Forms.Label lblInfo;
        public System.Windows.Forms.Label lblUsuario;
        public System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.DataGridView dgvEstoque;
        private System.Windows.Forms.Label lblEstoque;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcNome;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcEstoque;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcQuantidade;
    }
}

