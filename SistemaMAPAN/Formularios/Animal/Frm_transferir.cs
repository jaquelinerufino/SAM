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
    public partial class frm_tranferir : Form
    {
        public frm_tranferir()
        {
            InitializeComponent();
        }

        clsConexao.clsConexao Conexao = new clsConexao.clsConexao("mapan", "localhost", "root", "root");
        OdbcDataReader DR;

        List<int> lstCdAbrigoOrigem = new List<int>();
        List<int> lstCdAbrigoDestino = new List<int>();

        List<int> lstCdAnimaisOrigem = new List<int>();
        //List<int> lstCdAnimaisDestino = new List<int>();

        List<int> lstCdNovosAnimaisDestino = new List<int>();
        List<int> lstCdRemoverAnimaisOrigem = new List<int>();

        #region FormLoad
        private void Form1_Load(object sender, EventArgs e)
        {
            #region Carrega a cbb_origem e a cbb_destino

            DR = null;
            Conexao.IniciarStoredProcedure("ListaAbrigoPessoa");
            DR = Conexao.ChamarStoredProcedureCR();

            while(DR.Read())
            {
                cbb_origem.Items.Add(DR[0].ToString());
                cbb_destino.Items.Add(DR[0].ToString());

                lstCdAbrigoOrigem.Add(DR.GetInt32(1));
                lstCdAbrigoDestino.Add(DR.GetInt32(1));
            }

            DR.Close();
            DR.Dispose();
            Conexao.FecharConexao();

            #endregion
        }
        #endregion

        #region cbb_origem

        string NomeAbrigoSelecionado = "";
        int CdAbrigoSelecionado;

        private void cbb_origem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstCdNovosAnimaisDestino.Count > 0)
            {
                lstCdNovosAnimaisDestino.Clear();
                lstCdRemoverAnimaisOrigem.Clear();
                lst_destino.Items.Clear();
                lst_origem.Items.Clear();
                cbb_destino.SelectedIndex = -1;

                lblAnimaisDestino.Text = "0";
                lblAnimaisOrigem.Text = "0";
            }

            if (cbb_origem.SelectedIndex > -1)
            {
                #region Ajusta a cbb_destino conforme o index selecionado
                if (NomeAbrigoSelecionado != "")
                {
                    cbb_destino.Items.Add(NomeAbrigoSelecionado);
                    lstCdAbrigoDestino.Add(CdAbrigoSelecionado);
                }

                for (int i = 0; i < lstCdAbrigoDestino.Count; i++)
                {
                    if (lstCdAbrigoOrigem[cbb_origem.SelectedIndex] == lstCdAbrigoDestino[i])
                    {
                        NomeAbrigoSelecionado = cbb_destino.Items[i].ToString();
                        CdAbrigoSelecionado = lstCdAbrigoDestino[i];

                        cbb_destino.Items.RemoveAt(i);
                        lstCdAbrigoDestino.RemoveAt(i);
                    }
                }
                #endregion

                #region Carrega os animais do abrigo(pessoa) selecionado para origem
                lst_origem.Items.Clear();
                lblAnimaisOrigem.Text = "0";

                DR = null;
                Conexao.IniciarStoredProcedure("ListaAnimalAbrigo");
                Conexao.AdicionarParametroInteiro(lstCdAbrigoOrigem[cbb_origem.SelectedIndex]);
                DR = Conexao.ChamarStoredProcedureCR();

                while (DR.Read())
                {
                    lst_origem.Items.Add(DR[0].ToString() + " - " + DR[1].ToString());
                    lstCdAnimaisOrigem.Add(DR.GetInt32(0));

                    lblAnimaisOrigem.Text = (int.Parse(lblAnimaisOrigem.Text) + 1).ToString();
                }

                DR.Close();
                DR.Dispose();
                Conexao.FecharConexao();
                #endregion

                #region Valida o valor de animais do cbb_detino

                if (cbb_destino.Text == "")
                {
                    lblAnimaisDestino.Text = "0";
                }

                #endregion
            }
        }
        #endregion

        #region cbb_destino
        private void cbb_destino_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstCdNovosAnimaisDestino.Count > 0)
            {
                lstCdNovosAnimaisDestino.Clear();
                lstCdRemoverAnimaisOrigem.Clear();
                lst_destino.Items.Clear();
                lst_origem.Items.Clear();
                cbb_destino.SelectedIndex = -1;
                cbb_origem.SelectedIndex = -1;

                lblAnimaisOrigem.Text = "0";
                lblAnimaisDestino.Text = "0";
            }

            if (cbb_destino.SelectedIndex > -1)
            {
                #region Carrega os animais do abrigo(pessoa) selecionada para destino
                lblAnimaisDestino.Text = "0";

                DR = null;
                Conexao.IniciarStoredProcedure("ListaAnimalAbrigo");
                Conexao.AdicionarParametroInteiro(lstCdAbrigoDestino[cbb_destino.SelectedIndex]);
                DR = Conexao.ChamarStoredProcedureCR();

                while (DR.Read())
                {
                    //lst_destino.Items.Add(DR[0].ToString() + " - " + DR[1].ToString());
                    //lstCdAnimaisDestino.Add(DR.GetInt32(0));

                    lblAnimaisDestino.Text = (int.Parse(lblAnimaisDestino.Text) + 1).ToString();
                }

                DR.Close();
                DR.Dispose();
                Conexao.FecharConexao();
                #endregion
            }
            else
            {
                lblAnimaisDestino.Text = "0";
            }
        }
        #endregion

        #region lst_origem
        private void lst_origem_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region Habilita/Desabilita btn_colocar animais
            if (lst_origem.SelectedIndex > -1 && cbb_origem.SelectedIndex > -1 && cbb_destino.SelectedIndex > -1)
            {
                btn_colocar.Enabled = true;
            }
            else
            {
                btn_colocar.Enabled = false;
            }
            #endregion
        }
        #endregion

        #region lst_destino
        private void lst_destino_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region Habilita/Desabilita btn_retirar animal
            if (lst_destino.SelectedIndex > -1 && cbb_origem.SelectedIndex > -1 && cbb_destino.SelectedIndex > -1)
            {
                btn_retirar.Enabled = true;
            }
            else
            {
                btn_retirar.Enabled = false;
            }
            #endregion
        }
        #endregion

        #region btn_colocar
        private void btn_colocar_Click(object sender, EventArgs e)
        {
            #region Transfere os animais
            lst_destino.Items.Add(lst_origem.SelectedItem.ToString());
            lstCdNovosAnimaisDestino.Add(lstCdAnimaisOrigem[lst_origem.SelectedIndex]);
            lstCdRemoverAnimaisOrigem.Add(lstCdAnimaisOrigem[lst_origem.SelectedIndex]);

            lstCdAnimaisOrigem.RemoveAt(lst_origem.SelectedIndex);
            lst_origem.Items.RemoveAt(lst_origem.SelectedIndex);
            #endregion

            #region Habilita/Desabilita o transferir
            btn_transferir.Enabled = true;
            #endregion

            lblAnimaisOrigem.Text = (int.Parse(lblAnimaisOrigem.Text) - 1).ToString();
            lblAnimaisDestino.Text = (int.Parse(lblAnimaisDestino.Text) + 1).ToString();
        }
        #endregion

        #region btn_retirar
        private void btn_retirar_Click(object sender, EventArgs e)
        {
            #region Volta com os animais
            lst_origem.Items.Add(lst_destino.SelectedItem.ToString());
            lstCdAbrigoOrigem.Add(lstCdNovosAnimaisDestino[lst_destino.SelectedIndex]);

            lstCdNovosAnimaisDestino.RemoveAt(lst_destino.SelectedIndex);
            lstCdRemoverAnimaisOrigem.RemoveAt(lst_destino.SelectedIndex);
            lst_destino.Items.RemoveAt(lst_destino.SelectedIndex);
            #endregion

            #region Habilita/Desabilita o btn_transferir
            if (lstCdNovosAnimaisDestino.Count > 0)
            {
                btn_transferir.Enabled = true;
            }
            else
            {
                btn_transferir.Enabled = false;
            }
            #endregion

            lblAnimaisOrigem.Text = (int.Parse(lblAnimaisOrigem.Text) + 1).ToString();
            lblAnimaisDestino.Text = (int.Parse(lblAnimaisDestino.Text) - 1).ToString();
        }
        #endregion

        #region btn_limpar
        private void btn_limpar_Click(object sender, EventArgs e)
        {
            cbb_destino.SelectedIndex = -1;
            lblAnimaisDestino.Text = "0";
            cbb_origem.SelectedIndex = -1;
            lblAnimaisOrigem.Text = "0";
            lst_destino.Items.Clear();
            lst_origem.Items.Clear();
            lstCdNovosAnimaisDestino.Clear();
            lstCdRemoverAnimaisOrigem.Clear();
        }
        #endregion

        private void btn_transferir_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Tem certeza que deseja alterar o abrigo desses animais?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question)) == System.Windows.Forms.DialogResult.Yes)
            {
                for (int i = 0; i < lstCdNovosAnimaisDestino.Count; i++)
                {
                    Conexao.IniciarStoredProcedure("AlteraAnimalAbrigo");
                    Conexao.AdicionarParametroInteiro(lstCdAbrigoOrigem[cbb_origem.SelectedIndex]);
                    Conexao.AdicionarParametroInteiro(lstCdAbrigoDestino[cbb_destino.SelectedIndex]);
                    Conexao.AdicionarParametroInteiro(lstCdNovosAnimaisDestino[i]);
                    Conexao.ChamarStoredProcedureSR();
                }

                MessageBox.Show("Transferência realizada com sucessso!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

                cbb_destino.SelectedIndex = -1;
                cbb_origem.SelectedIndex = -1;

                lst_origem.Items.Clear();
                lst_destino.Items.Clear();

                lblAnimaisDestino.Text = "0";
                lblAnimaisOrigem.Text = "0";

                btn_colocar.Enabled = false;
                btn_retirar.Enabled = false;
                btn_transferir.Enabled = false;
            }
        }
    }
}
