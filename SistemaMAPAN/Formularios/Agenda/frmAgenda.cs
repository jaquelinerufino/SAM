using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Odbc;

namespace SistemaMAPAN
{
    public partial class frmAgenda : Form
    {
        int[] cd_eventos = new int[0];

        public frmAgenda()
        {
            InitializeComponent();
        }

        clsConexao.clsConexao Conexao = new clsConexao.clsConexao("mapan", "localhost", "root", "root");
        OdbcDataReader DR;
        OdbcDataReader LstAnimais;

        public List<string> lstData = new List<string>();
        public List<string> lstDataFn = new List<string>();
        public List<string> lstHora = new List<string>();
        public List<string> lstHoraFn = new List<string>();
        public List<string> lstValor = new List<string>();

        public List<string> lstDataVis = new List<string>();
        public List<string> lstHoraVis = new List<string>();
        public List<int> lstCdPessoa = new List<int>();

        public List<int> lstCdAnimais = new List<int>();

        bool editou; 

        clsAgenda agenda = new clsAgenda();
        frmNovoEvento evento = new frmNovoEvento();
        frmVistoria vistoria = new frmVistoria();
        frmRelacaoAnimalEvento RelAn = new frmRelacaoAnimalEvento();

        #region formLoad - Lista de Eventos
        private void frmAgenda_Load(object sender, EventArgs e)
        {
            editou = false;
            DR = null;

            Conexao.IniciarStoredProcedure("ListaEvento");
            DR = Conexao.ChamarStoredProcedureCR();

            #region Carrega a lista de eventos
            while (DR.Read())
            {
                if (DR[7].ToString() == "")
                {

                    lstData.Add(DR[0].ToString());
                    lstHora.Add(DR[1].ToString());
                    lstDataFn.Add(DR[2].ToString());
                    lstHoraFn.Add(DR[3].ToString());

                    lstValor.Add("");

                    dgvListas.Rows.Add(DR.GetDate(0).ToString("dd/MM/yyyy"), DR.GetDate(2).ToString("dd/MM/yyyy"), DR.GetString(4));
                }
            }

            DR.Close();
            DR.Dispose();
            Conexao.FecharConexao();

            rdbEvento.Checked = true;
            #endregion
        }
        #endregion

        #region Fechar
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region Habilita/Desabilita Editar ou Excluir - Carrega a lista de animais relacionados do evento
        private void dgvListas_SelectionChanged(object sender, EventArgs e)
        {
            if (rdbEvento.Checked)
            {
                #region Evento
                btnNovaRelacao.Visible = true;

                if (dgvListas.Rows.Count == 0)
                {
                    btnEditar.Enabled = false;
                    btnExcluir.Enabled = false;
                    btnNovaRelacao.Enabled = false;
                    return;
                }

                if (dgvListas.CurrentRow.Index >= 0 && lstData.Count > 0)
                {
                    btnEditar.Enabled = true;
                    dgvAnimais.Rows.Clear();
                    lstCdAnimais.Clear();
                    btnNovaRelacao.Text = "&Editar Relação";

                    LstAnimais = null;
                    Conexao.IniciarStoredProcedure("ListaAnimaisEvento");
                    Conexao.AdicionarParametroTexto(DateTime.Parse(lstData[dgvListas.CurrentRow.Index]).Date.ToString("yyyy-MM-dd"));
                    Conexao.AdicionarParametroTexto(lstHora[dgvListas.CurrentRow.Index]);
                    LstAnimais = Conexao.ChamarStoredProcedureCR();

                    if (dgvAnimais.Columns.Count == 1)
                    {

                        dgvAnimais.Columns[0].HeaderText = "Código dos Animais";
                        dgvAnimais.Columns[0].Width = 100;
                        dgvAnimais.Columns.Add("dgcCodigoAnimais", "Nome dos Animais");
                        dgvAnimais.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dgvAnimais.Columns[1].Width = 183;
                    }

                    while (LstAnimais.Read())
                    {
                        dgvAnimais.Rows.Add(LstAnimais[1].ToString(), LstAnimais[0].ToString());
                        lstCdAnimais.Add(LstAnimais.GetInt32(1));
                    }

                    LstAnimais.Close();
                    LstAnimais.Dispose();
                    Conexao.FecharConexao();

                    if (dgvAnimais.Rows.Count == 0)
                    {
                        btnExcluir.Enabled = true;
                        btnNovaRelacao.Text = "&Adicionar Relação";
                        this.StartPosition = FormStartPosition.CenterScreen;
                        this.CenterToScreen();
                    }
                    else
                    {
                        btnExcluir.Enabled = false;
                        btnNovaRelacao.Text = "&Editar Relação";
                        this.StartPosition = FormStartPosition.CenterScreen;
                        this.CenterToScreen();
                    }

                    if (DateTime.Parse(lstData[dgvListas.CurrentRow.Index]) < System.DateTime.Today.Date)
                    {
                        btnNovaRelacao.Enabled = false;
                    }
                    else
                    {
                        btnNovaRelacao.Enabled = true;
                    }
                }
                #endregion
            }
            else
            {
                #region Vistoria
                if (rdbVistoria.Checked)
                {
                    btnNovaRelacao.Visible = false;
                    dgvAnimais.Rows.Clear();
                    btnNovaRelacao.Text = "&Editar Relação";

                    if (dgvListas.Rows.Count == 0)
                    {
                        btnEditar.Enabled = false;
                        btnExcluir.Enabled = false;
                        btnNovaRelacao.Enabled = false;
                        return;
                    }

                    if (dgvListas.Rows.Count > 0 && lstCdPessoa.Count > 0)
                    {
                        btnEditar.Enabled = true;
                        btnExcluir.Enabled = true;

                        if (dgvAnimais.Columns.Count == 1)
                        {

                            dgvAnimais.Columns[0].HeaderText = "Código dos Animais";
                            dgvAnimais.Columns[0].Width = 100;
                            dgvAnimais.Columns.Add("dgcCodigoAnimais", "Nome dos Animais");
                            dgvListas.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
                            dgvAnimais.Columns[1].Width = 183;
                        }

                        LstAnimais = null;

                        Conexao.IniciarStoredProcedure("ListaAnimalPessoa");
                        Conexao.AdicionarParametroInteiro(lstCdPessoa[dgvListas.CurrentRow.Index]);
                        LstAnimais = Conexao.ChamarStoredProcedureCR();

                        while (LstAnimais.Read())
                        {
                            dgvAnimais.Rows.Add(LstAnimais[1].ToString(), LstAnimais[0].ToString());
                        }

                        LstAnimais.Close();
                        LstAnimais.Dispose();
                        Conexao.FecharConexao();

                        if (dgvAnimais.Rows.Count == 0)
                        {
                            this.StartPosition = FormStartPosition.CenterScreen;
                            this.CenterToScreen();
                        }
                        else
                        {
                            this.StartPosition = FormStartPosition.CenterScreen;
                            this.CenterToScreen();
                        }
                    }
                }
                #endregion
            }

            //int RowSelecionada = int.Parse(dgvListas.SelectedRows.ToString());
        }
        #endregion

        #region Altera conteudo do DataGridView - Lista de eventos ou lista de vistorias

        #region Lista de Eventos
        private void rdbEvento_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbEvento.Checked == true)
            {
                lblEventos.Text = "Lista de Eventos";
                lblEventoRelacionado.Text = "Animais participantes deste evento selecionado:";

                btnNovoEvento.Text = "&Novo Evento";

                lstData.Clear();
                lstHora.Clear();
                lstDataFn.Clear();
                lstHoraFn.Clear();
                lstValor.Clear();
                dgvListas.Rows.Clear();

                dgvListas.Columns[0].HeaderText = "Data de Início";
                dgvListas.Columns[1].HeaderText = "Data de Término";
                dgvListas.Columns[2].HeaderText = "Titulo do Evento";

                DR = null;

                Conexao.IniciarStoredProcedure("ListaEvento");
                DR = Conexao.ChamarStoredProcedureCR();

                while (DR.Read())
                {
                    if (DR[7].ToString() == "")
                    {

                        lstData.Add(DR[0].ToString());
                        lstHora.Add(DR[1].ToString());
                        lstDataFn.Add(DR[2].ToString());
                        lstHoraFn.Add(DR[3].ToString());

                        lstValor.Add("");

                        dgvListas.Rows.Add(DR.GetDate(0).ToString("dd/MM/yyyy"), DR.GetDate(2).ToString("dd/MM/yyyy"), DR.GetString(4));
                    }
                }

                DR.Close();
                DR.Dispose();
                Conexao.FecharConexao();

                string Horafn = (System.DateTime.Now.Hour).ToString() + ":" + (System.DateTime.Now.Minute).ToString();

                for (int i = 0; i < dgvListas.Rows.Count; i++)
                {
                    if (DateTime.Parse(lstDataFn[i]) < System.DateTime.Today && lstValor[i] == "" || DateTime.Parse(lstDataFn[i]).Date == System.DateTime.Today && lstValor[i] == "" && TimeSpan.Parse(lstHoraFn[i]) < TimeSpan.Parse(Horafn))
                    {
                        this.dgvListas.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                    }
                }
            }
        }
        #endregion

        #region Lista de Vistorias
        private void rdbVistoria_CheckedChanged(object sender, EventArgs e)
        {
 
                if (rdbVistoria.Checked == true)
                {
                    btnNovoEvento.Text = "&Nova Vistoria";

                    lblEventos.Text = "Lista de Vistorias";
                    lblEventoRelacionado.Text = "Animais em posse da pessoa selecionada:";

                    lstDataVis.Clear();
                    lstHoraVis.Clear();
                    lstCdPessoa.Clear();
                    dgvListas.Rows.Clear();

                    dgvListas.Columns[0].HeaderText = "Data da Vistoria";
                    //dgvListas.Columns[0].Width = 110;
                    dgvListas.Columns[1].HeaderText = "Nome do Vistoriado";
                    //dgvListas.Columns[1].Width = 110;
                    dgvListas.Columns[2].HeaderText = "Situação";
                    //dgvListas.Columns[2].Width = 195;

                    DR = null;
                    int index = 0;

                    Conexao.IniciarStoredProcedure("ListaVistoria");
                    DR = Conexao.ChamarStoredProcedureCR();

                    while (DR.Read())
                    {
                        lstDataVis.Add(DR[0].ToString());
                        lstHoraVis.Add(DR[2].ToString());
                        lstCdPessoa.Add(DR.GetInt32(3));

                        string hora = (System.DateTime.Now.Hour + ":" + System.DateTime.Now.Minute).ToString();

                        if (DR.GetDate(0) == System.DateTime.Today.Date && DR.GetTime(2) < TimeSpan.Parse(hora) && DR[6].ToString() == "" || DR.GetDate(0) < System.DateTime.Today.Date && DR[6].ToString() == "")
                        {
                            if (DR[4].ToString() == "" && DR[5].ToString() == "")
                            {
                                if (DR[7].ToString() != "")
                                {
                                    if (DR[7].ToString() != "1")
                                    {
                                        dgvListas.Rows.Add(DR.GetDate(0).ToString("dd/MM/yyyy"), DR[1].ToString(), "Dono de um abrigo inativo");
                                    }
                                    else
                                    {
                                        dgvListas.Rows.Add(DR.GetDate(0).ToString("dd/MM/yyyy"), DR[1].ToString(), "Dono de um abrigo Ativo");
                                    }
                                }
                                else
                                {
                                    if (DR[8].ToString() != "")
                                    {
                                        if (DR[7].ToString() != "1")
                                        {
                                            dgvListas.Rows.Add(DR.GetDate(0).ToString("dd/MM/yyyy"), DR[1].ToString(), "Adotante desbloqueado");
                                        }
                                        else
                                        {
                                            dgvListas.Rows.Add(DR.GetDate(0).ToString("dd/MM/yyyy"), DR[1].ToString(), "Adotante bloqueado");
                                        }
                                    }
                                    else
                                    {
                                        dgvListas.Rows.Add(DR.GetDate(0).ToString("dd/MM/yyyy"), DR[1].ToString(), "");
                                    }
                                }

                                dgvListas.Rows[index].DefaultCellStyle.BackColor = Color.Yellow;
                                index += 1;
                            }
                            else
                            {
                                if (DR[6].ToString() == "")
                                {
                                    if (DR[7].ToString() != "")
                                    {
                                        if (DR[7].ToString() != "1")
                                        {
                                            dgvListas.Rows.Add(DR.GetDate(0).ToString("dd/MM/yyyy"), DR[1].ToString(), "Dono de um abrigo inativo");
                                        }
                                        else
                                        {
                                            dgvListas.Rows.Add(DR.GetDate(0).ToString("dd/MM/yyyy"), DR[1].ToString(), "Dono de um abrigo Ativo");
                                        }
                                    }
                                    else
                                    {
                                        if (DR[8].ToString() != "")
                                        {
                                            if (DR[7].ToString() != "1")
                                            {
                                                dgvListas.Rows.Add(DR.GetDate(0).ToString("dd/MM/yyyy"), DR[1].ToString(), "Adotante desbloqueado");
                                            }
                                            else
                                            {
                                                dgvListas.Rows.Add(DR.GetDate(0).ToString("dd/MM/yyyy"), DR[1].ToString(), "Adotante bloqueado");
                                            }
                                        }
                                        else
                                        {
                                            dgvListas.Rows.Add(DR.GetDate(0).ToString("dd/MM/yyyy"), DR[1].ToString(), "");
                                        }
                                    }
                                }

                                index += 1;
                            }
                        }
                        else if (DR[6].ToString() == "")
                        {
                            if (DR[7].ToString() != "")
                            {
                                if (DR[7].ToString() != "1")
                                {
                                    dgvListas.Rows.Add(DR.GetDate(0).ToString("dd/MM/yyyy"), DR[1].ToString(), "Dono de um abrigo inativo");
                                }
                                else
                                {
                                    dgvListas.Rows.Add(DR.GetDate(0).ToString("dd/MM/yyyy"), DR[1].ToString(), "Dono de um abrigo Ativo");
                                }
                            }
                            else
                            {
                                if (DR[8].ToString() != "")
                                {
                                    if (DR[7].ToString() != "1")
                                    {
                                        dgvListas.Rows.Add(DR.GetDate(0).ToString("dd/MM/yyyy"), DR[1].ToString(), "Adotante desbloqueado");
                                    }
                                    else
                                    {
                                        dgvListas.Rows.Add(DR.GetDate(0).ToString("dd/MM/yyyy"), DR[1].ToString(), "Adotante bloqueado");
                                    }
                                }
                                else
                                {
                                    dgvListas.Rows.Add(DR.GetDate(0).ToString("dd/MM/yyyy"), DR[1].ToString(), "");
                                }
                            }
                            index += 1;
                        }
                    }
                    

                    DR.Close();
                    DR.Dispose();
                    Conexao.FecharConexao();
            
            
        }
        #endregion

        }
        #endregion

        #region Excluir Evento ou Vistoria
        private void btnExcluir_Click(object sender, EventArgs e)
        {
            #region Exclui Evento
            if (rdbEvento.Checked)
            {
                if (MessageBox.Show("Você deseja realmente excluir este evento?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    Conexao.IniciarStoredProcedure("ExcluirEvento");
                    Conexao.AdicionarParametroTexto(DateTime.Parse(lstData[dgvListas.CurrentRow.Index]).Date.ToString("yyyy-MM-dd"));
                    Conexao.AdicionarParametroTexto(lstHora[dgvListas.CurrentRow.Index]);
                    Conexao.ChamarStoredProcedureSR();

                    MessageBox.Show("Evento excluido com sucesso!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    lstData.Clear();
                    lstHora.Clear();
                    lstDataFn.Clear();
                    lstHoraFn.Clear();
                    lstValor.Clear();
                    dgvListas.Rows.Clear();

                    dgvListas.Columns[0].HeaderText = "Data de Início";
                    dgvListas.Columns[1].HeaderText = "Data de Término";
                    dgvListas.Columns[2].HeaderText = "Titulo do Evento";

                    DR = null;

                    Conexao.IniciarStoredProcedure("ListaEvento");
                    DR = Conexao.ChamarStoredProcedureCR();

                    while (DR.Read())
                    {
                        if (DR[7].ToString() == "")
                        {

                            lstData.Add(DR[0].ToString());
                            lstHora.Add(DR[1].ToString());
                            lstDataFn.Add(DR[2].ToString());
                            lstHoraFn.Add(DR[3].ToString());

                            lstValor.Add("");

                            dgvListas.Rows.Add(DR.GetDate(0).ToString("dd/MM/yyyy"), DR.GetDate(2).ToString("dd/MM/yyyy"), DR.GetString(4));
                        }
                    }

                    DR.Close();
                    DR.Dispose();
                    Conexao.FecharConexao();

                    string Horafn = (System.DateTime.Now.Hour).ToString() + ":" + (System.DateTime.Now.Minute).ToString();

                    for (int i = 0; i < dgvListas.Rows.Count; i++)
                    {
                        if (DateTime.Parse(lstDataFn[i]) < System.DateTime.Today && lstValor[i] == "" || DateTime.Parse(lstDataFn[i]).Date == System.DateTime.Today && lstValor[i] == "" && TimeSpan.Parse(lstHoraFn[i]) < TimeSpan.Parse(Horafn))
                        {
                            this.dgvListas.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                        }
                    }
                }
            }
            #endregion

            #region Exclui Vistoria
            else
            {
                if (MessageBox.Show("Você deseja realmente excluir esta vistoria?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    Conexao.IniciarStoredProcedure("ExcluirVistoria");
                    Conexao.AdicionarParametroTexto(DateTime.Parse(lstDataVis[dgvListas.CurrentRow.Index]).Date.ToString("yyyy-MM-dd"));
                    Conexao.AdicionarParametroTexto(lstHoraVis[dgvListas.CurrentRow.Index]);
                    Conexao.AdicionarParametroInteiro(lstCdPessoa[dgvListas.CurrentRow.Index]);
                    Conexao.ChamarStoredProcedureSR();

                    MessageBox.Show("Vistoria excluida com sucesso!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    rdbEvento.Checked = true;
                    rdbVistoria.Checked = true;

                    //lstDataVis.Clear();
                    //lstHoraVis.Clear();
                    //lstCdPessoa.Clear();
                    ////lstEventos.Items.Clear();
                    //dgvListas.Rows.Clear();
                    //lblEventos.Text = "Lista de Vistorias:";

                    //DR = null;
                    //int index = 0;

                    //Conexao.IniciarStoredProcedure("ListaVistoria");
                    //DR = Conexao.ChamarStoredProcedureCR();


                    //while (DR.Read())
                    //{
                    //    lstDataVis.Add(DR[0].ToString());
                    //    lstHoraVis.Add(DR[2].ToString());
                    //    lstCdPessoa.Add(DR.GetInt32(3));

                    //    string hora = (System.DateTime.Now.Hour + ":" + System.DateTime.Now.Minute).ToString();

                    //    if (DR.GetDate(0) == System.DateTime.Today.Date && DR.GetTime(2) < TimeSpan.Parse(hora) || DR.GetDate(0) < System.DateTime.Today.Date)
                    //    {
                    //        if (DR[4].ToString() == "" && DR[5].ToString() == "")
                    //        {
                    //            if (DR[7].ToString() != "")
                    //            {
                    //                if (DR[7].ToString() != "1")
                    //                {
                    //                    dgvListas.Rows.Add(DR.GetDate(0).ToString("dd/MM/yyyy"), DR[1].ToString(), "Dono de um abrigo inativo");
                    //                }
                    //                else
                    //                {
                    //                    dgvListas.Rows.Add(DR.GetDate(0).ToString("dd/MM/yyyy"), DR[1].ToString(), "Dono de um abrigo Ativo");
                    //                }
                    //            }
                    //            else
                    //            {
                    //                if (DR[8].ToString() != "")
                    //                {
                    //                    if (DR[7].ToString() != "1")
                    //                    {
                    //                        dgvListas.Rows.Add(DR.GetDate(0).ToString("dd/MM/yyyy"), DR[1].ToString(), "Adotante desbloqueado");
                    //                    }
                    //                    else
                    //                    {
                    //                        dgvListas.Rows.Add(DR.GetDate(0).ToString("dd/MM/yyyy"), DR[1].ToString(), "Adotante bloqueado");
                    //                    }
                    //                }
                    //                else
                    //                {
                    //                    dgvListas.Rows.Add(DR.GetDate(0).ToString("dd/MM/yyyy"), DR[1].ToString(), "");
                    //                }
                    //            }

                    //            dgvListas.Rows[index].DefaultCellStyle.BackColor = Color.Yellow;
                    //            index += 1;
                    //        }
                    //        else
                    //        {
                    //            if (DR[7].ToString() != "")
                    //            {
                    //                if (DR[7].ToString() != "1")
                    //                {
                    //                    dgvListas.Rows.Add(DR.GetDate(0).ToString("dd/MM/yyyy"), DR[1].ToString(), "Dono de um abrigo inativo");
                    //                }
                    //                else
                    //                {
                    //                    dgvListas.Rows.Add(DR.GetDate(0).ToString("dd/MM/yyyy"), DR[1].ToString(), "Dono de um abrigo Ativo");
                    //                }
                    //            }
                    //            else
                    //            {
                    //                if (DR[8].ToString() != "")
                    //                {
                    //                    if (DR[7].ToString() != "1")
                    //                    {
                    //                        dgvListas.Rows.Add(DR.GetDate(0).ToString("dd/MM/yyyy"), DR[1].ToString(), "Adotante desbloqueado");
                    //                    }
                    //                    else
                    //                    {
                    //                        dgvListas.Rows.Add(DR.GetDate(0).ToString("dd/MM/yyyy"), DR[1].ToString(), "Adotante bloqueado");
                    //                    }
                    //                }
                    //                else
                    //                {
                    //                    dgvListas.Rows.Add(DR.GetDate(0).ToString("dd/MM/yyyy"), DR[1].ToString(), "");
                    //                }
                    //            }
                    //            index += 1;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        if (DR[7].ToString() != "")
                    //        {
                    //            if (DR[7].ToString() != "1")
                    //            {
                    //                dgvListas.Rows.Add(DR.GetDate(0).ToString("dd/MM/yyyy"), DR[1].ToString(), "Dono de um abrigo inativo");
                    //            }
                    //            else
                    //            {
                    //                dgvListas.Rows.Add(DR.GetDate(0).ToString("dd/MM/yyyy"), DR[1].ToString(), "Dono de um abrigo Ativo");
                    //            }
                    //        }
                    //        else
                    //        {
                    //            if (DR[8].ToString() != "")
                    //            {
                    //                if (DR[7].ToString() != "1")
                    //                {
                    //                    dgvListas.Rows.Add(DR.GetDate(0).ToString("dd/MM/yyyy"), DR[1].ToString(), "Adotante desbloqueado");
                    //                }
                    //                else
                    //                {
                    //                    dgvListas.Rows.Add(DR.GetDate(0).ToString("dd/MM/yyyy"), DR[1].ToString(), "Adotante bloqueado");
                    //                }
                    //            }
                    //            else
                    //            {
                    //                dgvListas.Rows.Add(DR.GetDate(0).ToString("dd/MM/yyyy"), DR[1].ToString(), "");
                    //            }
                    //        }
                    //        index += 1;
                    //    }
                    //}

                    //DR.Close();
                    //DR.Dispose();
                    //Conexao.FecharConexao();
                }
            }
            #endregion
        }
        #endregion

        #region Abre tela de edição de Evento ou Visotira
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (rdbEvento.Checked)
            {
                evento.btnSalvar.Text = "&Salvar Edição";
                evento.btnCancelar.Text = "&Cancelar Edição";
                evento.caminho = 1;
                evento.ShowDialog(this);
            }
            else
            {
                vistoria.btnSalvar.Text = "&Salvar Edição";
                vistoria.btnCancelar.Text = "&Cancelar Edição";
                vistoria.Caminho = 1;
                vistoria.ShowDialog(this);
            }
        }
        #endregion

        #region Abre tela de relação animal
        private void btnNovaRelacao_Click(object sender, EventArgs e)
        {
            #region Exibição do evento selecionado
            DR = null;
            Conexao.IniciarStoredProcedure("ListaEvento");
            DR = Conexao.ChamarStoredProcedureCR();

            while (DR.Read())
            {
                if (DR[0].ToString() == lstData[dgvListas.CurrentRow.Index] && DR[1].ToString() == lstHora[dgvListas.CurrentRow.Index])
                {
                    RelAn.lblEvento.Text = DR.GetDate(0).ToString("dd/MM/yyyy") + " até " + DR.GetDate(2).ToString("dd/MM/yyyy") + " - " + DR[4].ToString();
                }
            }

            DR.Close();
            DR.Dispose();
            Conexao.FecharConexao();
            #endregion

            if (dgvAnimais.Rows.Count < 0)
            {
                RelAn.btnExcluirAnimal.Enabled = false;
            }

            RelAn.NovaRelação = false;
            RelAn.ShowDialog(this);
        }
        #endregion

        #region Ajusta o tamanho dos DataGridView
        public void controlarTamanhoGrid(System.Windows.Forms.DataGridView dgvNome, int alturaLinha, int alturaCabecalho, int larguraGrid, int alturaMaximaGrid)
        {
            int altura = ((dgvNome.RowCount) * alturaLinha) + (alturaCabecalho + 2);

            if (altura >= alturaMaximaGrid)
            {
                dgvNome.ScrollBars = ScrollBars.Vertical;
                dgvNome.Size = new System.Drawing.Size(larguraGrid + 17, alturaMaximaGrid);
            }
            else
            {
                dgvNome.ScrollBars = ScrollBars.None;
                dgvNome.Size = new System.Drawing.Size(larguraGrid, altura);
            }
        }

        private void dgvListas_Paint(object sender, PaintEventArgs e)
        {
            controlarTamanhoGrid(dgvListas, 22, 40, 368, 238);
        }

        private void dgvAnimais_Paint(object sender, PaintEventArgs e)
        {
            controlarTamanhoGrid(dgvAnimais, 22, 40, 287, 238);
        }
        #endregion

        #region Novo Evento
        public string Inicio, Final;
        public void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            evento.btnSalvar.Text = "Salvar";

            //Inicio = (monthCalendar1.SelectionStart.Year + "-" +
            //            monthCalendar1.SelectionStart.Month + "-" +
            //                monthCalendar1.SelectionStart.Day).ToString();

            Inicio = monthCalendar1.SelectionStart.ToString("dd/MM/yyyy");

            //Final = (monthCalendar1.SelectionEnd.Year + "-" + 
            //            monthCalendar1.SelectionEnd.Month + "-" + 
            //                monthCalendar1.SelectionEnd.Day).ToString();

            Final = monthCalendar1.SelectionEnd.ToString("dd/MM/yyyy");

            if (rdbEvento.Checked == true)
            {
                if (System.DateTime.Today.Date > DateTime.Parse(Inicio))
                {
                    MessageBox.Show("Atenção: A data " + Inicio + " já passou!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (Inicio == Final)
                {
                    /*aviso.ShowDialog();
                    aviso.lblTexto.Text = "Você deseja criar um novo evento desta data? " + Inicio;*/
                    if (MessageBox.Show("Você deseja criar um evento nessa data? " + Inicio, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        evento.ShowDialog(this);
                    }
                }
                else
                {
                    if (MessageBox.Show("Você deseja criar um evento neste período? " + Inicio + " - " + Final, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        evento.ShowDialog(this);
                    }
                }
            }
            else
            {
                if (System.DateTime.Today.Date > DateTime.Parse(Inicio))
                {
                    MessageBox.Show("Atenção: A data " + Inicio + " já passou!");
                    return;
                }

                if (Inicio == Final)
                {
                    /*aviso.ShowDialog();
                    aviso.lblTexto.Text = "Você deseja criar um novo evento desta data? " + Inicio;*/
                    if (MessageBox.Show("Você deseja criar uma nova vistoria nesta data? " + Inicio, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        vistoria.ShowDialog(this);
                    }
                }
                else
                {
                    MessageBox.Show("Atenção: A vistoria deve conter apenas uma data!");
                }
            }
        }
        #endregion

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void btnNovoEvento_Click(object sender, EventArgs e)
        {
            if (btnNovoEvento.Text == "&Novo Evento")
            {
                frmNovoEvento Evento = new frmNovoEvento();

                evento.caminho = 1;
                evento.ShowDialog(this);

                dgvListas.Rows.Clear();

                #region Carrega a lista de eventos

                lstData.Clear();
                lstHora.Clear();
                lstDataFn.Clear();
                lstHoraFn.Clear();
                lstValor.Clear();
                dgvListas.Rows.Clear();

                dgvListas.Columns[0].HeaderText = "Data de Início";
                dgvListas.Columns[1].HeaderText = "Data de Término";
                dgvListas.Columns[2].HeaderText = "Titulo do Evento";

                DR = null;

                Conexao.IniciarStoredProcedure("ListaEvento");
                DR = Conexao.ChamarStoredProcedureCR();

                while (DR.Read())
                {
                    if (DR[7].ToString() == "")
                    {

                        lstData.Add(DR[0].ToString());
                        lstHora.Add(DR[1].ToString());
                        lstDataFn.Add(DR[2].ToString());
                        lstHoraFn.Add(DR[3].ToString());

                        lstValor.Add("");

                        dgvListas.Rows.Add(DR.GetDate(0).ToString("dd/MM/yyyy"), DR.GetDate(2).ToString("dd/MM/yyyy"), DR.GetString(4));
                    }
                }

                DR.Close();
                DR.Dispose();
                Conexao.FecharConexao();

                string Horafn = (System.DateTime.Now.Hour).ToString() + ":" + (System.DateTime.Now.Minute).ToString();

                for (int i = 0; i < dgvListas.Rows.Count; i++)
                {
                    if (DateTime.Parse(lstDataFn[i]) < System.DateTime.Today.Date && lstValor[i] == "" || DateTime.Parse(lstDataFn[i]) == System.DateTime.Today.Date && lstValor[i] == "" && TimeSpan.Parse(lstHoraFn[i]) < TimeSpan.Parse(Horafn))
                    {
                        this.dgvListas.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                    }
                }
                #endregion
            }
            else
            {
                frmVistoria Vistoria = new frmVistoria();

                vistoria.Caminho = 1;
                vistoria.ShowDialog(this);

                dgvListas.Rows.Clear();

                #region Lista de Vistorias
                if (rdbVistoria.Checked == true)
                {
                    btnNovoEvento.Text = "Nova Vistoria";

                    lstDataVis.Clear();
                    lstHoraVis.Clear();
                    lstCdPessoa.Clear();
                    dgvListas.Rows.Clear();

                    dgvListas.Columns[0].HeaderText = "Data da Vistoria";
                    //dgvListas.Columns[0].Width = 110;
                    dgvListas.Columns[1].HeaderText = "Nome do Vistoriado";
                    //dgvListas.Columns[1].Width = 110;
                    dgvListas.Columns[2].HeaderText = "Situação";
                    //dgvListas.Columns[2].Width = 195;

                    DR = null;
                    int index = 0;

                    Conexao.IniciarStoredProcedure("ListaVistoria");
                    DR = Conexao.ChamarStoredProcedureCR();

                    while (DR.Read())
                    {
                        lstDataVis.Add(DR[0].ToString());
                        lstHoraVis.Add(DR[2].ToString());
                        lstCdPessoa.Add(DR.GetInt32(3));

                        string hora = (System.DateTime.Now.Hour + ":" + System.DateTime.Now.Minute).ToString();

                        if (DR.GetDate(0) == System.DateTime.Today.Date && DR.GetTime(2) < TimeSpan.Parse(hora) || DR.GetDate(0) < System.DateTime.Today.Date)
                        {
                            if (DR[4].ToString() == "" && DR[5].ToString() == "")
                            {
                                if (DR[7].ToString() != "")
                                {
                                    if (DR[7].ToString() != "1")
                                    {
                                        dgvListas.Rows.Add(DR.GetDate(0).ToString("dd/MM/yyyy"), DR[1].ToString(), "Dono de um abrigo inativo");
                                    }
                                    else
                                    {
                                        dgvListas.Rows.Add(DR.GetDate(0).ToString("dd/MM/yyyy"), DR[1].ToString(), "Dono de um abrigo Ativo");
                                    }
                                }
                                else
                                {
                                    if (DR[8].ToString() != "")
                                    {
                                        if (DR[7].ToString() != "1")
                                        {
                                            dgvListas.Rows.Add(DR.GetDate(0).ToString("dd/MM/yyyy"), DR[1].ToString(), "Adotante desbloqueado");
                                        }
                                        else
                                        {
                                            dgvListas.Rows.Add(DR.GetDate(0).ToString("dd/MM/yyyy"), DR[1].ToString(), "Adotante bloqueado");
                                        }
                                    }
                                    else
                                    {
                                        dgvListas.Rows.Add(DR.GetDate(0).ToString("dd/MM/yyyy"), DR[1].ToString(), "");
                                    }
                                }

                                dgvListas.Rows[index].DefaultCellStyle.BackColor = Color.Yellow;
                                index += 1;
                            }
                            else
                            {
                                if (DR[6].ToString() == "")
                                {
                                    if (DR[7].ToString() != "")
                                    {
                                        if (DR[7].ToString() != "1")
                                        {
                                            dgvListas.Rows.Add(DR.GetDate(0).ToString("dd/MM/yyyy"), DR[1].ToString(), "Dono de um abrigo inativo");
                                        }
                                        else
                                        {
                                            dgvListas.Rows.Add(DR.GetDate(0).ToString("dd/MM/yyyy"), DR[1].ToString(), "Dono de um abrigo Ativo");
                                        }
                                    }
                                    else
                                    {
                                        if (DR[8].ToString() != "")
                                        {
                                            if (DR[7].ToString() != "1")
                                            {
                                                dgvListas.Rows.Add(DR.GetDate(0).ToString("dd/MM/yyyy"), DR[1].ToString(), "Adotante desbloqueado");
                                            }
                                            else
                                            {
                                                dgvListas.Rows.Add(DR.GetDate(0).ToString("dd/MM/yyyy"), DR[1].ToString(), "Adotante bloqueado");
                                            }
                                        }
                                        else
                                        {
                                            dgvListas.Rows.Add(DR.GetDate(0).ToString("dd/MM/yyyy"), DR[1].ToString(), "");
                                        }
                                    }
                                }

                                index += 1;
                            }
                        }
                        else
                        {
                            if (DR[7].ToString() != "")
                            {
                                if (DR[7].ToString() != "1")
                                {
                                    dgvListas.Rows.Add(DR.GetDate(0).ToString("dd/MM/yyyy"), DR[1].ToString(), "Dono de um abrigo inativo");
                                }
                                else
                                {
                                    dgvListas.Rows.Add(DR.GetDate(0).ToString("dd/MM/yyyy"), DR[1].ToString(), "Dono de um abrigo Ativo");
                                }
                            }
                            else
                            {
                                if (DR[8].ToString() != "")
                                {
                                    if (DR[7].ToString() != "1")
                                    {
                                        dgvListas.Rows.Add(DR.GetDate(0).ToString("dd/MM/yyyy"), DR[1].ToString(), "Adotante desbloqueado");
                                    }
                                    else
                                    {
                                        dgvListas.Rows.Add(DR.GetDate(0).ToString("dd/MM/yyyy"), DR[1].ToString(), "Adotante bloqueado");
                                    }
                                }
                                else
                                {
                                    dgvListas.Rows.Add(DR.GetDate(0).ToString("dd/MM/yyyy"), DR[1].ToString(), "");
                                }
                            }
                            index += 1;
                        }
                    }

                    DR.Close();
                    DR.Dispose();
                    Conexao.FecharConexao();
                }
                #endregion
            }
        }

        private void btnHistorico_Click(object sender, EventArgs e)
        {
            SistemaMAPAN.Forms.frmHistorico Historico = new Forms.frmHistorico();

            Historico.ShowDialog(this);
        }
    }
}
