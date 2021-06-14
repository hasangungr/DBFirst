using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Entity
{
    public partial class Form1 : Form
    {
        DbSinavEntityEntities entityEntities = new DbSinavEntityEntities();
        Tbl_Student t = new Tbl_Student();
        Tbl_Lesson l = new Tbl_Lesson();
        Tbl_Points p = new Tbl_Points();
        public Form1()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnExamList_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=LAPTOP-H79DC8LN\SQLEXPRESS;Initial Catalog=DbSinavEntity;Integrated Security=True");
            SqlCommand command = new SqlCommand("select * from tbl_lesson", connection);
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void btnStudentList_Click(object sender, EventArgs e)
        {
            //modele ulaşmak için kullanılan sınıf

            //datagridde listeleme
            dataGridView1.DataSource = entityEntities.Tbl_Student.ToList();

            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;

        }

        private void btnPointsList_Click(object sender, EventArgs e)
        {
            //foreach
            //belirli alanları çekme
            var query = from item in entityEntities.Tbl_Points
                        select new
                        {
                            //süslü parantez içinde olan alanları seç
                            item.pointID,
                            item.Tbl_Student.studentName,
                            item.Tbl_Lesson.lessonName,
                            item.exam1,
                            item.exam2,
                            item.exam3,
                            item.average,
                            item.state
                        };
            dataGridView1.DataSource = query.ToList();

            //dataGridView1.DataSource = entityEntities.Tbl_Points.ToList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {



            if (txtStudentSurname.Text.Length > 0 & txtStudentName.Text.Length > 0)
            {
                t.studentName = txtStudentName.Text;
                t.studentSurname = txtStudentSurname.Text;
                entityEntities.Tbl_Student.Add(t);
                entityEntities.SaveChanges();// veritabanına kaydet
                MessageBox.Show("Öğrenci Eklendi");
                txtStudentName.Text = "";
                txtStudentName.Text = "";
                txtStudentPhoto.Text = "";
            }
            else if (txtLessonName.Text.Length > 0)
            {
                l.lessonName = txtLessonName.Text;
                entityEntities.Tbl_Lesson.Add(l);
                entityEntities.SaveChanges();
                MessageBox.Show("Ders Eklendi");
                txtLessonName.Text = "";
            }
            else
            {
                MessageBox.Show("Eklemek istediğiniz Ders veya Öğrenci Kutularında Boş Alanlar Var.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Remove
            if (txtStudentID.Text.Length > 0)
            {
                int id = Convert.ToInt32(txtStudentID.Text); //Textboxta yazılan id tutuyor
                var x = entityEntities.Tbl_Student.Find(id); //tutulan id'yi db'de bulup x'e atıyor
                entityEntities.Tbl_Student.Remove(x); //x'i siliyor
                entityEntities.SaveChanges();
                MessageBox.Show("Öğrenci Silindi");
                txtStudentID.Text = "";
            }
            else if (txtLessonID.Text.Length > 0)
            {
                int id2 = Convert.ToInt32(txtLessonID.Text);
                var x2 = entityEntities.Tbl_Lesson.Find(id2);
                entityEntities.Tbl_Lesson.Remove(x2);
                entityEntities.SaveChanges();
                MessageBox.Show("Ders Silindi");
                txtLessonID.Text = "";
            }
            else
            {
                MessageBox.Show("Silmek istediğiniz Ders veya Öğrenci Kutularında Boş Alanlar Var.");
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            if (txtStudentID.Text.Length > 0 & txtStudentName.Text.Length > 0 & txtStudentSurname.Text.Length > 0)
            {
                int id = Convert.ToInt32(txtStudentID.Text); //Textboxta yazılan id tutuyor
                var x = entityEntities.Tbl_Student.Find(id); //tutulan id'yi db'de bulup x'e atıyor
                x.studentName = txtStudentName.Text;
                x.studentSurname = txtStudentSurname.Text;
                x.studentPhoto = txtStudentPhoto.Text;
                entityEntities.SaveChanges();
                MessageBox.Show("Öğrenci Güncellendi");
                txtStudentID.Text = "";
                txtStudentName.Text = "";
                txtStudentSurname.Text = "";
                txtStudentPhoto.Text = "";

            }
            else if (txtLessonID.Text.Length > 0 & txtLessonName.Text.Length > 0)
            {
                int id2 = Convert.ToInt32(txtLessonID.Text); //Textboxta yazılan id tutuyor
                var x2 = entityEntities.Tbl_Lesson.Find(id2); //tutulan id'yi db'de bulup x'e atıyor
                x2.lessonName = txtLessonName.Text;
                entityEntities.SaveChanges();
                MessageBox.Show("Ders Güncellendi");
                txtLessonID.Text = "";
                txtLessonName.Text = "";
            }
            else
            {
                MessageBox.Show("Güncellemek istediğiniz Ders veya Öğrenci Kutularında Boş Alanlar Var.");
            }


        }

        private void btnProcedure_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = entityEntities.pointsList(); 
        }

        private void btnBul_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = entityEntities.Tbl_Student.Where(x =>x.studentName==txtStudentName.Text |
            x.studentSurname == txtStudentSurname.Text).ToList();
        }

        private void txtStudentName_TextChanged(object sender, EventArgs e)
        {
            string aranan = txtStudentName.Text;
            var degerler = from item in entityEntities.Tbl_Student  
                           where item.studentName.Contains(aranan) //içeriyorsa
                           select item;
            dataGridView1.DataSource = degerler.ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(radioButton1.Checked == true) {
                List<Tbl_Student> list1 = entityEntities.Tbl_Student.OrderBy(p => p.studentName).ToList(); //sıralama,asc
                dataGridView1.DataSource = list1;
            }
            else if(radioButton2.Checked ==true)
            {
                List<Tbl_Student> list2 = entityEntities.Tbl_Student.OrderByDescending(p => p.studentName).ToList(); //sıralama,desc
                dataGridView1.DataSource = list2;

            }
            else if (radioButton3.Checked == true)
            {
                List<Tbl_Student> list3 = entityEntities.Tbl_Student.OrderBy(p => p.studentName).Take(3).ToList(); //sıralama,desc
                dataGridView1.DataSource = list3;

            }
            else if (radioButton4.Checked == true)
            {
                int id;
                id =  Convert.ToInt32(txtStudentID.Text);
                List<Tbl_Student> list4 = entityEntities.Tbl_Student.Where(p => p.studentID == id).ToList();//sıralama,desc
                dataGridView1.DataSource = list4;

            }
            else if (radioButton5.Checked == true)
            {
                bool deger = entityEntities.Tbl_Clubs.Any();
                MessageBox.Show(deger.ToString(),"Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);

            }
            else if (radioButton6.Checked == true)
            {
                int toplam = entityEntities.Tbl_Student.Count();
                MessageBox.Show(toplam.ToString(), "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        
            else if (radioButton7.Checked == true)
            {
                var toplam = entityEntities.Tbl_Points.Sum(p => p.exam1);
                MessageBox.Show("Toplam Sınav-1 Puanı: " , toplam.ToString() , MessageBoxButtons.OK ,MessageBoxIcon.Information);
            }
            else if (radioButton8.Checked == true)
            {
                var ort = entityEntities.Tbl_Points.Average(p=>p.exam1);
                MessageBox.Show("Ortalama Sınav-1 Puanı: "+ ort.ToString());
            }
            else if (radioButton9.Checked == true)
            {
                var ort = entityEntities.Tbl_Points.Average(p => p.exam1);
                var list = entityEntities.Tbl_Points.Where(p => p.exam1 >= ort).ToList();
                dataGridView1.DataSource = list;
                MessageBox.Show("Ortalama Sınav-1 Puanı: " + ort.ToString());
            }
            else if (radioButton10.Checked == true)
            {
                var max1 = entityEntities.Tbl_Points.Max(p => p.exam1);
                var query = from item in entityEntities.Tbl_Points.Where(p => p.exam1 == max1).ToList()
                            select new { item.Tbl_Student.studentName };

                dataGridView1.DataSource = query.ToList();
                            
                MessageBox.Show("En Yüksek Sınav-1 Puanı: " + max1.ToString());
            }
            else if (radioButton11.Checked == true)
            {
                var min1 = entityEntities.Tbl_Points.Min(p => p.exam1);
                var query = from item in entityEntities.Tbl_Points.Where(p => p.exam1 == min1).ToList()
                            select new { item.Tbl_Student.studentName };
                dataGridView1.DataSource = query.ToList();

                MessageBox.Show("En Düşük Sınav-1 Puanı: " + min1.ToString());
            }
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnJoin_Click(object sender, EventArgs e)
        {
            var sorgu = from item in entityEntities.Tbl_Points
                        join item2 in entityEntities.Tbl_Student
                        on item.student equals item2.studentID
                        join item3 in entityEntities.Tbl_Lesson
                        on item.lesson equals item3.lessonID
                        select new
                        {
                            Öğrenci = item2.studentName+" "+item2.studentSurname,
                            Ders = item3.lessonName,

                            Sınav1 = item.exam1,
                            Sınav2 = item.exam2,
                            Sınav3 = item.exam3,
                            Ortalama = item.average
                        };
            dataGridView1.DataSource = sorgu.ToList();
        }
    }
}
