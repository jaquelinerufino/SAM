using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Odbc;

namespace SistemaMAPAN
{
    class clsAgenda
    {
        #region Conexão e Variaveis
        clsConexao.clsConexao Conexao = new clsConexao.clsConexao("mapan", "localhost", "root", "root");
        OdbcDataReader DR;

        List<int> CodigosTipoEvento1 = new List<int>();
        List<string> Nome = new List<string>();
        List<DateTime> DataEventoInicio = new List<DateTime>();
        List<DateTime> DataEventoTermino = new List<DateTime>();

        int tem;
        string periodoNew, periodoCad;

        //public DateTime DataI
        //{
        //    get { return DataInicio; }
        //    set { DataInicio = value; }
        //}

        //public DateTime DataF
        //{
        //    get { return DataTermino; }
        //    set { DataTermino = value; }
        //}

        public int TEM
        {
            get { return tem; }
            set { tem = value; }
        }
        #endregion

        #region Retorno codigo do tipo evento
        public int CodigosTE(int cdTE)
        {
            DR = null;
            CodigosTipoEvento1.Clear();

            Conexao.IniciarStoredProcedure("ListaEvento");
            DR = Conexao.ChamarStoredProcedureCR();

            while (DR.Read())
            {
                CodigosTipoEvento1.Add(DR.GetInt32(8));
            }

            DR.Close();
            DR.Dispose();
            Conexao.FecharConexao();

            return CodigosTipoEvento1[cdTE];
        }
        #endregion

        #region Retorna a quantidade de tipo evento
        public int QtdCodigos1()
        {
            DR = null;
            CodigosTipoEvento1.Clear();

            Conexao.IniciarStoredProcedure("ListaEvento");
            DR = Conexao.ChamarStoredProcedureCR();

            while (DR.Read())
            {
                CodigosTipoEvento1.Add(DR.GetInt32(8));
            }

            DR.Close();
            DR.Dispose();
            Conexao.FecharConexao();

            return CodigosTipoEvento1.Count;
        }
        #endregion

        //DateTime DataIHoraI, DateTime DataIHoraF, DateTime DataFHoraI, DateTime DataFHoraF

        #region Verificação da data para novo evento
        public string DataExiste(DateTime DataI, DateTime DataF)
        {
            string Data;
            tem = 0;

            DataEventoInicio.Clear();
            DataEventoTermino.Clear();
            DR = null;

            Conexao.IniciarStoredProcedure("ListaEvento");
            DR = Conexao.ChamarStoredProcedureCR();

            while (DR.Read())
            {
                Data = DR.GetString(0) + " " + DR.GetString(1);
                DataEventoInicio.Add(DateTime.Parse(Data));

                Data = DR.GetString(2) + " " + DR.GetString(3);
                DataEventoTermino.Add(DateTime.Parse(Data));

                Nome.Add(DR.GetString(4));
            }

            DR.Close();
            DR.Dispose();
            Conexao.FecharConexao();

            if (DataI.Date != DataF.Date)
            {
                for (int i = 0; i < DataEventoInicio.Count; i++)
                {
                    if (DataI.Date <= DataEventoInicio[i].Date && DataF.Date >= DataEventoTermino[i].Date || DataI.Date >= DataEventoInicio[i].Date && DataF.Date <= DataEventoTermino[i].Date)
                    {
                        if (DataI.Hour >= DataEventoInicio[i].Hour && DataF.Hour <= DataEventoTermino[i].Hour || DataI.Hour <= DataEventoInicio[i].Hour && DataF.Hour >= DataEventoTermino[i].Hour || DataI.Hour > DataEventoInicio[i].Hour && DataI.Hour < DataEventoTermino[i].Hour || DataF.Hour > DataEventoInicio[i].Hour && DataF.Hour < DataEventoTermino[i].Hour)
                        {
                            return "Esta data já esta cadastrada no evento: " + Nome[i] + " - " + DataEventoInicio[i].ToString() + " - " + DataEventoTermino[i].ToString();
                        }

                        if (DataI.Hour == DataEventoInicio[i].Hour || DataI.Hour == DataEventoTermino[i].Hour || DataF.Hour == DataEventoInicio[i].Hour || DataF.Hour == DataEventoTermino[i].Hour)
                        {
                            if (DataI.Hour == DataEventoInicio[i].Hour)
                            {
                                if (DataI.Minute >= DataEventoInicio[i].Minute)
                                {
                                    return "Esta data já esta cadastrada no evento: " + Nome[i] + " - " + DataEventoInicio[i].ToString() + " - " + DataEventoTermino[i].ToString();
                                }
                            }

                            if (DataI.Hour == DataEventoTermino[i].Hour)
                            {
                                if (DataI.Minute <= DataEventoTermino[i].Minute)
                                {
                                    return "Esta data já esta cadastrada no evento: " + Nome[i] + " - " + DataEventoInicio[i].ToString() + " - " + DataEventoTermino[i].ToString();
                                }
                            }

                            if (DataF.Hour == DataEventoInicio[i].Hour)
                            {
                                if (DataF.Minute >= DataEventoInicio[i].Minute)
                                {
                                    return "Esta data já esta cadastrada no evento: " + Nome[i] + " - " + DataEventoInicio[i].ToString() + " - " + DataEventoTermino[i].ToString();
                                }
                            }

                            if (DataF.Hour == DataEventoTermino[i].Hour)
                            {
                                if (DataF.Minute <= DataEventoTermino[i].Minute)
                                {
                                    return "Esta data já esta cadastrada no evento: " + Nome[i] + " - " + DataEventoInicio[i].ToString() + " - " + DataEventoTermino[i].ToString();
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < DataEventoInicio.Count; i++)
                {
                    if (DataI.Date >= DataEventoInicio[i].Date && DataI.Date <= DataEventoTermino[i].Date)
                    {
                        if (DataI.Hour >= DataEventoInicio[i].Hour && DataF.Hour <= DataEventoTermino[i].Hour || DataI.Hour <= DataEventoInicio[i].Hour && DataF.Hour >= DataEventoTermino[i].Hour || DataI.Hour > DataEventoInicio[i].Hour && DataI.Hour < DataEventoTermino[i].Hour || DataF.Hour > DataEventoInicio[i].Hour && DataF.Hour < DataEventoTermino[i].Hour)
                        {
                            return "Esta data já esta cadastrada no evento: " + Nome[i] + " - " + DataEventoInicio[i].ToString() + " - " + DataEventoTermino[i].ToString();
                        }

                        if (DataI.Hour == DataEventoInicio[i].Hour || DataI.Hour == DataEventoTermino[i].Hour || DataF.Hour == DataEventoInicio[i].Hour || DataF.Hour == DataEventoTermino[i].Hour)
                        {
                            if (DataI.Hour == DataEventoInicio[i].Hour)
                            {
                                if (DataI.Minute >= DataEventoInicio[i].Minute)
                                {
                                    return "Esta data já esta cadastrada no evento: " + Nome[i] + " - " + DataEventoInicio[i].ToString() + " - " + DataEventoTermino[i].ToString();
                                }
                            }

                            if (DataI.Hour == DataEventoTermino[i].Hour)
                            {
                                if (DataI.Minute <= DataEventoTermino[i].Minute)
                                {
                                    return "Esta data já esta cadastrada no evento: " + Nome[i] + " - " + DataEventoInicio[i].ToString() + " - " + DataEventoTermino[i].ToString();
                                }
                            }

                            if (DataF.Hour == DataEventoInicio[i].Hour)
                            {
                                if (DataF.Minute >= DataEventoInicio[i].Minute)
                                {
                                    return "Esta data já esta cadastrada no evento: " + Nome[i] + " - " + DataEventoInicio[i].ToString() + " - " + DataEventoTermino[i].ToString();
                                }
                            }

                            if (DataF.Hour == DataEventoTermino[i].Hour)
                            {
                                if (DataF.Minute <= DataEventoTermino[i].Minute)
                                {
                                    return "Esta data já esta cadastrada no evento: " + Nome[i] + " - " + DataEventoInicio[i].ToString() + " - " + DataEventoTermino[i].ToString();
                                }
                            }
                        }
                    }
                }
            }

            return "";
        }
        #endregion

        #region Verificação da data para edição do evento
        public string DataExisteEdicao(DateTime DataI, DateTime DataF, DateTime DataInOr, DateTime DataFnOr)
        {
            string Data;
            DataEventoInicio.Clear();
            DataEventoTermino.Clear();
            DR = null;

            Conexao.IniciarStoredProcedure("ListaEvento");
            DR = Conexao.ChamarStoredProcedureCR();

            while (DR.Read())
            {
                Data = DR.GetString(0) + " " + DR.GetString(1);
                DataEventoInicio.Add(DateTime.Parse(Data));

                Data = DR.GetString(2) + " " + DR.GetString(3);
                DataEventoTermino.Add(DateTime.Parse(Data));

                Nome.Add(DR.GetString(4));
            }

            DR.Close();
            DR.Dispose();
            Conexao.FecharConexao();

            for (int i = 0; i < DataEventoInicio.Count; i++)
            {
                if (DataEventoInicio[i] != DataInOr && DataEventoTermino[i] != DataFnOr)
                {
                    if (DataI.Date <= DataEventoInicio[i].Date && DataF.Date >= DataEventoTermino[i].Date || DataI.Date >= DataEventoInicio[i].Date && DataF.Date <= DataEventoTermino[i].Date)
                    {
                        if (DataI.Hour >= DataEventoInicio[i].Hour && DataF.Hour <= DataEventoTermino[i].Hour || DataI.Hour <= DataEventoInicio[i].Hour && DataF.Hour >= DataEventoTermino[i].Hour || DataI.Hour > DataEventoInicio[i].Hour && DataI.Hour < DataEventoTermino[i].Hour || DataF.Hour > DataEventoInicio[i].Hour && DataF.Hour < DataEventoTermino[i].Hour)
                        {
                            return "Esta data já esta cadastrada no evento: " + Nome[i] + " - " + DataEventoInicio[i].ToString() + " - " + DataEventoTermino[i].ToString();
                        }

                        if (DataI.Hour == DataEventoInicio[i].Hour || DataI.Hour == DataEventoTermino[i].Hour || DataF.Hour == DataEventoInicio[i].Hour || DataF.Hour == DataEventoTermino[i].Hour)
                        {
                            if (DataI.Hour == DataEventoInicio[i].Hour)
                            {
                                if (DataI.Minute >= DataEventoInicio[i].Minute)
                                {
                                    return "Esta data já esta cadastrada no evento: " + Nome[i] + " - " + DataEventoInicio[i].ToString() + " - " + DataEventoTermino[i].ToString();
                                }
                            }

                            if (DataI.Hour == DataEventoTermino[i].Hour)
                            {
                                if (DataI.Minute <= DataEventoTermino[i].Minute)
                                {
                                    return "Esta data já esta cadastrada no evento: " + Nome[i] + " - " + DataEventoInicio[i].ToString() + " - " + DataEventoTermino[i].ToString();
                                }
                            }

                            if (DataF.Hour == DataEventoInicio[i].Hour)
                            {
                                if (DataF.Minute >= DataEventoInicio[i].Minute)
                                {
                                    return "Esta data já esta cadastrada no evento: " + Nome[i] + " - " + DataEventoInicio[i].ToString() + " - " + DataEventoTermino[i].ToString();
                                }
                            }

                            if (DataF.Hour == DataEventoTermino[i].Hour)
                            {
                                if (DataF.Minute <= DataEventoTermino[i].Minute)
                                {
                                    return "Esta data já esta cadastrada no evento: " + Nome[i] + " - " + DataEventoInicio[i].ToString() + " - " + DataEventoTermino[i].ToString();
                                }
                            }
                        }
                    }
                }
            }

            return "";
        }
        #endregion



        #region Valida a data digitada pelo usuario na tela de vistoria
        public Boolean ValidaDataVistoria(int Dia, int Mes, int Ano)
        {
            if (Mes > 55)
            {
                return false;
            }

            if (Mes == 1 || Mes == 3 || Mes == 5 || Mes == 7 || Mes == 8 || Mes == 10 || Mes == 12)
            {
                if (Dia > 31)
                {
                    return false;
                }
            }

            if (Mes == 4 || Mes == 6 || Mes == 9 || Mes == 11)
            {
                if (Dia > 30)
                {
                    return false;
                }
            }

            if (Mes == 2)
            {
                if (Ano % 400 == 0 || Ano % 4 == 0 && Ano % 100 != 0)
                {
                    if (Dia > 29)
                    {
                        return false;
                    }
                }
                
                if (Dia > 28)
                {
                    return false;
                }
            }

            return true;
        }
        #endregion

        #region Verifica se há uma vistoria cadastrada com os mesmos termos
        public string ExisteVistoria(string Data, string hora, int pessoa, string Botao)
        {
            frmVistoria Vis = new frmVistoria();

            Conexao.IniciarStoredProcedure("ListaVistoria");
            DR = Conexao.ChamarStoredProcedureCR();

            if (int.Parse(hora.Replace(":", "")) >= 0600 && int.Parse(hora.Replace(":", "")) <= 1259)
            {
                periodoNew = "manhã";
            }

            if (int.Parse(hora.Replace(":", "")) >= 1300 && int.Parse(hora.Replace(":", "")) <= 1759)
            {
                periodoNew = "tarde";
            }

            if (int.Parse(hora.Replace(":", "")) >= 1800 && int.Parse(hora.Replace(":", "")) <= 2359 || int.Parse(hora.Replace(":", "")) >= 0000 && int.Parse(hora.Replace(":", "")) <= 0559)
            {
                periodoNew = "noite";
            }

            while (DR.Read())
            {
                string horaCad = DR[2].ToString();

                if (int.Parse(horaCad.Replace(":", "")) >= 060000 && int.Parse(horaCad.Replace(":", "")) <= 125900)
                {
                    periodoCad = "manhã";
                }

                if (int.Parse(horaCad.Replace(":", "")) >= 130000 && int.Parse(horaCad.Replace(":", "")) <= 175900)
                {
                    periodoCad = "tarde";
                }

                if (int.Parse(horaCad.Replace(":", "")) >= 180000 && int.Parse(horaCad.Replace(":", "")) <= 235900 || int.Parse(horaCad.Replace(":", "")) >= 000000 && int.Parse(horaCad.Replace(":", "")) <= 055900)
                {
                    periodoCad = "noite";
                }

                if (Botao == "&Salvar")
                {
                    if (Data == DR.GetDateTime(0).Date.ToString("dd/MM/yyyy") && pessoa == DR.GetInt32(3) && periodoNew == periodoCad)
                    {
                        return "Atenção: Há um evento cadastrado com estas condições: " + DR.GetDateTime(0).Date.ToString("dd/MM/yyyy") + " - " + DR[2].ToString() + " Período: " + periodoCad + " - " + DR[1].ToString();
                    }
                }
                else
                {

                }
            }

            DR.Close();
            DR.Dispose();
            Conexao.FecharConexao();

            return "";
        }
        #endregion
    }
}