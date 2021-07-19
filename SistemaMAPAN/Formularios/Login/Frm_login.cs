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
    public partial class Frm_login : Form
    {
        public Frm_login()
        {
            InitializeComponent();
        }

        #region Entrando na conexão

        clsConexao.clsConexao conexao = new clsConexao.clsConexao("Mapan", "localhost", "root", "root");
        OdbcDataReader dados;
        string apelido, TipoUsuario;
        frmPrincipal Prin;
        public Boolean Logado = false;

        #endregion

        #region Load

        private void Frm_login_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.CenterToScreen();
        }

        #endregion

        #region Botão Logar

        private void btn_logar_Click_1(object sender, EventArgs e)
        {
            Prin = (frmPrincipal)this.Owner;

            #region Variaveis

            string Login, senha;

            bool leu;

            Login = txt_login.Text;
            senha = txt_senha.Text;

            #endregion

            #region Validar entrada

            if (txt_login.Text == "")
            {
                Mensagem("O usuario não existe");
                return;
            }
            else
            {
                dados = null;
                conexao.IniciarStoredProcedure("Verifica_login");
                conexao.AdicionarParametroTexto(Login);
                dados = conexao.ChamarStoredProcedureCR();
                leu = false;
                while(dados.Read())
                {
                    leu = true;                    
                }
                if(leu == false)
                {
                    Mensagem("Usuario e/ou senha incorreta");
                }

                dados.Close();
                dados.Dispose();
                conexao.FecharConexao();

            }

            if (txt_senha.Text == "")
            {
                Mensagem("O usuario precisa de uma senha");
                return;
            }
            else
            {
                dados = null;
                conexao.IniciarStoredProcedure("Verifica_senha");
                conexao.AdicionarParametroTexto(Login);
                dados = conexao.ChamarStoredProcedureCR();
                while (dados.Read())
                {
                    if (dados.GetString(0) != senha)
                    {
                        Mensagem("O usuario precisa de uma senha");
                        return;
                    }
                }
                dados.Close();
                dados.Dispose();
                conexao.FecharConexao();

            }

            #endregion


            //enviar apelido para o forme principal e mostrar isso

            dados = null;
            conexao.IniciarStoredProcedure("apelido");
            conexao.AdicionarParametroTexto(Login);
            dados = conexao.ChamarStoredProcedureCR();
            while(dados.Read())
            {
                apelido = dados.GetString(0);
                TipoUsuario = dados[1].ToString();
            }

            Mensagem("Seja Bem-Vindo " + apelido);

            Prin.lblUsuario.Text = "Usuario: " + apelido + "\nTipo: " + TipoUsuario;
            Prin.menuStrip1.Enabled = true;
            Prin.TipoUsuario = TipoUsuario;

            Logado = true;
            Close();
          
        }

        #endregion

        #region Metodo Mensagem

        private static void Mensagem(string msg)
        {
            MessageBox.Show(msg,"Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        #endregion

        private void Frm_login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Logado == false)
            {
                MessageBox.Show("Atenção: Para utilização do sistema é necessário o login!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (MessageBox.Show("Deseja fechar o programa?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
