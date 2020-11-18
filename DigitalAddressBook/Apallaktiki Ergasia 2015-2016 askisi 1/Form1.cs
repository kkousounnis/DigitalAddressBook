using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
//periexei typous pou diabazoun arxeia kai grafoun se arxeia
using System.Text.RegularExpressions;
//einai gia ton elenxo tou email kai tou arithmou tilefonou
namespace Apallaktiki_Ergasia_2015_2016_askisi_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        class Person
        {
            public string Name;
            public string LastName; 
            public string Address;
            public string phone;
            public string City;
            public string Relative;
            public string Email;
            public string BirthDate;   
        }
        public class PhoneValidation
        {
            public bool CheckPhone(String Phone)//methodos h opoia elenxei an o arithmos periexei arithmous
            {
                bool IsValid = false;
                if (String.IsNullOrEmpty(Phone))return false;
                Regex r = new Regex(@"^([0-9]+)$");
                if (r.IsMatch(Phone))
                {
                    IsValid = true;
                }
                return IsValid;
            }
        }
         public class EmailValidation
        {
            public bool CheckEmail(string Email)//elenxos gia to email
            {
                bool IsValid = false;
                if (String.IsNullOrEmpty(Email))
                    return false;
                Regex r = new Regex(@"^([a-zA-Z0-9]+)([-!#\$%&'\*\+/=\?\^`\{\}\|~\w]+)@([a-zA-Z0-9]+)(\.[a-z]+)$");
                if(r.IsMatch(Email))
                {
                    IsValid = true;
                }
                return IsValid;
            }
         }
          
        private void Form1_Load(object sender, EventArgs e)
         {
            //kathe fora pou anoigw thn adjeda tha diabazw to arxeio txt kai tha pernaw ta stoixeia to ston pinaka ths adjendas
             StreamReader sr = new StreamReader("Text.txt");
             string line;
             while ((line = sr.ReadLine()) != null)
             {
                 string[] words = line.Split(' ');
                 listView1.Items.Add("").SubItems.AddRange(words);
             }
             sr.Close();
            //bazw ta onomata ta epitheta kai ta thlefwna sta antistoixa combobox gia na mporw na ta anazhthsw
             for (int x = 0; x <= listView1.Items.Count - 1; x++)
             {
                 if (listView1.Items[x].SubItems[1].Text != null) comboBox2.Items.Add(listView1.Items[x].SubItems[1].Text);
                 if (listView1.Items[x].SubItems[2].Text != null) comboBox3.Items.Add(listView1.Items[x].SubItems[2].Text);
                 if (listView1.Items[x].SubItems[3].Text != null) comboBox4.Items.Add(listView1.Items[x].SubItems[3].Text);
             }
            //briskw an kapoios apo auta ta atoma exei genethlia
             for (int i = 0; i <= listView1.Items.Count - 1; i++)
               {
                  string[] date = listView1.Items[i].SubItems[6].Text.Split('/');
                  string[] datenow = dateTimePicker1.Text.Split('/');
                  if (date[0] == datenow[0] && date[1] == datenow[1])
                    {
                       int d = int.Parse(date[2]) - int.Parse(date[2]);
                       MessageBox.Show(listView1.Items[i].SubItems[1].Text + " has birthday today");
                    }
              }
        }      
        private void Add_Click(object sender, EventArgs e)
        {
            int b = 0;
            //dimiourgia antikeimenou tupou Phonevalidation  me onoma c
            PhoneValidation c = new PhoneValidation();
            //dimiourgia antikeimenou tupou emailvalidation  me onoma a
            EmailValidation a = new EmailValidation();
            //dimiourgia antikeimenou tupou person  me onoma p
            Person p = new Person();
            //orismos timwn gia oles tis idiothtes tou person
            p.Name = textBox1.Text;
            p.LastName = textBox2.Text;
            //kalw thn methodo gia na elenksw to thlefwnw kai elenxo an to mhkos einai 10
            if(c.CheckPhone(textBox3.Text) && textBox3.Text.Length==10  )
            p.phone = textBox3.Text;
            else
            {
                MessageBox.Show("phone must have numbers and the size must be 10 ");
                b = 1;
            }
            p.Address = richTextBox1.Text;
            p.City = textBox5.Text;
            p.BirthDate = dateTimePicker1.Text;
            p.Relative = comboBox1.Text;
            //kalw thn methodo gia na elenksw to email
                if (a.CheckEmail(textBox4.Text))               
                    p.Email = textBox4.Text;            
                else
                {
                    MessageBox.Show("wrong email");
                    b = 1;
                }
            //bazw se mia lista oles tis idiothtes tou kathe anthrwpou
            string[] row1 = { p.Name, p.LastName, p.phone,p.Email, p.Address,p.BirthDate, p.City, p.Relative };
            if (b != 1)
            {
                listView1.Items.Add(" ").SubItems.AddRange(row1);
                comboBox2.Items.Add(p.Name);
                comboBox3.Items.Add(p.LastName);
                comboBox4.Items.Add(p.phone);
                Emptytextboxes();
            }        
        }
        //sunartisi pou adiazei ola ta textboxes
        private void Emptytextboxes()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            richTextBox1.Clear();          
            textBox5.Clear();
            comboBox1.Text = "";

        }
        
        private void Edit_Click_1(object sender, EventArgs e)
        {
            int d = 0;
            PhoneValidation c = new PhoneValidation();
            EmailValidation a = new EmailValidation();
            //kalw thn sunarthsh remove
            remove();
            //kathe fora pou diwrthwnw kati na dinei tis kainouries times
            listView1.SelectedItems[0].SubItems[1].Text = textBox1.Text;
            listView1.SelectedItems[0].SubItems[2].Text =  textBox2.Text;
            if (c.CheckPhone(textBox3.Text) && textBox3.Text.Length == 10)
            {
                listView1.SelectedItems[0].SubItems[3].Text = textBox3.Text;
                d=d+1;
            }
            else MessageBox.Show("phone must have numbers and the size must be 10");
            listView1.SelectedItems[0].SubItems[5].Text = richTextBox1.Text;
            listView1.SelectedItems[0].SubItems[6].Text = dateTimePicker1.Text;
            listView1.SelectedItems[0].SubItems[7].Text = textBox5.Text;
            listView1.SelectedItems[0].SubItems[8].Text = comboBox1.Text; 
            if (listView1.SelectedItems[0].SubItems[1].Text != null)
            {
                comboBox2.Items.Add(listView1.SelectedItems[0].SubItems[1].Text);              
            }
            if (listView1.SelectedItems[0].SubItems[2].Text != null)
            {
                comboBox3.Items.Add(listView1.SelectedItems[0].SubItems[2].Text);
            }
            if (listView1.SelectedItems[0].SubItems[1].Text != null)
            {
                comboBox4.Items.Add(listView1.SelectedItems[0].SubItems[3].Text);
            }
            if (a.CheckEmail(textBox4.Text))
            {
                listView1.SelectedItems[0].SubItems[4].Text = textBox4.Text;
                d = d + 1;
            }
            else MessageBox.Show("wrong email");
            if (d == 2) Emptytextboxes();
        }
        private void listView1_MouseClick(object sender, MouseEventArgs e)
        { 
            //kathe fora pou pataw kapoio stoixeio ston pinaka na mou bgazei ola ta stoixeia tou ston pinaka
            textBox1.Text = listView1.SelectedItems[0].SubItems[1].Text;
            textBox2.Text = listView1.SelectedItems[0].SubItems[2].Text;
            textBox3.Text = listView1.SelectedItems[0].SubItems[3].Text;
            textBox4.Text = listView1.SelectedItems[0].SubItems[4].Text;
            richTextBox1.Text = listView1.SelectedItems[0].SubItems[5].Text;
            dateTimePicker1.Text = listView1.SelectedItems[0].SubItems[6].Text;
            textBox5.Text = listView1.SelectedItems[0].SubItems[7].Text;
            comboBox1.Text = listView1.SelectedItems[0].SubItems[8].Text;            
        }
        //sunatisi poy afairei stoixeia apo ton pinaka
        public void remove()
        {
            comboBox2.Items.Remove(listView1.SelectedItems[0].SubItems[1].Text);
            comboBox3.Items.Remove(listView1.SelectedItems[0].SubItems[2].Text);
            comboBox4.Items.Remove(listView1.SelectedItems[0].SubItems[3].Text);
        }
        private void Delete_Click(object sender, EventArgs e)
        {
            remove();
            listView1.SelectedItems[0].Remove();                
        }       
        private void Save_Click(object sender, EventArgs e)
        {
            //dimiourgw ena arxeio txt kai grafw mesa ola ta stoixei poy exei o pinakas
            StreamWriter sw = new StreamWriter("Text.txt");
            
            for (int i = 0; i <= listView1.Items.Count - 1; i++)
            {
                for (int j = 0; j < 8; j++)
                { 
                    sw.Write(listView1.Items[i].SubItems[j+1].Text+" ");
                }
                sw.WriteLine();
            }
            sw.Close();
        }
        public int a = 0;
        public int x = 0;
        private void Show_Click(object sender, EventArgs e)
        {
            if (a ==  1)
            {
                listBox1.Items.Clear();
                
                a = 0;                         
            }
            //edw briskw ta stixoia apo to atomo pou exw epileksei kanw mia anazhthsh
            for (int i = 0; i <= listView1.Items.Count - 1; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    x = 0; 
                    if (listView1.Items[i].SubItems[1].Text == comboBox2.Text && x==0)
                    { 
                        listBox1.Items.Add(listView1.Items[i].SubItems[j].Text);
                        x = 1;
                    }
                    if (listView1.Items[i].SubItems[2].Text == comboBox3.Text && x==0) 
                    {
                        listBox1.Items.Add(listView1.Items[i].SubItems[j].Text);
                        x = 1;
                    }
                    if (listView1.Items[i].SubItems[3].Text == comboBox4.Text && x==0) 
                    {
                        listBox1.Items.Add(listView1.Items[i].SubItems[j].Text);
                        x = 1;
                    }                   
                }                                    
            }
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
            
            a = 1;           
        }       
    }
}
