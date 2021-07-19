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
    public partial class frmNovo : Form
    {
        #region Inicialização de Componentes
        public frmNovo()
        {
            InitializeComponent();
        }
        #endregion        

        #region Novo Doador
        private void btnNovoDoador_Click(object sender, EventArgs e)
        {
            frmCadastroDoador Novo = new frmCadastroDoador();

            Novo.ShowDialog(this);
            this.Close();
        }
        #endregion

        #region Nova Doação
        private void btnNovaDoacao_Click(object sender, EventArgs e)
        {
            frmDoacao Nova = new frmDoacao();

            Nova.ShowDialog(this);
            this.Close();
        }
        #endregion

        private void frmNovo_Load(object sender, EventArgs e)
        {

        }
        
    }
}
