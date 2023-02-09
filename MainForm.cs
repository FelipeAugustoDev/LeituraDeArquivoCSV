using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeituraDeArquivoCSV
{
    public partial class MainForm : Form
    {
        GerenciandoBD BD = new GerenciandoBD();
        Log log = new Log();
        public MainForm()
        {
            
            InitializeComponent();
            limparControles();
            

            try
            {
                BD = new GerenciandoBD("C:\\Users\\felipe.abalmeida\\Documents\\TabelaDeFelipe2.csv");
                btnLeitura.Text = "Salvar Dados";
                 
                lboPessoas.Items.Clear();
                lboPessoas.Items.AddRange(BD.leituraBase());
                btnLeitura.Text = "Salvar Dados";

            }
            catch (Exception)
            {
                BD = new GerenciandoBD();

            }
        }

        private void limparControles()
        {
            btnRemover.Visible = false;
            btnEditar.Visible = false;
            txtNome.Enabled = true;
            txtSobrenome.Enabled = true;
            dtpNascimento.Enabled = true;
            txtNome.Clear();
            txtSobrenome.Clear();
            dtpNascimento.Value = DateTime.Now;
            txtTelefone.Clear();
            txtEmail.Clear();

        }
        /// <summary>
        /// Inserção da base de dados no ListBox
        /// </summary>
        private void btnLeitura_Click(object sender, EventArgs e)
        {
            if (btnLeitura.Text == "Inserir dados")
            {
                //Limpa o ListBox e tenta adicionar a BD no ListBox
                try
                {
                    lboPessoas.Items.Clear();
                    lboPessoas.Items.AddRange(BD.leituraBaseComCaminho());
                    btnLeitura.Text = "Salvar Dados";
                }
                //Manda para a guia de erro
                catch (Exception E)
                {
                    tbPrincipal.SelectedIndex = 1;
                    lblErro.Text = E.Message;
    
                    return;
                }
            }
            else
            {
                try
                {
                    BD.salvarndoArquivo();
                }
                catch (Exception E)
                {
                    tbPrincipal.SelectedIndex = 1;
                    lblErro.Text = E.Message;

                    return;
                }
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {


            if (btnAdd.Text == "Adicionar")
            {

            
            //criando meus campos para pessoa
            string nome = txtNome.Text, sobrenome = txtSobrenome.Text, telefone = txtTelefone.Text, email = txtEmail.Text;
            DateTime dtNascimento = dtpNascimento.Value;



            //fazendo instancia da classe
            Pessoa pessoa = new Pessoa(nome, sobrenome, dtNascimento, telefone, email);
            //recebendo nova atualizacao da base de dados
            Pessoa[] pessoas = BD.adicionar(pessoa);


            //limpando minha lista
            lboPessoas.Items.Clear();

            //atualizando a listbox
            lboPessoas.Items.AddRange(pessoas);

            limparControles();
            }
            else
            {
                btnAdd.Text = "Adicionar";
                lboPessoas.ClearSelected();
                limparControles();
            }
            



        }


        private void btnRemover_Click(object sender, EventArgs e)
        {
            Pessoa pessoa = lboPessoas.SelectedItem as Pessoa;//instanciando pessoas
            lboPessoas.ClearSelected(); //limpando selecao
            lboPessoas.Items.Clear();//limpando itens selecionados

            lboPessoas.Items.AddRange(BD.remover(pessoa));//DEVOLVENDO A LISTA ATUALIZADA PRO LISTBOX
            limparControles();

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Pessoa pessoa = (Pessoa)lboPessoas.SelectedItem;
            string Email =  txtEmail.Text, Telefone = txtTelefone.Text;
            pessoa.Email = Email;
            pessoa.Telefone = Telefone;
            lboPessoas.ClearSelected(); //limpando selecao
            lboPessoas.Items.Clear();//limpando itens selecionados

            lboPessoas.Items.AddRange(BD.updateItem(pessoa));//DEVOLVENDO A LISTA ATUALIZADA PRO LISTBOX
            limparControles();

        }
        private void btnLimpeza_Click(object sender, EventArgs e)
        {
            lboPessoas.Items.Clear();
            BD.limparBD();
            limparControles();
        }

        private void btnErro_Click(object sender, EventArgs e)
        {
            tbPrincipal.SelectedIndex = 0;
        }

        private void lboPessoas_SelectedValueChanged(object sender, EventArgs e)
        {

            if (lboPessoas.SelectedItem == null) return;
            
            //bloqueia os controles para modificacao
            txtNome.Enabled = false;
            txtSobrenome.Enabled = false;
            dtpNascimento.Enabled = false;

            //resgatando os dados do elemento
            Pessoa pessoa = lboPessoas.SelectedItem as Pessoa;
            txtNome.Text = pessoa.Nome;
            txtSobrenome.Text = pessoa.Sobrenome;
            txtTelefone.Text = pessoa.Telefone;
            txtEmail.Text = pessoa.Email;
            dtpNascimento.Value = pessoa.DtNascimento;


            //criando controles visiveis
            btnEditar.Visible = true;
            btnRemover.Visible = true;
            btnAdd.Text = "Cancelar";



        }
    }
}
