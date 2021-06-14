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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        DbSinavEntityEntities db = new DbSinavEntityEntities();
        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //var degerler = db.Tbl_Student.OrderBy(p => p.studentCity).GroupBy(y => y.studentCity).Select(t => new
            //{
            //    Şehir = t.Key,
            //    Toplam = t.Count()
            //}).OrderByDescending(k=>k.Toplam);

            label1.Text = db.Tbl_Points.Max(p => p.average).ToString();

            label2.Text = db.Tbl_Points.Min(p => p.exam1).ToString();



            label3.Text = db.Tbl_Points.Where(p => p.state == false).Max(t => t.average).ToString();


            // dataGridView1.DataSource = degerler.ToList();
        }
    }
}
