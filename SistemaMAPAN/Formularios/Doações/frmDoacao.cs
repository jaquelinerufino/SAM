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
    public partial class frmDoacao : Form
    {
        public frmDoacao()
        {
            InitializeComponent();
        }

        #region Conexão com o Banco de Dados
        clsConexao.clsConexao Conexao = new clsConexao.clsConexao("mapan", "localhost", "root", "root");
        #endregion

        #region Declaração de Variáveis Globais
        OdbcDataReader dados;
        List<int> cdDoacao = new List<int>();
        List<int> cdTipoDoado = new List<int>();
        List<double> valorQuantidade = new List<double>();
        bool Salvar = false;
        #endregion

        #region Form: Load
        private void frmDoacao_Load(object sender, EventArgs e)
        {
            pnlMensalidade.Visible = false;
            pnlQtd.Visible = false;

            #region ComandoCR: Function VerificaTipoDeDoador
            dados = Conexao.ComandoCR(string.Format("SELECT VerificaTipoDeDoador({0})", frmCadastroDoador.CodPessoa));
            cdTipoDoado.Clear();
            while (dados.Read())
            {
                int resultado = dados.GetInt32(0);
            }

            dados.Close();
            dados.Dispose();
            #endregion            

            #region ComboBox: Popular Tipo de doações
            cdDoacao.Clear();
            cmbRegTipo.Items.Clear();
            dados = null;
            dados = Conexao.ComandoCR("SELECT cd_tipo_doacao, nm_tipo_doacao FROM tipo_doacao;");
            while (dados.Read())
            {
                cdDoacao.Add(dados.GetInt32(0));
                cmbRegTipo.Items.Add(dados.GetString(1));
            }

            dados.Close();
            dados.Dispose();
            Conexao.FecharConexao();
            #endregion

            #region DataGridViewButtonColumn
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            dtgDoacoes.Columns.Add(btn);
            btn.HeaderText = "";
            btn.Text = "Remover Item";
            btn.Name = "btn";
            btn.UseColumnTextForButtonValue = true;
            #endregion            
        }
        #endregion

        #region ComboBox SelectedIndexChanged: cmbRegTipo
        private void cmbRegTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cdDoacao[cmbRegTipo.SelectedIndex] == 1)
            {
                pnlMensalidade.Visible = true;
                pnlQtd.Visible = false;
                txtValor.Clear();
                txtValor.Focus();
            }
            else
            {
                pnlMensalidade.Visible = false;
                pnlQtd.Visible = true;
                txtQuantidade.Clear();
                txtQuantidade.Focus();
            }
        }
        #endregion

        #region Botão: Adicionar à Lista
        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            string qtd;

            cdTipoDoado.Add(cdDoacao[cmbRegTipo.SelectedIndex]);

            if (txtQuantidade.Text == "")
            {
                valorQuantidade.Add(Convert.ToDouble(txtValor.Text.Replace(" ", "").Replace("R", "").Replace("$", "").PadLeft(8, '0').PadRight(8, '0')));
                qtd = txtValor.Text.Replace(" ", "");
            }
            else
            {
                valorQuantidade.Add(Convert.ToDouble(txtQuantidade.Text));
                qtd = txtQuantidade.Text;
            }

            dtgDoacoes.Rows.Add(cmbRegTipo.Text, qtd);
            txtQuantidade.Clear();
            txtValor.Clear();
            cmbRegTipo.Text = "";
        }
        #endregion

        #region DataGridViewButton: Remover Item
        private void dtgDoacoes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                dtgDoacoes.Rows.Remove(dtgDoacoes.CurrentRow);
            }

        }
        #endregion

        #region Botão: Salvar
        private void btnSalvar_Click(object sender, EventArgs e)
        {   
            #region Doação

            #region Confirmação de dados
            if (Salvar == false)
            {
                MessageBox.Show("Por favor, verifique os dados! \n Eles não podem ser modificados!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                Salvar = true;
                return;
            }
            #endregion

            #region Stored Procedure: NovaColaboracao
            for (int i = 0; i <= dtgDoacoes.Rows.Count - 1; i++)
            {
                string Data = System.DateTime.Now.Date.Year.ToString() + System.DateTime.Now.Date.Month.ToString() + System.DateTime.Now.Date.Day.ToString();

                Conexao.IniciarStoredProcedure("NovaColaboracao");

                #region Parâmetros
                Conexao.AdicionarParametroTexto(Data);
                Conexao.AdicionarParametroInteiro(cdTipoDoado[i]);

                if (frmPesquisaDoadores.codPessoa.ToString() == "")
                {
                    Conexao.AdicionarParametroInteiro(frmCadastroDoador.CodPessoa);
                }
                else
                {
                    Conexao.AdicionarParametroInteiro(frmPesquisaDoadores.codPessoa);
                }

                Conexao.AdicionarParametroDecimal(valorQuantidade[i]);
                #endregion

                try
                {
                    Conexao.ChamarStoredProcedureSR();
                }
                catch
                {
                    MessageBox.Show("Atenção: Esta pessoa já fez uma doação deste tipo hoje! " + dtgDoacoes.Rows[i].Cells[0].Value.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            #endregion
            
            MessageBox.Show("Doação salva com sucesso", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
            #endregion
        }
        #endregion               

        #region Botão: Cancelar
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if ((txtQuantidade.Text != "") ||
               (cmbRegTipo.Text != "") ||
               (cmbRegTipo.Text != ""))
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

        #region Botão: Limpar
        private void btnLimpar_Click(object sender, EventArgs e)
        {
            Limpar();
        }
        #endregion

        #region Criar novo tipo de doacao
        private void btnAbrirTipoDoacao_Click(object sender, EventArgs e)
        {
            frmNovoTipoDoacao NovoTipoDoacao = new frmNovoTipoDoacao();

            NovoTipoDoacao.ShowDialog(this);

            #region ComboBox: Popular Tipo de doações
            cdDoacao.Clear();
            cmbRegTipo.Items.Clear();
            dados = null;
            dados = Conexao.ComandoCR("SELECT cd_tipo_doacao, nm_tipo_doacao FROM tipo_doacao;");
            while (dados.Read())
            {
                cdDoacao.Add(dados.GetInt32(0));
                cmbRegTipo.Items.Add(dados.GetString(1));
            }

            dados.Close();
            dados.Dispose();
            Conexao.FecharConexao();
            #endregion
        }
        #endregion

        #region Método: Limpar
        private void Limpar()
        {
            cmbRegTipo.Text = "";
            txtQuantidade.Clear();
            txtValor.Clear();
            dtgDoacoes.Rows.Clear();
        }
        #endregion        

        private void txtValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            //MUDE AQUI
            //{
            string v = txtValor.Text;
            int maxLength = 8;
            //}

            int iInteiros = 0;
            int d, r = 0;
            bool bInteiros = true;
            int iCursor = 0;
            bool bCursor = false;

            if (e.KeyChar != 8)
            {
                if (v.Length < maxLength && !(e.KeyChar < 48 || e.KeyChar > 57))
                {
                    if (v.Length > 3)
                    {
                        for (int i = 0; i < v.Length; i++)
                        {
                            if (bInteiros)
                                if (v.Substring(i, 1) == ",")
                                    bInteiros = false;
                                else
                                    iInteiros++;

                            if (v.Substring(i, 1) == ".")
                                v = v.Remove(i, 1);
                        }

                        d = (iInteiros + 1) / 3;
                        r = (iInteiros + 1) % 3;

                        if (v.Substring(0, 1) == "0")
                            v = v.Remove(0, 1);

                        for (int i = 0; i < v.Length; i++)
                            if (v.Substring(i, 1) == ",")
                            {
                                v = v.Remove(i, 1);
                                iCursor = i + 1;
                                bCursor = true;
                            }

                        if (bCursor)
                        {
                            v = v.Insert(iCursor, ",");
                            bCursor = false;
                        }

                        if (d > 0)
                        {
                            if (r == 1)
                                for (int i = 0; i < d; i++)
                                    v = v.Insert((i * 4) + 1, ".");

                            if (r == 2)
                                for (int i = 0; i < d; i++)
                                    v = v.Insert((i * 4) + 2, ".");

                            if (r == 0)
                                for (int i = 0; i < d; i++)
                                    v = v.Insert((i * 4), ".");

                            if (v.Substring(0, 1) == ".")
                                v = v.Remove(0, 1);
                        }
                    }
                    else
                    {
                        if (v.Length == 0)
                            if (e.KeyChar != 48)
                                v = "0,0";
                            else
                                e.KeyChar = Convert.ToChar(0);
                    }
                }
                else
                {
                    e.KeyChar = Convert.ToChar(0);
                }
            }
            else
            {
                for (int i = 0; i < v.Length; i++)
                {
                    if (bInteiros)
                        if (v.Substring(i, 1) == ",")
                            bInteiros = false;
                        else
                            iInteiros++;

                    if (v.Substring(i, 1) == ".")
                        v = v.Remove(i, 1);
                }

                d = (iInteiros - 1) / 3;
                r = (iInteiros - 1) % 3;

                if (v.Length > 4)
                {
                    for (int i = 0; i < v.Length; i++)
                        if (v.Substring(i, 1) == ",")
                        {
                            v = v.Remove(i, 1);
                            iCursor = i - 1;
                            bCursor = true;
                        }

                    if (bCursor)
                    {
                        v = v.Insert(iCursor, ",");
                        bCursor = false;
                    }
                }
                else
                {
                    if (v != "")
                        if (v.Substring(2, 1) == "0")
                            v = "";
                        else
                        {
                            v = v.Remove(1, 1);
                            v = v.Insert(0, "0,");
                        }
                }

                if (d > 0)
                {
                    if (r == 1)
                        for (int i = 0; i < d; i++)
                            v = v.Insert((i * 4) + 1, ".");

                    if (r == 2)
                        for (int i = 0; i < d; i++)
                            v = v.Insert((i * 4) + 2, ".");

                    if (r == 0)
                        for (int i = 0; i < d; i++)
                            v = v.Insert((i * 4), ".");

                    if (v.Substring(0, 1) == ".")
                        v = v.Remove(0, 1);
                }
            }

            //TAMBÉM MUDE AQUI
            //{
            txtValor.Text = v;
            //}
            txtValor.Select(txtValor.TextLength, 1);
        }
    }
}
