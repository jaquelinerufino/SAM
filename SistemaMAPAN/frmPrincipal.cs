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
    public partial class frmPrincipal : Form
    {
        #region INICIALIZAR COMPONENTE

        public frmPrincipal()
        {
            InitializeComponent();
        }
        #endregion

        #region EVENTOS VALIDAÇÃO/CONTROLE

        #region TOOLSTRIP PRINCIPAL

        #region TELAS

        
        #endregion

        #region SAIR

        private void sairToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion
        #endregion

        #region DO FORM

        private void frmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (lblUsuario.Text != "")
            {
                if (MessageBox.Show("Deseja realmente finalizar o sistema?", "Confirmação - " + this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    e.Cancel = true;
            }
        }
        #endregion

        #endregion

        #region Conexão

        clsConexao.clsConexao conexao = new clsConexao.clsConexao("Mapan", "localhost", "root", "root");
        OdbcDataReader dados;

        #endregion

        #region Variaveis

        public string TipoUsuario;

        #endregion

        private void frmPrincipal_Paint(object sender, PaintEventArgs e)
        {
            pnlDireita.Location = new System.Drawing.Point(this.Size.Width - pnlDireita.Size.Width, 29);
        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TipoUsuario != "Administrador")
            {
                MessageBox.Show("Atenção: Apensa um 'Administrador' tem permissão para esta área!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            SistemaMAPAN.frmCadAnimal AnimalCadastro = new SistemaMAPAN.frmCadAnimal();
            AnimalCadastro.ShowDialog();
        }

        private void consultaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SistemaMAPAN.frm_consulta_animal AnimalConsulta = new SistemaMAPAN.frm_consulta_animal();
            AnimalConsulta.ShowDialog();
        }

        private void novaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_contrato Contrato = new frm_contrato();
            Contrato.ShowDialog();
        }

        private void consultaToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frm_pesquisa_contrato pescontrato = new frm_pesquisa_contrato();
            pescontrato.ShowDialog();
        }

        private void eventoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNovoEvento Evento = new frmNovoEvento();
            Evento.ShowDialog(this);
        }

        private void vistoriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmVistoria Vis = new frmVistoria();
            Vis.ShowDialog(this);
        }

        private void consultaToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            frmAgenda Vis = new frmAgenda();
            Vis.ShowDialog();
        }

        private void financeiraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEstatistica est = new frmEstatistica();
            est.ShowDialog();
        }

        private void adoçõesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmEstatAdocao Adoc = new frmEstatAdocao();
            //Adoc.ShowDialog();
        }

        private void produtosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //prjControleDeEstoque.ControleEstoque.frmConsultaEstoque ConsultaEstoque = new prjControleDeEstoque.ControleEstoque.frmConsultaEstoque();
            //ConsultaEstoque.ShowDialog(this);
        }

        private void entregasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //prjControleDeEstoque.ControleEstoque.frmConfirmacaoEntrega ConfirmacaoEntrega = new prjControleDeEstoque.ControleEstoque.frmConfirmacaoEntrega();
            //ConfirmacaoEntrega.ShowDialog(this);
        }

        private void coffiguraçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //prjControleDeEstoque.ControleEstoque.frmConfiguracao Configuracao = new prjControleDeEstoque.ControleEstoque.frmConfiguracao();
            //Configuracao.ShowDialog(this);
        }

        private void novaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            prjdenuncia.FrmAdicionarDenuncia Den = new prjdenuncia.FrmAdicionarDenuncia();
            Den.ShowDialog();
        }

        private void consultaToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            prjdenuncia.FrmBuscarDenuncia Con = new prjdenuncia.FrmBuscarDenuncia();
            Con.ShowDialog();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            menuStrip1.Enabled = false;

            clsEventosProx Eve = new clsEventosProx();

            dados = null;
            conexao.IniciarStoredProcedure("ListaEvento");
            dados = conexao.ChamarStoredProcedureCR();

            DateTime First = Eve.FirstDay();
            DateTime Last = Eve.LastDay(First);

            while (dados.Read())
            {
                if (dados.GetDate(0).Date > System.DateTime.Today.Date && dados.GetDate(0) < Last)
                {
                    dgvEvento.Rows.Add(dados.GetDate(0).Date.ToString("dd/MM/yyyy"), dados[4].ToString());
                }
            }

            dados.Dispose();
            dados.Close();
            conexao.FecharConexao();

            if (dgvEvento.Rows.Count == 0)
            {
                dados = null;
                conexao.IniciarStoredProcedure("ListaVistoria");
                dados = conexao.ChamarStoredProcedureCR();

                while (dados.Read())
                {
                    if (dados.GetDate(0).Date >= System.DateTime.Today.Date && dados.GetDate(0) <= Last)
                    {
                        dgvEvento.Rows.Add(dados.GetDate(0).Date.ToString("dd/MM/yyyy"), dados[1].ToString());
                    }
                }

                dados.Dispose();
                dados.Close();
                conexao.FecharConexao();

                if (dgvEvento.Rows.Count == 0)
                {
                    dgvEvento.Visible = false;
                    rdbEvento.Visible = false;
                    rdbVistoria.Visible = false;
                    lblInfo.Visible = false;
                }
                else
                {
                    rdbVistoria.Checked = true;
                }
            }
            else
            {
                rdbEvento.Checked = true;
            }

            dados = null;
            conexao.IniciarStoredProcedure("ListaProdutosComEstoqueAbaixoDoLimite");
            dados = conexao.ChamarStoredProcedureCR();

            while(dados.Read())
            {
                dgvEstoque.Rows.Add(dados[5].ToString(), dados[2].ToString(), dados[6].ToString());
            }

            dados.Dispose();
            dados.Close();
            conexao.FecharConexao();

            Frm_login Login = new Frm_login();
            Login.ShowDialog(this);

            if (lblUsuario.Text == "")
            {
                Close();
            }
        }

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

        private void dgvEvento_Paint(object sender, PaintEventArgs e)
        {
            controlarTamanhoGrid(dgvEvento,22,23,227,420);
        }

        private void transferenciaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TipoUsuario != "Administrador")
            {
                MessageBox.Show("Atenção: Apensa um 'Administrador' tem permissão para esta área!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            frm_tranferir trans = new frm_tranferir();
            trans.ShowDialog();
        }

        private void rdbEvento_CheckedChanged(object sender, EventArgs e)
        {
            dgvEvento.Rows.Clear();
            lblInfo.Text = "Eventos da Semana!";

            clsEventosProx Eve = new clsEventosProx();

            dados = null;
            conexao.IniciarStoredProcedure("ListaEvento");
            dados = conexao.ChamarStoredProcedureCR();

            DateTime First = Eve.FirstDay();
            DateTime Last = Eve.LastDay(First);

            while (dados.Read())
            {
                if (dados.GetDate(0).Date >= System.DateTime.Today.Date && dados.GetDate(0) <= Last)
                {
                    dgvEvento.Rows.Add(dados.GetDate(0).Date.ToString("dd/MM/yyyy"), dados[4].ToString());
                }
            }

            dados.Dispose();
            dados.Close();
            conexao.FecharConexao();
        }

        private void rdbVistoria_CheckedChanged(object sender, EventArgs e)
        {
            dgvEvento.Rows.Clear();
            lblInfo.Text = "Vistorias da Semana!";

            clsEventosProx Eve = new clsEventosProx();
            DateTime First = Eve.FirstDay();
            DateTime Last = Eve.LastDay(First);

            dados = null;
            conexao.IniciarStoredProcedure("ListaVistoria");
            dados = conexao.ChamarStoredProcedureCR();

            while (dados.Read())
            {
                if (dados.GetDate(0).Date >= System.DateTime.Today.Date && dados.GetDate(0) <= Last)
                {
                    dgvEvento.Rows.Add(dados.GetDate(0).Date.ToString("dd/MM/yyyy"), dados[1].ToString());
                }
            }

            dados.Dispose();
            dados.Close();
            conexao.FecharConexao();
        }

        private void novoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (TipoUsuario != "Administrador")
            {
                MessageBox.Show("Atenção: Apensa um 'Administrador' tem permissão para esta área!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            frmAdicionaProtetor ProAdd = new frmAdicionaProtetor();
            ProAdd.ShowDialog();
        }

        private void consultaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (TipoUsuario != "Administrador")
            {
                frmPesquisaProtetor PesPro = new frmPesquisaProtetor();
                PesPro.btnNovo.Enabled = false;
                PesPro.btnEditar.Enabled = false;
                PesPro.ShowDialog();
            }
            else
            {
                frmPesquisaProtetor PesPro = new frmPesquisaProtetor();
                PesPro.ShowDialog();
            }
        }

        private void doaçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDoacao Doacao = new frmDoacao();
            Doacao.ShowDialog();
        }

        private void consultaToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            frmPesquisaDoadores PesDo = new frmPesquisaDoadores();
            PesDo.ShowDialog();
        }

        private void loginToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Frm_login Login = new Frm_login();

            if (lblUsuario.Text != "")
            {
                Login.Logado = true;
            }

            Login.ShowDialog();
        }

        private void novoToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Frm_usuario_novo NovoUsuario = new Frm_usuario_novo();
            NovoUsuario.ShowDialog();
        }

        private void colaboradoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCadastroDoador Doador = new frmCadastroDoador();
            Doador.ShowDialog();
        }

        private void dataGridView1_Paint(object sender, PaintEventArgs e)
        {
            controlarTamanhoGrid(dgvEstoque, 22, 23, 304,357);
        }
    }
}
