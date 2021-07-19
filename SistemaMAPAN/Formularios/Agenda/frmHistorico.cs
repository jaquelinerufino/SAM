using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Odbc;

namespace SistemaMAPAN.Forms
{
    public partial class frmHistorico : Form
    {
        public frmHistorico()
        {
            InitializeComponent();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }

        clsConexao.clsConexao Conexao = new clsConexao.clsConexao("mapan", "localhost", "root", "root");
        OdbcDataReader DR;

        frmAgenda Lista;
        int index;

        private void frmHistorico_Load(object sender, EventArgs e)
        {
            Lista = (frmAgenda)this.Owner;

            DR = null;

            if (Lista.rdbEvento.Checked)
            {

                if (dgvListas.Columns.Count != 9)
                {
                    dgvListas.Columns[6].Visible = true;
                    dgvListas.Columns[7].Visible = true;
                }

                dgvListas.Columns[0].HeaderText = "Data de Início";
                dgvListas.Columns[1].HeaderText = "Hora de Início";
                dgvListas.Columns[2].HeaderText = "Data de Término";
                dgvListas.Columns[3].HeaderText = "Hora de Término";
                dgvListas.Columns[4].HeaderText = "Titulo do Evento";
                dgvListas.Columns[5].HeaderText = "Descrição";
                dgvListas.Columns[5].Width = 250;
                //dgvListas.Columns[6].HeaderText = "Numero do Evento"; DR[5].ToString()
                dgvListas.Columns[6].HeaderText = "Tipo de Evento";
                dgvListas.Columns[6].Width = 100;
                dgvListas.Columns[7].HeaderText = "Valor Arrecadado";

                lblEventos.Text = "Lista de Eventos";
                lblEventoRelacionado.Text = "Animais participantes deste evento selecionado:";

                #region Lista de Eventos
                DR = null;

                Conexao.IniciarStoredProcedure("ListaEvento");
                DR = Conexao.ChamarStoredProcedureCR();

                while (DR.Read())
                {
                    if (DR[7].ToString() != "")
                    {
                        dgvListas.Rows.Add(DR.GetDate(0).ToString("dd/MM/yyyy"), DR[1].ToString(), DR.GetDate(2).ToString("dd/MM/yyyy"), DR[3].ToString(), DR[4].ToString(), DR[6].ToString(), DR[9].ToString(), DR[7].ToString());
                    }
                }

                DR.Close();
                DR.Dispose();
                Conexao.FecharConexao();

                #endregion
            }
            else
            {
                dgvListas.Columns[0].HeaderText = "Data da Vistoria";
                dgvListas.Columns[1].HeaderText = "Hora da Vistoria";
                dgvListas.Columns[2].HeaderText = "Nome da Pessoa";
                dgvListas.Columns[3].HeaderText = "Data de Vencimento";
                dgvListas.Columns[4].HeaderText = "Habilitado";
                dgvListas.Columns[4].Width = 50;
                dgvListas.Columns[5].HeaderText = "Descrição da Vistoria";
                dgvListas.Columns[5].Width += 150;
                dgvListas.Columns[6].HeaderText = "Situação Pessoa";
                dgvListas.Columns[6].Width += 50;
                dgvListas.Columns[7].Visible = false;

                lblEventos.Text = "Lista de Vistorias";
                lblEventoRelacionado.Text = "Animais em posse da pessoa selecionada:";

                #region Lista Vistoria

                DR = null;
                //int index = 0;

                Conexao.IniciarStoredProcedure("ListaVistoria");
                DR = Conexao.ChamarStoredProcedureCR();

                while (DR.Read())
                {
                    if (DR[7].ToString() == "1")
                    {
                        if (DR[4].ToString() == "1")
                        {
                            dgvListas.Rows.Add(DR.GetDate(0).ToString("dd/MM/yyyy"), DR[2].ToString(), DR[1].ToString(), DR.GetDate(6).ToString("dd/MM/yyyy"), "Sim", DR[5].ToString(), "Abrigo Ativo");
                        }
                        else if(DR[4].ToString() == "0")
                        {
                            dgvListas.Rows.Add(DR.GetDate(0).Date.ToString("dd/MM/yyyy"), DR[2].ToString(), DR[1].ToString(), DR.GetDate(6).ToString("dd/MM/yyyy"), "Não", DR[5].ToString(), "Abrigo Ativo");
                        }
                    }
                    else if (DR[7].ToString() == "0")
                    {
                        if (DR[4].ToString() == "1")
                        {
                            dgvListas.Rows.Add(DR.GetDate(0).Date.ToString("dd/MM/yyyy"), DR[2].ToString(), DR[1].ToString(), DR.GetDate(6).ToString("dd/MM/yyyy"), "Sim", DR[5].ToString(), "Abrigo Inativo");
                        }
                        else if (DR[4].ToString() == "0")
                        {
                            dgvListas.Rows.Add(DR.GetDate(0).Date.ToString("dd/MM/yyyy"), DR[2].ToString(), DR[1].ToString(), DR.GetDate(6).ToString("dd/MM/yyyy"), "Não", DR[5].ToString(), "Abrigo Inativo");
                        }
                    }

                    if (DR[8].ToString() == "1")
                    {
                        if (DR[4].ToString() == "1")
                        {
                            dgvListas.Rows.Add(DR.GetDate(0).Date.ToString("dd/MM/yyyy"), DR[2].ToString(), DR[1].ToString(), DR.GetDate(6).ToString("dd/MM/yyyy"), "Sim", DR[5].ToString(), "Adotante Desbloqueado");
                        }
                        else if(DR[4].ToString() == "0")
                        {
                            dgvListas.Rows.Add(DR.GetDate(0).Date.ToString("dd/MM/yyyy"), DR[2].ToString(), DR[1].ToString(), DR.GetDate(6).ToString("dd/MM/yyyy"), "Não", DR[5].ToString(), "Adotante Desbloqueado");
                        }
                    }
                    else if (DR[8].ToString() == "0")
                    {
                        if (DR[4].ToString() == "1")
                        {
                            dgvListas.Rows.Add(DR.GetDate(0).Date.ToString("dd/MM/yyyy"), DR[2].ToString(), DR[1].ToString(), DR.GetDate(6).ToString("dd/MM/yyyy"), "Sim", DR[5].ToString(), "Adotante Bloqueado");
                        }
                        else if (DR[4].ToString() == "0")
                        {
                            dgvListas.Rows.Add(DR.GetDate(0).Date.ToString("dd/MM/yyyy"), DR[2].ToString(), DR[1].ToString(), DR.GetDate(6).ToString("dd/MM/yyyy"), "Não", DR[5].ToString(), "Adotante Bloqueado");
                        }
                    }
                }

                DR.Close();
                DR.Dispose();
                Conexao.FecharConexao();

                #endregion
            }
        }
    }
}
