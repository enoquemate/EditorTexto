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
        StreamReader Leitura = null;
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
            /* string termo = Prompt.ShowDialog("Digite o texto a localizar:", "Localizar");
             int posicao = richTextBox1.Text.IndexOf(termo, StringComparison.CurrentCultureIgnoreCase);
             if (posicao >= 0)
             {
                 richTextBox1.Select(posicao, termo.Length);
                 richTextBox1.Focus();
             }
             else
             {
                 MessageBox.Show("Texto não encontrado.");
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
            printPreviewDialog1.Document = printDocument1;
            conteudoParaImprimir = richTextBox1.Text;
            printPreviewDialog1.ShowDialog();

        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        
    }
}
