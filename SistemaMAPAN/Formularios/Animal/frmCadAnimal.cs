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
    public partial class frmCadAnimal : Form
    {

        #region InitializeComponents
        public frmCadAnimal()
        {
            InitializeComponent();
        }
        #endregion

        #region Variáveis Globais
        string Hora, Data, Venc;
        int Ano, AniCod;

        #region List<int>
        List<int> lstCdPessoa = new List<int>();
        public List<int> lstCdVacina = new List<int>();
        List<int> lstCdEspecie = new List<int>();
        List<int> lstCdPorte = new List<int>();
        List<int> lstCdCor = new List<int>();
        List<int> codigoProtetores = new List<int>();
        List<int> codigoProtetoresComVaga = new List<int>();       
        #endregion

        #region List<string>
        public List<string> dtVencVacinacao = new List<string>();
        public List<string> dtVacinacao = new List<string>();
        public List<string> NmVacina = new List<string>(); 
        #endregion

        #region Propriedades
        static public int QtdMax { set; get; }
        static public int QtdAnimais { set; get; }
        static public string Endereco { set; get; }
        static public int CodEspecie { set; get; }
        static public int CodAnimal { set; get; }
        #endregion        
        #endregion

        #region Form: Load
        private void frmCadAnimal_Load(object sender, EventArgs e)
        {
            pnlConf.Visible = false;
            pnlConf.Location = new Point(0, 0);
            pnlConf.SendToBack();

            #region Data
            lblData.Text = System.DateTime.Today.ToString("dd/MM/yyyy");
            Ano = System.DateTime.Today.Year + 1;
            Venc = Ano.ToString() + "-" +  System.DateTime.Today.Month.ToString() + "-" + System.DateTime.Today.Day.ToString();
            #endregion

            #region Preencher ComboBoxes
            #region Espécie
            dados = Conexao.ComandoCR("SELECT cd_especie, nm_especie FROM especie");

            lstCdEspecie.Clear();
            cmbEspecieAnimal.Items.Clear();

            while (dados.Read())
            {
                lstCdEspecie.Add(dados.GetInt32(0));
                cmbEspecieAnimal.Items.Add(dados.GetString(1));
            }
            dados.Close();
            dados.Dispose();
            Conexao.FecharConexao();
            #endregion

            #region Sexo
            cmbSexoAnimal.Items.Clear();
            cmbSexoAnimal.Items.Add("M");
            cmbSexoAnimal.Items.Add("F");
            #endregion

            #region Porte
            dados = Conexao.ComandoCR("SELECT cd_porte, nm_porte FROM porte");

            lstCdPorte.Clear();
            cmbPorteAnimal.Items.Clear();

            while (dados.Read())
            {
                lstCdPorte.Add(dados.GetInt32(0));
                cmbPorteAnimal.Items.Add(dados.GetString(1));
            }

            dados.Close();
            dados.Dispose();
            Conexao.FecharConexao();
            #endregion

            #region Cor
            dados = Conexao.ComandoCR("SELECT cd_cor, nm_cor FROM cor");

            cmbCorAnimal.Items.Clear();
            lstCdCor.Clear();

            while (dados.Read())
            {
                lstCdCor.Add(dados.GetInt32(0));
                cmbCorAnimal.Items.Add(dados.GetString(1));
            }
            dados.Close();
            dados.Dispose();
            Conexao.FecharConexao();
            #endregion

            #region Abrigo
            #region Comando CR: CodigoProtetores
            dados = null;
            dados = Conexao.ComandoCR("SELECT cd_pessoa FROM pessoa WHERE ic_abrigo_ativo = 1;");
            while (dados.Read())
            {
                codigoProtetores.Add(dados.GetInt16(0));
            }

            dados.Close();
            dados.Dispose();
            Conexao.FecharConexao();
            #endregion

            #region Function: VerificaProtetorComNoMinimoUmaVaga
            for (int i = 0; i < codigoProtetores.Count; i++)
            {
                dados = null;
                dados = Conexao.ComandoCR(string.Format("SELECT VerificaProtetorComNoMinimoUmaVaga({0});", codigoProtetores[i]));

                while (dados.Read())
                {
                    if (dados.GetBoolean(0))
                    {
                        codigoProtetoresComVaga.Add(codigoProtetores[i]);
                    }
                }
                dados.Close();
                dados.Dispose();
                Conexao.FecharConexao();
            }
            #endregion

            #region Function: BuscarNomeProtetor
            for (int i = 0; i < codigoProtetoresComVaga.Count; i++)
            {
                dados = null;
                dados = Conexao.ComandoCR(string.Format("SELECT BuscarNomeProtetor({0})", codigoProtetoresComVaga[i]));
                while (dados.Read())
                {
                    lstCdPessoa.Add(codigoProtetoresComVaga[i]);
                    cmbProtetor.Items.Add(dados.GetString(0));
                }
            }
            #endregion
            #endregion
            #endregion

        }
        #endregion

        #region Conexao do banco
        clsConexao.clsConexao Conexao = new clsConexao.clsConexao("mapan", "localhost", "root", "root");
        OdbcDataReader dados;
        #endregion

        #region KeyPress
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar) || //Espaço
            char.IsPunctuation(e.KeyChar)) //Pontuação
                e.Handled = true;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar) || //Espaço
            char.IsPunctuation(e.KeyChar)) //Pontuação
                e.Handled = true;
        }
        #endregion

        #region Seleção das Combos
        private void cmbProtetor_SelectedIndexChanged(object sender, EventArgs e)
        {
            int CdPessoa = 0;

            if (cmbProtetor.SelectedIndex == -1)
            {
                return;
            }

            #region Informações sobre o abrigo
            CdPessoa = codigoProtetoresComVaga[cmbProtetor.SelectedIndex];
            btnMostrar.Enabled = true;

            dados = null;

            #region Stored Procedure: InformacaoAbrigo
            Conexao.IniciarStoredProcedure("InformacaoAbrigo");

            #region Parâmetros
            Conexao.AdicionarParametroInteiro(CdPessoa);
            #endregion

            dados = Conexao.ChamarStoredProcedureCR();

            while (dados.Read())
            {
                #region Leitura de Dados

                if (dados[0].ToString() == "")
                {
                    QtdAnimais = dados.GetInt32(0);
                }
                else
                {
                    QtdAnimais = 0;
                }

                QtdMax = dados.GetInt32(1);
                Endereco = dados[2].ToString() +", " + Convert.ToString(dados.GetInt32(3)) + " - " + dados[4].ToString();
                #endregion
            }
            dados.Close();
            dados.Dispose();
            Conexao.FecharConexao();

            #endregion            
            #endregion
        }
        #endregion

        #region Confirmação de dados
        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            #region Validação
            if (txtCodAnimal.Text == "")
            {
                MessageBox.Show("Preencha campo de código do animal!",this.Text,MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            else if (cmbEspecieAnimal.SelectedIndex == -1)
            {
                MessageBox.Show("Preencha campo de espécie do animal!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (cmbSexoAnimal.SelectedIndex == -1)
            {
                MessageBox.Show("Preencha campo de sexo do animal!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (cmbPorteAnimal.SelectedIndex == -1)
            {
                MessageBox.Show("Preencha campo de porte do animal!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (cmbCorAnimal.SelectedIndex == -1)
            {
                MessageBox.Show("Preencha campo de cor do animal!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (txtIdadeAnimal.Text == "")
            {
                MessageBox.Show("Preencha campo de idade do animal!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (txtDescricaoAnimal.Text == "")
            {
                MessageBox.Show("Preencha campo de descrição do animal!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (cmbProtetor.Text == "")
            {
                MessageBox.Show("Preencha campo de protetor!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #endregion

            #region Confirmação
            lblConfData.Text = lblData.Text;
            lblConfCod.Text = txtCodAnimal.Text;
            lblConfNome.Text = txtNomeAnimal.Text;
            lblConfEspecie.Text = cmbEspecieAnimal.SelectedItem.ToString();
            lblConfSexo.Text = cmbSexoAnimal.SelectedItem.ToString();
            lblConfPorte.Text = cmbPorteAnimal.SelectedItem.ToString();
            lblConfCor.Text = cmbCorAnimal.SelectedItem.ToString();
            lblConfIdade.Text = txtIdadeAnimal.Text;
            lblConfProtetor.Text = cmbProtetor.SelectedItem.ToString();
            txtConfDesc.Text = txtDescricaoAnimal.Text;

            for (int i = 0; i <= lstCdVacina.Count-1; i++)
            {
                dtg2.Rows.Add(NmVacina[i], dtVacinacao[i], dtVencVacinacao[i]);                
            }

            pnlInicio.Visible = false;
            pnlConf.Visible = true;
            #endregion

            this.Close();
        }
        #endregion

        #region Botões Sim/Não
        private void btnSim_Click(object sender, EventArgs e)
        {
            CodAnimal = Convert.ToInt32(txtCodAnimal.Text);
            Data = System.DateTime.Today.Date.ToString("yyyy-MM-dd");
            Hora = System.DateTime.Now.ToShortTimeString();

            #region Cadastro
            dados = null;

            #region Function: verificaCodAnimal
            dados = Conexao.ComandoCR(string.Format("SELECT verificaCodAnimal({0})", txtCodAnimal.Text));

            bool ExisteCod = false;
            while (dados.Read())
            {
                ExisteCod = dados.GetBoolean(0);
            }
            #endregion            

            if (ExisteCod == false)
            {
                #region Stored Procedure: Cadastrar_animal
                Conexao.IniciarStoredProcedure("Cadastrar_animal");

                #region Parâmetros
                Conexao.AdicionarParametroInteiro(CodAnimal);
                Conexao.AdicionarParametroTexto(txtNomeAnimal.Text);
                Conexao.AdicionarParametroInteiro(lstCdEspecie[cmbEspecieAnimal.SelectedIndex]);
                Conexao.AdicionarParametroTexto(cmbSexoAnimal.Text);
                Conexao.AdicionarParametroInteiro(lstCdPorte[cmbPorteAnimal.SelectedIndex]);
                Conexao.AdicionarParametroInteiro(lstCdCor[cmbCorAnimal.SelectedIndex]);
                Conexao.AdicionarParametroInteiro(int.Parse(txtIdadeAnimal.Text));
                Conexao.AdicionarParametroTexto(txtDescricaoAnimal.Text);
                Conexao.AdicionarParametroTexto(Data);
                Conexao.AdicionarParametroTexto(Hora);
                Conexao.AdicionarParametroInteiro(lstCdPessoa[cmbProtetor.SelectedIndex]);
                #endregion                

                Conexao.ChamarStoredProcedureSR();
                Conexao.FecharConexao();
                #endregion

                #region Stored Procedure: inserirVacina
                int total = lstCdVacina.Count - 1;
                for (int i = 0; i <= total; i++)
                {
                    Conexao.IniciarStoredProcedure("inserirVacina");

                    Conexao.AdicionarParametroInteiro(CodAnimal);
                    Conexao.AdicionarParametroInteiro(lstCdVacina[i]);
                    Conexao.AdicionarParametroTexto(DateTime.Parse(dtVacinacao[i]).Date.ToString("yyyy-MM-dd"));
                    Conexao.AdicionarParametroTexto(DateTime.Parse(dtVencVacinacao[i]).Date.ToString("yyyy-MM-dd"));

                    Conexao.ChamarStoredProcedureSR();
                }
                #endregion

                Limpar();
                MessageBox.Show("Animal cadastrado com sucesso!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                pnlConf.Visible = false;
                pnlInicio.Visible = true;

            }
            else
            {
                MessageBox.Show("Atenção! O código digitado já está cadastrado!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            #endregion
        }        

        private void btnNao_Click(object sender, EventArgs e)
        {
            pnlConf.Visible = false;
            pnlInicio.Visible = true;
        }
        #endregion

        #region Sair
        private void btnSair_Click(object sender, EventArgs e)
        {
            if (txtCodAnimal.Text != "" ||
                txtNomeAnimal.Text != "" ||
                cmbEspecieAnimal.SelectedIndex != -1 ||
                cmbSexoAnimal.SelectedIndex != -1 ||
                cmbPorteAnimal.SelectedIndex != -1 ||
                cmbCorAnimal.SelectedIndex != -1 ||
                txtIdadeAnimal.Text != "" ||
                cmbProtetor.SelectedIndex != -1 ||
                txtDescricaoAnimal.Text != "")
            {
                if (MessageBox.Show("Dados serão perdidos, deseja mesmo sair?", "Sair", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
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

        #region Mostrar informações do abrigo.
        private void btnMostrar_Click(object sender, EventArgs e)
        {
            frmInfo Informacao = new frmInfo();
            Informacao.ShowDialog(this);
        }
        #endregion

        #region Adicionar Vacina
        private void btnAddVacina_Click(object sender, EventArgs e)
        {
            if (cmbEspecieAnimal.SelectedIndex == -1)
            {
                MessageBox.Show("Você deve selecionar a espécie do animal!");
            }
            else
            {
                CodEspecie = lstCdEspecie[cmbEspecieAnimal.SelectedIndex];             
                frmVacinas NovaVacina = new frmVacinas();
                NovaVacina.Caminho = 1;
                NovaVacina.ShowDialog(this);

                for (int i = 0; i <= lstCdVacina.Count - 1; i++)
                {
                    dtvVacinas.Rows.Add(NmVacina[i], dtVacinacao[i], dtVencVacinacao[i]);
                }
            }
        }
        #endregion

        #region Botão: AutoComplete
        private void btnAutoComplete_Click(object sender, EventArgs e)
        {
            txtCodAnimal.Text = "12356";
            txtNomeAnimal.Text = "River";
            txtIdadeAnimal.Text = "2";
            txtDescricaoAnimal.Text = "Pelos volumosos e temperamento geralmente calmo";
            cmbCorAnimal.SelectedIndex = 0;
            cmbEspecieAnimal.SelectedIndex = 0;
            cmbPorteAnimal.SelectedIndex = 2;
            cmbSexoAnimal.SelectedIndex = 1;
            cmbProtetor.SelectedIndex = 0;
        }
        #endregion        

        #region Método: Limpar()
        private void Limpar()
        {
            txtCodAnimal.Clear();
            txtNomeAnimal.Clear();
            txtIdadeAnimal.Clear();
            txtDescricaoAnimal.Clear();
            cmbEspecieAnimal.SelectedIndex = -1;
            cmbSexoAnimal.SelectedIndex = -1;
            cmbPorteAnimal.SelectedIndex = -1;
            cmbCorAnimal.SelectedIndex = -1;
            cmbProtetor.SelectedIndex = -1;
        }
        #endregion

        #region Método: transferirVacina
        public void transferirVacina(int cod, string nome, string data, string dataV)
        {
            lstCdVacina.Add(cod);
            NmVacina.Add(nome);
            dtVacinacao.Add(data);
            dtVencVacinacao.Add(dataV);
        }
        #endregion        
    }
}
