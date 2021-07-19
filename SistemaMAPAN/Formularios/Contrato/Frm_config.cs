using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SistemaMAPAN
{
    public partial class Frm_config : Form
    {
        #region Iniciatização de componentes

        public Frm_config()
        {
            InitializeComponent();
        }

        #endregion

        #region Variaveis publicas

        public bool mudouCaminho { set; get; }
        public string caminho { set; get; }

        #endregion

        #region Form Load

        private void Frm_config_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.CenterToScreen();
            caminho = "";
            txt_escolha.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        #endregion

        #region Botão "Salvar"

        private void btn_salvar_Click(object sender, EventArgs e)
        {
            if (FBD_escolha_diretorio.ShowDialog() == DialogResult.OK)
            {
                txt_escolha.Text = FBD_escolha_diretorio.SelectedPath;
            }
        }

        #endregion

        #region Botão "Limpar"

        private void btn_limpar_Click(object sender, EventArgs e)
        {
            txt_escolha.Clear();
        }

        #endregion

        #region Botão "Salvar" 2
        private void btn_salvar_Click_1(object sender, EventArgs e)
        {
            mudouCaminho = true;
            caminho = txt_escolha.Text;
            this.Close();
        }

        #endregion
        
    }
}
