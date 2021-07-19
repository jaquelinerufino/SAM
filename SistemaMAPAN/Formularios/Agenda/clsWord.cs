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

        public void GerarContrato(string arqDestino, string diretorio, string DataVis, string DataVen, string Hora, string Nome,
            string Endereco,string Cidade,string Numero,string Bairro,string CEP, string Habilitado, string Descricao)
        {
            object missing = System.Reflection.Missing.Value;
            Word.Application oApp = new Word.Application();

            string arqOrigem = "Modelo.docx";
            arqDestino = arqDestino + ".docx";
            string origem = Application.StartupPath.ToString() + "\\Documentos\\" + arqOrigem;

            #region Criar pasta de Contrato

            string pasta = @diretorio + "\\Documentos";

            if (!Directory.Exists(pasta))
            {
                Directory.CreateDirectory(pasta);
            }

            #endregion

            //F:\Telas Tcc\1.Sistema MAPAN\Telas\Agenda Adaptada\prjAgendaTCC\prjAgendaTCC\bin\Debug\Documentos
            diretorio = @diretorio + "\\Documentos\\" + arqDestino;
            File.Copy(origem, diretorio, true);

            object template = diretorio;
            Word.Document oDoc = oApp.Documents.Open(template, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);
            Word.Range oRng = oDoc.Range(ref missing, ref missing);

            object FindText;
            object ReplaceWith;
            object MatchWholeWord;
            object Forward;

            # region Substituir o texto 

            oRng = oDoc.Range(ref missing, ref missing);
            FindText = "#DataMarcada#";
            ReplaceWith = DataVis;
            MatchWholeWord = true;
            Forward = false;
            oRng.Find.Execute(ref FindText, ref missing, ref MatchWholeWord, ref missing, ref missing, ref missing, ref Forward, ref missing, ref missing, ref ReplaceWith, ref missing, ref missing, ref missing, ref missing, ref missing);

            oRng = oDoc.Range(ref missing, ref missing);
            FindText = "#DataVistoria#";
            ReplaceWith = DataVen;
            MatchWholeWord = true;
            Forward = false;
            oRng.Find.Execute(ref FindText, ref missing, ref MatchWholeWord, ref missing, ref missing, ref missing, ref Forward, ref missing, ref missing, ref ReplaceWith, ref missing, ref missing, ref missing, ref missing, ref missing);

            oRng = oDoc.Range(ref missing, ref missing);
            FindText = "#CEP#";
            ReplaceWith = CEP;
            MatchWholeWord = true;
            Forward = false;
            oRng.Find.Execute(ref FindText, ref missing, ref MatchWholeWord, ref missing, ref missing, ref missing, ref Forward, ref missing, ref missing, ref ReplaceWith, ref missing, ref missing, ref missing, ref missing, ref missing);

            oRng = oDoc.Range(ref missing, ref missing);
            FindText = "#Hora#";
            ReplaceWith = Hora;
            MatchWholeWord = true;
            Forward = false;
            oRng.Find.Execute(ref FindText, ref missing, ref MatchWholeWord, ref missing, ref missing, ref missing, ref Forward, ref missing, ref missing, ref ReplaceWith, ref missing, ref missing, ref missing, ref missing, ref missing);

            oRng = oDoc.Range(ref missing, ref missing);
            FindText = "#Nome#";
            ReplaceWith = Nome;
            MatchWholeWord = true;
            Forward = false;
            oRng.Find.Execute(ref FindText, ref missing, ref MatchWholeWord, ref missing, ref missing, ref missing, ref Forward, ref missing, ref missing, ref ReplaceWith, ref missing, ref missing, ref missing, ref missing, ref missing);

            oRng = oDoc.Range(ref missing, ref missing);
            FindText = "#Endereco#";
            ReplaceWith = Endereco;
            MatchWholeWord = true;
            Forward = false;
            oRng.Find.Execute(ref FindText, ref missing, ref MatchWholeWord, ref missing, ref missing, ref missing, ref Forward, ref missing, ref missing, ref ReplaceWith, ref missing, ref missing, ref missing, ref missing, ref missing);

            oRng = oDoc.Range(ref missing, ref missing);
            FindText = "#Numero#";
            ReplaceWith = Numero;
            MatchWholeWord = true;
            Forward = false;
            oRng.Find.Execute(ref FindText, ref missing, ref MatchWholeWord, ref missing, ref missing, ref missing, ref Forward, ref missing, ref missing, ref ReplaceWith, ref missing, ref missing, ref missing, ref missing, ref missing);

            oRng = oDoc.Range(ref missing, ref missing);
            FindText = "#Bairro#";
            ReplaceWith = Bairro;
            MatchWholeWord = true;
            Forward = false;
            oRng.Find.Execute(ref FindText, ref missing, ref MatchWholeWord, ref missing, ref missing, ref missing, ref Forward, ref missing, ref missing, ref ReplaceWith, ref missing, ref missing, ref missing, ref missing, ref missing);

            oRng = oDoc.Range(ref missing, ref missing);
            FindText = "#Cidade#";
            ReplaceWith = Cidade;
            MatchWholeWord = true;
            Forward = false;
            oRng.Find.Execute(ref FindText, ref missing, ref MatchWholeWord, ref missing, ref missing, ref missing, ref Forward, ref missing, ref missing, ref ReplaceWith, ref missing, ref missing, ref missing, ref missing, ref missing);

            oRng = oDoc.Range(ref missing, ref missing);
            FindText = "#Hab#";
            ReplaceWith = Habilitado;
            MatchWholeWord = true;
            Forward = false;
            oRng.Find.Execute(ref FindText, ref missing, ref MatchWholeWord, ref missing, ref missing, ref missing, ref Forward, ref missing, ref missing, ref ReplaceWith, ref missing, ref missing, ref missing, ref missing, ref missing);

            oRng = oDoc.Range(ref missing, ref missing);
            FindText = "#Descrição#";
            ReplaceWith = Descricao;
            MatchWholeWord = true;
            Forward = false;
            oRng.Find.Execute(ref FindText, ref missing, ref MatchWholeWord, ref missing, ref missing, ref missing, ref Forward, ref missing, ref missing, ref ReplaceWith, ref missing, ref missing, ref missing, ref missing, ref missing);
            
            #endregion

            oDoc.Application.Documents[@diretorio].Save();
            Word._Document document = oDoc.Application.ActiveDocument;
            document.Close(Word.WdSaveOptions.wdDoNotSaveChanges);
            oApp.Visible = false;
        }

        #endregion

        #region Abrir o Contrato

        public static void abrircontrato(string arqDestino, string diretorio)
        {
 
            System.Diagnostics.Process.Start(@diretorio + "\\Documentos\\" + arqDestino);

        }

        #endregion
    }
}
