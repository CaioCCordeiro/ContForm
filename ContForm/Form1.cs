using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace ContForm
{
    public partial class Form1 : Form
    {
        Contatos contatos = new Contatos();
        List<string> nums = new List<string>();
        List<string> tipo = new List<string>();

        public Form1()
        {
            InitializeComponent();
            comboBoxTipo.SelectedIndex = 0;
        }

        public void listaDeNumeros()
        {
            listBoxNum.Items.Clear();
            listBoxTipo.Items.Clear();

            for (int i = 0; i < this.nums.Count; i++)
            {
                listBoxNum.Items.Add(this.nums[i]);
                listBoxTipo.Items.Add(this.tipo[i]);
            }
        }

        public void limparForm()
        {
            textBoxMail.Text = "";
            textBoxName.Text = "";
            textBoxNum.Text = "";
            comboBoxTipo.SelectedIndex = 0;
            listBoxNum.Items.Clear();
            listBoxTipo.Items.Clear();
            this.nums.Clear();
            this.tipo.Clear();
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            if(textBoxMail.Text != "" || textBoxName.Text != "" || textBoxNum.Text != "")
            {
                result = MessageBox.Show("Tem certeza que deseja limpar o formulário?","", buttons);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    limparForm();
                }
            }
            
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if(textBoxMail.Text != "")
            {
                List<Fone> teladd = new List<Fone>();
                for (int i = 0; i < this.nums.Count; i++)
                {
                    Fone newtel = new Fone(this.nums[i], this.tipo[i]);
                    teladd.Add(newtel);
                }

                contatos.Adicionar(new Contato(textBoxMail.Text, textBoxName.Text, teladd));

                MessageBox.Show("Contato salvo");
            }
            else
            {
                MessageBox.Show("ERRO: Campo E-mail não pode estar em branco");
            }

        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            
            string emailpesquisa = textBoxMail.Text;
            Contato contatoPesquisa = contatos.Pesquisar(new Contato(emailpesquisa));
            if (contatoPesquisa.Email == emailpesquisa)
            {
                textBoxMail.Text = contatoPesquisa.Email;
                textBoxName.Text = contatoPesquisa.Nome;

                this.nums.Clear();
                this.tipo.Clear();

                if(textBoxMail.Text != "")
                {
                    for (int i = 0; i < contatoPesquisa.Telefones.Count; i++)
                    {
                        this.nums.Add(contatoPesquisa.Telefones[i].Numero);
                        this.tipo.Add(contatoPesquisa.Telefones[i].Tipo);
                    }
                }
                

                listaDeNumeros();
            }

            else
                MessageBox.Show("Contato não encontrado");
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            string emailexclui = textBoxMail.Text;
            contatos.Remover(new Contato(emailexclui));
            MessageBox.Show("Usuário removido");
            limparForm();
        }

        private void textBoxNum_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            this.nums.Add(textBoxNum.Text);
            this.tipo.Add(comboBoxTipo.Text);

            listaDeNumeros();
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            this.tipo.RemoveAt(this.nums.IndexOf(textBoxNum.Text));
            this.nums.Remove(textBoxNum.Text);
            

            listaDeNumeros();
        }

        private void ListBoxNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBoxTipo.SelectedIndex = listBoxNum.SelectedIndex;
            textBoxNum.Text = listBoxNum.SelectedItem.ToString();
            comboBoxTipo.Text = listBoxTipo.Items[listBoxNum.SelectedIndex].ToString();
        }
    }
}
