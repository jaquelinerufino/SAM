using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office;
using Word = Microsoft.Office.Interop.Word;
using System.Collections;
using System.Windows.Forms;
using System.IO;


namespace contrato_adocao
{
    class clsWord
    {

        #region Gerar o Contrato

        public static void GerarContrato(string arqDestino, 
            string nr_contrato, string nm_adotante, string rg_adotante, string cpf_adotante,
            string endereco, string nr_endereco, string complemento, string bairro, string cidade,
            string cep, string residencial, string comercial, string nr_celular, string email, 
            string nm_animal, string idade, string porte, string cor, string dia, string mes,
            string ano, int result_vermifugado, int result_vacinado, int result_castrado, string diretorio)

        {
            object missing = System.Reflection.Missing.Value;
            Word.Application oApp = new Word.Application();
            //
            string arqOrigem = "modelo.docx";
            arqDestino = arqDestino + ".docx";
            string origem = Application.StartupPath.ToString() + "\\Modelo\\" + arqOrigem;
            //
            #region Criar pasta de Contrato

            string pasta = @diretorio + "\\Contratos";

            if (!Directory.Exists(pasta))
            {
                Directory.CreateDirectory(pasta);
            }

            #endregion
            //
            diretorio = @diretorio + "\\Contratos\\" + arqDestino;
            File.Copy(origem, diretorio, true);
            //
            object template = diretorio;
            Word.Document oDoc = oApp.Documents.Open(template, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);
            Word.Range oRng = oDoc.Range(ref missing, ref missing);
            //
            object FindText;
            object ReplaceWith;
            object MatchWholeWord;
            object Forward;
            //
            # region Substituir o texto 

            #region Vermifugado
            if (result_vermifugado >= 1)
            {
                oRng = oDoc.Range(ref missing, ref missing);
                FindText = "#SIMVERMI#";
                ReplaceWith = "X";
                MatchWholeWord = true;
                Forward = false;
                oRng.Find.Execute(ref FindText, ref missing, ref MatchWholeWord, ref missing, ref missing, ref missing, ref Forward, ref missing, ref missing, ref ReplaceWith, ref missing, ref missing, ref missing, ref missing, ref missing);
                //
                oRng = oDoc.Range(ref missing, ref missing);
                FindText = "#NAOVERMI#";
                ReplaceWith = " ";
                MatchWholeWord = true;
                Forward = false;
                oRng.Find.Execute(ref FindText, ref missing, ref MatchWholeWord, ref missing, ref missing, ref missing, ref Forward, ref missing, ref missing, ref ReplaceWith, ref missing, ref missing, ref missing, ref missing, ref missing);

            }
            else
            {
                oRng = oDoc.Range(ref missing, ref missing);
                FindText = "#SIMVERMI#";
                ReplaceWith = " ";
                MatchWholeWord = true;
                Forward = false;
                oRng.Find.Execute(ref FindText, ref missing, ref MatchWholeWord, ref missing, ref missing, ref missing, ref Forward, ref missing, ref missing, ref ReplaceWith, ref missing, ref missing, ref missing, ref missing, ref missing);
                //
                oRng = oDoc.Range(ref missing, ref missing);
                FindText = "#NAOVERMI#";
                ReplaceWith = "X";
                MatchWholeWord = true;
                Forward = false;
                oRng.Find.Execute(ref FindText, ref missing, ref MatchWholeWord, ref missing, ref missing, ref missing, ref Forward, ref missing, ref missing, ref ReplaceWith, ref missing, ref missing, ref missing, ref missing, ref missing);

            }
            #endregion

            #region Vacinado
            if (result_vacinado >= 1)
            {
                oRng = oDoc.Range(ref missing, ref missing);
                FindText = "#SIMVACI#";
                ReplaceWith = "X";
                MatchWholeWord = true;
                Forward = false;
                oRng.Find.Execute(ref FindText, ref missing, ref MatchWholeWord, ref missing, ref missing, ref missing, ref Forward, ref missing, ref missing, ref ReplaceWith, ref missing, ref missing, ref missing, ref missing, ref missing);
                //
                oRng = oDoc.Range(ref missing, ref missing);
                FindText = "#NAOVACI#";
                ReplaceWith = " ";
                MatchWholeWord = true;
                Forward = false;
                oRng.Find.Execute(ref FindText, ref missing, ref MatchWholeWord, ref missing, ref missing, ref missing, ref Forward, ref missing, ref missing, ref ReplaceWith, ref missing, ref missing, ref missing, ref missing, ref missing);

            }
            else
            {
                oRng = oDoc.Range(ref missing, ref missing);
                FindText = "#SIMVACI#";
                ReplaceWith = " ";
                MatchWholeWord = true;
                Forward = false;
                oRng.Find.Execute(ref FindText, ref missing, ref MatchWholeWord, ref missing, ref missing, ref missing, ref Forward, ref missing, ref missing, ref ReplaceWith, ref missing, ref missing, ref missing, ref missing, ref missing);
                //
                oRng = oDoc.Range(ref missing, ref missing);
                FindText = "#NAOVACI#";
                ReplaceWith = "X";
                MatchWholeWord = true;
                Forward = false;
                oRng.Find.Execute(ref FindText, ref missing, ref MatchWholeWord, ref missing, ref missing, ref missing, ref Forward, ref missing, ref missing, ref ReplaceWith, ref missing, ref missing, ref missing, ref missing, ref missing);

            }
            #endregion

            #region Castrado
            if (result_castrado >= 1)
            {
                oRng = oDoc.Range(ref missing, ref missing);
                FindText = "#SIMCASTRA#";
                ReplaceWith = "X";
                MatchWholeWord = true;
                Forward = false;
                oRng.Find.Execute(ref FindText, ref missing, ref MatchWholeWord, ref missing, ref missing, ref missing, ref Forward, ref missing, ref missing, ref ReplaceWith, ref missing, ref missing, ref missing, ref missing, ref missing);
                //
                oRng = oDoc.Range(ref missing, ref missing);
                FindText = "#NAOCASTRA#";
                ReplaceWith = " ";
                MatchWholeWord = true;
                Forward = false;
                oRng.Find.Execute(ref FindText, ref missing, ref MatchWholeWord, ref missing, ref missing, ref missing, ref Forward, ref missing, ref missing, ref ReplaceWith, ref missing, ref missing, ref missing, ref missing, ref missing);

            }
            else
            {
                oRng = oDoc.Range(ref missing, ref missing);
                FindText = "#SIMCASTRA#";
                ReplaceWith = " ";
                MatchWholeWord = true;
                Forward = false;
                oRng.Find.Execute(ref FindText, ref missing, ref MatchWholeWord, ref missing, ref missing, ref missing, ref Forward, ref missing, ref missing, ref ReplaceWith, ref missing, ref missing, ref missing, ref missing, ref missing);
                //
                oRng = oDoc.Range(ref missing, ref missing);
                FindText = "#NAOCASTRA#";
                ReplaceWith = "X";
                MatchWholeWord = true;
                Forward = false;
                oRng.Find.Execute(ref FindText, ref missing, ref MatchWholeWord, ref missing, ref missing, ref missing, ref Forward, ref missing, ref missing, ref ReplaceWith, ref missing, ref missing, ref missing, ref missing, ref missing);

            }
            #endregion


            oRng = oDoc.Range(ref missing, ref missing);
            FindText = "#NUM#";
            ReplaceWith = nr_contrato;
            MatchWholeWord = true;
            Forward = false;
            oRng.Find.Execute(ref FindText, ref missing, ref MatchWholeWord, ref missing, ref missing, ref missing, ref Forward, ref missing, ref missing, ref ReplaceWith, ref missing, ref missing, ref missing, ref missing, ref missing);
            //
            oRng = oDoc.Range(ref missing, ref missing);
            FindText = "#NOMEDOADOTANTE#";
            ReplaceWith = nm_adotante;
            MatchWholeWord = true;
            Forward = false;
            oRng.Find.Execute(ref FindText, ref missing, ref MatchWholeWord, ref missing, ref missing, ref missing, ref Forward, ref missing, ref missing, ref ReplaceWith, ref missing, ref missing, ref missing, ref missing, ref missing);
            //
            oRng = oDoc.Range(ref missing, ref missing);
            FindText = "#RGDOADOTANTE#";
            ReplaceWith = rg_adotante;
            MatchWholeWord = true;
            Forward = false;
            oRng.Find.Execute(ref FindText, ref missing, ref MatchWholeWord, ref missing, ref missing, ref missing, ref Forward, ref missing, ref missing, ref ReplaceWith, ref missing, ref missing, ref missing, ref missing, ref missing);
            //
            oRng = oDoc.Range(ref missing, ref missing);
            FindText = "#CPFDOADOTANTE#";
            ReplaceWith = cpf_adotante;
            MatchWholeWord = true;
            Forward = false;
            oRng.Find.Execute(ref FindText, ref missing, ref MatchWholeWord, ref missing, ref missing, ref missing, ref Forward, ref missing, ref missing, ref ReplaceWith, ref missing, ref missing, ref missing, ref missing, ref missing);
            //
            oRng = oDoc.Range(ref missing, ref missing);
            FindText = "#ENDERECO#";
            ReplaceWith = endereco;
            MatchWholeWord = true;
            Forward = false;
            oRng.Find.Execute(ref FindText, ref missing, ref MatchWholeWord, ref missing, ref missing, ref missing, ref Forward, ref missing, ref missing, ref ReplaceWith, ref missing, ref missing, ref missing, ref missing, ref missing);
            //
            oRng = oDoc.Range(ref missing, ref missing);
            FindText = "#NRDOENDERECO#";
            ReplaceWith = nr_endereco;
            MatchWholeWord = true;
            Forward = false;
            oRng.Find.Execute(ref FindText, ref missing, ref MatchWholeWord, ref missing, ref missing, ref missing, ref Forward, ref missing, ref missing, ref ReplaceWith, ref missing, ref missing, ref missing, ref missing, ref missing);

            oRng = oDoc.Range(ref missing, ref missing);
            FindText = "#COMPLEMENTO#";
            ReplaceWith = complemento;
            MatchWholeWord = true;
            Forward = false;
            oRng.Find.Execute(ref FindText, ref missing, ref MatchWholeWord, ref missing, ref missing, ref missing, ref Forward, ref missing, ref missing, ref ReplaceWith, ref missing, ref missing, ref missing, ref missing, ref missing);
            //
            oRng = oDoc.Range(ref missing, ref missing);
            FindText = "#BAIRRO#";
            ReplaceWith = bairro;
            MatchWholeWord = true;
            Forward = false;
            oRng.Find.Execute(ref FindText, ref missing, ref MatchWholeWord, ref missing, ref missing, ref missing, ref Forward, ref missing, ref missing, ref ReplaceWith, ref missing, ref missing, ref missing, ref missing, ref missing);
            //
            oRng = oDoc.Range(ref missing, ref missing);
            FindText = "#CIDADE#";
            ReplaceWith = cidade;
            MatchWholeWord = true;
            Forward = false;
            oRng.Find.Execute(ref FindText, ref missing, ref MatchWholeWord, ref missing, ref missing, ref missing, ref Forward, ref missing, ref missing, ref ReplaceWith, ref missing, ref missing, ref missing, ref missing, ref missing);
            //
            oRng = oDoc.Range(ref missing, ref missing);
            FindText = "#CEP#";
            ReplaceWith = cep;
            MatchWholeWord = true;
            Forward = false;
            oRng.Find.Execute(ref FindText, ref missing, ref MatchWholeWord, ref missing, ref missing, ref missing, ref Forward, ref missing, ref missing, ref ReplaceWith, ref missing, ref missing, ref missing, ref missing, ref missing);
            //
            oRng = oDoc.Range(ref missing, ref missing);
            FindText = "#RESIDENCIAL#";
            ReplaceWith = residencial;
            MatchWholeWord = true;
            Forward = false;
            oRng.Find.Execute(ref FindText, ref missing, ref MatchWholeWord, ref missing, ref missing, ref missing, ref Forward, ref missing, ref missing, ref ReplaceWith, ref missing, ref missing, ref missing, ref missing, ref missing);
            //
            oRng = oDoc.Range(ref missing, ref missing);
            FindText = "#COMERCIAL#";
            ReplaceWith = comercial;
            MatchWholeWord = true;
            Forward = false;
            oRng.Find.Execute(ref FindText, ref missing, ref MatchWholeWord, ref missing, ref missing, ref missing, ref Forward, ref missing, ref missing, ref ReplaceWith, ref missing, ref missing, ref missing, ref missing, ref missing);
            oDoc.Save();
            //
            oRng = oDoc.Range(ref missing, ref missing);
            FindText = "#EMAIL#";
            ReplaceWith = email;
            MatchWholeWord = true;
            Forward = false;
            oRng.Find.Execute(ref FindText, ref missing, ref MatchWholeWord, ref missing, ref missing, ref missing, ref Forward, ref missing, ref missing, ref ReplaceWith, ref missing, ref missing, ref missing, ref missing, ref missing);
            //
            oRng = oDoc.Range(ref missing, ref missing);
            FindText = "#CELULAR#";
            ReplaceWith = nr_celular;
            MatchWholeWord = true;
            Forward = false;
            oRng.Find.Execute(ref FindText, ref missing, ref MatchWholeWord, ref missing, ref missing, ref missing, ref Forward, ref missing, ref missing, ref ReplaceWith, ref missing, ref missing, ref missing, ref missing, ref missing);
            //
            oRng = oDoc.Range(ref missing, ref missing);
            FindText = "#NMDOANIMAL#";
            ReplaceWith = nm_animal;
            MatchWholeWord = true;
            Forward = false;
            oRng.Find.Execute(ref FindText, ref missing, ref MatchWholeWord, ref missing, ref missing, ref missing, ref Forward, ref missing, ref missing, ref ReplaceWith, ref missing, ref missing, ref missing, ref missing, ref missing);
            //
            oRng = oDoc.Range(ref missing, ref missing);
            FindText = "#IDADE#";
            ReplaceWith = idade;
            MatchWholeWord = true;
            Forward = false;
            oRng.Find.Execute(ref FindText, ref missing, ref MatchWholeWord, ref missing, ref missing, ref missing, ref Forward, ref missing, ref missing, ref ReplaceWith, ref missing, ref missing, ref missing, ref missing, ref missing);
            //
            oRng = oDoc.Range(ref missing, ref missing);
            FindText = "#PORTE#";
            ReplaceWith = porte;
            MatchWholeWord = true;
            Forward = false;
            oRng.Find.Execute(ref FindText, ref missing, ref MatchWholeWord, ref missing, ref missing, ref missing, ref Forward, ref missing, ref missing, ref ReplaceWith, ref missing, ref missing, ref missing, ref missing, ref missing);
            //
            oRng = oDoc.Range(ref missing, ref missing);
            FindText = "#COR#";
            ReplaceWith = cor;
            MatchWholeWord = true;
            Forward = false;
            oRng.Find.Execute(ref FindText, ref missing, ref MatchWholeWord, ref missing, ref missing, ref missing, ref Forward, ref missing, ref missing, ref ReplaceWith, ref missing, ref missing, ref missing, ref missing, ref missing);
            //
            oRng = oDoc.Range(ref missing, ref missing);
            FindText = "#DIA#";
            ReplaceWith = dia;
            MatchWholeWord = true;
            Forward = false;
            oRng.Find.Execute(ref FindText, ref missing, ref MatchWholeWord, ref missing, ref missing, ref missing, ref Forward, ref missing, ref missing, ref ReplaceWith, ref missing, ref missing, ref missing, ref missing, ref missing);
            //
            oRng = oDoc.Range(ref missing, ref missing);
            FindText = "#MES#";
            ReplaceWith = mes;
            MatchWholeWord = true;
            Forward = false;
            oRng.Find.Execute(ref FindText, ref missing, ref MatchWholeWord, ref missing, ref missing, ref missing, ref Forward, ref missing, ref missing, ref ReplaceWith, ref missing, ref missing, ref missing, ref missing, ref missing);
            //
            oRng = oDoc.Range(ref missing, ref missing);
            FindText = "#ANO#";
            ReplaceWith = ano;
            MatchWholeWord = true;
            Forward = false;
            oRng.Find.Execute(ref FindText, ref missing, ref MatchWholeWord, ref missing, ref missing, ref missing, ref Forward, ref missing, ref missing, ref ReplaceWith, ref missing, ref missing, ref missing, ref missing, ref missing);
            //
            #endregion
            //
            oDoc.Application.Documents[@diretorio].Save();
            Word._Document document = oDoc.Application.ActiveDocument;
            document.Close(Word.WdSaveOptions.wdDoNotSaveChanges);
            oApp.Visible = false;
        }

        #endregion

        #region Abrir o Contrato

        public static void abrircontrato(Boolean Foi_sim, string arqDestino, string diretorio)
        {
            
            if (Foi_sim == true)
            {
                System.Diagnostics.Process.Start(@diretorio + "\\Contratos\\" + arqDestino);
            }
        }

        #endregion
    }
}
