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
    public partial class frmNovoEvento : Form
    {
        public frmNovoEvento()
        {
            InitializeComponent();
        }

        List<int> CodigosTipoEvento = new List<int>();

        public List<int> lstCodigosAnimais = new List<int>();
        public List<int> lstNovosAnimais = new List<int>();
        public List<int> lstExcluirAnimais = new List<int>();
        string HoraI, HoraF;

        public int caminho = 0;

        clsConexao.clsConexao Conexao = new clsConexao.clsConexao("mapan", "localhost", "root", "root");
        OdbcDataReader DR;

        clsAgenda agenda = new clsAgenda();

        frmAgenda evento;

        #region btnCancelar - Cancela o evento
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (btnCancelar.Text == "&Cancelar")
            {
                if (mtxtInicio.ReadOnly == false)
                {
                    if (mtxtInicio.MaskCompleted == false && mtxtFinal.MaskCompleted == false && txtNome.Text == "" && mtxtHoraInicial.MaskCompleted == false && mtxtHoraFinal.MaskCompleted == false && txtDescricao.Text == "")
                    {
                        if (MessageBox.Show("Você deseja cancelar está edição de evento?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            Close();
                            lblComplemento.Visible = false;
                            mtxtFinal.Clear();
                        }
                    }
                    else
                    {
                        if (MessageBox.Show("Atenção: Hávera perda de dados caso cancelada está operação! Tem certeza disto?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            Close();
                            lblComplemento.Visible = false;
                            mtxtFinal.Clear();
                        }
                    }
                }
                else
                {
                    if (txtNome.Text == "" && txtDescricao.Text == "" && txtValor.Text == "")
                    {
                        if (MessageBox.Show("Você deseja cancelar está edição de evento?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            Close();
                            lblComplemento.Visible = false;
                            mtxtFinal.Clear();
                        }
                    }
                    else
                    {
                        if (MessageBox.Show("Atenção: Hávera perda de dados caso cancelada está operação! Tem certeza disto?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            Close();
                            lblComplemento.Visible = false;
                            mtxtFinal.Clear();
                        }
                    }
                }

            }
            else
            {
                //if (mtxtInicio.MaskCompleted == false && mtxtFinal.MaskCompleted == false && txtNome.Text == "" && mtxtHoraInicial.MaskCompleted == false && mtxtHoraFinal.MaskCompleted == false && txtDescricao.Text == "")
                //{
                //    if (MessageBox.Show("Você deseja cancelar este evento?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                //    {
                //        Close();
                //        lblComplemento.Visible = false;
                //        mtxtFinal.Clear();
                //    }
                //}
                //else
                //{
                //    if (MessageBox.Show("Atenção: Hávera perda de dados caso seja cancelada está operação! Tem certeza disto?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                //    {
                //        Close();
                //        lblComplemento.Visible = false;
                //        mtxtFinal.Clear();
                //    }
                //}

                if (MessageBox.Show("Você deseja cancelar esta edição evento?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    Close();
                    lblComplemento.Visible = false;
                    mtxtFinal.Clear();
                }
            }
        }
        #endregion

        #region formLoad - Carrega tipo de eventos para combobox - Carrega evento para edição
        private void frmNovoEvento_Load(object sender, EventArgs e)
        {
            #region Lista de Tipo de Eventos
            if (caminho == 1)
            {
                evento = (frmAgenda)this.Owner;
            }

            DR = null;

            Conexao.IniciarStoredProcedure("TipoEvento");
            DR = Conexao.ChamarStoredProcedureCR();

            while (DR.Read())
            {
                cmbTipoEvento.Items.Add(DR.GetString(0));
                CodigosTipoEvento.Add(DR.GetInt32(1));
            }
            #endregion

            #region Carrega evento para edição
            if (btnSalvar.Text != "&Salvar")
            {
                mtxtInicio.ReadOnly = true;
                mtxtFinal.ReadOnly = true;

                DR = null;

                Conexao.IniciarStoredProcedure("ListaEvento");
                DR = Conexao.ChamarStoredProcedureCR();

                while (DR.Read())
                {
                    string DataIn = DateTime.Parse(DR[0].ToString()).Year + "-" + DateTime.Parse(DR[0].ToString()).Month + "-" + DateTime.Parse(DR[0].ToString()).Day;
                    string DataFin = DateTime.Parse(DR[2].ToString()).Year + "-" + DateTime.Parse(DR[2].ToString()).Month + "-" + DateTime.Parse(DR[2].ToString()).Day;

                    if (DateTime.Parse(DataIn).Date == DateTime.Parse(evento.lstData[evento.dgvListas.CurrentRow.Index]).Date || DR[1].ToString() == (evento.lstHora[evento.dgvListas.CurrentRow.Index] + ":00"))
                    {
                        if (DR[0].ToString() == DR[2].ToString())
                        {
                            mtxtInicio.Text = DR.GetDate(0).ToString("dd/MM/yyyy");
                            mtxtFinal.Text = DR.GetDate(2).ToString("dd/MM/yyyy");
                            DataInOr = DateTime.Parse(mtxtInicio.Text + " " + DR[1].ToString());
                            DataFnOr = DateTime.Parse(mtxtFinal.Text + " " + DR[3].ToString());
                            lblComplemento.Visible = true;
                        }
                        else
                        {
                            mtxtInicio.Text = DR.GetDate(0).Date.ToString("dd/MM/yyyy");
                            mtxtFinal.Text = DR.GetDate(2).Date.ToString("dd/MM/yyyy");
                            DataInOr = DateTime.Parse(mtxtInicio.Text + " " + DR[1].ToString());
                            DataFnOr = DateTime.Parse(mtxtFinal.Text + " " + DR[3].ToString());
                            lblComplemento.Visible = true;
                        }

                        mtxtHoraInicial.Text = DR[1].ToString();
                        HoraI = mtxtHoraInicial.Text;

                        mtxtHoraFinal.Text = DR[3].ToString();
                        HoraF = mtxtHoraFinal.Text;

                        TimeSpan HoraEve = TimeSpan.Parse(mtxtHoraInicial.Text.Substring(0, 2) + ":" + mtxtHoraInicial.Text.Substring(2, 2));
                        TimeSpan HoraNow = TimeSpan.Parse(System.DateTime.Now.Hour + ":" + System.DateTime.Now.Minute);

                        if (DateTime.Parse(DataIn) < System.DateTime.Today || DateTime.Parse(DataIn) == System.DateTime.Today && HoraNow > HoraEve)
                        {
                            mtxtInicio.ReadOnly = true;
                            mtxtHoraInicial.ReadOnly = true;
                            mtxtHoraFinal.ReadOnly = true;
                            grbAnimais.Enabled = false;
                        }
                        else
                        {
                            mtxtInicio.ReadOnly = true;
                            mtxtHoraInicial.ReadOnly = true;
                            mtxtHoraFinal.ReadOnly = false;
                            grbAnimais.Enabled = true;
                        }

                        if (DateTime.Parse(DataFin) < System.DateTime.Today || DateTime.Parse(DataFin) == System.DateTime.Today && HoraNow > HoraEve)
                        {
                            mtxtFinal.ReadOnly = true;
                        }
                        else
                        {
                            mtxtFinal.ReadOnly = false;
                        }

                        txtNome.Text = DR[4].ToString();
                        txtDescricao.Text = DR[6].ToString();

                        lblNumero.Text = DR[5].ToString();

                        string Horafn = (System.DateTime.Now.Hour).ToString() + ":" + (System.DateTime.Now.Minute).ToString();

                        if (DateTime.Parse(DataFin) < System.DateTime.Today || DateTime.Parse(DataFin) == System.DateTime.Today && TimeSpan.Parse(DR[3].ToString()) < TimeSpan.Parse(Horafn))
                        {
                            lblValorArrecadado.Visible = true;
                            lblRS.Visible = true;
                            txtValor.Visible = true;

                            txtValor.Text = DR[7].ToString();
                        }

                        if (evento.lstCdAnimais.Count > 0)
                        {
                            //chkAnimais.Checked = true;

                            for (int i = 0; i < evento.lstCdAnimais.Count; i++)
                            {
                                lstAnimais.Items.Add(evento.dgvAnimais.Rows[i].Cells[1].Value);
                                lstCodigosAnimais.Add(evento.lstCdAnimais[i]);
                            }
                        }

                        cmbTipoEvento.SelectedIndex = (DR.GetInt32(8)-1);
                        cmbTipoEvento.DropDownStyle = ComboBoxStyle.Simple;
                        return;
                    }
                }
            }
            else
            {
                cmbTipoEvento.Enabled = true;
                btnAdd.Enabled = true;
                btnExc.Enabled = true;
                mtxtHoraInicial.ReadOnly = false;
                mtxtHoraFinal.ReadOnly = false;
                mtxtInicio.ReadOnly = false;
            }
            #endregion

            DR.Close();
            DR.Dispose();
            Conexao.FecharConexao();
        }
        #endregion

        #region Salva ou Edita um evento
        DateTime DataI, DataF;
        DateTime DataInOr ,DataFnOr;

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (mtxtHoraInicial.MaskCompleted == true && mtxtHoraFinal.MaskCompleted == true && cmbTipoEvento.Text != "" && txtNome.Text != "")
            {
                string horarioI = mtxtHoraInicial.Text;
                string horarioF = mtxtHoraFinal.Text;

                #region Validações antes da execução

                if (int.Parse(horarioI.Substring(0, 2)) > 23 || int.Parse(horarioI.Substring(2, 2)) > 59 || int.Parse(horarioF.Substring(0, 2)) > 23 || int.Parse(horarioF.Substring(2, 2)) > 59)
                {
                    MessageBox.Show("Atenção: Digite a hora corretamente! (00:00 - 23:59)", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (horarioI == horarioF)
                {
                    MessageBox.Show("Atenção: A hora de inicio não pode ser igual a de término!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (int.Parse(horarioI) > int.Parse(horarioF))
                {
                    MessageBox.Show("Atenção: A hora de inicio não pode ser maior que á de termino!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataI = DateTime.Parse(mtxtInicio.Text + " " + horarioI.Substring(0, 2) + ":" + horarioI.Substring(2, 2));

                if (mtxtFinal.Text == "  /  /")
                {
                    DataF = DateTime.Parse(mtxtInicio.Text + " " + horarioF.Substring(0, 2) + ":" + horarioF.Substring(2, 2));
                }
                else
                {
                    DataF = DateTime.Parse(mtxtFinal.Text + " " + horarioF.Substring(0, 2) + ":" + horarioF.Substring(2, 2));
                }

                if (DataValida == false && mtxtInicio.ReadOnly == false)
                {
                    MessageBox.Show("Atenção: Digite uma data válida!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (DataTermVal == false && mtxtFinal.ReadOnly == false)
                {
                    MessageBox.Show("Atenção: Digite uma data de término válida!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (DataI.Date == System.DateTime.Today.Date)
                {
                    horarioI = horarioI.Substring(0, 2) + ":" + horarioI.Substring(2, 2) + ":00";
                    horarioF = horarioF.Substring(0, 2) + ":" + horarioF.Substring(2, 2) + ":00";

                    if (TimeSpan.Parse(horarioI) < System.DateTime.Now.TimeOfDay && mtxtHoraInicial.ReadOnly == false)
                    {
                        MessageBox.Show("Atenção: Horário de inicio já passou!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (TimeSpan.Parse(horarioF) < System.DateTime.Now.TimeOfDay && mtxtHoraFinal.ReadOnly == false)
                    {
                        MessageBox.Show("Atenção: Horário de término já passou!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                #endregion

                if (btnSalvar.Text == "&Salvar")
                {
                    #region Salvar Novo Evento

                    if (cmbTipoEvento.SelectedIndex == -1)
                    {
                        if (MessageBox.Show("Este tipo de evento não está cadastrado, você deseja cadastra-lo?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            MessageBox.Show("Tipo de Evento salvo com sucesso!");

                            Conexao.IniciarStoredProcedure("AddTipoEvento");
                            Conexao.AdicionarParametroTexto(cmbTipoEvento.Text);
                            Conexao.ChamarStoredProcedureSR();
                            cmbTipoEvento.Items.Add(cmbTipoEvento.Text);
                            Conexao.FecharConexao();
                        }
                        else
                        {
                            return;
                        }
                    }


                    string Acao = agenda.DataExiste(DataI, DataF);
                    if (Acao != "")
                    {
                        MessageBox.Show(Acao);
                        return;
                    }
                    else
                    {
                        Conexao.IniciarStoredProcedure("AddEvento");
                        Conexao.AdicionarParametroTexto(DataI.ToString("yyyy-MM-dd"));
                        Conexao.AdicionarParametroTexto(horarioI.Replace(":", "").Substring(0, 2) + ":" + horarioI.Replace(":", "").Substring(2, 2));
                        Conexao.AdicionarParametroTexto(DataF.ToString("yyyy-MM-dd"));
                        Conexao.AdicionarParametroTexto(horarioF.Replace(":", "").Substring(0, 2) + ":" + horarioF.Replace(":", "").Substring(2, 2));
                        Conexao.AdicionarParametroTexto(txtNome.Text);
                        Conexao.AdicionarParametroInteiro(int.Parse(lblNumero.Text));
                        Conexao.AdicionarParametroTexto(txtDescricao.Text);
                        Conexao.AdicionarParametroInteiro(CodigosTipoEvento[cmbTipoEvento.SelectedIndex]);
                        Conexao.ChamarStoredProcedureSR();

                        if (lstExcluirAnimais.Count > 0)
                        {
                            for (int i = 0; i < lstExcluirAnimais.Count; i++)
                            {
                                Conexao.IniciarStoredProcedure("ExcluirAnimal");
                                Conexao.AdicionarParametroInteiro(lstExcluirAnimais[i]);
                                Conexao.AdicionarParametroTexto(DataI.ToString("yyyy-MM-dd"));
                                Conexao.AdicionarParametroTexto(horarioI.Replace(":", "").Substring(0, 2) + ":" + horarioI.Replace(":", "").Substring(2, 2));
                                Conexao.ChamarStoredProcedureSR();
                            }
                        }

                        if (lstNovosAnimais.Count > 0)
                        {
                            for (int i = 0; i < lstNovosAnimais.Count; i++)
                            {
                                Conexao.IniciarStoredProcedure("AddRelacaoAnimal");
                                Conexao.AdicionarParametroInteiro(lstNovosAnimais[i]);
                                Conexao.AdicionarParametroTexto(DataI.ToString("yyyy-MM-dd"));
                                Conexao.AdicionarParametroTexto(horarioI.Replace(":", "").Substring(0, 2) + ":" + horarioI.Replace(":", "").Substring(2, 2));
                                Conexao.ChamarStoredProcedureSR();
                            }
                        }

                        MessageBox.Show("Evento cadastrado com sucesso!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Close();
                    }
                    #endregion
                }
                else
                {
                    #region Salva Edição do evento

                    if (txtValor.Text == "" && DataI.Date < evento.monthCalendar1.TodayDate.Date)
                    {
                        MessageBox.Show("Atenção: Para salvar a edição é preciso que o campo de 'valor arrecadado' seja preenchido!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        for (int i = 0; i < txtValor.Text.Length; i++)
                        {
                            if (txtValor.Text.Substring(i, 1) == "," || txtValor.Text == "")
                            {
                                try
                                {
                                    txtValor.Text.Substring(i, 2);
                                }
                                catch
                                {
                                    txtValor.Text += "00";
                                }

                                try
                                {
                                    txtValor.Text.Substring(i, 3);
                                }
                                catch
                                {
                                    txtValor.Text += "0";
                                }

                                try
                                {
                                    txtValor.Text.Substring(i, 4);
                                    MessageBox.Show("Atenção: O formato do valor arrecadado está errado!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                catch
                                {
                                }
                            }
                        }
                    }

                    if (agenda.DataExisteEdicao(DataI, DataF, DataInOr, DataFnOr) == "")
                    {
                        Conexao.IniciarStoredProcedure("EditaEvento");
                        Conexao.AdicionarParametroTexto(DataInOr.Date.ToString("yyyy-MM-dd"));
                        Conexao.AdicionarParametroTexto(DataInOr.Hour.ToString() + ":" + DataInOr.Minute.ToString());
                        Conexao.AdicionarParametroTexto(DataI.ToString("yyyy-MM-dd"));
                        Conexao.AdicionarParametroTexto(DataI.Hour.ToString() + ":" + DataI.Minute.ToString());
                        Conexao.AdicionarParametroTexto(DataF.ToString("yyyy-MM-dd"));
                        Conexao.AdicionarParametroTexto(DataF.Hour.ToString() + ":" + DataF.Minute.ToString());
                        Conexao.AdicionarParametroTexto(txtNome.Text);
                        Conexao.AdicionarParametroTexto(txtDescricao.Text);

                        try
                        {
                            Conexao.AdicionarParametroDecimal(double.Parse(txtValor.Text));
                        }
                        catch
                        {
                            Conexao.AdicionarParametroDecimal(-1);
                        }

                        Conexao.ChamarStoredProcedureSR();

                        if (lstExcluirAnimais.Count > 0)
                        {
                            for (int i = 0; i < lstExcluirAnimais.Count; i++)
                            {
                                Conexao.IniciarStoredProcedure("ExcluirAnimal");
                                Conexao.AdicionarParametroInteiro(lstExcluirAnimais[i]);
                                Conexao.AdicionarParametroTexto(DataI.ToString("yyyy-MM-dd"));
                                Conexao.AdicionarParametroTexto(horarioI.Substring(0, 2) + ":" + horarioI.Substring(2, 2));
                                Conexao.ChamarStoredProcedureSR();
                            }
                        }

                        if (lstNovosAnimais.Count > 0)
                        {
                            for (int i = 0; i < lstNovosAnimais.Count; i++)
                            {
                                Conexao.IniciarStoredProcedure("AddRelacaoAnimal");
                                Conexao.AdicionarParametroInteiro(lstNovosAnimais[i]);
                                Conexao.AdicionarParametroTexto(DataI.ToString("yyyy-MM-dd"));
                                Conexao.AdicionarParametroTexto(horarioI.Substring(0, 2) + ":" + horarioI.Substring(2, 2));
                                Conexao.ChamarStoredProcedureSR();
                            }
                        }

                        MessageBox.Show("Edição salva com sucesso!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Close();
                    }
                    else
                    {
                        MessageBox.Show(agenda.DataExisteEdicao(DataI, DataF, DataInOr, DataFnOr));
                    }
                    #endregion
                }

                #region Alteração Tela Principal 

                frmPrincipal Prin = new frmPrincipal();

                Prin.dgvEvento.Rows.Clear();
                Prin.lblInfo.Text = "Eventos da Semana!";

                clsEventosProx Eve = new clsEventosProx();

                DR = null;
                Conexao.IniciarStoredProcedure("ListaEvento");
                DR = Conexao.ChamarStoredProcedureCR();

                DateTime First = Eve.FirstDay();
                DateTime Last = Eve.LastDay(First);

                while (DR.Read())
                {
                    if (DR.GetDate(0).Date >= System.DateTime.Today.Date && DR.GetDate(0) <= Last)
                    {
                        Prin.dgvEvento.Rows.Add(DR.GetDate(0).Date.ToString("dd/MM/yyyy"), DR[4].ToString());
                    }
                }

                DR.Dispose();
                DR.Close();
                Conexao.FecharConexao();

                #endregion

                Close();
            }
            else
            {
                MessageBox.Show("Atenção: Preencha todos os campos!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region Adiciona um novo Tipo de Evento
        private void btnAdd_Click(object sender, EventArgs e)
        {
            DR = null;

            if (cmbTipoEvento.Text == "")
            {
                MessageBox.Show("Digite um tipo de evento para cadastra-lo!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbTipoEvento.SelectedIndex != -1)
            {
                MessageBox.Show("Este evento já está cadastrado!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            for (int i = 0; i < cmbTipoEvento.Items.Count; i++)
            {
                if (cmbTipoEvento.Items[i].ToString().ToUpper() == cmbTipoEvento.Text.ToUpper())
                {
                    MessageBox.Show("Atenção: Já á um tipo de evento com este nome!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbTipoEvento.SelectedIndex = i;
                    return;
                }
            }

            Conexao.IniciarStoredProcedure("AddTipoEvento");
            Conexao.AdicionarParametroTexto(cmbTipoEvento.Text);
            Conexao.ChamarStoredProcedureSR();

            Conexao.IniciarStoredProcedure("TipoEvento");
            DR = Conexao.ChamarStoredProcedureCR();

            cmbTipoEvento.Items.Clear();
            CodigosTipoEvento.Clear();

            while (DR.Read())
            {
                cmbTipoEvento.Items.Add(DR.GetString(0));
                CodigosTipoEvento.Add(DR.GetInt32(1));
            }

            MessageBox.Show("Tipo de Evento cadastrado com sucesso!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            cmbTipoEvento.Text = "";

            DR.Close();
            DR.Dispose();
            Conexao.FecharConexao();
        }
        #endregion

        #region Exclui um Tipo de Evento
        private void btnExc_Click(object sender, EventArgs e)
        {
            if (cmbTipoEvento.Text == "")
            {
                MessageBox.Show("Atenção: Escolha um tipo de evento para exclui-lo!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbTipoEvento.SelectedIndex == -1)
            {
                MessageBox.Show("Atenção: Este evento não está cadastrado!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int codigo, i;
            codigo = CodigosTipoEvento[cmbTipoEvento.SelectedIndex];

            clsAgenda TE = new clsAgenda();

            for(i=0; i<TE.QtdCodigos1(); i++)
            {
                if ( codigo == TE.CodigosTE(i))
                {
                    MessageBox.Show("Atenção: Há um ou mais eventos cadastrados com o tipo de evento selecionado!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            Conexao.IniciarStoredProcedure("ExcluirTipoEvento");
            Conexao.AdicionarParametroInteiro(codigo);
            Conexao.AdicionarParametroTexto(cmbTipoEvento.Text);
            Conexao.ChamarStoredProcedureSR();

            DR = null;

            Conexao.IniciarStoredProcedure("TipoEvento");
            DR = Conexao.ChamarStoredProcedureCR();

            cmbTipoEvento.Items.Clear();
            CodigosTipoEvento.Clear();

            while (DR.Read())
            {
                cmbTipoEvento.Items.Add(DR.GetString(0));
                CodigosTipoEvento.Add(DR.GetInt32(1));
            }

            MessageBox.Show("Tipo de Evento excluido com sucesso!");
            cmbTipoEvento.SelectedIndex = -1;

            DR.Close();
            DR.Dispose();
            Conexao.FecharConexao();
        }
        #endregion

        #region Numero Do Evento
        private void cmbTipoEvento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (btnSalvar.Text == "&Salvar")
            {
                DR = null;

                if (cmbTipoEvento.SelectedIndex != -1)
                {
                    Conexao.IniciarStoredProcedure("NumeroEventoPorTipoEvento");
                    Conexao.AdicionarParametroInteiro(CodigosTipoEvento[cmbTipoEvento.SelectedIndex]);
                    DR = Conexao.ChamarStoredProcedureCR();

                    while (DR.Read())
                    {
                        if (DR[0].ToString() != "")
                        {
                            lblNumero.Text = DR[0].ToString();
                        }
                        else
                        {
                            lblNumero.Text = "1";
                        }
                    }

                    DR.Close();
                    DR.Dispose();
                    Conexao.FecharConexao();
                }
            }
        }
        #endregion

        #region Retira a digitação na combobox da tela de edição
        private void cmbTipoEvento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cmbTipoEvento.DropDownStyle == ComboBoxStyle.Simple)
            {
                e.KeyChar = Convert.ToChar(0);
            }
        }
        #endregion

        #region KeyChar - Valida a digitação - Valida a formatação
        private void txtValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            int NaoTem=0;

            #region Valida a digitação
            if (e.KeyChar == 8 || e.KeyChar > 47 && e.KeyChar < 58)
            {
                e.KeyChar = e.KeyChar;
            }
            else
            {
                e.KeyChar = Convert.ToChar(0);
            }
            #endregion

            #region Valida a formatação
            for (int i = 0; i < txtValor.Text.Length; i++)
            {
                if (txtValor.Text.Substring(i, 1) == "," || txtValor.Text == "")
                {
                    NaoTem = NaoTem + 1;
                }
            }

            if (NaoTem == txtValor.Text.Length)
            {
                txtValor.Text = ",";
            }
            #endregion
        }
        #endregion

        #region Valida data de inicio do evento

        Boolean DataValida;
        private void mtxtInicio_TextChanged(object sender, EventArgs e)
        {
            clsAgenda Agenda = new clsAgenda();
            DataValida = true;

            if (mtxtInicio.ReadOnly == false)
            {
                if (mtxtInicio.MaskCompleted == true)
                {
                    if (int.Parse(mtxtInicio.Text.Substring(3, 2)) > 12)
                    {
                        MessageBox.Show("Atenção: O mês deve ser menor que '12'!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        mtxtInicio.Focus();
                        DataValida = false;
                        //btnSalvar.Enabled = false;
                        return;
                    }

                    if (Agenda.ValidaDataVistoria(int.Parse(mtxtInicio.Text.Substring(0, 2)), int.Parse(mtxtInicio.Text.Substring(3, 2)), int.Parse(mtxtInicio.Text.Substring(6, 4))) == false)
                    {
                        MessageBox.Show("Atenção: A data ultrapassou o numero total de dias deste mês!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        mtxtInicio.Focus();
                        DataValida = false;
                        //btnSalvar.Enabled = false;
                        return;
                    }

                    if (DateTime.Parse(mtxtInicio.Text) < System.DateTime.Today)
                    {
                        MessageBox.Show("Atenção: A data " + mtxtInicio.Text + " já passou!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        mtxtInicio.Focus();
                        DataValida = false;
                        //btnSalvar.Enabled = false;
                        return;
                    }

                    DataValida = true;
                }
            }
        }
        #endregion

        #region Valida data de término do evento

        Boolean DataTermVal = true;
        private void mtxtFinal_TextChanged(object sender, EventArgs e)
        {
            clsAgenda Agenda = new clsAgenda();

            if (mtxtFinal.ReadOnly == false)
            {
                if (mtxtFinal.MaskCompleted == true)
                {
                    if (int.Parse(mtxtFinal.Text.Substring(3, 2)) > 12)
                    {
                        MessageBox.Show("Atenção: O mês deve ser menor que '12'!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        mtxtFinal.Focus();
                        DataTermVal = false;
                        return;
                    }

                    if (Agenda.ValidaDataVistoria(int.Parse(mtxtFinal.Text.Substring(0, 2)), int.Parse(mtxtFinal.Text.Substring(3, 2)), int.Parse(mtxtFinal.Text.Substring(6, 4))) == false)
                    {
                        MessageBox.Show("Atenção: A data ultrapassou o numero total de dias deste mês!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        mtxtFinal.Focus();
                        DataTermVal = false;
                        return;
                    }

                    if (DateTime.Parse(mtxtFinal.Text) < System.DateTime.Today)
                    {
                        MessageBox.Show("Atenção: A data " + mtxtFinal.Text + " já passou!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        mtxtFinal.Focus();
                        DataTermVal = false;
                        return;
                    }

                    if (DateTime.Parse(mtxtInicio.Text) > DateTime.Parse(mtxtFinal.Text))
                    {
                        MessageBox.Show("Atenção: A data de vecimento deve ser depois que a data do evento!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        mtxtFinal.Focus();
                        DataTermVal = false;
                        return;
                    }

                    DataTermVal = true;
                }
            }
        }
        #endregion

        #region Valida a hora caso seja um evento na data de hoje

        //Boolean HoraInicio;
        private void mtxtHoraInicial_TextChanged(object sender, EventArgs e)
        {
            //HoraInicio = true;

            //if (DateTime.Parse(mtxtInicio.Text).Date == System.DateTime.Today && mtxtHoraInicial.ReadOnly == false)
            //{
            //    if (mtxtHoraInicial.MaskCompleted && mtxtInicio.MaskCompleted)
            //    {
            //        DateTime HoraDig = (DateTime.Parse("01/01/0001 " + mtxtHoraInicial.Text.Substring(0, 2) + ":" + mtxtHoraInicial.Text.Substring(2, 2)));
            //        DateTime HoraAgora = DateTime.Parse("01/01/0001 " + System.DateTime.Now.Hour + ":" + System.DateTime.Now.Minute);

            //        if (DateTime.Parse(mtxtInicio.Text) == System.DateTime.Today && HoraDig < HoraAgora)
            //        {
            //            MessageBox.Show("Atenção: Está hora de inicio já passou!");
            //            HoraInicio = false;
            //            return;
            //        }
            //        else
            //        {
            //            HoraInicio = true;
            //        }
            //    }
            //}
        }

        //Boolean HoraFinal = true;
        private void mtxtHoraFinal_TextChanged(object sender, EventArgs e)
        {
            //if (DateTime.Parse(mtxtInicio.Text).Date == System.DateTime.Today && mtxtHoraFinal.ReadOnly == false)
            //{
            //    if (mtxtHoraFinal.MaskCompleted && mtxtInicio.MaskCompleted)
            //    {
            //        DateTime HoraDig = (DateTime.Parse("01/01/0001 " + mtxtHoraFinal.Text.Substring(0, 2) + ":" + mtxtHoraFinal.Text.Substring(2, 2)));
            //        DateTime HoraAgora = DateTime.Parse("01/01/0001 " + System.DateTime.Now.Hour + ":" + System.DateTime.Now.Minute);

            //        if (DateTime.Parse(mtxtInicio.Text) == System.DateTime.Today && HoraDig < HoraAgora)
            //        {
            //            MessageBox.Show("Atenção: Está hora de término já passou!");
            //            HoraFinal = false;
            //            return;
            //        }
            //        else
            //        {
            //            HoraFinal = true;
            //        }
            //    }
            //}
        }
        #endregion

        private void btnAnimais_Click(object sender, EventArgs e)
        {
            frmRelacaoAnimalEvento Relacao = new frmRelacaoAnimalEvento();

            Relacao.NovaRelação = true;
            Relacao.ShowDialog(this);
        }

        private void frmNovoEvento_FormClosing(object sender, FormClosingEventArgs e)
        {
            txtDescricao.Text = "";
            txtNome.Text = "";
            cmbTipoEvento.Text = "";
            txtValor.Text = "";
            lblNumero.Text = "";
            btnSalvar.Text = "&Salvar";
            btnCancelar.Text = "&Cancelar";

            cmbTipoEvento.Items.Clear();
            mtxtHoraInicial.Clear();
            mtxtHoraFinal.Clear();
            mtxtInicio.Clear();
            mtxtFinal.Clear();
            CodigosTipoEvento.Clear();
            lstCodigosAnimais.Clear();
            lstAnimais.Items.Clear();
            lstNovosAnimais.Clear();
            lstExcluirAnimais.Clear();

            mtxtInicio.ReadOnly = false;
            mtxtFinal.ReadOnly = false;
            grbAnimais.Enabled = true;

            cmbTipoEvento.DropDownStyle = ComboBoxStyle.DropDown;
        }
    }
}
