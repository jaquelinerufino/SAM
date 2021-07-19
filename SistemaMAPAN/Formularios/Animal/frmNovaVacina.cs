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
    public partial class frmNovaVacina : Form
    {
        public frmNovaVacina()
        {
            InitializeComponent();
        }

        #region Conexão Com o Banco de Dados
        clsConexao.clsConexao Conexao = new clsConexao.clsConexao("Mapan", "localhost", "root", "root");
        OdbcDataReader dados;
        #endregion        

        #region Variaveis
        int Indice = 0;
        List<int> lstCdVacina = new List<int>();
        List<int> lstCdDestinatario = new List<int>();

        //List<int> lstCdVacinaAdd = new List<int>();
        //List<int> lstCdDestinatarioAdd = new List<int>();

        List<int> lstCdVacinaExc = new List<int>();

        #endregion

        #region Form: Load
        private void frmNovaVacina_Load(object sender, EventArgs e)
        {
            PopularComboBox();
        }
        #endregion        

        #region ComboBox: SelectedIndexChanged
        private void cmbVacinas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (btnSalvar.Text == "Editar")
            {
                if (cmbVacinas.SelectedIndex != Indice)
                {
                    MessageBox.Show("Você está editando uma vacina." + "\n" + "Não é possível escolher outra vacina nesse momento!", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    cmbVacinas.SelectedIndex = Indice;
                    return;
                }
            }
        }
        #endregion       

        #region Botão: Adicionar/Editar Vacina
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (btnSalvar.Text == "A&dicionar")
            {
                #region Adicionar nova vacina
                #region Variaveis
                bool resultado = true;
                int CodigoDestinacao = 0;
                #endregion

                #region Validação
                if (cmbVacinas.Text == "")
                {
                    MessageBox.Show("Por favor, preencha o campo para adicionar uma nova vacina!", this.Text,MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    cmbVacinas.Focus();
                    return;
                }

                if (rdbCanina.Checked)
                {
                    CodigoDestinacao = 1;
                }

                if (rdbFelina.Checked)
                {
                    CodigoDestinacao = 2;
                }

                if (rdbAmbas.Checked)
                {
                    CodigoDestinacao = 3;
                }
                #endregion

                #region Function: Verifica Registro Tipo de Doação
                dados = Conexao.ComandoCR(string.Format("SELECT verificaRegistroVacina2('{0}');", cmbVacinas.Text));

                while (dados.Read())
                {
                    resultado = dados.GetBoolean(0);
                }

                dados.Close();
                dados.Dispose();
                Conexao.FecharConexao();
                #endregion

                if (resultado)
                {
                    if (MessageBox.Show("Essa vacina já está incluída \n" + "Você deseja editá-la?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                    {
                        btnSalvar.Text = "Editar";
                        Indice = cmbVacinas.SelectedIndex;
                        return;
                    }
                }

                #region Adicionar Nova Vacina
                Conexao.IniciarStoredProcedure("InserirNovaVacina");
                Conexao.AdicionarParametroTexto(cmbVacinas.Text);
                Conexao.AdicionarParametroInteiro(CodigoDestinacao);
                Conexao.ChamarStoredProcedureSR();
                #endregion
                               
                PopularComboBox();      
                MessageBox.Show("Vacina adicionada com sucesso!", this.Text);
                cmbVacinas.Text = "";
                #endregion
            }
            else
            {
                #region Editar uma vacina existente

                #region Variaveis
                int CodigoDestinacao = 0;
                #endregion

                #region Validação
                if (rdbCanina.Checked)
                {
                    CodigoDestinacao = 1;
                }

                if (rdbFelina.Checked)
                {
                    CodigoDestinacao = 2;
                }

                if (rdbAmbas.Checked)
                {
                    CodigoDestinacao = 3;
                }
                #endregion

                #region Stored Procedure: Editar vacina
                Conexao.IniciarStoredProcedure("EditarVacina");

                #region Parâmetros
                Conexao.AdicionarParametroInteiro(lstCdVacina[Indice]);
                Conexao.AdicionarParametroTexto(cmbVacinas.Text);
                Conexao.AdicionarParametroInteiro(CodigoDestinacao);
                #endregion

                Conexao.ChamarStoredProcedureSR();
                #endregion

                btnSalvar.Text = "A&dicionar";

                PopularComboBox();
                cmbVacinas.SelectedIndex = Indice;
                MessageBox.Show("Vacina editada com sucesso!", this.Text);

                cmbVacinas.Text = "";
                #endregion
            }
        }
        #endregion

        #region Botão: Excluir/Editar Vacina
        private void btnExcluir_Click(object sender, EventArgs e)
        {
            
            #region Validação
            if (cmbVacinas.SelectedIndex < 0)
            {
                MessageBox.Show("Por favor, escolha alguém para excluir!", this.Text);
                cmbVacinas.Focus();
                return;
            }
            #endregion

            #region Variaveis
            bool resultado = false;
            int SelectedIndexComboTipo = cmbVacinas.SelectedIndex;
            #endregion

            #region Function: Verifica Registro Vacina
            dados = Conexao.ComandoCR(string.Format("SELECT verificaRegistroVacina('{0}');", lstCdVacina[SelectedIndexComboTipo]));

            while (dados.Read())
            {
                resultado = dados.GetBoolean(0);
            }

            dados.Close();
            dados.Dispose();
            Conexao.FecharConexao();
            #endregion

            if (resultado == true)
            {
                #region Editar Vacina
                if (MessageBox.Show("Essa vacina não pode ser apagada" + "\n" + "Você deseja editá-la?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    cmbVacinas.Focus();
                    btnSalvar.Text = "Editar";
                    Indice = SelectedIndexComboTipo;
                    btnExcluir.Enabled = false;
                    return;
                }
                #endregion
            }
            else
            {
                #region Excluir Vacina
                if (MessageBox.Show("Deseja realmente apagar essa vacina?" + "\n" + "Essa ação não pode ser desfeita!", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    #region Stored Procedure: Excluir tipo da doação
                    Conexao.IniciarStoredProcedure("ApagarVacina");

                    #region Parametros
                    Conexao.AdicionarParametroInteiro(lstCdVacina[SelectedIndexComboTipo]);
                    #endregion

                    Conexao.ChamarStoredProcedureSR();
                    #endregion

                    MessageBox.Show("Tipo de doação excluído com sucesso!", this.Text);

                    PopularComboBox();
                    cmbVacinas.Text = "";
                }
                #endregion
            }
        }
        #endregion

        #region Botão: Cancelar
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (cmbVacinas.Text != "")
            {
                if (MessageBox.Show("Atenção! Dados serão perdidos!" + "\n" + "Tem certeza que deseja sair?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    this.Close();
                }
                else
                {
                    return;
                }
            }
            else
            {
                this.Close();
            }
        }
        #endregion

        #region Método: PopularComboBox
        private void PopularComboBox()
        {
            lstCdVacina.Clear();
            cmbVacinas.Items.Clear();
            dados = null;
            dados = Conexao.ComandoCR("SELECT cd_vacina, nm_vacina FROM vacina;");
            while (dados.Read())
            {
                lstCdVacina.Add(dados.GetInt32(0));
                cmbVacinas.Items.Add(dados.GetString(1));
            }

            dados.Close();
            dados.Dispose();
            Conexao.FecharConexao();
        }
        #endregion
    }
}
