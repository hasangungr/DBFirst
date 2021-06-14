using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Entity
{
    public partial class Form2 : Form
    {
        DbSinavEntityEntities db = new DbSinavEntityEntities();
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                var degerler = db.Tbl_Points.Where(p => p.exam1 <= 50);
                dataGridView1.DataSource = degerler.ToList();
            }
            else if (radioButton2.Checked == true)
            {
                var degerler = db.Tbl_Student.Where(p => p.studentName == "Ali");
                dataGridView1.DataSource = degerler.ToList();
            }
            else if (radioButton3.Checked == true)
            {
                var degerler = db.Tbl_Student.Where(p => p.studentName == textBox1.Text || p.studentSurname == textBox1.Text);
                dataGridView1.DataSource = degerler.ToList();
            }
            else if (radioButton4x.Checked == true)
            {
                var degerler = db.Tbl_Student.Select(x => new { Soyad = x.studentSurname });
                dataGridView1.DataSource = degerler.ToList();
            }
            else if (radioButton5.Checked == true)
            {
                var degerler = db.Tbl_Student.Select(x => new { Ad = x.studentName.ToUpper(), Soyad = x.studentSurname.ToLower() });
                dataGridView1.DataSource = degerler.ToList();
            }
            else if (radioButton6.Checked == true)
            {
                var degerler = db.Tbl_Student.Select(x => new
                {
                    Ad = x.studentName.ToUpper(),
                    Soyad = x.studentSurname.ToLower()
                }).Where(x => x.Ad != "Ali");
                dataGridView1.DataSource = degerler.ToList();
            }
            else if (radioButton4.Checked == true)
            {
                var degerler = db.Tbl_Points.Select(x => new
                {
                    ID = x.student,
                    Ortalama = x.average,
                    Durum = x.state == true ? "Geçti " : "Kaldı "    //true eşitse geçti yaz  //syntax
                });
                dataGridView1.DataSource = degerler.ToList();
            }
            else if (radioButton7.Checked == true)
            {
                var degerler = db.Tbl_Points.SelectMany(p => db.Tbl_Student.Where(y => y.studentID == p.student), (p, y) => new
                {
                    Ad_Soyad = y.studentName + " " + y.studentSurname,
                    Ortalama = p.average,
                    Durum = p.state ==true ? "Geçti" : "Kaldı"

                });
                dataGridView1.DataSource = degerler.ToList();
            }
            else if (radioButton8.Checked ==true)
            {
                var degerler = db.Tbl_Student.OrderBy(x => x.studentID).Take(3);
                dataGridView1.DataSource = degerler.ToList();
            }
            else if (radioButton9.Checked == true)
            {
                var degerler = db.Tbl_Student.OrderByDescending(x => x.studentID).Take(3);
                dataGridView1.DataSource = degerler.ToList();
            }
            else if (radioButton10.Checked == true)
            {
                var degerler = db.Tbl_Student.OrderBy(p => p.studentName);
                dataGridView1.DataSource = degerler.ToList();
            }
            else if (radioButton11.Checked == true)
            {
                var degerler = db.Tbl_Student.OrderBy(p => p.studentID).Skip(5);
                dataGridView1.DataSource = degerler.ToList();
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
