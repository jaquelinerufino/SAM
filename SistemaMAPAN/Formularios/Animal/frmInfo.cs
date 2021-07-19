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
    public partial class frmInfo : Form
    {
        #region Inicialização de Componentes
        public frmInfo()
        {
            InitializeComponent();
        }
        #endregion        

        #region Form: Load
        private void frmInfo_Load(object sender, EventArgs e)
        {
            frmCadAnimal FrmPai = (frmCadAnimal)this.Owner;

            lblQtd.Text = frmCadAnimal.QtdAnimais.ToString();
            lblQtdMax.Text = frmCadAnimal.QtdMax.ToString();
            lblEndereco.Text = frmCadAnimal.Endereco;
        }
        #endregion

        #region Botão: Ok
        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion        
    }
}
