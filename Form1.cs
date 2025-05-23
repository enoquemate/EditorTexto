using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EditorTexto
{
    public partial class Form1 : Form
    {
        StringReader Leitura = null;
        public Form1()
        {
            InitializeComponent();
        }
        string conteudoParaImprimir;

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void arquivoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(richTextBox1.Text))
            {
                var result = MessageBox.Show(
                    "Deseja salvar o arquivo atual antes de criar um novo?",
                    "Salvar arquivo",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Reutiliza o código do salvarToolStripMenuItem_Click
                    salvarToolStripMenuItem_Click(sender, e);
                }
                else if (result == DialogResult.Cancel)
                {
                    // Cancela a operação de novo arquivo
                    return;
                }
            }

            richTextBox1.Clear();
            richTextBox1.Focus();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Text = File.ReadAllText(openFileDialog1.FileName);
            }
        }

        private void salvarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Define o directório inicial (por exemplo: Documentos)
            saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // Define os tipos de ficheiro que podem ser gravados
            saveFileDialog1.Filter = "Ficheiros de texto (*.txt)|*.txt|Ficheiros Rich Text (*.rtf)|*.rtf|Todos os ficheiros (*.*)|*.*";

            // Define o ficheiro padrão
            saveFileDialog1.DefaultExt = "txt";
            saveFileDialog1.FileName = "novo_arquivo";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string caminho = saveFileDialog1.FileName;

                // Grava como .rtf se extensão for .rtf
                if (Path.GetExtension(caminho).ToLower() == ".rtf")
                {
                    richTextBox1.SaveFile(caminho, RichTextBoxStreamType.RichText);
                }
                else
                {
                    // Caso contrário, grava como texto simples
                    File.WriteAllText(caminho, richTextBox1.Text);
                }
            }

        }

        private void copiarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void recortarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void colarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void localizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
           /* string termo = InputBox("Digite o texto a localizar:", "Localizar");
            if (!string.IsNullOrEmpty(termo))
            {
                int posicao = richTextBox1.Text.IndexOf(termo, StringComparison.CurrentCultureIgnoreCase);
                if (posicao >= 0)
                {
                    richTextBox1.Select(posicao, termo.Length);
                    richTextBox1.Focus();
                }
                else
                {
                    MessageBox.Show("Texto não encontrado.");
                }
            }*/
        }

        private void fonteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Font = fontDialog1.Font;
            }
        }

        private void corToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.ForeColor = colorDialog1.Color;
            }

        }

        private void visualizarImpressãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            conteudoParaImprimir = richTextBox1.Text;
            Leitura = new StringReader(conteudoParaImprimir);
            printDocument1.PrintPage -= printDocument1_PrintPage; // Evita múltiplas assinaturas
            printDocument1.PrintPage += printDocument1_PrintPage;
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string texto = this.richTextBox1.Text;
            Leitura = new StringReader(texto);
            if(printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void esquerdaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void direitaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void centralizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
        }
        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            float linhasPorPagina = 0;
            float posY = 0;
            int contador = 0;
            float margemEsquerda = e.MarginBounds.Left;
            float margemSuperior = e.MarginBounds.Top;
            string linha = null;
            Font fonte = richTextBox1.Font;
            SolidBrush brush = new SolidBrush(richTextBox1.ForeColor);

            // Calcula o número de linhas por página
            linhasPorPagina = e.MarginBounds.Height / fonte.GetHeight(e.Graphics);

            while (contador < linhasPorPagina && ((linha = Leitura.ReadLine()) != null))
            {
                posY = margemSuperior + (contador * fonte.GetHeight(e.Graphics));
                e.Graphics.DrawString(linha, fonte, brush, margemEsquerda, posY, new StringFormat());
                contador++;
            }

            // Se ainda houver linhas, imprime mais páginas
            e.HasMorePages = (linha != null);
        }
    }
}
