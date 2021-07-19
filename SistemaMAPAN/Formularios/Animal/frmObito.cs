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
    public partial class frmObito : Form
    {
        public frmObito()
        {
            InitializeComponent();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (mskDataObito.MaskCompleted == false)
            {
                MessageBox.Show("Atenção: Preencha a data de óbito!",this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtDescricaoObito.Text == "")
            {

                FrmEditarAnimal Editar = (FrmEditarAnimal)this.Owner;

                Editar.DtObito = DateTime.Parse(mskDataObito.Text).ToString("yyyy-MM-dd");
                Editar.DsObito = "";
            }
            else
            {
                FrmEditarAnimal Editar = (FrmEditarAnimal)this.Owner;

                Editar.DtObito = DateTime.Parse(mskDataObito.Text).ToString("yyyy-MM-dd");
                Editar.DsObito = txtDescricaoObito.Text;
            }

            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
