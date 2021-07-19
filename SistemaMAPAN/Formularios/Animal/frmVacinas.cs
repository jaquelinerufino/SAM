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
    public partial class frmVacinas : Form
    {
        public frmVacinas()
        {
            InitializeComponent();
        }

        #region Conexao do banco
        clsConexao.clsConexao conexao = new clsConexao.clsConexao("MAPAN", "localhost", "root", "root");
        OdbcDataReader dados;
        #endregion

        #region Variaveis

        List<int> lstCdVacina = new List<int>();

        List<int> lstCdVacinaExc = new List<int>();

        List<int> lstCdVacinaAdd = new List<int>();
        List<string> lstDataVacina = new List<string>();
        List<string> lstDataVencimento = new List<string>();

        List<string> lstNomeVacina = new List<string>();

        FrmEditarAnimal Editar;
        public int Caminho = 0;

        #endregion

        #region FormLoad - Carrega tudo

        private void frmVacinas_Load(object sender, EventArgs e)
        {
            this.dgv_vacina.Rows.Clear();

            if (Caminho == 0)
            {
                Editar = (FrmEditarAnimal)this.Owner;
                dados = null;
                conexao.IniciarStoredProcedure("Situacao_vacinado");
                conexao.AdicionarParametroInteiro(int.Parse(Editar.txtCodAnimal.Text));
                dados = conexao.ChamarStoredProcedureCR();

                while (dados.Read())
                {
                    dgv_vacina.Rows.Add(dados.GetString(0), DateTime.Parse(dados.GetString(1)).ToString("dd/MM/yyyy"));

                    lstNomeVacina.Add(dados[0].ToString());
                    lstCdVacinaAdd.Add(dados.GetInt32(2));
                    lstDataVacina.Add(dados.GetString(1));
                    lstDataVencimento.Add(dados.GetString(3));
                }

                dados.Close();
                dados.Dispose();
                conexao.FecharConexao();
            }
            

            dados = null;
            conexao.IniciarStoredProcedure("ListaVacinas");

            if (Caminho == 0)
            {
                conexao.AdicionarParametroInteiro(Editar.CdEspecie);
            }
            else
            {
                conexao.AdicionarParametroInteiro(Caminho);
            }

            dados = conexao.ChamarStoredProcedureCR();

            while(dados.Read())
            {
                cmbVacina.Items.Add(dados[1].ToString());
                lstCdVacina.Add(dados.GetInt32(0));
            }
            dados.Close();
            dados.Dispose();
            conexao.FecharConexao();
        }

        #endregion

        #region Salva as alterações

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (dgv_vacina.Rows.Count == 0)
            {
                MessageBox.Show("Atenção: Adicione ao menos uma vacina para se salva-las ao animal.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Caminho == 0)
            {
                Editar = (FrmEditarAnimal)this.Owner;
                Editar.dgv_vacina.Rows.Clear();

                for (int i = 0; i < lstCdVacinaAdd.Count; i++)
                {
                    Editar.lstCdVacinaAdd.Add(lstCdVacinaAdd[i]);
                    Editar.lstDataVacina.Add(lstDataVacina[i]);
                    Editar.lstDataVencimento.Add(lstDataVencimento[i]);

                    Editar.dgv_vacina.Rows.Add(lstNomeVacina[i], lstDataVacina[i]);
                }

                for (int i = 0; i < lstCdVacinaExc.Count; i++)
                {
                    Editar.lstCdVacinaExc.Add(lstCdVacinaExc[i]);
                }
            }
            else
            {
                frmCadAnimal Cad = (frmCadAnimal)this.Owner;
                Cad.lstCdVacina.Clear();
                Cad.dtVacinacao.Clear();
                Cad.dtVencVacinacao.Clear();
                Cad.NmVacina.Clear();

                for (int i = 0; i < lstCdVacinaAdd.Count; i++)
                {
                    Cad.lstCdVacina.Add(lstCdVacinaAdd[i]);
                    Cad.dtVacinacao.Add(lstDataVacina[i]);
                    Cad.dtVencVacinacao.Add(lstDataVencimento[i]);
                    Cad.NmVacina.Add(dgv_vacina.Rows[i].Cells[0].Value.ToString());
                }
            }

            Close();
        }

        #endregion

        #region Adiciona uma vacina ao animal

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            int Dia, Mes, Ano;
            SistemaMAPAN.clsEditarAnimal Data = new SistemaMAPAN.clsEditarAnimal();

            if (cmbVacina.SelectedIndex == -1)
            {
                MessageBox.Show("Atenção: Escolha uma vacina para poder adiciona-la!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (mtxtDtVacina.MaskCompleted == false)
            {
                MessageBox.Show("Atenção: Digite a data de vacinação do animal!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Dia = int.Parse(mtxtDtVacina.Text.Substring(0, 2));
            Mes = int.Parse(mtxtDtVacina.Text.Substring(3, 2));
            Ano = int.Parse(mtxtDtVacina.Text.Substring(6, 4));

            if (Data.ValidaData(Dia, Mes, Ano) == false)
            {
                MessageBox.Show("Atenção: Data de vacina inválida!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (mtxtDtVencimento.MaskCompleted == false)
            {
                MessageBox.Show("Atenção: Digite uma data de vencimento da vacina!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Dia = int.Parse(mtxtDtVencimento.Text.Substring(0, 2));
            Mes = int.Parse(mtxtDtVencimento.Text.Substring(3, 2));
            Ano = int.Parse(mtxtDtVencimento.Text.Substring(6, 4));

            if (Data.ValidaData(Dia, Mes, Ano) == false)
            {
                MessageBox.Show("Atenção: Data de vencimento inválida!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            for (int i = 0; i < lstCdVacinaAdd.Count; i++)
            {
                if (lstCdVacina[cmbVacina.SelectedIndex] == lstCdVacinaAdd[i])
                {
                    MessageBox.Show("Atenção: Está vacina já está cadastrada!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            lstCdVacinaAdd.Add(lstCdVacina[cmbVacina.SelectedIndex]);
            lstDataVacina.Add(mtxtDtVacina.Text);
            lstDataVencimento.Add(mtxtDtVencimento.Text);
            lstNomeVacina.Add(cmbVacina.SelectedItem.ToString());

            dgv_vacina.Rows.Add(cmbVacina.SelectedItem.ToString(), mtxtDtVacina.Text);

            cmbVacina.SelectedIndex = -1;
            mtxtDtVacina.Clear();
            mtxtDtVencimento.Clear();
        }
        #endregion

        #region Exclui uma vacina

        private void btnExcluir_Click_1(object sender, EventArgs e)
        {
            if (dgv_vacina.CurrentRow.Index == -1)
            {
                MessageBox.Show("Atenção: Selecione uma vacina para retira-la!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            lstCdVacinaExc.Add(lstCdVacinaAdd[dgv_vacina.CurrentRow.Index]);

            lstCdVacinaAdd.RemoveAt(dgv_vacina.CurrentRow.Index);
            lstDataVacina.RemoveAt(dgv_vacina.CurrentRow.Index);
            lstDataVencimento.RemoveAt(dgv_vacina.CurrentRow.Index);
            lstNomeVacina.RemoveAt(dgv_vacina.CurrentRow.Index);

            dgv_vacina.Rows.RemoveAt(dgv_vacina.CurrentRow.Index);
        }

        #endregion

        #region Cancela as alterações

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (lstCdVacinaAdd.Count == dgv_vacina.Rows.Count && lstCdVacinaExc.Count == 0)
            {
                Close();
            }
            else
            {
                if (MessageBox.Show("Atenção: Hávera perda de dados caso seja cancelada a ação! Tem certeza disto?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    Close();
                }
            }
        }

        #endregion

        private void btnEditar_Click(object sender, EventArgs e)
        {
            SistemaMAPAN.frmNovaVacina Vacina = new SistemaMAPAN.frmNovaVacina();

            Vacina.ShowDialog();
        }
    }
}
